using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001CA RID: 458
	public class ConditionDescriptor
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0004E86A File Offset: 0x0004CA6A
		public ConditionDescriptor(int index, string description)
		{
			this.index = index;
			this.Description = description;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0004E880 File Offset: 0x0004CA80
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0004E888 File Offset: 0x0004CA88
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x0004E890 File Offset: 0x0004CA90
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x04000729 RID: 1833
		private int index;

		// Token: 0x0400072A RID: 1834
		private string description;
	}
}
