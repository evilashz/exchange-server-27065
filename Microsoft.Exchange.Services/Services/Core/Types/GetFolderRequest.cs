using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000435 RID: 1077
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetFolderRequest : BaseRequest, IRemoteArchiveRequest
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x000A0E6A File Offset: 0x0009F06A
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x000A0E72 File Offset: 0x0009F072
		[DataMember(Name = "FolderShape", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "FolderShape")]
		public FolderResponseShape FolderShape { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x000A0E7B File Offset: 0x0009F07B
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x000A0E83 File Offset: 0x0009F083
		[DataMember(Name = "ShapeName", IsRequired = false, Order = 2)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000A0E8C File Offset: 0x0009F08C
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x000A0E94 File Offset: 0x0009F094
		[XmlArray("FolderIds")]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "FolderIds", IsRequired = true, Order = 3)]
		public BaseFolderId[] Ids { get; set; }

		// Token: 0x06001F9C RID: 8092 RVA: 0x000A0EA0 File Offset: 0x0009F0A0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return ((IRemoteArchiveRequest)this).GetRemoteArchiveServiceCommand(callContext);
			}
			return new GetFolder(callContext, this);
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000A0ECA File Offset: 0x0009F0CA
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000A0ECD File Offset: 0x0009F0CD
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdListHierarchyOperations(callContext, this.Ids);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000A0EE8 File Offset: 0x0009F0E8
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Ids == null || this.Ids.Length < taskStep)
			{
				return null;
			}
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return null;
			}
			return base.GetResourceKeysForFolderIdHierarchyOperations(false, callContext, this.Ids[taskStep]);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000A0F2A File Offset: 0x0009F12A
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000A0F5C File Offset: 0x0009F15C
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000A0F76 File Offset: 0x0009F176
		// (set) Token: 0x06001FA3 RID: 8099 RVA: 0x000A0F7E File Offset: 0x0009F17E
		ExchangeServiceBinding IRemoteArchiveRequest.ArchiveService { get; set; }

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000A0FB4 File Offset: 0x0009F1B4
		bool IRemoteArchiveRequest.IsRemoteArchiveRequest(CallContext callContext)
		{
			return ComplianceUtil.TryCreateArchiveService(callContext, this, this.Ids != null, delegate
			{
				((IRemoteArchiveRequest)this).ArchiveService = ComplianceUtil.GetArchiveServiceForFolder(callContext, this.Ids);
			});
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000A0FF9 File Offset: 0x0009F1F9
		ServiceCommandBase IRemoteArchiveRequest.GetRemoteArchiveServiceCommand(CallContext callContext)
		{
			return new GetRemoteArchiveFolder(callContext, this);
		}

		// Token: 0x040013ED RID: 5101
		internal const string FolderIdsElementName = "FolderIds";
	}
}
