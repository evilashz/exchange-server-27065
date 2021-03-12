using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x02000110 RID: 272
	internal class ClientAccessRulesUsernamePatternProperty : Property
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0001DE98 File Offset: 0x0001C098
		public ClientAccessRulesUsernamePatternProperty(string propertyName, Type type) : base(propertyName, type)
		{
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		protected override object OnGetValue(RulesEvaluationContext baseContext)
		{
			ClientAccessRulesEvaluationContext clientAccessRulesEvaluationContext = (ClientAccessRulesEvaluationContext)baseContext;
			return clientAccessRulesEvaluationContext.UserName;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001DEC0 File Offset: 0x0001C0C0
		public static string GetDisplayValue(Regex regex)
		{
			string text = regex.ToString();
			text = text.Substring(1, text.Length - 2);
			text = text.Replace(".*", "*");
			return Regex.Unescape(text);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001DEFB File Offset: 0x0001C0FB
		public static Regex GetWildcardPatternRegex(string pattern)
		{
			pattern = Regex.Escape(pattern);
			pattern = pattern.Replace("\\*", ".*");
			return new Regex(string.Format("^{0}$", pattern), RegexOptions.IgnoreCase);
		}

		// Token: 0x040005F7 RID: 1527
		public const string PropertyName = "UsernamePatternProperty";
	}
}
