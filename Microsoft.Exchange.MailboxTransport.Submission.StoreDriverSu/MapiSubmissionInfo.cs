using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	internal class MapiSubmissionInfo : SubmissionInfo
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00008808 File Offset: 0x00006A08
		public MapiSubmissionInfo(string serverDN, Guid mailboxGuid, byte[] entryId, byte[] parentEntryId, long eventCounter, string serverFqdn, IPAddress networkAddress, Guid mdbGuid, string databaseName, DateTime originalCreateTime, bool isPublicFolder, TenantPartitionHint tenantHint, string mailboxHopLatency, LatencyTracker latencyTracker, bool shouldDeprioritize, bool shouldThrottle, IStoreDriverTracer storeDriverTracer) : base(serverDN, serverFqdn, networkAddress, mdbGuid, databaseName, originalCreateTime, tenantHint, mailboxHopLatency, latencyTracker, shouldDeprioritize, shouldThrottle)
		{
			this.mailboxGuid = mailboxGuid;
			this.entryId = entryId;
			this.parentEntryId = parentEntryId;
			this.eventCounter = eventCounter;
			this.isPublicFolder = isPublicFolder;
			this.storeDriverTracer = storeDriverTracer;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000885E File Offset: 0x00006A5E
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00008866 File Offset: 0x00006A66
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000886E File Offset: 0x00006A6E
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00008876 File Offset: 0x00006A76
		public byte[] ParentEntryId
		{
			get
			{
				return this.parentEntryId;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000887E File Offset: 0x00006A7E
		public bool IsPublicFolder
		{
			get
			{
				return this.isPublicFolder;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00008886 File Offset: 0x00006A86
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000888E File Offset: 0x00006A8E
		public TransportMiniRecipient SenderAdEntry
		{
			get
			{
				return this.senderAdEntry;
			}
			internal set
			{
				this.senderAdEntry = value;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008897 File Offset: 0x00006A97
		public override SubmissionItem CreateSubmissionItem(MailItemSubmitter context)
		{
			return new MapiSubmissionItem(this, context, this.storeDriverTracer);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000088A6 File Offset: 0x00006AA6
		public override OrganizationId GetOrganizationId()
		{
			if (this.senderAdEntry == null)
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Using ForestWideOrgId scope for PF replication mail");
				return OrganizationId.ForestWideOrgId;
			}
			return this.senderAdEntry.OrganizationId;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000088E3 File Offset: 0x00006AE3
		public override SenderGuidTraceFilter GetTraceFilter()
		{
			return new SenderGuidTraceFilter(base.MdbGuid, this.mailboxGuid);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000088F8 File Offset: 0x00006AF8
		public override SubmissionPoisonContext GetPoisonContext()
		{
			return new SubmissionPoisonContext(this.mailboxGuid.Equals(Guid.Empty) ? base.MdbGuid : this.mailboxGuid, this.eventCounter);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008934 File Offset: 0x00006B34
		public override void LogEvent(SubmissionInfo.Event submissionInfoEvent)
		{
			switch (submissionInfoEvent)
			{
			case SubmissionInfo.Event.StoreDriverSubmissionPoisonMessageInSubmission:
				this.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_StoreDriverSubmissionPoisonMessageInMapiSubmit);
				return;
			case SubmissionInfo.Event.FailedToGenerateNdrInSubmission:
				break;
			case SubmissionInfo.Event.InvalidSender:
				this.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_InvalidSender);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008970 File Offset: 0x00006B70
		public override void LogEvent(SubmissionInfo.Event submissionInfoEvent, Exception exception)
		{
			switch (submissionInfoEvent)
			{
			case SubmissionInfo.Event.StoreDriverSubmissionPoisonMessage:
				this.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_StoreDriverSubmissionPoisonMessage, exception);
				return;
			case SubmissionInfo.Event.StoreDriverSubmissionPoisonMessageInSubmission:
				break;
			case SubmissionInfo.Event.FailedToGenerateNdrInSubmission:
				this.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_FailedToGenerateNDRInMapiSubmit, exception);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000089AC File Offset: 0x00006BAC
		public void LoadAdRawEntry()
		{
			if (Guid.Empty.Equals(this.mailboxGuid))
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail(this.storeDriverTracer.MessageProbeActivityId, 0L, "Mailbox GUID was empty, unable to load AD entry.");
				return;
			}
			try
			{
				this.senderAdEntry = StoreProvider.FindByExchangeGuidIncludingAlternate<TransportMiniRecipient>(this.mailboxGuid, base.TenantHint);
			}
			catch (NonUniqueRecipientException)
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<Guid>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Multiple objects with Mailbox Guid {0} were found.", this.mailboxGuid);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008A48 File Offset: 0x00006C48
		public string GetSenderEmailAddress()
		{
			if (this.senderAdEntry == null)
			{
				return string.Empty;
			}
			SmtpAddress primarySmtpAddress = this.senderAdEntry.PrimarySmtpAddress;
			if (primarySmtpAddress.IsValidAddress)
			{
				return primarySmtpAddress.ToString();
			}
			ProxyAddressCollection emailAddresses = this.senderAdEntry.EmailAddresses;
			if (emailAddresses == null || 0 >= emailAddresses.Count)
			{
				return string.Empty;
			}
			ProxyAddress proxyAddress = emailAddresses.Find(new Predicate<ProxyAddress>(this.IsSmtpAddress));
			if (null != proxyAddress)
			{
				return proxyAddress.ToString();
			}
			return emailAddresses[0].ToString();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008AD3 File Offset: 0x00006CD3
		public MultiValuedProperty<Guid> GetAggregatedMailboxGuids()
		{
			if (this.senderAdEntry != null)
			{
				return this.senderAdEntry.AggregatedMailboxGuids;
			}
			return MultiValuedProperty<Guid>.Empty;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008AEE File Offset: 0x00006CEE
		public MultiValuedProperty<CultureInfo> GetSenderLocales()
		{
			if (this.senderAdEntry != null)
			{
				return this.senderAdEntry.Languages;
			}
			return MultiValuedProperty<CultureInfo>.Empty;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00008B0C File Offset: 0x00006D0C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Event {0}, mailbox {1}, mdb {2}", new object[]
			{
				this.eventCounter,
				this.mailboxGuid,
				base.MdbGuid
			});
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008B5A File Offset: 0x00006D5A
		private bool IsSmtpAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.Smtp);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008B6C File Offset: 0x00006D6C
		private void LogEvent(ExEventLog.EventTuple eventTuple)
		{
			StoreDriverSubmission.LogEvent(eventTuple, null, new object[]
			{
				this.EventCounter,
				this.MailboxGuid,
				base.MdbGuid
			});
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008BB4 File Offset: 0x00006DB4
		private void LogEvent(ExEventLog.EventTuple eventTuple, Exception exception)
		{
			StoreDriverSubmission.LogEvent(eventTuple, null, new object[]
			{
				this.EventCounter,
				this.MailboxGuid,
				base.MdbGuid,
				exception
			});
		}

		// Token: 0x04000083 RID: 131
		private readonly Guid mailboxGuid;

		// Token: 0x04000084 RID: 132
		private readonly byte[] entryId;

		// Token: 0x04000085 RID: 133
		private readonly byte[] parentEntryId;

		// Token: 0x04000086 RID: 134
		private readonly long eventCounter;

		// Token: 0x04000087 RID: 135
		private readonly bool isPublicFolder;

		// Token: 0x04000088 RID: 136
		private TransportMiniRecipient senderAdEntry;

		// Token: 0x04000089 RID: 137
		private IStoreDriverTracer storeDriverTracer;
	}
}
