using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B81 RID: 2945
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EwsWebHttpBehavior : WebHttpBehavior
	{
		// Token: 0x060055D9 RID: 21977 RVA: 0x00110AB1 File Offset: 0x0010ECB1
		protected override QueryStringConverter GetQueryStringConverter(OperationDescription operationDescription)
		{
			return new EwsWebQueryStringConverter(base.GetQueryStringConverter(operationDescription));
		}
	}
}
