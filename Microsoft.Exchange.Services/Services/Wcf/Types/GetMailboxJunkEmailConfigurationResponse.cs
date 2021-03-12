using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AAB RID: 2731
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxJunkEmailConfigurationResponse : OptionsResponseBase
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x00106AA5 File Offset: 0x00104CA5
		// (set) Token: 0x06004CFF RID: 19711 RVA: 0x00106AAD File Offset: 0x00104CAD
		[DataMember(IsRequired = true)]
		public MailboxJunkEmailConfigurationOptions Options { get; set; }

		// Token: 0x06004D00 RID: 19712 RVA: 0x00106AB6 File Offset: 0x00104CB6
		public override string ToString()
		{
			return string.Format("GetMailboxJunkEmailConfiguration: {0}", this.Options);
		}
	}
}
