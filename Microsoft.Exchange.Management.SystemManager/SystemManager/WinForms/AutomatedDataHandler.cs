using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000055 RID: 85
	internal class AutomatedDataHandler : AutomatedDataHandlerBase
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000C542 File Offset: 0x0000A742
		internal IList<ReaderTaskProfile> ReaderTaskProfileList
		{
			get
			{
				return this.readerTaskProfileList;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C54A File Offset: 0x0000A74A
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000C552 File Offset: 0x0000A752
		internal ICommandExecutionContextFactory ReaderExecutionContextFactory
		{
			get
			{
				return this.readerExecutionContextFactory;
			}
			set
			{
				this.readerExecutionContextFactory = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C55B File Offset: 0x0000A75B
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000C563 File Offset: 0x0000A763
		internal ICommandExecutionContextFactory SaverExecutionContextFactory
		{
			get
			{
				return this.saverExecutionContextFactory;
			}
			set
			{
				this.saverExecutionContextFactory = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C56C File Offset: 0x0000A76C
		public IList<SaverTaskProfile> SaverTaskProfileList
		{
			get
			{
				return this.saverTaskProfileList;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C574 File Offset: 0x0000A774
		public AutomatedDataHandler(Assembly resourceContainedAssembly, string resourceName, WorkUnit[] workUnits) : base(resourceContainedAssembly, resourceName)
		{
			this.readerTaskProfileList = base.ProfileBuilder.BuildReaderTaskProfile();
			this.saverTaskProfileList = base.ProfileBuilder.BuildSaverTaskProfile();
			this.pageToReaderTaskMapping = new PageToReaderTaskMapping(this.readerTaskProfileList, base.ProfileBuilder.BuildPageToDataObjectsMapping());
			if (workUnits != null)
			{
				base.EnableBulkEdit = true;
				foreach (ReaderTaskProfile readerTaskProfile in this.readerTaskProfileList)
				{
					readerTaskProfile.Runner = (readerTaskProfile.Runner as Reader).CreateBulkReader(readerTaskProfile.DataObjectName, base.DataObjectStore, base.Table);
				}
				foreach (SaverTaskProfile saverTaskProfile in this.saverTaskProfileList)
				{
					saverTaskProfile.Runner = (saverTaskProfile.Runner as Saver).CreateBulkSaver(workUnits.DeepCopy());
				}
				base.BreakOnError = false;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			if (this.pageToReaderTaskMapping.IsExecuted(pageName))
			{
				return;
			}
			if (base.Table.Rows.Count == 0)
			{
				base.Table.Rows.Add(base.Table.NewRow());
			}
			else
			{
				base.Table = base.Table.Copy();
			}
			using (CommandExecutionContext commandExecutionContext = this.CreateExecutionContextForReader())
			{
				WinFormsCommandInteractionHandler winFormsCommandInteractionHandler = interactionHandler as WinFormsCommandInteractionHandler;
				IUIService service = (winFormsCommandInteractionHandler == null) ? null : winFormsCommandInteractionHandler.UIService;
				commandExecutionContext.Open(service);
				foreach (ReaderTaskProfile readerTaskProfile in this.readerTaskProfileList)
				{
					if (readerTaskProfile.IsRunnable(base.Row, base.DataObjectStore) && this.pageToReaderTaskMapping.CanTaskExecuted(pageName, readerTaskProfile.Name))
					{
						readerTaskProfile.BuildParameters(base.Row, base.DataObjectStore);
						commandExecutionContext.Execute(readerTaskProfile, base.Row, base.DataObjectStore);
						base.DataObjectStore.UpdateDataObject(readerTaskProfile.DataObjectName, readerTaskProfile.DataObject);
						base.UpdateTable(base.Row, readerTaskProfile.DataObjectName, true);
					}
				}
			}
			if (this.pageToReaderTaskMapping.Count == 0)
			{
				base.Row.AcceptChanges();
			}
			this.pageToReaderTaskMapping.Execute(pageName);
			base.DataSource = base.Table;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000C824 File Offset: 0x0000AA24
		private CommandExecutionContext CreateExecutionContextForReader()
		{
			if (!base.EnableBulkEdit)
			{
				return this.readerExecutionContextFactory.CreateExecutionContext();
			}
			return new DummyExecutionContext();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000C83F File Offset: 0x0000AA3F
		public void InputValue(string columnName, object value)
		{
			base.Table.Columns[columnName].DefaultValue = value;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000C858 File Offset: 0x0000AA58
		public override void UpdateWorkUnits()
		{
			base.WorkUnits.Clear();
			foreach (SaverTaskProfile saverTaskProfile in this.SaverTaskProfileList)
			{
				if (saverTaskProfile.IsRunnable(base.Row, base.DataObjectStore))
				{
					saverTaskProfile.BuildParameters(base.Row, base.DataObjectStore);
					saverTaskProfile.UpdateWorkUnits(base.Row);
					base.WorkUnits.AddRange(saverTaskProfile.WorkUnits);
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public override void Cancel()
		{
			base.Cancel();
			foreach (SaverTaskProfile saverTaskProfile in this.saverTaskProfileList)
			{
				saverTaskProfile.Runner.Cancel();
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000C944 File Offset: 0x0000AB44
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			base.SavedResults.Clear();
			using (CommandExecutionContext commandExecutionContext = this.saverExecutionContextFactory.CreateExecutionContext())
			{
				WinFormsCommandInteractionHandler winFormsCommandInteractionHandler = interactionHandler as WinFormsCommandInteractionHandler;
				IUIService service = (winFormsCommandInteractionHandler == null) ? null : winFormsCommandInteractionHandler.UIService;
				commandExecutionContext.Open(service);
				foreach (SaverTaskProfile saverTaskProfile in this.saverTaskProfileList)
				{
					if (base.Cancelled)
					{
						break;
					}
					if (saverTaskProfile.IsRunnable(base.Row, base.DataObjectStore))
					{
						saverTaskProfile.BuildParameters(base.Row, base.DataObjectStore);
						try
						{
							saverTaskProfile.Runner.ProgressReport += base.OnProgressReport;
							commandExecutionContext.Execute(saverTaskProfile, base.Row, base.DataObjectStore);
						}
						finally
						{
							saverTaskProfile.Runner.ProgressReport -= base.OnProgressReport;
						}
						base.SavedResults.AddRange(saverTaskProfile.SavedResults);
						if (base.BreakOnError && !saverTaskProfile.IgnoreException && !saverTaskProfile.IsSucceeded)
						{
							break;
						}
					}
				}
				if (!base.HasWorkUnits || !base.WorkUnits.HasFailures)
				{
					if (commandExecutionContext.ShouldReload)
					{
						this.pageToReaderTaskMapping.Reset();
					}
					base.DataObjectStore.ClearModifiedColumns();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		internal override string CommandToRun
		{
			get
			{
				this.UpdateWorkUnits();
				StringBuilder stringBuilder = new StringBuilder();
				foreach (SaverTaskProfile saverTaskProfile in this.saverTaskProfileList)
				{
					if (saverTaskProfile.IsRunnable(base.Row, base.DataObjectStore))
					{
						saverTaskProfile.BuildParameters(base.Row, base.DataObjectStore);
						stringBuilder.Append(saverTaskProfile.CommandToRun);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public override string ModifiedParametersDescription
		{
			get
			{
				this.UpdateWorkUnits();
				StringBuilder stringBuilder = new StringBuilder();
				foreach (SaverTaskProfile saverTaskProfile in this.saverTaskProfileList)
				{
					if (saverTaskProfile.IsRunnable(base.Row, base.DataObjectStore))
					{
						saverTaskProfile.BuildParameters(base.Row, base.DataObjectStore);
						stringBuilder.Append(saverTaskProfile.ModifiedParametersDescription);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000CC00 File Offset: 0x0000AE00
		internal override bool TimeConsuming
		{
			get
			{
				return base.EnableBulkEdit;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		internal override bool HasViewPermissionForPage(string pageName)
		{
			if (EnvironmentAnalyzer.IsWorkGroup() || !base.ProfileBuilder.CanEnableUICustomization())
			{
				return true;
			}
			if (this.pageToReaderTaskMapping.ContainsKey(pageName))
			{
				return (from c in this.readerTaskProfileList
				where this.pageToReaderTaskMapping[pageName].Contains(c.Name)
				select c).Any((ReaderTaskProfile c) => c.HasPermission());
			}
			return true;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		internal override bool HasPermissionForProperty(string propertyName, bool canUpdate)
		{
			if (EnvironmentAnalyzer.IsWorkGroup() || !base.ProfileBuilder.CanEnableUICustomization())
			{
				return true;
			}
			ColumnProfile columnProfile = (from DataColumn c in base.Table.Columns
			where c.ColumnName == propertyName
			select c).First<DataColumn>().ExtendedProperties["ColumnProfile"] as ColumnProfile;
			string dataObjectName = columnProfile.DataObjectName;
			IEnumerable<ReaderTaskProfile> source = from c in this.readerTaskProfileList
			where c.DataObjectName == dataObjectName
			select c;
			ReaderTaskProfile readerTaskProfile = null;
			if (source.Count<ReaderTaskProfile>() > 0)
			{
				readerTaskProfile = source.First<ReaderTaskProfile>();
			}
			if (readerTaskProfile != null && !readerTaskProfile.HasPermission())
			{
				return false;
			}
			if (canUpdate)
			{
				if (columnProfile.IgnoreChangeTracking)
				{
					return true;
				}
				IEnumerable<SaverTaskProfile> source2 = from c in this.saverTaskProfileList
				where (c.Runner as Saver).GetConsumedDataObjectName() == dataObjectName && !string.IsNullOrEmpty(dataObjectName)
				select c;
				SaverTaskProfile saverTaskProfile = null;
				if (source2.Count<SaverTaskProfile>() > 0)
				{
					saverTaskProfile = source2.First<SaverTaskProfile>();
				}
				if (saverTaskProfile == null)
				{
					IEnumerable<SaverTaskProfile> enumerable = from c in this.saverTaskProfileList
					where (from p in c.ParameterProfileList
					where p.Reference == propertyName
					select p).Count<ParameterProfile>() > 0
					select c;
					using (IEnumerator<SaverTaskProfile> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							SaverTaskProfile saverTaskProfile2 = enumerator.Current;
							if (!saverTaskProfile2.HasPermission(columnProfile.MappingProperty))
							{
								return false;
							}
						}
						return true;
					}
				}
				if (saverTaskProfile != null && !saverTaskProfile.HasPermission(columnProfile.MappingProperty))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000E9 RID: 233
		private IList<ReaderTaskProfile> readerTaskProfileList;

		// Token: 0x040000EA RID: 234
		private IList<SaverTaskProfile> saverTaskProfileList;

		// Token: 0x040000EB RID: 235
		private PageToReaderTaskMapping pageToReaderTaskMapping;

		// Token: 0x040000EC RID: 236
		private ICommandExecutionContextFactory readerExecutionContextFactory = new MonadCommandExecutionContextFactory();

		// Token: 0x040000ED RID: 237
		private ICommandExecutionContextFactory saverExecutionContextFactory = new MonadCommandExecutionContextFactory();
	}
}
