using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200006E RID: 110
	internal class SingletonBuilder<InterfaceOfTypeToBuild, TypeToBuild> : IBuilder<!0> where TypeToBuild : InterfaceOfTypeToBuild, new()
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000A247 File Offset: 0x00008447
		InterfaceOfTypeToBuild IBuilder<!0>.Build()
		{
			return this.instance;
		}

		// Token: 0x040001AF RID: 431
		private InterfaceOfTypeToBuild instance = (InterfaceOfTypeToBuild)((object)((default(TypeToBuild) == null) ? Activator.CreateInstance<TypeToBuild>() : default(TypeToBuild)));
	}
}
