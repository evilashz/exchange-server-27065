using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000473 RID: 1139
	internal class BufferedStreamReader
	{
		// Token: 0x06002698 RID: 9880 RVA: 0x0008BD4C File Offset: 0x00089F4C
		public static async Task<StringBuilder> ReadAsync(Stream stream)
		{
			StringBuilder builder = new StringBuilder();
			byte[] buffer = new byte[1024];
			for (int bytesRead = await stream.ReadAsync(buffer, 0, 1024); bytesRead > 0; bytesRead = await stream.ReadAsync(buffer, 0, 1024))
			{
				builder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
				((IList)buffer).Clear();
			}
			return builder;
		}

		// Token: 0x04001687 RID: 5767
		private const int bufferSize = 1024;
	}
}
