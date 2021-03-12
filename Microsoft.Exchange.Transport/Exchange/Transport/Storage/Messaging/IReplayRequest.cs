using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000F2 RID: 242
	internal interface IReplayRequest : IDisposable
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000957 RID: 2391
		long TotalReplayedMessages { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000958 RID: 2392
		long ContinuationToken { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000959 RID: 2393
		DateTime DateCreated { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600095A RID: 2394
		Destination Destination { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600095B RID: 2395
		// (set) Token: 0x0600095C RID: 2396
		string DiagnosticInformation { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600095D RID: 2397
		DateTime EndTime { get; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600095E RID: 2398
		long RequestId { get; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		long PrimaryRequestId { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000961 RID: 2401
		DateTime StartTime { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000962 RID: 2402
		// (set) Token: 0x06000963 RID: 2403
		ResubmitRequestState State { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000964 RID: 2404
		int OutstandingMailItemCount { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000965 RID: 2405
		DateTime LastResubmittedMessageOrginalDeliveryTime { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000966 RID: 2406
		Guid CorrelationId { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000967 RID: 2407
		bool IsTestRequest { get; }

		// Token: 0x06000968 RID: 2408
		IEnumerable<TransportMailItem> GetMessagesForRedelivery(int count);

		// Token: 0x06000969 RID: 2409
		void AddMailItemReference();

		// Token: 0x0600096A RID: 2410
		void ReleaseMailItemReference();

		// Token: 0x0600096B RID: 2411
		void Commit();

		// Token: 0x0600096C RID: 2412
		void Materialize(Transaction transaction);

		// Token: 0x0600096D RID: 2413
		void Delete();
	}
}
