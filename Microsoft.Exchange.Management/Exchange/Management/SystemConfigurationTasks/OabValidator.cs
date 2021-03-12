using System;
using System.Net;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000818 RID: 2072
	internal class OabValidator : ServiceValidatorBase
	{
		// Token: 0x060047D9 RID: 18393 RVA: 0x00127817 File Offset: 0x00125A17
		public OabValidator(string uri, NetworkCredential credentials) : base(OabValidator.CombineUrl(uri, OabValidator.OABManifestFile), credentials)
		{
			base.TraceResponseBody = false;
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x060047DA RID: 18394 RVA: 0x00127832 File Offset: 0x00125A32
		protected override string Name
		{
			get
			{
				return Strings.ServiceNameOab;
			}
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0012783E File Offset: 0x00125A3E
		protected override void FillRequestProperties(HttpWebRequest request)
		{
			base.FillRequestProperties(request);
			request.Method = "GET";
			request.ContentType = null;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0012785C File Offset: 0x00125A5C
		private static string CombineUrl(string uri1, string uri2)
		{
			uri1 = uri1.TrimEnd(new char[]
			{
				'/'
			});
			uri2 = uri2.TrimStart(new char[]
			{
				'/'
			});
			return string.Format("{0}/{1}", uri1, uri2);
		}

		// Token: 0x04002B7D RID: 11133
		private static readonly string OABManifestFile = "oab.xml";
	}
}
