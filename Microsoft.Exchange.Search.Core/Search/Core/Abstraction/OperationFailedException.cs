using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class OperationFailedException : ComponentException
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000023EF File Offset: 0x000005EF
		public OperationFailedException() : base(Strings.OperationFailure)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023FC File Offset: 0x000005FC
		public OperationFailedException(Exception innerException) : base(Strings.OperationFailure, innerException)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000240A File Offset: 0x0000060A
		public OperationFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002413 File Offset: 0x00000613
		public OperationFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000241D File Offset: 0x0000061D
		protected OperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
