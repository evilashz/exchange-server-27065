using System;

namespace System.Security.Util
{
	// Token: 0x02000352 RID: 850
	internal struct TokenBasedSetEnumerator
	{
		// Token: 0x06002B53 RID: 11091 RVA: 0x000A150B File Offset: 0x0009F70B
		public bool MoveNext()
		{
			return this._tb != null && this._tb.MoveNext(ref this);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000A1523 File Offset: 0x0009F723
		public void Reset()
		{
			this.Index = -1;
			this.Current = null;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000A1533 File Offset: 0x0009F733
		public TokenBasedSetEnumerator(TokenBasedSet tb)
		{
			this.Index = -1;
			this.Current = null;
			this._tb = tb;
		}

		// Token: 0x0400111D RID: 4381
		public object Current;

		// Token: 0x0400111E RID: 4382
		public int Index;

		// Token: 0x0400111F RID: 4383
		private TokenBasedSet _tb;
	}
}
