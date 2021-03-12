using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200068A RID: 1674
	public class ConsistentHashSet<S, K>
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00038354 File Offset: 0x00036554
		public long ItemSize
		{
			get
			{
				return (long)((4 + IntPtr.Size) * this.slotRing.Count + 4 * this.keys.Length);
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00038378 File Offset: 0x00036578
		public ConsistentHashSet(S[] slots, int replicaCount, int numberOfNeighborVisits)
		{
			if (slots == null)
			{
				throw new ArgumentNullException("slots", "slots cannot be null");
			}
			if (slots.Length == 0)
			{
				throw new ArgumentException("There must be atleast one slot", "slots");
			}
			if (replicaCount <= 0)
			{
				throw new ArgumentException("Replica count cannot be zero or less", "replicaCount");
			}
			if (numberOfNeighborVisits < 1)
			{
				throw new ArgumentException("numberOfNeighborVisits", "numberOfNeighborVisits cannot be less than 1");
			}
			this.numberOfVisits = numberOfNeighborVisits;
			this.slotRing = new SortedDictionary<uint, S>();
			foreach (S value in slots)
			{
				uint num = (uint)(-1 / replicaCount);
				ushort num2 = 0;
				while ((int)num2 < replicaCount)
				{
					uint num3 = MurmurHash.Hash(Encoding.Unicode.GetBytes(value.GetHashCode().ToString() + (num * (uint)num2).ToString()));
					if (!this.slotRing.Keys.Contains(num3))
					{
						this.slotRing.Add(num3, value);
					}
					num2 += 1;
				}
			}
			this.keys = this.slotRing.Keys.ToArray<uint>();
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0003848C File Offset: 0x0003668C
		public S GetNearestNeighborSlot(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null or empty");
			}
			uint num = (uint)(-1 / this.numberOfVisits);
			decimal d = 2147483647m;
			uint key2 = 0U;
			ushort num2 = 0;
			while ((int)num2 < this.numberOfVisits)
			{
				uint num3 = MurmurHash.Hash(Encoding.Unicode.GetBytes(key.GetHashCode().ToString() + (num * (uint)num2).ToString()));
				int num4 = Array.BinarySearch<uint>(this.keys, num3);
				uint num5;
				if (num4 >= 0)
				{
					num5 = this.keys[num4];
				}
				else if (~num4 == this.keys.Count<uint>())
				{
					num5 = this.keys[0];
				}
				else
				{
					num5 = this.keys[~num4];
				}
				decimal num6 = num5 - num3;
				if (~num4 == this.keys.Count<uint>())
				{
					num6 = num5 + (uint.MaxValue - num3);
				}
				if (num6 <= d)
				{
					d = num6;
					key2 = num5;
				}
				num2 += 1;
			}
			return this.slotRing[key2];
		}

		// Token: 0x04001E41 RID: 7745
		private SortedDictionary<uint, S> slotRing;

		// Token: 0x04001E42 RID: 7746
		private uint[] keys;

		// Token: 0x04001E43 RID: 7747
		private readonly int numberOfVisits;
	}
}
