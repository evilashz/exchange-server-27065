using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000C9 RID: 201
	internal static class HelperExtension
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00027E60 File Offset: 0x00026060
		internal static bool IsDebugOptionsEnabled(this AmConfig config)
		{
			bool result = false;
			if (!config.IsUnknown)
			{
				result = config.DbState.GetDebugOption<bool>(null, AmDebugOptions.Enabled, false);
			}
			return result;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00027E88 File Offset: 0x00026088
		internal static bool IsIgnoreServerDebugOptionEnabled(this AmConfig config, AmServerName serverName)
		{
			bool result = false;
			if (config.IsDebugOptionsEnabled())
			{
				result = config.DbState.GetDebugOption<bool>(serverName, AmDebugOptions.IgnoreServerFromAutomaticActions, false);
			}
			return result;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002803C File Offset: 0x0002623C
		internal static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
		{
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					yield return enumerator.YieldBatchElements(batchSize - 1);
				}
			}
			yield break;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00028194 File Offset: 0x00026394
		internal static IEnumerable<T> YieldBatchElements<T>(this IEnumerator<T> source, int batchSize)
		{
			yield return source.Current;
			int i = 0;
			while (i < batchSize && source.MoveNext())
			{
				yield return source.Current;
				i++;
			}
			yield break;
		}
	}
}
