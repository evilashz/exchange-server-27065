using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.Class)]
	internal class HttpMethodAttribute : Attribute
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00007C9D File Offset: 0x00005E9D
		public HttpMethodAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00007CB7 File Offset: 0x00005EB7
		public string Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007CBF File Offset: 0x00005EBF
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00007CC7 File Offset: 0x00005EC7
		public HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			protected set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007CD0 File Offset: 0x00005ED0
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public string ContextToken
		{
			get
			{
				return this.contextToken;
			}
			set
			{
				this.contextToken = value;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007CE1 File Offset: 0x00005EE1
		public bool AppliesToToken(string token)
		{
			return this.contextToken == null || token == null || string.Compare(this.contextToken, token, StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x0400016F RID: 367
		private readonly string method;

		// Token: 0x04000170 RID: 368
		private HttpStatusCode statusCode = HttpStatusCode.OK;

		// Token: 0x04000171 RID: 369
		private string contextToken;
	}
}
