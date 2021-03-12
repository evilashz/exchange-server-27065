using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D0 RID: 208
	internal class UMServerMwiErrorStrategy : MwiFailureEventLogStrategy
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0001AB34 File Offset: 0x00018D34
		internal override void LogFailure(MwiMessage message, Exception ex)
		{
			string obj = this.ConstructErrorMessage(message, ex);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiMessageDeliveryFailedToUM, null, new object[]
			{
				message.UnreadVoicemailCount,
				message.TotalVoicemailCount - message.UnreadVoicemailCount,
				message.MailboxDisplayName,
				message.UserExtension,
				CommonUtil.ToEventLogString(obj)
			});
		}
	}
}
