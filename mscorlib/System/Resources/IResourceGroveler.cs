using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000360 RID: 864
	internal interface IResourceGroveler
	{
		// Token: 0x06002BCA RID: 11210
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06002BCB RID: 11211
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
