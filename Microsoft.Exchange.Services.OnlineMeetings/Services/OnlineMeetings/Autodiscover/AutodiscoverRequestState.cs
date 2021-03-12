using System;
using System.Net;
using System.Threading;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover
{
	// Token: 0x02000030 RID: 48
	internal class AutodiscoverRequestState
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00006704 File Offset: 0x00004904
		public AutodiscoverRequestState(HttpWebRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.request = request;
			this.url = request.RequestUri.ToString();
			this.Result = new AnonymousAutodiscoverResult();
			this.Result.Redirects.Add(request.RequestUri.ToString());
			this.manualResetEvent = new ManualResetEvent(false);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000676F File Offset: 0x0000496F
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006777 File Offset: 0x00004977
		public ManualResetEvent ManualResetEvent
		{
			get
			{
				return this.manualResetEvent;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000677F File Offset: 0x0000497F
		public HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006787 File Offset: 0x00004987
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000678F File Offset: 0x0000498F
		public HttpWebResponse Response { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006798 File Offset: 0x00004998
		public bool HasException
		{
			get
			{
				return this.Result.HasException;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000067A5 File Offset: 0x000049A5
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000067AD File Offset: 0x000049AD
		public AnonymousAutodiscoverResult Result { get; set; }

		// Token: 0x0400013F RID: 319
		private readonly HttpWebRequest request;

		// Token: 0x04000140 RID: 320
		private readonly ManualResetEvent manualResetEvent;

		// Token: 0x04000141 RID: 321
		private readonly string url;
	}
}
