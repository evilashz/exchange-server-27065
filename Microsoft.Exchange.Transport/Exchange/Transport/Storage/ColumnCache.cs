using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal abstract class ColumnCache
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001723D File Offset: 0x0001543D
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001724A File Offset: 0x0001544A
		public bool Dirty
		{
			get
			{
				return (this.flags & ColumnCache.States.Dirty) == ColumnCache.States.Dirty;
			}
			set
			{
				if (value)
				{
					this.flags |= ColumnCache.States.Dirty;
					return;
				}
				this.flags &= ~ColumnCache.States.Dirty;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001726D File Offset: 0x0001546D
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001727D File Offset: 0x0001547D
		public bool HasValue
		{
			get
			{
				return (this.flags & ColumnCache.States.Null) != ColumnCache.States.Null;
			}
			set
			{
				if (value)
				{
					this.flags &= ~ColumnCache.States.Null;
				}
				else
				{
					this.flags |= ColumnCache.States.Null;
					this.SetValueToDefault();
				}
				this.Dirty = true;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000172AE File Offset: 0x000154AE
		public virtual void CloneFrom(ColumnCache from)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (this == from)
			{
				throw new ArgumentException(Strings.CircularClone, "from");
			}
			this.flags = from.flags;
		}

		// Token: 0x0600059B RID: 1435
		protected abstract void SetValueToDefault();

		// Token: 0x040002D8 RID: 728
		private ColumnCache.States flags = ColumnCache.States.Null;

		// Token: 0x020000A1 RID: 161
		[Flags]
		protected enum States
		{
			// Token: 0x040002DA RID: 730
			Null = 1,
			// Token: 0x040002DB RID: 731
			Dirty = 2
		}
	}
}
