using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class PersistedGlobalActionEntry
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00006570 File Offset: 0x00004770
		public PersistedGlobalActionEntry()
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006578 File Offset: 0x00004778
		public PersistedGlobalActionEntry(RecoveryActionEntry entry)
		{
			this.Id = entry.Id;
			this.ResourceName = entry.ResourceName;
			this.RequestorName = entry.RequestorName;
			this.StartTime = entry.StartTime;
			this.ExpectedEndTime = entry.EndTime;
			this.Context = entry.Context;
			this.InstanceId = entry.InstanceId;
			this.LamProcessStartTime = entry.LamProcessStartTime;
			this.ReportedTime = ExDateTime.Now.LocalTime;
			this.ThrottleIdentity = entry.ThrottleIdentity;
			this.ThrottleParametersXml = entry.ThrottleParametersXml;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006616 File Offset: 0x00004816
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000661E File Offset: 0x0000481E
		public RecoveryActionId Id { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006627 File Offset: 0x00004827
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000662F File Offset: 0x0000482F
		public string ResourceName { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006638 File Offset: 0x00004838
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006640 File Offset: 0x00004840
		public string RequestorName { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006649 File Offset: 0x00004849
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00006651 File Offset: 0x00004851
		public DateTime StartTime { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000665A File Offset: 0x0000485A
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006662 File Offset: 0x00004862
		public DateTime ExpectedEndTime { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000666B File Offset: 0x0000486B
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006673 File Offset: 0x00004873
		public string InstanceId { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000667C File Offset: 0x0000487C
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006684 File Offset: 0x00004884
		public string Context { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000668D File Offset: 0x0000488D
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006695 File Offset: 0x00004895
		public DateTime LamProcessStartTime { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000669E File Offset: 0x0000489E
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000066A6 File Offset: 0x000048A6
		public string FinishEntryContext { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000066AF File Offset: 0x000048AF
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000066B7 File Offset: 0x000048B7
		public DateTime ReportedTime { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000066C0 File Offset: 0x000048C0
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000066C8 File Offset: 0x000048C8
		public string ThrottleIdentity { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000066D1 File Offset: 0x000048D1
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000066D9 File Offset: 0x000048D9
		public string ThrottleParametersXml { get; set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x000066E4 File Offset: 0x000048E4
		public static PersistedGlobalActionEntry ReadFromFile(RecoveryActionId id)
		{
			string fileName = PersistedGlobalActionEntry.GetFileName(id);
			PersistedGlobalActionEntry result = null;
			if (File.Exists(fileName))
			{
				try
				{
					result = Utilities.DeserializeObjectFromFile<PersistedGlobalActionEntry>(fileName);
				}
				catch (Exception ex)
				{
					ManagedAvailabilityCrimsonEvents.ActiveMonitoringUnexpectedError.Log<string, string>(string.Format("PersistedGlobalActionEntry.DeserializeObjectFromFile failed for {0}", fileName), ex.Message);
				}
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000673C File Offset: 0x0000493C
		public bool WriteToFile(TimeSpan timeout)
		{
			Task task = Task.Factory.StartNew(new Action(this.WriteToFile));
			return task.Wait(timeout);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006768 File Offset: 0x00004968
		public void WriteToFile()
		{
			string fileName = PersistedGlobalActionEntry.GetFileName(this.Id);
			try
			{
				Utilities.SerializeObjectToFile(this, fileName);
			}
			catch (Exception ex)
			{
				ManagedAvailabilityCrimsonEvents.ActiveMonitoringUnexpectedError.Log<string, string>(string.Format("PersistedGlobalActionEntry.SerializeObjectToFile failed for {0}", fileName), ex.Message);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000067B8 File Offset: 0x000049B8
		public RecoveryActionEntry ConvertToRecoveryActionStartEntry()
		{
			return RecoveryActionHelper.ConstructStartActionEntry(this.Id, this.ResourceName, this.RequestorName, this.StartTime, this.ExpectedEndTime, this.ThrottleIdentity, this.ThrottleParametersXml, this.Context, this.InstanceId, this.LamProcessStartTime);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006808 File Offset: 0x00004A08
		internal static string GetFileName(RecoveryActionId id)
		{
			return Path.Combine(ExchangeSetupContext.InstallPath, string.Format("Logging\\Monitoring\\PersistedGlobalRecoveryAction\\{0}.xml", id));
		}
	}
}
