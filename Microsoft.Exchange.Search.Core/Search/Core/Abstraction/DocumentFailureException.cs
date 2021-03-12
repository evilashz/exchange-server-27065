using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal class DocumentFailureException : OperationFailedException
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002427 File Offset: 0x00000627
		public DocumentFailureException() : base(Strings.DocumentFailure)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002434 File Offset: 0x00000634
		public DocumentFailureException(Exception innerException) : base(Strings.DocumentFailure, innerException)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002442 File Offset: 0x00000642
		public DocumentFailureException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000244B File Offset: 0x0000064B
		public DocumentFailureException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002455 File Offset: 0x00000655
		protected DocumentFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
