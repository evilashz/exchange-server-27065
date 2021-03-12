using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000793 RID: 1939
	[Serializable]
	public class WrongObjectTypeException : StoragePermanentException
	{
		// Token: 0x0600490F RID: 18703 RVA: 0x00131B95 File Offset: 0x0012FD95
		public WrongObjectTypeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00131B9E File Offset: 0x0012FD9E
		public WrongObjectTypeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00131BA8 File Offset: 0x0012FDA8
		protected WrongObjectTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
