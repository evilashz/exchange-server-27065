using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000734 RID: 1844
	public interface IResponseShapeResolver
	{
		// Token: 0x060037AD RID: 14253
		T GetResponseShape<T>(string shapeName, T clientResponseShape, IFeaturesManager featuresManager = null) where T : ResponseShape;
	}
}
