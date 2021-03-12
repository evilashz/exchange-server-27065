using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000661 RID: 1633
	internal class StoreCategoryInstanceEnumeration : IEnumerator
	{
		// Token: 0x06004E5A RID: 20058 RVA: 0x00117CCC File Offset: 0x00115ECC
		public StoreCategoryInstanceEnumeration(IEnumSTORE_CATEGORY_INSTANCE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00117CDB File Offset: 0x00115EDB
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00117CDE File Offset: 0x00115EDE
		private STORE_CATEGORY_INSTANCE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00117CF4 File Offset: 0x00115EF4
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x00117D01 File Offset: 0x00115F01
		public STORE_CATEGORY_INSTANCE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00117D0C File Offset: 0x00115F0C
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_INSTANCE[] array = new STORE_CATEGORY_INSTANCE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00117D4C File Offset: 0x00115F4C
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002152 RID: 8530
		private IEnumSTORE_CATEGORY_INSTANCE _enum;

		// Token: 0x04002153 RID: 8531
		private bool _fValid;

		// Token: 0x04002154 RID: 8532
		private STORE_CATEGORY_INSTANCE _current;
	}
}
