using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A09 RID: 2569
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarActionResponse
	{
		// Token: 0x06004886 RID: 18566 RVA: 0x0010194C File Offset: 0x000FFB4C
		public CalendarActionResponse(CalendarActionError errorCode)
		{
			this.WasSuccessful = false;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x00101962 File Offset: 0x000FFB62
		public CalendarActionResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x00101971 File Offset: 0x000FFB71
		// (set) Token: 0x06004889 RID: 18569 RVA: 0x00101979 File Offset: 0x000FFB79
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x00101982 File Offset: 0x000FFB82
		// (set) Token: 0x0600488B RID: 18571 RVA: 0x0010198A File Offset: 0x000FFB8A
		[DataMember]
		public CalendarActionError ErrorCode { get; set; }
	}
}
