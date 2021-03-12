using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000870 RID: 2160
	internal class QueryMatchResult
	{
		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x00132FBF File Offset: 0x001311BF
		// (set) Token: 0x06004A90 RID: 19088 RVA: 0x00132FC7 File Offset: 0x001311C7
		internal string QueryString { get; set; }

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x00132FD0 File Offset: 0x001311D0
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x00132FD8 File Offset: 0x001311D8
		internal string MatchingRuleId { get; set; }

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x00132FE1 File Offset: 0x001311E1
		// (set) Token: 0x06004A94 RID: 19092 RVA: 0x00132FE9 File Offset: 0x001311E9
		internal XElement MatchingRuleXElement { get; set; }

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06004A95 RID: 19093 RVA: 0x00132FF2 File Offset: 0x001311F2
		// (set) Token: 0x06004A96 RID: 19094 RVA: 0x00132FFA File Offset: 0x001311FA
		internal XElement MatchingResourceXElement { get; set; }
	}
}
