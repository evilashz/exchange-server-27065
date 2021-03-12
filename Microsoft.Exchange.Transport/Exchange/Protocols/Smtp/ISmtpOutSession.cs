using System;
using System.Net;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000396 RID: 918
	internal interface ISmtpOutSession
	{
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600286E RID: 10350
		IProtocolLogSession LogSession { get; }

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600286F RID: 10351
		IPEndPoint LocalEndPoint { get; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002870 RID: 10352
		AckDetails AckDetails { get; }

		// Token: 0x06002871 RID: 10353
		void StartUsingConnection();

		// Token: 0x06002872 RID: 10354
		void FailoverConnection(SmtpResponse smtpResponse, SessionSetupFailureReason failoverReason);

		// Token: 0x06002873 RID: 10355
		void ConnectionCompleted(NetworkConnection networkConnection);

		// Token: 0x06002874 RID: 10356
		void ShutdownConnection();

		// Token: 0x06002875 RID: 10357
		string GetConnectionInfo();

		// Token: 0x06002876 RID: 10358
		void PrepareForNextMessageOnCachedSession();
	}
}
