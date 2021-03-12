using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog
{
	// Token: 0x02000004 RID: 4
	public class OABDownloadLogLine : LogLine
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002208 File Offset: 0x00000408
		public OABDownloadLogLine(List<string> header, LogSourceLine line) : base(line)
		{
			this.data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			IList<string> columns = line.GetColumns();
			if (columns.Count < header.Count)
			{
				base.RaiseException(string.Format("Headers collection provided does not match the schema. Header count: {0}, Columns count: {1}", header.Count, columns.Count), new object[0]);
			}
			if (line.Timestamp != null)
			{
				this.timestamp = line.Timestamp.Value;
			}
			else
			{
				base.RaiseException("First column of line should be a well-formatted DateTime, column-value: {0}", new object[]
				{
					columns[0]
				});
			}
			for (int i = 0; i < header.Count; i++)
			{
				this.data[header[i]] = columns[i];
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000022DD File Offset: 0x000004DD
		public override DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022E5 File Offset: 0x000004E5
		public string Organization
		{
			get
			{
				return this.GetColumnValue("Domain");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022F2 File Offset: 0x000004F2
		public string HttpStatus
		{
			get
			{
				return this.GetColumnValue("HttpStatus");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022FF File Offset: 0x000004FF
		public string FailureCode
		{
			get
			{
				return this.GetColumnValue("FailureCode");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000230C File Offset: 0x0000050C
		public string LastRequestedTime
		{
			get
			{
				return this.GetColumnValue("LastRequestedTime");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002319 File Offset: 0x00000519
		public string LastTouchedTime
		{
			get
			{
				return this.GetColumnValue("LastTouchedTime");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002326 File Offset: 0x00000526
		public string GenericInfo
		{
			get
			{
				return this.GetColumnValue("GenericInfo");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002333 File Offset: 0x00000533
		public string GenericErrors
		{
			get
			{
				return this.GetColumnValue("GenericErrors");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002340 File Offset: 0x00000540
		public OrganizationStatus OrgStatus
		{
			get
			{
				string columnValue = this.GetColumnValue("OrganizationStatus");
				OrganizationStatus result = OrganizationStatus.Invalid;
				if (!string.IsNullOrEmpty(columnValue) && !Enum.TryParse<OrganizationStatus>(columnValue, out result))
				{
					result = OrganizationStatus.Invalid;
				}
				return result;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002370 File Offset: 0x00000570
		public bool IsAddressListDeleted
		{
			get
			{
				bool flag;
				return bool.TryParse(this.GetColumnValue("IsAddressListDeleted"), out flag) && flag;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002394 File Offset: 0x00000594
		private string GetColumnValue(string columnName)
		{
			string result;
			if (this.data != null && this.data.TryGetValue(columnName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000003 RID: 3
		private readonly DateTime timestamp;

		// Token: 0x04000004 RID: 4
		private readonly Dictionary<string, string> data;
	}
}
