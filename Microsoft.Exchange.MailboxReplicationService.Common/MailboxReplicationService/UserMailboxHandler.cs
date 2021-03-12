using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F3 RID: 499
	internal class UserMailboxHandler<T> : RequestIndexEntryHandler<T> where T : class, IRequestIndexEntry, IAggregatedAccountConfigurationWrapper, new()
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		public override T Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity)
		{
			ArgumentValidator.ThrowIfNull("targetExchangeGuid", identity.TargetExchangeGuid);
			ADRecipient adrecipient = requestIndexEntryProvider.Read<ADRecipient>((IRecipientSession session) => session.FindByExchangeGuidIncludingAlternate(identity.TargetExchangeGuid));
			if (adrecipient == null)
			{
				MrsTracer.Common.Warning("No ADRecipient found with ExchangeGuid '{0}' including alternates.", new object[]
				{
					identity.TargetExchangeGuid
				});
				return default(T);
			}
			ADUser aduser = adrecipient as ADUser;
			if (aduser == null)
			{
				MrsTracer.Common.Warning("'{0}' is not a user.", new object[]
				{
					adrecipient.Id.ToString()
				});
				return default(T);
			}
			T t = Activator.CreateInstance<T>();
			t.TargetUser = aduser;
			t.TargetExchangeGuid = identity.TargetExchangeGuid;
			t.TargetMDB = aduser.Database;
			T result = t;
			result.RequestGuid = identity.RequestGuid;
			result.SetExchangePrincipal();
			return result;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0002FCF8 File Offset: 0x0002DEF8
		public override void Delete(RequestIndexEntryProvider requestIndexEntryProvider, T instance)
		{
			ArgumentValidator.ThrowIfNull("instance", instance);
			ArgumentValidator.ThrowIfNull("instance.TargetUser", instance.TargetUser);
			ArgumentValidator.ThrowIfNull("instance.TargetExchangeGuid", instance.TargetExchangeGuid);
			instance.SetExchangePrincipal();
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(instance.GetExchangePrincipal(), CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				instance.Delete(new MailboxStoreTypeProvider(instance.TargetUser)
				{
					MailboxSession = mailboxSession
				});
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
		public override void Save(RequestIndexEntryProvider requestIndexEntryProvider, T instance)
		{
			ArgumentValidator.ThrowIfNull("instance", instance);
			ArgumentValidator.ThrowIfNull("instance.TargetUser", instance.TargetUser);
			ArgumentValidator.ThrowIfNull("instance.TargetExchangeGuid", instance.TargetExchangeGuid);
			instance.SetExchangePrincipal();
			using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(instance.GetExchangePrincipal(), CultureInfo.InvariantCulture, "Client=MSExchangeMigration"))
			{
				instance.Save(new MailboxStoreTypeProvider(instance.TargetUser)
				{
					MailboxSession = mailboxSession
				});
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0002FE84 File Offset: 0x0002E084
		public override T CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, RequestIndexId requestIndexId)
		{
			ArgumentValidator.ThrowIfNull("requestJob", requestJob);
			ArgumentValidator.ThrowIfNull("requestJob.TargetUser", requestJob.TargetUser);
			ArgumentValidator.ThrowIfNull("requestJob.TargetExchangeGuid", requestJob.TargetExchangeGuid);
			ArgumentValidator.ThrowIfInvalidValue<RequestJobBase>("requestJob.RequestType", requestJob, (RequestJobBase x) => x.RequestType == MRSRequestType.Sync);
			T result = Activator.CreateInstance<T>();
			result.UpdateData(requestJob);
			return result;
		}
	}
}
