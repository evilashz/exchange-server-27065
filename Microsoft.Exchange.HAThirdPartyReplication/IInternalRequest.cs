using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200000A RID: 10
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	[ServiceContract(Namespace = "http://Microsoft.Exchange.ThirdPartyReplication.Requests", ConfigurationName = "Microsoft.Exchange.ThirdPartyReplication.IInternalRequest")]
	public interface IInternalRequest
	{
		// Token: 0x0600002A RID: 42
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/GetPrimaryActiveManager", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/GetPrimaryActiveManagerResponse")]
		string GetPrimaryActiveManager(out byte[] ex);

		// Token: 0x0600002B RID: 43
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/ChangeActiveServer", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/ChangeActiveServerResponse")]
		byte[] ChangeActiveServer(Guid databaseId, string newActiveServerName);

		// Token: 0x0600002C RID: 44
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/ImmediateDismountMailboxDatabase", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/ImmediateDismountMailboxDatabaseResponse")]
		byte[] ImmediateDismountMailboxDatabase(Guid databaseId);

		// Token: 0x0600002D RID: 45
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/AmeIsStarting", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/AmeIsStartingResponse")]
		void AmeIsStarting(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout);

		// Token: 0x0600002E RID: 46
		[OperationContract(Action = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/AmeIsStopping", ReplyAction = "http://Microsoft.Exchange.ThirdPartyReplication.Requests/IInternalRequest/AmeIsStoppingResponse")]
		void AmeIsStopping();
	}
}
