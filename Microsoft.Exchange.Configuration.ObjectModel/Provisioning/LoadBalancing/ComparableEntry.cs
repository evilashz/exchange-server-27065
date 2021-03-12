using System;

namespace Microsoft.Exchange.Provisioning.LoadBalancing
{
	// Token: 0x02000206 RID: 518
	internal class ComparableEntry<T> : IComparable
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x00039009 File Offset: 0x00037209
		public ComparableEntry(T entry)
		{
			this.entry = entry;
			this.count = 0;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0003901F File Offset: 0x0003721F
		public T Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00039027 File Offset: 0x00037227
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0003902F File Offset: 0x0003722F
		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00039038 File Offset: 0x00037238
		public int CompareTo(object obj)
		{
			if (!base.GetType().IsInstanceOfType(obj))
			{
				throw new ArgumentException("obj is not of the required type");
			}
			ComparableEntry<T> comparableEntry = (ComparableEntry<T>)obj;
			return this.count.CompareTo(comparableEntry.Count);
		}

		// Token: 0x04000451 RID: 1105
		private T entry;

		// Token: 0x04000452 RID: 1106
		private int count;
	}
}
