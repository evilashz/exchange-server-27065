using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ADE RID: 2782
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxAutoReplyConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x00108676 File Offset: 0x00106876
		// (set) Token: 0x06004F29 RID: 20265 RVA: 0x0010867E File Offset: 0x0010687E
		[DataMember(IsRequired = true)]
		public MailboxAutoReplyConfigurationOptions Options { get; set; }

		// Token: 0x06004F2A RID: 20266 RVA: 0x00108687 File Offset: 0x00106887
		public override string ToString()
		{
			return string.Format("SetMailboxAutoReplyConfigurationRequest: {0}", this.Options);
		}
	}
}
