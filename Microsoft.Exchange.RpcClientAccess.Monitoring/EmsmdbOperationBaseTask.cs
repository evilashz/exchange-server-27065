using System;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EmsmdbOperationBaseTask : BaseRpcTask
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000769C File Offset: 0x0000589C
		public EmsmdbOperationBaseTask(IContext context, LocalizedString title, LocalizedString description, TaskType type, params ContextProperty[] dependentProperties) : base(context, title, description, type, dependentProperties.Concat(new ContextProperty[]
		{
			ContextPropertySchema.ResponseHeaderCollection.SetOnly(),
			ContextPropertySchema.ResponseStatusCode.SetOnly(),
			ContextPropertySchema.ResponseStatusCodeDescription.SetOnly(),
			ContextPropertySchema.RequestHeaderCollection.SetOnly()
		}).ToArray<ContextProperty>())
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000076FC File Offset: 0x000058FC
		protected TaskResult ApplyCallResult(EmsmdbCallResult callResult)
		{
			base.Set<WebHeaderCollection>(ContextPropertySchema.RequestHeaderCollection, callResult.HttpRequestHeaders);
			base.Set<WebHeaderCollection>(ContextPropertySchema.ResponseHeaderCollection, callResult.HttpResponseHeaders);
			base.Set<HttpStatusCode>(ContextPropertySchema.ResponseStatusCode, callResult.HttpResponseStatusCode);
			base.Set<string>(ContextPropertySchema.ResponseStatusCodeDescription, callResult.HttpResponseStatusCodeDescription);
			return base.ApplyCallResult(callResult);
		}
	}
}
