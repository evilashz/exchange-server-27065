using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Rus
{
	// Token: 0x020001CF RID: 463
	internal class RusConfigSchema : ADObjectSchema
	{
		// Token: 0x0400095A RID: 2394
		public static readonly HygienePropertyDefinition UniversalManifestVersion = new HygienePropertyDefinition("UniversalManifestVersion", typeof(string));

		// Token: 0x0400095B RID: 2395
		public static readonly HygienePropertyDefinition UniversalManifestVersionV2 = new HygienePropertyDefinition("UniversalManifestVersionV2", typeof(string));
	}
}
