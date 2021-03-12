using System;
using System.Collections;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200013C RID: 316
	internal class FaxTransferReferredByHeaderHandler : ReferredByHeaderHandler
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0002680C File Offset: 0x00024A0C
		internal PlatformSipUri SerializeFaxTransferUri(string recipient, string context)
		{
			if (string.IsNullOrEmpty(recipient) || string.IsNullOrEmpty(context))
			{
				throw new ArgumentNullException("FaxTransferUri");
			}
			return base.FrameHeader(new Hashtable
			{
				{
					"msExchUMFaxRecipient",
					recipient
				},
				{
					"msExchUMContext",
					context
				}
			});
		}

		// Token: 0x040008BB RID: 2235
		private const string FaxRecipient = "msExchUMFaxRecipient";

		// Token: 0x040008BC RID: 2236
		private const string UMContext = "msExchUMContext";
	}
}
