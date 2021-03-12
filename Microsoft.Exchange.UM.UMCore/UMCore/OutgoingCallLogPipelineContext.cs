using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C4 RID: 708
	internal class OutgoingCallLogPipelineContext : CallLogPipelineContextBase
	{
		// Token: 0x06001580 RID: 5504 RVA: 0x0005BD75 File Offset: 0x00059F75
		internal OutgoingCallLogPipelineContext(SubmissionHelper helper) : base(helper)
		{
			base.MessageType = "OutgoingCallLog";
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0005BD89 File Offset: 0x00059F89
		internal OutgoingCallLogPipelineContext(SubmissionHelper helper, ContactInfo targetContact, UMRecipient recipient) : base(helper, recipient)
		{
			base.ContactInfo = targetContact;
			base.MessageType = "OutgoingCallLog";
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005BDA5 File Offset: 0x00059FA5
		protected override string GetMessageSubject(MessageContentBuilder contentBuilder)
		{
			return contentBuilder.GetOutgoingCallLogSubject(base.ContactInfo, base.CallerId);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005BDB9 File Offset: 0x00059FB9
		protected override void AddMessageBody(MessageContentBuilder contentBuilder)
		{
			contentBuilder.AddOutgoingCallLogBody(base.CallerId, base.ContactInfo);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005BDCD File Offset: 0x00059FCD
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			base.MessageToSubmit.From = new Participant(base.CAMessageRecipient.ADRecipient);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005BDF0 File Offset: 0x00059FF0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OutgoingCallLogPipelineContext>(this);
		}
	}
}
