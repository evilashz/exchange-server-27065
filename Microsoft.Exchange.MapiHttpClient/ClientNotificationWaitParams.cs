using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ClientNotificationWaitParams
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003D97 File Offset: 0x00001F97
		public static void Serialize(BufferWriter writer, int flags)
		{
			writer.WriteInt32(flags);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public static void Parse(Reader reader, out int flags)
		{
			flags = reader.ReadInt32();
		}
	}
}
