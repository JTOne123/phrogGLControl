#region --- License ---
//
// The Open Toolkit Library License
//
// Copyright (c) 2017 phroggie
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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if NETSTANDARD1_0
[assembly: AssemblyTitle("phrogGLControl .NET Standard 1.0")]
#elif NETSTANDARD1_3
[assembly: AssemblyTitle("phrogGLControl .NET Standard 1.3")]
#elif PORTABLE40
[assembly: AssemblyTitle("phrogGLControl Portable .NET 4.0")]
#elif PORTABLE
[assembly: AssemblyTitle("phrogGLControl Portable")]
#elif DOTNET
[assembly: AssemblyTitle("phrogGLControl .NET Platform")]
#elif NET20
[assembly: AssemblyTitle("phrogGLControl .NET 2.0")]
[assembly: AllowPartiallyTrustedCallers]
#elif NET35
[assembly: AssemblyTitle("phrogGLControl .NET 3.5")]
[assembly: AllowPartiallyTrustedCallers]
#elif NET40
[assembly: AssemblyTitle("phrogGLControl .NET 4.0")]
[assembly: AllowPartiallyTrustedCallers]
#else
[assembly: AssemblyTitle("phrogGLControl")]
[assembly: AllowPartiallyTrustedCallers]
#endif

[assembly: AssemblyDescription("An extended OpenTK.GLControl fork allowing for additional design-time features")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("phroggiesoft")]
[assembly: AssemblyProduct("phrogGLControl")]
[assembly: AssemblyCopyright("Copyright © 2017 phroggie")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if HAVE_COM_ATTRIBUTES
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e01a6072-b97a-475e-a405-9d56308956b3")]
#endif

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.5.3.99")]
[assembly: AssemblyFileVersion("0.5.3.99")]
[assembly: CLSCompliant(true)]
