using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AB RID: 1195
	internal class ReceiveCommandEventSourceImpl : ReceiveCommandEventSource
	{
		// Token: 0x060035F0 RID: 13808 RVA: 0x000DDB03 File Offset: 0x000DBD03
		private ReceiveCommandEventSourceImpl(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000DDB0C File Offset: 0x000DBD0C
		public static ReceiveCommandEventSource Create(SmtpSession smtpSession)
		{
			return new ReceiveCommandEventSourceImpl(smtpSession);
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000DDB14 File Offset: 0x000DBD14
		public override void RejectCommand(SmtpResponse response)
		{
			this.RejectCommand(response, null);
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000DDB20 File Offset: 0x000DBD20
		public override void RejectCommand(SmtpResponse response, string trackingContext)
		{
			if (response.Equals(SmtpResponse.Empty))
			{
				throw new ArgumentException("Argument cannot be response.Empty", "response");
			}
			base.SmtpSession.SmtpResponse = response;
			base.SmtpSession.ExecutionControl.HaltExecution();
			StringBuilder stringBuilder = new StringBuilder(base.SmtpSession.ExecutionControl.ExecutingAgentName);
			if (!string.IsNullOrEmpty(trackingContext))
			{
				stringBuilder.AppendFormat("; {0}", trackingContext);
			}
			AckDetails ackDetails = new AckDetails(base.SmtpSession.LocalEndPoint, null, base.SmtpSession.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo), null, base.SmtpSession.RemoteEndPoint.Address);
			MessageTrackingLog.TrackRejectCommand(MessageTrackingSource.SMTP, stringBuilder.ToString(), ackDetails, response);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000DDBE0 File Offset: 0x000DBDE0
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000DDBED File Offset: 0x000DBDED
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000DDBFA File Offset: 0x000DBDFA
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}
	}
}
