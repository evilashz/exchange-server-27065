using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.ServiceSupport
{
	// Token: 0x020000F7 RID: 247
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ServiceEndpointAddress
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00014FF1 File Offset: 0x000131F1
		public ServiceEndpointAddress(string endpointSuffix)
		{
			this.endpointSuffix = endpointSuffix;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00015000 File Offset: 0x00013200
		public virtual string GetAddress(string serverName)
		{
			return string.Format("net.tcp://{0}/{1}", serverName, this.endpointSuffix);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00015013 File Offset: 0x00013213
		public virtual Uri[] GetBaseUris()
		{
			return Array<Uri>.Empty;
		}

		// Token: 0x040002DE RID: 734
		private readonly string endpointSuffix;
	}
}
