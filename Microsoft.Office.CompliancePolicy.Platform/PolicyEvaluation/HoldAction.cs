using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000AA RID: 170
	public class HoldAction : Action
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		public HoldAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
			if (arguments == null)
			{
				throw new CompliancePolicyValidationException("Argument list is null");
			}
			string text = arguments[0].GetValue(null).ToString();
			int num;
			this.HoldDurationDays = ((!int.TryParse(text, out num)) ? int.MaxValue : num);
			if (arguments.Count == 2)
			{
				text = arguments[1].GetValue(null).ToString();
				HoldDurationHint holdDurationHint;
				this.HoldDurationDisplayHint = ((!Enum.TryParse<HoldDurationHint>(text, out holdDurationHint)) ? HoldDurationHint.Days : holdDurationHint);
				return;
			}
			this.HoldDurationDisplayHint = HoldDurationHint.Days;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000D852 File Offset: 0x0000BA52
		public override string Name
		{
			get
			{
				return "Hold";
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000D859 File Offset: 0x0000BA59
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000D861 File Offset: 0x0000BA61
		public HoldDurationHint HoldDurationDisplayHint { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000D86A File Offset: 0x0000BA6A
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000D872 File Offset: 0x0000BA72
		public int HoldDurationDays { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000D87B File Offset: 0x0000BA7B
		public override Type[] ArgumentsType
		{
			get
			{
				return HoldAction.argumentTypes;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public override void ValidateArguments(List<Argument> inputArguments)
		{
			if (inputArguments == null)
			{
				throw new CompliancePolicyValidationException("Argument list is null - action '{0}'", new object[]
				{
					this.Name
				});
			}
			if (inputArguments.Count == this.ArgumentsType.Count<Type>())
			{
				if (!this.ArgumentsType.SequenceEqual(from x in inputArguments
				select x.Type))
				{
					throw new CompliancePolicyValidationException("Argument list mismatches - action '{0}'", new object[]
					{
						this.Name
					});
				}
			}
			else
			{
				if (inputArguments.Count != this.ArgumentsType.Count<Type>() - 1)
				{
					throw new CompliancePolicyValidationException("Argument list mismatches - action '{0}'", new object[]
					{
						this.Name
					});
				}
				if (this.ArgumentsType.First<Type>() != inputArguments.First<Argument>().Type)
				{
					throw new CompliancePolicyValidationException("Argument list mismatches - action '{0}'", new object[]
					{
						this.Name
					});
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000D984 File Offset: 0x0000BB84
		protected override ExecutionControl OnExecute(PolicyEvaluationContext context)
		{
			throw new NotImplementedException("The HoldAction can only be used for PS object model serialization. Workloads must implement OnExecute.");
		}

		// Token: 0x040002BB RID: 699
		public const string ActionName = "Hold";

		// Token: 0x040002BC RID: 700
		internal const int PermanentHoldDurationValue = 0;

		// Token: 0x040002BD RID: 701
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
