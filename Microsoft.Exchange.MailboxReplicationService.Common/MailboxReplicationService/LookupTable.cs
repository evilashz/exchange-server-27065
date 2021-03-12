using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000145 RID: 325
	internal class LookupTable<TData> where TData : class
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0001548E File Offset: 0x0001368E
		public LookupTable()
		{
			this.indexes = new List<LookupTable<TData>.IIndex>();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000154A1 File Offset: 0x000136A1
		public void RegisterIndex(LookupTable<TData>.IIndex index)
		{
			this.indexes.Add(index);
			index.SetOwner(this);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000154B8 File Offset: 0x000136B8
		public void Clear()
		{
			foreach (LookupTable<TData>.IIndex index in this.indexes)
			{
				index.Clear();
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001550C File Offset: 0x0001370C
		public void LookupUnresolvedKeys()
		{
			foreach (LookupTable<TData>.IIndex index in this.indexes)
			{
				index.LookupUnresolvedKeys();
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00015560 File Offset: 0x00013760
		public void InsertObject(TData data)
		{
			foreach (LookupTable<TData>.IIndex index in this.indexes)
			{
				index.InsertObject(data);
			}
		}

		// Token: 0x04000656 RID: 1622
		private List<LookupTable<TData>.IIndex> indexes;

		// Token: 0x02000146 RID: 326
		internal interface IIndex
		{
			// Token: 0x06000AE1 RID: 2785
			void SetOwner(LookupTable<TData> owner);

			// Token: 0x06000AE2 RID: 2786
			void InsertObject(TData data);

			// Token: 0x06000AE3 RID: 2787
			void LookupUnresolvedKeys();

			// Token: 0x06000AE4 RID: 2788
			void Clear();
		}
	}
}
