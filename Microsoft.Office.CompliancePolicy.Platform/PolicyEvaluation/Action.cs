using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000A5 RID: 165
	public abstract class Action
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000D464 File Offset: 0x0000B664
		public Action(List<Argument> arguments, string externalName = null)
		{
			this.ValidateArguments(arguments);
			this.arguments = arguments;
			if (externalName != null)
			{
				this.ExternalName = externalName;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000436 RID: 1078
		public abstract string Name { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000D48F File Offset: 0x0000B68F
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000D4AB File Offset: 0x0000B6AB
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public bool HasExternalName
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.externalName) && !this.Name.Equals(this.externalName);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000D4D9 File Offset: 0x0000B6D9
		public virtual Version MinimumVersion
		{
			get
			{
				return PolicyRule.BaseVersion;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public IList<Argument> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public virtual Type[] ArgumentsType
		{
			get
			{
				return Action.DefaultArgumentsTypeList;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000D510 File Offset: 0x0000B710
		public virtual void ValidateArguments(List<Argument> inputArguments)
		{
			if (inputArguments == null)
			{
				throw new CompliancePolicyValidationException("Argument list is null - action '{0}'", new object[]
				{
					this.Name
				});
			}
			Type[] argumentsType = this.ArgumentsType;
			if (argumentsType.Length != inputArguments.Count)
			{
				throw new CompliancePolicyValidationException("Argument list mismatches - action '{0}'", new object[]
				{
					this.Name
				});
			}
			if (argumentsType.Where((Type t, int index) => t != inputArguments[index].Type).Any<Type>())
			{
				throw new CompliancePolicyValidationException("Argument list mismatches - action '{0}'", new object[]
				{
					this.Name
				});
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		public virtual bool ShouldExecute(RuleMode mode)
		{
			return RuleMode.Enforce == mode;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000D5BF File Offset: 0x0000B7BF
		public ExecutionControl Execute(PolicyEvaluationContext context)
		{
			return this.OnExecute(context);
		}

		// Token: 0x06000440 RID: 1088
		protected abstract ExecutionControl OnExecute(PolicyEvaluationContext context);

		// Token: 0x040002AF RID: 687
		internal static readonly Type[] DefaultArgumentsTypeList = new Type[0];

		// Token: 0x040002B0 RID: 688
		private List<Argument> arguments = new List<Argument>();

		// Token: 0x040002B1 RID: 689
		private string externalName;
	}
}
