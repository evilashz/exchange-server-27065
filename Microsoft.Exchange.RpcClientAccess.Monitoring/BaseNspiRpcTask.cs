using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseNspiRpcTask : BaseRpcTask
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00006ECB File Offset: 0x000050CB
		public BaseNspiRpcTask(IContext context, LocalizedString title, LocalizedString description, TaskType type, params ContextProperty[] dependentProperties) : base(context, title, description, type, dependentProperties)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006EDA File Offset: 0x000050DA
		protected TaskResult ApplyCallResult(NspiCallResult callResult)
		{
			if (callResult.NspiException != null)
			{
				base.Set<NspiDataException>(ContextPropertySchema.Exception, callResult.NspiException);
			}
			return base.ApplyCallResult(callResult);
		}
	}
}
