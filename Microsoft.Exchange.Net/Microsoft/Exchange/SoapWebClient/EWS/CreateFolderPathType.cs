using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200047C RID: 1148
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateFolderPathType : BaseRequestType
	{
		// Token: 0x04001795 RID: 6037
		public TargetFolderIdType ParentFolderId;

		// Token: 0x04001796 RID: 6038
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] RelativeFolderPath;
	}
}
