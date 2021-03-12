using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071A RID: 1818
	[Serializable]
	public class CannotMoveDefaultFolderException : StoragePermanentException
	{
		// Token: 0x0600479C RID: 18332 RVA: 0x001302AA File Offset: 0x0012E4AA
		public CannotMoveDefaultFolderException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001302B3 File Offset: 0x0012E4B3
		protected CannotMoveDefaultFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
