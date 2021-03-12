using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Etw
{
	// Token: 0x02000024 RID: 36
	internal class GCPrivateEventsParser : IParser
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000063B0 File Offset: 0x000045B0
		public static Guid ProviderGuid
		{
			get
			{
				return GCPrivateEventsParser.providerGuid;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000063B8 File Offset: 0x000045B8
		public IEnumerable<Guid> Guids
		{
			get
			{
				return new Guid[]
				{
					GCPrivateEventsParser.providerGuid
				};
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000063E0 File Offset: 0x000045E0
		public unsafe void Parse(EtwTraceNativeComponents.EVENT_RECORD* rawData)
		{
			if (rawData->EventHeader.Opcode == 17)
			{
				GCPerHeapHistoryTraceEvent gcperHeapHistoryTraceEvent = new GCPerHeapHistoryTraceEvent(GCPrivateEventsParser.providerGuid, "Microsoft-Windows-DotNETRuntimePrivate", rawData);
				List<long[]> list;
				if (!this.processData.TryGetValue(rawData->EventHeader.ProcessId, out list))
				{
					list = new List<long[]>();
					this.processData.Add(rawData->EventHeader.ProcessId, list);
				}
				long[] array = new long[4];
				for (Gens gens = Gens.Gen0; gens <= Gens.GenLargeObj; gens++)
				{
					long fragmentation = gcperHeapHistoryTraceEvent.GenData(gens).Fragmentation;
					array[(int)gens] = fragmentation;
				}
				list.Add(array);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006474 File Offset: 0x00004674
		public Dictionary<int, long[][]> GetGenData()
		{
			Dictionary<int, long[][]> dictionary = new Dictionary<int, long[][]>();
			foreach (KeyValuePair<int, List<long[]>> keyValuePair in this.processData)
			{
				int num = 1;
				long[][] array = new long[num][];
				keyValuePair.Value.CopyTo(0, array, 0, num);
				dictionary.Add(keyValuePair.Key, array);
			}
			return dictionary;
		}

		// Token: 0x04000107 RID: 263
		private const string ProviderName = "Microsoft-Windows-DotNETRuntimePrivate";

		// Token: 0x04000108 RID: 264
		private const int PrivatePerHeapDataOpcode = 17;

		// Token: 0x04000109 RID: 265
		private static Guid providerGuid = new Guid(1983895380, 28806, 19966, 149, 235, 192, 26, 70, 250, 244, 202);

		// Token: 0x0400010A RID: 266
		private Dictionary<int, List<long[]>> processData = new Dictionary<int, List<long[]>>();
	}
}
