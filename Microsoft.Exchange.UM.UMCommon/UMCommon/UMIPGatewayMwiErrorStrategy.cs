using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CF RID: 207
	internal class UMIPGatewayMwiErrorStrategy : MwiFailureEventLogStrategy
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0001AABC File Offset: 0x00018CBC
		internal override void LogFailure(MwiMessage message, Exception ex)
		{
			string obj = this.ConstructErrorMessage(message, ex);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiMessageDeliveryFailed, null, new object[]
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
