using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F2 RID: 498
	internal class AggregatedAccountListConfigurationWrapper : AggregatedAccountListConfiguration, IRequestIndexEntry, IConfigurable, IAggregatedAccountConfigurationWrapper
	{
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x0002F990 File Offset: 0x0002DB90
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0002F998 File Offset: 0x0002DB98
		public ADUser TargetUser { get; set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x0002F9A1 File Offset: 0x0002DBA1
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x0002F9A9 File Offset: 0x0002DBA9
		public string Name { get; set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0002F9B2 File Offset: 0x0002DBB2
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x0002F9BA File Offset: 0x0002DBBA
		public RequestStatus Status { get; set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0002F9C3 File Offset: 0x0002DBC3
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x0002F9CB File Offset: 0x0002DBCB
		public RequestFlags Flags { get; set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0002F9D4 File Offset: 0x0002DBD4
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x0002F9DC File Offset: 0x0002DBDC
		public string RemoteHostName { get; set; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0002F9E5 File Offset: 0x0002DBE5
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x0002F9ED File Offset: 0x0002DBED
		public string BatchName { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0002F9F6 File Offset: 0x0002DBF6
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0002F9FE File Offset: 0x0002DBFE
		public ADObjectId SourceMDB { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0002FA07 File Offset: 0x0002DC07
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x0002FA0F File Offset: 0x0002DC0F
		public ADObjectId TargetMDB { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0002FA18 File Offset: 0x0002DC18
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x0002FA20 File Offset: 0x0002DC20
		public ADObjectId StorageMDB { get; set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0002FA29 File Offset: 0x0002DC29
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0002FA31 File Offset: 0x0002DC31
		public string FilePath { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0002FA3A File Offset: 0x0002DC3A
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x0002FA3D File Offset: 0x0002DC3D
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

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0002FA3F File Offset: 0x0002DC3F
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x0002FA47 File Offset: 0x0002DC47
		public ADObjectId TargetUserId { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0002FA50 File Offset: 0x0002DC50
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x0002FA58 File Offset: 0x0002DC58
		public Guid TargetExchangeGuid { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0002FA61 File Offset: 0x0002DC61
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x0002FA69 File Offset: 0x0002DC69
		public ADObjectId SourceUserId { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0002FA72 File Offset: 0x0002DC72
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x0002FA7A File Offset: 0x0002DC7A
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0002FA83 File Offset: 0x0002DC83
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x0002FA8B File Offset: 0x0002DC8B
		public DateTime? WhenChanged { get; set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0002FA94 File Offset: 0x0002DC94
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x0002FA9C File Offset: 0x0002DC9C
		public DateTime? WhenCreated { get; set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0002FAA5 File Offset: 0x0002DCA5
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x0002FAAD File Offset: 0x0002DCAD
		public DateTime? WhenChangedUTC { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0002FAB6 File Offset: 0x0002DCB6
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x0002FABE File Offset: 0x0002DCBE
		public DateTime? WhenCreatedUTC { get; set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0002FAC7 File Offset: 0x0002DCC7
		public RequestIndexId RequestIndexId
		{
			get
			{
				return AggregatedAccountListConfigurationWrapper.indexId;
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0002FACE File Offset: 0x0002DCCE
		public RequestJobObjectId GetRequestJobId()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0002FAD5 File Offset: 0x0002DCD5
		public RequestIndexEntryObjectId GetRequestIndexEntryId(RequestBase owner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0002FADC File Offset: 0x0002DCDC
		public IExchangePrincipal GetExchangePrincipal()
		{
			return base.Principal;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0002FAE4 File Offset: 0x0002DCE4
		public void SetExchangePrincipal()
		{
			if (base.Principal == null)
			{
				base.Principal = AggregatedAccountListConfigurationWrapper.GetExchangePrincipalFromADUser(this.TargetUser);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0002FB00 File Offset: 0x0002DD00
		public void UpdateData(RequestJobBase requestJob)
		{
			base.Principal = AggregatedAccountListConfigurationWrapper.GetExchangePrincipalFromADUser(requestJob.TargetUser);
			this.TargetUser = requestJob.TargetUser;
			this.TargetExchangeGuid = requestJob.TargetExchangeGuid;
			base.SmtpAddress = requestJob.EmailAddress;
			base.RequestGuid = requestJob.RequestGuid;
			base.AggregatedMailboxGuid = (requestJob.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox) ? requestJob.TargetExchangeGuid : Guid.Empty);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0002FB7D File Offset: 0x0002DD7D
		private static ExchangePrincipal GetExchangePrincipalFromADUser(ADUser targetUser)
		{
			return ExchangePrincipal.FromADUser(targetUser, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0002FB9B File Offset: 0x0002DD9B
		Guid IRequestIndexEntry.get_RequestGuid()
		{
			return base.RequestGuid;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0002FBA3 File Offset: 0x0002DDA3
		void IRequestIndexEntry.set_RequestGuid(Guid A_1)
		{
			base.RequestGuid = A_1;
		}

		// Token: 0x04000A6E RID: 2670
		private static RequestIndexId indexId = new RequestIndexId(RequestIndexLocation.UserMailboxList);
	}
}
