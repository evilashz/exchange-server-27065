using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000406 RID: 1030
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateFolderRequest : BaseRequest
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0009F0F4 File Offset: 0x0009D2F4
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0009F0FC File Offset: 0x0009D2FC
		[XmlElement("ParentFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "ParentFolderId", IsRequired = true)]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0009F105 File Offset: 0x0009D305
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0009F10D File Offset: 0x0009D30D
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "Folders", IsRequired = true)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] Folders { get; set; }

		// Token: 0x06001D43 RID: 7491 RVA: 0x0009F116 File Offset: 0x0009D316
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateFolder(callContext, this);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0009F11F File Offset: 0x0009D31F
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0009F122 File Offset: 0x0009D322
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0009F135 File Offset: 0x0009D335
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.resources == null)
			{
				this.resources = base.GetResourceKeysForFolderIdHierarchyOperations(true, callContext, this.ParentFolderId.BaseFolderId);
			}
			return this.resources;
		}

		// Token: 0x04001316 RID: 4886
		internal const string ParentFolderIdElementName = "ParentFolderId";

		// Token: 0x04001317 RID: 4887
		internal const string FoldersElementName = "Folders";

		// Token: 0x04001318 RID: 4888
		private ResourceKey[] resources;
	}
}
