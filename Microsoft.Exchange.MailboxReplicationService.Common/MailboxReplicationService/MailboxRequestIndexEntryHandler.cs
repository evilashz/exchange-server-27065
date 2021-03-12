using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001EF RID: 495
	internal class MailboxRequestIndexEntryHandler : RequestIndexEntryHandler<MRSRequestMailboxEntry>
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x0002ED28 File Offset: 0x0002CF28
		public static string GetOtherRequests(ADUser user, Guid? requestGuid)
		{
			IEnumerable<IRequestIndexEntry> requestIndexEntries = MailboxRequestIndexEntryHandler.GetRequestIndexEntries(user, null);
			IEnumerable<IRequestIndexEntry> source = from r in requestIndexEntries
			where !MailboxRelocationRequestStatistics.IsTerminalState(r.Status) && (requestGuid == null || r.RequestGuid != requestGuid)
			select r;
			List<IRequestIndexEntry> list = new List<IRequestIndexEntry>();
			foreach (IGrouping<ADObjectId, IRequestIndexEntry> grouping in from r in source
			group r by r.StorageMDB)
			{
				using (RequestJobProvider requestJobProvider = new RequestJobProvider(grouping.Key.ObjectGuid))
				{
					foreach (IRequestIndexEntry requestIndexEntry in grouping)
					{
						TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)requestJobProvider.Read<TransactionalRequestJob>(requestIndexEntry.GetRequestJobId(), ReadJobFlags.SkipValidation | ReadJobFlags.SkipReadingMailboxRequestIndexEntries);
						if (transactionalRequestJob != null && !MailboxRelocationRequestStatistics.IsTerminalState(transactionalRequestJob))
						{
							list.Add(requestIndexEntry);
						}
					}
				}
			}
			return string.Join<Guid>(", ", from r in list
			select r.RequestGuid);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0002EE80 File Offset: 0x0002D080
		public override MRSRequestMailboxEntry CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, RequestIndexId requestIndexId)
		{
			ArgumentValidator.ThrowIfNull("requestJob", requestJob);
			return new MRSRequestMailboxEntry
			{
				Type = requestJob.RequestType,
				Name = requestJob.Name,
				RequestGuid = requestJob.RequestGuid,
				Status = requestJob.Status,
				Flags = requestJob.Flags,
				RemoteHostName = requestJob.RemoteHostName,
				BatchName = requestJob.BatchName,
				SourceMDB = requestJob.SourceDatabase,
				TargetMDB = requestJob.TargetDatabase,
				StorageMDB = requestJob.WorkItemQueueMdb,
				FilePath = requestJob.FilePath,
				TargetUserId = requestJob.TargetUserId,
				SourceUserId = requestJob.SourceUserId,
				OrganizationId = requestJob.OrganizationId,
				RequestIndexId = requestIndexId
			};
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0002EF50 File Offset: 0x0002D150
		public override void Delete(RequestIndexEntryProvider requestIndexEntryProvider, MRSRequestMailboxEntry instance)
		{
			this.ValidateRequestIndexId(instance.RequestIndexId);
			ADUser aduser = requestIndexEntryProvider.ReadADUser(instance.RequestIndexId.Mailbox, Guid.Empty);
			if (instance.Principal == null)
			{
				instance.Principal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
			}
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(instance.Principal, CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				MailboxStoreTypeProvider session = new MailboxStoreTypeProvider(aduser)
				{
					MailboxSession = mailboxSession
				};
				instance.Delete(session);
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
		public override MRSRequestMailboxEntry Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity)
		{
			this.ValidateRequestIndexId(identity.IndexId);
			ADUser aduser = requestIndexEntryProvider.ReadADUser(identity.IndexId.Mailbox, Guid.Empty);
			if (aduser == null)
			{
				return null;
			}
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
			MRSRequestMailboxEntry result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				MailboxStoreTypeProvider session = new MailboxStoreTypeProvider(aduser)
				{
					MailboxSession = mailboxSession
				};
				MRSRequestMailboxEntry mrsrequestMailboxEntry = MRSRequest.Read<MRSRequestMailboxEntry>(session, identity.RequestGuid);
				mrsrequestMailboxEntry.RequestIndexId = identity.IndexId;
				mrsrequestMailboxEntry.OrganizationId = identity.OrganizationId;
				result = mrsrequestMailboxEntry;
			}
			return result;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0002F08C File Offset: 0x0002D28C
		public override IEnumerable<MRSRequestMailboxEntry> FindPaged(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = filter as RequestIndexEntryQueryFilter;
			ADUser aduser = requestIndexEntryProvider.ReadADUser(requestIndexEntryQueryFilter.MailboxId, Guid.Empty);
			if (aduser == null)
			{
				return null;
			}
			List<MRSRequestMailboxEntry> list = new List<MRSRequestMailboxEntry>(MailboxRequestIndexEntryHandler.GetRequestIndexEntries(aduser, requestIndexEntryQueryFilter));
			foreach (MRSRequestMailboxEntry mrsrequestMailboxEntry in list)
			{
				mrsrequestMailboxEntry.RequestIndexId = new RequestIndexId(requestIndexEntryQueryFilter.MailboxId);
				mrsrequestMailboxEntry.OrganizationId = aduser.OrganizationId;
			}
			return list;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0002F120 File Offset: 0x0002D320
		public override void Save(RequestIndexEntryProvider requestIndexEntryProvider, MRSRequestMailboxEntry instance)
		{
			this.ValidateRequestIndexId(instance.RequestIndexId);
			ADUser aduser = requestIndexEntryProvider.ReadADUser(instance.RequestIndexId.Mailbox, Guid.Empty);
			if (instance.Principal == null)
			{
				instance.Principal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
			}
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(instance.Principal, CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				MailboxStoreTypeProvider session = new MailboxStoreTypeProvider(aduser)
				{
					MailboxSession = mailboxSession
				};
				instance.Save(session);
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0002F1B0 File Offset: 0x0002D3B0
		private static IEnumerable<MRSRequestMailboxEntry> GetRequestIndexEntries(ADUser user, RequestIndexEntryQueryFilter requestIndexEntryQueryFilter)
		{
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			IEnumerable<MRSRequestMailboxEntry> result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				result = MRSRequestMailboxEntry.Read(mailboxSession, requestIndexEntryQueryFilter);
			}
			return result;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		private void ValidateRequestIndexId(RequestIndexId requestIndexId)
		{
			if (requestIndexId == null)
			{
				throw new ArgumentNullException("requestIndexId");
			}
			if (requestIndexId.Location != RequestIndexLocation.Mailbox)
			{
				throw new ArgumentException("This RequestIndexEntryHandler only supports saving IRequestIndexEntries to a mailbox.", "requestIndexId");
			}
			if (requestIndexId.Mailbox == null)
			{
				throw new ArgumentException("The RequestIndexId supplied must specify a mailbox.", "requestIndexId");
			}
		}
	}
}
