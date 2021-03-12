using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E4 RID: 996
	[KnownType(typeof(JsonFaultResponse))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OnlineMeetingType
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0007852C File Offset: 0x0007672C
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x00078534 File Offset: 0x00076734
		[DataMember]
		public ItemId ItemId { get; set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0007853D File Offset: 0x0007673D
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x00078545 File Offset: 0x00076745
		[DataMember(IsRequired = false)]
		public string ConferenceId { get; set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0007854E File Offset: 0x0007674E
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00078556 File Offset: 0x00076756
		[DataMember]
		public string HelpUrl { get; set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0007855F File Offset: 0x0007675F
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00078567 File Offset: 0x00076767
		[DataMember]
		public string LegalUrl { get; set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00078570 File Offset: 0x00076770
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x00078578 File Offset: 0x00076778
		[DataMember]
		public string CustomFooterText { get; set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00078581 File Offset: 0x00076781
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00078589 File Offset: 0x00076789
		[DataMember]
		public string ExternalDirectoryUri { get; set; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x00078592 File Offset: 0x00076792
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x0007859A File Offset: 0x0007679A
		[DataMember]
		public string InternalDirectoryUri { get; set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000785A3 File Offset: 0x000767A3
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x000785AB File Offset: 0x000767AB
		[DataMember]
		public string LogoUrl { get; set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x000785B4 File Offset: 0x000767B4
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x000785BC File Offset: 0x000767BC
		[DataMember]
		public string WebUrl { get; set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x000785C5 File Offset: 0x000767C5
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x000785CD File Offset: 0x000767CD
		[DataMember(IsRequired = false)]
		public DialInNumberType[] Numbers { get; set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x000785D6 File Offset: 0x000767D6
		// (set) Token: 0x0600200C RID: 8204 RVA: 0x000785DE File Offset: 0x000767DE
		[DataMember(IsRequired = false)]
		public AcpInformationType AcpInformation { get; set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000785E7 File Offset: 0x000767E7
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x000785EF File Offset: 0x000767EF
		public LobbyBypass LobbyBypass
		{
			get
			{
				return this.lobbyBypass;
			}
			set
			{
				this.lobbyBypass = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x000785F8 File Offset: 0x000767F8
		// (set) Token: 0x06002010 RID: 8208 RVA: 0x0007860A File Offset: 0x0007680A
		[DataMember(Name = "LobbyBypass")]
		public string LobbyBypassString
		{
			get
			{
				return this.lobbyBypass.ToString();
			}
			set
			{
				this.lobbyBypass = (LobbyBypass)Enum.Parse(typeof(LobbyBypass), value);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x00078627 File Offset: 0x00076827
		// (set) Token: 0x06002012 RID: 8210 RVA: 0x0007862F File Offset: 0x0007682F
		public OnlineMeetingAccessLevel AccessLevel
		{
			get
			{
				return this.accessLevel;
			}
			set
			{
				this.accessLevel = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x00078638 File Offset: 0x00076838
		// (set) Token: 0x06002014 RID: 8212 RVA: 0x0007864A File Offset: 0x0007684A
		[DataMember(Name = "AccessLevel")]
		public string AccessLevelString
		{
			get
			{
				return this.accessLevel.ToString();
			}
			set
			{
				this.accessLevel = (OnlineMeetingAccessLevel)Enum.Parse(typeof(OnlineMeetingAccessLevel), value);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00078667 File Offset: 0x00076867
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0007866F File Offset: 0x0007686F
		public Presenters Presenters
		{
			get
			{
				return this.presenters;
			}
			set
			{
				this.presenters = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00078678 File Offset: 0x00076878
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0007868A File Offset: 0x0007688A
		[DataMember(Name = "Presenters")]
		public string PresentersString
		{
			get
			{
				return this.presenters.ToString();
			}
			set
			{
				this.presenters = (Presenters)Enum.Parse(typeof(Presenters), value);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000786A7 File Offset: 0x000768A7
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x000786AF File Offset: 0x000768AF
		[DataMember]
		public string DiagnosticInfo { get; set; }

		// Token: 0x0600201B RID: 8219 RVA: 0x000786B8 File Offset: 0x000768B8
		public static OnlineMeetingType CreateFailedOnlineMeetingType(string diagnosticInfo)
		{
			return new OnlineMeetingType
			{
				DiagnosticInfo = diagnosticInfo
			};
		}

		// Token: 0x04001219 RID: 4633
		private LobbyBypass lobbyBypass;

		// Token: 0x0400121A RID: 4634
		private OnlineMeetingAccessLevel accessLevel;

		// Token: 0x0400121B RID: 4635
		private Presenters presenters;
	}
}
