using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PermanentOperationLevelForItemsException : LocalizedException, IOperationLevelForItemException
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000056D5 File Offset: 0x000038D5
		public PermanentOperationLevelForItemsException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000056DE File Offset: 0x000038DE
		public PermanentOperationLevelForItemsException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000056E8 File Offset: 0x000038E8
		protected PermanentOperationLevelForItemsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
