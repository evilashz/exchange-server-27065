using System;
using Microsoft.Exchange.Cluster.Common.ConfigurableParameters;
using Microsoft.Exchange.Cluster.Common.Extensions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000285 RID: 645
	internal class AutoReseedWorkflowStateDefinitions : ConfigurableParameterDefinitionsBase
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0006767C File Offset: 0x0006587C
		private AutoReseedWorkflowStateDefinitions() : base(10, Assert.Instance)
		{
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0006768C File Offset: 0x0006588C
		protected override void DefineParameters()
		{
			base.DefineParameter(new StringConfigurableParameter("AssignedVolumeName", string.Empty, 0, 2));
			base.DefineParameter(new EnumConfigurableParameter<ReseedState>("LastReseedRecoveryAction", ReseedState.Unknown, 0, 2));
			base.DefineParameter(new BooleanConfigurableParameter("IgnoreInPlaceOverwriteDelay", false, 0, 2));
			base.DefineParameter(new Int32ConfigurableParameter("ReseedRecoveryActionRetryCount", 0, 0, 2, null, null));
			base.DefineParameter(new StringConfigurableParameter("WorkflowExecutionError", string.Empty, 0, 2));
			base.DefineParameter(new EnumConfigurableParameter<AutoReseedWorkflowExecutionResult>("WorkflowExecutionResult2", AutoReseedWorkflowExecutionResult.Unknown, 0, 2));
			base.DefineParameter(new DateTimeConfigurableParameter("WorkflowExecutionTime", DateTime.MinValue, 0, 2));
			base.DefineParameter(new DateTimeConfigurableParameter("WorkflowNextExecutionTime", DateTime.MinValue, 0, 2));
			base.DefineParameter(new BooleanConfigurableParameter("WorkflowInfoIsValid", false, 0, 2));
		}

		// Token: 0x04000A08 RID: 2568
		private const int ParameterCapacity = 10;

		// Token: 0x04000A09 RID: 2569
		public static AutoReseedWorkflowStateDefinitions Instance = new AutoReseedWorkflowStateDefinitions();
	}
}
