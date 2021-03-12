using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FB RID: 1019
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("BaseMoveCopyFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(MoveFolderRequest))]
	[KnownType(typeof(CopyFolderRequest))]
	[KnownType(typeof(MoveFolderRequest))]
	[XmlInclude(typeof(CopyFolderRequest))]
	public class BaseMoveCopyFolderRequest : BaseRequest
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0009E885 File Offset: 0x0009CA85
		// (set) Token: 0x06001CDA RID: 7386 RVA: 0x0009E88D File Offset: 0x0009CA8D
		[XmlElement("ToFolderId")]
		[DataMember(IsRequired = true, Order = 1)]
		public TargetFolderId ToFolderId { get; set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0009E896 File Offset: 0x0009CA96
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0009E89E File Offset: 0x0009CA9E
		[DataMember(Name = "FolderIds", IsRequired = true, Order = 2)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("FolderIds")]
		public BaseFolderId[] Ids { get; set; }

		// Token: 0x06001CDD RID: 7389 RVA: 0x0009E8A8 File Offset: 0x0009CAA8
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids)
			{
				this.ToFolderId.BaseFolderId
			};
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0009E8D3 File Offset: 0x0009CAD3
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0009E8D6 File Offset: 0x0009CAD6
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.ToFolderId.BaseFolderId);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0009E8EC File Offset: 0x0009CAEC
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			if (this.toFolderResourceKey == null)
			{
				BaseServerIdInfo serverInfoForFolderIdHierarchyOperations = BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.ToFolderId.BaseFolderId);
				if (serverInfoForFolderIdHierarchyOperations != null)
				{
					this.toFolderResourceKey = serverInfoForFolderIdHierarchyOperations.ToResourceKey(true);
				}
			}
			ResourceKey[] array = null;
			BaseServerIdInfo serverInfoForFolderIdHierarchyOperations2 = BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.Ids[currentStep]);
			if (serverInfoForFolderIdHierarchyOperations2 != null)
			{
				array = serverInfoForFolderIdHierarchyOperations2.ToResourceKey(false);
			}
			List<ResourceKey> list = new List<ResourceKey>();
			if (this.toFolderResourceKey != null)
			{
				list.AddRange(this.toFolderResourceKey);
			}
			if (array != null)
			{
				list.AddRange(array);
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x040012F0 RID: 4848
		public const string ToFolderIdElementName = "ToFolderId";

		// Token: 0x040012F1 RID: 4849
		public const string FolderIdsElementName = "FolderIds";

		// Token: 0x040012F2 RID: 4850
		private ResourceKey[] toFolderResourceKey;
	}
}
