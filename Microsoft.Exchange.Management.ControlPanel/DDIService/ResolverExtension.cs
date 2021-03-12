using System;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000147 RID: 327
	public class ResolverExtension : MarkupExtension
	{
		// Token: 0x0600213E RID: 8510 RVA: 0x000644BF File Offset: 0x000626BF
		public ResolverExtension(string resolverType)
		{
			this.resolverType = (ResolverType)Enum.Parse(typeof(ResolverType), resolverType);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000644E2 File Offset: 0x000626E2
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DDIResolver(this.resolverType);
		}

		// Token: 0x04001D16 RID: 7446
		private ResolverType resolverType;
	}
}
