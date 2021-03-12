using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000493 RID: 1171
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AutomaticLinkCriteria
	{
		// Token: 0x060033EB RID: 13291 RVA: 0x000D3154 File Offset: 0x000D1354
		internal static bool CanMergeGALLinkState(ContactInfoForLinking contactBeingSaved, ContactInfoForLinking otherContact)
		{
			return contactBeingSaved.GALLinkState == GALLinkState.NotLinked || otherContact.GALLinkState == GALLinkState.NotLinked || (contactBeingSaved.GALLinkState == GALLinkState.NotAllowed && otherContact.GALLinkState == GALLinkState.NotAllowed) || (contactBeingSaved.GALLinkState == GALLinkState.Linked && otherContact.GALLinkState == GALLinkState.Linked && contactBeingSaved.GALLinkID == otherContact.GALLinkID);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000D31DC File Offset: 0x000D13DC
		internal static ContactLinkingOperation CanLink(ContactInfoForLinking contact1, ContactInfoForLinking contact2)
		{
			Util.ThrowOnNullArgument(contact1, "contact1");
			Util.ThrowOnNullArgument(contact2, "contact2");
			if (!AutomaticLinkCriteria.CanMergeGALLinkState(contact1, contact2))
			{
				return ContactLinkingOperation.AutoLinkSkippedConflictingGALLinkState;
			}
			if (AutomaticLinkCriteria.IsPresentInLinkRejectHistory(contact1, contact2) || AutomaticLinkCriteria.IsPresentInLinkRejectHistory(contact2, contact1))
			{
				return ContactLinkingOperation.AutoLinkSkippedInLinkRejectHistory;
			}
			return AutomaticLinkRegularContactComparer.Instance.Match(contact1, contact2);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000D322B File Offset: 0x000D142B
		private static bool IsPresentInLinkRejectHistory(ContactInfoForLinking contact1, ContactInfoForLinking contact2)
		{
			return contact2.LinkRejectHistory != null && contact1.PersonId != null && contact2.LinkRejectHistory.Contains(contact1.PersonId);
		}

		// Token: 0x04001BF1 RID: 7153
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;
	}
}
