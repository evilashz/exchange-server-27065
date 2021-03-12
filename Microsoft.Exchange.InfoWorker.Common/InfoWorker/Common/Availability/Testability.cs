using System;
using System.Net;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000D8 RID: 216
	internal static class Testability
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public static bool HandleSmtpAddressAsContact(string checkAddress)
		{
			return Testability.SmtpAddressAsContacts != null && Array.Exists<string>(Testability.SmtpAddressAsContacts, (string address) => StringComparer.OrdinalIgnoreCase.Equals(address, checkAddress));
		}

		// Token: 0x0400034F RID: 847
		internal static NetworkCredential WebServiceCredentials;

		// Token: 0x04000350 RID: 848
		public static string[] SmtpAddressAsContacts;

		// Token: 0x020000D9 RID: 217
		public class TestWebServiceCredential : IDisposable
		{
			// Token: 0x06000593 RID: 1427 RVA: 0x00018D09 File Offset: 0x00016F09
			public TestWebServiceCredential(NetworkCredential credential)
			{
				Testability.WebServiceCredentials = credential;
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00018D17 File Offset: 0x00016F17
			public void Dispose()
			{
				Testability.WebServiceCredentials = null;
			}
		}
	}
}
