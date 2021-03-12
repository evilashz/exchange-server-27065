using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048E RID: 1166
	[XmlType("SyncFolderItemsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderItemsRequest : BaseRequest
	{
		// Token: 0x060022BD RID: 8893 RVA: 0x000A3489 File Offset: 0x000A1689
		public SyncFolderItemsRequest()
		{
			this.Init();
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000A3497 File Offset: 0x000A1697
		private void Init()
		{
			this.SyncScope = SyncFolderItemsScope.NormalItems;
			this.NumberOfDays = -1;
			this.MinimumCount = -1;
			this.MaximumCount = 5000;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000A34B9 File Offset: 0x000A16B9
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x000A34C1 File Offset: 0x000A16C1
		[XmlElement(ElementName = "ItemShape")]
		[DataMember(Name = "ItemShape", IsRequired = true)]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000A34CA File Offset: 0x000A16CA
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x000A34D2 File Offset: 0x000A16D2
		[DataMember(Name = "ShapeName", IsRequired = false)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000A34DB File Offset: 0x000A16DB
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x000A34E3 File Offset: 0x000A16E3
		[DataMember(Name = "SyncFolderId", IsRequired = true)]
		[XmlElement("SyncFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public TargetFolderId SyncFolderId { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000A34EC File Offset: 0x000A16EC
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x000A34F4 File Offset: 0x000A16F4
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SyncState", IsRequired = false)]
		public string SyncState { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x000A34FD File Offset: 0x000A16FD
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x000A3505 File Offset: 0x000A1705
		[XmlArrayItem("ItemId", typeof(ItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "Ignore", IsRequired = false)]
		[XmlArray("Ignore", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ItemId[] Ignore { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000A350E File Offset: 0x000A170E
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x000A3516 File Offset: 0x000A1716
		[XmlElement("MaxChangesReturned", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "MaxChangesReturned", IsRequired = true)]
		public int MaxChangesReturned { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000A351F File Offset: 0x000A171F
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x000A3527 File Offset: 0x000A1727
		[XmlElement("NumberOfDays", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "NumberOfDays", IsRequired = false)]
		public int NumberOfDays { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000A3530 File Offset: 0x000A1730
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x000A3538 File Offset: 0x000A1738
		[XmlIgnore]
		[DataMember(Name = "MinimumCount", IsRequired = false)]
		public int MinimumCount { get; set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000A3541 File Offset: 0x000A1741
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x000A3549 File Offset: 0x000A1749
		[DataMember(Name = "MaximumCount", IsRequired = false)]
		[XmlIgnore]
		public int MaximumCount { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000A3552 File Offset: 0x000A1752
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x000A355A File Offset: 0x000A175A
		[DataMember(Name = "DoQuickSync", IsRequired = false)]
		[XmlElement("DoQuickSync", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool DoQuickSync { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000A3563 File Offset: 0x000A1763
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x000A356B File Offset: 0x000A176B
		[XmlElement("SyncScope", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[IgnoreDataMember]
		public SyncFolderItemsScope SyncScope { get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000A3574 File Offset: 0x000A1774
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x000A3581 File Offset: 0x000A1781
		[XmlIgnore]
		[DataMember(Name = "SyncScope", IsRequired = false)]
		public string SyncScopeString
		{
			get
			{
				return EnumUtilities.ToString<SyncFolderItemsScope>(this.SyncScope);
			}
			set
			{
				this.SyncScope = EnumUtilities.Parse<SyncFolderItemsScope>(value);
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000A358F File Offset: 0x000A178F
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SyncFolderItems(callContext, this);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000A3598 File Offset: 0x000A1798
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.SyncFolderId == null || this.SyncFolderId.BaseFolderId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.SyncFolderId.BaseFolderId);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000A35C2 File Offset: 0x000A17C2
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000A35CC File Offset: 0x000A17CC
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}
	}
}
