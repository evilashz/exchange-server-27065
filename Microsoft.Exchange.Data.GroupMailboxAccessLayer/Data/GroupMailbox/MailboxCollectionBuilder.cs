using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxCollectionBuilder
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000EFDE File Offset: 0x0000D1DE
		public MailboxCollectionBuilder(IRecipientSession adSession)
		{
			this.adSession = adSession;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F018 File Offset: 0x0000D218
		public IEnumerable<UserMailbox> BuildUserMailboxes(GroupMailboxLocator groupLocator, IEnumerable<MailboxAssociation> associations, bool loadAllDetails)
		{
			IEnumerable<ADObjectId> owners = loadAllDetails ? this.GetGroupOwners(groupLocator) : null;
			IEnumerable<IMailboxBuilder<UserMailbox>> enumerable = from association in associations
			select new UserMailboxBuilder(association.User, owners).BuildFromAssociation(association);
			if (loadAllDetails)
			{
				return this.BuildMailboxesFromAD<UserMailbox>(enumerable, UserMailboxBuilder.AllADProperties);
			}
			return from builder in enumerable
			select builder.Mailbox;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		public IEnumerable<GroupMailbox> BuildGroupMailboxes(IEnumerable<MailboxAssociation> associations, bool loadAllDetails)
		{
			IEnumerable<IMailboxBuilder<GroupMailbox>> enumerable = from association in associations
			select new GroupMailboxBuilder(association.Group).BuildFromAssociation(association);
			if (loadAllDetails)
			{
				return this.BuildMailboxesFromAD<GroupMailbox>(enumerable, GroupMailboxBuilder.AllADProperties);
			}
			return from builder in enumerable
			select builder.Mailbox;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F47C File Offset: 0x0000D67C
		private IEnumerable<T> BuildMailboxesFromAD<T>(IEnumerable<IMailboxBuilder<T>> mailboxBuilders, PropertyDefinition[] properties) where T : ILocatableMailbox
		{
			List<IMailboxBuilder<T>> queuedBuilders = new List<IMailboxBuilder<T>>(ADRecipientObjectSession.ReadMultipleMaxBatchSize);
			foreach (IMailboxBuilder<T> builder in mailboxBuilders)
			{
				queuedBuilders.Add(builder);
				if (queuedBuilders.Count == ADRecipientObjectSession.ReadMultipleMaxBatchSize)
				{
					IEnumerable<T> builtMailboxes = this.BuildMailboxBatchFromAD<T>(queuedBuilders, properties);
					foreach (T mailbox in builtMailboxes)
					{
						yield return mailbox;
					}
					queuedBuilders.Clear();
				}
			}
			if (queuedBuilders.Count > 0)
			{
				IEnumerable<T> builtMailboxes2 = this.BuildMailboxBatchFromAD<T>(queuedBuilders, properties);
				foreach (T mailbox2 in builtMailboxes2)
				{
					yield return mailbox2;
				}
			}
			yield break;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F998 File Offset: 0x0000DB98
		private IEnumerable<T> BuildMailboxBatchFromAD<T>(List<IMailboxBuilder<T>> builders, PropertyDefinition[] properties) where T : ILocatableMailbox
		{
			Stopwatch stopwatch = null;
			if (MailboxCollectionBuilder.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
			}
			string[] legacyDNs = builders.Select(delegate(IMailboxBuilder<T> builder)
			{
				T mailbox2 = builder.Mailbox;
				return mailbox2.Locator.LegacyDn;
			}).ToArray<string>();
			Result<ADRawEntry>[] results = this.adSession.FindByLegacyExchangeDNs(legacyDNs, properties);
			if (MailboxCollectionBuilder.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				stopwatch.Stop();
				MailboxCollectionBuilder.Tracer.TraceDebug<long>(0L, "MailboxCollectionBuilder::BuildMailboxesFromAD. Batch AD Query took {0} ms.", stopwatch.ElapsedMilliseconds);
			}
			var pairs = builders.Zip(results, (IMailboxBuilder<T> builder, Result<ADRawEntry> adResult) => new
			{
				Builder = builder,
				ADResult = adResult
			});
			foreach (var pair in pairs)
			{
				if (pair.ADResult.Error != null || pair.ADResult.Data == null)
				{
					Microsoft.Exchange.Diagnostics.Trace tracer = MailboxCollectionBuilder.Tracer;
					long id = (long)this.GetHashCode();
					string formatString = "MailboxCollectionBuilder::BuildMailboxesFromAD. Unable to find LegacyDN {0} in AD using the batch query: {1}";
					T mailbox = pair.Builder.Mailbox;
					tracer.TraceWarning<string, string>(id, formatString, mailbox.Locator.LegacyDn, (pair.ADResult.Error != null) ? pair.ADResult.Error.ToString() : "Result.Data is null");
					ADUser groupADUser;
					if (this.TryGetGroupADUserFromADCache(pair.Builder.Mailbox, out groupADUser))
					{
						yield return pair.Builder.BuildFromDirectory(groupADUser).Mailbox;
					}
				}
				else
				{
					yield return pair.Builder.BuildFromDirectory(pair.ADResult.Data).Mailbox;
				}
			}
			yield break;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private IEnumerable<ADObjectId> GetGroupOwners(GroupMailboxLocator groupMailboxLocator)
		{
			MailboxCollectionBuilder.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupMailboxAccessLayer::GetGroupOwners. Retrieving AD User for Group by LegacyDN={0}", groupMailboxLocator.LegacyDn);
			ADUser aduser = this.adSession.FindByLegacyExchangeDN(groupMailboxLocator.LegacyDn) as ADUser;
			if (aduser == null)
			{
				throw new MailboxNotFoundException(ServerStrings.InvalidAddressError(groupMailboxLocator.LegacyDn));
			}
			return aduser.Owners;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000FA20 File Offset: 0x0000DC20
		private bool TryGetGroupADUserFromADCache(ILocatableMailbox mailbox, out ADUser adUser)
		{
			GroupMailbox groupMailbox = mailbox as GroupMailbox;
			adUser = null;
			if (groupMailbox == null)
			{
				return false;
			}
			if (groupMailbox.JoinDate < ExDateTime.UtcNow.Subtract(MailboxCollectionBuilder.JoinDateThresholdForOneOffADLookup))
			{
				return false;
			}
			bool result;
			try
			{
				adUser = groupMailbox.Locator.FindAdUser();
				result = true;
			}
			catch (MailboxNotFoundException)
			{
				MailboxCollectionBuilder.Tracer.TraceError<string>((long)this.GetHashCode(), "MailboxCollectionBuilder::TryGetGroupADUserFromADCache. Unable to find group with LegacyDN {0} in AD using the one-off query; it is not therefore in the AD cache.", groupMailbox.Locator.LegacyDn);
				result = false;
			}
			return result;
		}

		// Token: 0x04000155 RID: 341
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x04000156 RID: 342
		private static readonly TimeSpan JoinDateThresholdForOneOffADLookup = TimeSpan.FromHours(6.0);

		// Token: 0x04000157 RID: 343
		private readonly IRecipientSession adSession;
	}
}
