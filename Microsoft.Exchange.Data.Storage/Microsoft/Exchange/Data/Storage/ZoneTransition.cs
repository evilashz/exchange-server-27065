using System;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDF RID: 3807
	[Serializable]
	public class ZoneTransition
	{
		// Token: 0x170022E6 RID: 8934
		// (get) Token: 0x06008376 RID: 33654 RVA: 0x0023B829 File Offset: 0x00239A29
		// (set) Token: 0x06008377 RID: 33655 RVA: 0x0023B831 File Offset: 0x00239A31
		public int Bias
		{
			get
			{
				return this.bias;
			}
			set
			{
				this.bias = value;
			}
		}

		// Token: 0x170022E7 RID: 8935
		// (get) Token: 0x06008378 RID: 33656 RVA: 0x0023B83A File Offset: 0x00239A3A
		// (set) Token: 0x06008379 RID: 33657 RVA: 0x0023B842 File Offset: 0x00239A42
		public ChangeDate ChangeDate
		{
			get
			{
				return this.changeDate;
			}
			set
			{
				this.changeDate = value;
			}
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x0023B84B File Offset: 0x00239A4B
		public ZoneTransition()
		{
		}

		// Token: 0x0600837B RID: 33659 RVA: 0x0023B853 File Offset: 0x00239A53
		internal ZoneTransition(int bias, NativeMethods.SystemTime systemTime)
		{
			this.bias = bias;
			this.changeDate = new ChangeDate(systemTime);
		}

		// Token: 0x040057FA RID: 22522
		private int bias;

		// Token: 0x040057FB RID: 22523
		private ChangeDate changeDate;
	}
}
