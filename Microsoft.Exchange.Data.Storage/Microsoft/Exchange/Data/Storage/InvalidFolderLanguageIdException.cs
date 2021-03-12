using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000738 RID: 1848
	[Serializable]
	public class InvalidFolderLanguageIdException : StoragePermanentException
	{
		// Token: 0x060047F5 RID: 18421 RVA: 0x001307E1 File Offset: 0x0012E9E1
		public InvalidFolderLanguageIdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x001307EA File Offset: 0x0012E9EA
		public InvalidFolderLanguageIdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x001307F4 File Offset: 0x0012E9F4
		protected InvalidFolderLanguageIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
