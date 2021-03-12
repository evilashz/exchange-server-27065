using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	internal class WacCheckFileResponse
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00008B30 File Offset: 0x00006D30
		public WacCheckFileResponse(string fileName, long fileSize, string sha256Hash, string downloadUrl, string ownerId, string userId, string userDisplayName, string userPuid, bool isMessageIrmProtected, bool directFileAccessEnabled, bool externalServicesEnabled, bool wacOMEXEnabled)
		{
			this.BaseFileName = fileName;
			this.Size = fileSize;
			this.Version = sha256Hash;
			this.SHA256 = sha256Hash;
			if (!isMessageIrmProtected && directFileAccessEnabled)
			{
				this.DownloadUrl = downloadUrl;
			}
			this.OwnerId = ownerId;
			this.UserId = userId;
			this.UserFriendlyName = userDisplayName;
			this.HostAuthenticationId = userPuid;
			this.ClientUrl = string.Empty;
			this.CloseUrl = "about:blank";
			this.HostViewUrl = string.Empty;
			this.HostEditUrl = string.Empty;
			this.HostEmbeddedViewUrl = string.Empty;
			this.HostEmbeddedEditUrl = string.Empty;
			this.EmbeddingRequiresShareChanges = false;
			this.FileUrl = string.Empty;
			this.PrivacyUrl = string.Empty;
			this.TermsOfUseUrl = string.Empty;
			this.UserCanWrite = false;
			this.ReadOnly = true;
			this.UserCanPresent = false;
			this.UserCanAttend = false;
			this.SupportsUpdate = false;
			this.SupportsLocks = false;
			this.SupportsCobalt = false;
			this.SupportsFolders = false;
			this.CloseButtonClosesWindow = false;
			this.DisableBrowserCachingOfUserContent = true;
			this.DisablePrint = !directFileAccessEnabled;
			this.ProtectInClient = isMessageIrmProtected;
			this.DisableTranslation = !externalServicesEnabled;
			this.AllowExternalMarketplace = wacOMEXEnabled;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008C65 File Offset: 0x00006E65
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00008C6D File Offset: 0x00006E6D
		[DataMember]
		public bool CloseButtonClosesWindow { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008C76 File Offset: 0x00006E76
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00008C7E File Offset: 0x00006E7E
		[DataMember]
		public bool DisableBrowserCachingOfUserContent { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008C87 File Offset: 0x00006E87
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00008C8F File Offset: 0x00006E8F
		[DataMember]
		public bool DisablePrint { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008C98 File Offset: 0x00006E98
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008CA0 File Offset: 0x00006EA0
		[DataMember]
		public bool ProtectInClient { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008CA9 File Offset: 0x00006EA9
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00008CB1 File Offset: 0x00006EB1
		[DataMember]
		public string Version { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008CBA File Offset: 0x00006EBA
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00008CC2 File Offset: 0x00006EC2
		[DataMember]
		public string BaseFileName { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008CCB File Offset: 0x00006ECB
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00008CD3 File Offset: 0x00006ED3
		[DataMember]
		public string OwnerId { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00008CDC File Offset: 0x00006EDC
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00008CE4 File Offset: 0x00006EE4
		[DataMember]
		public string UserId { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00008CED File Offset: 0x00006EED
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00008CF5 File Offset: 0x00006EF5
		[DataMember]
		public string UserFriendlyName { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00008CFE File Offset: 0x00006EFE
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00008D06 File Offset: 0x00006F06
		public string HostAuthenticationId { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00008D0F File Offset: 0x00006F0F
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00008D17 File Offset: 0x00006F17
		[DataMember]
		public long Size { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00008D20 File Offset: 0x00006F20
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00008D28 File Offset: 0x00006F28
		[DataMember]
		public string SHA256 { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008D31 File Offset: 0x00006F31
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00008D39 File Offset: 0x00006F39
		[DataMember]
		public string ClientUrl { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00008D42 File Offset: 0x00006F42
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00008D4A File Offset: 0x00006F4A
		[DataMember]
		public string DownloadUrl { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008D53 File Offset: 0x00006F53
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00008D5B File Offset: 0x00006F5B
		[DataMember]
		public string CloseUrl { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008D64 File Offset: 0x00006F64
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008D6C File Offset: 0x00006F6C
		[DataMember]
		public string HostViewUrl { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008D75 File Offset: 0x00006F75
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00008D7D File Offset: 0x00006F7D
		[DataMember]
		public string HostEditUrl { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008D86 File Offset: 0x00006F86
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00008D8E File Offset: 0x00006F8E
		[DataMember]
		public string HostEmbeddedViewUrl { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008D97 File Offset: 0x00006F97
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00008D9F File Offset: 0x00006F9F
		[DataMember]
		public string HostEmbeddedEditUrl { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008DA8 File Offset: 0x00006FA8
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00008DB0 File Offset: 0x00006FB0
		public bool EmbeddingRequiresShareChanges { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008DB9 File Offset: 0x00006FB9
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00008DC1 File Offset: 0x00006FC1
		[DataMember]
		public string FileUrl { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008DCA File Offset: 0x00006FCA
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00008DD2 File Offset: 0x00006FD2
		[DataMember]
		public string PrivacyUrl { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008DDB File Offset: 0x00006FDB
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00008DE3 File Offset: 0x00006FE3
		[DataMember]
		public string TermsOfUseUrl { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008DEC File Offset: 0x00006FEC
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00008DF4 File Offset: 0x00006FF4
		[DataMember]
		public bool UserCanWrite { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008DFD File Offset: 0x00006FFD
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00008E05 File Offset: 0x00007005
		[DataMember]
		public bool ReadOnly { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00008E0E File Offset: 0x0000700E
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00008E16 File Offset: 0x00007016
		[DataMember]
		public bool UserCanPresent { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00008E1F File Offset: 0x0000701F
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00008E27 File Offset: 0x00007027
		[DataMember]
		public bool UserCanAttend { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00008E30 File Offset: 0x00007030
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00008E38 File Offset: 0x00007038
		[DataMember]
		public bool SupportsUpdate { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008E41 File Offset: 0x00007041
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008E49 File Offset: 0x00007049
		[DataMember]
		public bool SupportsLocks { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008E52 File Offset: 0x00007052
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00008E5A File Offset: 0x0000705A
		[DataMember]
		public bool SupportsCobalt { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008E63 File Offset: 0x00007063
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00008E6B File Offset: 0x0000706B
		[DataMember]
		public bool SupportsFolders { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00008E74 File Offset: 0x00007074
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00008E7C File Offset: 0x0000707C
		[DataMember]
		public bool AllowExternalMarketplace { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008E85 File Offset: 0x00007085
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00008E8D File Offset: 0x0000708D
		[DataMember]
		public bool DisableTranslation { get; set; }
	}
}
