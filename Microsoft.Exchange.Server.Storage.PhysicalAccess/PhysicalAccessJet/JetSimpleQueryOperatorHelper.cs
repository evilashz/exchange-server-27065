using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C2 RID: 194
	internal static class JetSimpleQueryOperatorHelper
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x000274E0 File Offset: 0x000256E0
		public static int GetCount(IJetSimpleQueryOperator jetQueryOperator)
		{
			int num = 0;
			int num2;
			bool flag = jetQueryOperator.MoveFirst(out num2);
			while (flag)
			{
				num++;
				flag = jetQueryOperator.MoveNext();
			}
			return num;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002750C File Offset: 0x0002570C
		public static int GetOrdinalPosition(IJetSimpleQueryOperator jetQueryOperator, SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			int num = 0;
			int num2;
			for (bool flag = jetQueryOperator.MoveFirst(out num2); flag && !JetSimpleQueryOperatorHelper.TestKeyValue(jetQueryOperator, sortOrder, stopKey, compareInfo); flag = jetQueryOperator.MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00027540 File Offset: 0x00025740
		private static bool TestKeyValue(IJetSimpleQueryOperator jetQueryOperator, SortOrder sortOrder, StartStopKey stopKey, CompareInfo compareInfo)
		{
			for (int i = 0; i < stopKey.Count; i++)
			{
				object columnValue = jetQueryOperator.GetColumnValue(sortOrder.Columns[i]);
				int num = ValueHelper.ValuesCompare(columnValue, stopKey.Values[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				if (!sortOrder.Ascending[i])
				{
					num = -num;
				}
				if (num < 0)
				{
					return false;
				}
				if (num > 0)
				{
					return true;
				}
			}
			return stopKey.Inclusive;
		}
	}
}
