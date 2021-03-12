using System;
using System.Web;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000052 RID: 82
	internal class ExWebHealthResponseWrapper : IExWebHealthResponseWrapper
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00008433 File Offset: 0x00006633
		internal ExWebHealthResponseWrapper(HttpResponse response)
		{
			this.response = response;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008442 File Offset: 0x00006642
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000844F File Offset: 0x0000664F
		public int StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
			set
			{
				this.response.StatusCode = value;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000845D File Offset: 0x0000665D
		public void AddHeader(string name, string value)
		{
			this.response.AddHeader(name, value);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000846C File Offset: 0x0000666C
		public string GetHeaderValue(string name)
		{
			return this.response.Headers[name];
		}

		// Token: 0x04000189 RID: 393
		private HttpResponse response;
	}
}
