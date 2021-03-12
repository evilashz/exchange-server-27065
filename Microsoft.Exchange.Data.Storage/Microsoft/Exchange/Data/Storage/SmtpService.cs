using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D73 RID: 3443
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SmtpService : Service
	{
		// Token: 0x060076F3 RID: 30451 RVA: 0x0020D6D8 File Offset: 0x0020B8D8
		private SmtpService(TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniReceiveConnector smtpReceiveConnector, Hostname hostname) : base(serverInfo, ServiceType.Smtp, clientAccessType, authenticationMethod)
		{
			bool flag = (smtpReceiveConnector.AuthMechanism & (AuthMechanisms.Tls | AuthMechanisms.BasicAuthRequireTLS)) != AuthMechanisms.None;
			EncryptionType? encryptionType = null;
			if (flag)
			{
				encryptionType = new EncryptionType?(EncryptionType.TLS);
			}
			List<ProtocolConnectionSettings> list = new List<ProtocolConnectionSettings>(smtpReceiveConnector.Bindings.Count);
			HashSet<int> hashSet = new HashSet<int>();
			foreach (IPBinding ipbinding in smtpReceiveConnector.Bindings)
			{
				if (!hashSet.Contains(ipbinding.Port))
				{
					list.Add(new ProtocolConnectionSettings(hostname, ipbinding.Port, encryptionType));
					hashSet.Add(ipbinding.Port);
				}
			}
			this.ProtocolConnectionSettings = new ReadOnlyCollection<ProtocolConnectionSettings>(list);
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x060076F4 RID: 30452 RVA: 0x0020D7AC File Offset: 0x0020B9AC
		// (set) Token: 0x060076F5 RID: 30453 RVA: 0x0020D7B4 File Offset: 0x0020B9B4
		public ReadOnlyCollection<ProtocolConnectionSettings> ProtocolConnectionSettings { get; private set; }

		// Token: 0x060076F6 RID: 30454 RVA: 0x0020D7C0 File Offset: 0x0020B9C0
		internal static bool TryCreateSmtpService(MiniReceiveConnector smtpReceiveConnector, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, out Service service)
		{
			Hostname hostname = null;
			service = null;
			if (!smtpReceiveConnector.AdvertiseClientSettings)
			{
				ExTraceGlobals.SmtpServiceTracer.TraceDebug<string>(0L, "SMTP Receive Connector: {0}, does not have AdvertiseClientSettings set.", smtpReceiveConnector.Name);
				return false;
			}
			if (smtpReceiveConnector.ServiceDiscoveryFqdn == null && smtpReceiveConnector.Fqdn == null)
			{
				ExTraceGlobals.SmtpServiceTracer.TraceDebug<string>(0L, "SMTP Receive Connector: {0}, has null Fqdn and ServiceDiscoveryFqdn.", smtpReceiveConnector.Name);
				return false;
			}
			if (smtpReceiveConnector.ServiceDiscoveryFqdn != null)
			{
				if (!Hostname.TryParse(smtpReceiveConnector.ServiceDiscoveryFqdn.ToString(), out hostname))
				{
					ExTraceGlobals.SmtpServiceTracer.TraceDebug<string>(0L, "SMTP Receive Connector: {0}, has unparsable ServiceDiscoveryFqdn.", smtpReceiveConnector.Name);
					return false;
				}
			}
			else if (smtpReceiveConnector.Fqdn != null && !Hostname.TryParse(smtpReceiveConnector.Fqdn.ToString(), out hostname))
			{
				ExTraceGlobals.SmtpServiceTracer.TraceDebug<string>(0L, "SMTP Receive Connector: {0}, has unparsable FQDN.", smtpReceiveConnector.Name);
				return false;
			}
			AuthenticationMethod authenticationMethod = AuthenticationMethod.None;
			authenticationMethod |= (((smtpReceiveConnector.AuthMechanism & (AuthMechanisms.BasicAuth | AuthMechanisms.BasicAuthRequireTLS)) != AuthMechanisms.None) ? AuthenticationMethod.Basic : AuthenticationMethod.None);
			authenticationMethod |= (((smtpReceiveConnector.AuthMechanism & AuthMechanisms.Integrated) != AuthMechanisms.None) ? AuthenticationMethod.WindowsIntegrated : AuthenticationMethod.None);
			service = new SmtpService(serverInfo, clientAccessType, authenticationMethod, smtpReceiveConnector, hostname);
			return true;
		}
	}
}
