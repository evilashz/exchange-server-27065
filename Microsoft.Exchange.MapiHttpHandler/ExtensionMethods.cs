using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExtensionMethods
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000C980 File Offset: 0x0000AB80
		internal static void DisposeIfPresent(this WorkBuffer[] workBuffers)
		{
			if (workBuffers != null)
			{
				foreach (WorkBuffer disposable in workBuffers)
				{
					Util.DisposeIfPresent(disposable);
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		internal static TimeSpan Bound(this TimeSpan timeSpan, TimeSpan minTime, TimeSpan maxTime)
		{
			TimeSpan timeSpan2 = timeSpan;
			if (timeSpan2 < minTime)
			{
				timeSpan2 = minTime;
			}
			if (timeSpan2 > maxTime)
			{
				timeSpan2 = maxTime;
			}
			return timeSpan2;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		internal static PropertyTag[] GetColumns(this PropertyValue[][] rowSet)
		{
			if (rowSet == null)
			{
				return null;
			}
			if (rowSet.Length > 0)
			{
				PropertyTag[] array = new PropertyTag[rowSet[0].Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = rowSet[0][i].PropertyTag;
					for (int j = 0; j < rowSet.Length; j++)
					{
						if (!rowSet[j][i].IsError)
						{
							array[i] = rowSet[j][i].PropertyTag;
							break;
						}
					}
				}
				return array;
			}
			return Array<PropertyTag>.Empty;
		}
	}
}
