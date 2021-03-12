using System;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000004 RID: 4
	internal interface ISerializationGuard
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9
		bool StopSerializedEvents { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10
		object SerializationLocker { get; }
	}
}
