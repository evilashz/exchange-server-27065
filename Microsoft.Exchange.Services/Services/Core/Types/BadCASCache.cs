using System;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000241 RID: 577
	internal class BadCASCache : BaseWebCache<string, string>
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0004B45C File Offset: 0x0004965C
		internal BadCASCache() : base("_BCC_", SlidingOrAbsoluteTimeout.Absolute, 5)
		{
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0004B46B File Offset: 0x0004966B
		protected override bool ValidateAddition(string key, string value)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[BadCASCache::ValidateAddition] A CAS is being added to the bad CAS cache.  FQDN: {0}", key);
			return base.ValidateAddition(key, value);
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0004B48C File Offset: 0x0004968C
		public static BadCASCache Singleton
		{
			get
			{
				return BadCASCache.singleton;
			}
		}

		// Token: 0x04000BA4 RID: 2980
		private const string BadCASKeyPrefix = "_BCC_";

		// Token: 0x04000BA5 RID: 2981
		private const int TimeoutInMinutes = 5;

		// Token: 0x04000BA6 RID: 2982
		private static BadCASCache singleton = new BadCASCache();
	}
}
