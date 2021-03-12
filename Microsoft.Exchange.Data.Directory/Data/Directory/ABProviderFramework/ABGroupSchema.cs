using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ABGroupSchema : ABObjectSchema
	{
		// Token: 0x04000025 RID: 37
		public static readonly ABPropertyDefinition OwnerId = new ABPropertyDefinition("OwnerId", typeof(ABObjectId), PropertyDefinitionFlags.ReadOnly, null);

		// Token: 0x04000026 RID: 38
		public static readonly ABPropertyDefinition HiddenMembership = new ABPropertyDefinition("HiddenMembership", typeof(bool?), PropertyDefinitionFlags.ReadOnly, null);

		// Token: 0x04000027 RID: 39
		public static readonly ABPropertyDefinition MembersCount = new ABPropertyDefinition("MembersCount", typeof(int?), PropertyDefinitionFlags.ReadOnly, null);
	}
}
