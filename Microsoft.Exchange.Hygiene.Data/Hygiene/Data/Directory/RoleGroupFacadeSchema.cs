using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000FC RID: 252
	internal class RoleGroupFacadeSchema : ADObjectSchema
	{
		// Token: 0x0400052A RID: 1322
		public static readonly HygienePropertyDefinition Members = new HygienePropertyDefinition("Members", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x0400052B RID: 1323
		public static readonly HygienePropertyDefinition BypassSecurityGroupManagerCheck = new HygienePropertyDefinition("BypassSecurityGroupManagerCheck", typeof(bool), true, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
