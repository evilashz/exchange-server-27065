using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078C RID: 1932
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoFileTooSmallException : UserPhotoProcessingException
	{
		// Token: 0x060048FD RID: 18685 RVA: 0x00131AD6 File Offset: 0x0012FCD6
		public UserPhotoFileTooSmallException() : base(ClientStrings.UserPhotoFileTooSmall(0))
		{
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00131AE4 File Offset: 0x0012FCE4
		protected UserPhotoFileTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
