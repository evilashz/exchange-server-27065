using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EE1 RID: 3809
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class WorkingHoursSaveFailedException : StoragePermanentException
	{
		// Token: 0x0600837F RID: 33663 RVA: 0x0023B88B File Offset: 0x00239A8B
		public WorkingHoursSaveFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008380 RID: 33664 RVA: 0x0023B894 File Offset: 0x00239A94
		public WorkingHoursSaveFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008381 RID: 33665 RVA: 0x0023B89E File Offset: 0x00239A9E
		private WorkingHoursSaveFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
