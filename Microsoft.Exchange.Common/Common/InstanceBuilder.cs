using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200006D RID: 109
	internal class InstanceBuilder<InterfaceOfTypeToBuild, TypeToBuild> : IBuilder<InterfaceOfTypeToBuild> where TypeToBuild : InterfaceOfTypeToBuild, new()
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000A208 File Offset: 0x00008408
		InterfaceOfTypeToBuild IBuilder<!0>.Build()
		{
			return (InterfaceOfTypeToBuild)((object)((default(TypeToBuild) == null) ? Activator.CreateInstance<TypeToBuild>() : default(TypeToBuild)));
		}
	}
}
