using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200001E RID: 30
	internal static class MigMonUtilities
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005A94 File Offset: 0x00003C94
		public static DateTime ConverttoDateTime(string s)
		{
			DateTime result;
			if (string.IsNullOrWhiteSpace(s) || !DateTime.TryParse(s, out result))
			{
				result = default(DateTime);
			}
			else
			{
				result = result.ToUniversalTime();
			}
			if (result.CompareTo(MigMonUtilities.sqlDateTimeMinValue) <= 0)
			{
				return MigMonUtilities.sqlDateTimeMinValue;
			}
			if (result.CompareTo(MigMonUtilities.sqlDateTimeMaxValue) >= 0)
			{
				return MigMonUtilities.sqlDateTimeMaxValue;
			}
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public static SqlDateTime ConvertToSqlDateTime(string s)
		{
			return new SqlDateTime(MigMonUtilities.ConverttoDateTime(s));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005B00 File Offset: 0x00003D00
		public static Guid ConvertToGuid(string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return Guid.Empty;
			}
			Guid result;
			if (Guid.TryParse(s, out result))
			{
				return result;
			}
			return Guid.Empty;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005B2C File Offset: 0x00003D2C
		public static string Encrypt(string originalString)
		{
			string result;
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] bytes = Encoding.ASCII.GetBytes("ABE94726E3321250");
				CryptoStream cryptoStream = new CryptoStream(memoryStream, aesCryptoServiceProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
				StreamWriter streamWriter = new StreamWriter(cryptoStream);
				streamWriter.Write(originalString);
				streamWriter.Flush();
				cryptoStream.FlushFinalBlock();
				streamWriter.Flush();
				result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005BBC File Offset: 0x00003DBC
		public static string Decrypt(string cryptedString)
		{
			string result;
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				MemoryStream stream = new MemoryStream(Convert.FromBase64String(cryptedString));
				byte[] bytes = Encoding.ASCII.GetBytes("ABE94726E3321250");
				CryptoStream stream2 = new CryptoStream(stream, aesCryptoServiceProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
				StreamReader streamReader = new StreamReader(stream2);
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005C2C File Offset: 0x00003E2C
		public static int? GetLocalServerId()
		{
			if (!MigrationMonitor.KnownStringIdMap[KnownStringType.LocalServerName].ContainsKey(MigrationMonitor.ComputerName))
			{
				MigrationMonitor.KnownStringIdMap[KnownStringType.LocalServerName].Add(MigrationMonitor.ComputerName, new int?(MigrationMonitor.SqlHelper.GetLoggingServerId()));
			}
			return MigrationMonitor.KnownStringIdMap[KnownStringType.LocalServerName][MigrationMonitor.ComputerName];
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005C8C File Offset: 0x00003E8C
		public static int? GetValueFromIdMap(string stringValue, KnownStringType stringType, string sqlLookupName)
		{
			if (stringType <= KnownStringType.EndpointGuid)
			{
				if (stringType == KnownStringType.TenantName)
				{
					return MigMonUtilities.GetTenantNameId(stringValue);
				}
				if (stringType == KnownStringType.EndpointGuid)
				{
					return MigMonUtilities.GetEndpointId(stringValue);
				}
			}
			else if (stringType != KnownStringType.WatsonHash)
			{
				if (stringType == KnownStringType.LocalServerName)
				{
					return MigMonUtilities.GetLocalServerId();
				}
			}
			else
			{
				if (!MigrationMonitor.KnownStringIdMap[stringType].ContainsKey(stringValue))
				{
					return null;
				}
				return MigrationMonitor.KnownStringIdMap[stringType][stringValue];
			}
			if (!MigrationMonitor.KnownStringIdMap[stringType].ContainsKey(stringValue))
			{
				MigrationMonitor.KnownStringIdMap[stringType].Add(stringValue, MigrationMonitor.SqlHelper.GetIdForKnownString(stringValue, sqlLookupName));
			}
			return MigrationMonitor.KnownStringIdMap[stringType][stringValue];
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005D3C File Offset: 0x00003F3C
		public static int? GetTenantNameId(string tenantName)
		{
			if (!MigrationMonitor.KnownStringIdMap[KnownStringType.TenantName].ContainsKey(tenantName))
			{
				MigrationMonitor.KnownStringIdMap[KnownStringType.TenantName].Add(tenantName, new int?(MigrationMonitor.SqlHelper.GetIdForKnownTenantName(tenantName)));
			}
			return MigrationMonitor.KnownStringIdMap[KnownStringType.TenantName][tenantName];
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005D90 File Offset: 0x00003F90
		public static int? GetEndpointId(string endpointGuid)
		{
			if (!MigrationMonitor.KnownStringIdMap[KnownStringType.EndpointGuid].ContainsKey(endpointGuid))
			{
				MigrationMonitor.KnownStringIdMap[KnownStringType.EndpointGuid].Add(endpointGuid, new int?(MigrationMonitor.SqlHelper.GetIdForKnownEndpoint(endpointGuid)));
			}
			return MigrationMonitor.KnownStringIdMap[KnownStringType.EndpointGuid][endpointGuid];
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static int? GetWatsonHashId(string watsonHash, string stackTrace, string service)
		{
			string service2 = MigMonUtilities.TruncateMessage(service, 32);
			if (!MigrationMonitor.KnownStringIdMap[KnownStringType.WatsonHash].ContainsKey(watsonHash))
			{
				if (string.IsNullOrWhiteSpace(stackTrace))
				{
					return null;
				}
				MigrationMonitor.KnownStringIdMap[KnownStringType.WatsonHash].Add(watsonHash, new int?(MigrationMonitor.SqlHelper.GetIdForKnownWatsonHash(watsonHash, stackTrace, service2)));
			}
			return MigrationMonitor.KnownStringIdMap[KnownStringType.WatsonHash][watsonHash];
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005E5C File Offset: 0x0000405C
		public static string GetColumnStringValue(CsvRow logRow, string columnName)
		{
			if (logRow.ColumnMap.Contains(columnName))
			{
				return logRow[columnName];
			}
			return string.Empty;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005E8C File Offset: 0x0000408C
		public static T GetColumnValue<T>(CsvRow logRow, string columnName)
		{
			if (!logRow.ColumnMap.Contains(columnName))
			{
				return default(T);
			}
			if (string.IsNullOrWhiteSpace(logRow[columnName]))
			{
				return default(T);
			}
			if (typeof(T) == typeof(Guid))
			{
				Guid guid = MigMonUtilities.ConvertToGuid(logRow[columnName]);
				return (T)((object)Convert.ChangeType(guid, typeof(T)));
			}
			if (typeof(T) == typeof(string))
			{
				return (T)((object)Convert.ChangeType(MigMonUtilities.GetColumnStringValue(logRow, columnName), typeof(T)));
			}
			if (typeof(T) == typeof(SqlDateTime))
			{
				SqlDateTime sqlDateTime = MigMonUtilities.ConvertToSqlDateTime(logRow[columnName]);
				return (T)((object)Convert.ChangeType(sqlDateTime, typeof(T)));
			}
			if (typeof(T) == typeof(DateTime))
			{
				DateTime dateTime = MigMonUtilities.ConverttoDateTime(logRow[columnName]);
				return (T)((object)Convert.ChangeType(dateTime, typeof(T)));
			}
			if (!(typeof(T) == typeof(int)) && !(typeof(T) == typeof(bool)) && !(typeof(T) == typeof(long)) && !(typeof(T) == typeof(double)))
			{
				if (!(typeof(T) == typeof(float)))
				{
					goto IL_20B;
				}
			}
			try
			{
				return (T)((object)Convert.ChangeType(logRow[columnName], typeof(T)));
			}
			catch (Exception ex)
			{
				if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
				{
					return default(T);
				}
				throw;
			}
			IL_20B:
			throw new ArgumentException("Only support string, int, bool, SqlDateTime, DateTime, long, double, float and Guid");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000060DC File Offset: 0x000042DC
		public static ColumnDefinition<int> GetLookupColumnDefinition(List<ColumnDefinition<int>> columnsList, KnownStringType stringType)
		{
			if (!columnsList.Any<ColumnDefinition<int>>())
			{
				return new ColumnDefinition<int>();
			}
			return columnsList.FirstOrDefault((ColumnDefinition<int> t) => t.KnownStringType == stringType);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006116 File Offset: 0x00004316
		public static string TruncateMessage(string message, int length = 500)
		{
			if (string.IsNullOrEmpty(message) || message.Length <= length)
			{
				return message;
			}
			return message.Substring(0, length - 3) + "...";
		}

		// Token: 0x040000A6 RID: 166
		private const string CryptoKey = "ABE94726E3321250";

		// Token: 0x040000A7 RID: 167
		private const int MaxMessageLength = 500;

		// Token: 0x040000A8 RID: 168
		private const int MaxErrorLength = 4000;

		// Token: 0x040000A9 RID: 169
		private static readonly DateTime sqlDateTimeMinValue = SqlDateTime.MinValue.Value;

		// Token: 0x040000AA RID: 170
		private static readonly DateTime sqlDateTimeMaxValue = SqlDateTime.MaxValue.Value;
	}
}
