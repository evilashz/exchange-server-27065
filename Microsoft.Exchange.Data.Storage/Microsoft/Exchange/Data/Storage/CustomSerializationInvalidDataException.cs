using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000724 RID: 1828
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CustomSerializationInvalidDataException : CustomSerializationException
	{
		// Token: 0x060047BC RID: 18364 RVA: 0x001304A8 File Offset: 0x0012E6A8
		public CustomSerializationInvalidDataException() : base(ServerStrings.ExInvalidCustomSerializationData)
		{
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x001304B5 File Offset: 0x0012E6B5
		public CustomSerializationInvalidDataException(Exception innerException) : base(ServerStrings.ExInvalidCustomSerializationData, innerException)
		{
		}
	}
}
