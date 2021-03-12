using System;
using System.Text;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B72 RID: 2930
	[Serializable]
	internal sealed class TokenTarget
	{
		// Token: 0x06003EDA RID: 16090 RVA: 0x000A514C File Offset: 0x000A334C
		public TokenTarget(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Uri = uri;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000A516F File Offset: 0x000A336F
		public TokenTarget(Uri uri, Uri[] tokenIssuerUris)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (tokenIssuerUris == null)
			{
				throw new ArgumentNullException("tokenIssuerUris");
			}
			this.TokenIssuerUris = tokenIssuerUris;
			this.Uri = uri;
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x000A51A7 File Offset: 0x000A33A7
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x000A51AF File Offset: 0x000A33AF
		public Uri Uri { get; private set; }

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x000A51B8 File Offset: 0x000A33B8
		// (set) Token: 0x06003EDF RID: 16095 RVA: 0x000A51C0 File Offset: 0x000A33C0
		public Uri[] TokenIssuerUris { get; private set; }

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000A51CC File Offset: 0x000A33CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Uri=");
			stringBuilder.Append(this.Uri);
			if (this.TokenIssuerUris != null)
			{
				stringBuilder.Append(",TokenIssuerUris=");
				bool flag = true;
				foreach (Uri uri in this.TokenIssuerUris)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(uri.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000A5254 File Offset: 0x000A3454
		public static Uri Fix(string domain)
		{
			Uri originalUri = new Uri("http://" + domain, UriKind.Absolute);
			return TokenTarget.Fix(originalUri);
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000A5279 File Offset: 0x000A3479
		public static Uri Fix(Uri originalUri)
		{
			if (originalUri.IsAbsoluteUri)
			{
				return originalUri;
			}
			return new Uri("http://" + originalUri.OriginalString, UriKind.Absolute);
		}

		// Token: 0x0400369B RID: 13979
		private const string Prefix = "http://";
	}
}
