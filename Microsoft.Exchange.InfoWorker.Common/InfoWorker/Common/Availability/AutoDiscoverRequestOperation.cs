using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000060 RID: 96
	internal abstract class AutoDiscoverRequestOperation : AsyncRequest, IDisposable
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B3C1 File Offset: 0x000095C1
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000B3C9 File Offset: 0x000095C9
		public Uri TargetUri { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000B3D2 File Offset: 0x000095D2
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000B3DA File Offset: 0x000095DA
		public AutoDiscoverAuthenticator Authenticator { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000B3E3 File Offset: 0x000095E3
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000B3EB File Offset: 0x000095EB
		public EmailAddress[] EmailAddresses { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000B3F4 File Offset: 0x000095F4
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B3FC File Offset: 0x000095FC
		public Exception Exception { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000B405 File Offset: 0x00009605
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B40D File Offset: 0x0000960D
		public string FrontEndServerName { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000B416 File Offset: 0x00009616
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B41E File Offset: 0x0000961E
		public string BackEndServerName { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B427 File Offset: 0x00009627
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B42F File Offset: 0x0000962F
		public AutoDiscoverRequestResult[] Results { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B438 File Offset: 0x00009638
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B440 File Offset: 0x00009640
		public AutodiscoverType AutodiscoverType { get; private set; }

		// Token: 0x06000242 RID: 578 RVA: 0x0000B449 File Offset: 0x00009649
		internal AutoDiscoverRequestOperation(Application application, ClientContext clientContext, RequestLogger requestLogger, Uri targetUri, AutoDiscoverAuthenticator authenticator, EmailAddress[] emailAddresses, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger)
		{
			this.TargetUri = targetUri;
			this.Authenticator = authenticator;
			this.EmailAddresses = emailAddresses;
			this.AutodiscoverType = autodiscoverType;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B474 File Offset: 0x00009674
		public virtual void Dispose()
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B476 File Offset: 0x00009676
		protected void HandleResultsAndCompleteRequest(SoapAutoDiscoverRequest request)
		{
			this.FrontEndServerName = request.FrontEndServerName;
			this.BackEndServerName = request.BackEndServerName;
			this.HandleResultsAndCompleteRequest(request.Exception, request.Results);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B4A4 File Offset: 0x000096A4
		protected void HandleResultsAndCompleteRequest(Exception exception, AutoDiscoverRequestResult[] results)
		{
			try
			{
				this.Exception = exception;
				this.Results = results;
			}
			finally
			{
				base.Complete();
			}
		}

		// Token: 0x04000179 RID: 377
		protected static readonly Trace AutoDiscoverTracer = ExTraceGlobals.AutoDiscoverTracer;
	}
}
