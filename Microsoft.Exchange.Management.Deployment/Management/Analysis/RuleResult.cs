using System;
using Microsoft.Exchange.CommonHelpProvider;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000048 RID: 72
	internal sealed class RuleResult : Result<bool>
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00007BC9 File Offset: 0x00005DC9
		static RuleResult()
		{
			HelpProvider.Initialize(HelpProvider.HelpAppName.Setup);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007BD1 File Offset: 0x00005DD1
		public RuleResult(bool value) : base(value)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007BDA File Offset: 0x00005DDA
		public RuleResult(Exception exception) : base(exception)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007BE3 File Offset: 0x00005DE3
		public string GetHelpUrl()
		{
			return HelpProvider.ConstructHelpRenderingUrlWithQualifierHelpId("ms.exch.setupreadiness.", ((Rule)base.Source).GetHelpId()).ToString();
		}
	}
}
