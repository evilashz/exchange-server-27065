using System;
using System.IO;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000010 RID: 16
	public class QueryHistoryInputDictionaryEntry
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000060DA File Offset: 0x000042DA
		public QueryHistoryInputDictionaryEntry()
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000060E4 File Offset: 0x000042E4
		public QueryHistoryInputDictionaryEntry(string query)
		{
			this.Rank = 0.001;
			this.LastUsed = DateTime.UtcNow.Ticks;
			this.Query = query.Trim();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006125 File Offset: 0x00004325
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000612D File Offset: 0x0000432D
		public string Query { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006136 File Offset: 0x00004336
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000613E File Offset: 0x0000433E
		public double Rank { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006147 File Offset: 0x00004347
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000614F File Offset: 0x0000434F
		public long LastUsed { get; set; }

		// Token: 0x06000108 RID: 264 RVA: 0x00006158 File Offset: 0x00004358
		public void SerializeTo(BinaryWriter writer)
		{
			writer.Write(this.Query);
			writer.Write(this.Rank);
			writer.Write(this.LastUsed);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006180 File Offset: 0x00004380
		public bool DeserializeFrom(BinaryReader reader)
		{
			bool result = false;
			try
			{
				this.Query = reader.ReadString();
				this.Rank = reader.ReadDouble();
				this.LastUsed = reader.ReadInt64();
				result = true;
			}
			catch (EndOfStreamException)
			{
			}
			return result;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000061CC File Offset: 0x000043CC
		public override bool Equals(object obj)
		{
			return this.Query.Equals(((QueryHistoryInputDictionaryEntry)obj).Query, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000061E5 File Offset: 0x000043E5
		public override int GetHashCode()
		{
			return this.Query.GetHashCode();
		}
	}
}
