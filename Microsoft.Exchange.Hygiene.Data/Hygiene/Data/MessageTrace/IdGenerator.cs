using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000146 RID: 326
	internal class IdGenerator
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x000274FB File Offset: 0x000256FB
		public static Guid GenerateIdentifier(IdScope scope)
		{
			return CombGuidGenerator.NewGuid();
		}
	}
}
