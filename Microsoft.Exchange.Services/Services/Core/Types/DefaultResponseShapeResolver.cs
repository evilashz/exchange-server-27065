using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000735 RID: 1845
	public class DefaultResponseShapeResolver : IResponseShapeResolver
	{
		// Token: 0x060037AF RID: 14255 RVA: 0x000C5C09 File Offset: 0x000C3E09
		public T GetResponseShape<T>(string shapeName, T clientResponseShape, IFeaturesManager featuresManager = null) where T : ResponseShape
		{
			return clientResponseShape;
		}
	}
}
