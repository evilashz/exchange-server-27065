using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065D RID: 1629
	internal class StoreCategoryEnumeration : IEnumerator
	{
		// Token: 0x06004E44 RID: 20036 RVA: 0x00117BA4 File Offset: 0x00115DA4
		public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00117BB3 File Offset: 0x00115DB3
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00117BB6 File Offset: 0x00115DB6
		private STORE_CATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x00117BCC File Offset: 0x00115DCC
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x00117BD9 File Offset: 0x00115DD9
		public STORE_CATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00117BE4 File Offset: 0x00115DE4
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY[] array = new STORE_CATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00117C24 File Offset: 0x00115E24
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400214C RID: 8524
		private IEnumSTORE_CATEGORY _enum;

		// Token: 0x0400214D RID: 8525
		private bool _fValid;

		// Token: 0x0400214E RID: 8526
		private STORE_CATEGORY _current;
	}
}
