using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001A RID: 26
	[ServiceContract(SessionMode = SessionMode.Required)]
	internal interface IMailboxReplicationService
	{
		// Token: 0x060001A7 RID: 423
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion);

		// Token: 0x060001A8 RID: 424
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MoveRequestInfo GetMoveRequestInfo(Guid requestGuid);

		// Token: 0x060001A9 RID: 425
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void SyncNow(List<SyncNowNotification> notifications);

		// Token: 0x060001AA RID: 426
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void RefreshMoveRequest(Guid requestGuid, Guid mdbGuid, MoveRequestNotification op);

		// Token: 0x060001AB RID: 427
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void RefreshMoveRequest2(Guid requestGuid, Guid mdbGuid, int requestFlags, MoveRequestNotification op);

		// Token: 0x060001AC RID: 428
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MailboxInformation GetMailboxInformation2(Guid primaryMailboxGuid, Guid physicalMailboxGuid, Guid targetMdbGuid, string targetMdbName, string remoteHostName, string remoteOrgName, string remoteDCName, string username, string password, string domain);

		// Token: 0x060001AD RID: 429
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		MailboxInformation GetMailboxInformation3(Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid targetMdbGuid, string targetMdbName, string remoteHostName, string remoteOrgName, string remoteDCName, string username, string password, string domain);

		// Token: 0x060001AE RID: 430
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		MailboxInformation GetMailboxInformation4(string requestJobXml, Guid primaryMailboxGuid, Guid physicalMailboxGuid, byte[] partitionHint, Guid targetMdbGuid, string targetMdbName, string remoteHostName, string remoteOrgName, string remoteDCName, string username, string password, string domain);

		// Token: 0x060001AF RID: 431
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		string ValidateAndPopulateRequestJob(string requestJobXML, out string reportEntryXMLs);

		// Token: 0x060001B0 RID: 432
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[OperationContract]
		void AttemptConnectToMRSProxy(string remoteHostName, Guid mbxGuid, string username, string password, string domain);

		// Token: 0x060001B1 RID: 433
		[OperationContract]
		[FaultContract(typeof(MailboxReplicationServiceFault), ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		void PingMRSProxy(string serverFqdn, string username, string password, string domain, bool useHttps);
	}
}
