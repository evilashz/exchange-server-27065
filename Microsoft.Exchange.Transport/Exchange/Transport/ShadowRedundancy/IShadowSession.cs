using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000369 RID: 873
	internal interface IShadowSession
	{
		// Token: 0x060025E7 RID: 9703
		IAsyncResult BeginOpen(TransportMailItem transportMailItem, AsyncCallback asyncCallback, object state);

		// Token: 0x060025E8 RID: 9704
		bool EndOpen(IAsyncResult asyncResult);

		// Token: 0x060025E9 RID: 9705
		IAsyncResult BeginWrite(byte[] buffer, int offset, int count, bool seenEod, AsyncCallback asyncCallback, object state);

		// Token: 0x060025EA RID: 9706
		bool EndWrite(IAsyncResult asyncResult);

		// Token: 0x060025EB RID: 9707
		IAsyncResult BeginComplete(AsyncCallback asyncCallback, object state);

		// Token: 0x060025EC RID: 9708
		bool EndComplete(IAsyncResult asyncResult);

		// Token: 0x060025ED RID: 9709
		bool MailItemRequiresShadowCopy(TransportMailItem mailItem);

		// Token: 0x060025EE RID: 9710
		void NotifyProxyFailover(string shadowServer, SmtpResponse smtpResponse);

		// Token: 0x060025EF RID: 9711
		void PrepareForNewCommand(BaseDataSmtpCommand newCommand);

		// Token: 0x060025F0 RID: 9712
		void NotifyLocalMessageDiscarded(TransportMailItem mailItem);

		// Token: 0x060025F1 RID: 9713
		void NotifyMessageRejected(TransportMailItem mailItem);

		// Token: 0x060025F2 RID: 9714
		void NotifyMessageComplete(TransportMailItem mailItem);

		// Token: 0x060025F3 RID: 9715
		void Close(AckStatus ackStatus, SmtpResponse smtpResponse);

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060025F4 RID: 9716
		string ShadowServerContext { get; }
	}
}
