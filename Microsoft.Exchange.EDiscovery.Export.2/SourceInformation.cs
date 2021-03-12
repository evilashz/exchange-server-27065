using System;
using System.IO;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200001F RID: 31
	internal class SourceInformation
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00004FD6 File Offset: 0x000031D6
		public SourceInformation(string name, string id, string sourceFilter, Uri serviceEndpoint, string legacyExchangeDN)
		{
			this.Configuration = new SourceInformation.SourceConfiguration(name, id, sourceFilter, serviceEndpoint, legacyExchangeDN);
			this.Status = new SourceInformation.SourceStatus();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004FFB File Offset: 0x000031FB
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005003 File Offset: 0x00003203
		public ISourceDataProvider ServiceClient { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000500C File Offset: 0x0000320C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00005014 File Offset: 0x00003214
		public SourceInformation.SourceConfiguration Configuration { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000501D File Offset: 0x0000321D
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005025 File Offset: 0x00003225
		public SourceInformation.SourceStatus Status { get; set; }

		// Token: 0x02000020 RID: 32
		[Serializable]
		public class SourceConfiguration
		{
			// Token: 0x06000117 RID: 279 RVA: 0x0000502E File Offset: 0x0000322E
			public SourceConfiguration(string name, string id, string sourceFilter, Uri serviceEndpoint, string legacyExchangeDN)
			{
				this.Name = name;
				this.Id = id;
				this.SourceFilter = sourceFilter;
				this.ServiceEndpoint = serviceEndpoint;
				this.LegacyExchangeDN = legacyExchangeDN;
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000118 RID: 280 RVA: 0x0000505B File Offset: 0x0000325B
			// (set) Token: 0x06000119 RID: 281 RVA: 0x00005063 File Offset: 0x00003263
			public string Name { get; private set; }

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600011A RID: 282 RVA: 0x0000506C File Offset: 0x0000326C
			// (set) Token: 0x0600011B RID: 283 RVA: 0x00005074 File Offset: 0x00003274
			public string Id { get; private set; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x0600011C RID: 284 RVA: 0x0000507D File Offset: 0x0000327D
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00005085 File Offset: 0x00003285
			public string SourceFilter { get; private set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600011E RID: 286 RVA: 0x0000508E File Offset: 0x0000328E
			// (set) Token: 0x0600011F RID: 287 RVA: 0x00005096 File Offset: 0x00003296
			public Uri ServiceEndpoint { get; internal set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000120 RID: 288 RVA: 0x0000509F File Offset: 0x0000329F
			// (set) Token: 0x06000121 RID: 289 RVA: 0x000050A7 File Offset: 0x000032A7
			public string LegacyExchangeDN { get; private set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000122 RID: 290 RVA: 0x000050B0 File Offset: 0x000032B0
			// (set) Token: 0x06000123 RID: 291 RVA: 0x000050B8 File Offset: 0x000032B8
			internal string SearchName { get; set; }
		}

		// Token: 0x02000022 RID: 34
		[Serializable]
		public class SourceStatus : ISourceStatus
		{
			// Token: 0x0600012E RID: 302 RVA: 0x000050C1 File Offset: 0x000032C1
			public SourceStatus()
			{
				this.ItemCount = -1;
				this.UnsearchableItemCount = -1;
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600012F RID: 303 RVA: 0x000050D7 File Offset: 0x000032D7
			// (set) Token: 0x06000130 RID: 304 RVA: 0x000050DF File Offset: 0x000032DF
			public int ProcessedItemCount { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000131 RID: 305 RVA: 0x000050E8 File Offset: 0x000032E8
			// (set) Token: 0x06000132 RID: 306 RVA: 0x000050F0 File Offset: 0x000032F0
			public int ItemCount { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x06000133 RID: 307 RVA: 0x000050F9 File Offset: 0x000032F9
			// (set) Token: 0x06000134 RID: 308 RVA: 0x00005101 File Offset: 0x00003301
			public long TotalSize { get; set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000135 RID: 309 RVA: 0x0000510A File Offset: 0x0000330A
			// (set) Token: 0x06000136 RID: 310 RVA: 0x00005112 File Offset: 0x00003312
			public int ProcessedUnsearchableItemCount { get; set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000137 RID: 311 RVA: 0x0000511B File Offset: 0x0000331B
			// (set) Token: 0x06000138 RID: 312 RVA: 0x00005123 File Offset: 0x00003323
			public int UnsearchableItemCount { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000139 RID: 313 RVA: 0x0000512C File Offset: 0x0000332C
			// (set) Token: 0x0600013A RID: 314 RVA: 0x00005134 File Offset: 0x00003334
			public long DuplicateItemCount { get; set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600013B RID: 315 RVA: 0x0000513D File Offset: 0x0000333D
			// (set) Token: 0x0600013C RID: 316 RVA: 0x00005145 File Offset: 0x00003345
			public long UnsearchableDuplicateItemCount { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x0600013D RID: 317 RVA: 0x0000514E File Offset: 0x0000334E
			// (set) Token: 0x0600013E RID: 318 RVA: 0x00005156 File Offset: 0x00003356
			public long ErrorItemCount { get; set; }

			// Token: 0x0600013F RID: 319 RVA: 0x0000515F File Offset: 0x0000335F
			public bool IsSearchCompleted(bool includeSearchableItems, bool includeUnsearchableItems)
			{
				return (this.ItemCount >= 0 || !includeSearchableItems) && (this.UnsearchableItemCount >= 0 || !includeUnsearchableItems);
			}

			// Token: 0x06000140 RID: 320 RVA: 0x0000517E File Offset: 0x0000337E
			public bool IsExportCompleted(bool includeSearchableItems, bool includeUnsearchableItems)
			{
				return ((this.ItemCount >= 0 && this.ItemCount <= this.ProcessedItemCount) || !includeSearchableItems) && ((this.UnsearchableItemCount >= 0 && this.UnsearchableItemCount <= this.ProcessedUnsearchableItemCount) || !includeUnsearchableItems);
			}

			// Token: 0x06000141 RID: 321 RVA: 0x000051BC File Offset: 0x000033BC
			public void SaveToStream(Stream stream)
			{
				stream.Write(BitConverter.GetBytes(this.ProcessedItemCount), 0, 4);
				stream.Write(BitConverter.GetBytes(this.ItemCount), 0, 4);
				stream.Write(BitConverter.GetBytes(this.TotalSize), 0, 8);
				stream.Write(BitConverter.GetBytes(this.ProcessedUnsearchableItemCount), 0, 4);
				stream.Write(BitConverter.GetBytes(this.UnsearchableItemCount), 0, 4);
				stream.Write(BitConverter.GetBytes(this.DuplicateItemCount), 0, 8);
				stream.Write(BitConverter.GetBytes(this.UnsearchableDuplicateItemCount), 0, 8);
				stream.Write(BitConverter.GetBytes(this.ErrorItemCount), 0, 8);
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00005264 File Offset: 0x00003464
			public void LoadFromStream(Stream stream)
			{
				byte[] array = new byte[8];
				SourceInformation.SourceStatus.SafeRead(stream, array, 4);
				this.ProcessedItemCount = BitConverter.ToInt32(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 4);
				this.ItemCount = BitConverter.ToInt32(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 8);
				this.TotalSize = BitConverter.ToInt64(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 4);
				this.ProcessedUnsearchableItemCount = BitConverter.ToInt32(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 4);
				this.UnsearchableItemCount = BitConverter.ToInt32(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 8);
				this.DuplicateItemCount = BitConverter.ToInt64(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 8);
				this.UnsearchableDuplicateItemCount = BitConverter.ToInt64(array, 0);
				SourceInformation.SourceStatus.SafeRead(stream, array, 8);
				this.ErrorItemCount = BitConverter.ToInt64(array, 0);
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00005320 File Offset: 0x00003520
			private static void SafeRead(Stream stream, byte[] buffer, int length)
			{
				int num = stream.Read(buffer, 0, length);
				if (num != length)
				{
					throw new ExportException(ExportErrorType.CorruptedStatus);
				}
			}
		}
	}
}
