using System;
using System.Web.Services.Protocols;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006DE RID: 1758
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class SoapHttpClientTraceExtensionAttribute : SoapExtensionAttribute
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00041466 File Offset: 0x0003F666
		public override Type ExtensionType
		{
			get
			{
				return typeof(SoapHttpClientTraceExtension);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x00041472 File Offset: 0x0003F672
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x0004147A File Offset: 0x0003F67A
		public override int Priority { get; set; }
	}
}
