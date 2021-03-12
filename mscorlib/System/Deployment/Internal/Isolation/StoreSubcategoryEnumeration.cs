using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065F RID: 1631
	internal class StoreSubcategoryEnumeration : IEnumerator
	{
		// Token: 0x06004E4F RID: 20047 RVA: 0x00117C38 File Offset: 0x00115E38
		public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00117C47 File Offset: 0x00115E47
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00117C4A File Offset: 0x00115E4A
		private STORE_CATEGORY_SUBCATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x00117C60 File Offset: 0x00115E60
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00117C6D File Offset: 0x00115E6D
		public STORE_CATEGORY_SUBCATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00117C78 File Offset: 0x00115E78
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_SUBCATEGORY[] array = new STORE_CATEGORY_SUBCATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00117CB8 File Offset: 0x00115EB8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400214F RID: 8527
		private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;

		// Token: 0x04002150 RID: 8528
		private bool _fValid;

		// Token: 0x04002151 RID: 8529
		private STORE_CATEGORY_SUBCATEGORY _current;
	}
}
