using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001C0 RID: 448
	internal class PartnerHostedUMGrammarTenantCache : UMGrammarTenantCache
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00065C08 File Offset: 0x00063E08
		protected override Dictionary<Guid, OrganizationId> GetTenantsNeedingGrammars(List<ADUser> systemMailboxes)
		{
			UMTracer.DebugTrace("PartnerHostedUMGrammarTenantCache.GetTenantsNeedingGrammars", new object[0]);
			return new Dictionary<Guid, OrganizationId>();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00065C1F File Offset: 0x00063E1F
		protected override HashSet<CultureInfo> InternalGetGrammarCultures()
		{
			UMTracer.DebugTrace("PartnerHostedUMGrammarTenantCache.InternalGetGrammarCultures", new object[0]);
			return new HashSet<CultureInfo>();
		}
	}
}
