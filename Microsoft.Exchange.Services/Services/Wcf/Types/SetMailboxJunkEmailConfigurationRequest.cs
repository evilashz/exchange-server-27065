using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ADF RID: 2783
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetMailboxJunkEmailConfigurationRequest : BaseJsonRequest
	{
		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06004F2C RID: 20268 RVA: 0x001086A1 File Offset: 0x001068A1
		// (set) Token: 0x06004F2D RID: 20269 RVA: 0x001086A9 File Offset: 0x001068A9
		[DataMember(IsRequired = true)]
		public MailboxJunkEmailConfigurationOptions Options { get; set; }

		// Token: 0x06004F2E RID: 20270 RVA: 0x001086B2 File Offset: 0x001068B2
		public override string ToString()
		{
			return string.Format("SetMailboxJunkEmailConfigurationRequest: {0}", this.Options);
		}
	}
}
