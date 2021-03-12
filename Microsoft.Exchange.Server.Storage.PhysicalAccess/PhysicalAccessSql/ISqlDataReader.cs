using System;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D5 RID: 213
	public interface ISqlDataReader : IDisposable
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000945 RID: 2373
		int FieldCount { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000946 RID: 2374
		bool IsClosed { get; }

		// Token: 0x06000947 RID: 2375
		void Close();

		// Token: 0x06000948 RID: 2376
		bool GetBoolean(int i);

		// Token: 0x06000949 RID: 2377
		long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);

		// Token: 0x0600094A RID: 2378
		long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		// Token: 0x0600094B RID: 2379
		DateTime GetDateTime(int i);

		// Token: 0x0600094C RID: 2380
		Type GetFieldType(int i);

		// Token: 0x0600094D RID: 2381
		Guid GetGuid(int i);

		// Token: 0x0600094E RID: 2382
		short GetInt16(int i);

		// Token: 0x0600094F RID: 2383
		int GetInt32(int i);

		// Token: 0x06000950 RID: 2384
		long GetInt64(int i);

		// Token: 0x06000951 RID: 2385
		string GetName(int i);

		// Token: 0x06000952 RID: 2386
		int GetOrdinal(string name);

		// Token: 0x06000953 RID: 2387
		SqlBinary GetSqlBinary(int i);

		// Token: 0x06000954 RID: 2388
		string GetString(int i);

		// Token: 0x06000955 RID: 2389
		object GetValue(int i);

		// Token: 0x06000956 RID: 2390
		bool IsDBNull(int i);

		// Token: 0x06000957 RID: 2391
		bool NextResult();

		// Token: 0x06000958 RID: 2392
		bool Read();
	}
}
