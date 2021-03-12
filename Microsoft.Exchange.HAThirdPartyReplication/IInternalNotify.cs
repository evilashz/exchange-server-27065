using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	[ServiceContract(Namespace = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications", ConfigurationName = "Microsoft.Exchange.ThirdPartyReplication.IInternalNotify")]
	public interface IInternalNotify
	{
		// Token: 0x0600001D RID: 29
		[OperationContract(IsOneWay = true, Action = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/BecomePame")]
		void BecomePame();

		// Token: 0x0600001E RID: 30
		[OperationContract(IsOneWay = true, Action = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/RevokePame")]
		void RevokePame();

		// Token: 0x0600001F RID: 31
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/DatabaseMoveNeeded", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/DatabaseMoveNeededResponse")]
		NotificationResponse DatabaseMoveNeeded(Guid databaseId, string currentActiveFqdn, bool mountDesired);

		// Token: 0x06000020 RID: 32
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/GetTimeouts", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Notifications/IInternalNotify/GetTimeoutsResponse")]
		int GetTimeouts(out TimeSpan retryDelay, out TimeSpan openTimeout, out TimeSpan sendTimeout, out TimeSpan receiveTimeout);
	}
}
