using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DC RID: 476
	internal abstract class EnumConverter : BaseConverter
	{
		// Token: 0x06000CBC RID: 3260 RVA: 0x00041BBB File Offset: 0x0003FDBB
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			return this.ConvertToString(propertyValue);
		}
	}
}
