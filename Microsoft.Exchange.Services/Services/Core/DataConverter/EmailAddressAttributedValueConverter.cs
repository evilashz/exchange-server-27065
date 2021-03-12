using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E6 RID: 486
	internal sealed class EmailAddressAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x000425D8 File Offset: 0x000407D8
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000425DB File Offset: 0x000407DB
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000425E4 File Offset: 0x000407E4
		public override object ConvertToServiceObjectValue(object propertyValue, IdConverterWithCommandSettings idConverterWithCommandSettings)
		{
			AttributedValue<Participant> attributedValue = (AttributedValue<Participant>)propertyValue;
			if (attributedValue == null)
			{
				return null;
			}
			EmailAddressWrapper emailAddressWrapper = ParticipantConverter.ConvertParticipantToEmailAddressWrapper(attributedValue.Value, idConverterWithCommandSettings);
			if (emailAddressWrapper == null)
			{
				return null;
			}
			return new EmailAddressAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Value = emailAddressWrapper
			};
		}
	}
}
