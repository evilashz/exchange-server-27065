using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000057 RID: 87
	internal class WildcardMatcher
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00009E15 File Offset: 0x00008015
		internal WildcardMatcher(string wildcardPattern) : this(new MultiValuedProperty<string>(wildcardPattern))
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009E24 File Offset: 0x00008024
		internal WildcardMatcher(MultiValuedProperty<string> wildcardPatterns)
		{
			this.regex = null;
			if (wildcardPatterns == null || wildcardPatterns.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string str in wildcardPatterns)
			{
				stringBuilder.Append("^" + Regex.Escape(str).Replace("\\*", ".*").Replace("\\?", ".") + "$|");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				this.regex = new Regex(stringBuilder.ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009EF0 File Offset: 0x000080F0
		internal bool IsMatch(string inputString)
		{
			return this.regex != null && inputString != null && this.regex.IsMatch(inputString);
		}

		// Token: 0x0400013E RID: 318
		private Regex regex;
	}
}
