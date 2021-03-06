using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008F RID: 143
	internal class ADConfigurationProvider : IPushNotificationPublisherConfigurationProvider
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public IEnumerable<IPushNotificationRawSettings> LoadSettings(bool ignoreErrors = true)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 37, "LoadSettings", "f:\\15.00.1497\\sources\\dev\\PushNotifications\\src\\publishers\\Configuration\\ADConfigurationProvider.cs");
			if (!ignoreErrors)
			{
				return topologyConfigurationSession.FindAllPaged<PushNotificationApp>();
			}
			string text = null;
			try
			{
				return topologyConfigurationSession.FindAllPaged<PushNotificationApp>();
			}
			catch (ADTransientException exception)
			{
				text = exception.ToTraceString();
			}
			catch (ADOperationException exception2)
			{
				text = exception2.ToTraceString();
			}
			if (text != null)
			{
				PushNotificationsCrimsonEvents.ErrorReadingConfiguration.Log<string>(text);
				ExTraceGlobals.PushNotificationServiceTracer.TraceWarning<string>((long)this.GetHashCode(), "An error was generated when attempting to read the server configuration '{0}'.", text);
			}
			return new PushNotificationApp[0];
		}
	}
}
