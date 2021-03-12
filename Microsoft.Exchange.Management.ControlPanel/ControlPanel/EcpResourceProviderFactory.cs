using System;
using System.Web.Compilation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000552 RID: 1362
	public class EcpResourceProviderFactory : ResourceProviderFactory
	{
		// Token: 0x06003FC6 RID: 16326 RVA: 0x000C09C0 File Offset: 0x000BEBC0
		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new EcpGlobalResourceProvider(classKey);
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000C09C8 File Offset: 0x000BEBC8
		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			throw new NotSupportedException();
		}
	}
}
