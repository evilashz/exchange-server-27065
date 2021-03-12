using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000504 RID: 1284
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscFolderDisplayNameGenerator : IEnumerable<string>, IEnumerable
	{
		// Token: 0x0600379E RID: 14238 RVA: 0x000E0057 File Offset: 0x000DE257
		public OscFolderDisplayNameGenerator(Guid provider, int count)
		{
			if (count < 1)
			{
				throw new ArgumentOutOfRangeException("count", count, "At least 1 name must be generated.");
			}
			this.defaultFolderDisplayName = OscProviderRegistry.GetDefaultFolderDisplayName(provider);
			this.namesToGenerate = count;
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000E017C File Offset: 0x000DE37C
		public IEnumerator<string> GetEnumerator()
		{
			yield return this.defaultFolderDisplayName;
			for (int suffix = 1; suffix < this.namesToGenerate; suffix++)
			{
				yield return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					this.defaultFolderDisplayName,
					suffix
				});
			}
			yield break;
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000E0198 File Offset: 0x000DE398
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001D87 RID: 7559
		private readonly int namesToGenerate;

		// Token: 0x04001D88 RID: 7560
		private readonly string defaultFolderDisplayName;
	}
}
