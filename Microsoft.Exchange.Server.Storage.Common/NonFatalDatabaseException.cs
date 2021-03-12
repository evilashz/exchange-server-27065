using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000024 RID: 36
	public class NonFatalDatabaseException : Exception
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00007B62 File Offset: 0x00005D62
		public NonFatalDatabaseException(string message) : base(message)
		{
			this.errorCode = ErrorCodeValue.DatabaseError;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007B76 File Offset: 0x00005D76
		public NonFatalDatabaseException(string message, Exception innerException) : base(message, innerException)
		{
			this.errorCode = ErrorCodeValue.DatabaseError;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00007B8B File Offset: 0x00005D8B
		public NonFatalDatabaseException(ErrorCodeValue errorCode, string message, Exception innerException) : base(message, innerException)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00007B9C File Offset: 0x00005D9C
		public ErrorCodeValue Error
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x040003F3 RID: 1011
		private const string ErrorCodeSerializationLabel = "errorCode";

		// Token: 0x040003F4 RID: 1012
		private ErrorCodeValue errorCode;
	}
}
