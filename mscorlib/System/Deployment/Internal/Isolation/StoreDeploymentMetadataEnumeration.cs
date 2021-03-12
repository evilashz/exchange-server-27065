using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000655 RID: 1621
	internal class StoreDeploymentMetadataEnumeration : IEnumerator
	{
		// Token: 0x06004E18 RID: 19992 RVA: 0x0011795D File Offset: 0x00115B5D
		public StoreDeploymentMetadataEnumeration(IEnumSTORE_DEPLOYMENT_METADATA pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0011796C File Offset: 0x00115B6C
		private IDefinitionAppId GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x00117982 File Offset: 0x00115B82
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x0011798A File Offset: 0x00115B8A
		public IDefinitionAppId Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00117992 File Offset: 0x00115B92
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00117998 File Offset: 0x00115B98
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			IDefinitionAppId[] array = new IDefinitionAppId[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = (num == 1U);
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x001179D4 File Offset: 0x00115BD4
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002140 RID: 8512
		private IEnumSTORE_DEPLOYMENT_METADATA _enum;

		// Token: 0x04002141 RID: 8513
		private bool _fValid;

		// Token: 0x04002142 RID: 8514
		private IDefinitionAppId _current;
	}
}
