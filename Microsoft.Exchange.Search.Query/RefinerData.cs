using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x0200001A RID: 26
	internal class RefinerData
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000071A3 File Offset: 0x000053A3
		internal RefinerData(PropertyDefinition property, IReadOnlyCollection<RefinerDataEntry> entries)
		{
			InstantSearch.ThrowOnNullArgument(property, "property");
			InstantSearch.ThrowOnNullArgument(entries, "entries");
			this.Property = property;
			this.Entries = entries;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000071CF File Offset: 0x000053CF
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000071D7 File Offset: 0x000053D7
		public PropertyDefinition Property { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000071E0 File Offset: 0x000053E0
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000071E8 File Offset: 0x000053E8
		public IReadOnlyCollection<RefinerDataEntry> Entries { get; private set; }

		// Token: 0x0600014B RID: 331 RVA: 0x000071F1 File Offset: 0x000053F1
		public override string ToString()
		{
			return this.Property.Name + ":" + this.Entries.Count;
		}
	}
}
