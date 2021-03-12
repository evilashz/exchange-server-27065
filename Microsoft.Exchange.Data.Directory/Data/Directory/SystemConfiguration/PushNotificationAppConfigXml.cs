using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200054D RID: 1357
	[XmlType(TypeName = "PushNotificationAppConfigXml")]
	[Serializable]
	public sealed class PushNotificationAppConfigXml : XMLSerializableBase, IEquatable<PushNotificationAppConfigXml>
	{
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000E86DF File Offset: 0x000E68DF
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x000E86E7 File Offset: 0x000E68E7
		[XmlElement]
		public bool? Enabled { get; set; }

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000E86F0 File Offset: 0x000E68F0
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x000E86F8 File Offset: 0x000E68F8
		[XmlElement]
		public string ExchangeMinimumVersion { get; set; }

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000E8701 File Offset: 0x000E6901
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000E8709 File Offset: 0x000E6909
		[XmlElement]
		public string ExchangeMaximumVersion { get; set; }

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000E8712 File Offset: 0x000E6912
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000E871A File Offset: 0x000E691A
		[XmlElement]
		public int? QueueSize { get; set; }

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000E8723 File Offset: 0x000E6923
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000E872B File Offset: 0x000E692B
		[XmlElement]
		public int? NumberOfChannels { get; set; }

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000E8734 File Offset: 0x000E6934
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000E873C File Offset: 0x000E693C
		[XmlElement]
		public int? BackOffTimeInSeconds { get; set; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000E8745 File Offset: 0x000E6945
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000E874D File Offset: 0x000E694D
		[XmlElement]
		public string AuthId { get; set; }

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000E8756 File Offset: 0x000E6956
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000E875E File Offset: 0x000E695E
		[XmlElement]
		public string AuthKey { get; set; }

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000E8767 File Offset: 0x000E6967
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000E876F File Offset: 0x000E696F
		[XmlElement]
		public string AuthKeyFallback { get; set; }

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000E8778 File Offset: 0x000E6978
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000E8780 File Offset: 0x000E6980
		[XmlElement]
		public bool? IsAuthKeyEncrypted { get; set; }

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000E8789 File Offset: 0x000E6989
		// (set) Token: 0x06003D1B RID: 15643 RVA: 0x000E8791 File Offset: 0x000E6991
		[XmlElement]
		public string Url { get; set; }

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000E879A File Offset: 0x000E699A
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000E87A2 File Offset: 0x000E69A2
		[XmlElement]
		public int? Port { get; set; }

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000E87AB File Offset: 0x000E69AB
		// (set) Token: 0x06003D1F RID: 15647 RVA: 0x000E87B3 File Offset: 0x000E69B3
		[XmlElement]
		public string SecondaryUrl { get; set; }

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000E87BC File Offset: 0x000E69BC
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000E87C4 File Offset: 0x000E69C4
		[XmlElement]
		public int? SecondaryPort { get; set; }

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000E87CD File Offset: 0x000E69CD
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x000E87D5 File Offset: 0x000E69D5
		[XmlElement]
		public string UriTemplate { get; set; }

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000E87DE File Offset: 0x000E69DE
		// (set) Token: 0x06003D25 RID: 15653 RVA: 0x000E87E6 File Offset: 0x000E69E6
		[XmlElement]
		public string RegistrationTemplate { get; set; }

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000E87EF File Offset: 0x000E69EF
		// (set) Token: 0x06003D27 RID: 15655 RVA: 0x000E87F7 File Offset: 0x000E69F7
		[XmlElement]
		public bool? RegistrationEnabled { get; set; }

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000E8800 File Offset: 0x000E6A00
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000E8808 File Offset: 0x000E6A08
		[XmlElement]
		public bool? MultifactorRegistrationEnabled { get; set; }

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000E8811 File Offset: 0x000E6A11
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x000E8819 File Offset: 0x000E6A19
		[XmlElement]
		public string PartitionName { get; set; }

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x000E8822 File Offset: 0x000E6A22
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x000E882A File Offset: 0x000E6A2A
		[XmlElement]
		public bool? IsDefaultPartitionName { get; set; }

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x000E8833 File Offset: 0x000E6A33
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x000E883B File Offset: 0x000E6A3B
		[XmlElement]
		public DateTime? LastUpdateTimeUtc { get; set; }

		// Token: 0x06003D30 RID: 15664 RVA: 0x000E8844 File Offset: 0x000E6A44
		public bool ShouldSerializeEnabled()
		{
			return this.Enabled != null;
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000E8860 File Offset: 0x000E6A60
		public bool ShouldSerializeQueueSize()
		{
			return this.QueueSize != null;
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000E887C File Offset: 0x000E6A7C
		public bool ShouldSerializeNumberOfChannels()
		{
			return this.NumberOfChannels != null;
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000E8898 File Offset: 0x000E6A98
		public bool ShouldSerializeBackOffTimeInSeconds()
		{
			return this.BackOffTimeInSeconds != null;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000E88B4 File Offset: 0x000E6AB4
		public bool ShouldSerializeIsAuthKeyEncrypted()
		{
			return this.IsAuthKeyEncrypted != null;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000E88D0 File Offset: 0x000E6AD0
		public bool ShouldSerializePort()
		{
			return this.Port != null;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000E88EC File Offset: 0x000E6AEC
		public bool ShouldSerializeSecondaryPort()
		{
			return this.SecondaryPort != null;
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000E8908 File Offset: 0x000E6B08
		public bool ShouldSerializeRegistrationEnabled()
		{
			return this.RegistrationEnabled != null;
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000E8924 File Offset: 0x000E6B24
		public bool ShouldSerializeMultifactorRegistrationEnabled()
		{
			return this.MultifactorRegistrationEnabled != null;
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000E8940 File Offset: 0x000E6B40
		public bool ShouldSerializeIsDefaultPartitionName()
		{
			return this.IsDefaultPartitionName != null;
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000E895C File Offset: 0x000E6B5C
		public bool ShouldSerializeLastUpdateTimeUtc()
		{
			return this.LastUpdateTimeUtc != null;
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000E8977 File Offset: 0x000E6B77
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PushNotificationAppConfigXml);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000E8985 File Offset: 0x000E6B85
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000E898D File Offset: 0x000E6B8D
		public bool Equals(PushNotificationAppConfigXml other)
		{
			return other != null && this.ToString().Equals(other.ToString());
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000E89A8 File Offset: 0x000E6BA8
		protected override void OnDeserialized()
		{
			if (base.UnknownAttributes == null)
			{
				return;
			}
			foreach (XmlAttribute xmlAttribute in base.UnknownAttributes)
			{
				if (this.Enabled == null && xmlAttribute.Name.Equals("ENA"))
				{
					this.Enabled = new bool?(bool.Parse(xmlAttribute.Value));
				}
				if (this.ExchangeMinimumVersion == null && xmlAttribute.Name.Equals("EMINV"))
				{
					this.ExchangeMinimumVersion = xmlAttribute.Value;
				}
				if (this.ExchangeMaximumVersion == null && xmlAttribute.Name.Equals("EMAXV"))
				{
					this.ExchangeMaximumVersion = xmlAttribute.Value;
				}
				if (this.QueueSize == null && xmlAttribute.Name.Equals("QSZ"))
				{
					this.QueueSize = new int?(int.Parse(xmlAttribute.Value));
				}
				if (this.NumberOfChannels == null && xmlAttribute.Name.Equals("NWT"))
				{
					this.NumberOfChannels = new int?(int.Parse(xmlAttribute.Value));
				}
				if (this.BackOffTimeInSeconds == null && xmlAttribute.Name.Equals("BFT"))
				{
					this.BackOffTimeInSeconds = new int?(int.Parse(xmlAttribute.Value));
				}
				if (this.AuthKey == null && xmlAttribute.Name.Equals("ANKP"))
				{
					this.AuthKey = xmlAttribute.Value;
				}
				if (this.AuthKeyFallback == null && xmlAttribute.Name.Equals("ANKF"))
				{
					this.AuthKeyFallback = xmlAttribute.Value;
				}
				if (this.IsAuthKeyEncrypted == null && xmlAttribute.Name.Equals("EANK"))
				{
					this.IsAuthKeyEncrypted = new bool?(bool.Parse(xmlAttribute.Value));
				}
				if (this.Url == null && xmlAttribute.Name.Equals("URL"))
				{
					this.Url = xmlAttribute.Value;
				}
				if (this.Port == null && xmlAttribute.Name.Equals("PRT"))
				{
					this.Port = new int?(int.Parse(xmlAttribute.Value));
				}
				if (this.SecondaryUrl == null && xmlAttribute.Name.Equals("SURL"))
				{
					this.SecondaryUrl = xmlAttribute.Value;
				}
				if (this.SecondaryPort == null && xmlAttribute.Name.Equals("SPRT"))
				{
					this.SecondaryPort = new int?(int.Parse(xmlAttribute.Value));
				}
			}
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000E8C50 File Offset: 0x000E6E50
		[Conditional("DEBUG")]
		private static void ValidateClassStructure()
		{
			Type typeFromHandle = typeof(Nullable<>);
			Type typeFromHandle2 = typeof(PushNotificationAppConfigXml);
			foreach (PropertyInfo propertyInfo in typeFromHandle2.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.PropertyType.IsValueType && propertyInfo.GetCustomAttribute<XmlElementAttribute>() != null)
				{
					if (!propertyInfo.PropertyType.IsGenericType || !(propertyInfo.PropertyType.GetGenericTypeDefinition() == typeFromHandle))
					{
						throw new NotSupportedException(string.Format("PushNotificationAppConfigXml's property {0} must be defined as nullable", propertyInfo.Name));
					}
					if (typeFromHandle2.GetMethod(string.Format("ShouldSerialize{0}", propertyInfo.Name)) == null)
					{
						throw new NotSupportedException(string.Format("A ShouldSerialize method should be added for PushNotificationAppConfigXml's property {0}", propertyInfo.Name));
					}
				}
			}
		}
	}
}
