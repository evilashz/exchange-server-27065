using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A6B RID: 2667
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarNotificationResponse : OptionsResponseBase
	{
		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x00105BE7 File Offset: 0x00103DE7
		// (set) Token: 0x06004BAD RID: 19373 RVA: 0x00105BEF File Offset: 0x00103DEF
		[DataMember(IsRequired = true)]
		public CalendarNotificationOptions Options { get; set; }

		// Token: 0x06004BAE RID: 19374 RVA: 0x00105BF8 File Offset: 0x00103DF8
		public override string ToString()
		{
			return string.Format("GetCalendarNotificationResponse: {0}", this.Options);
		}
	}
}
