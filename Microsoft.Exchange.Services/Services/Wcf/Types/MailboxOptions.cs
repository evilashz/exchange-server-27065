using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ABF RID: 2751
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MailboxOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004E41 RID: 20033 RVA: 0x00107B0D File Offset: 0x00105D0D
		// (set) Token: 0x06004E42 RID: 20034 RVA: 0x00107B15 File Offset: 0x00105D15
		[DataMember]
		public string AddressString
		{
			get
			{
				return this.addressString;
			}
			set
			{
				this.addressString = value;
				base.TrackPropertyChanged("AddressString");
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004E43 RID: 20035 RVA: 0x00107B29 File Offset: 0x00105D29
		// (set) Token: 0x06004E44 RID: 20036 RVA: 0x00107B31 File Offset: 0x00105D31
		[DataMember]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return this.deliverToMailboxAndForward;
			}
			set
			{
				this.deliverToMailboxAndForward = value;
				base.TrackPropertyChanged("DeliverToMailboxAndForward");
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004E45 RID: 20037 RVA: 0x00107B45 File Offset: 0x00105D45
		// (set) Token: 0x06004E46 RID: 20038 RVA: 0x00107B4D File Offset: 0x00105D4D
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

		// Token: 0x04002BFD RID: 11261
		private string addressString;

		// Token: 0x04002BFE RID: 11262
		private bool deliverToMailboxAndForward;

		// Token: 0x04002BFF RID: 11263
		private Identity identity;
	}
}
