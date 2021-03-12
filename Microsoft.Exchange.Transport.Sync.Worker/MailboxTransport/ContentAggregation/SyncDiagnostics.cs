using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncDiagnostics : IDiagnosable
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000766C File Offset: 0x0000586C
		public SyncDiagnostics(AggregationScheduler aggregationScheduler, RemoteServerHealthManager remoteServerHealthManager)
		{
			SyncUtilities.ThrowIfArgumentNull("aggregationScheduler", aggregationScheduler);
			SyncUtilities.ThrowIfArgumentNull("remoteServerHealthManager", remoteServerHealthManager);
			this.aggregationScheduler = aggregationScheduler;
			this.remoteServerHealthManager = remoteServerHealthManager;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007698 File Offset: 0x00005898
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "SyncWorker";
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000076A0 File Offset: 0x000058A0
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("SyncWorker");
			if (string.IsNullOrEmpty(parameters.Argument) || string.Equals(parameters.Argument, "help", StringComparison.OrdinalIgnoreCase))
			{
				xelement.Add(new XElement("help", "Supported argument(s): basic, verbose, help, sub-components:scheduler, remoteserverhealthmanager. Specifying a component will only return info for that component. E.g. verbose scheduler. That will only return verbose information for scheduler and nothing for the other components."));
			}
			else
			{
				bool verbose = 0 <= parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase);
				bool flag = 0 <= parameters.Argument.IndexOf("scheduler", StringComparison.OrdinalIgnoreCase);
				bool flag2 = 0 <= parameters.Argument.IndexOf("remoteserverhealthmanager", StringComparison.OrdinalIgnoreCase);
				bool flag3 = flag || flag2;
				if (!flag3 || flag)
				{
					xelement.Add(this.aggregationScheduler.GetDiagnosticInfo(verbose));
				}
				if (!flag3 || flag2)
				{
					xelement.Add(this.remoteServerHealthManager.GetDiagnosticInfo(verbose));
				}
			}
			return xelement;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007781 File Offset: 0x00005981
		internal void Register()
		{
			ProcessAccessManager.RegisterComponent(this);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007789 File Offset: 0x00005989
		internal void Unregister()
		{
			ProcessAccessManager.UnregisterComponent(this);
		}

		// Token: 0x040000A3 RID: 163
		private const string ProcessAccessManagerComponentName = "SyncWorker";

		// Token: 0x040000A4 RID: 164
		private readonly AggregationScheduler aggregationScheduler;

		// Token: 0x040000A5 RID: 165
		private readonly RemoteServerHealthManager remoteServerHealthManager;
	}
}
