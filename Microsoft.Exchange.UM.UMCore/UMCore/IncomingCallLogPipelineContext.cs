using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C0 RID: 704
	internal class IncomingCallLogPipelineContext : CallLogPipelineContextBase
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x0005B66C File Offset: 0x0005986C
		internal IncomingCallLogPipelineContext(SubmissionHelper helper) : base(helper)
		{
			base.MessageType = "IncomingCallLog";
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0005B680 File Offset: 0x00059880
		internal IncomingCallLogPipelineContext(SubmissionHelper helper, UMRecipient recipient) : base(helper, recipient)
		{
			base.MessageType = "IncomingCallLog";
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0005B695 File Offset: 0x00059895
		protected override string GetMessageSubject(MessageContentBuilder contentBuilder)
		{
			return contentBuilder.GetIncomingCallLogSubject(base.ContactInfo, base.CallerId);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0005B6A9 File Offset: 0x000598A9
		protected override void AddMessageBody(MessageContentBuilder contentBuilder)
		{
			contentBuilder.AddIncomingCallLogBody(base.CallerId, base.ContactInfo);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0005B6BD File Offset: 0x000598BD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IncomingCallLogPipelineContext>(this);
		}
	}
}
