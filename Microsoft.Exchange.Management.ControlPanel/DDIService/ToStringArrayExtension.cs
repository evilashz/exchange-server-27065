using System;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000149 RID: 329
	public class ToStringArrayExtension : MarkupExtension
	{
		// Token: 0x06002145 RID: 8517 RVA: 0x0006452A File Offset: 0x0006272A
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DDIToStringArrayConverter(this.Property);
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x00064537 File Offset: 0x00062737
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0006453F File Offset: 0x0006273F
		public string Property { get; set; }
	}
}
