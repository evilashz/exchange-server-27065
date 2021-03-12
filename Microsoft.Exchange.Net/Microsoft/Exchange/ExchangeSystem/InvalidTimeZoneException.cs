using System;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000B03 RID: 2819
	[Serializable]
	internal class InvalidTimeZoneException : ExArgumentException
	{
		// Token: 0x06003CA3 RID: 15523 RVA: 0x0009DF59 File Offset: 0x0009C159
		public InvalidTimeZoneException(string message) : base(message)
		{
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x0009DF62 File Offset: 0x0009C162
		public InvalidTimeZoneException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
