using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000410 RID: 1040
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(FolderId))]
	[KnownType(typeof(DistinguishedFolderId))]
	[XmlType("DeleteFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DeleteFolderRequest : BaseRequest
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0009F637 File Offset: 0x0009D837
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x0009F63F File Offset: 0x0009D83F
		[XmlArray("FolderIds")]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "FolderIds", IsRequired = true, Order = 1)]
		public BaseFolderId[] Ids { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0009F648 File Offset: 0x0009D848
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0009F650 File Offset: 0x0009D850
		[XmlAttribute("DeleteType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[IgnoreDataMember]
		public DisposalType DeleteType { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0009F659 File Offset: 0x0009D859
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0009F666 File Offset: 0x0009D866
		[DataMember(Name = "DeleteType", IsRequired = true, Order = 2)]
		[XmlIgnore]
		public string DeleteTypeString
		{
			get
			{
				return EnumUtilities.ToString<DisposalType>(this.DeleteType);
			}
			set
			{
				this.DeleteType = EnumUtilities.Parse<DisposalType>(value);
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0009F674 File Offset: 0x0009D874
		protected override List<ServiceObjectId> GetAllIds()
		{
			return new List<ServiceObjectId>(this.Ids);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0009F681 File Offset: 0x0009D881
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DeleteFolder(callContext, this);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0009F68A File Offset: 0x0009D88A
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0009F68D File Offset: 0x0009D88D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.Ids == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdListHierarchyOperations(callContext, this.Ids);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0009F6A5 File Offset: 0x0009D8A5
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Ids == null || this.Ids.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForFolderIdHierarchyOperations(true, callContext, this.Ids[taskStep]);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0009F6CC File Offset: 0x0009D8CC
		internal override void Validate()
		{
			base.Validate();
			if (this.Ids == null || this.Ids.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}

		// Token: 0x04001345 RID: 4933
		internal const string FolderIdsElementName = "FolderIds";

		// Token: 0x04001346 RID: 4934
		internal const string DeleteTypeElementName = "DeleteType";
	}
}
