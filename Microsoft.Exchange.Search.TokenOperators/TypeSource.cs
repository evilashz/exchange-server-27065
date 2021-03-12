using System;
using Microsoft.Ceres.ContentEngine.Fields.Stream;
using Microsoft.Ceres.CoreServices.Services.Container;
using Microsoft.Ceres.CoreServices.Services.Node;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200002C RID: 44
	[DynamicComponent]
	public class TypeSource : AbstractContainerManaged
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00006B30 File Offset: 0x00004D30
		public TypeSource()
		{
			StreamFactory singleton = StreamFactory.Singleton;
			singleton.Register("exchange", () => new ExchangeStream());
		}
	}
}
