using System;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200003B RID: 59
	public class OwaUrl
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000872D File Offset: 0x0000692D
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00008735 File Offset: 0x00006935
		public string AuthenticationMethods
		{
			get
			{
				return this.authenticationMethods;
			}
			set
			{
				this.authenticationMethods = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000873E File Offset: 0x0000693E
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00008746 File Offset: 0x00006946
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x04000199 RID: 409
		private string authenticationMethods;

		// Token: 0x0400019A RID: 410
		private string url;
	}
}
