using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D1 RID: 1233
	internal class SmtpReceiveConfiguration : ISmtpReceiveConfiguration
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000E86C1 File Offset: 0x000E68C1
		// (set) Token: 0x060038D7 RID: 14551 RVA: 0x000E86C9 File Offset: 0x000E68C9
		public IDiagnosticsConfigProvider DiagnosticsConfiguration { get; private set; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000E86D2 File Offset: 0x000E68D2
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000E86DA File Offset: 0x000E68DA
		public IRoutingConfigProvider RoutingConfiguration { get; private set; }

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000E86E3 File Offset: 0x000E68E3
		// (set) Token: 0x060038DB RID: 14555 RVA: 0x000E86EB File Offset: 0x000E68EB
		public ITransportConfigProvider TransportConfiguration { get; private set; }

		// Token: 0x060038DC RID: 14556 RVA: 0x000E86F4 File Offset: 0x000E68F4
		public static SmtpReceiveConfiguration Create(ITransportAppConfig appConfig, ITransportConfiguration transportConfiguration)
		{
			ArgumentValidator.ThrowIfNull("appConfig", appConfig);
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			return new SmtpReceiveConfiguration(appConfig, transportConfiguration);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000E8713 File Offset: 0x000E6913
		private SmtpReceiveConfiguration(ITransportAppConfig appConfig, ITransportConfiguration transportConfiguration)
		{
			this.DiagnosticsConfiguration = DiagnosticsConfigAdapter.Create(appConfig);
			this.RoutingConfiguration = RoutingConfigAdapter.Create(appConfig);
			this.TransportConfiguration = TransportConfigAdapter.Create(appConfig, transportConfiguration);
		}
	}
}
