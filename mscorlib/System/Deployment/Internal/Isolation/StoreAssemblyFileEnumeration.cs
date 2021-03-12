using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200065B RID: 1627
	internal class StoreAssemblyFileEnumeration : IEnumerator
	{
		// Token: 0x06004E39 RID: 20025 RVA: 0x00117B10 File Offset: 0x00115D10
		public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00117B1F File Offset: 0x00115D1F
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00117B22 File Offset: 0x00115D22
		private STORE_ASSEMBLY_FILE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x00117B38 File Offset: 0x00115D38
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x00117B45 File Offset: 0x00115D45
		public STORE_ASSEMBLY_FILE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00117B50 File Offset: 0x00115D50
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY_FILE[] array = new STORE_ASSEMBLY_FILE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00117B90 File Offset: 0x00115D90
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002149 RID: 8521
		private IEnumSTORE_ASSEMBLY_FILE _enum;

		// Token: 0x0400214A RID: 8522
		private bool _fValid;

		// Token: 0x0400214B RID: 8523
		private STORE_ASSEMBLY_FILE _current;
	}
}
