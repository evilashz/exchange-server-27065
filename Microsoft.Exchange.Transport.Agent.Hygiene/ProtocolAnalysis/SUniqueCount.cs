using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Agent.Hygiene;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal class SUniqueCount
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000C22C File Offset: 0x0000A42C
		public SUniqueCount()
		{
			this.tbl = new ushort[SUniqueCount.nTBL];
			this.Reset();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C24C File Offset: 0x0000A44C
		public void Merge(SUniqueCount source)
		{
			for (int i = 0; i < source.entries; i++)
			{
				this.AddHash(source.tbl[i]);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000C278 File Offset: 0x0000A478
		public void Reset()
		{
			Array.Clear(this.tbl, 0, this.tbl.Length);
			this.entries = 0;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000C298 File Offset: 0x0000A498
		public void AddItem(string itemName)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(itemName);
			ushort ushort16HashCode = PrivateHashAlgorithm1.GetUShort16HashCode(bytes);
			this.AddHash(ushort16HashCode);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		public int Count()
		{
			return this.entries;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		private void AddHash(ushort h)
		{
			bool flag = false;
			int num = -1;
			if (this.entries == this.tbl.Length)
			{
				flag = true;
			}
			else if (this.entries == 0)
			{
				num = 0;
			}
			else
			{
				int num2 = 0;
				int num3 = this.entries - 1;
				while (!flag && num == -1)
				{
					if (h < this.tbl[num2])
					{
						num = num2;
					}
					else if (h > this.tbl[num3])
					{
						num = num3 + 1;
					}
					else if (h == this.tbl[num2] || h == this.tbl[num3])
					{
						flag = true;
					}
					else
					{
						int num4 = (num2 + num3) / 2;
						if (h > this.tbl[num4])
						{
							if (num4 != num2)
							{
								num2 = num4;
							}
							else
							{
								num = num2 + 1;
							}
						}
						else if (h < this.tbl[num4])
						{
							if (num4 != num3)
							{
								num3 = num4;
							}
							else
							{
								num = num3;
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			if (!flag && num == -1)
			{
				throw new LocalizedException(AgentStrings.FailedToFindInsertionPoint);
			}
			if (!flag && this.entries < this.tbl.Length)
			{
				for (int i = this.entries - 1; i >= num; i--)
				{
					this.tbl[i + 1] = this.tbl[i];
				}
				this.tbl[num] = h;
				this.entries++;
			}
		}

		// Token: 0x04000154 RID: 340
		private static int nTBL = 200;

		// Token: 0x04000155 RID: 341
		private ushort[] tbl;

		// Token: 0x04000156 RID: 342
		private int entries;
	}
}
