using System;
using System.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200028D RID: 653
	internal static class ContentChangesFetcherUtils
	{
		// Token: 0x06002001 RID: 8193 RVA: 0x00043F58 File Offset: 0x00042158
		[Conditional("DEBUG")]
		internal static void ValidateEnumeration(IFolder sourceFolder, EnumerateContentChangesFlags flags, IContentChangesFetcher contentChangesFetcher, bool hasMoreChangesPrevPage, bool isPagedEnumeration)
		{
			flags.HasFlag(EnumerateContentChangesFlags.FirstPage);
			flags.HasFlag(EnumerateContentChangesFlags.Catchup);
			if (!isPagedEnumeration || flags.HasFlag(EnumerateContentChangesFlags.FirstPage) || flags.HasFlag(EnumerateContentChangesFlags.Catchup))
			{
				return;
			}
		}

		// Token: 0x04000CEF RID: 3311
		internal static readonly Restriction ExcludeV40RulesRestriction = Restriction.Not(Restriction.And(new Restriction[]
		{
			Restriction.Content(PropTag.MessageClass, "IPM.Rule.Message", ContentFlags.Prefix),
			Restriction.EQ(PropTag.RuleMsgVersion, 1)
		}));

		// Token: 0x04000CF0 RID: 3312
		internal static readonly Restriction ExcludeAllRulesRestriction = Restriction.Not(Restriction.Or(new Restriction[]
		{
			Restriction.Content(PropTag.MessageClass, "IPM.Rule.Message", ContentFlags.Prefix),
			Restriction.Content(PropTag.MessageClass, "IPM.Rule.Version2.Message", ContentFlags.Prefix),
			Restriction.Content(PropTag.MessageClass, "IPM.ExtendedRule.Message", ContentFlags.Prefix)
		}));
	}
}
