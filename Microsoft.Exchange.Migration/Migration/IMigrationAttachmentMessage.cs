using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationAttachmentMessage
	{
		// Token: 0x06000A11 RID: 2577
		IMigrationAttachment CreateAttachment(string name);

		// Token: 0x06000A12 RID: 2578
		IMigrationAttachment GetAttachment(string name, PropertyOpenMode openMode);

		// Token: 0x06000A13 RID: 2579
		bool TryGetAttachment(string name, PropertyOpenMode openMode, out IMigrationAttachment attachment);

		// Token: 0x06000A14 RID: 2580
		void DeleteAttachment(string name);
	}
}
