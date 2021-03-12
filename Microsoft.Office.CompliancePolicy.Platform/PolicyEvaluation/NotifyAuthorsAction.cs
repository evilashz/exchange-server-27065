using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000AB RID: 171
	public class NotifyAuthorsAction : NotifyActionBase
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public NotifyAuthorsAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
			this.OverrideOption = (RuleOverrideOptions)Enum.Parse(typeof(RuleOverrideOptions), ((Value)arguments[1]).RawValues[0]);
			if (arguments.Count == 3)
			{
				this.CustomizedContent = (string)arguments[2].GetValue(null);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000DA2B File Offset: 0x0000BC2B
		public override string Name
		{
			get
			{
				return "NotifyAuthors";
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000DA32 File Offset: 0x0000BC32
		public override Version MinimumVersion
		{
			get
			{
				return NotifyAuthorsAction.minVersion;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000DA39 File Offset: 0x0000BC39
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000DA41 File Offset: 0x0000BC41
		protected internal RuleOverrideOptions OverrideOption { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000DA52 File Offset: 0x0000BC52
		protected internal string CustomizedContent { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000DA5B File Offset: 0x0000BC5B
		protected internal string DefaultContent
		{
			get
			{
				return NotifyAuthorsAction.defaultContent;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public override void ValidateArguments(List<Argument> inputArguments)
		{
			base.ValidateArguments(inputArguments);
			if (inputArguments.Count < 2 || inputArguments.Count > 3)
			{
				throw new CompliancePolicyValidationException("Wrong argument count: Expected 2 or 3, but {0} actually. The - action '{1}'", new object[]
				{
					inputArguments.Count,
					this.Name
				});
			}
			Value value = inputArguments[1] as Value;
			if (value == null || value.ParsedValue == null)
			{
				throw new CompliancePolicyValidationException("Argument 'allow override' must not be empty - action '{0}'", new object[]
				{
					this.Name
				});
			}
			RuleOverrideOptions ruleOverrideOptions;
			if (!Enum.TryParse<RuleOverrideOptions>(value.RawValues[0], true, out ruleOverrideOptions))
			{
				throw new CompliancePolicyValidationException("Argument {0} has the wrong type - action '{1}'", new object[]
				{
					value.RawValues[0],
					this.Name
				});
			}
			if (inputArguments.Count != 3)
			{
				return;
			}
			value = (inputArguments[2] as Value);
			if (value == null || value.ParsedValue == null || !(value.ParsedValue is string))
			{
				throw new CompliancePolicyValidationException("Argument 'notify text' must not be empty - action '{0}'", new object[]
				{
					this.Name
				});
			}
			ArgumentValidator.ThrowIfNullOrWhiteSpace("NotifyText", (string)value.ParsedValue);
		}

		// Token: 0x040002C1 RID: 705
		private static readonly Version minVersion = new Version("1.00.0002.000");

		// Token: 0x040002C2 RID: 706
		private static readonly string defaultContent = "toBeDefinedContent-NotifyAuthors";
	}
}
