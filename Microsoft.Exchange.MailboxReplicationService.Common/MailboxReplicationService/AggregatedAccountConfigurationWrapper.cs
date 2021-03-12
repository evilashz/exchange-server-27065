using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F1 RID: 497
	[Serializable]
	internal class AggregatedAccountConfigurationWrapper : AggregatedAccountConfiguration, IRequestIndexEntry, IConfigurable, IAggregatedAccountConfigurationWrapper
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0002F68B File Offset: 0x0002D88B
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0002F693 File Offset: 0x0002D893
		public ADUser TargetUser { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0002F69C File Offset: 0x0002D89C
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0002F6C6 File Offset: 0x0002D8C6
		public Guid RequestGuid
		{
			get
			{
				Guid? syncRequestGuid = base.SyncRequestGuid;
				if (syncRequestGuid == null)
				{
					return Guid.Empty;
				}
				return syncRequestGuid.GetValueOrDefault();
			}
			set
			{
				base.SyncRequestGuid = new Guid?(value);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0002F6D4 File Offset: 0x0002D8D4
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		public string Name { get; set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0002F6E5 File Offset: 0x0002D8E5
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0002F6ED File Offset: 0x0002D8ED
		public RequestStatus Status { get; set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0002F6F6 File Offset: 0x0002D8F6
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0002F6FE File Offset: 0x0002D8FE
		public RequestFlags Flags { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0002F707 File Offset: 0x0002D907
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0002F70F File Offset: 0x0002D90F
		public string RemoteHostName { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0002F718 File Offset: 0x0002D918
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x0002F720 File Offset: 0x0002D920
		public string BatchName { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0002F729 File Offset: 0x0002D929
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x0002F731 File Offset: 0x0002D931
		public ADObjectId SourceMDB { get; set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0002F73A File Offset: 0x0002D93A
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0002F742 File Offset: 0x0002D942
		public ADObjectId TargetMDB { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0002F74B File Offset: 0x0002D94B
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0002F753 File Offset: 0x0002D953
		public ADObjectId StorageMDB { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0002F75C File Offset: 0x0002D95C
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0002F764 File Offset: 0x0002D964
		public string FilePath { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0002F76D File Offset: 0x0002D96D
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0002F770 File Offset: 0x0002D970
		public MRSRequestType Type
		{
			get
			{
				return MRSRequestType.Sync;
			}
			set
			{
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0002F772 File Offset: 0x0002D972
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x0002F77A File Offset: 0x0002D97A
		public ADObjectId TargetUserId { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0002F783 File Offset: 0x0002D983
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0002F78B File Offset: 0x0002D98B
		public Guid TargetExchangeGuid { get; set; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0002F794 File Offset: 0x0002D994
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x0002F79C File Offset: 0x0002D99C
		public ADObjectId SourceUserId { get; set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0002F7A5 File Offset: 0x0002D9A5
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x0002F7AD File Offset: 0x0002D9AD
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0002F7B6 File Offset: 0x0002D9B6
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0002F7BE File Offset: 0x0002D9BE
		public DateTime? WhenChanged { get; set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0002F7C7 File Offset: 0x0002D9C7
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0002F7CF File Offset: 0x0002D9CF
		public DateTime? WhenCreated { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0002F7E0 File Offset: 0x0002D9E0
		public DateTime? WhenChangedUTC { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0002F7E9 File Offset: 0x0002D9E9
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0002F7F1 File Offset: 0x0002D9F1
		public DateTime? WhenCreatedUTC { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0002F7FA File Offset: 0x0002D9FA
		public RequestIndexId RequestIndexId
		{
			get
			{
				return AggregatedAccountConfigurationWrapper.indexId;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0002F801 File Offset: 0x0002DA01
		public RequestJobObjectId GetRequestJobId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0002F808 File Offset: 0x0002DA08
		public RequestIndexEntryObjectId GetRequestIndexEntryId(RequestBase owner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0002F80F File Offset: 0x0002DA0F
		public IExchangePrincipal GetExchangePrincipal()
		{
			return base.Principal;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0002F817 File Offset: 0x0002DA17
		public void SetExchangePrincipal()
		{
			base.Principal = AggregatedAccountConfigurationWrapper.GetExchangePrincipal(this.TargetUser, this.TargetExchangeGuid, this.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0002F84C File Offset: 0x0002DA4C
		public void UpdateData(RequestJobBase requestJob)
		{
			base.Principal = AggregatedAccountConfigurationWrapper.GetExchangePrincipal(requestJob.TargetUser, requestJob.TargetExchangeGuid, requestJob.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox));
			this.TargetUser = requestJob.TargetUser;
			this.TargetExchangeGuid = requestJob.TargetExchangeGuid;
			base.EmailAddress = new SmtpAddress?(requestJob.EmailAddress);
			base.SyncFailureCode = requestJob.FailureCode;
			base.SyncFailureTimestamp = (ExDateTime?)requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.Failure);
			base.SyncFailureType = requestJob.FailureType;
			base.SyncLastUpdateTimestamp = (ExDateTime?)requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.LastUpdate);
			base.SyncQueuedTimestamp = (ExDateTime?)requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.Creation);
			base.SyncRequestGuid = new Guid?(requestJob.RequestGuid);
			base.SyncStartTimestamp = (ExDateTime?)requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.Start);
			base.SyncStatus = new RequestStatus?(requestJob.Status);
			base.SyncSuspendedTimestamp = (ExDateTime?)requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.Suspended);
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0002F961 File Offset: 0x0002DB61
		private static ExchangePrincipal GetExchangePrincipal(ADUser targetUser, Guid targetExchangeGuid, bool isAggregated)
		{
			if (!isAggregated)
			{
				return ExchangePrincipal.FromADUser(targetUser, RemotingOptions.AllowCrossSite);
			}
			return ExchangePrincipal.FromADUser(targetUser, RemotingOptions.AllowCrossSite).GetAggregatedExchangePrincipal(targetExchangeGuid);
		}

		// Token: 0x04000A5B RID: 2651
		private static RequestIndexId indexId = new RequestIndexId(RequestIndexLocation.UserMailbox);
	}
}
