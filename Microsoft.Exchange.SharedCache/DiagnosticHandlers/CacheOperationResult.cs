using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache.DiagnosticHandlers
{
	// Token: 0x0200000F RID: 15
	public class CacheOperationResult
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000033FE File Offset: 0x000015FE
		internal CacheOperationResult()
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003406 File Offset: 0x00001606
		internal CacheOperationResult(OperationQuery query, OperationResult result)
		{
			ArgumentValidator.ThrowIfNull("query", query);
			this.Key = query.Constraint;
			this.Operation = query.Operation;
			this.Result = result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003438 File Offset: 0x00001638
		internal CacheOperationResult(OperationQuery query, OperationResult result, string extraDetails) : this(query, result)
		{
			ArgumentValidator.ThrowIfNull("query", query);
			this.ExtraDetails = extraDetails;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003454 File Offset: 0x00001654
		internal CacheOperationResult(OperationQuery query, KeyValuePair<string, CacheEntry> keyAndEntry) : this(query, (keyAndEntry.Value != null) ? OperationResult.OK : OperationResult.NotFound)
		{
			ArgumentValidator.ThrowIfNull("query", query);
			ArgumentValidator.ThrowIfNull("keyAndEntry", keyAndEntry);
			if (query.Operation == Operation.Delete)
			{
				this.ExtraDetails = string.Format("{0} entries removed.", (keyAndEntry.Value != null) ? 1 : 0);
			}
			if (keyAndEntry.Value != null)
			{
				this.Value = CacheOperationResult.ConvertToXMLSafeString(Encoding.ASCII.GetString(keyAndEntry.Value.Value));
				this.Base64Value = Convert.ToBase64String(keyAndEntry.Value.Value);
				this.CreatedAt = keyAndEntry.Value.CreatedAt;
				this.UpdatedAt = keyAndEntry.Value.UpdatedAt;
				this.Key = keyAndEntry.Key;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000352D File Offset: 0x0000172D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003535 File Offset: 0x00001735
		public Operation Operation { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000353E File Offset: 0x0000173E
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003546 File Offset: 0x00001746
		public OperationResult Result { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000354F File Offset: 0x0000174F
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003557 File Offset: 0x00001757
		public string Key { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003560 File Offset: 0x00001760
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003568 File Offset: 0x00001768
		public string Value { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003571 File Offset: 0x00001771
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003579 File Offset: 0x00001779
		public string Base64Value { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003582 File Offset: 0x00001782
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000358A File Offset: 0x0000178A
		public DateTime CreatedAt { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003593 File Offset: 0x00001793
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000359B File Offset: 0x0000179B
		public DateTime UpdatedAt { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000035A4 File Offset: 0x000017A4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000035AC File Offset: 0x000017AC
		public string ExtraDetails { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x000035B8 File Offset: 0x000017B8
		private static string ConvertToXMLSafeString(string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				int num = (int)c;
				if (num >= 32 && num <= 127)
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(' ');
				}
			}
			return stringBuilder.ToString();
		}
	}
}
