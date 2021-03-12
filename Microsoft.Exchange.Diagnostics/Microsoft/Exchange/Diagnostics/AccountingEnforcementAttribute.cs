using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000DD RID: 221
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AccountingEnforcementAttribute : Attribute
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00019B07 File Offset: 0x00017D07
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00019B0F File Offset: 0x00017D0F
		public int RpcCount
		{
			get
			{
				return this.rpcCount;
			}
			set
			{
				this.rpcCount = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00019B18 File Offset: 0x00017D18
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00019B20 File Offset: 0x00017D20
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				this.comments = value;
			}
		}

		// Token: 0x04000450 RID: 1104
		private int rpcCount;

		// Token: 0x04000451 RID: 1105
		private string comments;
	}
}
