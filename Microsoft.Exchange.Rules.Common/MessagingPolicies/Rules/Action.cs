using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000002 RID: 2
	public abstract class Action
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Action(ShortList<Argument> arguments)
		{
			this.ValidateArguments(arguments);
			this.arguments = arguments;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		public abstract string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000210D File Offset: 0x0000030D
		public string ExternalName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.externalName))
				{
					return this.Name;
				}
				return this.externalName;
			}
			set
			{
				this.externalName = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002116 File Offset: 0x00000316
		public bool HasExternalName
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.externalName) && !this.Name.Equals(this.externalName);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000213B File Offset: 0x0000033B
		public virtual Version MinimumVersion
		{
			get
			{
				return Rule.BaseVersion;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002142 File Offset: 0x00000342
		public IList<Argument> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000214A File Offset: 0x0000034A
		public virtual Type[] ArgumentsType
		{
			get
			{
				return Action.DefaultArgumentsTypeList;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public virtual void ValidateArguments(ShortList<Argument> inputArguments)
		{
			Type[] argumentsType = this.ArgumentsType;
			if (argumentsType.Length != inputArguments.Count)
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
			if (argumentsType.Where((Type t, int index) => t != inputArguments[index].Type).Any<Type>())
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
			if (inputArguments.OfType<Value>().Any((Value value) => value.RawValues.Count > 1))
			{
				throw new RulesValidationException(RulesStrings.NoMultiValueForActionArgument);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000222A File Offset: 0x0000042A
		public virtual bool ShouldExecute(RuleMode mode, RulesEvaluationContext context)
		{
			return RuleMode.Enforce == mode && context.ShouldExecuteActions;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002238 File Offset: 0x00000438
		public virtual int GetEstimatedSize()
		{
			int num = 18;
			if (this.arguments != null)
			{
				num += 18;
				foreach (Argument argument in this.arguments)
				{
					num += argument.GetEstimatedSize();
				}
			}
			if (this.externalName != null)
			{
				num += this.externalName.Length * 2;
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B8 File Offset: 0x000004B8
		public ExecutionControl Execute(RulesEvaluationContext context)
		{
			ExecutionControl result = this.OnExecute(context);
			this.LogActionExecution(context);
			return result;
		}

		// Token: 0x0600000D RID: 13
		protected abstract ExecutionControl OnExecute(RulesEvaluationContext context);

		// Token: 0x0600000E RID: 14 RVA: 0x000022D8 File Offset: 0x000004D8
		private void LogActionExecution(RulesEvaluationContext context)
		{
			if (context.NeedsLogging)
			{
				StringBuilder stringBuilder = new StringBuilder(this.Arguments.Count * 10);
				for (int i = 0; i < this.Arguments.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(this.Arguments[i].GetValue(context).ToString());
				}
				context.LogActionExecution(this.Name, stringBuilder.ToString());
			}
		}

		// Token: 0x04000001 RID: 1
		internal static readonly Type[] DefaultArgumentsTypeList = new Type[0];

		// Token: 0x04000002 RID: 2
		private ShortList<Argument> arguments = new ShortList<Argument>();

		// Token: 0x04000003 RID: 3
		private string externalName;
	}
}
