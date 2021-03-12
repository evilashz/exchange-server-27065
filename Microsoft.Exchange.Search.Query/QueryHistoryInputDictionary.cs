using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x0200000F RID: 15
	public class QueryHistoryInputDictionary : IEnumerable<QueryHistoryInputDictionaryEntry>, IEnumerable
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005D12 File Offset: 0x00003F12
		public void InitializeFrom(Stream stream)
		{
			this.ParseQueryHistoryInput(stream);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005D1C File Offset: 0x00003F1C
		public void SerializeTo(Stream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(1);
				lock (this.collectionLock)
				{
					binaryWriter.Write(this.sortedEntriesByQuery.Count);
					foreach (QueryHistoryInputDictionaryEntry queryHistoryInputDictionaryEntry in this.sortedEntriesByQuery.Values)
					{
						queryHistoryInputDictionaryEntry.SerializeTo(binaryWriter);
					}
				}
				stream.SetLength(stream.Position);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public void Merge(string query)
		{
			QueryHistoryInputDictionaryEntry entry = new QueryHistoryInputDictionaryEntry(query);
			this.Merge(entry);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005E00 File Offset: 0x00004000
		public void Merge(QueryHistoryInputDictionaryEntry entry)
		{
			lock (this.collectionLock)
			{
				QueryHistoryInputDictionaryEntry queryHistoryInputDictionaryEntry;
				if (this.sortedEntriesByQuery.TryGetValue(entry.Query, out queryHistoryInputDictionaryEntry))
				{
					queryHistoryInputDictionaryEntry.Rank = Math.Min(queryHistoryInputDictionaryEntry.Rank + 0.001, 1.0);
					queryHistoryInputDictionaryEntry.LastUsed = entry.LastUsed;
				}
				else
				{
					while (this.sortedEntriesByQuery.Count >= 1000)
					{
						string key = string.Empty;
						long num = DateTime.MaxValue.Ticks;
						foreach (QueryHistoryInputDictionaryEntry queryHistoryInputDictionaryEntry2 in this.sortedEntriesByQuery.Values)
						{
							if (queryHistoryInputDictionaryEntry2.LastUsed < num)
							{
								num = queryHistoryInputDictionaryEntry2.LastUsed;
								key = queryHistoryInputDictionaryEntry2.Query;
							}
						}
						this.sortedEntriesByQuery.Remove(key);
					}
					this.sortedEntriesByQuery.Add(entry.Query, entry);
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005F2C File Offset: 0x0000412C
		public bool Remove(string query)
		{
			bool result;
			lock (this.collectionLock)
			{
				result = this.sortedEntriesByQuery.Remove(query);
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005F74 File Offset: 0x00004174
		public void Clear()
		{
			lock (this.collectionLock)
			{
				this.sortedEntriesByQuery.Clear();
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005FBC File Offset: 0x000041BC
		public IEnumerator<QueryHistoryInputDictionaryEntry> GetEnumerator()
		{
			return this.sortedEntriesByQuery.Values.GetEnumerator();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005FD3 File Offset: 0x000041D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005FDC File Offset: 0x000041DC
		private void ParseQueryHistoryInput(Stream inputStream)
		{
			if (inputStream == null)
			{
				return;
			}
			if (inputStream.Length > 0L)
			{
				using (BinaryReader binaryReader = new BinaryReader(inputStream))
				{
					int num;
					try
					{
						num = binaryReader.ReadInt32();
					}
					catch (EndOfStreamException)
					{
						return;
					}
					if (num >= 1)
					{
						int num2;
						try
						{
							num2 = binaryReader.ReadInt32();
						}
						catch (EndOfStreamException)
						{
							return;
						}
						lock (this.collectionLock)
						{
							this.sortedEntriesByQuery.Clear();
							for (int i = 0; i < num2; i++)
							{
								QueryHistoryInputDictionaryEntry queryHistoryInputDictionaryEntry = new QueryHistoryInputDictionaryEntry();
								if (!queryHistoryInputDictionaryEntry.DeserializeFrom(binaryReader))
								{
									break;
								}
								this.sortedEntriesByQuery.Add(queryHistoryInputDictionaryEntry.Query, queryHistoryInputDictionaryEntry);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000079 RID: 121
		public const string Name = "Search.QueryHistoryInput";

		// Token: 0x0400007A RID: 122
		public const int CurrentSupportedVersion = 1;

		// Token: 0x0400007B RID: 123
		public const int MaximumTrackedQueries = 1000;

		// Token: 0x0400007C RID: 124
		public const double Increment = 0.001;

		// Token: 0x0400007D RID: 125
		private readonly SortedDictionary<string, QueryHistoryInputDictionaryEntry> sortedEntriesByQuery = new SortedDictionary<string, QueryHistoryInputDictionaryEntry>();

		// Token: 0x0400007E RID: 126
		private object collectionLock = new object();
	}
}
