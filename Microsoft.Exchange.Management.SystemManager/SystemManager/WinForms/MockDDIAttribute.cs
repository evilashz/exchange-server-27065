using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000105 RID: 261
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public class MockDDIAttribute : DDIValidateAttribute
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0002118A File Offset: 0x0001F38A
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00021192 File Offset: 0x0001F392
		public bool ReportError { get; set; }

		// Token: 0x0600096D RID: 2413 RVA: 0x0002119B File Offset: 0x0001F39B
		public MockDDIAttribute() : base("MockDDIAttribute")
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000211A8 File Offset: 0x0001F3A8
		public override List<string> Validate(object target, PageConfigurableProfile profile)
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
