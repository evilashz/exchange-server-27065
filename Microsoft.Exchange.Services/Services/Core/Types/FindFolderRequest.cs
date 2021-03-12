using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000420 RID: 1056
	[XmlType(TypeName = "FindFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FindFolderRequest : BaseRequest, IRemoteArchiveRequest
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x000A017D File Offset: 0x0009E37D
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x000A0185 File Offset: 0x0009E385
		[DataMember(IsRequired = true)]
		[XmlElement]
		public FolderResponseShape FolderShape { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x000A018E File Offset: 0x0009E38E
		// (set) Token: 0x06001E89 RID: 7817 RVA: 0x000A0196 File Offset: 0x0009E396
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000A019F File Offset: 0x0009E39F
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x000A01A7 File Offset: 0x0009E3A7
		[XmlElement("FractionalPageFolderView", typeof(FractionalPageView))]
		[XmlElement("IndexedPageFolderView", typeof(IndexedPageView))]
		[DataMember(IsRequired = true)]
		public Microsoft.Exchange.Services.Core.Search.BasePagingType Paging { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000A01B0 File Offset: 0x0009E3B0
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x000A01B8 File Offset: 0x0009E3B8
		[XmlElement("Restriction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public RestrictionType Restriction { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x000A01C1 File Offset: 0x0009E3C1
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x000A01C9 File Offset: 0x0009E3C9
		[DataMember(IsRequired = true)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderId[] ParentFolderIds { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000A01D2 File Offset: 0x0009E3D2
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x000A01DA File Offset: 0x0009E3DA
		[IgnoreDataMember]
		[XmlElement("MailboxGuid")]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000A01E4 File Offset: 0x0009E3E4
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x000A0205 File Offset: 0x0009E405
		[DataMember(Name = "MailboxGuid", IsRequired = false)]
		[XmlIgnore]
		public string MailboxGuidString
		{
			get
			{
				return this.MailboxGuid.ToString();
			}
			set
			{
				this.MailboxGuid = (string.IsNullOrEmpty(value) ? Guid.Empty : Guid.Parse(value));
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000A0222 File Offset: 0x0009E422
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x000A022A File Offset: 0x0009E42A
		[IgnoreDataMember]
		[XmlAttribute]
		public FolderQueryTraversal Traversal { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000A0233 File Offset: 0x0009E433
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x000A0240 File Offset: 0x0009E440
		[DataMember(Name = "Traversal", IsRequired = true)]
		[XmlIgnore]
		public string TraversalString
		{
			get
			{
				return EnumUtilities.ToString<FolderQueryTraversal>(this.Traversal);
			}
			set
			{
				this.Traversal = EnumUtilities.Parse<FolderQueryTraversal>(value);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000A024E File Offset: 0x0009E44E
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000A0256 File Offset: 0x0009E456
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public bool ReturnParentFolder { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000A025F File Offset: 0x0009E45F
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000A0267 File Offset: 0x0009E467
		[XmlIgnore]
		[IgnoreDataMember]
		public DistinguishedFolderIdName[] FoldersToMoveToTop { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000A0270 File Offset: 0x0009E470
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x000A027D File Offset: 0x0009E47D
		[DataMember(Name = "FoldersToMoveToTop", IsRequired = false)]
		[XmlIgnore]
		public string[] FoldersToMoveToTopString
		{
			get
			{
				return EnumUtilities.ToStringArray<DistinguishedFolderIdName>(this.FoldersToMoveToTop);
			}
			set
			{
				this.FoldersToMoveToTop = EnumUtilities.ParseStringArray<DistinguishedFolderIdName>(value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000A028B File Offset: 0x0009E48B
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x000A0293 File Offset: 0x0009E493
		[IgnoreDataMember]
		[XmlIgnore]
		public DistinguishedFolderIdName[] RequiredFolders { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x000A029C File Offset: 0x0009E49C
		// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x000A02A9 File Offset: 0x0009E4A9
		[DataMember(Name = "RequiredFolders", IsRequired = false)]
		[XmlIgnore]
		public string[] RequiredFoldersString
		{
			get
			{
				return EnumUtilities.ToStringArray<DistinguishedFolderIdName>(this.RequiredFolders);
			}
			set
			{
				this.RequiredFolders = EnumUtilities.ParseStringArray<DistinguishedFolderIdName>(value);
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000A02B7 File Offset: 0x0009E4B7
		protected override List<ServiceObjectId> GetAllIds()
		{
			if (this.ParentFolderIds == null)
			{
				return null;
			}
			return new List<ServiceObjectId>(this.ParentFolderIds);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000A02D0 File Offset: 0x0009E4D0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return ((IRemoteArchiveRequest)this).GetRemoteArchiveServiceCommand(callContext);
			}
			return new FindFolder(callContext, this);
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x000A02FA File Offset: 0x0009E4FA
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000A02FD File Offset: 0x0009E4FD
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ParentFolderIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdListHierarchyOperations(callContext, this.ParentFolderIds);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000A0318 File Offset: 0x0009E518
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.ParentFolderIds == null || this.ParentFolderIds.Length < taskStep)
			{
				return null;
			}
			if (this != null && ((IRemoteArchiveRequest)this).IsRemoteArchiveRequest(callContext))
			{
				return null;
			}
			return base.GetResourceKeysForFolderIdHierarchyOperations(false, callContext, this.ParentFolderIds[taskStep]);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000A035C File Offset: 0x0009E55C
		internal override void Validate()
		{
			base.Validate();
			if (this.MailboxGuid == Guid.Empty && (this.ParentFolderIds == null || this.ParentFolderIds.Length == 0))
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x000A03A9 File Offset: 0x0009E5A9
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x000A03B1 File Offset: 0x0009E5B1
		ExchangeServiceBinding IRemoteArchiveRequest.ArchiveService { get; set; }

		// Token: 0x06001EAA RID: 7850 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		bool IRemoteArchiveRequest.IsRemoteArchiveRequest(CallContext callContext)
		{
			return ComplianceUtil.TryCreateArchiveService(callContext, this, this.ParentFolderIds != null, delegate
			{
				((IRemoteArchiveRequest)this).ArchiveService = ComplianceUtil.GetArchiveServiceForFolder(callContext, this.ParentFolderIds);
			});
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000A042D File Offset: 0x0009E62D
		ServiceCommandBase IRemoteArchiveRequest.GetRemoteArchiveServiceCommand(CallContext callContext)
		{
			return new FindRemoteArchiveFolder(callContext, this);
		}

		// Token: 0x04001387 RID: 4999
		internal const string ParentFolderIdsElementName = "ParentFolderIds";
	}
}
