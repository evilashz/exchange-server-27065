using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000187 RID: 391
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public class MockDDIAttribute : DDIValidateAttribute
	{
		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00068487 File Offset: 0x00066687
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x0006848F File Offset: 0x0006668F
		public bool ReportError { get; set; }

		// Token: 0x0600226C RID: 8812 RVA: 0x00068498 File Offset: 0x00066698
		public MockDDIAttribute() : base("MockDDIAttribute")
		{
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000684A8 File Offset: 0x000666A8
		public override List<string> Validate(object target, Service profile)
		{
			List<string> list = new List<string>();
			if (this.ReportError)
			{
				list.Add("MockDDIAttribute error: " + profile.Name);
			}
			return list;
		}
	}
}
