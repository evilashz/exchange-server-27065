using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000002 RID: 2
	public static class ClientContextFactory
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static IClientContext Create(string url)
		{
			if (ClientContextFactory.UseMockAttachmentDataProvider)
			{
				return new MockClientContext(url);
			}
			return new ClientContextWrapper(url);
		}

		// Token: 0x04000001 RID: 1
		private static readonly bool UseMockAttachmentDataProvider = new BoolAppSettingsEntry("UseMockAttachmentDataProvider", false, ExTraceGlobals.AttachmentHandlingTracer).Value;
	}
}
