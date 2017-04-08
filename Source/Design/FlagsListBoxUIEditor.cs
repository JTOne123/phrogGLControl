#region --- License ---
//
// The "Unknown" CodeProject License
//
// Author:
//      CodeProject user 'LogicNP', with modifications by phroggie
//
// Copyright (c) 2006 CodeProject user 'LogicNP'
//      https://www.codeproject.com/Articles/13793/A-UITypeEditor-for-easy-editing-of-flag-enum-prope
// Copyright (c) 2017 phroggie
//
// TODO: Licensing terms for this source code document are unknown...
//       Major credit to non-active 'LogicNP'... I wish that you'd get back regarding licensing.
//
// Barring any conflicts with "The 'Unknown' CodeProject License", any departures from,
//      or modifications applied to, said abandoned source code are licensed as follows:
//
// The Open Toolkit Library License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Phroggiesoft.Controls.Design
{
    // This is a UI editor for design and run-time manipulations of flag enumerations.
    // It doesn't have any fancy "magic"; merely a CheckedListBox that allows multiple
    // selections showing the names of the enum items.
    //
    // TODO: migrate this to something functionally similar that has an identifiable license.
    //      It should also not be explicitly tied to integers. Valid enum types are:
    //      byte, sbyte, short, ushort, int, uint, long, or ulong
    internal class FlagsListBoxUIEditor : UITypeEditor
    {
        #region --- Nesting doll first stage: UITypeEditor ---

        private FlagsListBox _flagsLB;

        public FlagsListBoxUIEditor()
        {
            _flagsLB = new FlagsListBox();
            _flagsLB.BorderStyle = BorderStyle.None;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                IWindowsFormsEditorService wfes = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (wfes != null)
                {
                    Enum e = (Enum)Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
                    _flagsLB.EnumValue = e;
                    wfes.DropDownControl(_flagsLB);
                    return _flagsLB.EnumValue;
                }
            }
            return null;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        #endregion // --- Nesting doll first stage: UITypeEditor ---

        #region --- Nesting doll middle and inner stage- FlagsListBox : CheckedListBox; FlagsListBoxItem : Object---

        private class FlagsListBox : CheckedListBox
        {
            #region --- Nesting doll middle stage: FlagsListBox : CheckedListBox ---

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Enum EnumValue
            {
                get
                {
                    return Enum.ToObject(enumType, GetCurrentValue()) as Enum;
                }
                set
                {
                    Items.Clear();
                    enumValue = value;
                    enumType = value.GetType();
                    Populate();
                    ApplyEnumValue();
                }
            }

            public FlagsListBox()
            {
                SuspendLayout();
                CheckOnClick = true;
                ResumeLayout(false);
            }

            public IntFlagsListBoxItem Add(int v, string c)
            {
                IntFlagsListBoxItem item = new IntFlagsListBoxItem(v, c);
                return Add(item);
            }

            public IntFlagsListBoxItem Add(IntFlagsListBoxItem item)
            {
                Items.Add(item);
                return item;
            }

            public int GetCurrentValue()
            {
                int sum = 0;

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i] as IntFlagsListBoxItem;
                    if (GetItemChecked(i))
                        sum |= item.Value;
                }

                return sum;
            }


            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            protected override void OnItemCheck(ItemCheckEventArgs ice)
            {
                base.OnItemCheck(ice);

                if (suspendUpdates)
                    return;

                IntFlagsListBoxItem item = Items[ice.Index] as IntFlagsListBoxItem;
                UpdateCheckedItems(item, ice.NewValue);
            }

            private void InnerUpdateCheckedItems(int value)
            {
                suspendUpdates = true;

                for (int i = 0; i < Items.Count; i++)
                {
                    IntFlagsListBoxItem item = Items[i] as IntFlagsListBoxItem;

                    if (item.Value == 0)
                        SetItemChecked(i, (value == 0));
                    else
                        SetItemChecked(i, ((item.Value & value) == item.Value && item.Value != 0));
                }

                suspendUpdates = false;
            }

            private void UpdateCheckedItems(IntFlagsListBoxItem items, CheckState cs)
            {
                if (items.Value == 0)
                    InnerUpdateCheckedItems(0);

                int sum = GetCurrentValue();
                if (cs == CheckState.Unchecked)
                    sum = sum & ~items.Value;
                else
                    sum |= items.Value;

                InnerUpdateCheckedItems(sum);
            }


            private IContainer components = null;
            private Type enumType;
            private Enum enumValue;
            private bool suspendUpdates = false;

            private void ApplyEnumValue()
            {
                InnerUpdateCheckedItems((int)Convert.ChangeType(enumValue, typeof(int)));
            }

            private void Populate()
            {
                foreach (var name in Enum.GetNames(enumType))
                {
                    var val = Enum.Parse(enumType, name);
                    var intVal = (int)Convert.ChangeType(val, typeof(int));
                    Add(intVal, name);
                }
            }

            #endregion // --- Nesting doll middle stage: CheckedListBox ---

            #region --- Nesting doll inner stage: FlagsListBoxItem<T> ---

            // Represents an enum value item's value and name.
            public abstract class FlagsListBoxItem<T>
            {
                public string Caption { get; private set; }
                public T Value { get; private set; }

                public abstract bool ContainsFlag { get; }

                public FlagsListBoxItem(T v, string c)
                {
                    Caption = c;
                    Value = v;
                }

                public override string ToString()
                {
                    return Caption;
                }
            }

            public class ByteFlagsListBoxItem : FlagsListBoxItem<byte>
            {
                public ByteFlagsListBoxItem(byte v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class SByteFlagsListBoxItem : FlagsListBoxItem<sbyte>
            {
                public SByteFlagsListBoxItem(sbyte v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class ShortFlagsListBoxItem : FlagsListBoxItem<short>
            {
                public ShortFlagsListBoxItem(short v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class UShortFlagsListBoxItem : FlagsListBoxItem<ushort>
            {
                public UShortFlagsListBoxItem(ushort v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class IntFlagsListBoxItem : FlagsListBoxItem<int>
            {
                public IntFlagsListBoxItem(int v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class UIntFlagsListBoxItem : FlagsListBoxItem<uint>
            {
                public UIntFlagsListBoxItem(uint v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class LongFlagsListBoxItem : FlagsListBoxItem<long>
            {
                public LongFlagsListBoxItem(long v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            public class ULongFlagsListBoxItem : FlagsListBoxItem<ulong>
            {
                public ULongFlagsListBoxItem(ulong v, string c) : base(v, c) { }

                public override bool ContainsFlag
                {
                    get
                    {
                        return ((Value & (Value - 1)) == 0);
                    }
                }
            }

            #endregion // --- Nesting doll inner stage: FlagsListBoxItem<T> ---
        }

        #endregion // --- Nesting doll middle and inner stages: CheckedListBox, FlagsListBoxItem ---
    }
}
