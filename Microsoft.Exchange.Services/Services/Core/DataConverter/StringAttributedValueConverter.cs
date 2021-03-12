using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FE RID: 510
	internal sealed class StringAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x00043404 File Offset: 0x00041604
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00043407 File Offset: 0x00041607
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00043410 File Offset: 0x00041610
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<string> attributedValue = (AttributedValue<string>)propertyValue;
			return new StringAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Value = attributedValue.Value
			};
		}
	}
}
