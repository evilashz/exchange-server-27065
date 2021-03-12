using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000657 RID: 1623
	internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
	{
		// Token: 0x06004E23 RID: 20003 RVA: 0x001179E8 File Offset: 0x00115BE8
		public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001179F7 File Offset: 0x00115BF7
		private StoreOperationMetadataProperty GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x00117A0D File Offset: 0x00115C0D
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00117A1A File Offset: 0x00115C1A
		public StoreOperationMetadataProperty Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x00117A22 File Offset: 0x00115C22
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x00117A28 File Offset: 0x00115C28
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			StoreOperationMetadataProperty[] array = new StoreOperationMetadataProperty[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x00117A68 File Offset: 0x00115C68
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002143 RID: 8515
		private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;

		// Token: 0x04002144 RID: 8516
		private bool _fValid;

		// Token: 0x04002145 RID: 8517
		private StoreOperationMetadataProperty _current;
	}
}
