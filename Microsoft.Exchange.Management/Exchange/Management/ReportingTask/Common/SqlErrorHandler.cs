using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006AE RID: 1710
	internal class SqlErrorHandler
	{
		// Token: 0x06003C90 RID: 15504 RVA: 0x00101E28 File Offset: 0x00100028
		public static LocalizedException TrasnlateError(SqlException sqlException)
		{
			if (sqlException.Number == 208 || sqlException.Number == 207)
			{
				return new InvalidDataSourceException(sqlException.Number, sqlException);
			}
			if (sqlException.Number == -2)
			{
				return new DataMartTimeoutException(sqlException);
			}
			if (sqlException.Class >= 11 && sqlException.Class <= 16)
			{
				return new InvalidQueryException(sqlException.Number, sqlException);
			}
			if (sqlException.Class >= 17 && sqlException.Class <= 25)
			{
				return new InternalErrorException(sqlException.Number, sqlException);
			}
			return new DatabaseException(sqlException.Number, sqlException.Message, sqlException);
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x00101EC1 File Offset: 0x001000C1
		public static LocalizedException TrasnlateConnectionError(SqlException sqlException)
		{
			return new ConnectionFailedException(sqlException.Number, sqlException);
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x00101ECF File Offset: 0x001000CF
		public static LocalizedException TrasnlateError(SqlTypeException sqlException)
		{
			return new InvalidDataException(sqlException.Message, sqlException);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x00101EDD File Offset: 0x001000DD
		public static bool IsObjectNotFoundError(SqlException sqlException)
		{
			return sqlException.Number == 208;
		}

		// Token: 0x0400273F RID: 10047
		private const int ObjectNotFound = 208;

		// Token: 0x04002740 RID: 10048
		private const int ColumnNotFound = 207;

		// Token: 0x04002741 RID: 10049
		private const int TimeoutExpired = -2;
	}
}
