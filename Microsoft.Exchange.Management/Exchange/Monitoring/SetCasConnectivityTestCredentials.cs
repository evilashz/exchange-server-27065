using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C4 RID: 1476
	[Cmdlet("set", "CasConnectivityTestCredentials")]
	public class SetCasConnectivityTestCredentials : TestCasConnectivity
	{
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000D1A55 File Offset: 0x000CFC55
		protected override string MonitoringEventSource
		{
			get
			{
				return "MSExchange Monitoring CasConnectivityTestCredentials";
			}
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000D1A5C File Offset: 0x000CFC5C
		protected override uint GetDefaultTimeOut()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000D1A68 File Offset: 0x000CFC68
		protected override List<CasTransactionOutcome> BuildPerformanceOutcomes(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mailboxFqdn)
		{
			throw new NotSupportedException("BuildPerformanceOutcomes() should not be called from SetCasConnectivityCredentials");
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000D1A74 File Offset: 0x000CFC74
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InitializeTopologyInformation();
			TaskLogger.LogExit();
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000D1A88 File Offset: 0x000CFC88
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				LocalizedException ex = null;
				if (!Datacenter.IsMultiTenancyEnabled())
				{
					ex = base.ResetAutomatedCredentialsOnMailboxServer(this.localServer, true);
				}
				if (ex == null)
				{
					base.WriteMonitoringEvent(1000, this.MonitoringEventSource, EventTypeEnumeration.Success, Strings.AllActiveSyncTransactionsSucceeded);
				}
				else if (ex is CasHealthUserNotFoundException)
				{
					base.WriteMonitoringEvent(1008, this.MonitoringEventSource, EventTypeEnumeration.Warning, Strings.InstructResetCredentials(base.ShortErrorMsgFromException(ex)));
				}
				else
				{
					base.WriteMonitoringEvent(1001, this.MonitoringEventSource, EventTypeEnumeration.Error, base.ShortErrorMsgFromException(ex));
				}
			}
			finally
			{
				if (base.MonitoringContext)
				{
					base.WriteMonitoringData();
				}
				TaskLogger.LogExit();
			}
		}
	}
}
