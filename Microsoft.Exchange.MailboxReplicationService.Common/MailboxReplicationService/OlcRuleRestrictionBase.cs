using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A9 RID: 169
	[KnownType(typeof(OlcRuleFolderRestriction))]
	[KnownType(typeof(OlcRuleMarkedAsReadRestriction))]
	[KnownType(typeof(OlcRuleOrRestriction))]
	[KnownType(typeof(OlcRuleNotRestriction))]
	[KnownType(typeof(OlcRuleCategoryRestriction))]
	[KnownType(typeof(OlcRuleHierarchyRestriction))]
	[KnownType(typeof(OlcRuleHasAttachmentRestriction))]
	[KnownType(typeof(OlcMessageStringPropertyComparisionRestriction))]
	[KnownType(typeof(OlcRuleAndRestriction))]
	[DataContract]
	internal abstract class OlcRuleRestrictionBase
	{
	}
}
