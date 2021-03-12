﻿using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000028 RID: 40
	internal sealed class RmsProtocolDecryptionAgent : SmtpReceiveAgent
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000A3B3 File Offset: 0x000085B3
		public RmsProtocolDecryptionAgent()
		{
			base.OnEndOfData += this.EndOfDataHandler;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A3D0 File Offset: 0x000085D0
		private void EndOfDataHandler(ReceiveMessageEventSource receiveMessageEventSource, EndOfDataEventArgs endOfDataEventArgs)
		{
			DecryptionBaseComponent decryptionBaseComponent = new DecryptionBaseComponent(base.MailItem, new OnProcessDecryption(this.OnProcessDecryption));
			if (!decryptionBaseComponent.ShouldProcess())
			{
				return;
			}
			this.source = receiveMessageEventSource;
			decryptionBaseComponent.StartDecryption();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A40C File Offset: 0x0000860C
		private object OnProcessDecryption(DecryptionStatus status, TransportDecryptionSetting settings, AgentAsyncState asyncState, Exception exception)
		{
			if (status == DecryptionStatus.StartAsync)
			{
				return base.GetAgentAsyncContext();
			}
			if (status == DecryptionStatus.PermanentFailure)
			{
				this.OnPermanentFailure(settings);
			}
			else if (status == DecryptionStatus.Success)
			{
				string localTcpInfo = this.source.SmtpSession.LocalEndPoint.Address.ToString();
				HeaderList headers = base.MailItem.Message.RootPart.Headers;
				Utils.PatchReceiverHeader(headers, localTcpInfo, "Transport Decrypted");
			}
			if (asyncState != null)
			{
				asyncState.Complete();
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A47C File Offset: 0x0000867C
		private void OnPermanentFailure(TransportDecryptionSetting setting)
		{
			if (setting == TransportDecryptionSetting.Mandatory)
			{
				RmsDecryptionAgentPerfCounters.MessageFailedToDecrypt.Increment();
				DecryptionBaseComponent.UpdatePercentileCounters(false);
				RmsClientManager.TraceFail(this, base.MailItem.SystemProbeId, "NDRMessage for message {0}, Response {1}", new object[]
				{
					base.MailItem.Message.MessageId,
					"Microsoft Exchange Transport cannot RMS decrypt the message."
				});
				this.source.RejectMessage(Utils.GetResponseForNDR(new string[]
				{
					"Microsoft Exchange Transport cannot RMS decrypt the message."
				}));
			}
		}

		// Token: 0x04000124 RID: 292
		private ReceiveMessageEventSource source;
	}
}
