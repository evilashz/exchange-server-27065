using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000789 RID: 1929
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoNotFoundException : StoragePermanentException
	{
		// Token: 0x060048F7 RID: 18679 RVA: 0x00131A89 File Offset: 0x0012FC89
		public UserPhotoNotFoundException(bool preview) : base(preview ? ClientStrings.UserPhotoPreviewNotFound : ClientStrings.UserPhotoNotFound)
		{
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x00131AA0 File Offset: 0x0012FCA0
		protected UserPhotoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
