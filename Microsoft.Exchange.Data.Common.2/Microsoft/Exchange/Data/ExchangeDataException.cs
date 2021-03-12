using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class ExchangeDataException : LocalizedException
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x000101A3 File Offset: 0x0000E3A3
		public ExchangeDataException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000101B1 File Offset: 0x0000E3B1
		public ExchangeDataException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000101C0 File Offset: 0x0000E3C0
		protected ExchangeDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
