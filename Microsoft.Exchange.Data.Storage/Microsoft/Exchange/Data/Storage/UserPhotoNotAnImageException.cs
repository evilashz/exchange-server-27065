using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078E RID: 1934
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoNotAnImageException : UserPhotoProcessingException
	{
		// Token: 0x06004901 RID: 18689 RVA: 0x00131B0A File Offset: 0x0012FD0A
		public UserPhotoNotAnImageException() : base(ClientStrings.UserPhotoNotAnImage)
		{
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00131B17 File Offset: 0x0012FD17
		protected UserPhotoNotAnImageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
