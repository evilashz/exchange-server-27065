using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000668 RID: 1640
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderHierarchyCreateOrUpdateType : SyncFolderHierarchyChangeBase
	{
		// Token: 0x06003232 RID: 12850 RVA: 0x000B78ED File Offset: 0x000B5AED
		public SyncFolderHierarchyCreateOrUpdateType()
		{
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000B78F5 File Offset: 0x000B5AF5
		public SyncFolderHierarchyCreateOrUpdateType(BaseFolderType folder, bool isUpdate)
		{
			this.Folder = folder;
			this.isUpdate = isUpdate;
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000B790B File Offset: 0x000B5B0B
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x000B7913 File Offset: 0x000B5B13
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		[DataMember(Name = "Folder", EmitDefaultValue = false)]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		public BaseFolderType Folder { get; set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000B791C File Offset: 0x000B5B1C
		public override SyncFolderHierarchyChangesEnum ChangeType
		{
			get
			{
				if (!this.isUpdate)
				{
					return SyncFolderHierarchyChangesEnum.Create;
				}
				return SyncFolderHierarchyChangesEnum.Update;
			}
		}

		// Token: 0x04001CA2 RID: 7330
		private bool isUpdate;
	}
}
