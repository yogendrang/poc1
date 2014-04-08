using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApplication2.Processor;
using System.Xml;

namespace ConsoleApplication2.Models
{
    class DLLModel 
    {
        private string dllFileName;
        private bool isManaged;
        private string fullyQualifiedPath;
        private Dictionary<string, ClassModel> allClassesInDLL = new Dictionary<string, ClassModel>();
        private Dictionary<string, ClassModel> userSelectedclassesInDLL = new Dictionary<string, ClassModel>();

        public DLLModel(string fullyQualifiedPath)
        {
            this.fullyQualifiedPath = fullyQualifiedPath;
            isManaged = DLLProcessor.checkIfDLLIsManaged(this.fullyQualifiedPath);
            dllFileName = Path.GetFileName(this.fullyQualifiedPath);
        }

        public Dictionary<string, ConsoleApplication2.Models.ClassModel> getAllClassesInThisDll()
        {
            return allClassesInDLL;
        }

        public Dictionary<string, ConsoleApplication2.Models.ClassModel> getUserSelectedClassesInThisDll()
        {
            return userSelectedclassesInDLL;
        } 

        public string getFullyQualifiedPath()
        {
            return fullyQualifiedPath;
        }

       public DLLModel processDll(DLLModel dllModel)
        {

            return new DLLProcessor().processDLL(dllModel);
        }

        public bool isDLLManaged()
        {
            return this.isManaged;
        }

        public void setDllFileName(string dllFileName)
        {
            this.dllFileName = dllFileName;
        }

        public string getDllFileName()
        {
            return this.dllFileName;
        }

        public string generateCodeForDllContents()
        {
            StringBuilder codeForDllContents = new StringBuilder();
            codeForDllContents.AppendLine("using System.IO;");
            codeForDllContents.AppendLine("using System;");
            codeForDllContents.AppendLine("using Newtonsoft.Json;");
            codeForDllContents.AppendLine("using System.Web.Http;")
                .AppendLine("namespace " + this.getDllFileName() + " {");
            Dictionary<string, ClassModel> userSelectedClassesInDll = this.getUserSelectedClassesInThisDll();
            //Iterate through each class and generate a corresponding controller for it.
            foreach (KeyValuePair<string, ClassModel> pair in userSelectedClassesInDll)
            {
                codeForDllContents.Append(((ClassModel)(pair.Value)).generateCodeForControllerClass());
            }
            codeForDllContents.AppendLine("}");

            return codeForDllContents.ToString();
        }

        public void generateXml()
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(this.dllFileName + ".xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("dll");
                writer.WriteElementString("dllFileName", this.getDllFileName() + ".xml");
                writer.WriteElementString("fullyQualifiedPath", this.getFullyQualifiedPath());
                writer.WriteElementString("isManaged", this.isDLLManaged() + "");
                writer.WriteStartElement("userSelectedClassesInDll");
                
                Dictionary<string, ClassModel> classesInDll = this.getUserSelectedClassesInThisDll();
                foreach (KeyValuePair<string, ClassModel> pair in classesInDll)
                {
                    ClassModel classAtHand = pair.Value;
                    XmlReader xmlReaderForClass = classAtHand.generateXml();
                    
                    writer.WriteNode(xmlReaderForClass, true);
                    
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }
    }
}
