using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000730 RID: 1840
	[Serializable]
	public class FolderSaveConditionViolationException : StoragePermanentException
	{
		// Token: 0x060047DB RID: 18395 RVA: 0x0013067E File Offset: 0x0012E87E
		public FolderSaveConditionViolationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x00130688 File Offset: 0x0012E888
		protected FolderSaveConditionViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
