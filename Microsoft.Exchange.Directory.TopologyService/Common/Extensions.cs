using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x02000025 RID: 37
	internal static class Extensions
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000A870 File Offset: 0x00008A70
		public static void TraceList<T>(this Trace trace, int lid, List<T> list, string message)
		{
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (!list.IsNullOrEmpty())
				{
					using (List<T>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							stringBuilder.AppendLine(t.ToString());
						}
						goto IL_61;
					}
				}
				stringBuilder.Append("<NullOrEmpty>");
				IL_61:
				trace.TraceDebug<string, string>((long)lid, "{0} | {1} |", message, stringBuilder.ToString());
			}
		}
	}
}
