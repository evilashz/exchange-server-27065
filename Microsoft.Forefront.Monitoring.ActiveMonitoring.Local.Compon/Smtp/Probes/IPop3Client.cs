using System;
using System.Collections.Generic;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000221 RID: 545
	public interface IPop3Client : IDisposable
	{
		// Token: 0x060011B1 RID: 4529
		void Connect(string server, int port, string username, string password, bool enableSsl, int readTimeout);

		// Token: 0x060011B2 RID: 4530
		void Disconnect();

		// Token: 0x060011B3 RID: 4531
		List<Pop3Message> List();

		// Token: 0x060011B4 RID: 4532
		void RetrieveHeader(Pop3Message message);

		// Token: 0x060011B5 RID: 4533
		void Retrieve(Pop3Message message);

		// Token: 0x060011B6 RID: 4534
		void Retrieve(List<Pop3Message> messages);

		// Token: 0x060011B7 RID: 4535
		void Delete(Pop3Message message);
	}
}
