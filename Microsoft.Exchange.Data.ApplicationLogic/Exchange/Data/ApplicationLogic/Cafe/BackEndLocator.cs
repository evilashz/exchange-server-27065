using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class BackEndLocator
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public static BackEndServer GetAnyBackEndServer()
		{
			return BackEndLocator.CallWithExceptionHandling<BackEndServer>(() => HttpProxyBackEndHelper.GetAnyBackEndServer());
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001D548 File Offset: 0x0001B748
		public static BackEndServer GetBackEndServer(ADUser aduser)
		{
			if (aduser == null)
			{
				throw new ArgumentNullException("aduser");
			}
			return BackEndLocator.CallWithExceptionHandling<BackEndServer>(() => BackEndLocator.GetBackEndServerByDatabase(aduser.Database, aduser.OrganizationId, aduser.PrimarySmtpAddress));
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		public static BackEndServer GetBackEndServer(ADObjectId database)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			return BackEndLocator.CallWithExceptionHandling<BackEndServer>(() => BackEndLocator.GetBackEndServerByDatabase(database, null, default(SmtpAddress)));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001D624 File Offset: 0x0001B824
		public static BackEndServer GetBackEndServer(MiniRecipient miniRecipient)
		{
			if (miniRecipient == null)
			{
				throw new ArgumentNullException("miniRecipient");
			}
			return BackEndLocator.CallWithExceptionHandling<BackEndServer>(() => BackEndLocator.GetBackEndServerByDatabase(miniRecipient.Database, miniRecipient.OrganizationId, miniRecipient.PrimarySmtpAddress));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001D698 File Offset: 0x0001B898
		public static IList<BackEndServer> GetBackEndServerList(MiniRecipient miniRecipient, int maxServers)
		{
			if (miniRecipient == null)
			{
				throw new ArgumentNullException("miniRecipient");
			}
			return BackEndLocator.CallWithExceptionHandling<IList<BackEndServer>>(() => BackEndLocator.GetBackEndServerListForDatabase(miniRecipient.Database, miniRecipient.OrganizationId, miniRecipient.PrimarySmtpAddress, maxServers));
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001D768 File Offset: 0x0001B968
		public static BackEndServer GetBackEndServer(IMailboxInfo mailbox)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			return BackEndLocator.CallWithExceptionHandling<BackEndServer>(delegate
			{
				if (mailbox.Location != null)
				{
					BackEndServer backEndServer = new BackEndServer(mailbox.Location.ServerFqdn, mailbox.Location.ServerVersion);
					ExTraceGlobals.CafeTracer.TraceDebug<BackEndServer, IMailboxInfo>(0L, "[BackEndLocator.GetBackEndServer] Returns back end server {0} for Mailbox {1}", backEndServer, mailbox);
					return backEndServer;
				}
				return BackEndLocator.GetBackEndServerByDatabase(mailbox.MailboxDatabase, mailbox.OrganizationId, mailbox.PrimarySmtpAddress);
			});
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001D7A6 File Offset: 0x0001B9A6
		public static Uri GetBackEndWebServicesUrl(ADUser aduser)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<WebServicesService>(aduser);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001D7AE File Offset: 0x0001B9AE
		public static Uri GetBackEndWebServicesUrl(MiniRecipient miniRecipient)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<WebServicesService>(miniRecipient);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001D7B6 File Offset: 0x0001B9B6
		public static Uri GetBackEndWebServicesUrl(IMailboxInfo mailbox)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<WebServicesService>(mailbox);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001D7BE File Offset: 0x0001B9BE
		public static Uri GetBackEndWebServicesUrl(BackEndServer backEndServer)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<WebServicesService>(backEndServer);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		public static Uri GetBackEndOwaUrl(IMailboxInfo mailbox)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<OwaService>(mailbox);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001D7CE File Offset: 0x0001B9CE
		public static Uri GetBackEndEcpUrl(IMailboxInfo mailbox)
		{
			return BackEndLocator.GetBackEndHttpServiceUrl<EcpService>(mailbox);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		public static bool ShouldWrapInBackendLocatorException(Exception exception)
		{
			return exception is ObjectNotFoundException || exception is ServerLocatorClientException || exception is ServerLocatorClientTransientException || exception is ServiceDiscoveryPermanentException || exception is ServiceDiscoveryTransientException || (exception is InsufficientMemoryException && exception.InnerException is SocketException);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001D850 File Offset: 0x0001BA50
		private static Uri GetBackEndHttpServiceUrl<ServiceType>(ADUser aduser) where ServiceType : HttpService
		{
			if (aduser == null)
			{
				throw new ArgumentNullException("aduser");
			}
			return BackEndLocator.CallWithExceptionHandling<Uri>(delegate
			{
				BackEndServer backEndServer = BackEndLocator.GetBackEndServer(aduser);
				return HttpProxyBackEndHelper.GetBackEndServiceUrlByServer<ServiceType>(backEndServer);
			});
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
		private static Uri GetBackEndHttpServiceUrl<ServiceType>(MiniRecipient miniRecipient) where ServiceType : HttpService
		{
			if (miniRecipient == null)
			{
				throw new ArgumentNullException("miniRecipient");
			}
			return BackEndLocator.CallWithExceptionHandling<Uri>(delegate
			{
				BackEndServer backEndServer = BackEndLocator.GetBackEndServer(miniRecipient);
				return HttpProxyBackEndHelper.GetBackEndServiceUrlByServer<ServiceType>(backEndServer);
			});
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001D920 File Offset: 0x0001BB20
		private static Uri GetBackEndHttpServiceUrl<ServiceType>(IMailboxInfo mailbox) where ServiceType : HttpService
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			return BackEndLocator.CallWithExceptionHandling<Uri>(delegate
			{
				BackEndServer backEndServer = BackEndLocator.GetBackEndServer(mailbox);
				return HttpProxyBackEndHelper.GetBackEndServiceUrlByServer<ServiceType>(backEndServer);
			});
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001D974 File Offset: 0x0001BB74
		private static Uri GetBackEndHttpServiceUrl<ServiceType>(BackEndServer backEndServer) where ServiceType : HttpService
		{
			if (backEndServer == null)
			{
				throw new ArgumentNullException("backEndServer");
			}
			return BackEndLocator.CallWithExceptionHandling<Uri>(() => HttpProxyBackEndHelper.GetBackEndServiceUrlByServer<ServiceType>(backEndServer));
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		private static BackEndServer GetBackEndServerByDatabase(ADObjectId database, OrganizationId organizationId, SmtpAddress primarySmtpAddress)
		{
			if (database == null)
			{
				return BackEndLocator.GetBackEndServerByOrganization(organizationId);
			}
			string domainName = null;
			if (organizationId != null && !organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				domainName = primarySmtpAddress.Domain;
			}
			BackEndServer result;
			using (MailboxServerLocator mailboxServerLocator = MailboxServerLocator.Create(database.ObjectGuid, domainName, database.PartitionFQDN))
			{
				BackEndServer server = mailboxServerLocator.GetServer();
				ExTraceGlobals.CafeTracer.TraceDebug<BackEndServer, ADObjectId>(0L, "[BackEndLocator.GetBackEndServerByDatabase] Returns back end server {0} for database {1}", server, database);
				result = server;
			}
			return result;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001DA38 File Offset: 0x0001BC38
		private static IList<BackEndServer> GetBackEndServerListForDatabase(ADObjectId database, OrganizationId organizationId, SmtpAddress primarySmtpAddress, int maxServers)
		{
			if (maxServers == 0)
			{
				return new BackEndServer[0];
			}
			if (database == null)
			{
				return BackEndLocator.GetBackEndServerListForOrganization(organizationId, maxServers);
			}
			string domainName = null;
			if (organizationId != null && !organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				domainName = primarySmtpAddress.Domain;
			}
			IList<BackEndServer> result;
			using (MailboxServerLocator mailboxServerLocator = MailboxServerLocator.Create(database.ObjectGuid, domainName, database.PartitionFQDN))
			{
				BackEndServer server = mailboxServerLocator.GetServer();
				ExTraceGlobals.CafeTracer.TraceDebug<BackEndServer, ADObjectId>(0L, "[BackEndLocator.GetBackEndServerByDatabase] Returns back end server {0} for database {1}", server, database);
				IList<BackEndServer> list = new List<BackEndServer>();
				list.Add(server);
				int num = 1;
				foreach (KeyValuePair<Guid, BackEndServer> keyValuePair in mailboxServerLocator.AvailabilityGroupServers)
				{
					if (num >= maxServers)
					{
						break;
					}
					if (!string.Equals(keyValuePair.Value.Fqdn, server.Fqdn, StringComparison.OrdinalIgnoreCase))
					{
						list.Add(keyValuePair.Value);
						num++;
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001DB48 File Offset: 0x0001BD48
		private static IList<BackEndServer> GetBackEndServerListForOrganization(OrganizationId organizationId, int maxServers)
		{
			ADUser defaultOrganizationMailbox = HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(organizationId, null);
			if (defaultOrganizationMailbox == null || defaultOrganizationMailbox.Database == null)
			{
				ExTraceGlobals.CafeTracer.TraceError<OrganizationId>(0L, "[BackEndLocator.GetBackEndServerByOrganization] Cannot find organization mailbox for organization {1}", organizationId);
				throw new AdUserNotFoundException(ServerStrings.ADUserNotFound);
			}
			return BackEndLocator.GetBackEndServerListForDatabase(defaultOrganizationMailbox.Database, organizationId, defaultOrganizationMailbox.PrimarySmtpAddress, maxServers);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		private static BackEndServer GetBackEndServerByOrganization(OrganizationId organizationId)
		{
			ADUser defaultOrganizationMailbox = HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(organizationId, null);
			if (defaultOrganizationMailbox == null || defaultOrganizationMailbox.Database == null)
			{
				ExTraceGlobals.CafeTracer.TraceError<OrganizationId>(0L, "[BackEndLocator.GetBackEndServerByOrganization] Cannot find organization mailbox for organization {1}", organizationId);
				throw new AdUserNotFoundException(ServerStrings.ADUserNotFound);
			}
			return BackEndLocator.GetBackEndServerByDatabase(defaultOrganizationMailbox.Database, organizationId, defaultOrganizationMailbox.PrimarySmtpAddress);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		private static T CallWithExceptionHandling<T>(Func<T> actualCall)
		{
			T result;
			try
			{
				result = actualCall();
			}
			catch (BackEndLocatorException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CafeTracer.TraceError<Exception>(0L, "[BackEndLocator.CallWithExceptionHandling] Caught exception {0}.", ex);
				if (BackEndLocator.ShouldWrapInBackendLocatorException(ex))
				{
					throw new BackEndLocatorException(ex);
				}
				throw;
			}
			return result;
		}
	}
}
