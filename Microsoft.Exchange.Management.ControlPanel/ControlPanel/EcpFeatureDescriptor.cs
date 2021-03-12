using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000551 RID: 1361
	internal class EcpFeatureDescriptor
	{
		// Token: 0x06003FB9 RID: 16313 RVA: 0x000C086D File Offset: 0x000BEA6D
		public EcpFeatureDescriptor(EcpFeature id, string serverPath, string url, bool useAbsoluteUrl = false)
		{
			this.Id = id;
			this.Name = id.GetName();
			this.ServerPath = serverPath;
			this.url = url;
			this.UseAbsoluteUrl = useAbsoluteUrl;
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x000C089E File Offset: 0x000BEA9E
		// (set) Token: 0x06003FBB RID: 16315 RVA: 0x000C08A6 File Offset: 0x000BEAA6
		public EcpFeature Id { get; private set; }

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x000C08AF File Offset: 0x000BEAAF
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x000C08B7 File Offset: 0x000BEAB7
		public string Name { get; private set; }

		// Token: 0x170024D1 RID: 9425
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x000C08C0 File Offset: 0x000BEAC0
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x000C08C8 File Offset: 0x000BEAC8
		public bool UseAbsoluteUrl { get; private set; }

		// Token: 0x170024D2 RID: 9426
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000C08D4 File Offset: 0x000BEAD4
		public Uri AbsoluteUrl
		{
			get
			{
				if (this.Url.IsAbsoluteUri)
				{
					return this.Url;
				}
				Uri uri = new Uri(HttpContext.Current.GetRequestUrl(), this.Url);
				return EcpUrl.ResolveClientUrl(uri);
			}
		}

		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x000C0911 File Offset: 0x000BEB11
		// (set) Token: 0x06003FC2 RID: 16322 RVA: 0x000C0919 File Offset: 0x000BEB19
		public string ServerPath { get; private set; }

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x000C0922 File Offset: 0x000BEB22
		public Uri Url
		{
			get
			{
				if (this.calculatedUrl == null)
				{
					this.CalculateUrl();
				}
				return this.calculatedUrl;
			}
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000C0940 File Offset: 0x000BEB40
		private void CalculateUrl()
		{
			Uri uri = new Uri(this.url, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				string text = EcpUrl.GetLeftPart(this.url, UriPartial.Path);
				string text2 = this.url.Substring(text.Length);
				text = VirtualPathUtility.ToAbsolute(text);
				if (!string.IsNullOrEmpty(text2))
				{
					text += text2;
				}
				text = EcpUrl.AppendQueryParameter(text, "exsvurl", "1");
				uri = new Uri(text, UriKind.Relative);
			}
			this.calculatedUrl = uri;
		}

		// Token: 0x04002A67 RID: 10855
		private readonly string url;

		// Token: 0x04002A68 RID: 10856
		private Uri calculatedUrl;
	}
}
