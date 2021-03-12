using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class RuleDescription
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000042C9 File Offset: 0x000024C9
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000042D1 File Offset: 0x000024D1
		internal string ActivationDescription { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000042DA File Offset: 0x000024DA
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000042E2 File Offset: 0x000024E2
		internal string ExpiryDescription { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000042EB File Offset: 0x000024EB
		internal List<string> ActionDescriptions
		{
			get
			{
				return this.actionDescriptions;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000042F3 File Offset: 0x000024F3
		internal List<string> ConditionDescriptions
		{
			get
			{
				return this.conditionDescriptions;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000042FB File Offset: 0x000024FB
		internal List<string> ExceptionDescriptions
		{
			get
			{
				return this.exceptionDescriptions;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004303 File Offset: 0x00002503
		internal virtual string RuleDescriptionIf
		{
			get
			{
				return RulesStrings.RuleDescriptionIf;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000430F File Offset: 0x0000250F
		internal virtual string RuleDescriptionTakeActions
		{
			get
			{
				return RulesStrings.RuleDescriptionTakeActions;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000431B File Offset: 0x0000251B
		internal virtual string RuleDescriptionExceptIf
		{
			get
			{
				return RulesStrings.RuleDescriptionExceptIf;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004327 File Offset: 0x00002527
		internal virtual string RuleDescriptionActivation
		{
			get
			{
				return RulesStrings.RuleDescriptionActivation;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004333 File Offset: 0x00002533
		internal virtual string RuleDescriptionExpiry
		{
			get
			{
				return RulesStrings.RuleDescriptionExpiry;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004340 File Offset: 0x00002540
		public static string BuildDescriptionStringFromStringArray(ICollection<string> stringValues, string delimiter, int maxDescriptionLength = 200)
		{
			if (stringValues == null || stringValues.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(40 * stringValues.Count);
			stringBuilder.Append("'");
			bool flag = true;
			foreach (string value in stringValues)
			{
				if (stringBuilder.Length > maxDescriptionLength)
				{
					stringBuilder.Append(delimiter);
					stringBuilder.Append("...");
					break;
				}
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(delimiter);
					stringBuilder.Append(" '");
				}
				stringBuilder.Append(value);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004410 File Offset: 0x00002610
		public override string ToString()
		{
			int num = this.ConditionDescriptions.Count + this.ActionDescriptions.Count + this.ExceptionDescriptions.Count + (string.IsNullOrEmpty(this.ActivationDescription) ? 0 : 1) + (string.IsNullOrEmpty(this.ExpiryDescription) ? 0 : 1);
			StringBuilder stringBuilder = new StringBuilder(num * 50);
			if (this.ConditionDescriptions.Any<string>())
			{
				stringBuilder.Append(this.RuleDescriptionIf);
				stringBuilder.Append("\r\n");
				this.BuildDescriptionStrings(stringBuilder, this.ConditionDescriptions, RulesStrings.RuleDescriptionAndDelimiter);
			}
			if (this.ActionDescriptions.Any<string>())
			{
				stringBuilder.Append(this.RuleDescriptionTakeActions);
				stringBuilder.Append("\r\n");
				this.BuildDescriptionStrings(stringBuilder, this.ActionDescriptions, RulesStrings.RuleDescriptionAndDelimiter);
			}
			if (this.ExceptionDescriptions.Any<string>())
			{
				stringBuilder.Append(this.RuleDescriptionExceptIf);
				stringBuilder.Append("\r\n");
				this.BuildDescriptionStrings(stringBuilder, this.ExceptionDescriptions, RulesStrings.RuleDescriptionOrDelimiter);
			}
			if (!string.IsNullOrEmpty(this.ActivationDescription))
			{
				stringBuilder.Append(this.RuleDescriptionActivation);
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ActivationDescription);
				stringBuilder.Append("\r\n");
			}
			if (!string.IsNullOrEmpty(this.ExpiryDescription))
			{
				stringBuilder.Append(this.RuleDescriptionExpiry);
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ExpiryDescription);
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000045AC File Offset: 0x000027AC
		private void BuildDescriptionStrings(StringBuilder sb, List<string> descriptions, string delimiter)
		{
			if (descriptions == null || descriptions.Count == 0)
			{
				return;
			}
			bool flag = true;
			foreach (string value in descriptions)
			{
				sb.Append("\t");
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(delimiter);
					sb.Append(" ");
				}
				sb.Append(value);
				sb.Append("\r\n");
			}
		}

		// Token: 0x0400008F RID: 143
		internal const string CrLfString = "\r\n";

		// Token: 0x04000090 RID: 144
		internal const string TabString = "\t";

		// Token: 0x04000091 RID: 145
		internal const string SpaceString = " ";

		// Token: 0x04000092 RID: 146
		public const int MaxDescriptionLength = 200;

		// Token: 0x04000093 RID: 147
		private readonly List<string> actionDescriptions = new List<string>();

		// Token: 0x04000094 RID: 148
		private readonly List<string> conditionDescriptions = new List<string>();

		// Token: 0x04000095 RID: 149
		private readonly List<string> exceptionDescriptions = new List<string>();
	}
}
