using System;
using System.IO;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B2A RID: 2858
	internal static class StreamExtension
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x000A0968 File Offset: 0x0009EB68
		public static int CopyTo(this Stream readStream, Stream writeStream)
		{
			int num = 0;
			byte[] array = new byte[4096];
			int num2;
			while ((num2 = readStream.Read(array, 0, array.Length)) > 0)
			{
				writeStream.Write(array, 0, num2);
				num += num2;
			}
			return num;
		}
	}
}
