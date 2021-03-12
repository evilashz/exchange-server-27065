using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class MsgStorageException : ExchangeDataException
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00017E1A File Offset: 0x0001601A
		internal MsgStorageException(MsgStorageErrorCode errorCode, string message) : base(string.Format("{0}, Errorcode = {1}", message, errorCode))
		{
			this.errorCode = errorCode;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00017E3A File Offset: 0x0001603A
		internal MsgStorageException(MsgStorageErrorCode errorCode, string message, int hResult) : base(string.Format("{0}, Errorcode = {1}, HResult = {1}", message, errorCode, hResult))
		{
			base.HResult = hResult;
			this.errorCode = errorCode;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00017E67 File Offset: 0x00016067
		internal MsgStorageException(MsgStorageErrorCode errorCode, string message, Exception exc) : base(message, exc)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00017E78 File Offset: 0x00016078
		protected MsgStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00017E82 File Offset: 0x00016082
		public MsgStorageErrorCode MsgStorageErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x04000561 RID: 1377
		private const string ExceptionMessageFormatString = "{0}, Errorcode = {1}";

		// Token: 0x04000562 RID: 1378
		private const string ExceptionMessageHResultFormatString = "{0}, Errorcode = {1}, HResult = {1}";

		// Token: 0x04000563 RID: 1379
		private MsgStorageErrorCode errorCode;
	}
}
