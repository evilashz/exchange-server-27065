using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE0 RID: 2784
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxMessageConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x001086CC File Offset: 0x001068CC
		// (set) Token: 0x06004F31 RID: 20273 RVA: 0x001086D4 File Offset: 0x001068D4
		[DataMember(IsRequired = true)]
		public MailboxMessageConfigurationOptions Options { get; set; }

		// Token: 0x06004F32 RID: 20274 RVA: 0x001086DD File Offset: 0x001068DD
		public override string ToString()
		{
			return string.Format("SetMailboxMessageConfigurationRequest: {0}", this.Options);
		}
	}
}
