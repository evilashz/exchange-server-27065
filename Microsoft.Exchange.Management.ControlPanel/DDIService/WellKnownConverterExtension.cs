using System;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200014C RID: 332
	public class WellKnownConverterExtension : MarkupExtension
	{
		// Token: 0x0600214F RID: 8527 RVA: 0x000645B4 File Offset: 0x000627B4
		public WellKnownConverterExtension(string converterType)
		{
			this.ConverterType = (WellKnownConverterType)Enum.Parse(typeof(WellKnownConverterType), converterType);
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000645D7 File Offset: 0x000627D7
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x000645DF File Offset: 0x000627DF
		public WellKnownConverterType ConverterType { get; set; }

		// Token: 0x06002152 RID: 8530 RVA: 0x000645E8 File Offset: 0x000627E8
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			switch (this.ConverterType)
			{
			case WellKnownConverterType.PasswordInput:
				return new SecureStringInputConverter();
			case WellKnownConverterType.ToString:
				return new ToStringConverter(ConvertMode.PerItemInEnumerable);
			default:
				throw new NotImplementedException();
			}
		}
	}
}
