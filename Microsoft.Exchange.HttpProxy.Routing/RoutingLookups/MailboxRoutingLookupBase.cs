using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations;
using Microsoft.Exchange.HttpProxy.Routing.RoutingEntries;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000033 RID: 51
	internal abstract class MailboxRoutingLookupBase<T> : IRoutingLookup where T : class, IRoutingKey
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00003C5C File Offset: 0x00001E5C
		protected MailboxRoutingLookupBase(IUserProvider userProvider)
		{
			if (userProvider == null)
			{
				throw new ArgumentNullException("userProvider");
			}
			this.userProvider = userProvider;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003C79 File Offset: 0x00001E79
		protected IUserProvider UserProvider
		{
			get
			{
				return this.userProvider;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C84 File Offset: 0x00001E84
		IRoutingEntry IRoutingLookup.GetRoutingEntry(IRoutingKey routingKey, IRoutingDiagnostics diagnostics)
		{
			if (routingKey == null)
			{
				throw new ArgumentNullException("routingKey");
			}
			if (diagnostics == null)
			{
				throw new ArgumentNullException("diagnostics");
			}
			T t = routingKey as T;
			if (t == null)
			{
				string message = string.Format("Routing key type {0} is not supported", routingKey.GetType());
				throw new ArgumentException(message, "routingKey");
			}
			return this.GetMailboxRoutingEntry(t, diagnostics);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public MailboxRoutingEntry GetMailboxRoutingEntry(T routingKey, IRoutingDiagnostics diagnostics)
		{
			if (routingKey == null)
			{
				throw new ArgumentNullException("routingKey");
			}
			MailboxRoutingEntry result;
			try
			{
				User user = this.FindUser(routingKey, diagnostics);
				if (user == null)
				{
					result = this.CreateFailedEntry(routingKey, "Unable to find user");
				}
				else
				{
					Guid? guid = null;
					string resourceForest = null;
					this.SelectDatabaseGuidResourceForest(routingKey, user, out guid, out resourceForest);
					if (guid == null)
					{
						result = this.CreateFailedEntry(routingKey, "User object missing database GUID");
					}
					else
					{
						long timestamp = (user.LastModifiedTime != null) ? user.LastModifiedTime.Value.ToFileTimeUtc() : DateTime.UtcNow.ToFileTimeUtc();
						string domainName = this.GetDomainName(routingKey);
						result = new SuccessfulMailboxRoutingEntry(routingKey, new DatabaseGuidRoutingDestination(guid.Value, domainName, resourceForest), timestamp);
					}
				}
			}
			catch (UserProviderException ex)
			{
				ErrorRoutingDestination destination = new ErrorRoutingDestination(ex.Message);
				result = new FailedMailboxRoutingEntry(routingKey, destination, DateTime.UtcNow.ToFileTimeUtc());
			}
			return result;
		}

		// Token: 0x060000DB RID: 219
		protected abstract User FindUser(T routingKey, IRoutingDiagnostics diagnostics);

		// Token: 0x060000DC RID: 220
		protected abstract string GetDomainName(T routingKey);

		// Token: 0x060000DD RID: 221 RVA: 0x00003E08 File Offset: 0x00002008
		protected virtual void SelectDatabaseGuidResourceForest(T routingKey, User user, out Guid? databaseGuid, out string resourceForest)
		{
			databaseGuid = user.DatabaseGuid;
			resourceForest = user.DatabaseResourceForest;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E20 File Offset: 0x00002020
		private FailedMailboxRoutingEntry CreateFailedEntry(IRoutingKey smtpRoutingKey, string message)
		{
			ErrorRoutingDestination destination = new ErrorRoutingDestination(message);
			return new FailedMailboxRoutingEntry(smtpRoutingKey, destination, DateTime.UtcNow.ToFileTimeUtc());
		}

		// Token: 0x0400005E RID: 94
		private readonly IUserProvider userProvider;
	}
}
