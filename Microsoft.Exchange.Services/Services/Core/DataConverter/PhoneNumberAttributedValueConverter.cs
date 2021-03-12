using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F5 RID: 501
	internal sealed class PhoneNumberAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000D27 RID: 3367 RVA: 0x00042DC5 File Offset: 0x00040FC5
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00042DC8 File Offset: 0x00040FC8
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00042DD0 File Offset: 0x00040FD0
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<PhoneNumber> attributedValue = (AttributedValue<PhoneNumber>)propertyValue;
			return new PhoneNumberAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Value = new PhoneNumber
				{
					Number = attributedValue.Value.Number,
					Type = attributedValue.Value.Type
				}
			};
		}
	}
}
