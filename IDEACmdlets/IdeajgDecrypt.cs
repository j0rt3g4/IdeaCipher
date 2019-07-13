using System;
using System.IO;
using System.Management.Automation;

namespace IDEACmdlets
{
    //Install-Package System.Management.Automation.dll -Version 10.0.10586
    [Cmdlet(VerbsSecurity.Unprotect, "ClientAndSecret")]
    public class IdeajgDecrypt : Cmdlet
    {
        private readonly string Key = "This is my super secret key";

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        [ValidateNotNull]
        public string Input { get; set; }


        private String _inputFilename = "In.txt";
        private String _tempInputFilename = "tempPlainText.txt";
        private String tempOutputFilename = "tempEncryptedData.dat";

        protected override void BeginProcessing()
        {
            //create tmp file
            // File.WriteAllText(tempInputFilename, _inputFilename);
            if (string.IsNullOrWhiteSpace(Input))
            {
                WriteError(new ErrorRecord(new Exception("There was an error with the Empty Input argument"), "0", ErrorCategory.InvalidArgument, null));
            }

            if (string.IsNullOrWhiteSpace(Key))
            {
                WriteError(new ErrorRecord(new Exception("There was an error with the Empty Key argument"), "0", ErrorCategory.InvalidArgument, null));
            }


            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            String[] values = Input.Split(' ');
            byte[] bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                bytes[i] = Byte.Parse(values[i]);
            }


            File.WriteAllBytes(tempOutputFilename, bytes);
            try
            {
                IdeaCrypt.CryptFile(tempOutputFilename, _tempInputFilename, Key, false);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(new Exception($"There was an error with the Empty Key argument\n Message: {ex.Message}"), "0", ErrorCategory.InvalidArgument, null));
            }
            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            if (File.Exists(_inputFilename))
            {
                File.Delete(_inputFilename);
            }
            WriteObject(File.ReadAllText(_tempInputFilename));
            base.EndProcessing();
        }

        protected override void StopProcessing()
        {
            WriteWarning("stopped");
            base.StopProcessing();
        }
    }
}
