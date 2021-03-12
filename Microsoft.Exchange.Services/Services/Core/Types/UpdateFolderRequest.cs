using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000495 RID: 1173
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UpdateFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateFolderRequest : BaseRequest
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x000A368F File Offset: 0x000A188F
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x000A3697 File Offset: 0x000A1897
		[XmlArrayItem(ElementName = "FolderChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(FolderChange))]
		[DataMember(Name = "FolderChanges", IsRequired = true)]
		[XmlArray(ElementName = "FolderChanges", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FolderChange[] FolderChanges { get; set; }

		// Token: 0x060022F4 RID: 8948 RVA: 0x000A36A0 File Offset: 0x000A18A0
		protected override List<ServiceObjectId> GetAllIds()
		{
			List<ServiceObjectId> list = new List<ServiceObjectId>();
			foreach (FolderChange folderChange in this.FolderChanges)
			{
				list.Add(folderChange.FolderId);
			}
			return list;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A36D9 File Offset: 0x000A18D9
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateFolder(callContext, this);
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000A36E2 File Offset: 0x000A18E2
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A36E5 File Offset: 0x000A18E5
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.FolderChanges == null || this.FolderChanges.Length == 0)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.FolderChanges[0].FolderId);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000A3710 File Offset: 0x000A1910
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.FolderChanges == null || this.FolderChanges.Length == 0)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForFolderIdHierarchyOperations = BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.FolderChanges[taskStep].FolderId);
			return BaseRequest.ServerInfoToResourceKeys(true, serverInfoForFolderIdHierarchyOperations);
		}

		// Token: 0x0400151E RID: 5406
		internal const string FolderChangesElementName = "FolderChanges";
	}
}
