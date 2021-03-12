using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000197 RID: 407
	internal class QuarantineConfig
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000485AF File Offset: 0x000467AF
		internal string Mailbox
		{
			get
			{
				return this.quarantineMailbox;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000485B8 File Offset: 0x000467B8
		internal static ADObjectId GetConfigObjectId()
		{
			ADObjectId adobjectId = QuarantineConfig.session.GetOrgContainerId();
			adobjectId = adobjectId.GetChildId("Transport Settings");
			adobjectId = adobjectId.GetChildId("Message Hygiene");
			return adobjectId.GetChildId("ContentFilterConfig");
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00048628 File Offset: 0x00046828
		internal bool Load()
		{
			ContentFilterConfig[] configObjects = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId configObjectId = QuarantineConfig.GetConfigObjectId();
				configObjects = QuarantineConfig.session.Find<ContentFilterConfig>(configObjectId, QueryScope.Base, null, null, 1);
			});
			if (!adoperationResult.Succeeded)
			{
				QuarantineConfig.LogConfigError(adoperationResult.Exception);
				return false;
			}
			if (configObjects == null || configObjects.Length == 0 || configObjects[0].QuarantineMailbox == null || configObjects[0].QuarantineMailbox.Value.Length == 0)
			{
				this.quarantineMailbox = string.Empty;
				return true;
			}
			this.quarantineMailbox = configObjects[0].QuarantineMailbox.ToString();
			ExTraceGlobals.DSNTracer.TraceDebug<string>((long)this.GetHashCode(), "Quarantine configuration was loaded, mailbox: {0}", this.quarantineMailbox);
			return true;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00048700 File Offset: 0x00046900
		private static void LogConfigError(Exception configException)
		{
			ExTraceGlobals.DSNTracer.TraceError<string>(0L, "Failed to read quarantine-email address, exception message: {0}", configException.Message);
			string periodicKey = DateTime.UtcNow.Hour.ToString();
			QuarantineConfig.logger.LogEvent(TransportEventLogConstants.Tuple_DsnUnableToReadQuarantineConfig, periodicKey, null);
		}

		// Token: 0x04000964 RID: 2404
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.DSNTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000965 RID: 2405
		private static IConfigurationSession session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 55, "session", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\transport\\QuarantineConfig.cs");

		// Token: 0x04000966 RID: 2406
		private string quarantineMailbox;
	}
}
