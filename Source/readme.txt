phrogGLControl Notes for NuGet consumers:
------------------------------------------

This package is intended to be a drop-in replacement for
OpenTK.GLControl, while offering some distinct advantages to interested
parties:
    Enhanced design-time support
    Run-time context shifting (many features are in-progress)

To add a new GLControl in Visual Studio 2015:
    1) Build your project (or run `nuget restore`).
    2) Copy packages/OpenTK.2.0.0/lib/net20/OpenTK.dll
        to  packages/phrogGLControl.x.y.z/lib/net462/
        (alongside phrogGLControl.dll)
    3) Open a designable component, then right-click in the "Toolbox"
         panel, and click "Chose Items...".
    4) Click the "Browse..." button, and select the phrogGLControl.dll
         file from the packages/phrogGLControl.x.y.z/lib/net462/ folder.
    5) Click OK to dismiss the dialog, then drag and drop a GLControl
         from the toolbox.

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

Note that you will likely want to add an Application.Idle delegate to call `phrogGLControl1.Invalidate()`
in order to force painting. This  may change to be handled by the phrogGLControl library at some point.
(TODO: handle this in phrogGLControl, and add timing to allow for measured callbacks, or something.)
