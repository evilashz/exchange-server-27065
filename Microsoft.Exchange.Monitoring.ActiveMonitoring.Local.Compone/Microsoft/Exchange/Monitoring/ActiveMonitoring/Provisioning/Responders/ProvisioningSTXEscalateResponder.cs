using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning.Responders
{
	// Token: 0x02000407 RID: 1031
	public class ProvisioningSTXEscalateResponder : EscalateResponder
	{
		// Token: 0x06001A23 RID: 6691 RVA: 0x0008DFB6 File Offset: 0x0008C1B6
		public static void InitTypeNameAndAssemblyPath(ResponderDefinition definition)
		{
			definition.AssemblyPath = ProvisioningSTXEscalateResponder.AssemblyPath;
			definition.TypeName = ProvisioningSTXEscalateResponder.TypeName;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0008DFD0 File Offset: 0x0008C1D0
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			bool flag = Convert.ToBoolean(base.Definition.ExtensionAttributes);
			string stateAttribute = string.Empty;
			if (flag)
			{
				StxLogType logType = this.GetLogType();
				try
				{
					FowardSyncEventRecord arbitrationEventLog = ForwardSyncEventlogUtil.GetArbitrationEventLog();
					if (arbitrationEventLog != null)
					{
						stateAttribute = arbitrationEventLog.ServiceInstanceName;
					}
					base.DoResponderWork(cancellationToken);
				}
				catch (Exception ex)
				{
					StxLoggerBase.GetLoggerInstance(logType).BeginAppend(Dns.GetHostName(), false, new TimeSpan(0L), 1, ex.Message, "escalate", stateAttribute, null, null);
					throw;
				}
				StxLoggerBase.GetLoggerInstance(logType).BeginAppend(Dns.GetHostName(), true, new TimeSpan(0L), 0, null, "escalate", stateAttribute, null, null);
				return;
			}
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0008E080 File Offset: 0x0008C280
		private StxLogType GetLogType()
		{
			if (base.Definition.Name == "ForwardSyncCompanyEscalate")
			{
				return StxLogType.TestForwardSyncCompanyResponder;
			}
			if (base.Definition.Name == "ForwardSyncCookieNotUpToDateEscalate")
			{
				return StxLogType.TestForwardSyncCookieResponder;
			}
			throw new Exception("The log Type is not supported yet");
		}

		// Token: 0x040011CB RID: 4555
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040011CC RID: 4556
		private static readonly string TypeName = typeof(ProvisioningSTXEscalateResponder).FullName;
	}
}
