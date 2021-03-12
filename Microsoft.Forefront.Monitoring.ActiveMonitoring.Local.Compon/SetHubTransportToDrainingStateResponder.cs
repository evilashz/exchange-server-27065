﻿using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200027D RID: 637
	public class SetHubTransportToDrainingStateResponder : RestartServiceResponder
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x000401B4 File Offset: 0x0003E3B4
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			Exception ex = ServerComponentStateManager.RunLocalRegistryOperationNoThrow(delegate
			{
				ServerComponentStates.UpdateLocalState(LocalServer.GetServer().Fqdn, ServerComponentRequest.Maintenance.ToString(), ServerComponentEnum.HubTransport.ToString(), ServiceState.Draining);
			});
			if (ex != null)
			{
				throw new ServerComponentApiException(DirectoryStrings.ServerComponentLocalRegistryError(ex.ToString()), ex);
			}
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.RestartService, base.WindowsServiceName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
			{
				this.InternalRestartService(startEntry, cancellationToken);
			});
		}
	}
}
