using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000024 RID: 36
	internal static class DiagnosticQueryStrings
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000096EC File Offset: 0x000078EC
		public static string QueryNull()
		{
			return "Query must not be null or empty";
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000096F3 File Offset: 0x000078F3
		public static string ExceededTokenLimit(int tokenLimit)
		{
			return string.Format("Exceeded maximum token limit ({0})", tokenLimit);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009705 File Offset: 0x00007905
		public static string DatabaseNotFound(string databaseName)
		{
			return string.Format("Could not find database with MdbName \"{0}\"", databaseName);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009712 File Offset: 0x00007912
		public static string NoAvailableDatabase()
		{
			return "No databases are available for query";
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009719 File Offset: 0x00007919
		public static string DatabaseRequired(string tableName)
		{
			return string.Format("Table \"{0}\" must be prefixed with a database name", tableName);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009726 File Offset: 0x00007926
		public static string DatabaseMismatch()
		{
			return string.Format("Query and Table prefix indicate different databases", new object[0]);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009738 File Offset: 0x00007938
		public static string InvalidFrom()
		{
			return "Invalid From clause";
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000973F File Offset: 0x0000793F
		public static string TableNotFound(string tableName)
		{
			return string.Format("Table \"{0}\" was not found or is not supported by the current database schema.", tableName);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000974C File Offset: 0x0000794C
		public static string ColumnNotFound(string columnName)
		{
			return string.Format("Column \"{0}\" was not recognized as a physical column, property tag, or a property info OR it's not supported by the current database schema.", columnName);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00009759 File Offset: 0x00007959
		public static string ColumnTypeMissing(string columnName)
		{
			return string.Format("Column \"{0}\" is missing a valid Type.", columnName);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009766 File Offset: 0x00007966
		public static string ColumnTableMissing(string columnName)
		{
			return string.Format("Column \"{0}\" is missing a valid Table.", columnName);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009773 File Offset: 0x00007973
		public static string UnknownCriterionType()
		{
			return "Unknown type of query criterion";
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000977A File Offset: 0x0000797A
		public static string UnknownQueryOperator(DiagnosticQueryOperator op)
		{
			return string.Format("Unknown query operator {0}", op);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000978C File Offset: 0x0000798C
		public static string UnsupportedQueryValueType(Type type)
		{
			return string.Format("Values of type {0} are not yet supported by the query translator", type.Name);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000979E File Offset: 0x0000799E
		public static string FailedToTranslateValue(string value, Type type)
		{
			return string.Format("Couldn't translate value \"{0}\" to type {1}", value, type.Name);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000097B1 File Offset: 0x000079B1
		public static string InvalidTop()
		{
			return "Top requires a positive integer value";
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000097B8 File Offset: 0x000079B8
		public static string InvalidCountOrderBy()
		{
			return "Cannot combine a Count query with an Order-by clause";
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000097BF File Offset: 0x000079BF
		public static string DuplicateSortColumn(string columnName)
		{
			return string.Format("Sort Order contains a duplicate column \"{0}\"", columnName);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000097CC File Offset: 0x000079CC
		public static string DatabaseOffline(string databaseName)
		{
			return string.Format("Database [{0}] is offline and unavailable for query", databaseName);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000097D9 File Offset: 0x000079D9
		public static string UnsupportedQueryOperator(DiagnosticQueryOperator op)
		{
			return string.Format("Query operator {0} is not supported for this criterion", op);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000097EB File Offset: 0x000079EB
		public static string PartitionedTable(string tableName, IEnumerable<string> columnNames)
		{
			return string.Format("Table {0} is partitioned by columns {1}", tableName, string.Join(", ", columnNames));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009803 File Offset: 0x00007A03
		public static string DuplicateSet(string columnName)
		{
			return string.Format("Set list contains a duplicate column \"{0}\"", columnName);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009810 File Offset: 0x00007A10
		public static string UnsupportedQueryType()
		{
			return "Unknown or unsupported query type";
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009817 File Offset: 0x00007A17
		public static string InvalidQueryContext()
		{
			return "Only a Select query may target a Table Function";
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000981E File Offset: 0x00007A1E
		public static string InvalidStoreContext()
		{
			return "The connection provider given is not a context provider.";
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009825 File Offset: 0x00007A25
		public static string ColumnRequiresValue(string columnName)
		{
			return string.Format("Non-identity Column \"{0}\" must be provided a non-null value", columnName);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009832 File Offset: 0x00007A32
		public static string UnimplementedKeyword()
		{
			return "Unimplemented keyword";
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009839 File Offset: 0x00007A39
		public static string IncorrectParameterCount(string tableName, int expectedCount)
		{
			return string.Format("Table function \"{0}\" expects {1} parameter(s)", tableName, expectedCount);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000984C File Offset: 0x00007A4C
		public static string InvalidFolderId()
		{
			return "Folder ID format is invalid";
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009853 File Offset: 0x00007A53
		public static string LikeOperatorNotAllowed(string tableName, string columnName)
		{
			return string.Format("The LIKE comparison operator is not allowed for column \"{0}\" on table \"{1}\"", columnName, tableName);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009861 File Offset: 0x00007A61
		public static string RedactedTable(string tableName)
		{
			return string.Format("Table \"{0}\" may contain PII; to query this target requires elevated access", tableName);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000986E File Offset: 0x00007A6E
		public static string PrivateTable(string tableName)
		{
			return string.Format("Table \"{0}\" contains private content; this target may not be queried", tableName);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000987B File Offset: 0x00007A7B
		public static string RestrictedColumnValue(string prefix, int length)
		{
			return string.Format("{0} ({1} bytes)", prefix, length.ToString("N0"));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009894 File Offset: 0x00007A94
		public static string TruncatedColumnValue(long length)
		{
			return string.Format("TRUNCATED ({0} bytes)", length.ToString("N0"));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000098AC File Offset: 0x00007AAC
		public static string EmptySelectList()
		{
			return "Query must select at least one column";
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000098B3 File Offset: 0x00007AB3
		public static string ProcessorNotFound(string processor)
		{
			return string.Format("Processor \"{0}\"  was not found", processor);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static string ProcessorEmptyArguments()
		{
			return string.Format("Processor usage error: provide one or more column identifiers", new object[0]);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000098D2 File Offset: 0x00007AD2
		public static string ProcessorColumnNotFound(string column)
		{
			return string.Format("Unable to locate column {0} in arguments collection", column);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000098DF File Offset: 0x00007ADF
		public static string ProcessorUnsupportedType(string column, string type)
		{
			return string.Format("Column {0} type {1} is not supported by sizing", column, type);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000098ED File Offset: 0x00007AED
		public static string ProcessorCustomError(string message)
		{
			return string.Format("Processor error: {0}", message);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000098FA File Offset: 0x00007AFA
		public static string InvalidFixedColumnValue(string columnName, int expectedLength, int actualLength)
		{
			return string.Format("The specified value for column \"{0}\" in where clause does not match the required length: expected length = {1}, actual length={2}", columnName, expectedLength, actualLength);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009913 File Offset: 0x00007B13
		public static string InvalidEntryIdFormat()
		{
			return "EntryId length is expected to be either 46 bytes (for a folder) or 70 bytes (for a message).";
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000991A File Offset: 0x00007B1A
		public static string InvalidCategorizationInfoFormat()
		{
			return "Categorization info blob format is invalid.";
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009921 File Offset: 0x00007B21
		public static string UnableToParseEntryId()
		{
			return "Unable to parse EntryId, likely due to an unsupported entry id type.";
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009928 File Offset: 0x00007B28
		public static string InvalidExchangeIdBinaryFormat()
		{
			return "ExchangeId length is expected to be 8, 9, 22, 24, or 26 bytes.";
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000992F File Offset: 0x00007B2F
		public static string InvalidExchangeIdStringFormat()
		{
			return "ExchangeId string is expected to have the form <ReplId(Hex)>-<GlobCnt> or <ReplIdGuid>[<ReplId(Decimal)>]-<GlobCnt>.";
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009936 File Offset: 0x00007B36
		public static string InvalidExchangeIdListFormat()
		{
			return "ExchangeIdList length is expected to be greater than 0.";
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000993D File Offset: 0x00007B3D
		public static string UnableToOpenMailbox(int mailboxNumber)
		{
			return string.Format("Unable to open mailbox with MailboxNumber = {0}.", mailboxNumber);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000994F File Offset: 0x00007B4F
		public static string InvalidRestrictionFormat()
		{
			return "Restriction length is expected to be greater than 0.";
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009956 File Offset: 0x00007B56
		public static string UnableToLockMailbox(int mailboxNumber)
		{
			return string.Format("Unable to lock mailbox with MailboxNumber = {0}.", mailboxNumber);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009968 File Offset: 0x00007B68
		public static string MailboxStateNotFound(int mailboxNumber)
		{
			return string.Format("MailboxState is not found for mailbox with MailboxNumber = {0}.", mailboxNumber);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000997A File Offset: 0x00007B7A
		public static string ClusterNotInstalled()
		{
			return "The database is hosted on a server where the Failover Cluster Service is not installed.";
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009981 File Offset: 0x00007B81
		public static string ServerIsNotDAGMember()
		{
			return "The database is hosted on a server that is not a member of a DAG.";
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009988 File Offset: 0x00007B88
		public static string InvalidAclTableAndSDFormat(string innerMessage)
		{
			return string.Format("Invalid AclTableAndSD property format: {0}", innerMessage);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009995 File Offset: 0x00007B95
		public static string FaultExecutingFullTextQuery(Exception ex)
		{
			return string.Format("{0} while executing FullTextQuery: {1}", ex.GetType().Name, ex.Message);
		}
	}
}
