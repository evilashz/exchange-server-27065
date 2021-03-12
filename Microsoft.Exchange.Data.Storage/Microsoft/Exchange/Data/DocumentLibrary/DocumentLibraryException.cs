using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C1 RID: 1729
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class DocumentLibraryException : LocalizedException
	{
		// Token: 0x06004576 RID: 17782 RVA: 0x00127B7E File Offset: 0x00125D7E
		public DocumentLibraryException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00127B8C File Offset: 0x00125D8C
		public DocumentLibraryException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}
	}
}
