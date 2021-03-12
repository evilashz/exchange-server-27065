using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000739 RID: 1849
	[Serializable]
	public class InvalidFolderTypeException : StoragePermanentException
	{
		// Token: 0x060047F8 RID: 18424 RVA: 0x001307FE File Offset: 0x0012E9FE
		public InvalidFolderTypeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00130807 File Offset: 0x0012EA07
		public InvalidFolderTypeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x00130811 File Offset: 0x0012EA11
		protected InvalidFolderTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
