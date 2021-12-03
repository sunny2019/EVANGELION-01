@echo off
for %%i in (*.proto) do (
   echo gen %%~nxi...
   tool\protoc.exe --csharp_out=../Main/Assets/Scripts/Scripts_HotFix/Utilities/ExProtobuf/ProtoCS/  %%~nxi)

echo finish... 
pause