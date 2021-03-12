using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000EC RID: 236
	internal interface IMailRecipientStorage
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008FC RID: 2300
		long RecipId { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008FD RID: 2301
		// (set) Token: 0x060008FE RID: 2302
		long MsgId { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008FF RID: 2303
		// (set) Token: 0x06000900 RID: 2304
		AdminActionStatus AdminActionStatus { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000901 RID: 2305
		// (set) Token: 0x06000902 RID: 2306
		DateTime? DeliveryTime { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000903 RID: 2307
		// (set) Token: 0x06000904 RID: 2308
		DsnFlags DsnCompleted { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000905 RID: 2309
		// (set) Token: 0x06000906 RID: 2310
		DsnFlags DsnNeeded { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000907 RID: 2311
		// (set) Token: 0x06000908 RID: 2312
		DsnRequestedFlags DsnRequested { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000909 RID: 2313
		// (set) Token: 0x0600090A RID: 2314
		Destination DeliveredDestination { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600090B RID: 2315
		// (set) Token: 0x0600090C RID: 2316
		string Email { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600090D RID: 2317
		// (set) Token: 0x0600090E RID: 2318
		string ORcpt { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600090F RID: 2319
		// (set) Token: 0x06000910 RID: 2320
		string PrimaryServerFqdnGuid { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000911 RID: 2321
		// (set) Token: 0x06000912 RID: 2322
		int RetryCount { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000913 RID: 2323
		// (set) Token: 0x06000914 RID: 2324
		Status Status { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000915 RID: 2325
		// (set) Token: 0x06000916 RID: 2326
		RequiredTlsAuthLevel? TlsAuthLevel { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000917 RID: 2327
		// (set) Token: 0x06000918 RID: 2328
		int OutboundIPPool { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000919 RID: 2329
		IExtendedPropertyCollection ExtendedProperties { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600091A RID: 2330
		bool IsDeleted { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600091B RID: 2331
		bool IsInSafetyNet { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600091C RID: 2332
		bool IsActive { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600091D RID: 2333
		bool PendingDatabaseUpdates { get; }

		// Token: 0x0600091E RID: 2334
		void MarkToDelete();

		// Token: 0x0600091F RID: 2335
		void Materialize(Transaction transaction);

		// Token: 0x06000920 RID: 2336
		void Commit(TransactionCommitMode commitMode);

		// Token: 0x06000921 RID: 2337
		void ReleaseFromActive();

		// Token: 0x06000922 RID: 2338
		void AddToSafetyNet();

		// Token: 0x06000923 RID: 2339
		IMailRecipientStorage MoveTo(long targetMailItemId);

		// Token: 0x06000924 RID: 2340
		IMailRecipientStorage CopyTo(long targetMailItemId);

		// Token: 0x06000925 RID: 2341
		void MinimizeMemory();
	}
}
