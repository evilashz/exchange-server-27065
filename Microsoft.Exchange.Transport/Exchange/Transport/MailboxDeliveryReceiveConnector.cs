﻿using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000183 RID: 387
	internal class MailboxDeliveryReceiveConnector : InMemoryReceiveConnector
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x00044BD0 File Offset: 0x00042DD0
		public MailboxDeliveryReceiveConnector(string name, Server localServer, bool acceptAnonymousUsers) : base(name, localServer, acceptAnonymousUsers)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			ArgumentValidator.ThrowIfNull("localServer", localServer);
			base.Bindings = new MultiValuedProperty<IPBinding>(false, ReceiveConnectorSchema.Bindings, new IPBinding[]
			{
				IPBinding.Parse(string.Format("0.0.0.0:{0}", 475)),
				IPBinding.Parse(string.Format("[::]:{0}", 475))
			});
			base.RemoteIPRanges = new MultiValuedProperty<IPRange>(false, ReceiveConnectorSchema.RemoteIPRanges, new IPRange[]
			{
				IPRange.Parse("0.0.0.0-255.255.255.255"),
				IPRange.Parse("[::]-FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF")
			});
			base.TransportRole = ServerRole.Mailbox;
			base.AuthMechanism = AuthMechanisms.ExchangeServer;
			base.PermissionGroups = PermissionGroups.ExchangeServers;
			base.MaxInboundConnection = 5000;
			base.MaxInboundConnectionPerSource = Unlimited<int>.UnlimitedValue;
			base.MaxProtocolErrors = 5;
			base.TouchCalculatedProperties();
		}
	}
}
