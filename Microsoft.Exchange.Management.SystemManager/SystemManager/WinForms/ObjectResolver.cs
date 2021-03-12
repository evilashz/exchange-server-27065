using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022E RID: 558
	public class ObjectResolver
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x0006FEE5 File Offset: 0x0006E0E5
		public ObjectResolver(ObjectPicker objectPicker)
		{
			this.loader = objectPicker.CreateDataTableLoaderForResolver();
			this.loader.RefreshCompleted += this.loader_RefreshCompleted;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0006FF10 File Offset: 0x0006E110
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0006FF18 File Offset: 0x0006E118
		[DefaultValue(false)]
		public bool IsResolving
		{
			get
			{
				return this.isResolving;
			}
			set
			{
				this.isResolving = value;
				this.OnIsResolvingChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0006FF2C File Offset: 0x0006E12C
		public DataTable ResolvedObjects
		{
			get
			{
				return this.loader.Table;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006FF39 File Offset: 0x0006E139
		public RefreshableComponent Refresher
		{
			get
			{
				return this.loader;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006FF44 File Offset: 0x0006E144
		public void ResolveObjects(ADObjectId rootId, QueryFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			ResultsLoaderProfile resultsLoaderProfile = this.loader.RefreshArgument as ResultsLoaderProfile;
			if (resultsLoaderProfile != null)
			{
				resultsLoaderProfile.Scope = rootId;
				resultsLoaderProfile.InputValue("RecipientPreviewFilter", filter);
			}
			this.IsResolving = true;
			this.loader.Refresh(NullProgress.Value);
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0006FF9D File Offset: 0x0006E19D
		// (set) Token: 0x060019CA RID: 6602 RVA: 0x0006FFA8 File Offset: 0x0006E1A8
		[DefaultValue(true)]
		public bool PrefillBeforeResolving
		{
			get
			{
				return this.prefillBeforeResolving;
			}
			set
			{
				if (value != this.prefillBeforeResolving)
				{
					this.prefillBeforeResolving = value;
					if (this.prefillBeforeResolving)
					{
						this.loader.Table.Columns.Add("LoadStatusColumn", typeof(ItemLoadStatus));
						return;
					}
					this.loader.Table.Columns.Remove("LoadStatusColumn");
				}
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x0007000D File Offset: 0x0006E20D
		// (set) Token: 0x060019CC RID: 6604 RVA: 0x00070015 File Offset: 0x0006E215
		[DefaultValue(false)]
		public bool FastResolving { get; set; }

		// Token: 0x060019CD RID: 6605 RVA: 0x00070020 File Offset: 0x0006E220
		private void UpdateNonResolvedObjects(DataTable table)
		{
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!DBNull.Value.Equals(dataRow["LoadStatusColumn"]))
				{
					this.SetColumnValue("LoadStatusColumn", ItemLoadStatus.Failed, dataRow);
				}
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0007009C File Offset: 0x0006E29C
		private PreFillADObjectIdFiller CreatePreFiller(ICollection idCollection)
		{
			DataTable dataTable = this.loader.Table.Clone();
			dataTable.BeginLoadData();
			foreach (object obj in idCollection)
			{
				ADObjectId adobjectId = (ADObjectId)obj;
				DataRow row = dataTable.NewRow();
				this.SetColumnValue("Identity", adobjectId, row);
				this.SetColumnValue("Guid", adobjectId.ObjectGuid, row);
				this.SetColumnValue("Name", adobjectId.Name, row);
				this.SetColumnValue("LoadStatusColumn", ItemLoadStatus.Loading, row);
				dataTable.Rows.Add(row);
			}
			dataTable.EndLoadData();
			return new PreFillADObjectIdFiller(dataTable);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0007016C File Offset: 0x0006E36C
		private void SetColumnValue(string columnName, object value, DataRow row)
		{
			if (!string.IsNullOrEmpty(columnName) && row.Table.Columns.Contains(columnName))
			{
				row[columnName] = value;
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000701E0 File Offset: 0x0006E3E0
		internal void ResolveObjectIds(ADPropertyDefinition property, ICollection idCollection)
		{
			if (property == null)
			{
				throw new ArgumentOutOfRangeException("property", "property should not be null.");
			}
			if (idCollection == null)
			{
				throw new ArgumentOutOfRangeException("idCollection", "idCollection should not be null.");
			}
			if (idCollection.Count == 0)
			{
				throw new ArgumentOutOfRangeException("idCollection", "idCollection should not be empty.");
			}
			ISupportResolvingIds supportResolvingIds = this.loader as ISupportResolvingIds;
			if (supportResolvingIds != null)
			{
				supportResolvingIds.IdentitiesToResolve = new ArrayList(idCollection);
				supportResolvingIds.PropertyForResolving = property;
			}
			ResultsLoaderProfile profile = this.loader.RefreshArgument as ResultsLoaderProfile;
			if (profile != null)
			{
				profile.PipelineObjects = new ArrayList(idCollection).ToArray();
				profile.IsResolving = true;
				profile.ResolveProperty = property.Name;
				profile.SearchText = string.Empty;
				profile.Scope = null;
				if (this.PrefillBeforeResolving)
				{
					if (this.CanApplyPrefill(profile))
					{
						if (this.FastResolving)
						{
							profile.ClearFiller();
						}
						AbstractDataTableFiller preFiller = this.CreatePreFiller(idCollection);
						profile.InsertTableFiller(0, preFiller);
						this.ResolveObjectIdsCompleted += delegate(object param0, RunWorkerCompletedEventArgs param1)
						{
							this.UpdateNonResolvedObjects(this.loader.Table);
							profile.RemoveTableFiller(preFiller);
						};
					}
					else
					{
						this.PrefillBeforeResolving = false;
					}
				}
			}
			this.IsResolving = true;
			this.loader.Refresh(NullProgress.Value);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000703A8 File Offset: 0x0006E5A8
		private bool CanApplyPrefill(ResultsLoaderProfile profile)
		{
			bool flag;
			if (profile.PipelineObjects.All((object obj) => obj is ADObjectId))
			{
				flag = (from filler in profile.TableFillers
				select filler.CommandBuilder).All((ICommandBuilder builder) => builder is ExchangeCommandBuilder && (builder as ExchangeCommandBuilder).resolveForIdentity() && !(builder as ExchangeCommandBuilder).UseFilterToResolveNonId);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			List<string> supportedPrimaryKeys = new List<string>
			{
				"Identity",
				"Guid",
				"Name"
			};
			bool flag3 = profile.DataTable.PrimaryKey.All((DataColumn column) => supportedPrimaryKeys.Contains(column.ColumnName));
			return flag2 && flag3;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00070484 File Offset: 0x0006E684
		private void loader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!this.loader.Refreshing)
			{
				this.IsResolving = false;
				this.OnResolveObjectIdsCompleted(e);
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000704A4 File Offset: 0x0006E6A4
		protected virtual void OnResolveObjectIdsCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler resolveObjectIdsCompleted = this.ResolveObjectIdsCompleted;
			if (resolveObjectIdsCompleted != null)
			{
				resolveObjectIdsCompleted(this, e);
			}
		}

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060019D4 RID: 6612 RVA: 0x000704C4 File Offset: 0x0006E6C4
		// (remove) Token: 0x060019D5 RID: 6613 RVA: 0x000704FC File Offset: 0x0006E6FC
		public event RunWorkerCompletedEventHandler ResolveObjectIdsCompleted;

		// Token: 0x060019D6 RID: 6614 RVA: 0x00070534 File Offset: 0x0006E734
		protected virtual void OnIsResolvingChanged(EventArgs e)
		{
			EventHandler isResolvingChanged = this.IsResolvingChanged;
			if (isResolvingChanged != null)
			{
				isResolvingChanged(this, e);
			}
		}

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060019D7 RID: 6615 RVA: 0x00070554 File Offset: 0x0006E754
		// (remove) Token: 0x060019D8 RID: 6616 RVA: 0x0007058C File Offset: 0x0006E78C
		public event EventHandler IsResolvingChanged;

		// Token: 0x0400099F RID: 2463
		public const string LoadStatusColumn = "LoadStatusColumn";

		// Token: 0x040009A0 RID: 2464
		private DataTableLoader loader;

		// Token: 0x040009A1 RID: 2465
		private bool isResolving;

		// Token: 0x040009A2 RID: 2466
		private bool prefillBeforeResolving;
	}
}
