using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000189 RID: 393
	public class VariableReference
	{
		// Token: 0x06002293 RID: 8851 RVA: 0x00068722 File Offset: 0x00066922
		public VariableReference()
		{
			this.UseInput = true;
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x00068731 File Offset: 0x00066931
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x00068739 File Offset: 0x00066939
		public string Variable { get; set; }

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x00068742 File Offset: 0x00066942
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0006874A File Offset: 0x0006694A
		public bool UseInput { get; set; }
	}
}
