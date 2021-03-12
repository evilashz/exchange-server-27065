using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000227 RID: 551
	internal abstract class RoutingTopologyBase
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x00062A25 File Offset: 0x00060C25
		protected RoutingTopologyBase()
		{
			this.session = RoutingTopologyBase.CreateADSession();
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001829 RID: 6185
		public abstract TopologyServer LocalServer { get; }

		// Token: 0x0600182A RID: 6186
		public abstract IEnumerable<MiniDatabase> GetDatabases(bool forcedReload);

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600182B RID: 6187
		public abstract IEnumerable<TopologyServer> Servers { get; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600182C RID: 6188
		public abstract IList<TopologySite> Sites { get; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600182D RID: 6189
		public abstract IList<MailGateway> SendConnectors { get; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600182E RID: 6190
		public abstract IList<PublicFolderTree> PublicFolderTrees { get; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600182F RID: 6191
		public abstract IList<RoutingGroup> RoutingGroups { get; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001830 RID: 6192
		public abstract IList<RoutingGroupConnector> RoutingGroupConnectors { get; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001831 RID: 6193
		public abstract IList<Server> HubServersOnEdge { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00062A38 File Offset: 0x00060C38
		public DateTime WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00062A40 File Offset: 0x00060C40
		protected ADObjectId RootId
		{
			get
			{
				if (this.rootId == null)
				{
					this.rootId = this.session.GetOrgContainerId().GetChildId("Administrative Groups");
				}
				return this.rootId;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00062A6B File Offset: 0x00060C6B
		protected ITopologyConfigurationSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00062A74 File Offset: 0x00060C74
		public static void UnregisterFromADNotifications(IList<ADNotificationRequestCookie> cookies)
		{
			RoutingUtils.ThrowIfNull(cookies, "cookies");
			RoutingDiag.Tracer.TraceDebug(0L, "Unregistering from AD notifications");
			foreach (ADNotificationRequestCookie requestCookie in cookies)
			{
				ADNotificationAdapter.UnregisterChangeNotification(requestCookie, true);
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00062AFC File Offset: 0x00060CFC
		public ADOperationResult TryRegisterForADNotifications(ADNotificationCallback callback, out IList<ADNotificationRequestCookie> cookies)
		{
			cookies = null;
			List<ADNotificationRequestCookie> tempCookies = new List<ADNotificationRequestCookie>(8);
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Registering for AD notifications");
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.RegisterForADNotifications(callback, tempCookies);
			});
			if (!adoperationResult.Succeeded)
			{
				RoutingDiag.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to register for AD notifications due to {0}", adoperationResult.Exception);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingAdUnavailable, null, new object[0]);
				RoutingTopologyBase.UnregisterFromADNotifications(tempCookies);
			}
			else
			{
				cookies = tempCookies;
			}
			return adoperationResult;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00062BA5 File Offset: 0x00060DA5
		public void PreLoad()
		{
			this.whenCreated = DateTime.UtcNow;
			this.PreLoadInternal();
			this.Validate();
		}

		// Token: 0x06001838 RID: 6200
		public abstract void LogData(RoutingTableLogger logger);

		// Token: 0x06001839 RID: 6201
		protected abstract void PreLoadInternal();

		// Token: 0x0600183A RID: 6202
		protected abstract void RegisterForADNotifications(ADNotificationCallback callback, IList<ADNotificationRequestCookie> cookies);

		// Token: 0x0600183B RID: 6203
		protected abstract void Validate();

		// Token: 0x0600183C RID: 6204 RVA: 0x00062BC1 File Offset: 0x00060DC1
		protected IList<T> LoadAll<T>() where T : ADConfigurationObject, new()
		{
			return this.LoadAll<T>((T configObject) => true);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00062BD8 File Offset: 0x00060DD8
		protected IList<T> LoadAll<T>(Func<T, bool> filter) where T : ADConfigurationObject, new()
		{
			RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Loading all objects of class {1} from AD", this.whenCreated, typeof(T).Name);
			List<T> list = this.FindAllPaged<T>().Where(filter).ToList<T>();
			RoutingDiag.Tracer.TraceDebug<DateTime, string, int>((long)this.GetHashCode(), "[{0}] Loaded all objects of class {1} from AD; found {2} object(s)", this.whenCreated, typeof(T).Name, list.Count);
			return list;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00062C54 File Offset: 0x00060E54
		protected ADPagedReader<T> FindAllPaged<T>() where T : ADConfigurationObject, new()
		{
			return this.session.FindPaged<T>(this.RootId, QueryScope.SubTree, null, null, ADGenericPagedReader<T>.DefaultPageSize);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00062C70 File Offset: 0x00060E70
		protected void LogSendConnectors(RoutingTableLogger logger)
		{
			logger.WriteStartElement("SendConnectors");
			foreach (MailGateway connector in this.SendConnectors)
			{
				logger.WriteSendConnector(connector);
			}
			logger.WriteEndElement();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00062CD0 File Offset: 0x00060ED0
		private static ITopologyConfigurationSession CreateADSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 345, "CreateADSession", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Categorizer\\Routing\\RoutingTopologyBase.cs");
		}

		// Token: 0x04000BCB RID: 3019
		private ITopologyConfigurationSession session;

		// Token: 0x04000BCC RID: 3020
		private ADObjectId rootId;

		// Token: 0x04000BCD RID: 3021
		private DateTime whenCreated;
	}
}
