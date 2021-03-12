using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000309 RID: 777
	[ComVisible(false)]
	internal class IdentityReferenceEnumerator : IEnumerator<IdentityReference>, IDisposable, IEnumerator
	{
		// Token: 0x060027EE RID: 10222 RVA: 0x00092ABC File Offset: 0x00090CBC
		internal IdentityReferenceEnumerator(IdentityReferenceCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._Collection = collection;
			this._Current = -1;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00092AE0 File Offset: 0x00090CE0
		object IEnumerator.Current
		{
			get
			{
				return this._Collection.Identities[this._Current];
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x00092AF8 File Offset: 0x00090CF8
		public IdentityReference Current
		{
			get
			{
				return ((IEnumerator)this).Current as IdentityReference;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00092B05 File Offset: 0x00090D05
		public bool MoveNext()
		{
			this._Current++;
			return this._Current < this._Collection.Count;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00092B28 File Offset: 0x00090D28
		public void Reset()
		{
			this._Current = -1;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00092B31 File Offset: 0x00090D31
		public void Dispose()
		{
		}

		// Token: 0x04000FD6 RID: 4054
		private int _Current;

		// Token: 0x04000FD7 RID: 4055
		private readonly IdentityReferenceCollection _Collection;
	}
}
