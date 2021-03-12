using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000218 RID: 536
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class TooComplexPhotoRequestException : Exception
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x000503DF File Offset: 0x0004E5DF
		public TooComplexPhotoRequestException()
		{
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000503E7 File Offset: 0x0004E5E7
		public TooComplexPhotoRequestException(string message) : base(message)
		{
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000503F0 File Offset: 0x0004E5F0
		public TooComplexPhotoRequestException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000503FA File Offset: 0x0004E5FA
		protected TooComplexPhotoRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
