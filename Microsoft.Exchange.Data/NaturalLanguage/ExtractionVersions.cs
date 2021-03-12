using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ExtractionVersions
	{
		// Token: 0x04000145 RID: 325
		public static readonly Version Office15 = new Version(15, 0, 0, 0);

		// Token: 0x04000146 RID: 326
		public static readonly Version CurrentVersion = ExtractionVersions.Office15;
	}
}
