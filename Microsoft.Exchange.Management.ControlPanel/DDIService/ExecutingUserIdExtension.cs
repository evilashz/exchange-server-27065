using System;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000145 RID: 325
	public class ExecutingUserIdExtension : MarkupExtension
	{
		// Token: 0x0600213B RID: 8507 RVA: 0x000643E6 File Offset: 0x000625E6
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Identity.FromExecutingUserId();
		}
	}
}
