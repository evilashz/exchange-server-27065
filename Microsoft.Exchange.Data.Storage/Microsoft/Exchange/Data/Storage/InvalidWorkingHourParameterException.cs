using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EE2 RID: 3810
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class InvalidWorkingHourParameterException : CorruptDataException
	{
		// Token: 0x06008382 RID: 33666 RVA: 0x0023B8A8 File Offset: 0x00239AA8
		public InvalidWorkingHourParameterException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008383 RID: 33667 RVA: 0x0023B8B1 File Offset: 0x00239AB1
		public InvalidWorkingHourParameterException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008384 RID: 33668 RVA: 0x0023B8BB File Offset: 0x00239ABB
		private InvalidWorkingHourParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
