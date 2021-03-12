using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000176 RID: 374
	internal sealed class WebClientReadFormQueryStringProperty : WebClientQueryStringPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x000340DE File Offset: 0x000322DE
		private WebClientReadFormQueryStringProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000340E7 File Offset: 0x000322E7
		public static WebClientReadFormQueryStringProperty CreateCommand(CommandContext commandContext)
		{
			return new WebClientReadFormQueryStringProperty(commandContext);
		}
	}
}
