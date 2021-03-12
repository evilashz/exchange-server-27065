using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005FE RID: 1534
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "OnlineMeetingSettingsType")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "OnlineMeetingSettingsType")]
	[Serializable]
	public class OnlineMeetingSettingsType
	{
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x000B3F47 File Offset: 0x000B2147
		// (set) Token: 0x06002F44 RID: 12100 RVA: 0x000B3F4F File Offset: 0x000B214F
		[IgnoreDataMember]
		[XmlAttribute("LobbyBypass")]
		public LobbyBypass LobbyBypass { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x000B3F58 File Offset: 0x000B2158
		// (set) Token: 0x06002F46 RID: 12102 RVA: 0x000B3F65 File Offset: 0x000B2165
		[DataMember(Name = "LobbyBypass", IsRequired = true, Order = 1)]
		[XmlIgnore]
		public string LobbyBypassString
		{
			get
			{
				return EnumUtilities.ToString<LobbyBypass>(this.LobbyBypass);
			}
			set
			{
				this.LobbyBypass = EnumUtilities.Parse<LobbyBypass>(value);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000B3F73 File Offset: 0x000B2173
		// (set) Token: 0x06002F48 RID: 12104 RVA: 0x000B3F7B File Offset: 0x000B217B
		[XmlAttribute("AccessLevel")]
		[IgnoreDataMember]
		public OnlineMeetingAccessLevel AccessLevel { get; set; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000B3F84 File Offset: 0x000B2184
		// (set) Token: 0x06002F4A RID: 12106 RVA: 0x000B3F91 File Offset: 0x000B2191
		[DataMember(Name = "AccessLevel", IsRequired = true, Order = 2)]
		[XmlIgnore]
		public string AccessLevelString
		{
			get
			{
				return EnumUtilities.ToString<OnlineMeetingAccessLevel>(this.AccessLevel);
			}
			set
			{
				this.AccessLevel = EnumUtilities.Parse<OnlineMeetingAccessLevel>(value);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000B3F9F File Offset: 0x000B219F
		// (set) Token: 0x06002F4C RID: 12108 RVA: 0x000B3FA7 File Offset: 0x000B21A7
		[IgnoreDataMember]
		[XmlAttribute("Presenters")]
		public Presenters Presenters { get; set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000B3FB0 File Offset: 0x000B21B0
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x000B3FBD File Offset: 0x000B21BD
		[DataMember(Name = "Presenters", IsRequired = true, Order = 3)]
		[XmlIgnore]
		public string PresentersString
		{
			get
			{
				return EnumUtilities.ToString<Presenters>(this.Presenters);
			}
			set
			{
				this.Presenters = EnumUtilities.Parse<Presenters>(value);
			}
		}
	}
}
