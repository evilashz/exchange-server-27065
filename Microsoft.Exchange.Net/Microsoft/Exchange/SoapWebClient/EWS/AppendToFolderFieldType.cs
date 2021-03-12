using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003D5 RID: 981
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class AppendToFolderFieldType : FolderChangeDescriptionType
	{
		// Token: 0x0400156B RID: 5483
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		public BaseFolderType Item1;
	}
}
