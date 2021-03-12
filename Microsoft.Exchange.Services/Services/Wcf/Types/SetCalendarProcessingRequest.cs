using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A71 RID: 2673
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarProcessingRequest : BaseJsonRequest
	{
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x00105DF9 File Offset: 0x00103FF9
		// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x00105E01 File Offset: 0x00104001
		[DataMember(IsRequired = true)]
		public CalendarProcessingOptions Options { get; set; }

		// Token: 0x06004BDA RID: 19418 RVA: 0x00105E0A File Offset: 0x0010400A
		public override string ToString()
		{
			return string.Format("SetCalendarProcessingRequest: {0}", this.Options);
		}
	}
}
