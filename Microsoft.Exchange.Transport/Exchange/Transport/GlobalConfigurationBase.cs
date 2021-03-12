using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002D6 RID: 726
	internal abstract class GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT> where ConfigObjectT : ADConfigurationObject, new() where SingletonWrapperT : GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>, new()
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06002031 RID: 8241 RVA: 0x0007B49C File Offset: 0x0007969C
		// (remove) Token: 0x06002032 RID: 8242 RVA: 0x0007B4D0 File Offset: 0x000796D0
		public static event ADNotificationCallback ConfigurationObjectChanged;

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0007B503 File Offset: 0x00079703
		public static SingletonWrapperT Instance
		{
			get
			{
				return GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.instance;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0007B50A File Offset: 0x0007970A
		public ConfigObjectT ConfigObject
		{
			get
			{
				return this.configObject;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002035 RID: 8245
		protected abstract string ConfigObjectName { get; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002036 RID: 8246
		protected abstract string ReloadFailedString { get; }

		// Token: 0x06002037 RID: 8247 RVA: 0x0007B514 File Offset: 0x00079714
		public static void Start()
		{
			SingletonWrapperT singletonWrapperT = Activator.CreateInstance<SingletonWrapperT>();
			GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.instance = singletonWrapperT;
			ExTraceGlobals.ConfigurationTracer.TraceDebug<string>(0L, "Starting up monitoring of {0} configuration", singletonWrapperT.ConfigObjectName);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 105, "Start", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\GlobalConfigurationBase.cs");
			GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.cookie = ADNotificationAdapter.RegisterChangeNotification<ConfigObjectT>(singletonWrapperT.GetObjectId(tenantOrTopologyConfigurationSession), new ADNotificationCallback(GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.Notify));
			ExTraceGlobals.ConfigurationTracer.TraceDebug<string>(0L, "Registered change notification for {0} configuration with AD. Startup successfull.", singletonWrapperT.ConfigObjectName);
			ADOperationResult adoperationResult;
			if (!singletonWrapperT.Load(out adoperationResult))
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Could not load {0} configuration from AD. The service will be shut down", singletonWrapperT.ConfigObjectName);
				GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.Stop();
				throw new TransportComponentLoadFailedException(singletonWrapperT.ReloadFailedString, adoperationResult.Exception);
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0007B5F7 File Offset: 0x000797F7
		public static void Stop()
		{
			if (GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.cookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.cookie);
			}
			ExTraceGlobals.ConfigurationTracer.TraceDebug<string>(0L, "Shut down {0} configuration monitoring", GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.instance.ConfigObjectName);
		}

		// Token: 0x06002039 RID: 8249
		protected abstract ADObjectId GetObjectId(IConfigurationSession session);

		// Token: 0x0600203A RID: 8250 RVA: 0x0007B62B File Offset: 0x0007982B
		protected virtual void HandleObjectLoaded()
		{
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0007B62D File Offset: 0x0007982D
		protected virtual bool HandleObjectNotFound()
		{
			return false;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0007B658 File Offset: 0x00079858
		private static void Notify(ADNotificationEventArgs args)
		{
			SingletonWrapperT singletonWrapperT = Activator.CreateInstance<SingletonWrapperT>();
			ExTraceGlobals.ConfigurationTracer.TraceDebug<string>(0L, "{0} change notification received", singletonWrapperT.ConfigObjectName);
			ADOperationResult adoperationResult;
			if (singletonWrapperT.Load(out adoperationResult))
			{
				GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.instance = singletonWrapperT;
			}
			ADNotificationCallback configurationObjectChanged = GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.ConfigurationObjectChanged;
			if (configurationObjectChanged != null)
			{
				Delegate[] invocationList = configurationObjectChanged.GetInvocationList();
				Delegate[] array = invocationList;
				for (int i = 0; i < array.Length; i++)
				{
					ADNotificationCallback handler = (ADNotificationCallback)array[i];
					adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						handler(args);
					});
					if (adoperationResult != ADOperationResult.Success)
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<string, string, string>(0L, "An unhandled exception was raised by the {0} config. notification callback at {1}.{2}", singletonWrapperT.ConfigObjectName, handler.Method.DeclaringType.FullName, handler.Method.Name);
						GlobalConfigurationBase<ConfigObjectT, SingletonWrapperT>.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ComponentFailedTransportServerUpdate, null, new object[]
						{
							singletonWrapperT.ConfigObjectName
						});
					}
				}
			}
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0007B7EC File Offset: 0x000799EC
		private bool Load(out ADOperationResult result)
		{
			ConfigObjectT[] objects = null;
			result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 220, "Load", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\GlobalConfigurationBase.cs");
				objects = tenantOrTopologyConfigurationSession.Find<ConfigObjectT>(this.GetObjectId(tenantOrTopologyConfigurationSession), QueryScope.Base, null, null, 1);
			});
			if (!result.Succeeded)
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string, string>((long)this.GetHashCode(), "Error reading {0} object, {1}", this.ConfigObjectName, (result.Exception == null) ? "no exception" : result.Exception.Message);
				return false;
			}
			if (objects != null && objects.Length == 1)
			{
				this.configObject = objects[0];
				this.HandleObjectLoaded();
				ExTraceGlobals.ConfigurationTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} configuration object was read successfully.", this.ConfigObjectName);
				return true;
			}
			if (this.HandleObjectNotFound())
			{
				ExTraceGlobals.ConfigurationTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} configuration object could not be read, but it is not critical.", this.ConfigObjectName);
				return true;
			}
			ExTraceGlobals.ConfigurationTracer.TraceError<string>((long)this.GetHashCode(), "{0} configuration object could not be read.", this.ConfigObjectName);
			return false;
		}

		// Token: 0x040010DC RID: 4316
		private static ExEventLog eventLogger = new ExEventLog(new Guid("bad66201-f623-43e1-8fb4-bc5a8932c9f3"), TransportEventLog.GetEventSource());

		// Token: 0x040010DD RID: 4317
		private static SingletonWrapperT instance;

		// Token: 0x040010DE RID: 4318
		private static ADNotificationRequestCookie cookie;

		// Token: 0x040010DF RID: 4319
		private ConfigObjectT configObject;
	}
}
