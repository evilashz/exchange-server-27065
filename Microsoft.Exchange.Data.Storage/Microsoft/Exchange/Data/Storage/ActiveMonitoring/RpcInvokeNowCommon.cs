using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000335 RID: 821
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcInvokeNowCommon
	{
		// Token: 0x02000336 RID: 822
		[Serializable]
		public class Request
		{
			// Token: 0x06002489 RID: 9353 RVA: 0x00093D87 File Offset: 0x00091F87
			public Request(string identity, string propertyBag, string extensionAttributes)
			{
				this.MonitorIdentity = identity;
				this.PropertyBag = propertyBag;
				this.ExtensionAttributes = extensionAttributes;
			}

			// Token: 0x17000C3F RID: 3135
			// (get) Token: 0x0600248A RID: 9354 RVA: 0x00093DA4 File Offset: 0x00091FA4
			// (set) Token: 0x0600248B RID: 9355 RVA: 0x00093DAC File Offset: 0x00091FAC
			public string MonitorIdentity { get; set; }

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x0600248C RID: 9356 RVA: 0x00093DB5 File Offset: 0x00091FB5
			// (set) Token: 0x0600248D RID: 9357 RVA: 0x00093DBD File Offset: 0x00091FBD
			public string PropertyBag { get; set; }

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x0600248E RID: 9358 RVA: 0x00093DC6 File Offset: 0x00091FC6
			// (set) Token: 0x0600248F RID: 9359 RVA: 0x00093DCE File Offset: 0x00091FCE
			public string ExtensionAttributes { get; set; }
		}
	}
}
