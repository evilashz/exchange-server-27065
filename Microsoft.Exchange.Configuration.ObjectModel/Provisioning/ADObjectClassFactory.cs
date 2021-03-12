using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001FB RID: 507
	public static class ADObjectClassFactory
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x000375AC File Offset: 0x000357AC
		public static TResult GetObjectInstance<TResult>() where TResult : ADObject, new()
		{
			return Activator.CreateInstance<TResult>();
		}
	}
}
