using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000EE RID: 238
	internal interface IMessagingDatabase
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000926 RID: 2342
		DataSource DataSource { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000927 RID: 2343
		ServerInfoTable ServerInfoTable { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000928 RID: 2344
		QueueTable QueueTable { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000929 RID: 2345
		string CurrentState { get; }

		// Token: 0x0600092A RID: 2346
		void SuspendDataCleanup();

		// Token: 0x0600092B RID: 2347
		void ResumeDataCleanup();

		// Token: 0x0600092C RID: 2348
		void BootLoadCompleted();

		// Token: 0x0600092D RID: 2349
		IMailRecipientStorage NewRecipientStorage(long messageId);

		// Token: 0x0600092E RID: 2350
		IMailItemStorage NewMailItemStorage(bool loadDefaults);

		// Token: 0x0600092F RID: 2351
		IMailItemStorage LoadMailItemFromId(long msgId);

		// Token: 0x06000930 RID: 2352
		IEnumerable<IMailRecipientStorage> LoadMailRecipientsFromMessageId(long messageId);

		// Token: 0x06000931 RID: 2353
		IMailRecipientStorage LoadMailRecipientFromId(long recipientId);

		// Token: 0x06000932 RID: 2354
		IReplayRequest NewReplayRequest(Guid correlationId, Destination destination, DateTime startTime, DateTime endTime, bool isTestRequest = false);

		// Token: 0x06000933 RID: 2355
		IEnumerable<IReplayRequest> GetAllReplayRequests();

		// Token: 0x06000934 RID: 2356
		Transaction BeginNewTransaction();

		// Token: 0x06000935 RID: 2357
		void Attach(IMessagingDatabaseConfig config);

		// Token: 0x06000936 RID: 2358
		void Detach();

		// Token: 0x06000937 RID: 2359
		XElement GetDiagnosticInfo(DiagnosableParameters parameters);

		// Token: 0x06000938 RID: 2360
		IEnumerable<MailItemAndRecipients> GetMessages(List<long> messageIds);

		// Token: 0x06000939 RID: 2361
		MessagingDatabaseResultStatus ReadUnprocessedMessageIds(out Dictionary<byte, List<long>> unprocessedMessageIds);

		// Token: 0x0600093A RID: 2362
		void Start();
	}
}
