using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D7D RID: 3453
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidIdException : StoragePermanentException
	{
		// Token: 0x0600771B RID: 30491 RVA: 0x0020DB2E File Offset: 0x0020BD2E
		internal InvalidIdException() : base(LocalizedString.Empty)
		{
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x0020DB3B File Offset: 0x0020BD3B
		private InvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
