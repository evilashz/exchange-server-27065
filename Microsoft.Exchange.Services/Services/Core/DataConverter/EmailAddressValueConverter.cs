using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001E7 RID: 487
	internal sealed class EmailAddressValueConverter : BaseConverter
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x00042633 File Offset: 0x00040833
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00042636 File Offset: 0x00040836
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00042640 File Offset: 0x00040840
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			EmailAddress emailAddress = (EmailAddress)propertyValue;
			return new EmailAddressWrapper
			{
				Name = emailAddress.Name,
				EmailAddress = emailAddress.Address,
				RoutingType = emailAddress.RoutingType,
				OriginalDisplayName = emailAddress.OriginalDisplayName
			};
		}
	}
}
