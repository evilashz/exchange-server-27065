using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001B6 RID: 438
	internal class DatacenterUMGrammarTenantCache : UMGrammarTenantCache
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x000635C4 File Offset: 0x000617C4
		protected override Dictionary<Guid, OrganizationId> GetTenantsNeedingGrammars(List<ADUser> systemMailboxes)
		{
			UMTracer.DebugTrace("DatacenterUMGrammarTenantCache.GetTenantsNeedingGrammars", new object[0]);
			Dictionary<Guid, OrganizationId> dictionary = new Dictionary<Guid, OrganizationId>();
			foreach (ADUser aduser in systemMailboxes)
			{
				if (aduser.OrganizationId != null && aduser.OrganizationId.ConfigurationUnit != null && aduser.OrganizationId.OrganizationalUnit != null)
				{
					if (SharedConfiguration.IsSharedConfiguration(aduser.OrganizationId))
					{
						UMTracer.DebugTrace("Skipping Mbox='{0}', Organization='{1}' because it is a shared configuration", new object[]
						{
							aduser.ExchangeGuid,
							aduser.OrganizationId
						});
					}
					else
					{
						IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(aduser.OrganizationId, null);
						if (iadrecipientLookup.TenantSizeExceedsThreshold(GrammarRecipientHelper.GetUserFilter(), 10))
						{
							if (!dictionary.ContainsKey(aduser.ExchangeGuid))
							{
								UMTracer.DebugTrace("Adding Mbox='{0}', Organization='{1}'", new object[]
								{
									aduser.ExchangeGuid,
									aduser.OrganizationId
								});
								dictionary.Add(aduser.ExchangeGuid, aduser.OrganizationId);
							}
							else
							{
								UMTracer.DebugTrace("Skipping Mbox='{0}', Organization='{1}' because Mbox is already included. ", new object[]
								{
									aduser.ExchangeGuid,
									aduser.OrganizationId
								});
							}
						}
						else
						{
							UMTracer.DebugTrace("Skipping Mbox='{0}', Organization='{1}' because it does not exceed size threshold", new object[]
							{
								aduser.ExchangeGuid,
								aduser.OrganizationId
							});
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0006376C File Offset: 0x0006196C
		protected override HashSet<CultureInfo> InternalGetGrammarCultures()
		{
			UMTracer.DebugTrace("DatacenterUMGrammarTenantCache.InternalGetGrammarCultures", new object[0]);
			return this.GetUmServerLanguages();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00063784 File Offset: 0x00061984
		private HashSet<CultureInfo> GetUmServerLanguages()
		{
			UMTracer.DebugTrace("DatacenterUMGrammarTenantCache.GetUmServerLanguages", new object[0]);
			HashSet<CultureInfo> hashSet = new HashSet<CultureInfo>();
			foreach (UMLanguage umlanguage in UMLanguage.Datacenterlanguages)
			{
				UMTracer.DebugTrace("Adding culture='{0}'", new object[]
				{
					umlanguage.Culture
				});
				hashSet.Add(umlanguage.Culture);
			}
			return hashSet;
		}
	}
}
