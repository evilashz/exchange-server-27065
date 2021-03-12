using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000331 RID: 817
	internal class GetWellKnownShapes : ServiceCommand<GetWellKnownShapesResponse>
	{
		// Token: 0x06001B17 RID: 6935 RVA: 0x00066C31 File Offset: 0x00064E31
		public GetWellKnownShapes(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00066C3C File Offset: 0x00064E3C
		protected override GetWellKnownShapesResponse InternalExecute()
		{
			List<string> list = new List<string>();
			List<ResponseShape> list2 = new List<ResponseShape>();
			foreach (KeyValuePair<WellKnownShapeName, ResponseShape> keyValuePair in WellKnownShapes.ResponseShapes)
			{
				list.Add(keyValuePair.Key.ToString());
				list2.Add(keyValuePair.Value);
			}
			return new GetWellKnownShapesResponse
			{
				ShapeNames = list.ToArray(),
				ResponseShapes = list2.ToArray()
			};
		}
	}
}
