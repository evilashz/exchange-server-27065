using System;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000148 RID: 328
	public class TargetTypeExtension : MarkupExtension
	{
		// Token: 0x06002140 RID: 8512 RVA: 0x000644EF File Offset: 0x000626EF
		public TargetTypeExtension(Type targetType)
		{
			this.targetType = targetType;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000644FE File Offset: 0x000626FE
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DDIStrongTypeConverter(this.targetType, this.ConvertMode);
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00064511 File Offset: 0x00062711
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x00064519 File Offset: 0x00062719
		public ConvertMode ConvertMode { get; set; }

		// Token: 0x04001D17 RID: 7447
		private Type targetType;
	}
}
