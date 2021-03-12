using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000A7 RID: 167
	public abstract class NotifyActionBase : Action
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000D60C File Offset: 0x0000B80C
		public NotifyActionBase(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
			List<string> list = (List<string>)arguments[0].GetValue(null);
			this.RecipientList = new ReadOnlyCollection<string>(list);
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000D640 File Offset: 0x0000B840
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000D648 File Offset: 0x0000B848
		protected internal ReadOnlyCollection<string> RecipientList { get; private set; }

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D654 File Offset: 0x0000B854
		public override void ValidateArguments(List<Argument> inputArguments)
		{
			if (inputArguments == null || !inputArguments.Any<Argument>())
			{
				throw new CompliancePolicyValidationException("Argument list is empty - action '{0}'", new object[]
				{
					this.Name
				});
			}
			NotifyActionBase.ValidateStringListArgument(inputArguments[0], "recipient list", this.Name);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D69F File Offset: 0x0000B89F
		protected override ExecutionControl OnExecute(PolicyEvaluationContext context)
		{
			throw new NotImplementedException("The NotifyActionBase-based actions can only be used for PS object model serialization. Workloads must implement OnExecute.");
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		private static void ValidateStringListArgument(Argument argument, string parameterName, string actionName)
		{
			Value value = argument as Value;
			if (value == null || value.ParsedValue == null)
			{
				throw new CompliancePolicyValidationException("Argument '{0}' must not be empty - action '{1}'", new object[]
				{
					parameterName,
					actionName
				});
			}
			List<string> list = value.ParsedValue as List<string>;
			if (list == null)
			{
				throw new CompliancePolicyValidationException("Argument '{0}' has the wrong type - action '{1}'", new object[]
				{
					parameterName,
					actionName
				});
			}
			if (list.Any<string>())
			{
				if (!list.Any((string p) => string.IsNullOrWhiteSpace(p)))
				{
					return;
				}
			}
			throw new CompliancePolicyValidationException("Argument '{0}' must not be empty or contain empty items - action '{1}'", new object[]
			{
				parameterName,
				actionName
			});
		}
	}
}
