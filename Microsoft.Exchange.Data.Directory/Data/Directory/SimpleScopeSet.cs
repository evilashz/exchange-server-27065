using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	internal class SimpleScopeSet<TScope>
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0004F747 File Offset: 0x0004D947
		public SimpleScopeSet(TScope recipientReadScope, TScope recipientWriteScope, TScope configReadScope, TScope configWriteScope)
		{
			this.recipientReadScope = recipientReadScope;
			this.recipientWriteScope = recipientWriteScope;
			this.configReadScope = configReadScope;
			this.configWriteScope = configWriteScope;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0004F76C File Offset: 0x0004D96C
		public TScope RecipientReadScope
		{
			get
			{
				return this.recipientReadScope;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0004F774 File Offset: 0x0004D974
		public TScope RecipientWriteScope
		{
			get
			{
				return this.recipientWriteScope;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0004F77C File Offset: 0x0004D97C
		public TScope ConfigReadScope
		{
			get
			{
				return this.configReadScope;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0004F784 File Offset: 0x0004D984
		public TScope ConfigWriteScope
		{
			get
			{
				return this.configWriteScope;
			}
		}

		// Token: 0x170002BB RID: 699
		public TScope this[ScopeLocation scopeLocation]
		{
			get
			{
				switch (scopeLocation)
				{
				case ScopeLocation.RecipientRead:
					return this.recipientReadScope;
				case ScopeLocation.RecipientWrite:
					return this.recipientWriteScope;
				case ScopeLocation.ConfigRead:
					return this.configReadScope;
				case ScopeLocation.ConfigWrite:
					return this.configWriteScope;
				default:
					throw new ArgumentException("scopeLocation");
				}
			}
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004F7DC File Offset: 0x0004D9DC
		public bool Equals(SimpleScopeSet<TScope> obj)
		{
			if (obj == null)
			{
				return false;
			}
			bool flag;
			if (this.ConfigReadScope == null)
			{
				flag = (null == obj.ConfigReadScope);
			}
			else
			{
				TScope tscope = this.ConfigReadScope;
				flag = tscope.Equals(obj.ConfigReadScope);
			}
			if (this.ConfigWriteScope == null)
			{
				flag = (flag && null == obj.ConfigWriteScope);
			}
			else
			{
				bool flag2;
				if (flag)
				{
					TScope tscope2 = this.ConfigWriteScope;
					flag2 = tscope2.Equals(obj.ConfigWriteScope);
				}
				else
				{
					flag2 = false;
				}
				flag = flag2;
			}
			if (this.RecipientReadScope == null)
			{
				flag = (flag && null == obj.RecipientReadScope);
			}
			else
			{
				bool flag3;
				if (flag)
				{
					TScope tscope3 = this.RecipientReadScope;
					flag3 = tscope3.Equals(obj.RecipientReadScope);
				}
				else
				{
					flag3 = false;
				}
				flag = flag3;
			}
			if (this.RecipientWriteScope == null)
			{
				flag = (flag && null == obj.RecipientWriteScope);
			}
			else
			{
				bool flag4;
				if (flag)
				{
					TScope tscope4 = this.RecipientWriteScope;
					flag4 = tscope4.Equals(obj.RecipientWriteScope);
				}
				else
				{
					flag4 = false;
				}
				flag = flag4;
			}
			return flag;
		}

		// Token: 0x0400095F RID: 2399
		private TScope recipientReadScope;

		// Token: 0x04000960 RID: 2400
		private TScope recipientWriteScope;

		// Token: 0x04000961 RID: 2401
		private TScope configReadScope;

		// Token: 0x04000962 RID: 2402
		private TScope configWriteScope;
	}
}
