using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200023B RID: 571
	internal class MapiVersionRanges
	{
		// Token: 0x060013AD RID: 5037 RVA: 0x0003C270 File Offset: 0x0003A470
		public MapiVersionRanges(string s)
		{
			if (s == null || s.Trim().Length == 0)
			{
				return;
			}
			string[] array = s.Split(MapiVersionRanges.separators);
			foreach (string s2 in array)
			{
				this.Ranges.Add(MapiVersionRanges.MapiVersionRange.Parse(s2));
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
		public bool IsIncluded(MapiVersion version)
		{
			foreach (MapiVersionRanges.MapiVersionRange mapiVersionRange in this.Ranges)
			{
				if (mapiVersionRange.IsInlcuded(version))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000B87 RID: 2951
		internal readonly List<MapiVersionRanges.MapiVersionRange> Ranges = new List<MapiVersionRanges.MapiVersionRange>();

		// Token: 0x04000B88 RID: 2952
		private static readonly char[] separators = new char[]
		{
			',',
			';'
		};

		// Token: 0x0200023C RID: 572
		public struct MapiVersionRange
		{
			// Token: 0x060013B0 RID: 5040 RVA: 0x0003C354 File Offset: 0x0003A554
			public static MapiVersionRanges.MapiVersionRange Parse(string s)
			{
				string[] array = s.Split(new char[]
				{
					'-'
				});
				string text;
				string text2;
				if (array.Length == 2)
				{
					text = array[0];
					text2 = array[1];
				}
				else
				{
					if (array.Length != 1)
					{
						throw new FormatException();
					}
					text2 = (text = array[0]);
				}
				bool flag = false;
				bool flag2 = false;
				MapiVersion start;
				if (text.Trim().Length == 0)
				{
					start = MapiVersion.Min;
				}
				else
				{
					start = MapiVersion.Parse(text);
					flag = true;
				}
				MapiVersion end;
				if (text2.Trim().Length == 0)
				{
					end = MapiVersion.Max;
				}
				else
				{
					end = MapiVersion.Parse(text2);
					flag2 = true;
				}
				if (!flag && !flag2)
				{
					throw new FormatException();
				}
				return new MapiVersionRanges.MapiVersionRange(start, end);
			}

			// Token: 0x060013B1 RID: 5041 RVA: 0x0003C3F8 File Offset: 0x0003A5F8
			private MapiVersionRange(MapiVersion start, MapiVersion end)
			{
				this.Start = start;
				this.End = end;
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x0003C408 File Offset: 0x0003A608
			public bool IsInlcuded(MapiVersion version)
			{
				return this.Start <= version && version <= this.End;
			}

			// Token: 0x04000B89 RID: 2953
			internal readonly MapiVersion Start;

			// Token: 0x04000B8A RID: 2954
			internal readonly MapiVersion End;
		}
	}
}
