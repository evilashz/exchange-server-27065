using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.MessagingPolicies;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000404 RID: 1028
	public abstract class RuleDataService : DataSourceService
	{
		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x060034AF RID: 13487
		public abstract int RuleNameMaxLength { get; }

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000A3870 File Offset: 0x000A1A70
		public string TaskNoun
		{
			get
			{
				return this.taskNoun;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x060034B1 RID: 13489
		public abstract RulePhrase[] SupportedConditions { get; }

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x060034B2 RID: 13490
		public abstract RulePhrase[] SupportedActions { get; }

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x060034B3 RID: 13491
		public abstract RulePhrase[] SupportedExceptions { get; }

		// Token: 0x060034B4 RID: 13492 RVA: 0x000A3878 File Offset: 0x000A1A78
		protected RuleDataService(string taskNoun)
		{
			this.taskNoun = taskNoun;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000A3888 File Offset: 0x000A1A88
		protected PowerShellResults ChangePriority<T>(Identity[] identities, int offset, WebServiceParameters parameters) where T : RuleRow
		{
			identities.FaultIfNotExactlyOne();
			PowerShellResults<T> @object = base.GetObject<T>("Get-" + this.TaskNoun, identities[0]);
			if (@object.Failed)
			{
				return @object;
			}
			int num = @object.Output[0].Priority + offset;
			if (num < 0)
			{
				return new PowerShellResults();
			}
			PSCommand pscommand = new PSCommand().AddCommand("Set-" + this.TaskNoun);
			pscommand.AddParameter("Priority", num);
			PowerShellResults powerShellResults = base.Invoke(pscommand, identities, parameters);
			if (powerShellResults.ErrorRecords != null && powerShellResults.ErrorRecords.Length == 1 && powerShellResults.ErrorRecords[0].Exception is InvalidPriorityException)
			{
				powerShellResults.ErrorRecords = new ErrorRecord[0];
			}
			return powerShellResults;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000A3980 File Offset: 0x000A1B80
		protected string GetUniqueRuleName(string ruleName, RuleRow[] existingRules)
		{
			if (string.IsNullOrEmpty(ruleName))
			{
				return ruleName;
			}
			int ruleNameMaxLength = this.RuleNameMaxLength;
			if (Array.FindIndex<RuleRow>(existingRules, (RuleRow x) => x.Name == ruleName) == -1 && ruleName.Length <= ruleNameMaxLength)
			{
				return ruleName;
			}
			string text = ruleName;
			string text2 = string.Empty;
			if (ruleName.EndsWith("..."))
			{
				text = ruleName.Substring(0, ruleName.Length - 3);
				text2 = "...";
			}
			StringBuilder stringBuilder = new StringBuilder(ruleNameMaxLength);
			if (ruleName.Length > ruleNameMaxLength)
			{
				stringBuilder.Append(text.SurrogateSubstring(0, ruleNameMaxLength - text2.Length));
				stringBuilder.Append(text2);
				ruleName = stringBuilder.ToString().TrimEnd(new char[0]);
			}
			int num = 2;
			for (;;)
			{
				if (Array.FindIndex<RuleRow>(existingRules, (RuleRow x) => x.Name == ruleName) < 0)
				{
					break;
				}
				string text3 = num.ToString();
				int num2 = 1 + (RtlUtil.IsRtl ? 1 : 0);
				if (text.Length + text2.Length + text3.Length + num2 > ruleNameMaxLength)
				{
					text = text.SurrogateSubstring(0, ruleNameMaxLength - text2.Length - text3.Length - num2);
				}
				stringBuilder.Length = 0;
				stringBuilder.Append(text);
				stringBuilder.Append(text2);
				stringBuilder.Append(' ');
				if (RtlUtil.IsRtl)
				{
					stringBuilder.Append(RtlUtil.DecodedRtlDirectionMark);
				}
				stringBuilder.Append(text3);
				ruleName = stringBuilder.ToString();
				num++;
			}
			return ruleName;
		}

		// Token: 0x04002526 RID: 9510
		internal const string Conditions = "Conditions";

		// Token: 0x04002527 RID: 9511
		internal const string Actions = "Actions";

		// Token: 0x04002528 RID: 9512
		internal const string Exceptions = "Exceptions";

		// Token: 0x04002529 RID: 9513
		private string taskNoun;
	}
}
