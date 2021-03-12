using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200036A RID: 874
	internal class NullShadowSession : IShadowSession
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x00093E4E File Offset: 0x0009204E
		public IAsyncResult BeginOpen(TransportMailItem transportMailItem, AsyncCallback asyncCallback, object state)
		{
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			return new AsyncResult(asyncCallback, state, true);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00093E66 File Offset: 0x00092066
		public bool EndOpen(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return true;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00093E77 File Offset: 0x00092077
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, bool seenEod, AsyncCallback asyncCallback, object state)
		{
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			return new AsyncResult(asyncCallback, state, true);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00093E92 File Offset: 0x00092092
		public bool EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return true;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00093EA3 File Offset: 0x000920A3
		public IAsyncResult BeginComplete(AsyncCallback asyncCallback, object state)
		{
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			return new AsyncResult(asyncCallback, state, true);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00093EBB File Offset: 0x000920BB
		public bool EndComplete(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return true;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00093ECC File Offset: 0x000920CC
		public void NotifyProxyFailover(string shadowServer, SmtpResponse smtpResponse)
		{
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x00093ECE File Offset: 0x000920CE
		public void PrepareForNewCommand(BaseDataSmtpCommand newCommand)
		{
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x00093ED0 File Offset: 0x000920D0
		public bool MailItemRequiresShadowCopy(TransportMailItem mailItem)
		{
			return false;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00093ED3 File Offset: 0x000920D3
		public void NotifyLocalMessageDiscarded(TransportMailItem mailItem)
		{
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00093ED5 File Offset: 0x000920D5
		public void NotifyMessageRejected(TransportMailItem mailItem)
		{
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00093ED7 File Offset: 0x000920D7
		public void NotifyMessageComplete(TransportMailItem mailItem)
		{
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00093ED9 File Offset: 0x000920D9
		public void Close(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00093EDB File Offset: 0x000920DB
		public string ShadowServerContext
		{
			get
			{
				return null;
			}
		}
	}
}
