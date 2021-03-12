using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D4B RID: 3403
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EmailTransportService : Service
	{
		// Token: 0x060075FC RID: 30204 RVA: 0x00209CC4 File Offset: 0x00207EC4
		internal EmailTransportService(TopologyServerInfo serverInfo, ServiceType serviceType, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniEmailTransport emailTransport) : base(serverInfo, serviceType, clientAccessType, authenticationMethod)
		{
			this.PopImapTransport = (emailTransport.IsPop3 || emailTransport.IsImap4);
			if (this.PopImapTransport)
			{
				this.UnencryptedOrTLSPort = EmailTransportService.GetPort(emailTransport.UnencryptedOrTLSBindings);
				this.SSLPort = EmailTransportService.GetPort(emailTransport.SSLBindings);
				this.InternalConnectionSettings = Service.ConvertToReadOnlyCollection<ProtocolConnectionSettings>(emailTransport.InternalConnectionSettings);
				this.ExternalConnectionSettings = Service.ConvertToReadOnlyCollection<ProtocolConnectionSettings>(emailTransport.ExternalConnectionSettings);
				this.LoginType = emailTransport.LoginType;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x060075FD RID: 30205 RVA: 0x00209D52 File Offset: 0x00207F52
		// (set) Token: 0x060075FE RID: 30206 RVA: 0x00209D5A File Offset: 0x00207F5A
		public bool PopImapTransport { get; private set; }

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x060075FF RID: 30207 RVA: 0x00209D63 File Offset: 0x00207F63
		// (set) Token: 0x06007600 RID: 30208 RVA: 0x00209D6B File Offset: 0x00207F6B
		public int UnencryptedOrTLSPort { get; private set; }

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06007601 RID: 30209 RVA: 0x00209D74 File Offset: 0x00207F74
		// (set) Token: 0x06007602 RID: 30210 RVA: 0x00209D7C File Offset: 0x00207F7C
		public int SSLPort { get; private set; }

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x06007603 RID: 30211 RVA: 0x00209D85 File Offset: 0x00207F85
		// (set) Token: 0x06007604 RID: 30212 RVA: 0x00209D8D File Offset: 0x00207F8D
		public ReadOnlyCollection<ProtocolConnectionSettings> InternalConnectionSettings { get; private set; }

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06007605 RID: 30213 RVA: 0x00209D96 File Offset: 0x00207F96
		// (set) Token: 0x06007606 RID: 30214 RVA: 0x00209D9E File Offset: 0x00207F9E
		public ReadOnlyCollection<ProtocolConnectionSettings> ExternalConnectionSettings { get; private set; }

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x06007607 RID: 30215 RVA: 0x00209DA7 File Offset: 0x00207FA7
		// (set) Token: 0x06007608 RID: 30216 RVA: 0x00209DAF File Offset: 0x00207FAF
		public LoginOptions LoginType { get; private set; }

		// Token: 0x06007609 RID: 30217 RVA: 0x00209DB8 File Offset: 0x00207FB8
		internal static bool TryCreateEmailTransportService(MiniEmailTransport emailTransport, TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			service = new EmailTransportService(serverInfo, ServiceType.Invalid, clientAccessType, authenticationMethod, emailTransport);
			return true;
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x00209DC8 File Offset: 0x00207FC8
		private static int GetPort(MultiValuedProperty<IPBinding> bindings)
		{
			if (bindings == null || bindings.Count <= 0)
			{
				return -1;
			}
			return bindings[0].Port;
		}
	}
}
