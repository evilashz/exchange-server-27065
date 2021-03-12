using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FF RID: 511
	internal class StringConverter : BaseConverter
	{
		// Token: 0x06000D55 RID: 3413 RVA: 0x00043450 File Offset: 0x00041650
		public override object ConvertToObject(string propertyString)
		{
			return propertyString;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00043453 File Offset: 0x00041653
		public override string ConvertToString(object propertyValue)
		{
			return (string)propertyValue;
		}
	}
}
