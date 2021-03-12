using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200001A RID: 26
	internal abstract class SubmissionItem : SubmissionItemBase
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00008BFE File Offset: 0x00006DFE
		public SubmissionItem(string mailProtocol, IStoreDriverTracer storeDriverTracer) : this(mailProtocol, null, null, storeDriverTracer)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008C0A File Offset: 0x00006E0A
		public SubmissionItem(string mailProtocol, MailItemSubmitter context, SubmissionInfo submissionInfo, IStoreDriverTracer storeDriverTracer) : base(mailProtocol, storeDriverTracer)
		{
			this.Context = context;
			this.Info = submissionInfo;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00008C23 File Offset: 0x00006E23
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00008C2B File Offset: 0x00006E2B
		private protected SubmissionInfo Info { protected get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00008C34 File Offset: 0x00006E34
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00008C3C File Offset: 0x00006E3C
		private protected MailItemSubmitter Context { protected get; private set; }

		// Token: 0x06000118 RID: 280 RVA: 0x00008C45 File Offset: 0x00006E45
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x0400008A RID: 138
		private static readonly Trace diag = ExTraceGlobals.MapiStoreDriverSubmissionTracer;
	}
}
