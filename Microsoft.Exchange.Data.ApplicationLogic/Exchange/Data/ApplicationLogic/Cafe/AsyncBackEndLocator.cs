using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000B0 RID: 176
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncBackEndLocator
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0001D180 File Offset: 0x0001B380
		public IAsyncResult BeginGetBackEndServerList(MiniRecipient miniRecipient, int maxServers, AsyncCallback callback, object state)
		{
			if (miniRecipient == null)
			{
				throw new ArgumentNullException("miniRecipient");
			}
			if (maxServers <= 0)
			{
				throw new ArgumentException("maxServers needs to be greater than zero");
			}
			this.database = miniRecipient.Database;
			this.maxServers = maxServers;
			OrganizationId organizationId = miniRecipient.OrganizationId;
			SmtpAddress primarySmtpAddress = miniRecipient.PrimarySmtpAddress;
			if (this.database == null)
			{
				ADUser defaultOrganizationMailbox = HttpProxyBackEndHelper.GetDefaultOrganizationMailbox(organizationId, null);
				if (defaultOrganizationMailbox == null || defaultOrganizationMailbox.Database == null)
				{
					ExTraceGlobals.CafeTracer.TraceError<OrganizationId>(0L, "[BackEndLocator.BeginGetBackEndServerList] Cannot find organization mailbox for organization {1}", organizationId);
					throw new AdUserNotFoundException(ServerStrings.ADUserNotFound);
				}
				this.database = defaultOrganizationMailbox.Database;
				primarySmtpAddress = defaultOrganizationMailbox.PrimarySmtpAddress;
			}
			string domainName = null;
			if (organizationId != null && !organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				domainName = primarySmtpAddress.Domain;
			}
			this.serverLocator = MailboxServerLocator.Create(this.database.ObjectGuid, domainName, this.database.PartitionFQDN);
			bool flag = true;
			IAsyncResult result;
			try
			{
				result = this.serverLocator.BeginGetServer(callback, state);
				flag = false;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CafeTracer.TraceError<Exception>(0L, "[AsyncBackEndLocator.BeginGetBackEndServerList] Caught exception {0}.", ex);
				if (BackEndLocator.ShouldWrapInBackendLocatorException(ex))
				{
					throw new BackEndLocatorException(ex);
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					this.serverLocator.Dispose();
					this.serverLocator = null;
				}
			}
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		public IList<BackEndServer> EndGetBackEndServerList(IAsyncResult asyncResult)
		{
			IList<BackEndServer> result;
			try
			{
				BackEndServer backEndServer = this.serverLocator.EndGetServer(asyncResult);
				ExTraceGlobals.CafeTracer.TraceDebug<BackEndServer, ADObjectId>(0L, "[MailboxServerLocator.EndGetServer] called inside [AsyncBackEndLocator.EndGetBackEndServerList] returned back end server {0} for database {1}", backEndServer, this.database);
				IList<BackEndServer> list = new List<BackEndServer>();
				list.Add(backEndServer);
				int num = 1;
				Random localRandom = new Random(AsyncBackEndLocator.GetRandomNumber(int.MaxValue));
				IEnumerable<KeyValuePair<Guid, BackEndServer>> enumerable = from x in this.serverLocator.AvailabilityGroupServers
				orderby localRandom.Next()
				select x;
				foreach (KeyValuePair<Guid, BackEndServer> keyValuePair in enumerable)
				{
					if (num >= this.maxServers)
					{
						break;
					}
					if (!this.IsServerInBackendList(list, keyValuePair.Value))
					{
						list.Add(keyValuePair.Value);
						num++;
					}
				}
				result = list;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CafeTracer.TraceError<Exception>(0L, "[AsyncBackEndLocator.EndGetBackEndServerList] Caught exception {0}.", ex);
				if (BackEndLocator.ShouldWrapInBackendLocatorException(ex))
				{
					throw new BackEndLocatorException(ex);
				}
				throw;
			}
			finally
			{
				this.serverLocator.Dispose();
				this.serverLocator = null;
			}
			return result;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001D428 File Offset: 0x0001B628
		private static int GetRandomNumber(int maxValue)
		{
			int result;
			lock (AsyncBackEndLocator.RandomSeeder)
			{
				result = AsyncBackEndLocator.RandomSeeder.Next(maxValue);
			}
			return result;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001D494 File Offset: 0x0001B694
		private bool IsServerInBackendList(IEnumerable<BackEndServer> serverList, BackEndServer backendServer)
		{
			return serverList.Any((BackEndServer server) => string.Equals(server.Fqdn, backendServer.Fqdn, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04000362 RID: 866
		private static readonly Random RandomSeeder = new Random((int)DateTime.UtcNow.Ticks);

		// Token: 0x04000363 RID: 867
		private MailboxServerLocator serverLocator;

		// Token: 0x04000364 RID: 868
		private ADObjectId database;

		// Token: 0x04000365 RID: 869
		private int maxServers;
	}
}
