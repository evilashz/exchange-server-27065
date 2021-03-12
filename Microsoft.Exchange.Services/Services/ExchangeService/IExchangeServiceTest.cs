using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DED RID: 3565
	[ServiceContract]
	public interface IExchangeServiceTest
	{
		// Token: 0x06005C37 RID: 23607
		[OperationContract]
		void CreateItem(string folderId, string subject, string body);

		// Token: 0x06005C38 RID: 23608
		[OperationContract]
		string CreateUnifiedMailbox();

		// Token: 0x06005C39 RID: 23609
		[OperationContract]
		bool IsOffice365Domain(string emailAddress);

		// Token: 0x06005C3A RID: 23610
		[OperationContract]
		AggregatedAccountType AddAggregatedAccount(string emailAddress, string userName, string password, string incomingServer, string incomingPort, string incomingProtocol, string security, string authentication, string outgoingServer, string outgoingPort, string outgoingProtocol, string interval);

		// Token: 0x06005C3B RID: 23611
		[OperationContract]
		void RemoveAggregatedAccount(string emailAddress);

		// Token: 0x06005C3C RID: 23612
		[OperationContract]
		void SetAggregatedAccount(string authentication, string emailAddress, string userName, string password, string interval, string incomingServer, string incomingPort, string incomingProtocol, string security);

		// Token: 0x06005C3D RID: 23613
		[OperationContract]
		AggregatedAccountType[] GetAggregatedAccount();

		// Token: 0x06005C3E RID: 23614
		[OperationContract(Name = "FindConversation")]
		ConversationType[] FindConversation(string parentFolderId);

		// Token: 0x06005C3F RID: 23615
		[OperationContract(Name = "FindConversationForUnifiedMailbox")]
		ConversationType[] FindConversation(Guid[] aggregatedMailboxGuids, DistinguishedFolderIdName defaultFolder);

		// Token: 0x06005C40 RID: 23616
		[OperationContract(Name = "FindItem")]
		ItemType[] FindItem(string parentFolderId);

		// Token: 0x06005C41 RID: 23617
		[OperationContract(Name = "FindItemForUnifiedMailbox")]
		ItemType[] FindItem(string[] folderIds);

		// Token: 0x06005C42 RID: 23618
		[OperationContract(Name = "FindFolder")]
		BaseFolderType[] FindFolder(DistinguishedFolderIdName distinguishedFolder);

		// Token: 0x06005C43 RID: 23619
		[OperationContract(Name = "FindFolderByMailboxGuid")]
		BaseFolderType[] FindFolder(string mailboxGuid);

		// Token: 0x06005C44 RID: 23620
		[OperationContract]
		Guid GetMailboxGuid();

		// Token: 0x06005C45 RID: 23621
		[OperationContract]
		void SubscribeToConversationChanges(string subscriptionId, string parentFolderId);

		// Token: 0x06005C46 RID: 23622
		[OperationContract(Name = "SubscribeToConversationChangesForUnifiedMailbox")]
		void SubscribeToConversationChanges(string subscriptionId, Guid[] aggregatedMailboxGuids, DistinguishedFolderIdName defaultFolder);

		// Token: 0x06005C47 RID: 23623
		[OperationContract]
		ConversationNotification GetNextConversationChange(string subscriptionId);

		// Token: 0x06005C48 RID: 23624
		[OperationContract]
		void SubscribeToCalendarChanges(string subscriptionId, string parentFolderId);

		// Token: 0x06005C49 RID: 23625
		[OperationContract]
		CalendarChangeNotificationType? GetNextCalendarChange(string subscriptionId);

		// Token: 0x06005C4A RID: 23626
		[OperationContract]
		InstantSearchPayloadType PerformInstantSearch(string deviceId, string searchSessionId, string kqlQuery, FolderId[] folderScope);

		// Token: 0x06005C4B RID: 23627
		[OperationContract]
		InstantSearchPayloadType GetNextInstantSearchPayload(string sessionId);

		// Token: 0x06005C4C RID: 23628
		[OperationContract]
		bool EndInstantSearchSession(string deviceId, string sessionId);

		// Token: 0x06005C4D RID: 23629
		[OperationContract]
		bool GetFolderFidAndMailboxFromEwsId(string ewsId, out long fid, out Guid mailboxGuid);

		// Token: 0x06005C4E RID: 23630
		[OperationContract]
		long GetFolderFidFromEwsId(string ewsId);

		// Token: 0x06005C4F RID: 23631
		[OperationContract]
		string GetEwsIdFromFolderFid(long fid, Guid mailboxGuid);

		// Token: 0x06005C50 RID: 23632
		[OperationContract]
		void SubscribeToHierarchyChanges(string subscriptionId, Guid mailboxGuid);

		// Token: 0x06005C51 RID: 23633
		[OperationContract]
		HierarchyNotification GetNextHierarchyChange(string subscriptionId);

		// Token: 0x06005C52 RID: 23634
		[OperationContract]
		void SubscribeToMessageChanges(string subscriptionId, string parentFolderId);

		// Token: 0x06005C53 RID: 23635
		[OperationContract(Name = "SubscribeToMessageChangesForUnifiedMailbox")]
		void SubscribeToMessageChanges(string subscriptionId, Guid[] aggregatedMailboxGuids, DistinguishedFolderIdName defaultFolder);

		// Token: 0x06005C54 RID: 23636
		[OperationContract]
		MessageNotification GetNextMessageChange(string subscriptionId);
	}
}
