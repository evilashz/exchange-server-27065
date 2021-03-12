using System;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Partner;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AE RID: 1198
	internal class SmtpReceiveServer : ExtendedSmtpServer
	{
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000DDD80 File Offset: 0x000DBF80
		public static string ServerName
		{
			get
			{
				if (SmtpReceiveServer.serverName == null)
				{
					lock (SmtpReceiveServer.syncRoot)
					{
						if (SmtpReceiveServer.serverName == null)
						{
							SmtpReceiveServer.serverName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
						}
					}
				}
				return SmtpReceiveServer.serverName;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000DDDD8 File Offset: 0x000DBFD8
		public override string Name
		{
			get
			{
				return SmtpReceiveServer.ServerName;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000DDDDF File Offset: 0x000DBFDF
		public override Version Version
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x000DDDE7 File Offset: 0x000DBFE7
		public override IPPermission IPPermission
		{
			get
			{
				return SmtpReceiveServer.ipPermission;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
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
							this.addressBook = new AddressBookImpl(this.isMemberOfResolver);
						}
					}
				}
				return this.addressBook;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x000DDE5C File Offset: 0x000DC05C
		public override AcceptedDomainCollection AcceptedDomains
		{
			get
			{
				return this.acceptedDomains;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000DDE64 File Offset: 0x000DC064
		public override RemoteDomainCollection RemoteDomains
		{
			get
			{
				return this.remoteDomains;
			}
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000DDE6C File Offset: 0x000DC06C
		public override void SubmitMessage(EmailMessage message)
		{
			if (this.TransportMailItem != null)
			{
				SubmitHelper.CreateTransportMailItemAndSubmit(this.TransportMailItem, message, SmtpReceiveServer.ServerName, this.serverVersion, base.AssociatedAgent.Name, null, this.TransportMailItem.ExternalOrganizationId, false);
				return;
			}
			SubmitHelper.CreateNewTransportMailItemAndSubmit(message, SmtpReceiveServer.ServerName, this.serverVersion, base.AssociatedAgent.Name, default(Guid), null, null, false);
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000DDEE1 File Offset: 0x000DC0E1
		private TransportMailItem TransportMailItem
		{
			get
			{
				if (this.smtpInSession != null)
				{
					return this.smtpInSession.TransportMailItem;
				}
				return this.sessionState.TransportMailItem;
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000DDF02 File Offset: 0x000DC102
		public static SmtpReceiveServer FromSmtpInSession(ISmtpInSession smtpInSession, AcceptedDomainCollection acceptedDomains, RemoteDomainCollection remoteDomains, Version serverVersion)
		{
			return new SmtpReceiveServer(smtpInSession, acceptedDomains, remoteDomains, serverVersion);
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000DDF0D File Offset: 0x000DC10D
		public static SmtpReceiveServer FromSmtpInSessionState(SmtpInSessionState sessionState, AcceptedDomainCollection acceptedDomains, RemoteDomainCollection remoteDomains, Version serverVersion, IIsMemberOfResolver<RoutingAddress> isMemberOfResolver)
		{
			return new SmtpReceiveServer(sessionState, acceptedDomains, remoteDomains, serverVersion, isMemberOfResolver);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000DDF1C File Offset: 0x000DC11C
		private SmtpReceiveServer(ISmtpInSession smtpInSession, AcceptedDomainCollection acceptedDomains, RemoteDomainCollection remoteDomains, Version serverVersion)
		{
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			ArgumentValidator.ThrowIfNull("acceptedDomains", acceptedDomains);
			ArgumentValidator.ThrowIfNull("remoteDomains", remoteDomains);
			ArgumentValidator.ThrowIfNull("serverVersion", serverVersion);
			this.smtpInSession = smtpInSession;
			this.acceptedDomains = acceptedDomains;
			this.remoteDomains = remoteDomains;
			this.serverVersion = serverVersion;
			this.isMemberOfResolver = smtpInSession.IsMemberOfResolver;
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000DDF90 File Offset: 0x000DC190
		private SmtpReceiveServer(SmtpInSessionState sessionState, AcceptedDomainCollection acceptedDomains, RemoteDomainCollection remoteDomains, Version serverVersion, IIsMemberOfResolver<RoutingAddress> isMemberOfResolver)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("acceptedDomains", acceptedDomains);
			ArgumentValidator.ThrowIfNull("remoteDomains", remoteDomains);
			ArgumentValidator.ThrowIfNull("serverVersion", serverVersion);
			ArgumentValidator.ThrowIfNull("isMemberOfResolver", isMemberOfResolver);
			this.sessionState = sessionState;
			this.acceptedDomains = acceptedDomains;
			this.remoteDomains = remoteDomains;
			this.serverVersion = serverVersion;
			this.isMemberOfResolver = isMemberOfResolver;
		}

		// Token: 0x04001BA9 RID: 7081
		private static readonly object syncRoot = new object();

		// Token: 0x04001BAA RID: 7082
		private static string serverName;

		// Token: 0x04001BAB RID: 7083
		private static readonly IPPermission ipPermission = new IPPermissionImpl();

		// Token: 0x04001BAC RID: 7084
		private volatile AddressBookImpl addressBook;

		// Token: 0x04001BAD RID: 7085
		private readonly object addressBookCreationLock = new object();

		// Token: 0x04001BAE RID: 7086
		private readonly AcceptedDomainCollection acceptedDomains;

		// Token: 0x04001BAF RID: 7087
		private readonly RemoteDomainCollection remoteDomains;

		// Token: 0x04001BB0 RID: 7088
		private readonly Version serverVersion;

		// Token: 0x04001BB1 RID: 7089
		private readonly IIsMemberOfResolver<RoutingAddress> isMemberOfResolver;

		// Token: 0x04001BB2 RID: 7090
		private readonly ISmtpInSession smtpInSession;

		// Token: 0x04001BB3 RID: 7091
		private readonly SmtpInSessionState sessionState;
	}
}
