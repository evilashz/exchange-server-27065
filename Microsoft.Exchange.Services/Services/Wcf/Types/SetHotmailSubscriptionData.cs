using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD8 RID: 2776
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetHotmailSubscriptionData : OptionsPropertyChangeTracker
	{
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00108430 File Offset: 0x00106630
		// (set) Token: 0x06004EFC RID: 20220 RVA: 0x00108438 File Offset: 0x00106638
		[DataMember]
		public Identity Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				base.TrackPropertyChanged("Identity");
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0010844C File Offset: 0x0010664C
		// (set) Token: 0x06004EFE RID: 20222 RVA: 0x00108454 File Offset: 0x00106654
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				base.TrackPropertyChanged("DisplayName");
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00108468 File Offset: 0x00106668
		// (set) Token: 0x06004F00 RID: 20224 RVA: 0x00108470 File Offset: 0x00106670
		[DataMember]
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
				base.TrackPropertyChanged("Password");
			}
		}

		// Token: 0x04002C42 RID: 11330
		private string displayName;

		// Token: 0x04002C43 RID: 11331
		private Identity identity;

		// Token: 0x04002C44 RID: 11332
		private string password;
	}
}
