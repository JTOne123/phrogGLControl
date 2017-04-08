phrogGLControl Notes for NuGet consumers:
------------------------------------------

This package is designed to be a drop-in replacement for OpenTK.GLControl,
while offering some distinct advantages to interested parties:
	Enhanced design-time support
	Run-time context shifting (many features are in-progress)

To use the design-time features in Visual Studio (2015, likely newer, maybe older):
	1) Set/ensure that OpenTK.dll.config "Copy to output directory" property is set to "Copy if newer."
		(Not mandatory, but will prevent some cross-platform issues as OpenTK NuGet package fails to do this)
	2) Build your project in Release mode, then open a designer-enabled Component, such as a Control or Form.
	3) Right-click in the "Toolbox" panel, and click "Chose Items...".
	4) Click the "Browse..." button, and select the phrogGLControl.dll file from your built project's bin\release folder.

-------------------------------------------

Check out the OpenTK documentation for information on how to actually use this in practice.
Typically, you will want to hook in to the GLControl.Load and GLControl.Paint events:
	.Load: Fires once before the control is first shown. Useful for setting initial matrices, vectors, etc.
	.Paint: Fires every time the control needs to paint (render).

Important: do not attempt to invoke any GL method calls before the .Load event has fired. To handle this gracefully, try:
        private void phrogGLControl1_Load(object sender, EventArgs e)
        {
            phrogGLControl1.Paint += phrogGLControl1_Paint;
        }

Note that you will still likely want to add an Application.Idle delegate to call `phrogGLControl1.Invalidate()`
in order to force painting. This  may change to be handled by the phrogGLControl library at some
point. Set VSync false or true to limit painting frequency.
(TODO: handle this in phrogGLControl, and add timing to allow for measured callbacks, or something.)
