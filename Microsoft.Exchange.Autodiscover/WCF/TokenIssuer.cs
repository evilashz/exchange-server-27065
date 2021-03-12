using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B4 RID: 180
	[DataContract(Name = "TokenIssuer", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class TokenIssuer
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00018176 File Offset: 0x00016376
		public TokenIssuer()
		{
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001817E File Offset: 0x0001637E
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00018186 File Offset: 0x00016386
		[DataMember(Name = "Uri", IsRequired = false)]
		public Uri Uri { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001818F File Offset: 0x0001638F
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00018197 File Offset: 0x00016397
		[DataMember(Name = "Endpoint", IsRequired = false)]
		public Uri Endpoint { get; set; }

		// Token: 0x06000465 RID: 1125 RVA: 0x000181A0 File Offset: 0x000163A0
		public TokenIssuer(Uri uri, Uri endpoint)
		{
			this.Uri = uri;
			this.Endpoint = endpoint;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000181B8 File Offset: 0x000163B8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Uri=",
				this.Uri,
				",Endpoint=",
				this.Endpoint
			});
		}
	}
}
