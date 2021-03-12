using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Logging
{
	// Token: 0x02000081 RID: 129
	internal interface IProtocolLogSession
	{
		// Token: 0x060003A2 RID: 930
		void LogConnect();

		// Token: 0x060003A3 RID: 931
		void LogDisconnect(DisconnectReason reason);

		// Token: 0x060003A4 RID: 932
		void LogDisconnect(DisconnectReason reason, string remoteError);

		// Token: 0x060003A5 RID: 933
		void LogSend(byte[] data);

		// Token: 0x060003A6 RID: 934
		void LogReceive(byte[] data);

		// Token: 0x060003A7 RID: 935
		void LogInformation(ProtocolLoggingLevel loggingLevel, byte[] data, string formatString, params object[] parameterList);

		// Token: 0x060003A8 RID: 936
		void LogInformation(ProtocolLoggingLevel loggingLevel, byte[] data, string context);

		// Token: 0x060003A9 RID: 937
		void LogCertificate(string type, IX509Certificate2 cert);

		// Token: 0x060003AA RID: 938
		void LogCertificate(string type, X509Certificate2 cert);

		// Token: 0x060003AB RID: 939
		void LogCertificateThumbprint(string type, IX509Certificate2 cert);

		// Token: 0x060003AC RID: 940
		void LogCertificateThumbprint(string type, X509Certificate2 cert);

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AD RID: 941
		// (set) Token: 0x060003AE RID: 942
		ProtocolLoggingLevel ProtocolLoggingLevel { get; set; }

		// Token: 0x170000E7 RID: 231
		// (set) Token: 0x060003AF RID: 943
		IPEndPoint LocalEndPoint { set; }
	}
}
