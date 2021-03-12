using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver.Shared
{
	// Token: 0x0200002C RID: 44
	internal class StoreDriverServer : SmtpServer
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00007C60 File Offset: 0x00005E60
		protected StoreDriverServer(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			this.organizationId = organizationId;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public override string Name
		{
			get
			{
				return StoreDriverServer.serverName;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007CBF File Offset: 0x00005EBF
		public override Version Version
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007CC7 File Offset: 0x00005EC7
		public override IPPermission IPPermission
		{
			get
			{
				return StoreDriverServer.allowDenyList;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007CD0 File Offset: 0x00005ED0
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007D34 File Offset: 0x00005F34
		public override AcceptedDomainCollection AcceptedDomains
		{
			get
			{
				if (this.acceptedDomains == null)
				{
					PerTenantAcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(this.organizationId);
					this.acceptedDomains = acceptedDomainTable.AcceptedDomainTable;
				}
				return this.acceptedDomains;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override RemoteDomainCollection RemoteDomains
		{
			get
			{
				if (this.remoteDomains == null)
				{
					PerTenantRemoteDomainTable remoteDomainTable = Components.Configuration.GetRemoteDomainTable(this.organizationId);
					this.remoteDomains = remoteDomainTable.RemoteDomainTable;
				}
				return this.remoteDomains;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static StoreDriverServer GetInstance(OrganizationId organizationId)
		{
			return new StoreDriverServer(organizationId);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007DAC File Offset: 0x00005FAC
		public override void SubmitMessage(EmailMessage message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007DB3 File Offset: 0x00005FB3
		public virtual void SubmitMessage(IReadOnlyMailItem originalMailItem, EmailMessage message, OrganizationId organizationId, Guid externalOrganizationId, bool suppressDSNs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007DBA File Offset: 0x00005FBA
		public virtual void SubmitMailItem(TransportMailItem mailItem, bool suppressDSNs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000099 RID: 153
		private static string serverName = Dns.GetHostName();

		// Token: 0x0400009A RID: 154
		private static IPPermission allowDenyList = new IPPermissionImpl();

		// Token: 0x0400009B RID: 155
		private volatile AddressBookImpl addressBook;

		// Token: 0x0400009C RID: 156
		private object addressBookCreationLock = new object();

		// Token: 0x0400009D RID: 157
		private OrganizationId organizationId;

		// Token: 0x0400009E RID: 158
		private AcceptedDomainCollection acceptedDomains;

		// Token: 0x0400009F RID: 159
		private RemoteDomainCollection remoteDomains;

		// Token: 0x040000A0 RID: 160
		private Version serverVersion = Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion;
	}
}
