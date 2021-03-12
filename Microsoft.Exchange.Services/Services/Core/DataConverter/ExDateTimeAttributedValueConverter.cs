using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E8 RID: 488
	internal sealed class ExDateTimeAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00042695 File Offset: 0x00040895
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00042698 File Offset: 0x00040898
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000426A0 File Offset: 0x000408A0
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<ExDateTime> attributedValue = (AttributedValue<ExDateTime>)propertyValue;
			return new StringAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Value = ExDateTimeConverter.ToUtcXsdDateTime(attributedValue.Value)
			};
		}
	}
}
