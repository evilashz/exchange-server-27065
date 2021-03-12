using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020001A6 RID: 422
	internal class ServerPickerManager : ADConfigurationLoader<Server, PickerServerList>
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00041B34 File Offset: 0x0003FD34
		public ServerPickerManager(string serviceName, ServerRole serverRole, Trace tracer) : this(serviceName, serverRole, tracer, ServerPickerClient.Default)
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00041B44 File Offset: 0x0003FD44
		public ServerPickerManager(string serviceName, ServerRole serverRole, Trace tracer, ServerPickerClient serverPickerClient)
		{
			this.serverRole = serverRole;
			this.serviceName = serviceName;
			this.tracer = tracer;
			this.serverPickerClient = serverPickerClient;
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 98, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\ServerPicker\\ServerPickerManager.cs");
			base.ReadConfiguration();
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x0600100E RID: 4110 RVA: 0x00041BA8 File Offset: 0x0003FDA8
		// (remove) Token: 0x0600100F RID: 4111 RVA: 0x00041BE0 File Offset: 0x0003FDE0
		public event EventHandler OnServersUpdated;

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00041C15 File Offset: 0x0003FE15
		public ServerRole ServerRole
		{
			get
			{
				base.CheckDisposed();
				return this.serverRole;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00041C23 File Offset: 0x0003FE23
		public ServerComponentEnum Component
		{
			get
			{
				base.CheckDisposed();
				if (this.serverRole == ServerRole.HubTransport)
				{
					return ServerComponentEnum.HubTransport;
				}
				return ServerComponentEnum.None;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00041C38 File Offset: 0x0003FE38
		public Trace Tracer
		{
			get
			{
				base.CheckDisposed();
				return this.tracer;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00041C46 File Offset: 0x0003FE46
		public ServerPickerClient ServerPickerClient
		{
			get
			{
				base.CheckDisposed();
				return this.serverPickerClient;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00041C54 File Offset: 0x0003FE54
		public ITopologyConfigurationSession ConfigurationSession
		{
			get
			{
				base.CheckDisposed();
				return this.configurationSession;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00041C64 File Offset: 0x0003FE64
		public bool HasValidConfiguration
		{
			get
			{
				bool result;
				lock (this.serversLock)
				{
					result = (this.servers != null && this.servers.IsValid);
				}
				return result;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public PickerServerList GetPickerServerList()
		{
			base.CheckDisposed();
			PickerServerList result;
			lock (this.serversLock)
			{
				this.servers.AddRef();
				result = this.servers;
			}
			return result;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00041D0C File Offset: 0x0003FF0C
		internal void UpdateServersInRetryPerfmon(PickerServerList pickerServerList)
		{
			base.CheckDisposed();
			lock (this.serversLock)
			{
				if (this.servers == pickerServerList)
				{
					this.serverPickerClient.UpdateServersInRetryPerfmon(pickerServerList.RetryServerCount);
					if (pickerServerList.RetryServerCount == pickerServerList.Count)
					{
						this.serverPickerClient.LogNoActiveServerEvent(this.serverRole.ToString());
						this.serverPickerClient.UpdateServersPercentageActivePerfmon(0);
					}
					else
					{
						int percentage = (pickerServerList.Count == 0) ? 0 : ((pickerServerList.Count - pickerServerList.RetryServerCount) * 100 / pickerServerList.Count);
						this.serverPickerClient.UpdateServersPercentageActivePerfmon(percentage);
					}
				}
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00041DCC File Offset: 0x0003FFCC
		internal void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			base.CheckDisposed();
			object[] array = new object[messageArgs.Length + 1];
			array[0] = this.serviceName;
			Array.Copy(messageArgs, 0, array, 1, messageArgs.Length);
			ServerPickerManager.EventLog.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00041E10 File Offset: 0x00040010
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.InternalDispose(disposing);
				lock (this.serversLock)
				{
					if (this.servers != null)
					{
						this.servers.Release();
						this.servers = null;
					}
				}
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00041E70 File Offset: 0x00040070
		protected override void LogFailure(ADConfigurationLoader<Server, PickerServerList>.FailureLocation failureLocation, Exception exception)
		{
			this.tracer.TraceError<ADConfigurationLoader<Server, PickerServerList>.FailureLocation, Exception>(0L, "Failed to perform {0} due to exception {1}", failureLocation, exception);
			if (exception == null)
			{
				this.LogEvent(ApplicationLogicEventLogConstants.Tuple_TopologyException, "Null Exception", new object[]
				{
					"null"
				});
				return;
			}
			if (exception is LocalServerNotFoundException)
			{
				this.tracer.TraceError(0L, "No local server");
				this.LogEvent(this.HasValidConfiguration ? ApplicationLogicEventLogConstants.Tuple_NoLocalServerWarning : ApplicationLogicEventLogConstants.Tuple_NoLocalServer, exception.GetType().FullName, new object[]
				{
					exception
				});
				return;
			}
			this.LogEvent(ApplicationLogicEventLogConstants.Tuple_TopologyException, exception.GetType().FullName, new object[]
			{
				exception
			});
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00041F23 File Offset: 0x00040123
		protected override void PreAdOperation(ref PickerServerList newServers)
		{
			newServers = new PickerServerList(this);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00041F2D File Offset: 0x0004012D
		protected override void AdOperation(ref PickerServerList newServers)
		{
			newServers.LoadFromAD(this.servers);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00041F3C File Offset: 0x0004013C
		protected override void PostAdOperation(PickerServerList newServers, bool wasSuccessful)
		{
			this.lastLoadTime = DateTime.UtcNow;
			if (newServers.IsValid)
			{
				this.SetServers(newServers);
				this.wasLastLoadSuccessful = true;
				return;
			}
			lock (this.serversLock)
			{
				if (this.servers == null)
				{
					this.SetServers(newServers);
				}
				else
				{
					newServers.Release();
				}
			}
			this.wasLastLoadSuccessful = false;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00041FE0 File Offset: 0x000401E0
		protected override void OnServerChangeCallback(ADNotificationEventArgs args)
		{
			if (args != null)
			{
				this.tracer.TraceDebug<string, string>(0L, "OnServerChangeCallback notification change type {0}, object ID {1}", args.ChangeType.ToString(), (args.Id == null) ? "(null)" : args.Id.ToString());
			}
			if (args.ChangeType == ADNotificationChangeType.ModifyOrAdd && args.Id != null)
			{
				Server server;
				ADOperationResult adoperationResult;
				if (ADNotificationAdapter.TryReadConfiguration<Server>(() => this.configurationSession.Read<Server>(args.Id), out server, out adoperationResult))
				{
					lock (this.serversLock)
					{
						if (this.servers != null && this.servers.IsChangeIgnorable(server))
						{
							return;
						}
						goto IL_107;
					}
				}
				this.tracer.TraceError<ADObjectId, Exception>(0L, "Failed to read server object with Id {0} due to {1}", args.Id, adoperationResult.Exception);
			}
			IL_107:
			base.ReadConfiguration();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00042128 File Offset: 0x00040328
		private void SetServers(PickerServerList newServers)
		{
			lock (this.serversLock)
			{
				if (this.servers != newServers)
				{
					if (this.servers != null)
					{
						this.servers.Release();
					}
					this.servers = newServers;
				}
				this.serverPickerClient.UpdateServersTotalPerfmon(this.servers.Count);
				this.UpdateServersInRetryPerfmon(this.servers);
				if (this.servers.Count == 0)
				{
					this.LogEvent(ApplicationLogicEventLogConstants.Tuple_NoServerInSite, this.serverRole.ToString(), new object[]
					{
						this.serverRole.ToString()
					});
				}
			}
			EventHandler eventHandler = this.OnServersUpdated;
			if (eventHandler != null)
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					eventHandler(this, null);
				});
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00042228 File Offset: 0x00040428
		public XElement GetDiagnosticInfo(string argument)
		{
			PickerServerList pickerServerList = this.GetPickerServerList();
			XElement result;
			try
			{
				result = new XElement("ServerPickerManager", new object[]
				{
					new XElement("serverRole", this.serverRole),
					new XElement("hasValidConfiguration", this.HasValidConfiguration),
					new XElement("lastLoadTime", this.lastLoadTime),
					new XElement("wasLastLoadSuccessful", this.wasLastLoadSuccessful),
					pickerServerList.GetDiagnosticInfo(argument)
				});
			}
			finally
			{
				pickerServerList.Release();
			}
			return result;
		}

		// Token: 0x04000893 RID: 2195
		private readonly ServerRole serverRole;

		// Token: 0x04000894 RID: 2196
		private readonly ITopologyConfigurationSession configurationSession;

		// Token: 0x04000895 RID: 2197
		private readonly string serviceName;

		// Token: 0x04000896 RID: 2198
		private readonly Trace tracer;

		// Token: 0x04000897 RID: 2199
		private readonly ServerPickerClient serverPickerClient;

		// Token: 0x04000898 RID: 2200
		private static Guid eventLogComponentGuid = new Guid("{FF503692-3FF7-48c9-9BF9-8586487B4E36}");

		// Token: 0x04000899 RID: 2201
		public static readonly ExEventLog EventLog = new ExEventLog(ServerPickerManager.eventLogComponentGuid, "MSExchangeApplicationLogic");

		// Token: 0x0400089A RID: 2202
		private PickerServerList servers;

		// Token: 0x0400089B RID: 2203
		private object serversLock = new object();

		// Token: 0x0400089C RID: 2204
		private DateTime lastLoadTime;

		// Token: 0x0400089D RID: 2205
		private bool wasLastLoadSuccessful;
	}
}
