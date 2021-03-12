using System;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A2 RID: 162
	internal class XmlDefinitionReaderWorkItem : MaintenanceWorkItem
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x000205D4 File Offset: 0x0001E7D4
		public static MaintenanceDefinition CreateDefinition()
		{
			MaintenanceDefinition maintenanceDefinition = new MaintenanceDefinition();
			maintenanceDefinition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
			maintenanceDefinition.TypeName = typeof(XmlDefinitionReaderWorkItem).FullName;
			maintenanceDefinition.Name = typeof(XmlDefinitionReaderWorkItem).Name;
			maintenanceDefinition.ServiceName = ExchangeComponent.Monitoring.Name;
			maintenanceDefinition.RecurrenceIntervalSeconds = 0;
			maintenanceDefinition.TimeoutSeconds = 300;
			maintenanceDefinition.Enabled = true;
			StartupNotification.SetStartupNotificationDefinition(maintenanceDefinition, LocalDataAccess.EndpointManagerNotificationId, 300);
			maintenanceDefinition.SyncExtensionAttributes(false);
			maintenanceDefinition.StartTime = DateTime.UtcNow;
			return maintenanceDefinition;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00020670 File Offset: 0x0001E870
		protected override void DoWork(CancellationToken cancellationToken)
		{
			StringBuilder stringBuilder = new StringBuilder();
			WorkDefinitionDeploymentFileReader workDefinitionDeploymentFileReader = new WorkDefinitionDeploymentFileReader(Settings.FileStorageLocation, base.TraceContext);
			foreach (WorkDefinition workDefinition in workDefinitionDeploymentFileReader.GetWorkDefinitions(null))
			{
				try
				{
					if (workDefinition != null && !string.IsNullOrEmpty(workDefinition.Name) && !string.IsNullOrEmpty(workDefinition.AssemblyPath))
					{
						if (workDefinition is ProbeDefinition)
						{
							base.Broker.AddWorkDefinition<ProbeDefinition>((ProbeDefinition)workDefinition, base.TraceContext);
						}
						else if (workDefinition is MonitorDefinition)
						{
							base.Broker.AddWorkDefinition<MonitorDefinition>((MonitorDefinition)workDefinition, base.TraceContext);
						}
						else if (workDefinition is ResponderDefinition)
						{
							base.Broker.AddWorkDefinition<ResponderDefinition>((ResponderDefinition)workDefinition, base.TraceContext);
						}
						else if (workDefinition is MaintenanceDefinition)
						{
							base.Broker.AddWorkDefinition<MaintenanceDefinition>((MaintenanceDefinition)workDefinition, base.TraceContext);
						}
						else
						{
							stringBuilder.Append(workDefinition.Name);
							stringBuilder.Append(": definition type is not supported\n");
						}
					}
				}
				catch (Exception ex)
				{
					stringBuilder.Append(workDefinition.Name);
					stringBuilder.AppendFormat(": {0}\n", ex.Message);
				}
			}
			if (workDefinitionDeploymentFileReader.exceptionList != null && workDefinitionDeploymentFileReader.exceptionList.Count > 0)
			{
				foreach (Exception ex2 in workDefinitionDeploymentFileReader.exceptionList)
				{
					stringBuilder.AppendFormat("\n{0}\n", ex2.Message);
				}
			}
			if (stringBuilder.Length > 0)
			{
				string message = string.Format("The following definitions were not created successfully:\n{0}", stringBuilder.ToString());
				throw new Exception(message);
			}
		}
	}
}
