using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class MsgStorageNotFoundException : MsgStorageException
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00017E8A File Offset: 0x0001608A
		internal MsgStorageNotFoundException(MsgStorageErrorCode errorCode, string message, Exception exc) : base(errorCode, message, exc)
		{
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00017E95 File Offset: 0x00016095
		protected MsgStorageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
