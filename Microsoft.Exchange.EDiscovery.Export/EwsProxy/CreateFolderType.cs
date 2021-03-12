using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A4 RID: 932
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CreateFolderType : BaseRequestType
	{
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0002A5D7 File Offset: 0x000287D7
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0002A5DF File Offset: 0x000287DF
		public TargetFolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0002A5E8 File Offset: 0x000287E8
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0002A5F0 File Offset: 0x000287F0
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] Folders
		{
			get
			{
				return this.foldersField;
			}
			set
			{
				this.foldersField = value;
			}
		}

		// Token: 0x04001356 RID: 4950
		private TargetFolderIdType parentFolderIdField;

		// Token: 0x04001357 RID: 4951
		private BaseFolderType[] foldersField;
	}
}
