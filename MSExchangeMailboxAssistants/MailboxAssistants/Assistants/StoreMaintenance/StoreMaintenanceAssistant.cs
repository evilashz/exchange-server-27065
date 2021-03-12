using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreMaintenance
{
	// Token: 0x020001CA RID: 458
	internal class StoreMaintenanceAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x00067832 File Offset: 0x00065A32
		public StoreMaintenanceAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0006783D File Offset: 0x00065A3D
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0006791C File Offset: 0x00065B1C
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			StoreMaintenanceAssistant.MaintenanceMailboxData maintenanceMailboxData = invokeArgs.MailboxData as StoreMaintenanceAssistant.MaintenanceMailboxData;
			try
			{
				TimeBasedAssistant.TrackAdminRpcCalls(base.DatabaseInfo, "Client=Maintenance", delegate(ExRpcAdmin rpcAdmin)
				{
					rpcAdmin.ExecuteTask(this.DatabaseInfo.Guid, maintenanceMailboxData.MaintenanceId, maintenanceMailboxData.MailboxNumber);
					if (maintenanceMailboxData.MailboxNumber == 0)
					{
						customDataToLog.Add(new KeyValuePair<string, object>("DatabaseMaintenance", maintenanceMailboxData.MaintenanceId.ToString()));
						return;
					}
					customDataToLog.Add(new KeyValuePair<string, object>("MailboxMaintenance", maintenanceMailboxData.MaintenanceId.ToString()));
					customDataToLog.Add(new KeyValuePair<string, object>("Mailbox", maintenanceMailboxData.MailboxGuid.ToString()));
				});
			}
			catch (MapiRetryableException innerException)
			{
				throw new SkipException(innerException);
			}
			catch (MapiPermanentException innerException2)
			{
				throw new SkipException(innerException2);
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000679D8 File Offset: 0x00065BD8
		protected List<MailboxData> GetMailboxesToProcess(MailboxTableFlags mailboxTableFlags)
		{
			List<MailboxData> list = new List<MailboxData>(10);
			PropValue[][] rows = null;
			try
			{
				TimeBasedAssistant.TrackAdminRpcCalls(base.DatabaseInfo, "Client=Maintenance", delegate(ExRpcAdmin rpcAdmin)
				{
					rows = rpcAdmin.GetMailboxTableInfo(this.DatabaseInfo.Guid, Guid.Empty, mailboxTableFlags, StoreMaintenanceAssistant.columnsToRequest);
				});
			}
			catch (MapiRetryableException innerException)
			{
				throw new SkipException(innerException);
			}
			catch (MapiPermanentException innerException2)
			{
				throw new SkipException(innerException2);
			}
			if (rows != null)
			{
				foreach (PropValue[] array in rows)
				{
					list.Add(new StoreMaintenanceAssistant.MaintenanceMailboxData(array[0].GetGuid(), base.DatabaseInfo.Guid, array[1].GetInt(), array[2].GetGuid()));
				}
			}
			return list;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00067AC8 File Offset: 0x00065CC8
		public override List<MailboxData> GetMailboxesToProcess()
		{
			return this.GetMailboxesToProcess(MailboxTableFlags.MaintenanceItems);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00067AD1 File Offset: 0x00065CD1
		public void OnWindowEnd()
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00067B06 File Offset: 0x00065D06
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00067B0E File Offset: 0x00065D0E
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00067B16 File Offset: 0x00065D16
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000B04 RID: 2820
		private static PropTag[] columnsToRequest = new PropTag[]
		{
			(PropTag)1035665480U,
			PropTag.MailboxNumber,
			PropTag.MailboxDSGuidGuid
		};

		// Token: 0x020001CB RID: 459
		private class MaintenanceMailboxData : AdminRpcMailboxData
		{
			// Token: 0x060011BA RID: 4538 RVA: 0x00067B1E File Offset: 0x00065D1E
			internal MaintenanceMailboxData(Guid maintenanceId, Guid databaseGuid, int mailboxNumber, Guid mailboxGuid) : base(mailboxGuid, mailboxNumber, databaseGuid)
			{
				this.maintenanceId = maintenanceId;
			}

			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x060011BB RID: 4539 RVA: 0x00067B31 File Offset: 0x00065D31
			internal Guid MaintenanceId
			{
				get
				{
					return this.maintenanceId;
				}
			}

			// Token: 0x060011BC RID: 4540 RVA: 0x00067B3C File Offset: 0x00065D3C
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				StoreMaintenanceAssistant.MaintenanceMailboxData maintenanceMailboxData = other as StoreMaintenanceAssistant.MaintenanceMailboxData;
				return maintenanceMailboxData != null && this.Equals(maintenanceMailboxData);
			}

			// Token: 0x060011BD RID: 4541 RVA: 0x00067B61 File Offset: 0x00065D61
			public bool Equals(StoreMaintenanceAssistant.MaintenanceMailboxData other)
			{
				return other != null && this.maintenanceId == other.MaintenanceId && base.Equals(other);
			}

			// Token: 0x060011BE RID: 4542 RVA: 0x00067B84 File Offset: 0x00065D84
			public override int GetHashCode()
			{
				return base.GetHashCode() ^ this.maintenanceId.GetHashCode();
			}

			// Token: 0x04000B05 RID: 2821
			private readonly Guid maintenanceId;
		}
	}
}
