using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000374 RID: 884
	internal class AuthenticationImageHandler : IHttpHandler
	{
		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x0009305C File Offset: 0x0009125C
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x00093060 File Offset: 0x00091260
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "image/gif";
			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			context.Response.Cache.SetNoStore();
			context.Response.BinaryWrite(AuthenticationImageHandler.clearImageByteArray);
		}

		// Token: 0x04002350 RID: 9040
		private static readonly byte[] clearImageByteArray = new byte[]
		{
			71,
			73,
			70,
			56,
			57,
			97,
			1,
			0,
			1,
			0,
			240,
			0,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			33,
			249,
			4,
			1,
			0,
			0,
			1,
			0,
			44,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			0,
			0,
			2,
			2,
			76,
			1,
			0,
			59
		};
	}
}
