using System;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200099C RID: 2460
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TeamMailboxNameIdResolver : LazyLookupTimeoutCache<string, ADRawEntry>
	{
		// Token: 0x06005AD5 RID: 23253 RVA: 0x0017BF24 File Offset: 0x0017A124
		private TeamMailboxNameIdResolver() : base(10, 5000, false, TimeSpan.FromHours(4.0), TimeSpan.FromHours(12.0))
		{
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0017BF50 File Offset: 0x0017A150
		public static ADRawEntry Resolve(IRecipientSession dataSession, string id, out Exception ex)
		{
			if (dataSession == null)
			{
				throw new ArgumentNullException("dataSession");
			}
			ADRawEntry result = null;
			ex = null;
			if (!string.IsNullOrEmpty(id))
			{
				TeamMailboxNameIdResolver.instance.dataSession = dataSession;
				try
				{
					TeamMailboxNameIdResolver.instance.newSidEx = null;
					result = TeamMailboxNameIdResolver.instance.Get(id);
					ex = TeamMailboxNameIdResolver.instance.newSidEx;
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (ADExternalException ex3)
				{
					ex = ex3;
				}
				catch (ADOperationException ex4)
				{
					ex = ex4;
				}
			}
			return result;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0017BFE4 File Offset: 0x0017A1E4
		protected override ADRawEntry CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			ADRawEntry adrawEntry = null;
			bool useGlobalCatalog = TeamMailboxNameIdResolver.instance.dataSession.UseGlobalCatalog;
			try
			{
				TeamMailboxNameIdResolver.instance.dataSession.UseGlobalCatalog = true;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
				{
					ITenantRecipientSession tenantRecipientSession = TeamMailboxNameIdResolver.instance.dataSession as ITenantRecipientSession;
					if (tenantRecipientSession != null)
					{
						adrawEntry = tenantRecipientSession.FindUniqueEntryByNetID(key, TeamMailboxNameIdResolver.UserObjectProperties);
					}
				}
				else
				{
					SecurityIdentifier securityIdentifier = null;
					try
					{
						securityIdentifier = new SecurityIdentifier(key);
					}
					catch (ArgumentException ex)
					{
						this.newSidEx = ex;
					}
					if (securityIdentifier != null)
					{
						adrawEntry = TeamMailboxNameIdResolver.instance.dataSession.FindADRawEntryBySid(securityIdentifier, TeamMailboxNameIdResolver.UserObjectProperties);
					}
				}
			}
			finally
			{
				TeamMailboxNameIdResolver.instance.dataSession.UseGlobalCatalog = useGlobalCatalog;
			}
			shouldAdd = (adrawEntry != null);
			return adrawEntry;
		}

		// Token: 0x04003210 RID: 12816
		private static readonly PropertyDefinition[] UserObjectProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.RecipientTypeDetails
		};

		// Token: 0x04003211 RID: 12817
		private static TeamMailboxNameIdResolver instance = new TeamMailboxNameIdResolver();

		// Token: 0x04003212 RID: 12818
		private IRecipientSession dataSession;

		// Token: 0x04003213 RID: 12819
		private ArgumentException newSidEx;
	}
}
