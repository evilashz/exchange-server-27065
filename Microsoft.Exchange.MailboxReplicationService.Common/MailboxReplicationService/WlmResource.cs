using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000267 RID: 615
	internal abstract class WlmResource : ResourceBase
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x00040EBC File Offset: 0x0003F0BC
		public WlmResource(WorkloadType workloadType)
		{
			this.WlmWorkloadType = workloadType;
			this.ResourceGuid = Guid.Empty;
			base.ConfigContext = new GenericSettingsContext("WorkloadType", this.WlmWorkloadType.ToString(), base.ConfigContext);
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00040EFC File Offset: 0x0003F0FC
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x00040F04 File Offset: 0x0003F104
		public WorkloadType WlmWorkloadType { get; private set; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00040F0D File Offset: 0x0003F10D
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x00040F15 File Offset: 0x0003F115
		public Guid ResourceGuid { get; protected set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00040F20 File Offset: 0x0003F120
		public string WlmWorkloadTypeSuffix
		{
			get
			{
				WorkloadType wlmWorkloadType = this.WlmWorkloadType;
				if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
				{
					return "-HiPri";
				}
				switch (wlmWorkloadType)
				{
				case WorkloadType.MailboxReplicationServiceInternalMaintenance:
					return "-IM";
				case WorkloadType.MailboxReplicationServiceInteractive:
					return "-I";
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00040F64 File Offset: 0x0003F164
		public WorkloadClassification WorkloadClassification
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.GetObject<IWorkloadSettings>(this.WlmWorkloadType, new object[0]).Classification;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00040FA0 File Offset: 0x0003F1A0
		public int DynamicCapacity
		{
			get
			{
				int num = this.StaticCapacity;
				foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
				{
					if (wlmResourceHealthMonitor.DynamicCapacity < num)
					{
						num = wlmResourceHealthMonitor.DynamicCapacity;
					}
				}
				return num;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00041004 File Offset: 0x0003F204
		public override bool IsUnhealthy
		{
			get
			{
				foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
				{
					if (wlmResourceHealthMonitor.IsUnhealthy)
					{
						return true;
					}
				}
				return base.IsUnhealthy;
			}
		}

		// Token: 0x06001F28 RID: 7976
		public abstract List<WlmResourceHealthMonitor> GetWlmResources();

		// Token: 0x06001F29 RID: 7977 RVA: 0x00041064 File Offset: 0x0003F264
		public void UpdateHealthState(bool logHealthState)
		{
			this.InitializeMonitors();
			foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
			{
				wlmResourceHealthMonitor.UpdateHealthState(logHealthState);
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000410C0 File Offset: 0x0003F2C0
		protected override void VerifyDynamicCapacity(ReservationBase reservation)
		{
			this.InitializeMonitors();
			foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
			{
				wlmResourceHealthMonitor.VerifyDynamicCapacity(reservation);
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0004111C File Offset: 0x0003F31C
		protected override void AddReservation(ReservationBase reservation)
		{
			foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
			{
				wlmResourceHealthMonitor.AddReservation(reservation);
			}
			base.AddReservation(reservation);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00041178 File Offset: 0x0003F378
		protected override XElement GetDiagnosticInfoInternal(MRSDiagnosticArgument arguments)
		{
			this.InitializeMonitors();
			ResourceDiagnosticInfoXML resourceDiagnosticInfoXML = new ResourceDiagnosticInfoXML
			{
				ResourceName = this.ResourceName,
				ResourceGuid = this.ResourceGuid,
				ResourceType = this.ResourceType,
				StaticCapacity = this.StaticCapacity,
				DynamicCapacity = this.DynamicCapacity,
				Utilization = base.Utilization,
				IsUnhealthy = this.IsUnhealthy,
				WlmWorkloadType = this.WlmWorkloadType.ToString(),
				TransferRatePerMin = this.TransferRate.GetValue()
			};
			if (this.healthMonitors.Count > 0)
			{
				resourceDiagnosticInfoXML.WlmResourceHealthMonitors = new List<WlmResourceHealthMonitorDiagnosticInfoXML>();
				foreach (WlmResourceHealthMonitor wlmResourceHealthMonitor in this.healthMonitors)
				{
					WlmResourceHealthMonitorDiagnosticInfoXML wlmResourceHealthMonitorDiagnosticInfoXML = wlmResourceHealthMonitor.PopulateDiagnosticInfo(arguments);
					if (wlmResourceHealthMonitorDiagnosticInfoXML != null)
					{
						resourceDiagnosticInfoXML.WlmResourceHealthMonitors.Add(wlmResourceHealthMonitorDiagnosticInfoXML);
					}
				}
			}
			return resourceDiagnosticInfoXML.ToDiagnosticInfo(null);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00041288 File Offset: 0x0003F488
		private void InitializeMonitors()
		{
			if (this.healthMonitors != null)
			{
				return;
			}
			this.healthMonitors = this.GetWlmResources();
		}

		// Token: 0x04000C89 RID: 3209
		private List<WlmResourceHealthMonitor> healthMonitors;
	}
}
