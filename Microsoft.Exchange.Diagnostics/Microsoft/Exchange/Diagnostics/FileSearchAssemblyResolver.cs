using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001DA RID: 474
	public sealed class FileSearchAssemblyResolver : AssemblyResolver
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00038226 File Offset: 0x00036426
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0003822E File Offset: 0x0003642E
		public bool Recursive { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00038237 File Offset: 0x00036437
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0003823F File Offset: 0x0003643F
		public string[] SearchPaths { get; set; }

		// Token: 0x06000D5C RID: 3420 RVA: 0x00038270 File Offset: 0x00036470
		protected override IEnumerable<string> GetCandidateAssemblyPaths(AssemblyName nameToResolve)
		{
			string fileName = AssemblyResolver.GetAssemblyFileNameFromFullName(nameToResolve);
			return base.FilterDirectoryPaths(this.SearchPaths).SelectMany((string path) => this.FindAssembly(path, fileName, this.Recursive));
		}
	}
}
