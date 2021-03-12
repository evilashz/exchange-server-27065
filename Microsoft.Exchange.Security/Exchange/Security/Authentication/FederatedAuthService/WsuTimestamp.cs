using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000090 RID: 144
	internal class WsuTimestamp
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00028210 File Offset: 0x00026410
		static WsuTimestamp()
		{
			string s = DateTime.UtcNow.ToString("o");
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			WsuTimestamp.wsuByteCount = WsuTimestamp.wsuBytesP1.Length + WsuTimestamp.wsuBytesP2.Length + WsuTimestamp.wsuBytesP3.Length + bytes.Length * 2;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0002829A File Offset: 0x0002649A
		internal static int EncodedByteCount
		{
			get
			{
				return WsuTimestamp.wsuByteCount;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000282A4 File Offset: 0x000264A4
		internal static void WriteTimestamp(DateTime time, Stream output)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(time.ToString("o"));
			byte[] bytes2 = Encoding.UTF8.GetBytes(time.AddMinutes(5.0).ToString("o"));
			output.Write(WsuTimestamp.wsuBytesP1, 0, WsuTimestamp.wsuBytesP1.Length);
			output.Write(bytes, 0, bytes.Length);
			output.Write(WsuTimestamp.wsuBytesP2, 0, WsuTimestamp.wsuBytesP2.Length);
			output.Write(bytes2, 0, bytes2.Length);
			output.Write(WsuTimestamp.wsuBytesP3, 0, WsuTimestamp.wsuBytesP3.Length);
		}

		// Token: 0x0400053F RID: 1343
		private const string WsuTemplateP1 = "<wsu:Timestamp Id=\"Timestamp\"><wsu:Created>";

		// Token: 0x04000540 RID: 1344
		private const string WsuTemplateP2 = "</wsu:Created><wsu:Expires>";

		// Token: 0x04000541 RID: 1345
		private const string WsuTemplateP3 = "</wsu:Expires></wsu:Timestamp>";

		// Token: 0x04000542 RID: 1346
		private static readonly byte[] wsuBytesP1 = Encoding.UTF8.GetBytes("<wsu:Timestamp Id=\"Timestamp\"><wsu:Created>");

		// Token: 0x04000543 RID: 1347
		private static readonly byte[] wsuBytesP2 = Encoding.UTF8.GetBytes("</wsu:Created><wsu:Expires>");

		// Token: 0x04000544 RID: 1348
		private static readonly byte[] wsuBytesP3 = Encoding.UTF8.GetBytes("</wsu:Expires></wsu:Timestamp>");

		// Token: 0x04000545 RID: 1349
		private static readonly int wsuByteCount;
	}
}
