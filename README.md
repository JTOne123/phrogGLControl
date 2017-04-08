phrogGLControl
======

![phrogGLControl logo](Logo.png)

An extended [OpenTK](https://opentk.github.io/).GLControl fork allowing for additional design-time features.

### Installation:

`nuget install Phroggiesoft.phrogGLControl`

To *add* new phrogGLControls to a container in the designer (not required to edit pre-existing controls):
   1. Build your project (or run `nuget restore`).
   2. Copy `packages/OpenTK.2.0.0/lib/net20/OpenTK.dll` over to `packages/phrogGLControl.x.y.z/lib/net462/`.
   3. Open a designable component, then right-click in the "Toolbox" panel, and click "Chose Items...".
   4. Click the *Browse...* button, and select the `phrogGLControl.dll` file from the `packages/phrogGLControl.x.y.z/lib/net462/` folder.
   5. Click *OK* to dismiss the dialog, then drag and drop a phrogGLControl from the toolbox.

