using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200039B RID: 923
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateFolderPathType : BaseRequestType
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0002A498 File Offset: 0x00028698
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x0002A4A0 File Offset: 0x000286A0
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0002A4A9 File Offset: 0x000286A9
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x0002A4B1 File Offset: 0x000286B1
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] RelativeFolderPath
		{
			get
			{
				return this.relativeFolderPathField;
			}
			set
			{
				this.relativeFolderPathField = value;
			}
		}

		// Token: 0x04001343 RID: 4931
		private TargetFolderIdType parentFolderIdField;

		// Token: 0x04001344 RID: 4932
		private BaseFolderType[] relativeFolderPathField;
	}
}
