using System;
using System.IO;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000098 RID: 152
	internal static class MapiProtocolsHelpers
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0002707F File Offset: 0x0002527F
		public static void AssertPropValueIsNotSqlType(object propValue)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00027081 File Offset: 0x00025281
		public static byte[] GetUnderlyingBytesFromMemoryStream(MemoryStream memoryStream)
		{
			return memoryStream.GetBuffer();
		}
	}
}
