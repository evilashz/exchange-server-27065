using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073D RID: 1853
	[Serializable]
	public class InvalidParentFolderException : StoragePermanentException
	{
		// Token: 0x06004804 RID: 18436 RVA: 0x00130872 File Offset: 0x0012EA72
		public InvalidParentFolderException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0013087B File Offset: 0x0012EA7B
		protected InvalidParentFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
