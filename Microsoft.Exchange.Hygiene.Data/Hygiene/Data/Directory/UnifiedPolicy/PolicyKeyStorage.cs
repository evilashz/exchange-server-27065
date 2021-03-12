using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	internal sealed class PolicyKeyStorage : Dictionary<string, string>
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x0001F36A File Offset: 0x0001D56A
		public PolicyKeyStorage() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001F377 File Offset: 0x0001D577
		public PolicyKeyStorage(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
