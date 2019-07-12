using System;
using System.IO;
using System.Management.Automation;

namespace IDEACmdlets
{
    //Install-Package System.Management.Automation.dll -Version 10.0.10586
    [Cmdlet(VerbsSecurity.Block, "DataWithIDEA")]
    public class IdeaEncrypt : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ValidateNotNull]
        public string Key { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        [ValidateNotNull]
        public string Input { get; set; }


        private readonly String tempOutputFilename = "tempEncryptedData.dat";

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
                WriteError(new ErrorRecord(new Exception("There was an error with the Empty Key argument"), "1", ErrorCategory.InvalidArgument, null));
            }


            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            MemoryStream inputMs = GenerateStreamFromString(Input);

            IdeaCrypt.CryptFile(inputMs, tempOutputFilename, Key, true);

            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            WriteObject(String.Join(" ", File.ReadAllBytes(tempOutputFilename)));
            base.EndProcessing();
        }

        protected override void StopProcessing()
        {
            WriteWarning("stopped");
            base.StopProcessing();
        }



        private static MemoryStream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}

