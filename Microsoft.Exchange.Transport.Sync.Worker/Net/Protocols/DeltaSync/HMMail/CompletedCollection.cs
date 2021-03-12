using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000094 RID: 148
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class CompletedCollection : ArrayList
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00019796 File Offset: 0x00017996
		public bitType Add(bitType obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000197A6 File Offset: 0x000179A6
		public bitType Add()
		{
			return this.Add(bitType.zero);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000197AF File Offset: 0x000179AF
		public void Insert(int index, bitType obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000197BE File Offset: 0x000179BE
		public void Remove(bitType obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000211 RID: 529
		public bitType this[int index]
		{
			get
			{
				return (bitType)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
