using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FD RID: 509
	internal sealed class StringArrayAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x000433BA File Offset: 0x000415BA
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000433BD File Offset: 0x000415BD
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000433C4 File Offset: 0x000415C4
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<string[]> attributedValue = (AttributedValue<string[]>)propertyValue;
			return new StringArrayAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Values = attributedValue.Value
			};
		}
	}
}
