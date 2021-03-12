using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABObjectSchema : ObjectSchema
	{
		// Token: 0x04000005 RID: 5
		public static readonly ABPropertyDefinition Id = new ABPropertyDefinition("Id", typeof(ABObjectId), PropertyDefinitionFlags.ReadOnly, null);

		// Token: 0x04000006 RID: 6
		public static readonly ABPropertyDefinition CanEmail = new ABPropertyDefinition("CanEmail", typeof(bool), PropertyDefinitionFlags.ReadOnly, false);

		// Token: 0x04000007 RID: 7
		public static readonly ABPropertyDefinition LegacyExchangeDN = new ABPropertyDefinition("LegacyExchangeDN", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000008 RID: 8
		public static readonly ABPropertyDefinition DisplayName = new ABPropertyDefinition("DisplayName", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000009 RID: 9
		public static readonly ABPropertyDefinition Alias = new ABPropertyDefinition("Alias", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400000A RID: 10
		public static readonly ABPropertyDefinition EmailAddress = new ABPropertyDefinition("EmailAddress", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);
	}
}
