using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000ABD RID: 2749
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxJunkEmailConfigurationOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x001077B5 File Offset: 0x001059B5
		// (set) Token: 0x06004E04 RID: 19972 RVA: 0x001077BD File Offset: 0x001059BD
		[DataMember]
		public string[] TrustedSendersAndDomains
		{
			get
			{
				return this.trustedSendersAndDomains;
			}
			set
			{
				this.trustedSendersAndDomains = value;
				base.TrackPropertyChanged("TrustedSendersAndDomains");
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x001077D1 File Offset: 0x001059D1
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x001077D9 File Offset: 0x001059D9
		[DataMember]
		public string[] BlockedSendersAndDomains
		{
			get
			{
				return this.blockedSendersAndDomains;
			}
			set
			{
				this.blockedSendersAndDomains = value;
				base.TrackPropertyChanged("BlockedSendersAndDomains");
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x001077ED File Offset: 0x001059ED
		// (set) Token: 0x06004E08 RID: 19976 RVA: 0x001077F5 File Offset: 0x001059F5
		[DataMember]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				base.TrackPropertyChanged("Enabled");
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x00107809 File Offset: 0x00105A09
		// (set) Token: 0x06004E0A RID: 19978 RVA: 0x00107811 File Offset: 0x00105A11
		[DataMember]
		public bool TrustedListsOnly
		{
			get
			{
				return this.trustedListsOnly;
			}
			set
			{
				this.trustedListsOnly = value;
				base.TrackPropertyChanged("TrustedListsOnly");
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x00107825 File Offset: 0x00105A25
		// (set) Token: 0x06004E0C RID: 19980 RVA: 0x0010782D File Offset: 0x00105A2D
		[DataMember]
		public bool ContactsTrusted
		{
			get
			{
				return this.contactsTrusted;
			}
			set
			{
				this.contactsTrusted = value;
				base.TrackPropertyChanged("ContactsTrusted");
			}
		}

		// Token: 0x04002BDF RID: 11231
		private string[] trustedSendersAndDomains;

		// Token: 0x04002BE0 RID: 11232
		private string[] blockedSendersAndDomains;

		// Token: 0x04002BE1 RID: 11233
		private bool enabled;

		// Token: 0x04002BE2 RID: 11234
		private bool contactsTrusted;

		// Token: 0x04002BE3 RID: 11235
		private bool trustedListsOnly;
	}
}
