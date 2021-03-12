using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E8 RID: 232
	public struct PCL : IEnumerable<byte[]>, IEnumerable
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0002B298 File Offset: 0x00029498
		public PCL(int initialCapacity)
		{
			this.cnset = new List<byte[]>(initialCapacity);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002B2A6 File Offset: 0x000294A6
		public int Count
		{
			get
			{
				if (this.cnset != null)
				{
					return this.cnset.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700025D RID: 605
		public byte[] this[int index]
		{
			get
			{
				return this.cnset[index];
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002B2CB File Offset: 0x000294CB
		public void Add(ExchangeId id)
		{
			this.Add(id.To22ByteArray());
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002B2DC File Offset: 0x000294DC
		public void Add(byte[] id)
		{
			bool flag = false;
			int i = 0;
			while (i < this.Count)
			{
				if (PCL.GuidsEqual(this.cnset[i], id))
				{
					flag = true;
					if (!PCL.CounterGreaterOrEqual(this.cnset[i], id))
					{
						this.ReplaceEntry(i, id);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (!flag)
			{
				this.AddEntry(id);
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002B33C File Offset: 0x0002953C
		public bool TryLoadBinaryLXCN(byte[] buffer)
		{
			if (buffer != null)
			{
				int num = buffer.Length;
				int i = 0;
				while (i < num)
				{
					if (num - i < 2)
					{
						return false;
					}
					byte b = buffer[i++];
					if (b <= 16 || (int)b > num - i)
					{
						return false;
					}
					byte[] array = new byte[(int)b];
					Buffer.BlockCopy(buffer, i, array, 0, (int)b);
					i += (int)b;
					this.Add(array);
				}
			}
			return true;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002B392 File Offset: 0x00029592
		public void LoadBinaryLXCN(byte[] buffer)
		{
			if (!this.TryLoadBinaryLXCN(buffer))
			{
				throw new StoreException((LID)56917U, ErrorCodeValue.InvalidParameter);
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002B3B4 File Offset: 0x000295B4
		public byte[] DumpBinaryLXCN()
		{
			if (this.Count == 0)
			{
				return PCL.emptyByteArray;
			}
			this.cnset.Sort(new Comparison<byte[]>(PCL.CompareGuids));
			int num = 0;
			for (int i = 0; i < this.cnset.Count; i++)
			{
				num += 1 + this.cnset[i].Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < this.cnset.Count; j++)
			{
				array[num2++] = (byte)this.cnset[j].Length;
				Buffer.BlockCopy(this.cnset[j], 0, array, num2, this.cnset[j].Length);
				num2 += this.cnset[j].Length;
			}
			return array;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002B482 File Offset: 0x00029682
		public void LoadBinaryLTID(byte[] buffer)
		{
			if (!this.TryLoadBinaryLTID(buffer))
			{
				throw new StoreException((LID)59512U, ErrorCodeValue.InvalidParameter);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002B4A4 File Offset: 0x000296A4
		public bool TryLoadBinaryLTID(byte[] buffer)
		{
			if (buffer != null)
			{
				int num = buffer.Length;
				if (num % 24 != 0)
				{
					return false;
				}
				int i = 0;
				while (i < num)
				{
					byte[] array = new byte[22];
					Buffer.BlockCopy(buffer, i, array, 0, 22);
					i += 24;
					this.Add(array);
				}
			}
			return true;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002B4E8 File Offset: 0x000296E8
		public byte[] DumpBinaryLTID()
		{
			if (this.Count == 0)
			{
				return PCL.emptyByteArray;
			}
			this.cnset.Sort(new Comparison<byte[]>(PCL.CompareGuids));
			int num = 0;
			for (int i = 0; i < this.cnset.Count; i++)
			{
				if (this.cnset[i].Length == 22 || this.cnset[i].Length == 24)
				{
					num += 24;
				}
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < this.cnset.Count; j++)
			{
				if (this.cnset[j].Length == 22 || this.cnset[j].Length == 24)
				{
					Buffer.BlockCopy(this.cnset[j], 0, array, num2, this.cnset[j].Length);
					num2 += 24;
				}
			}
			return array;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002B5D0 File Offset: 0x000297D0
		public bool IgnoreChange(PCL remotePCL)
		{
			for (int i = 0; i < remotePCL.Count; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < this.Count)
				{
					if (PCL.GuidsEqual(this.cnset[j], remotePCL.cnset[i]))
					{
						flag = true;
						if (!PCL.CounterGreaterOrEqual(this.cnset[j], remotePCL.cnset[i]))
						{
							return false;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002B64C File Offset: 0x0002984C
		public void Merge(PCL remotePCL)
		{
			for (int i = 0; i < remotePCL.Count; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < this.Count)
				{
					if (PCL.GuidsEqual(this.cnset[j], remotePCL.cnset[i]))
					{
						flag = true;
						if (!PCL.CounterGreaterOrEqual(this.cnset[j], remotePCL.cnset[i]))
						{
							this.ReplaceEntry(j, remotePCL.cnset[i]);
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					this.AddEntry(remotePCL.cnset[i]);
				}
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002B6F2 File Offset: 0x000298F2
		public List<byte[]>.Enumerator GetEnumerator()
		{
			if (this.cnset == null)
			{
				return PCL.emptyList.GetEnumerator();
			}
			return this.cnset.GetEnumerator();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002B712 File Offset: 0x00029912
		IEnumerator<byte[]> IEnumerable<byte[]>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002B71F File Offset: 0x0002991F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002B72C File Offset: 0x0002992C
		private static int CompareGuids(byte[] lhs, byte[] rhs)
		{
			for (int i = 0; i < 16; i++)
			{
				if (lhs[i] > rhs[i])
				{
					return 1;
				}
				if (lhs[i] < rhs[i])
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002B75C File Offset: 0x0002995C
		private static bool GuidsEqual(byte[] lhs, byte[] rhs)
		{
			for (int i = 0; i < 16; i++)
			{
				if (rhs[i] != lhs[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002B784 File Offset: 0x00029984
		private static bool CounterGreaterOrEqual(byte[] lhs, byte[] rhs)
		{
			if (lhs.Length != rhs.Length)
			{
				throw new StoreException((LID)51320U, ErrorCodeValue.InvalidParameter);
			}
			for (int i = 16; i < rhs.Length; i++)
			{
				if (lhs[i] < rhs[i])
				{
					return false;
				}
				if (lhs[i] > rhs[i])
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002B7D0 File Offset: 0x000299D0
		private bool AlreadyThere(byte[] cn)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (ValueHelper.ArraysEqual<byte>(this.cnset[i], cn))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002B805 File Offset: 0x00029A05
		private void AddEntry(byte[] cn)
		{
			if (this.cnset == null)
			{
				this.cnset = new List<byte[]>(4);
			}
			this.cnset.Add(cn);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0002B827 File Offset: 0x00029A27
		private void ReplaceEntry(int index, byte[] cn)
		{
			this.cnset[index] = cn;
		}

		// Token: 0x0400053F RID: 1343
		private const int LengthOfGID = 22;

		// Token: 0x04000540 RID: 1344
		private const int LengthOfLTID = 24;

		// Token: 0x04000541 RID: 1345
		private static readonly List<byte[]> emptyList = new List<byte[]>(0);

		// Token: 0x04000542 RID: 1346
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000543 RID: 1347
		private List<byte[]> cnset;
	}
}
