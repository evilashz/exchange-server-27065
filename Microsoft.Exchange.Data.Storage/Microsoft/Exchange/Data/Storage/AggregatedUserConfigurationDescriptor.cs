using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C0 RID: 448
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedUserConfigurationDescriptor
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x000769DB File Offset: 0x00074BDB
		public AggregatedUserConfigurationDescriptor(string name, IEnumerable<UserConfigurationDescriptor> sources)
		{
			this.name = name;
			this.sources = sources;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000769F1 File Offset: 0x00074BF1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x000769F9 File Offset: 0x00074BF9
		public IEnumerable<UserConfigurationDescriptor> Sources
		{
			get
			{
				return this.sources;
			}
		}

		// Token: 0x04000CBB RID: 3259
		private readonly string name;

		// Token: 0x04000CBC RID: 3260
		private readonly IEnumerable<UserConfigurationDescriptor> sources;
	}
}
