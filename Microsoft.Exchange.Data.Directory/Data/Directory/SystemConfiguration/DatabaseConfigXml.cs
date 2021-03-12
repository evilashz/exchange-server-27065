using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000593 RID: 1427
	[XmlType(TypeName = "DatabaseConfig")]
	[Serializable]
	public sealed class DatabaseConfigXml : XMLSerializableBase
	{
		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000FABBC File Offset: 0x000F8DBC
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x000FABC4 File Offset: 0x000F8DC4
		[XmlElement(ElementName = "MailboxProvisioningAttributes")]
		public MailboxProvisioningAttributes MailboxProvisioningAttributes { get; set; }

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x000FABCD File Offset: 0x000F8DCD
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x000FABD5 File Offset: 0x000F8DD5
		[XmlIgnore]
		public int? MailboxLoadBalanceRelativeCapacity { get; set; }

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000FABDE File Offset: 0x000F8DDE
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x000FABEB File Offset: 0x000F8DEB
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

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000FAC05 File Offset: 0x000F8E05
		// (set) Token: 0x0600427C RID: 17020 RVA: 0x000FAC0D File Offset: 0x000F8E0D
		[XmlIgnore]
		public int? MailboxLoadBalanceOverloadThreshold { get; set; }

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000FAC16 File Offset: 0x000F8E16
		// (set) Token: 0x0600427E RID: 17022 RVA: 0x000FAC23 File Offset: 0x000F8E23
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

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x000FAC3D File Offset: 0x000F8E3D
		// (set) Token: 0x06004280 RID: 17024 RVA: 0x000FAC45 File Offset: 0x000F8E45
		[XmlIgnore]
		public int? MailboxLoadBalanceMinimumBalancingThreshold { get; set; }

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x000FAC4E File Offset: 0x000F8E4E
		// (set) Token: 0x06004282 RID: 17026 RVA: 0x000FAC5B File Offset: 0x000F8E5B
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

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x000FAC75 File Offset: 0x000F8E75
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x000FAC7D File Offset: 0x000F8E7D
		[XmlIgnore]
		public ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize { get; set; }

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x000FAC88 File Offset: 0x000F8E88
		// (set) Token: 0x06004286 RID: 17030 RVA: 0x000FACCC File Offset: 0x000F8ECC
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

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x000FAD02 File Offset: 0x000F8F02
		// (set) Token: 0x06004288 RID: 17032 RVA: 0x000FAD0A File Offset: 0x000F8F0A
		[XmlIgnore]
		public bool? MailboxLoadBalanceEnabled { get; set; }

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x000FAD13 File Offset: 0x000F8F13
		// (set) Token: 0x0600428A RID: 17034 RVA: 0x000FAD20 File Offset: 0x000F8F20
		[XmlAttribute("MLBE")]
		public string MailboxLoadBalanceEnabledRaw
		{
			get
			{
				return XMLSerializableBase.GetNullableSerializationValue<bool>(this.MailboxLoadBalanceEnabled);
			}
			set
			{
				this.MailboxLoadBalanceEnabled = XMLSerializableBase.GetNullableAttribute<bool>(value, new XMLSerializableBase.TryParseDelegate<bool>(bool.TryParse));
			}
		}
	}
}
