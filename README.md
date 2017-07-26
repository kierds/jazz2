# ![Jazz² Resurrection](https://github.com/deathkiller/jazz2/raw/master/Docs/Logo.gif)
Jazz² Resurrection is reimplementation of game ***Jazz Jackrabbit 2*** from year 1998. Supports various versions of the game (Shareware Demo, Holiday Hare '98, The Secret Files and Christmas Chronicles). Also, it partially supports some features of [JJ2+](http://jj2.plus/) extension.


## Dependencies
### Windows
* .NET Framework 4.5.2 (or newer)
* [OpenALSoft](https://github.com/opentk/opentk-dependencies)
  * Copy `x86/openal32.dll` to `‹Game›/Extensions/OpenALSoft32.dll`
  * Copy `x64/openal32.dll` to `‹Game›/Extensions/OpenALSoft64.dll`
* [libopenmpt](http://lib.openmpt.org/libopenmpt/)
  * Copy `libopenmpt.dll` (and its dependencies) to `‹Game›` directory

### Linux
* [Mono](http://www.mono-project.com/download/) 5.0 (or newer)
* OpenAL
* [libopenmpt](http://lib.openmpt.org/libopenmpt/)
  * Copy `libopenmpt.so` (and its dependencies) to `‹Game›` directory

### Android
* Xamarin
* [libopenmpt](http://lib.openmpt.org/libopenmpt/) (included)

Requires [Microsoft Visual Studio 2017](https://www.visualstudio.com/) (or equivalent Mono compiler) to build the solution.

## Running the application
### Windows/Linux
* Build the solution
* Copy `Content` directory to `‹Game›/Content`
* Run `‹Game›/Import.exe "Path to JJ2"` (or drag and drop JJ2 directory on `Import.exe`)
* Run `‹Game›/Jazz2.exe`

*You can run `Import.exe` to show additional options.*

### Android
* Build the solution
* Run `‹Game›/Import.exe "Path to JJ2"` (or drag and drop JJ2 directory on `Import.exe`)
* Copy `‹Game›/Content` directory to `‹SDCard›/Jazz2.Android/Content` 
* Copy files from `Jazz2.Android/Shaders` directory to `‹SDCard›/Jazz2.Android/Content/Shaders` 
* Copy files from `Jazz2.Android/Shaders/Internal` directory to `‹SDCard›/Jazz2.Android/Content/Internal`
* *Create empty file `.nomedia` in `‹SDCard›/Jazz2.Android` to hide game files in Android Gallery (optimal)*
* Install APK file on Android
* Run the application

*Requires device with Android 4.4 (or newer) and OpenGL ES 3.0. `‹SDCard›` could be internal or external storage. The application tries to autodetect correct path. Also, you can use `‹SDCard›/Android/Data/Jazz2.Android` or `‹SDCard›/Download/Jazz2.Android` instead.*

## License
This software is licensed under the [GNU General Public License v3.0](./LICENSE).