using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000025 RID: 37
	[XmlType(TypeName = "JobPickupRec")]
	public class JobPickupRec : XMLSerializableBase
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00009135 File Offset: 0x00007335
		public JobPickupRec()
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009140 File Offset: 0x00007340
		internal JobPickupRec(MoveJob job, JobPickupResult pickupResult, DateTime nextRecommendedPickup, LocalizedString locMessage, ResourceReservationException ex = null)
		{
			this.RequestGuid = job.RequestGuid;
			this.RequestType = job.RequestType;
			this.RequestStatus = job.Status;
			this.WorkloadType = job.WorkloadType;
			this.Priority = job.Priority;
			this.LastUpdateTimeStamp = job.LastUpdateTimeStamp;
			this.Timestamp = DateTime.UtcNow;
			this.PickupResult = pickupResult;
			this.NextRecommendedPickup = nextRecommendedPickup;
			this.locMessage = locMessage;
			if (this.PickupResult == JobPickupResult.ReservationFailure && ex != null)
			{
				this.ReservationFailureRecord = new JobPickupRec.ReservationFailureRec(ex);
				return;
			}
			this.ReservationFailureRecord = null;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000091E2 File Offset: 0x000073E2
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000091EA File Offset: 0x000073EA
		[XmlAttribute(AttributeName = "RequestGuid")]
		public Guid RequestGuid { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000091F3 File Offset: 0x000073F3
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000091FB File Offset: 0x000073FB
		[XmlAttribute(AttributeName = "Type")]
		public MRSRequestType RequestType { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009204 File Offset: 0x00007404
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000920C File Offset: 0x0000740C
		[XmlAttribute(AttributeName = "Status")]
		public RequestStatus RequestStatus { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00009215 File Offset: 0x00007415
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000921D File Offset: 0x0000741D
		[XmlAttribute(AttributeName = "Workload")]
		public RequestWorkloadType WorkloadType { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009226 File Offset: 0x00007426
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000922E File Offset: 0x0000742E
		[XmlAttribute(AttributeName = "Pri")]
		public int Priority { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00009237 File Offset: 0x00007437
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000923F File Offset: 0x0000743F
		[XmlAttribute(AttributeName = "LastUpdate")]
		public DateTime LastUpdateTimeStamp { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009248 File Offset: 0x00007448
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009250 File Offset: 0x00007450
		[XmlAttribute(AttributeName = "PickupAttemptTime")]
		public DateTime Timestamp { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009259 File Offset: 0x00007459
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009261 File Offset: 0x00007461
		[XmlAttribute(AttributeName = "Result")]
		public JobPickupResult PickupResult { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000926A File Offset: 0x0000746A
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000927D File Offset: 0x0000747D
		[XmlText]
		public string Message
		{
			get
			{
				return this.locMessage.ToString();
			}
			set
			{
				this.locMessage = new LocalizedString(value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000928B File Offset: 0x0000748B
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009293 File Offset: 0x00007493
		[XmlIgnore]
		public LocalizedString LocMessage
		{
			get
			{
				return this.locMessage;
			}
			set
			{
				this.locMessage = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000929C File Offset: 0x0000749C
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000092A4 File Offset: 0x000074A4
		[XmlAttribute(AttributeName = "NextRecommendPickup")]
		public DateTime NextRecommendedPickup { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000092AD File Offset: 0x000074AD
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000092B5 File Offset: 0x000074B5
		[XmlElement(ElementName = "ReserveFailDetail")]
		public JobPickupRec.ReservationFailureRec ReservationFailureRecord { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000092BE File Offset: 0x000074BE
		public bool HasPickupFailed
		{
			get
			{
				return this.PickupResult >= JobPickupResult.ReservationFailure;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000092D0 File Offset: 0x000074D0
		public LocalizedString GetPickupFailureMessage()
		{
			if (!this.HasPickupFailed)
			{
				return LocalizedString.Empty;
			}
			return this.locMessage;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000092E6 File Offset: 0x000074E6
		public bool IsQueuedOrActiveJob
		{
			get
			{
				return this.PickupResult != JobPickupResult.CompletedJobCleanedUp && this.PickupResult != JobPickupResult.CompletedJobSkipped && this.PickupResult != JobPickupResult.InvalidJob && (this.RequestStatus == RequestStatus.Queued || this.RequestStatus == RequestStatus.InProgress);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000931E File Offset: 0x0000751E
		public bool IsActiveJob
		{
			get
			{
				return this.RequestStatus == RequestStatus.InProgress && (this.PickupResult == JobPickupResult.JobPickedUp || this.PickupResult == JobPickupResult.JobAlreadyActive);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009340 File Offset: 0x00007540
		public bool IsQueuedJob
		{
			get
			{
				return this.IsQueuedOrActiveJob && !this.IsActiveJob;
			}
		}

		// Token: 0x040000A5 RID: 165
		private LocalizedString locMessage;

		// Token: 0x02000026 RID: 38
		public sealed class ReservationFailureRec : XMLSerializableBase
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x00009355 File Offset: 0x00007555
			public ReservationFailureRec()
			{
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x00009360 File Offset: 0x00007560
			internal ReservationFailureRec(ResourceReservationException ex)
			{
				this.Reason = ex.GetType().Name;
				StaticCapacityExceededReservationException ex2 = ex as StaticCapacityExceededReservationException;
				if (ex2 != null)
				{
					this.Name = ex2.ResourceName;
					this.Type = ex2.ResourceType;
				}
				WlmCapacityExceededReservationException ex3 = ex as WlmCapacityExceededReservationException;
				if (ex3 != null)
				{
					this.Name = ex3.ResourceName;
					this.Type = ex3.ResourceType;
					this.WLMResourceKey = ex3.WlmResourceKey;
					this.WLMResourceMonitorType = ex3.WlmResourceMetricType;
				}
				WlmResourceUnhealthyException ex4 = ex as WlmResourceUnhealthyException;
				if (ex4 != null)
				{
					this.Name = ex4.ResourceName;
					this.Type = ex4.ResourceType;
					this.WLMResourceKey = ex4.WlmResourceKey;
					this.WLMResourceMonitorType = ex4.WlmResourceMetricType;
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000941A File Offset: 0x0000761A
			// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009422 File Offset: 0x00007622
			[XmlText]
			public string Reason { get; set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000942B File Offset: 0x0000762B
			// (set) Token: 0x060001AA RID: 426 RVA: 0x00009433 File Offset: 0x00007633
			[XmlAttribute(AttributeName = "ResName")]
			public string Name { get; set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001AB RID: 427 RVA: 0x0000943C File Offset: 0x0000763C
			// (set) Token: 0x060001AC RID: 428 RVA: 0x00009444 File Offset: 0x00007644
			[XmlAttribute(AttributeName = "ResType")]
			public string Type { get; set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001AD RID: 429 RVA: 0x0000944D File Offset: 0x0000764D
			// (set) Token: 0x060001AE RID: 430 RVA: 0x00009455 File Offset: 0x00007655
			[XmlAttribute(AttributeName = "WLMResKey")]
			public string WLMResourceKey { get; set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001AF RID: 431 RVA: 0x0000945E File Offset: 0x0000765E
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009466 File Offset: 0x00007666
			[XmlAttribute(AttributeName = "WLMResMonitorType")]
			public int WLMResourceMonitorType { get; set; }
		}
	}
}
