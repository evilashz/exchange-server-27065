using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200078A RID: 1930
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UserPhotoProcessingException : StoragePermanentException
	{
		// Token: 0x060048F9 RID: 18681 RVA: 0x00131AAA File Offset: 0x0012FCAA
		public UserPhotoProcessingException(LocalizedString msg) : base(msg)
		{
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x00131AB3 File Offset: 0x0012FCB3
		protected UserPhotoProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
