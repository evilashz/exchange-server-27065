using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public class ServiceEndpoint
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0002BD56 File Offset: 0x00029F56
		public ServiceEndpoint(Uri uri) : this(uri, string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002BD6E File Offset: 0x00029F6E
		public ServiceEndpoint(Uri uri, string certificateSubject) : this(uri, string.Empty, certificateSubject, string.Empty)
		{
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002BD82 File Offset: 0x00029F82
		public ServiceEndpoint(Uri uri, string uriTemplate, string certificateSubject, string token)
		{
			this.uri = uri;
			this.uriTemplate = uriTemplate;
			this.certificateSubject = certificateSubject;
			this.token = token;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		public ServiceEndpoint ApplyTemplate(params object[] uriTemplateArgs)
		{
			if (uriTemplateArgs == null)
			{
				throw new ArgumentNullException("uriTemplateArgs");
			}
			if (this.uri != null)
			{
				throw new FormatException(string.Format("URI {0} is already formatted", this.uri.AbsoluteUri));
			}
			if (string.IsNullOrEmpty(this.uriTemplate))
			{
				throw new FormatException("URI template is empty");
			}
			Uri uri = new Uri(string.Format(this.uriTemplate, uriTemplateArgs));
			return new ServiceEndpoint(uri, string.Empty, this.certificateSubject, this.token);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0002BE2D File Offset: 0x0002A02D
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0002BE35 File Offset: 0x0002A035
		public string UriTemplate
		{
			get
			{
				return this.uriTemplate;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0002BE3D File Offset: 0x0002A03D
		public string CertificateSubject
		{
			get
			{
				return this.certificateSubject;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0002BE45 File Offset: 0x0002A045
		public string Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04000862 RID: 2146
		private Uri uri;

		// Token: 0x04000863 RID: 2147
		private string uriTemplate;

		// Token: 0x04000864 RID: 2148
		private string certificateSubject;

		// Token: 0x04000865 RID: 2149
		private string token;
	}
}
