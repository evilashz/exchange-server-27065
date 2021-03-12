using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006EE RID: 1774
	internal interface IProvidePropertyPaths
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003618 RID: 13848
		PropertyPath[] PropertyPaths { get; }

		// Token: 0x06003619 RID: 13849
		string GetPropertyPathsString();
	}
}
