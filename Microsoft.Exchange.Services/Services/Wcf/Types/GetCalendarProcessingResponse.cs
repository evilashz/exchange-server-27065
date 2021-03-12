using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A6C RID: 2668
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarProcessingResponse : OptionsResponseBase
	{
		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06004BB0 RID: 19376 RVA: 0x00105C12 File Offset: 0x00103E12
		// (set) Token: 0x06004BB1 RID: 19377 RVA: 0x00105C1A File Offset: 0x00103E1A
		[DataMember(IsRequired = true)]
		public CalendarProcessingOptions Options { get; set; }

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00105C23 File Offset: 0x00103E23
		public override string ToString()
		{
			return string.Format("GetCalendarProcessingResponse: {0}", this.Options);
		}
	}
}
