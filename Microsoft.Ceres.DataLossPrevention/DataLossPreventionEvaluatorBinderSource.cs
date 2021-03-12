using System;
using Microsoft.Ceres.CoreServices.Services.Container;
using Microsoft.Ceres.CoreServices.Services.DependencyInjection;
using Microsoft.Ceres.CoreServices.Services.Node;
using Microsoft.Ceres.Evaluation.Processing;

namespace Microsoft.Ceres.DataLossPrevention
{
	// Token: 0x0200000F RID: 15
	[DynamicComponent]
	public class DataLossPreventionEvaluatorBinderSource : AbstractContainerManaged
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003B75 File Offset: 0x00001D75
		[Exposed]
		public IEvaluatorBinder DataLossPreventionEvaluatorBinder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly DataLossPreventionEvaluatorBinder binder = new DataLossPreventionEvaluatorBinder();
	}
}
