using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Partner;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000201 RID: 513
	internal class CatServer : ExtendedRoutingSmtpServer
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x0005C8AC File Offset: 0x0005AAAC
		private CatServer(IReadOnlyMailItem currentMailItem, AcceptedDomainCollection acceptedDomains)
		{
			if (acceptedDomains == null)
			{
				throw new ArgumentNullException("acceptedDomains");
			}
			this.currentMailItem = currentMailItem;
			this.acceptedDomains = acceptedDomains;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0005C915 File Offset: 0x0005AB15
		public override string Name
		{
			get
			{
				return CatServer.serverName;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005C91C File Offset: 0x0005AB1C
		public override Version Version
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0005C924 File Offset: 0x0005AB24
		public override IPPermission IPPermission
		{
			get
			{
				return CatServer.allowDenyList;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005C92C File Offset: 0x0005AB2C
		public override AddressBook AddressBook
		{
			get
			{
				if (this.addressBook == null)
				{
					lock (this.addressBookCreationLock)
					{
						if (this.addressBook == null)
						{
							this.addressBook = new AddressBookImpl();
						}
					}
				}
				return this.addressBook;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0005C990 File Offset: 0x0005AB90
		public override AcceptedDomainCollection AcceptedDomains
		{
			get
			{
				return this.acceptedDomains;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0005C998 File Offset: 0x0005AB98
		public override RemoteDomainCollection RemoteDomains
		{
			get
			{
				return this.remoteDomains;
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0005C9A0 File Offset: 0x0005ABA0
		public static CatServer GetInstance(IReadOnlyMailItem currentMailItem, AcceptedDomainCollection acceptedDomains)
		{
			return new CatServer(currentMailItem, acceptedDomains);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0005C9A9 File Offset: 0x0005ABA9
		public override void SubmitMessage(EmailMessage message)
		{
			SubmitHelper.CreateTransportMailItemAndSubmit(this.currentMailItem, message, CatServer.serverName, this.serverVersion, base.AssociatedAgent.Name);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0005C9CD File Offset: 0x0005ABCD
		public void SubmitMessage(EmailMessage message, OrganizationId organizationId, Guid externalOrgId = default(Guid))
		{
			SubmitHelper.CreateTransportMailItemAndSubmit(this.currentMailItem, message, CatServer.serverName, this.serverVersion, base.AssociatedAgent.Name, organizationId, externalOrgId, false);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005C9F4 File Offset: 0x0005ABF4
		public override void TrackAgentInfo(string agentName, string groupName, List<KeyValuePair<string, string>> data)
		{
			QueuedMessageEventSource queuedMessageEventSource = base.AssociatedAgent.Session.CurrentEventSource as QueuedMessageEventSource;
			if (queuedMessageEventSource == null)
			{
				throw new InvalidOperationException("Not invoked from a routing agent");
			}
			queuedMessageEventSource.TrackAgentInfo(agentName, groupName, data);
		}

		// Token: 0x04000B5E RID: 2910
		private static readonly string serverName = Dns.GetHostName();

		// Token: 0x04000B5F RID: 2911
		private static IPPermission allowDenyList = new IPPermissionImpl();

		// Token: 0x04000B60 RID: 2912
		private IReadOnlyMailItem currentMailItem;

		// Token: 0x04000B61 RID: 2913
		private volatile AddressBookImpl addressBook;

		// Token: 0x04000B62 RID: 2914
		private object addressBookCreationLock = new object();

		// Token: 0x04000B63 RID: 2915
		private AcceptedDomainCollection acceptedDomains;

		// Token: 0x04000B64 RID: 2916
		private RemoteDomainCollection remoteDomains = Components.AgentComponent.RemoteDomains;

		// Token: 0x04000B65 RID: 2917
		private Version serverVersion = Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion;
	}
}
