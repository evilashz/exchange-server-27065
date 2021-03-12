using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001CE RID: 462
	internal abstract class StoreIntegrityCheckAssistantBase : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x00067C1C File Offset: 0x00065E1C
		public StoreIntegrityCheckAssistantBase(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060011CF RID: 4559
		protected abstract IntegrityCheckExecutionFlags ExecutionFlags { get; }

		// Token: 0x060011D0 RID: 4560 RVA: 0x00067C27 File Offset: 0x00065E27
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00067CAC File Offset: 0x00065EAC
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob job = invokeArgs.MailboxData as StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob;
			try
			{
				TimeBasedAssistant.TrackAdminRpcCalls(base.DatabaseInfo, "Client=Maintenance", delegate(ExRpcAdmin rpcAdmin)
				{
					rpcAdmin.StoreIntegrityCheckEx(this.DatabaseInfo.Guid, job.MailboxGuid, job.JobGuid, 6U, (uint)this.ExecutionFlags, null, null);
					customDataToLog.Add(new KeyValuePair<string, object>("StoreIntegrityCheck", job.JobGuid.ToString()));
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

		// Token: 0x060011D2 RID: 4562 RVA: 0x00067D78 File Offset: 0x00065F78
		protected List<MailboxData> GetReadyToExecuteJobs(IntegrityCheckQueryFlags flags)
		{
			List<MailboxData> list = new List<MailboxData>(10);
			PropValue[][] rows = null;
			try
			{
				TimeBasedAssistant.TrackAdminRpcCalls(base.DatabaseInfo, "Client=Maintenance", delegate(ExRpcAdmin rpcAdmin)
				{
					rows = rpcAdmin.StoreIntegrityCheckEx(this.DatabaseInfo.Guid, Guid.Empty, Guid.Empty, 2U, (uint)flags, null, StoreIntegrityCheckAssistantBase.columnsToRequest);
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
					list.Add(new StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob(base.DatabaseInfo.Guid, array[0].GetInt(), array[1].GetGuid(), array[2].GetGuid()));
				}
			}
			return list;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00067E68 File Offset: 0x00066068
		public void OnWindowEnd()
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00067E9E File Offset: 0x0006609E
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00067EA6 File Offset: 0x000660A6
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00067EAE File Offset: 0x000660AE
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000B07 RID: 2823
		private static PropTag[] columnsToRequest = new PropTag[]
		{
			PropTag.RtfSyncPrefixCount,
			PropTag.IsIntegJobMailboxGuid,
			PropTag.IsIntegJobGuid
		};

		// Token: 0x020001CF RID: 463
		private class StoreIntegrityCheckJob : AdminRpcMailboxData
		{
			// Token: 0x060011D8 RID: 4568 RVA: 0x00067EB6 File Offset: 0x000660B6
			internal StoreIntegrityCheckJob(Guid databaseGuid, int mailboxNumber, Guid mailboxGuid, Guid jobGuid) : base(mailboxGuid, mailboxNumber, databaseGuid)
			{
				this.jobGuid = jobGuid;
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00067EC9 File Offset: 0x000660C9
			internal Guid JobGuid
			{
				get
				{
					return this.jobGuid;
				}
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x00067ED4 File Offset: 0x000660D4
			public override bool Equals(object other)
			{
				if (other == null)
				{
					return false;
				}
				StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob storeIntegrityCheckJob = other as StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob;
				return storeIntegrityCheckJob != null && this.Equals(storeIntegrityCheckJob);
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x00067EF9 File Offset: 0x000660F9
			public bool Equals(StoreIntegrityCheckAssistantBase.StoreIntegrityCheckJob other)
			{
				return other != null && this.jobGuid == other.JobGuid && base.Equals(other);
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x00067F1C File Offset: 0x0006611C
			public override int GetHashCode()
			{
				return base.GetHashCode() ^ this.jobGuid.GetHashCode();
			}

			// Token: 0x04000B08 RID: 2824
			private readonly Guid jobGuid;
		}
	}
}
