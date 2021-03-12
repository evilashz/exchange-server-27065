using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048D RID: 1165
	[XmlType("SyncFolderHierarchyType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderHierarchyRequest : BaseRequest
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000A33AD File Offset: 0x000A15AD
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x000A33B5 File Offset: 0x000A15B5
		[DataMember(Name = "FolderShape", IsRequired = true, Order = 1)]
		[XmlElement(ElementName = "FolderShape", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FolderResponseShape FolderShape { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000A33BE File Offset: 0x000A15BE
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x000A33C6 File Offset: 0x000A15C6
		[DataMember(Name = "ShapeName", IsRequired = false, Order = 2)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x000A33CF File Offset: 0x000A15CF
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x000A33D7 File Offset: 0x000A15D7
		[DataMember(Name = "SyncFolderId", IsRequired = false, Order = 3)]
		[XmlElement("SyncFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public TargetFolderId SyncFolderId { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000A33E0 File Offset: 0x000A15E0
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x000A33E8 File Offset: 0x000A15E8
		[XmlElement(ElementName = "SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SyncState", IsRequired = false, Order = 4)]
		public string SyncState { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000A33F1 File Offset: 0x000A15F1
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x000A33F9 File Offset: 0x000A15F9
		[XmlIgnore]
		[DataMember(Name = "ReturnRootFolder", IsRequired = false, Order = 5)]
		public bool ReturnRootFolder { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000A3402 File Offset: 0x000A1602
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x000A340A File Offset: 0x000A160A
		[DataMember(Name = "ReturnPeopleIKnowFolder", IsRequired = false)]
		[XmlIgnore]
		public bool ReturnPeopleIKnowFolder { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000A3413 File Offset: 0x000A1613
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x000A341B File Offset: 0x000A161B
		[XmlIgnore]
		[IgnoreDataMember]
		public DistinguishedFolderIdName[] FolderToMoveToTop { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x000A3424 File Offset: 0x000A1624
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x000A3431 File Offset: 0x000A1631
		[DataMember(Name = "FolderToMoveToTop", IsRequired = false)]
		[XmlIgnore]
		public string[] FoldersToMoveToTopString
		{
			get
			{
				return EnumUtilities.ToStringArray<DistinguishedFolderIdName>(this.FolderToMoveToTop);
			}
			set
			{
				this.FolderToMoveToTop = EnumUtilities.ParseStringArray<DistinguishedFolderIdName>(value);
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000A343F File Offset: 0x000A163F
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SyncFolderHierarchy(callContext, this);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000A3448 File Offset: 0x000A1648
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.SyncFolderId == null || this.SyncFolderId.BaseFolderId == null)
			{
				return callContext.GetServerInfoForEffectiveCaller();
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.SyncFolderId.BaseFolderId);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000A3477 File Offset: 0x000A1677
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
