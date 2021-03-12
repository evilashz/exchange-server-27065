using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F6 RID: 502
	internal class PhoneNumberConverter : BaseConverter
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00042E33 File Offset: 0x00041033
		public override object ConvertToObject(string propertyString)
		{
			return null;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00042E36 File Offset: 0x00041036
		public override string ConvertToString(object propertyValue)
		{
			return string.Empty;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00042E40 File Offset: 0x00041040
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			PhoneNumber phoneNumber = propertyValue as PhoneNumber;
			if (phoneNumber == null)
			{
				return null;
			}
			return new PhoneNumber(phoneNumber.Number, phoneNumber.Type);
		}
	}
}
