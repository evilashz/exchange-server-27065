using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078D RID: 1933
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoImageTooSmallException : UserPhotoProcessingException
	{
		// Token: 0x060048FF RID: 18687 RVA: 0x00131AEE File Offset: 0x0012FCEE
		public UserPhotoImageTooSmallException() : base(ClientStrings.UserPhotoImageTooSmall(UserPhotoUtilities.MinImageSize))
		{
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00131B00 File Offset: 0x0012FD00
		protected UserPhotoImageTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
