using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000718 RID: 1816
	[Serializable]
	public class CalendarProcessingException : CorruptDataException
	{
		// Token: 0x06004796 RID: 18326 RVA: 0x00130270 File Offset: 0x0012E470
		public CalendarProcessingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00130279 File Offset: 0x0012E479
		public CalendarProcessingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00130283 File Offset: 0x0012E483
		protected CalendarProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
