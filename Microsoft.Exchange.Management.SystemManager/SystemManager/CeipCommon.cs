using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000003 RID: 3
	public static class CeipCommon
	{
		// Token: 0x02000006 RID: 6
		public class IndustryTypeEnumListSource : EnumListSource
		{
			// Token: 0x06000018 RID: 24 RVA: 0x000022FB File Offset: 0x000004FB
			public IndustryTypeEnumListSource() : base(typeof(IndustryType))
			{
				base.Comparer = new CeipCommon.IndustryTypeEnumListSource.IndustryTypeComparer();
			}

			// Token: 0x06000019 RID: 25 RVA: 0x00002318 File Offset: 0x00000518
			protected override string GetValueText(object objectValue)
			{
				string text = base.GetValueText(objectValue);
				if (CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectValue, IndustryType.Other) || CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectValue, IndustryType.NotSpecified))
				{
					text = "-- " + text + " --";
				}
				return text;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x00002352 File Offset: 0x00000552
			private static bool CompareIndustryType(object objectValue, IndustryType industryType)
			{
				return (IndustryType)objectValue == industryType;
			}

			// Token: 0x02000007 RID: 7
			private class IndustryTypeComparer : IComparer
			{
				// Token: 0x0600001B RID: 27 RVA: 0x00002360 File Offset: 0x00000560
				public int Compare(object x, object y)
				{
					ObjectListSourceItem objectListSourceItem = x as ObjectListSourceItem;
					ObjectListSourceItem objectListSourceItem2 = y as ObjectListSourceItem;
					if (objectListSourceItem != null)
					{
						int num = objectListSourceItem.CompareTo(objectListSourceItem2);
						if (num != 0)
						{
							if (CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectListSourceItem.Value, IndustryType.NotSpecified) || CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectListSourceItem2.Value, IndustryType.Other))
							{
								num = -1;
							}
							else if (CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectListSourceItem.Value, IndustryType.Other) || CeipCommon.IndustryTypeEnumListSource.CompareIndustryType(objectListSourceItem2.Value, IndustryType.NotSpecified))
							{
								num = 1;
							}
						}
						return num;
					}
					if (objectListSourceItem2 != null)
					{
						return -1;
					}
					return 0;
				}
			}
		}
	}
}
