using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x0200002F RID: 47
	internal class AutodiscoverError
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00006649 File Offset: 0x00004849
		public AutodiscoverError(AutodiscoverStep step, Exception exception)
		{
			this.failureStep = step;
			this.exception = exception;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006660 File Offset: 0x00004860
		public AutodiscoverError(AutodiscoverStep step, Exception exception, WebRequest request, WebResponse response) : this(step, exception)
		{
			if (request != null)
			{
				this.RequestHeaders = request.GetRequestHeadersAsString();
			}
			if (response != null)
			{
				this.ResponseHeaders = response.GetResponseHeadersAsString();
				this.ResponseBody = response.GetResponseBodyAsString();
				this.ResponseFailureReason = response.GetReasonHeader();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000066AF File Offset: 0x000048AF
		public AutodiscoverStep FailureStep
		{
			get
			{
				return this.failureStep;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000066B7 File Offset: 0x000048B7
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000066BF File Offset: 0x000048BF
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000066C7 File Offset: 0x000048C7
		public string RequestHeaders { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000066D0 File Offset: 0x000048D0
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000066D8 File Offset: 0x000048D8
		public string ResponseHeaders { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000066E1 File Offset: 0x000048E1
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000066E9 File Offset: 0x000048E9
		public string ResponseBody { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000066F2 File Offset: 0x000048F2
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000066FA File Offset: 0x000048FA
		public string ResponseFailureReason { get; set; }

		// Token: 0x04000139 RID: 313
		private readonly AutodiscoverStep failureStep;

		// Token: 0x0400013A RID: 314
		private readonly Exception exception;
	}
}
