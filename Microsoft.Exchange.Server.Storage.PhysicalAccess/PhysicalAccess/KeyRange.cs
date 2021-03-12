using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200006E RID: 110
	public struct KeyRange
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00017151 File Offset: 0x00015351
		public KeyRange(StartStopKey startKey, StartStopKey stopKey)
		{
			this.StartKey = startKey;
			this.StopKey = stopKey;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00017161 File Offset: 0x00015361
		public bool IsAllRows
		{
			get
			{
				return this.StartKey.IsEmpty && this.StopKey.IsEmpty;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017180 File Offset: 0x00015380
		internal static int CompareStartStart(StartStopKey key1, StartStopKey key2, SortOrder sortOrder, CompareInfo compareInfo, bool backwards)
		{
			int num = 0;
			if (key1.IsEmpty && key2.IsEmpty)
			{
				num = 0;
			}
			else if (key1.IsEmpty)
			{
				num = -1;
			}
			else if (key2.IsEmpty)
			{
				num = 1;
			}
			else
			{
				for (int i = 0; i < Math.Min(key1.Count, key2.Count); i++)
				{
					int num2 = ValueHelper.ValuesCompare(key1.Values[i], key2.Values[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
					if (num2 != 0)
					{
						num = (sortOrder.Ascending[i] ? num2 : (-num2));
						num = (backwards ? (-num) : num);
						break;
					}
				}
				if (num == 0)
				{
					if (key1.Count > key2.Count)
					{
						if (key2.Inclusive)
						{
							num = 1;
						}
						else
						{
							num = -1;
						}
					}
					else if (key1.Count < key2.Count)
					{
						if (key1.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
					else if (key1.Inclusive != key2.Inclusive)
					{
						if (!key1.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017294 File Offset: 0x00015494
		internal static int CompareStopStop(StartStopKey key1, StartStopKey key2, SortOrder sortOrder, CompareInfo compareInfo, bool backwards)
		{
			int num = 0;
			if (key1.IsEmpty && key2.IsEmpty)
			{
				num = 0;
			}
			else if (key1.IsEmpty)
			{
				num = 1;
			}
			else if (key2.IsEmpty)
			{
				num = -1;
			}
			else
			{
				for (int i = 0; i < Math.Min(key1.Count, key2.Count); i++)
				{
					int num2 = ValueHelper.ValuesCompare(key1.Values[i], key2.Values[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
					if (num2 != 0)
					{
						num = (sortOrder.Ascending[i] ? num2 : (-num2));
						num = (backwards ? (-num) : num);
						break;
					}
				}
				if (num == 0)
				{
					if (key1.Count > key2.Count)
					{
						if (key2.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
					else if (key1.Count < key2.Count)
					{
						if (key1.Inclusive)
						{
							num = 1;
						}
						else
						{
							num = -1;
						}
					}
					else if (key1.Inclusive != key2.Inclusive)
					{
						if (!key1.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000173A8 File Offset: 0x000155A8
		internal static int CompareStartStop(StartStopKey startKey, StartStopKey stopKey, SortOrder sortOrder, CompareInfo compareInfo, bool backwards)
		{
			int num = 0;
			if (startKey.IsEmpty && stopKey.IsEmpty)
			{
				num = 1;
			}
			else if (startKey.IsEmpty || stopKey.IsEmpty)
			{
				num = -1;
			}
			else
			{
				for (int i = 0; i < Math.Min(startKey.Count, stopKey.Count); i++)
				{
					int num2 = ValueHelper.ValuesCompare(startKey.Values[i], stopKey.Values[i], compareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
					if (num2 != 0)
					{
						num = (sortOrder.Ascending[i] ? num2 : (-num2));
						num = (backwards ? (-num) : num);
						break;
					}
				}
				if (num == 0)
				{
					if (startKey.Count > stopKey.Count)
					{
						if (stopKey.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
					else if (startKey.Count < stopKey.Count)
					{
						if (startKey.Inclusive)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
					}
					else if (!startKey.Inclusive || !stopKey.Inclusive)
					{
						num = 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000174D8 File Offset: 0x000156D8
		public static IList<KeyRange> Normalize(IList<KeyRange> ranges, SortOrder sortOrder, CompareInfo compareInfo, bool backwards)
		{
			if (ranges.Count == 1)
			{
				StartStopKey startKey = ranges[0].StartKey;
				if (!startKey.IsEmpty)
				{
					StartStopKey stopKey = ranges[0].StopKey;
					if (!stopKey.IsEmpty && KeyRange.CompareStartStop(ranges[0].StartKey, ranges[0].StopKey, sortOrder, compareInfo, backwards) > 0)
					{
						return new List<KeyRange>(0);
					}
				}
				return ranges;
			}
			List<KeyRange> list = new List<KeyRange>(ranges);
			int i;
			for (i = 0; i < list.Count; i++)
			{
				StartStopKey startKey2 = list[i].StartKey;
				if (!startKey2.IsEmpty)
				{
					StartStopKey stopKey2 = list[i].StopKey;
					if (!stopKey2.IsEmpty && KeyRange.CompareStartStop(list[i].StartKey, list[i].StopKey, sortOrder, compareInfo, backwards) > 0)
					{
						list.RemoveAt(i);
						continue;
					}
				}
			}
			list.Sort((KeyRange x, KeyRange y) => KeyRange.CompareStartStart(x.StartKey, y.StartKey, sortOrder, compareInfo, backwards));
			i = 0;
			while (i < list.Count - 1)
			{
				if (KeyRange.CompareStartStop(list[i + 1].StartKey, list[i].StopKey, sortOrder, compareInfo, backwards) <= 0)
				{
					if (KeyRange.CompareStopStop(list[i + 1].StopKey, list[i].StopKey, sortOrder, compareInfo, backwards) >= 0)
					{
						list[i] = new KeyRange(list[i].StartKey, list[i + 1].StopKey);
					}
					list.RemoveAt(i + 1);
				}
				else
				{
					i++;
				}
			}
			return list;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000176B8 File Offset: 0x000158B8
		public static IList<KeyRange> RemoveInaccessibleRanges(IList<KeyRange> ranges, StartStopKey startKey, StartStopKey stopKey, SortOrder sortOrder, CompareInfo compareInfo, bool backwards)
		{
			if (startKey.IsEmpty && stopKey.IsEmpty)
			{
				return KeyRange.Normalize(ranges, sortOrder, compareInfo, backwards);
			}
			List<KeyRange> list = new List<KeyRange>(ranges);
			int i = 0;
			while (i < list.Count)
			{
				if (KeyRange.CompareStartStop(startKey, list[i].StopKey, sortOrder, compareInfo, backwards) > 0 || KeyRange.CompareStartStop(list[i].StartKey, stopKey, sortOrder, compareInfo, backwards) > 0)
				{
					list.RemoveAt(i);
				}
				else if (KeyRange.CompareStartStart(list[i].StartKey, startKey, sortOrder, compareInfo, backwards) >= 0 && KeyRange.CompareStopStop(list[i].StopKey, stopKey, sortOrder, compareInfo, backwards) <= 0)
				{
					i++;
				}
				else if (KeyRange.CompareStartStart(list[i].StartKey, startKey, sortOrder, compareInfo, backwards) < 0)
				{
					if (KeyRange.CompareStopStop(list[i].StopKey, stopKey, sortOrder, compareInfo, backwards) > 0)
					{
						list[i] = new KeyRange(startKey, stopKey);
					}
					else
					{
						list[i] = new KeyRange(startKey, list[i].StopKey);
					}
					i++;
				}
				else
				{
					list[i] = new KeyRange(list[i].StartKey, stopKey);
					i++;
				}
			}
			return KeyRange.Normalize(list, sortOrder, compareInfo, backwards);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017808 File Offset: 0x00015A08
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendToStringBuilder(stringBuilder, StringFormatOptions.None);
			return stringBuilder.ToString();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001782C File Offset: 0x00015A2C
		internal void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions)
		{
			sb.Append("StartKey:[");
			this.StartKey.AppendToStringBuilder(sb, formatOptions);
			sb.Append("],");
			sb.Append("StopKey:[");
			this.StopKey.AppendToStringBuilder(sb, formatOptions);
			sb.Append("]");
		}

		// Token: 0x04000174 RID: 372
		public static readonly KeyRange AllRowsRange = new KeyRange(StartStopKey.Empty, StartStopKey.Empty);

		// Token: 0x04000175 RID: 373
		public static readonly IList<KeyRange> AllRows = new ReadOnlyCollection<KeyRange>(new List<KeyRange>
		{
			KeyRange.AllRowsRange
		});

		// Token: 0x04000176 RID: 374
		public StartStopKey StartKey;

		// Token: 0x04000177 RID: 375
		public StartStopKey StopKey;
	}
}
