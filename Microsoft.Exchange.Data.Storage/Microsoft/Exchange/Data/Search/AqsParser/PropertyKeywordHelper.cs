using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CF3 RID: 3315
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PropertyKeywordHelper
	{
		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06007248 RID: 29256 RVA: 0x001F9ADA File Offset: 0x001F7CDA
		// (set) Token: 0x06007249 RID: 29257 RVA: 0x001F9AE1 File Offset: 0x001F7CE1
		public static HashSet<PropertyKeyword> AllPropertyKeywords { get; private set; } = new HashSet<PropertyKeyword>((PropertyKeyword[])Enum.GetValues(typeof(PropertyKeyword)));

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x0600724A RID: 29258 RVA: 0x001F9AE9 File Offset: 0x001F7CE9
		// (set) Token: 0x0600724B RID: 29259 RVA: 0x001F9AF0 File Offset: 0x001F7CF0
		public static HashSet<PropertyKeyword> CiPropertyKeywords { get; private set; }

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x001F9AF8 File Offset: 0x001F7CF8
		// (set) Token: 0x0600724D RID: 29261 RVA: 0x001F9AFF File Offset: 0x001F7CFF
		public static HashSet<PropertyKeyword> BasicPropertyKeywords { get; private set; }

		// Token: 0x0600724E RID: 29262 RVA: 0x001F9B70 File Offset: 0x001F7D70
		static PropertyKeywordHelper()
		{
			IEnumerable<PropertyKeyword> collection = from x in typeof(PropertyKeyword).GetTypeInfo().DeclaredFields
			where x.IsPublic && x.GetCustomAttributes(typeof(CIKeyword), false).Count<object>() > 0
			select (PropertyKeyword)x.GetValue(null);
			PropertyKeywordHelper.CiPropertyKeywords = new HashSet<PropertyKeyword>(collection);
			IEnumerable<PropertyKeyword> collection2 = from x in typeof(PropertyKeyword).GetTypeInfo().DeclaredFields
			where x.IsPublic && x.GetCustomAttributes(typeof(BasicKeyword), false).Count<object>() > 0
			select (PropertyKeyword)x.GetValue(null);
			PropertyKeywordHelper.BasicPropertyKeywords = new HashSet<PropertyKeyword>(collection2);
		}
	}
}
