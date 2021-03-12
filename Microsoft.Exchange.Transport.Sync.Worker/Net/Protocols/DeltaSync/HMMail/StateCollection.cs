using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x02000095 RID: 149
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class StateCollection : ArrayList
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x000197F1 File Offset: 0x000179F1
		public byte Add(byte obj)
		{
			base.Add(obj);
			return obj;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019801 File Offset: 0x00017A01
		public byte Add()
		{
			return this.Add(0);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001980A File Offset: 0x00017A0A
		public void Insert(int index, byte obj)
		{
			base.Insert(index, obj);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019819 File Offset: 0x00017A19
		public void Remove(byte obj)
		{
			base.Remove(obj);
		}

		// Token: 0x17000212 RID: 530
		public byte this[int index]
		{
			get
			{
				return (byte)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
