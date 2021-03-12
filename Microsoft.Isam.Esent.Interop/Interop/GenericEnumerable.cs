using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000228 RID: 552
	internal class GenericEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x00015296 File Offset: 0x00013496
		public GenericEnumerable(GenericEnumerable<T>.CreateEnumerator enumeratorCreator)
		{
			this.enumeratorCreator = enumeratorCreator;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000152A5 File Offset: 0x000134A5
		public IEnumerator<T> GetEnumerator()
		{
			return this.enumeratorCreator();
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x000152B2 File Offset: 0x000134B2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000340 RID: 832
		private readonly GenericEnumerable<T>.CreateEnumerator enumeratorCreator;

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06000A79 RID: 2681
		public delegate IEnumerator<T> CreateEnumerator();
	}
}
