using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000786 RID: 1926
	[XmlType(TypeName = "FindFolderParentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FindFolderParentWrapper : FindParentWrapperBase
	{
		// Token: 0x06003994 RID: 14740 RVA: 0x000CB733 File Offset: 0x000C9933
		public FindFolderParentWrapper()
		{
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000CB73B File Offset: 0x000C993B
		internal FindFolderParentWrapper(BaseFolderType[] folders, BaseFolderType parentFolder, BasePageResult paging) : base(paging)
		{
			this.Folders = folders;
			this.ParentFolder = parentFolder;
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x000CB752 File Offset: 0x000C9952
		// (set) Token: 0x06003997 RID: 14743 RVA: 0x000CB75A File Offset: 0x000C995A
		[XmlArrayItem("Folder", typeof(FolderType), IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), IsNullable = false)]
		[DataMember(IsRequired = true)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), IsNullable = false)]
		public BaseFolderType[] Folders { get; set; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003998 RID: 14744 RVA: 0x000CB763 File Offset: 0x000C9963
		// (set) Token: 0x06003999 RID: 14745 RVA: 0x000CB76B File Offset: 0x000C996B
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public BaseFolderType ParentFolder { get; set; }
	}
}
