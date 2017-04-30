phrogGLControl
======

![phrogGLControl logo](Logo.png)

An extended [OpenTK](https://opentk.github.io/).GLControl fork allowing for additional design-time features.

### Installation:

`nuget install Phroggiesoft.phrogGLControl`

To *add* new phrogGLControls to a container in the designer (not required to edit pre-existing controls):
   1. Reference phrogGLControl.dll, and build your project.
   2. Open a designable component, then right-click in the "Toolbox" panel, and click "Chose Items...".
   3. Click the *Browse...* button, and select the `phrogGLControl.dll` file from your project's `bin\$(Configuration)` folder.
   4. Click *OK* to dismiss the dialog, then drag and drop a phrogGLControl from the toolbox.

