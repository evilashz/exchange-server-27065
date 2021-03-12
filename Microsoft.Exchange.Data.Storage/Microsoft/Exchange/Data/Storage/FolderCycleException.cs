using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072F RID: 1839
	[Serializable]
	public class FolderCycleException : StoragePermanentException
	{
		// Token: 0x060047D9 RID: 18393 RVA: 0x0013066A File Offset: 0x0012E86A
		public FolderCycleException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00130674 File Offset: 0x0012E874
		protected FolderCycleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
