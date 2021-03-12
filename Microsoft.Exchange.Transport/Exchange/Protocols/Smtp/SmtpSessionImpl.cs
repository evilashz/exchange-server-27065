using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B0 RID: 1200
	internal sealed class SmtpSessionImpl : SmtpSession
	{
		// Token: 0x06003619 RID: 13849 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
		public SmtpSessionImpl(ISmtpInSession internalSmtpSession, INetworkConnection networkConnection, bool isExternalConnection)
		{
			ArgumentValidator.ThrowIfNull("internalSmtpSession", internalSmtpSession);
			ArgumentValidator.ThrowIfNull("networkConnection", networkConnection);
			this.networkConnection = networkConnection;
			this.SmtpResponse = SmtpResponse.Empty;
			this.internalSmtpSession = internalSmtpSession;
			this.RemoteEndPoint = networkConnection.RemoteEndPoint;
			this.IsExternalConnection = isExternalConnection;
			this.LastExternalIPAddress = (isExternalConnection ? networkConnection.RemoteEndPoint.Address : null);
			this.sessionId = Microsoft.Exchange.Transport.SessionId.GetNextSessionId();
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000DE46D File Offset: 0x000DC66D
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000DE475 File Offset: 0x000DC675
		public override string HelloDomain { get; internal set; }

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000DE47E File Offset: 0x000DC67E
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.networkConnection.LocalEndPoint;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000DE48B File Offset: 0x000DC68B
		// (set) Token: 0x0600361E RID: 13854 RVA: 0x000DE493 File Offset: 0x000DC693
		public override IPEndPoint RemoteEndPoint { get; internal set; }

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000DE49C File Offset: 0x000DC69C
		public override IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000DE4A4 File Offset: 0x000DC6A4
		public override long SessionId
		{
			get
			{
				return (long)this.sessionId;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000DE4AC File Offset: 0x000DC6AC
		public override bool IsConnected
		{
			get
			{
				return !this.ShouldDisconnect;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000DE4B7 File Offset: 0x000DC6B7
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000DE4BF File Offset: 0x000DC6BF
		public override bool IsExternalConnection { get; internal set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000DE4C8 File Offset: 0x000DC6C8
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000DE4D0 File Offset: 0x000DC6D0
		public override IPAddress LastExternalIPAddress { get; internal set; }

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000DE4D9 File Offset: 0x000DC6D9
		public override AuthenticationSource AuthenticationSource
		{
			get
			{
				return this.internalSmtpSession.AuthenticationSourceForAgents;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000DE4E6 File Offset: 0x000DC6E6
		public override bool AntispamBypass
		{
			get
			{
				return SmtpInSessionUtils.HasSMTPAntiSpamBypassPermission(this.internalSmtpSession.Permissions);
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000DE4F8 File Offset: 0x000DC6F8
		public override bool IsTls
		{
			get
			{
				return this.internalSmtpSession.IsTls;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000DE505 File Offset: 0x000DC705
		internal override bool DiscardingMessage
		{
			get
			{
				return this.internalSmtpSession.DiscardingMessage;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000DE512 File Offset: 0x000DC712
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000DE51A File Offset: 0x000DC71A
		internal override bool ShouldDisconnect { get; set; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000DE523 File Offset: 0x000DC723
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x000DE52B File Offset: 0x000DC72B
		internal override bool IsInboundProxiedSession { get; set; }

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000DE534 File Offset: 0x000DC734
		// (set) Token: 0x0600362F RID: 13871 RVA: 0x000DE53C File Offset: 0x000DC73C
		internal override bool IsClientProxiedSession { get; set; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000DE545 File Offset: 0x000DC745
		internal override bool XAttrAdvertised
		{
			get
			{
				return this.internalSmtpSession.AdvertisedEhloOptions.XAttr;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000DE557 File Offset: 0x000DC757
		internal override string ReceiveConnectorName
		{
			get
			{
				return this.internalSmtpSession.Connector.Name;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000DE569 File Offset: 0x000DC769
		internal override X509Certificate2 TlsRemoteCertificate
		{
			get
			{
				return this.internalSmtpSession.TlsRemoteCertificate;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000DE576 File Offset: 0x000DC776
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x000DE57E File Offset: 0x000DC77E
		internal override SmtpResponse Banner { get; set; }

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000DE587 File Offset: 0x000DC787
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x000DE58F File Offset: 0x000DC78F
		internal override SmtpResponse SmtpResponse { get; set; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000DE598 File Offset: 0x000DC798
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x000DE5A0 File Offset: 0x000DC7A0
		internal override DisconnectReason DisconnectReason { get; set; }

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000DE5A9 File Offset: 0x000DC7A9
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000DE5B1 File Offset: 0x000DC7B1
		internal override IExecutionControl ExecutionControl { get; set; }

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000DE5BA File Offset: 0x000DC7BA
		internal override string CurrentMessageTemporaryId
		{
			get
			{
				return this.internalSmtpSession.CurrentMessageTemporaryId;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000DE5C7 File Offset: 0x000DC7C7
		// (set) Token: 0x0600363D RID: 13885 RVA: 0x000DE5D4 File Offset: 0x000DC7D4
		internal override bool DisableStartTls
		{
			get
			{
				return this.internalSmtpSession.DisableStartTls;
			}
			set
			{
				this.internalSmtpSession.DisableStartTls = value;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000DE5E2 File Offset: 0x000DC7E2
		// (set) Token: 0x0600363F RID: 13887 RVA: 0x000DE5EF File Offset: 0x000DC7EF
		internal override bool RequestClientTlsCertificate
		{
			get
			{
				return this.internalSmtpSession.ForceRequestClientTlsCertificate;
			}
			set
			{
				this.internalSmtpSession.ForceRequestClientTlsCertificate = value;
			}
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000DE5FD File Offset: 0x000DC7FD
		internal override void GrantMailItemPermissions(Permission permissionsToGrant)
		{
			this.internalSmtpSession.GrantMailItemPermissions(permissionsToGrant);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000DE60B File Offset: 0x000DC80B
		internal override void RejectMessage(SmtpResponse response)
		{
			this.RejectMessage(response, null);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000DE618 File Offset: 0x000DC818
		internal override void RejectMessage(SmtpResponse response, string sourceContext)
		{
			this.SmtpResponse = response;
			SmtpSessionHelper.RejectMessage(response, sourceContext, this.ExecutionControl, this.internalSmtpSession.TransportMailItem, this.internalSmtpSession.LocalEndPoint, this.internalSmtpSession.RemoteEndPoint, this.internalSmtpSession.SessionId, this.internalSmtpSession.Connector, this.internalSmtpSession.LogSession, this.messageTrackingLogWrapper);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000DE684 File Offset: 0x000DC884
		internal override void DiscardMessage(SmtpResponse response, string sourceContext)
		{
			if (response.SmtpResponseType != SmtpResponseType.Success)
			{
				throw new InvalidOperationException("Response provided must be a success (2xx) one. If you want to reject, call RejectMessage instead");
			}
			this.SmtpResponse = response;
			SmtpSessionHelper.DiscardMessage(sourceContext, this.ExecutionControl, this.internalSmtpSession.TransportMailItem, this.internalSmtpSession.LogSession, this.messageTrackingLogWrapper);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000DE6D5 File Offset: 0x000DC8D5
		internal override void Disconnect()
		{
			this.ShouldDisconnect = true;
			this.ExecutionControl.HaltExecution();
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000DE6E9 File Offset: 0x000DC8E9
		internal override CertificateValidationStatus ValidateCertificate()
		{
			this.ThrowIfNotTls();
			return SmtpSessionHelper.ConvertChainValidityStatusToCertValidationStatus(this.internalSmtpSession.TlsRemoteCertificateChainValidationStatus);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000DE704 File Offset: 0x000DC904
		internal override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			this.ThrowIfNotTls();
			return SmtpSessionHelper.ValidateCertificate(domain, this.internalSmtpSession.TlsRemoteCertificate, this.internalSmtpSession.SecureState, this.internalSmtpSession.TlsRemoteCertificateChainValidationStatus, this.internalSmtpSession.SmtpInServer.CertificateValidator, this.internalSmtpSession.LogSession, out matchedCertDomain);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000DE75A File Offset: 0x000DC95A
		private void ThrowIfNotTls()
		{
			if (!this.internalSmtpSession.IsTls)
			{
				throw new InvalidOperationException("GetCertificateValidationStatus can only be invoked for TLS session.");
			}
		}

		// Token: 0x04001BB5 RID: 7093
		internal const string DateTimeFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffZ";

		// Token: 0x04001BB6 RID: 7094
		private readonly ISmtpInSession internalSmtpSession;

		// Token: 0x04001BB7 RID: 7095
		private readonly INetworkConnection networkConnection;

		// Token: 0x04001BB8 RID: 7096
		private readonly IDictionary<string, object> properties = new Dictionary<string, object>();

		// Token: 0x04001BB9 RID: 7097
		private readonly ulong sessionId;

		// Token: 0x04001BBA RID: 7098
		private readonly MessageTrackingLogWrapper messageTrackingLogWrapper = new MessageTrackingLogWrapper();
	}
}
