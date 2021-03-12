using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078B RID: 1931
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoFileTooLargeException : UserPhotoProcessingException
	{
		// Token: 0x060048FB RID: 18683 RVA: 0x00131ABD File Offset: 0x0012FCBD
		public UserPhotoFileTooLargeException() : base(ClientStrings.UserPhotoFileTooLarge(20))
		{
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00131ACC File Offset: 0x0012FCCC
		protected UserPhotoFileTooLargeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
