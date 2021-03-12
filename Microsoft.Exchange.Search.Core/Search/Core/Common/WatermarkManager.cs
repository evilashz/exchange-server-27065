using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000090 RID: 144
	internal abstract class WatermarkManager<T> where T : IComparable
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000C78B File Offset: 0x0000A98B
		protected WatermarkManager(int batchSize)
		{
			this.batchSize = batchSize;
			this.diagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("WatermarkManager", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreComponentTracer, (long)this.GetHashCode());
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		protected IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		protected bool TryFindNewWatermark(SortedDictionary<T, bool> stateUpdateList, out T newWatermark)
		{
			this.diagnosticsSession.TraceDebug<int>("TryFindNewWatermark for {0} items", stateUpdateList.Count);
			List<T> list = null;
			T t = default(T);
			foreach (KeyValuePair<T, bool> keyValuePair in stateUpdateList)
			{
				T key = keyValuePair.Key;
				if (!keyValuePair.Value)
				{
					this.diagnosticsSession.TraceDebug<T>("Document (Id={0}) has not completed", key);
					break;
				}
				if (list == null)
				{
					list = new List<T>(Math.Min(this.batchSize, stateUpdateList.Count));
				}
				list.Add(key);
				t = key;
			}
			if (list == null)
			{
				newWatermark = default(T);
				return false;
			}
			this.diagnosticsSession.TraceDebug<T, int>("Last Id = {0}, Count={1}", t, list.Count);
			if (list.Count == stateUpdateList.Count)
			{
				stateUpdateList.Clear();
				newWatermark = t;
				return true;
			}
			if (list.Count >= this.batchSize)
			{
				foreach (T key2 in list)
				{
					stateUpdateList.Remove(key2);
				}
				newWatermark = t;
				return true;
			}
			newWatermark = default(T);
			return false;
		}

		// Token: 0x040001A9 RID: 425
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040001AA RID: 426
		private readonly int batchSize;
	}
}
