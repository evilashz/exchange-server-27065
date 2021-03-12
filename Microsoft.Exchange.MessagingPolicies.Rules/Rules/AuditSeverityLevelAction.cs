using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005F RID: 95
	internal class AuditSeverityLevelAction : TransportAction
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00012D08 File Offset: 0x00010F08
		public AuditSeverityLevelAction(ShortList<Argument> arguments) : base(arguments)
		{
			string text = arguments[0].GetValue(null).ToString();
			if (Enum.TryParse<AuditSeverityLevel>(text, out this.severityLevel))
			{
				return;
			}
			if (text.Equals("Informational", StringComparison.OrdinalIgnoreCase))
			{
				this.severityLevel = AuditSeverityLevel.Low;
				return;
			}
			if (text.Equals("AuditOff", StringComparison.OrdinalIgnoreCase))
			{
				this.severityLevel = AuditSeverityLevel.DoNotAudit;
				return;
			}
			throw new RulesValidationException(TransportRulesStrings.InvalidAuditSeverityLevel(text));
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00012D75 File Offset: 0x00010F75
		public override Version MinimumVersion
		{
			get
			{
				return HasSenderOverridePredicate.ComplianceProgramsBaseVersion;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00012D7C File Offset: 0x00010F7C
		public override string Name
		{
			get
			{
				return "AuditSeverityLevel";
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00012D83 File Offset: 0x00010F83
		public override Type[] ArgumentsType
		{
			get
			{
				return AuditSeverityLevelAction.argumentTypes;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012D8C File Offset: 0x00010F8C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.CurrentAuditSeverityLevel = this.severityLevel;
			return ExecutionControl.Execute;
		}

		// Token: 0x04000210 RID: 528
		private static readonly Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};

		// Token: 0x04000211 RID: 529
		private readonly AuditSeverityLevel severityLevel;
	}
}
