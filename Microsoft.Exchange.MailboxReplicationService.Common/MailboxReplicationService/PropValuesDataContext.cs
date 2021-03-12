using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FF RID: 255
	internal class PropValuesDataContext : DataContext
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x00012815 File Offset: 0x00010A15
		public PropValuesDataContext(PropValueData[][] pvdaa)
		{
			this.pvdaa = pvdaa;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00012824 File Offset: 0x00010A24
		public PropValuesDataContext(PropValueData[] pvda)
		{
			this.pvdaa = new PropValueData[][]
			{
				pvda
			};
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001285C File Offset: 0x00010A5C
		public override string ToString()
		{
			string arg;
			if (this.pvdaa == null || this.pvdaa.Length == 0)
			{
				arg = string.Empty;
			}
			else if (this.pvdaa.Length == 1)
			{
				arg = CommonUtils.ConcatEntries<PropValueData>(this.pvdaa[0], null);
			}
			else
			{
				arg = CommonUtils.ConcatEntries<PropValueData[]>(this.pvdaa, (PropValueData[] pvda) => CommonUtils.ConcatEntries<PropValueData>(this.pvdaa[0], null));
			}
			return string.Format("PropValues: {0}", arg);
		}

		// Token: 0x04000565 RID: 1381
		private PropValueData[][] pvdaa;
	}
}
