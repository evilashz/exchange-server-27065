using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200007A RID: 122
	public struct StartStopKey
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00019620 File Offset: 0x00017820
		public StartStopKey(bool inclusive, IList<object> values)
		{
			this.values = values;
			this.inclusive = inclusive;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00019630 File Offset: 0x00017830
		public StartStopKey(bool inclusive, params object[] values)
		{
			this = new StartStopKey(inclusive, (IList<object>)values);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00019640 File Offset: 0x00017840
		public static void AppendKeyValuesToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, IList<object> keyValues)
		{
			int num = (keyValues != null) ? keyValues.Count : 0;
			for (int i = 0; i < num; i++)
			{
				if (i != 0)
				{
					sb.Append(", ");
				}
				sb.Append("[");
				if ((formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None)
				{
					if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || !(keyValues[i] is byte[]) || ((byte[])keyValues[i]).Length <= 32)
					{
						sb.AppendAsString(keyValues[i]);
					}
					else
					{
						sb.Append("<long blob>");
					}
				}
				else
				{
					sb.AppendAsString((keyValues[i] != null) ? "X" : "Null");
				}
				sb.Append("]");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000196F6 File Offset: 0x000178F6
		public bool IsEmpty
		{
			get
			{
				return this.values == null || this.values.Count == 0;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00019710 File Offset: 0x00017910
		public int Count
		{
			get
			{
				if (this.values != null)
				{
					return this.values.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00019727 File Offset: 0x00017927
		public IList<object> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001972F File Offset: 0x0001792F
		public bool Inclusive
		{
			get
			{
				return this.Count == 0 || this.inclusive;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00019741 File Offset: 0x00017941
		internal static int CommonKeyPrefix(StartStopKey startKey, StartStopKey stopKey, CompareInfo compareInfo)
		{
			return StartStopKey.CommonKeyPrefix(startKey.Values, stopKey.Values, compareInfo);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00019758 File Offset: 0x00017958
		internal static int CommonKeyPrefix(IList<object> first, IList<object> second, CompareInfo compareInfo)
		{
			if (!object.ReferenceEquals(first, second))
			{
				int num = Math.Min((first == null) ? 0 : first.Count, (second == null) ? 0 : second.Count);
				for (int i = 0; i < num; i++)
				{
					if (!ValueHelper.ValuesEqual(first[i], second[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth))
					{
						return i;
					}
				}
				return num;
			}
			if (first != null)
			{
				return first.Count;
			}
			return 0;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000197C0 File Offset: 0x000179C0
		internal void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions)
		{
			if (this.IsEmpty)
			{
				sb.Append("<Empty>");
				return;
			}
			sb.Append(this.inclusive ? "inclusive:[" : "exclusive:[");
			StartStopKey.AppendKeyValuesToStringBuilder(sb, formatOptions, this.values);
			sb.Append("]");
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019818 File Offset: 0x00017A18
		public new string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendToStringBuilder(stringBuilder, StringFormatOptions.IncludeDetails);
			return stringBuilder.ToString();
		}

		// Token: 0x0400019D RID: 413
		public static readonly StartStopKey Empty = default(StartStopKey);

		// Token: 0x0400019E RID: 414
		private readonly IList<object> values;

		// Token: 0x0400019F RID: 415
		private bool inclusive;
	}
}
