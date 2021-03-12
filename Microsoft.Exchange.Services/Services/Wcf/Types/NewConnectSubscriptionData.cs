using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AFE RID: 2814
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewConnectSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x001091BB File Offset: 0x001073BB
		// (set) Token: 0x06004FFA RID: 20474 RVA: 0x001091C3 File Offset: 0x001073C3
		[DataMember]
		public string AppAuthorizationCode
		{
			get
			{
				return this.appAuthorizationCode;
			}
			set
			{
				this.appAuthorizationCode = value;
				base.TrackPropertyChanged("AppAuthorizationCode");
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004FFB RID: 20475 RVA: 0x001091D7 File Offset: 0x001073D7
		// (set) Token: 0x06004FFC RID: 20476 RVA: 0x001091DF File Offset: 0x001073DF
		[IgnoreDataMember]
		public ConnectSubscriptionType ConnectSubscriptionType { get; set; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004FFD RID: 20477 RVA: 0x001091E8 File Offset: 0x001073E8
		// (set) Token: 0x06004FFE RID: 20478 RVA: 0x001091F0 File Offset: 0x001073F0
		[DataMember]
		public string OAuthVerifier
		{
			get
			{
				return this.oAuthVerifier;
			}
			set
			{
				this.oAuthVerifier = value;
				base.TrackPropertyChanged("OAuthVerifier");
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x00109204 File Offset: 0x00107404
		// (set) Token: 0x06005000 RID: 20480 RVA: 0x0010920C File Offset: 0x0010740C
		[DataMember]
		public string RequestToken
		{
			get
			{
				return this.requestToken;
			}
			set
			{
				this.requestToken = value;
				base.TrackPropertyChanged("RequestToken");
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x00109220 File Offset: 0x00107420
		// (set) Token: 0x06005002 RID: 20482 RVA: 0x00109228 File Offset: 0x00107428
		[DataMember]
		public string RequestSecret
		{
			get
			{
				return this.requestSecret;
			}
			set
			{
				this.requestSecret = value;
				base.TrackPropertyChanged("RequestSecret");
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06005003 RID: 20483 RVA: 0x0010923C File Offset: 0x0010743C
		// (set) Token: 0x06005004 RID: 20484 RVA: 0x00109249 File Offset: 0x00107449
		[DataMember(Name = "ConnectSubscriptionType", EmitDefaultValue = false)]
		public string ConnectSubscriptionTypeString
		{
			get
			{
				return EnumUtilities.ToString<ConnectSubscriptionType>(this.ConnectSubscriptionType);
			}
			set
			{
				this.ConnectSubscriptionType = EnumUtilities.Parse<ConnectSubscriptionType>(value);
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06005005 RID: 20485 RVA: 0x00109257 File Offset: 0x00107457
		// (set) Token: 0x06005006 RID: 20486 RVA: 0x0010925F File Offset: 0x0010745F
		[DataMember]
		public string RedirectUri
		{
			get
			{
				return this.redirectUri;
			}
			set
			{
				this.redirectUri = value;
				base.TrackPropertyChanged("RedirectUri");
			}
		}

		// Token: 0x04002CC3 RID: 11459
		private string appAuthorizationCode;

		// Token: 0x04002CC4 RID: 11460
		private string oAuthVerifier;

		// Token: 0x04002CC5 RID: 11461
		private string redirectUri;

		// Token: 0x04002CC6 RID: 11462
		private string requestSecret;

		// Token: 0x04002CC7 RID: 11463
		private string requestToken;
	}
}
