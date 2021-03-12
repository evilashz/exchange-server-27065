using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A6D RID: 2669
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailboxCalendarConfigurationResponse : OptionsResponseBase
	{
		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x00105C3D File Offset: 0x00103E3D
		// (set) Token: 0x06004BB5 RID: 19381 RVA: 0x00105C45 File Offset: 0x00103E45
		[DataMember(IsRequired = true)]
		public MailboxCalendarConfiguration Options { get; set; }

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00105C4E File Offset: 0x00103E4E
		public override string ToString()
		{
			return string.Format("GetMailboxCalendarConfigurationResponse: {0}", this.Options);
		}
	}
}
