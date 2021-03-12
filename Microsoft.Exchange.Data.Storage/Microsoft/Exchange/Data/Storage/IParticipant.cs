using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008F3 RID: 2291
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IParticipant : IEquatable<IParticipant>
	{
		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x060055DF RID: 21983
		IEqualityComparer<IParticipant> EmailAddressEqualityComparer { get; }

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x060055E0 RID: 21984
		string DisplayName { get; }

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060055E1 RID: 21985
		string OriginalDisplayName { get; }

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060055E2 RID: 21986
		string EmailAddress { get; }

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060055E3 RID: 21987
		string SmtpEmailAddress { get; }

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060055E4 RID: 21988
		string RoutingType { get; }

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060055E5 RID: 21989
		ParticipantOrigin Origin { get; }

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060055E6 RID: 21990
		string SipUri { get; }

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060055E7 RID: 21991
		// (set) Token: 0x060055E8 RID: 21992
		bool Submitted { get; set; }

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060055E9 RID: 21993
		ICollection<PropertyDefinition> LoadedProperties { get; }

		// Token: 0x060055EA RID: 21994
		T GetValueOrDefault<T>(PropertyDefinition propertyDefinition);
	}
}
