using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000013 RID: 19
	public class ErrorDetail
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005418 File Offset: 0x00003618
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005420 File Offset: 0x00003620
		public string ErrorType { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005429 File Offset: 0x00003629
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005431 File Offset: 0x00003631
		public string ErrorMessage { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000543A File Offset: 0x0000363A
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005442 File Offset: 0x00003642
		public string StackTrace { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000544B File Offset: 0x0000364B
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005453 File Offset: 0x00003653
		public string UserEmail { get; set; }
	}
}
