using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003D7 RID: 983
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SetFolderFieldType : FolderChangeDescriptionType
	{
		// Token: 0x0400156C RID: 5484
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		public BaseFolderType Item1;
	}
}
