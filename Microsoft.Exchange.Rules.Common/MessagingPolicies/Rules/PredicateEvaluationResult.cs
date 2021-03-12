using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000021 RID: 33
	internal class PredicateEvaluationResult
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000381E File Offset: 0x00001A1E
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003826 File Offset: 0x00001A26
		internal List<string> MatchResults { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000382F File Offset: 0x00001A2F
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003837 File Offset: 0x00001A37
		internal bool IsMatch { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003840 File Offset: 0x00001A40
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003848 File Offset: 0x00001A48
		internal int SupplementalInfo { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003851 File Offset: 0x00001A51
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003859 File Offset: 0x00001A59
		internal Type Type { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003862 File Offset: 0x00001A62
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000386A File Offset: 0x00001A6A
		internal string PropertyName { get; private set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00003873 File Offset: 0x00001A73
		internal PredicateEvaluationResult()
		{
			this.Type = typeof(object);
			this.MatchResults = new List<string>();
			this.SupplementalInfo = 0;
			this.IsMatch = false;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000038A4 File Offset: 0x00001AA4
		internal PredicateEvaluationResult(Type predicateType, bool isMatch, IEnumerable<string> matchingValues, int supplementalInfo)
		{
			this.IsMatch = isMatch;
			this.Type = predicateType;
			this.MatchResults = new List<string>(matchingValues);
			this.SupplementalInfo = supplementalInfo;
		}
	}
}
