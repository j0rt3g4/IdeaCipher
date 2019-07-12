# IdeaCipher
C# implementation of Idea block cipher.
https://en.wikipedia.org/wiki/International_Data_Encryption_Algorithm


Open SLN.
Compile the projet

# How to use the IDEACmdlets
- Go to the Realease or BIN Folder
- Copy all the content into $env:PSModulePath.split(';') search for any convenient path on the list.
> [!NOTE]
> **based on [StackOverflow Answer](https://stackoverflow.com/questions/12457652/powershell-v2-0-modules-default-load-path-user-windows-system-folder/45788304#45788304)**
- Create a folder IDEACmdlets
- Copy the content from /bin/release or /bin/debug into the folder
- Open powershell
- Run import-module "<path>\IDEACmdlets.dll" (ex: import-module D:\Libs\Documents\WindowsPowerShell\Modules\IDEACmdlets\IDEACmdlets.dll)
- now you can use 
  
  ```Powershell
  Block-DataWithIDEA -key "yourKey" -input "your message to be encripted"
  #or
  Unblock-DataWithIDEA -Key "YourKey" -Input "90 223 1 250 41 144 183 36 107 118 226 255 138 227 216 226 201 154 158 70 3 168 245 191"
  ```

Keys are case sensitive.




 
