using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning.Responders
{
	// Token: 0x02000408 RID: 1032
	public class ProvisioningSTXRestartServiceResponder : RestartServiceResponder
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x0008E0EC File Offset: 0x0008C2EC
		public static void InitTypeNameAndAssemblyPath(ResponderDefinition definition)
		{
			definition.AssemblyPath = ProvisioningSTXRestartServiceResponder.AssemblyPath;
			definition.TypeName = ProvisioningSTXRestartServiceResponder.TypeName;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0008E104 File Offset: 0x0008C304
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			bool flag = Convert.ToBoolean(base.Definition.ExtensionAttributes);
			if (flag)
			{
				string stateAttribute = string.Empty;
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
					StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookieResponder).BeginAppend(Dns.GetHostName(), false, new TimeSpan(0L), 1, ex.Message, "restart service", base.WindowsServiceName, stateAttribute, null);
					throw;
				}
				StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookieResponder).BeginAppend(Dns.GetHostName(), true, new TimeSpan(0L), 0, null, "restart service", base.WindowsServiceName, stateAttribute, null);
				return;
			}
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x040011CD RID: 4557
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040011CE RID: 4558
		private static readonly string TypeName = typeof(ProvisioningSTXRestartServiceResponder).FullName;
	}
}
