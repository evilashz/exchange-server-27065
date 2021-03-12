using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000D4 RID: 212
	public class MembersSaveTask : Saver
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x00019306 File Offset: 0x00017506
		public MembersSaveTask() : this(null, null)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00019310 File Offset: 0x00017510
		public MembersSaveTask(string workUnitTextColumn, string workUnitIconColumn) : base(workUnitTextColumn, workUnitIconColumn)
		{
			this.dataHandler = new ExchangeDataHandler(false);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00019332 File Offset: 0x00017532
		public override void Cancel()
		{
			this.dataHandler.Cancel();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00019340 File Offset: 0x00017540
		public override void Run(object interactionHandler, DataRow row, DataObjectStore store)
		{
			this.dataHandler.ProgressReport += base.OnProgressReport;
			try
			{
				this.dataHandler.Save(interactionHandler as CommandInteractionHandler);
				if (this.dataHandler.HasWorkUnits && !this.dataHandler.WorkUnits.HasFailures)
				{
					MultiValuedProperty<ADObjectId> multiValuedProperty = row["Members"] as MultiValuedProperty<ADObjectId>;
					if (multiValuedProperty != null)
					{
						multiValuedProperty.ResetChangeTracking();
					}
				}
			}
			finally
			{
				this.dataHandler.ProgressReport -= base.OnProgressReport;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000193D8 File Offset: 0x000175D8
		public override bool IsRunnable(DataRow row, DataObjectStore store)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = row["Members"] as MultiValuedProperty<ADObjectId>;
			return multiValuedProperty != null && (multiValuedProperty.Added.Length > 0 || multiValuedProperty.Removed.Length > 0) && base.IsRunnable(row, store);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00019423 File Offset: 0x00017623
		public override object WorkUnits
		{
			get
			{
				return this.dataHandler.WorkUnits;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00019430 File Offset: 0x00017630
		public override List<object> SavedResults
		{
			get
			{
				return this.dataHandler.SavedResults;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00019440 File Offset: 0x00017640
		public override void UpdateWorkUnits(DataRow row)
		{
			ADObjectId identity = row["Identity"] as ADObjectId;
			this.members = (row["Members"] as MultiValuedProperty<ADObjectId>);
			if (this.members != null && (this.members.Added.Length > 0 || this.members.Removed.Length > 0))
			{
				this.dataHandler.DataHandlers.Clear();
				foreach (ADObjectId member in this.members.Added)
				{
					this.dataHandler.DataHandlers.Add(this.CreateDataHandler("Add-DistributionGroupMember", identity, member));
				}
				foreach (ADObjectId member2 in this.members.Removed)
				{
					this.dataHandler.DataHandlers.Add(this.CreateDataHandler("Remove-DistributionGroupMember", identity, member2));
				}
			}
			this.dataHandler.UpdateWorkUnits();
			this.dataHandler.ResetCancel();
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00019555 File Offset: 0x00017755
		public override string CommandToRun
		{
			get
			{
				return this.dataHandler.CommandToRun;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00019564 File Offset: 0x00017764
		public override string ModifiedParametersDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.members != null)
				{
					if (this.members.Added.Length > 0)
					{
						stringBuilder.AppendLine(string.Format("Members: {0} {1}", Strings.AddMembersToGroup, MonadCommand.FormatParameterValue(this.members.Added)));
					}
					if (this.members.Removed.Length > 0)
					{
						stringBuilder.AppendLine(string.Format("Members: {0} {1}", Strings.RemoveMembersFromGroup, MonadCommand.FormatParameterValue(this.members.Removed)));
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000195FB File Offset: 0x000177FB
		public override void BuildParameters(DataRow row, DataObjectStore store, IList<ParameterProfile> paramInfos)
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00019600 File Offset: 0x00017800
		private SingleTaskDataHandler CreateDataHandler(string commandText, ADObjectId identity, ADObjectId member)
		{
			SingleTaskDataHandler singleTaskDataHandler;
			if (this.workUnits.Length > 0)
			{
				singleTaskDataHandler = new BulkSaveDataHandler(this.workUnits.DeepCopy(), commandText);
			}
			else
			{
				singleTaskDataHandler = new SingleTaskDataHandler(commandText);
				singleTaskDataHandler.Parameters.AddWithValue("Identity", identity);
			}
			singleTaskDataHandler.Parameters.AddWithValue("Member", member);
			return singleTaskDataHandler;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00019658 File Offset: 0x00017858
		public override Saver CreateBulkSaver(WorkUnit[] workUnits)
		{
			this.workUnits = workUnits;
			return this;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00019664 File Offset: 0x00017864
		internal void UpdateConnection(MonadConnection connection)
		{
			foreach (DataHandler dataHandler in this.dataHandler.DataHandlers)
			{
				SingleTaskDataHandler singleTaskDataHandler = (SingleTaskDataHandler)dataHandler;
				singleTaskDataHandler.Command.Connection = connection;
			}
		}

		// Token: 0x0400038D RID: 909
		private DataHandler dataHandler;

		// Token: 0x0400038E RID: 910
		private WorkUnit[] workUnits = new WorkUnit[0];

		// Token: 0x0400038F RID: 911
		private MultiValuedProperty<ADObjectId> members;
	}
}
