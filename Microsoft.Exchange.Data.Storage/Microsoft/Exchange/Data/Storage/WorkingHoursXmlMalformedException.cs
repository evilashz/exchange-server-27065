using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EE0 RID: 3808
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class WorkingHoursXmlMalformedException : CorruptDataException
	{
		// Token: 0x0600837C RID: 33660 RVA: 0x0023B86E File Offset: 0x00239A6E
		public WorkingHoursXmlMalformedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x0023B877 File Offset: 0x00239A77
		public WorkingHoursXmlMalformedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600837E RID: 33662 RVA: 0x0023B881 File Offset: 0x00239A81
		private WorkingHoursXmlMalformedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
