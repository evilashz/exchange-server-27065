using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003E5 RID: 997
	[XmlType(TypeName = "DAGConfig")]
	[Serializable]
	public sealed class DatabaseAvailabilityGroupConfigXml : XMLSerializableBase
	{
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000BB194 File Offset: 0x000B9394
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000BB19C File Offset: 0x000B939C
		[XmlIgnore]
		public int? MailboxLoadBalanceRelativeCapacity { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000BB1A5 File Offset: 0x000B93A5
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000BB1B2 File Offset: 0x000B93B2
		[XmlAttribute("MLBRC")]
		public string MailboxLoadBalanceRelativeCapacityRaw
		{
			get
			{
				return XMLSerializableBase.GetNullableSerializationValue<int>(this.MailboxLoadBalanceRelativeCapacity);
			}
			set
			{
				this.MailboxLoadBalanceRelativeCapacity = XMLSerializableBase.GetNullableAttribute<int>(value, new XMLSerializableBase.TryParseDelegate<int>(int.TryParse));
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000BB1CC File Offset: 0x000B93CC
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x000BB1D4 File Offset: 0x000B93D4
		[XmlIgnore]
		public int? MailboxLoadBalanceOverloadThreshold { get; set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x000BB1DD File Offset: 0x000B93DD
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x000BB1EA File Offset: 0x000B93EA
		[XmlAttribute("MLBOT")]
		public string MailboxLoadBalanceOverloadThresholdRaw
		{
			get
			{
				return XMLSerializableBase.GetNullableSerializationValue<int>(this.MailboxLoadBalanceOverloadThreshold);
			}
			set
			{
				this.MailboxLoadBalanceOverloadThreshold = XMLSerializableBase.GetNullableAttribute<int>(value, new XMLSerializableBase.TryParseDelegate<int>(int.TryParse));
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000BB204 File Offset: 0x000B9404
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x000BB20C File Offset: 0x000B940C
		[XmlIgnore]
		public int? MailboxLoadBalanceMinimumBalancingThreshold { get; set; }

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002DDA RID: 11738 RVA: 0x000BB215 File Offset: 0x000B9415
		// (set) Token: 0x06002DDB RID: 11739 RVA: 0x000BB222 File Offset: 0x000B9422
		[XmlAttribute("MLBMT")]
		public string MailboxLoadBalanceMinimumBalancingThresholdRaw
		{
			get
			{
				return XMLSerializableBase.GetNullableSerializationValue<int>(this.MailboxLoadBalanceMinimumBalancingThreshold);
			}
			set
			{
				this.MailboxLoadBalanceMinimumBalancingThreshold = XMLSerializableBase.GetNullableAttribute<int>(value, new XMLSerializableBase.TryParseDelegate<int>(int.TryParse));
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x000BB23C File Offset: 0x000B943C
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x000BB244 File Offset: 0x000B9444
		[XmlIgnore]
		public ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize { get; set; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000BB250 File Offset: 0x000B9450
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x000BB294 File Offset: 0x000B9494
		[XmlAttribute("MLBMFS")]
		public string MailboxLoadBalanceMaximumEdbFileSizeBytes
		{
			get
			{
				if (this.MailboxLoadBalanceMaximumEdbFileSize != null)
				{
					return this.MailboxLoadBalanceMaximumEdbFileSize.Value.ToBytes().ToString(CultureInfo.InvariantCulture);
				}
				return null;
			}
			set
			{
				this.MailboxLoadBalanceMaximumEdbFileSize = (string.IsNullOrWhiteSpace(value) ? null : new ByteQuantifiedSize?(ByteQuantifiedSize.FromBytes(ulong.Parse(value))));
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000BB2CA File Offset: 0x000B94CA
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000BB2D2 File Offset: 0x000B94D2
		[XmlAttribute("MLBE")]
		public bool MailboxLoadBalanceEnabled { get; set; }
	}
}
