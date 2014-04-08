using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Models
{
    class ParameterModel : iModel
    {
        private MethodModel methodThisParameterBelongsTo;
        private string parameterName;
        //place holder for actual type in case type is changed to string if its complex
        private Type actualType;
        private Type typeOfParameter;
        private int positionOfParameter;
        private bool isComplexType;

        public ParameterModel(string parameterName, Type typeOfParameter, int positionOfParameter, MethodModel methodThisParameterBelongsTo)
        {
            this.parameterName = parameterName;
            this.typeOfParameter = typeOfParameter;
            this.actualType = actualType;
            this.positionOfParameter = positionOfParameter;
            this.methodThisParameterBelongsTo = methodThisParameterBelongsTo;
            this.isComplexType = determineIfParamIsComplex(typeOfParameter);
            if (this.isComplexType)
            {
                this.parameterName += "TO";
                Console.WriteLine("Complex Type encountered");
                this.typeOfParameter = this.parameterName.GetType();
            }
        }

        public string getParameterName()
        {
            return parameterName;
        }

        public Type getTypeOfParameter()
        {
            return typeOfParameter;
        }

        public int getPositionOfParameter()
        {
            return this.positionOfParameter;
        }

        public void setPositionOfParameter(int positionOfParameter)
        {
            this.positionOfParameter = positionOfParameter;
        }

        public string generateXMLForStructure()
        {
            StringBuilder xmlStructure = new StringBuilder();

            return xmlStructure.ToString();
        }

        private bool determineIfParamIsComplex(Type typeOfParameter)
        {
            bool isParamComplexType = false;
            if (!typeOfParameter.Namespace.StartsWith("System"))
            {
                isParamComplexType = true;
            }
            return isParamComplexType;
        }

        public bool isParameterComplex() {
            return this.isComplexType;
        }

        public XmlReader generateXml()
        {
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;
            writerSettings.Indent = true;

            XmlWriter paramWriter = XmlWriter.Create(stream, writerSettings);
            paramWriter.WriteStartDocument();

            paramWriter.WriteStartElement("parameter");

            paramWriter.WriteElementString("parameterName", this.getParameterName());
            paramWriter.WriteElementString("positionOfParameter", this.getPositionOfParameter() + "");
            paramWriter.WriteElementString("parameterType", this.getTypeOfParameter() + "");

            paramWriter.WriteEndElement();
            paramWriter.WriteEndDocument();
            paramWriter.Flush();
            paramWriter.Close();

            stream.Position = 0;
            XmlReader xmlReader = XmlReader.Create(stream);
            return xmlReader;
        }
    }
}
