using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E6 RID: 1254
	internal class EhloSmtpInCommand : HeloSmtpInCommandBase
	{
		// Token: 0x06003A21 RID: 14881 RVA: 0x000F0CD2 File Offset: 0x000EEED2
		public EhloSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
			this.role = sessionState.Configuration.TransportConfiguration.ProcessTransportRole;
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000F0CF4 File Offset: 0x000EEEF4
		protected override void OnParseComplete(out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			EhloCommandEventArgs ehloCommandEventArgs = new EhloCommandEventArgs(this.sessionState);
			if (!string.IsNullOrEmpty(this.parseOutput.HeloDomain))
			{
				this.sessionState.HelloDomain = this.parseOutput.HeloDomain;
				ehloCommandEventArgs.Domain = this.parseOutput.HeloDomain;
			}
			agentEventTopic = "OnEhloCommand";
			agentEventArgs = ehloCommandEventArgs;
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000F0D50 File Offset: 0x000EEF50
		protected override Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsyncInternal(CancellationToken cancellationToken)
		{
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("AUTH LOGIN", this.ShouldAuthLoginBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("AUTH GSSAPI", this.ShouldAuthGssApiBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("AUTH NTLM", this.ShouldAuthNtlmBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("X-EXPS GSSAPI", this.ShouldExpsGssApiBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("X-EXPS EXCHANGEAUTH", this.ShouldExpsExchangeAuthBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.AddAuthenticationMechanism("X-EXPS NTLM", this.ShouldExpsNtlmBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.StartTls, this.ShouldStartTlsBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.AnonymousTls, this.ShouldAnonymousTlsBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.Xoorg, this.ShouldXoorgBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.Xproxy, this.ShouldXproxyBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XproxyFrom, this.ShouldXproxyFromBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XproxyTo, this.ShouldXproxyToBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XrsetProxyTo, this.ShouldXrsetProxyToBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XSessionMdbGuid, this.ShouldXSessionMdbGuidBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XAttr, this.ShouldXAttrBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XSysProbe, this.ShouldXSysProbeBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XExProps, this.ShouldExtendedPropertiesBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XAdrc, this.ShouldADRecipientCacheBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XFastIndex, this.ShouldFastIndexBeAdvertised);
			this.sessionState.AdvertisedEhloOptions.SetFlags(EhloOptionsFlags.XSessionType, this.ShouldXSessionTypeBeAdvertised);
			return Task.FromResult<ParseAndProcessResult<SmtpInStateMachineEvents>>(new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, this.sessionState.AdvertisedEhloOptions.CreateSmtpResponse(this.sessionState.MessageContextBlob.AdrcSmtpMessageContextBlob, this.sessionState.MessageContextBlob.ExtendedPropertiesSmtpMessageContextBlob, this.sessionState.MessageContextBlob.FastIndexSmtpMessageContextBlob), SmtpInStateMachineEvents.EhloProcessed, false));
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000F0FC3 File Offset: 0x000EF1C3
		protected override HeloOrEhlo Command
		{
			get
			{
				return HeloOrEhlo.Ehlo;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000F0FC6 File Offset: 0x000EF1C6
		protected virtual bool ShouldAuthLoginBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldAuthLoginBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000F0FE8 File Offset: 0x000EF1E8
		protected virtual bool ShouldAuthGssApiBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldAuthGssApiBeAdvertised(this.sessionState.IsIntegratedAuthSupported, this.sessionState.ReceiveConnector.EnableAuthGSSAPI);
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000F100A File Offset: 0x000EF20A
		protected virtual bool ShouldAuthNtlmBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldAuthNtlmBeAdvertised(this.sessionState.IsIntegratedAuthSupported);
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000F101C File Offset: 0x000EF21C
		protected virtual bool ShouldExpsGssApiBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldExpsGssApiBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism, this.role);
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000F1039 File Offset: 0x000EF239
		protected virtual bool ShouldExpsExchangeAuthBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldExpsExchangeAuthBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism, this.sessionState.SecureState, this.role);
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000F1061 File Offset: 0x000EF261
		protected virtual bool ShouldExpsNtlmBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldExpsNtlmBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism);
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000F1078 File Offset: 0x000EF278
		protected virtual bool ShouldStartTlsBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldStartTlsBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism, this.sessionState.SecureState, this.sessionState.IsStartTlsSupported);
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000F10A5 File Offset: 0x000EF2A5
		protected virtual bool ShouldAnonymousTlsBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldAnonymousTlsBeAdvertised(this.sessionState.ReceiveConnector.AuthMechanism, this.sessionState.SecureState, this.sessionState.IsAnonymousTlsSupported);
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000F10D2 File Offset: 0x000EF2D2
		protected virtual bool ShouldXoorgBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXoorgBeAdvertised(this.sessionState.Capabilities);
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x000F10E4 File Offset: 0x000EF2E4
		protected virtual bool ShouldXproxyBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXproxyBeAdvertised(this.role, this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000F1107 File Offset: 0x000EF307
		protected virtual bool ShouldXproxyFromBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXproxyFromBeAdvertised(this.role, this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000F112A File Offset: 0x000EF32A
		protected virtual bool ShouldXproxyToBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXproxyToBeAdvertised(this.role, this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x000F114D File Offset: 0x000EF34D
		protected virtual bool ShouldXrsetProxyToBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXrsetProxyToBeAdvertised(this.role, this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000F1170 File Offset: 0x000EF370
		protected virtual bool ShouldXSessionMdbGuidBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXSessionMdbGuidBeAdvertised(this.role, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003A33 RID: 14899 RVA: 0x000F1188 File Offset: 0x000EF388
		protected virtual bool ShouldXAttrBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXAttrBeAdvertised(this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000F11A5 File Offset: 0x000EF3A5
		protected virtual bool ShouldXSysProbeBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXSysProbeBeAdvertised(this.sessionState.Capabilities, this.sessionState.SecureState);
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06003A35 RID: 14901 RVA: 0x000F11C2 File Offset: 0x000EF3C2
		protected virtual bool ShouldExtendedPropertiesBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldExtendedPropertiesBeAdvertised(this.role, this.sessionState.SecureState, this.sessionState.Configuration.TransportConfiguration.AdvertiseExtendedProperties);
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000F11EF File Offset: 0x000EF3EF
		protected virtual bool ShouldADRecipientCacheBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldADRecipientCacheBeAdvertised(this.role, this.sessionState.SecureState, this.sessionState.Configuration.TransportConfiguration.AdvertiseADRecipientCache);
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000F121C File Offset: 0x000EF41C
		protected virtual bool ShouldFastIndexBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldFastIndexBeAdvertised(this.role, this.sessionState.SecureState, this.sessionState.Configuration.TransportConfiguration.AdvertiseFastIndex);
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000F1249 File Offset: 0x000EF449
		protected virtual bool ShouldXSessionTypeBeAdvertised
		{
			get
			{
				return SmtpInSessionUtils.ShouldXSessionTypeBeAdvertised(this.role, this.sessionState.SecureState);
			}
		}

		// Token: 0x04001D59 RID: 7513
		private readonly ProcessTransportRole role;
	}
}
