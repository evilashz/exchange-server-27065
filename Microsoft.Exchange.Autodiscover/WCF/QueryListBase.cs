using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000075 RID: 117
	internal abstract class QueryListBase<T> : IQueryList where T : ResultBase
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00014194 File Offset: 0x00012394
		public void Add(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string, QueryListBase<T>>((long)this.GetHashCode(), "Adding address '{0}' to {1}.", userResultMapping.Mailbox, this);
			string key = userResultMapping.SmtpProxyAddress.ToString();
			T t;
			if (!this.resultDictionary.TryGetValue(key, out t))
			{
				t = this.CreateResult(userResultMapping);
				this.resultDictionary.Add(key, t);
			}
			userResultMapping.Result = t;
		}

		// Token: 0x06000316 RID: 790
		protected abstract T CreateResult(UserResultMapping userResultMapping);

		// Token: 0x06000317 RID: 791
		public abstract void Execute();

		// Token: 0x040002ED RID: 749
		protected Dictionary<string, T> resultDictionary = new Dictionary<string, T>();
	}
}
