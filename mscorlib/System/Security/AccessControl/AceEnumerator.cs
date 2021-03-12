using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x0200020A RID: 522
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x0006AC0F File Offset: 0x00068E0F
		internal AceEnumerator(GenericAcl collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._acl = collection;
			this.Reset();
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0006AC32 File Offset: 0x00068E32
		object IEnumerator.Current
		{
			get
			{
				if (this._current == -1 || this._current >= this._acl.Count)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
				}
				return this._acl[this._current];
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0006AC71 File Offset: 0x00068E71
		public GenericAce Current
		{
			get
			{
				return ((IEnumerator)this).Current as GenericAce;
			}
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x0006AC7E File Offset: 0x00068E7E
		public bool MoveNext()
		{
			this._current++;
			return this._current < this._acl.Count;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x0006ACA1 File Offset: 0x00068EA1
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x04000B03 RID: 2819
		private int _current;

		// Token: 0x04000B04 RID: 2820
		private readonly GenericAcl _acl;
	}
}
