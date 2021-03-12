using System;
using System.Collections;

namespace System.Security
{
	// Token: 0x020001DB RID: 475
	internal class PermissionSetEnumerator : IEnumerator
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0006242C File Offset: 0x0006062C
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00062439 File Offset: 0x00060639
		public bool MoveNext()
		{
			return this.enm.MoveNext();
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00062446 File Offset: 0x00060646
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00062453 File Offset: 0x00060653
		internal PermissionSetEnumerator(PermissionSet permSet)
		{
			this.enm = new PermissionSetEnumeratorInternal(permSet);
		}

		// Token: 0x04000A08 RID: 2568
		private PermissionSetEnumeratorInternal enm;
	}
}
