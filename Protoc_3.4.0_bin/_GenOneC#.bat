@echo gen %~nx1...
@tool\protoc.exe --csharp_out=../Main/Assets/Scripts/Scripts_HotFix/Utilities/ExProtobuf/ProtoCS/  %~nx1
@echo finish... 
@pause