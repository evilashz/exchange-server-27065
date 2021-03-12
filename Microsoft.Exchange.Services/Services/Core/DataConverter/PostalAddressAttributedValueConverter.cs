using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F7 RID: 503
	internal sealed class PostalAddressAttributedValueConverter : BaseConverter
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x00042E72 File Offset: 0x00041072
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00042E75 File Offset: 0x00041075
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00042E7C File Offset: 0x0004107C
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			if (propertyValue == null)
			{
				return null;
			}
			AttributedValue<Microsoft.Exchange.Data.Storage.PostalAddress> attributedValue = (AttributedValue<Microsoft.Exchange.Data.Storage.PostalAddress>)propertyValue;
			return new PostalAddressAttributedValue
			{
				Attributions = attributedValue.Attributions,
				Value = new Microsoft.Exchange.Services.Core.Types.PostalAddress
				{
					Street = attributedValue.Value.Street,
					City = attributedValue.Value.City,
					State = attributedValue.Value.State,
					Country = attributedValue.Value.Country,
					PostalCode = attributedValue.Value.PostalCode,
					PostOfficeBox = attributedValue.Value.PostOfficeBox,
					LocationSource = (LocationSourceType)attributedValue.Value.LocationSource,
					LocationUri = attributedValue.Value.LocationUri,
					Type = attributedValue.Value.Type,
					Latitude = attributedValue.Value.Latitude,
					Longitude = attributedValue.Value.Longitude,
					Accuracy = attributedValue.Value.Accuracy,
					Altitude = attributedValue.Value.Altitude,
					AltitudeAccuracy = attributedValue.Value.AltitudeAccuracy
				}
			};
		}
	}
}
