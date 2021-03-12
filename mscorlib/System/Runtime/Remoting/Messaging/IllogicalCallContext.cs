using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000860 RID: 2144
	internal class IllogicalCallContext
	{
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00140724 File Offset: 0x0013E924
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x0014073F File Offset: 0x0013E93F
		// (set) Token: 0x06005B9E RID: 23454 RVA: 0x00140747 File Offset: 0x0013E947
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x00140750 File Offset: 0x0013E950
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0014076A File Offset: 0x0013E96A
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x00140778 File Offset: 0x0013E978
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x00140786 File Offset: 0x0013E986
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x00140798 File Offset: 0x0013E998
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x0400291C RID: 10524
		private Hashtable m_Datastore;

		// Token: 0x0400291D RID: 10525
		private object m_HostContext;

		// Token: 0x02000C47 RID: 3143
		internal struct Reader
		{
			// Token: 0x06006F96 RID: 28566 RVA: 0x0017FFBC File Offset: 0x0017E1BC
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x1700133B RID: 4923
			// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0017FFC5 File Offset: 0x0017E1C5
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x06006F98 RID: 28568 RVA: 0x0017FFD0 File Offset: 0x0017E1D0
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x1700133C RID: 4924
			// (get) Token: 0x06006F99 RID: 28569 RVA: 0x0017FFE8 File Offset: 0x0017E1E8
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x04003727 RID: 14119
			private IllogicalCallContext m_ctx;
		}
	}
}
