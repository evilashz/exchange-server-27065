using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000659 RID: 1625
	internal class StoreAssemblyEnumeration : IEnumerator
	{
		// Token: 0x06004E2E RID: 20014 RVA: 0x00117A7C File Offset: 0x00115C7C
		public StoreAssemblyEnumeration(IEnumSTORE_ASSEMBLY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x00117A8B File Offset: 0x00115C8B
		private STORE_ASSEMBLY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x00117AA1 File Offset: 0x00115CA1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x00117AAE File Offset: 0x00115CAE
		public STORE_ASSEMBLY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x00117AB6 File Offset: 0x00115CB6
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00117ABC File Offset: 0x00115CBC
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY[] array = new STORE_ASSEMBLY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00117AFC File Offset: 0x00115CFC
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002146 RID: 8518
		private IEnumSTORE_ASSEMBLY _enum;

		// Token: 0x04002147 RID: 8519
		private bool _fValid;

		// Token: 0x04002148 RID: 8520
		private STORE_ASSEMBLY _current;
	}
}
