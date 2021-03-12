using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000733 RID: 1843
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class GlobalCounterRangeExceededException : StorageTransientException
	{
		// Token: 0x060047E1 RID: 18401 RVA: 0x001306CE File Offset: 0x0012E8CE
		public GlobalCounterRangeExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x001306D8 File Offset: 0x0012E8D8
		protected GlobalCounterRangeExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
