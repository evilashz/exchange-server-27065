using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE1 RID: 2785
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxRequest : BaseJsonRequest
	{
		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x001086F7 File Offset: 0x001068F7
		// (set) Token: 0x06004F35 RID: 20277 RVA: 0x001086FF File Offset: 0x001068FF
		[DataMember(IsRequired = true)]
		public MailboxOptions Mailbox { get; set; }

		// Token: 0x06004F36 RID: 20278 RVA: 0x00108708 File Offset: 0x00106908
		public override string ToString()
		{
			return string.Format("SetMailboxRequest: Mailbox={0}", this.Mailbox);
		}
	}
}
