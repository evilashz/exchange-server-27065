using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A72 RID: 2674
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxCalendarConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x00105E24 File Offset: 0x00104024
		// (set) Token: 0x06004BDD RID: 19421 RVA: 0x00105E2C File Offset: 0x0010402C
		[DataMember(IsRequired = true)]
		public MailboxCalendarConfiguration Options { get; set; }

		// Token: 0x06004BDE RID: 19422 RVA: 0x00105E35 File Offset: 0x00104035
		public override string ToString()
		{
			return string.Format("SetMailboxCalendarConfigurationRequest: {0}", this.Options);
		}
	}
}
