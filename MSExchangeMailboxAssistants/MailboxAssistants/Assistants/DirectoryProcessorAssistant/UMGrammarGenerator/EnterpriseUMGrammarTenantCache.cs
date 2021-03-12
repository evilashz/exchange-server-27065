using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001B9 RID: 441
	internal class EnterpriseUMGrammarTenantCache : UMGrammarTenantCache
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x00063880 File Offset: 0x00061A80
		protected override Dictionary<Guid, OrganizationId> GetTenantsNeedingGrammars(List<ADUser> systemMailboxes)
		{
			UMTracer.DebugTrace("EnterpriseUMGrammarTenantCache.GetTenantsNeedingGrammars", new object[0]);
			ExAssert.RetailAssert(systemMailboxes.Count <= 1, "For an Enterprise, there should be no more than one arbitration mailbox for speech grammar generation. Current count={0}", new object[]
			{
				systemMailboxes.Count
			});
			Dictionary<Guid, OrganizationId> dictionary = new Dictionary<Guid, OrganizationId>();
			foreach (ADUser aduser in systemMailboxes)
			{
				UMTracer.DebugTrace("Adding Mbox='{0}', Organization='{1}'", new object[]
				{
					aduser.ExchangeGuid,
					aduser.OrganizationId
				});
				dictionary.Add(aduser.ExchangeGuid, aduser.OrganizationId);
			}
			return dictionary;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0006394C File Offset: 0x00061B4C
		protected override HashSet<CultureInfo> InternalGetGrammarCultures()
		{
			UMTracer.DebugTrace("EnterpriseUMGrammarTenantCache.InternalGetGrammarCultures", new object[0]);
			return this.GetInstalledUMLanguages();
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00063964 File Offset: 0x00061B64
		private HashSet<CultureInfo> GetInstalledUMLanguages()
		{
			UMTracer.DebugTrace("EnterpriseUMGrammarTenantCache.GetInstalledUMLanguages", new object[0]);
			HashSet<CultureInfo> hashSet = new HashSet<CultureInfo>();
			foreach (UMLanguage umlanguage in Utils.ComputeUnionOfUmServerLanguages())
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
