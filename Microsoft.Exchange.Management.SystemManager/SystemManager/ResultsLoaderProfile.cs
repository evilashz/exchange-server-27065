using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000CA RID: 202
	public class ResultsLoaderProfile : ICloneable, IHasPermission
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x00017A6C File Offset: 0x00015C6C
		public ResultsLoaderProfile(string displayName, bool hideIcon, string imageProperty, string commandText, DataTable inputTable, DataTable dataTable, ResultsColumnProfile[] displayedColumnCollection, ICommandBuilder commandBuilder) : this(displayName, hideIcon, imageProperty, inputTable, dataTable, displayedColumnCollection)
		{
			MonadAdapterFiller filler = new MonadAdapterFiller(commandText, commandBuilder);
			this.AddTableFiller(filler);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00017A9C File Offset: 0x00015C9C
		public ResultsLoaderProfile(string displayName, string imageProperty, string commandText, DataTable inputTable, DataTable dataTable, ResultsColumnProfile[] displayedColumnCollection, ICommandBuilder commandBuilder) : this(displayName, false, imageProperty, commandText, inputTable, dataTable, displayedColumnCollection, commandBuilder)
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00017ABC File Offset: 0x00015CBC
		public ResultsLoaderProfile(string displayName, bool hideIcon, string imageProperty, DataTable inputTable, DataTable dataTable, ResultsColumnProfile[] displayedColumnCollection) : this(new UIPresentationProfile(displayedColumnCollection)
		{
			DisplayName = displayName,
			HideIcon = hideIcon,
			ImageProperty = imageProperty
		}, inputTable, dataTable)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00017AF4 File Offset: 0x00015CF4
		public ResultsLoaderProfile(UIPresentationProfile uiPresentationProfile, DataTable inputTable, DataTable dataTable)
		{
			this.UIPresentationProfile = (uiPresentationProfile ?? new UIPresentationProfile());
			this.dataTable = dataTable;
			this.inputTable = inputTable;
			this.inputTable.Rows.Add(inputTable.NewRow());
			this.tableFillers = new List<AbstractDataTableFiller>();
			this.TableFillers = new ReadOnlyCollection<AbstractDataTableFiller>(this.tableFillers);
			this.CommandsProfile = new ResultCommandsProfile();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00017B8E File Offset: 0x00015D8E
		public ResultsLoaderProfile(DataTable inputTable, DataTable dataTable) : this(null, inputTable, dataTable)
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00017B99 File Offset: 0x00015D99
		public void AddTableFiller(AbstractDataTableFiller filler, string runnableLambdaExpression)
		{
			this.tableFillers.Add(filler);
			this.runnableLambdaExpressions[filler] = runnableLambdaExpression;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public void AddTableFiller(AbstractDataTableFiller filler)
		{
			this.AddTableFiller(filler, string.Empty);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00017BC2 File Offset: 0x00015DC2
		public void InsertTableFiller(int index, AbstractDataTableFiller filler)
		{
			this.tableFillers.Insert(index, filler);
			this.runnableLambdaExpressions[filler] = string.Empty;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00017BE2 File Offset: 0x00015DE2
		public void RemoveTableFiller(AbstractDataTableFiller filler)
		{
			this.tableFillers.Remove(filler);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00017BF1 File Offset: 0x00015DF1
		public void ClearFiller()
		{
			this.tableFillers.Clear();
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00017BFE File Offset: 0x00015DFE
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00017C06 File Offset: 0x00015E06
		public ReadOnlyCollection<AbstractDataTableFiller> TableFillers { get; private set; }

		// Token: 0x060006AE RID: 1710 RVA: 0x00017C0F File Offset: 0x00015E0F
		public string GetRunnableLambdaExpression(AbstractDataTableFiller filler)
		{
			if (!this.runnableLambdaExpressions.ContainsKey(filler))
			{
				return string.Empty;
			}
			return this.runnableLambdaExpressions[filler];
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00017C34 File Offset: 0x00015E34
		public bool IsRunnable(AbstractDataTableFiller filler)
		{
			bool flag = true;
			string runnableLambdaExpression = this.GetRunnableLambdaExpression(filler);
			if (!string.IsNullOrEmpty(runnableLambdaExpression))
			{
				ColumnExpression expression = ExpressionCalculator.BuildColumnExpression(runnableLambdaExpression);
				flag = (bool)ExpressionCalculator.CalculateLambdaExpression(expression, typeof(bool), null, this.InputTable.Rows[0]);
			}
			if (flag && this.IsResolving)
			{
				flag = (this.PipelineObjects != null && this.PipelineObjects.Length > 0);
			}
			return flag;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00017CA5 File Offset: 0x00015EA5
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00017CAD File Offset: 0x00015EAD
		[DefaultValue(0)]
		public FillType FillType { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00017CB6 File Offset: 0x00015EB6
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00017CBE File Offset: 0x00015EBE
		public int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "value <= 0");
				}
				this.batchSize = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00017CE1 File Offset: 0x00015EE1
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00017CE9 File Offset: 0x00015EE9
		public bool IsResolving { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00017CF2 File Offset: 0x00015EF2
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00017CFA File Offset: 0x00015EFA
		[DefaultValue("")]
		public string DistinguishIdentity { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00017D03 File Offset: 0x00015F03
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00017D0B File Offset: 0x00015F0B
		public string NameProperty
		{
			get
			{
				return this.nameProperty;
			}
			set
			{
				this.nameProperty = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00017D14 File Offset: 0x00015F14
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00017D1C File Offset: 0x00015F1C
		public string ResolveProperty
		{
			get
			{
				return this.resolveProperty;
			}
			set
			{
				this.resolveProperty = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00017D25 File Offset: 0x00015F25
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00017D2D File Offset: 0x00015F2D
		public List<object> ResolvedObjects
		{
			get
			{
				return this.resolvedObjects;
			}
			set
			{
				this.resolvedObjects = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00017D36 File Offset: 0x00015F36
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00017D3E File Offset: 0x00015F3E
		public string WholeObjectProperty
		{
			get
			{
				return this.wholeObjectProperty;
			}
			set
			{
				this.wholeObjectProperty = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00017D47 File Offset: 0x00015F47
		public DataTable InputTable
		{
			get
			{
				return this.inputTable;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00017D4F File Offset: 0x00015F4F
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00017D57 File Offset: 0x00015F57
		public IPartialOrderComparer InputTablePartialOrderComparer { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00017D60 File Offset: 0x00015F60
		public DataTable DataTable
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00017D68 File Offset: 0x00015F68
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00017D70 File Offset: 0x00015F70
		public IDataColumnsCalculator DataColumnsCalculator { get; set; }

		// Token: 0x060006C6 RID: 1734 RVA: 0x00017D7C File Offset: 0x00015F7C
		public void BuildCommand(AbstractDataTableFiller filler)
		{
			if (filler is MonadAdapterFiller)
			{
				(filler as MonadAdapterFiller).IsResolving = this.IsResolving;
			}
			if (this.IsResolving && filler.CommandBuilder != null)
			{
				filler.CommandBuilder.ResolveProperty = this.ResolveProperty;
			}
			if ((this.ScopeSupportingLevel == ScopeSupportingLevel.NoScoping && !this.UseTreeViewForm) || this.IsResolving)
			{
				filler.BuildCommand(this.SearchText, this.PipelineObjects, this.InputTable.Rows[0]);
				return;
			}
			filler.BuildCommandWithScope(this.SearchText, this.PipelineObjects, this.InputTable.Rows[0], this.Scope);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00017E28 File Offset: 0x00016028
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00017E30 File Offset: 0x00016030
		public ResultCommandsProfile CommandsProfile { get; set; }

		// Token: 0x060006C9 RID: 1737 RVA: 0x00017E39 File Offset: 0x00016039
		public object Clone()
		{
			return this.CloneInternal(false);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00017E42 File Offset: 0x00016042
		public object CloneWithSharedInputTable()
		{
			return this.CloneInternal(true);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00017E4C File Offset: 0x0001604C
		private ResultsLoaderProfile CloneInternal(bool shareInputTable)
		{
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(new UIPresentationProfile(this.DisplayedColumnCollection), shareInputTable ? this.inputTable : this.inputTable.Copy(), this.dataTable);
			resultsLoaderProfile.Name = this.Name;
			resultsLoaderProfile.DisplayName = this.DisplayName;
			resultsLoaderProfile.HideIcon = this.HideIcon;
			resultsLoaderProfile.ImageProperty = this.ImageProperty;
			resultsLoaderProfile.SearchText = this.searchText;
			resultsLoaderProfile.PipelineObjects = this.PipelineObjects;
			resultsLoaderProfile.ScopeSupportingLevel = this.ScopeSupportingLevel;
			resultsLoaderProfile.Scope = this.Scope;
			resultsLoaderProfile.UseTreeViewForm = this.UseTreeViewForm;
			resultsLoaderProfile.IsResolving = this.IsResolving;
			resultsLoaderProfile.ResolveProperty = this.ResolveProperty;
			resultsLoaderProfile.WholeObjectProperty = this.WholeObjectProperty;
			resultsLoaderProfile.NameProperty = this.NameProperty;
			resultsLoaderProfile.LoadableFromProfilePredicate = this.LoadableFromProfilePredicate;
			resultsLoaderProfile.PostRefreshAction = this.PostRefreshAction;
			resultsLoaderProfile.SerializationLevel = this.SerializationLevel;
			resultsLoaderProfile.MultiSelect = this.MultiSelect;
			resultsLoaderProfile.FillType = this.FillType;
			foreach (AbstractDataTableFiller abstractDataTableFiller in this.TableFillers)
			{
				resultsLoaderProfile.AddTableFiller(abstractDataTableFiller.Clone() as AbstractDataTableFiller, this.GetRunnableLambdaExpression(abstractDataTableFiller));
			}
			resultsLoaderProfile.BatchSize = this.BatchSize;
			resultsLoaderProfile.CommandsProfile = new ResultCommandsProfile();
			resultsLoaderProfile.CommandsProfile.ResultPaneCommands.AddRange(this.CommandsProfile.ResultPaneCommands);
			resultsLoaderProfile.CommandsProfile.CustomSelectionCommands.AddRange(this.CommandsProfile.CustomSelectionCommands);
			resultsLoaderProfile.CommandsProfile.DeleteSelectionCommands.AddRange(this.CommandsProfile.DeleteSelectionCommands);
			resultsLoaderProfile.CommandsProfile.ShowSelectionPropertiesCommands.AddRange(this.CommandsProfile.ShowSelectionPropertiesCommands);
			resultsLoaderProfile.AutoGenerateColumns = this.AutoGenerateColumns;
			resultsLoaderProfile.DataColumnsCalculator = this.DataColumnsCalculator;
			resultsLoaderProfile.DistinguishIdentity = this.DistinguishIdentity;
			return resultsLoaderProfile;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00018058 File Offset: 0x00016258
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00018060 File Offset: 0x00016260
		public string SearchText
		{
			get
			{
				return this.searchText;
			}
			set
			{
				this.searchText = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00018069 File Offset: 0x00016269
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00018071 File Offset: 0x00016271
		public object[] PipelineObjects
		{
			get
			{
				return this.pipelineObjects;
			}
			set
			{
				this.pipelineObjects = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001807A File Offset: 0x0001627A
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00018082 File Offset: 0x00016282
		public object Scope
		{
			get
			{
				return this.rootId;
			}
			set
			{
				this.rootId = value;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001808C File Offset: 0x0001628C
		public DataTable CreateResultsDataTable()
		{
			DataTable dataTable = this.DataTable.Clone();
			dataTable.TableName = (string.IsNullOrEmpty(this.DataTable.TableName) ? base.GetType().Name : this.DataTable.TableName);
			dataTable.Locale = CultureInfo.InvariantCulture;
			return dataTable;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000180E1 File Offset: 0x000162E1
		public void InputValue(string columnName, object value)
		{
			if (!this.InputTable.Columns.Contains(columnName))
			{
				throw new ArgumentException("The column {0} you try to access doesn't exist.", columnName);
			}
			this.TryInputValue(columnName, value);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001810C File Offset: 0x0001630C
		public bool TryInputValue(string columnName, object value)
		{
			if (this.InputTable.Columns.Contains(columnName))
			{
				if (value == null && this.InputTable.Columns[columnName].DataType.IsValueType)
				{
					value = DBNull.Value;
				}
				this.InputTable.Rows[0][columnName] = value;
				return true;
			}
			return false;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00018170 File Offset: 0x00016370
		public object GetValue(string columnName)
		{
			if (!this.InputTable.Columns.Contains(columnName))
			{
				throw new ArgumentException("The column {0} you try to access doesn't exist.", columnName);
			}
			object obj = this.InputTable.Rows[0][columnName];
			if (DBNull.Value.Equals(obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000181C4 File Offset: 0x000163C4
		public bool IsLoadable(DataRow row)
		{
			if (this.LoadableFromProfilePredicate == null)
			{
				throw new InvalidOperationException("LoadableFromPredicate has no value!");
			}
			return this.LoadableFromProfilePredicate.IsLoadableFrom(this, row);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000181E6 File Offset: 0x000163E6
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000181EE File Offset: 0x000163EE
		public ILoadableFromProfile LoadableFromProfilePredicate { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000181F7 File Offset: 0x000163F7
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x000181FF File Offset: 0x000163FF
		public PostRefreshActionBase PostRefreshAction { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00018208 File Offset: 0x00016408
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00018210 File Offset: 0x00016410
		public UIPresentationProfile UIPresentationProfile { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00018219 File Offset: 0x00016419
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00018226 File Offset: 0x00016426
		public bool AutoGenerateColumns
		{
			get
			{
				return this.UIPresentationProfile.AutoGenerateColumns;
			}
			set
			{
				this.UIPresentationProfile.AutoGenerateColumns = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00018234 File Offset: 0x00016434
		public ResultsColumnProfile[] DisplayedColumnCollection
		{
			get
			{
				return this.UIPresentationProfile.DisplayedColumnCollection;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00018241 File Offset: 0x00016441
		public ExchangeColumnHeader[] CreateColumnHeaders()
		{
			return this.UIPresentationProfile.CreateColumnHeaders();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001824E File Offset: 0x0001644E
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001825B File Offset: 0x0001645B
		public bool HideIcon
		{
			get
			{
				return this.UIPresentationProfile.HideIcon;
			}
			set
			{
				this.UIPresentationProfile.HideIcon = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001826C File Offset: 0x0001646C
		internal ObjectSchema FilterObjectSchema
		{
			get
			{
				ObjectSchema result = this.UIPresentationProfile.FilterObjectSchema;
				if (this.UIPresentationProfile.FilterLanguage == FilterLanguage.Ado)
				{
					IList<PropertyDefinition> list = new List<PropertyDefinition>();
					foreach (FilterablePropertyDescription filterablePropertyDescription in this.UIPresentationProfile.FilterableProperties.Values)
					{
						list.Add(filterablePropertyDescription.PropertyDefinition);
					}
					result = new ADOFilterObjectSchema(list);
				}
				return result;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000182F8 File Offset: 0x000164F8
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00018305 File Offset: 0x00016505
		public string DisplayName
		{
			get
			{
				return this.UIPresentationProfile.DisplayName;
			}
			set
			{
				this.UIPresentationProfile.DisplayName = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00018313 File Offset: 0x00016513
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00018320 File Offset: 0x00016520
		public string ImageProperty
		{
			get
			{
				return this.UIPresentationProfile.ImageProperty;
			}
			set
			{
				this.UIPresentationProfile.ImageProperty = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001832E File Offset: 0x0001652E
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001833B File Offset: 0x0001653B
		public string SortProperty
		{
			get
			{
				return this.UIPresentationProfile.SortProperty;
			}
			set
			{
				this.UIPresentationProfile.SortProperty = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00018349 File Offset: 0x00016549
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00018356 File Offset: 0x00016556
		public bool UseTreeViewForm
		{
			get
			{
				return this.UIPresentationProfile.UseTreeViewForm;
			}
			set
			{
				this.UIPresentationProfile.UseTreeViewForm = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00018364 File Offset: 0x00016564
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00018371 File Offset: 0x00016571
		public string HelpTopic
		{
			get
			{
				return this.UIPresentationProfile.HelpTopic;
			}
			set
			{
				this.UIPresentationProfile.HelpTopic = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001837F File Offset: 0x0001657F
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001838C File Offset: 0x0001658C
		public ScopeSupportingLevel ScopeSupportingLevel
		{
			get
			{
				return this.UIPresentationProfile.ScopeSupportingLevel;
			}
			set
			{
				this.UIPresentationProfile.ScopeSupportingLevel = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001839A File Offset: 0x0001659A
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x000183A7 File Offset: 0x000165A7
		public ExchangeRunspaceConfigurationSettings.SerializationLevel SerializationLevel
		{
			get
			{
				return this.UIPresentationProfile.SerializationLevel;
			}
			set
			{
				this.UIPresentationProfile.SerializationLevel = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000183B5 File Offset: 0x000165B5
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x000183C2 File Offset: 0x000165C2
		public bool MultiSelect
		{
			get
			{
				return this.UIPresentationProfile.MultiSelect;
			}
			set
			{
				this.UIPresentationProfile.MultiSelect = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000183D0 File Offset: 0x000165D0
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x000183D8 File Offset: 0x000165D8
		public string Name { get; set; }

		// Token: 0x060006F6 RID: 1782 RVA: 0x000183E4 File Offset: 0x000165E4
		public bool HasPermission()
		{
			bool flag = false;
			if (this.FillType == null)
			{
				using (List<AbstractDataTableFiller>.Enumerator enumerator = this.tableFillers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AbstractDataTableFiller abstractDataTableFiller = enumerator.Current;
						MonadAdapterFiller monadAdapterFiller = abstractDataTableFiller as MonadAdapterFiller;
						if (monadAdapterFiller == null || monadAdapterFiller.HasPermission())
						{
							flag = true;
							break;
						}
					}
					goto IL_9B;
				}
			}
			flag = true;
			foreach (AbstractDataTableFiller abstractDataTableFiller2 in this.tableFillers)
			{
				MonadAdapterFiller monadAdapterFiller2 = abstractDataTableFiller2 as MonadAdapterFiller;
				if (monadAdapterFiller2 != null && !monadAdapterFiller2.HasPermission())
				{
					flag = false;
					break;
				}
			}
			IL_9B:
			if (string.Equals(this.Name, "MailboxMigration", StringComparison.OrdinalIgnoreCase))
			{
				flag = (EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-MoveRequest", new string[]
				{
					"Identity",
					"ResultSize"
				}) && EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-MoveRequestStatistics", new string[]
				{
					"Identity"
				}));
			}
			else if (string.Equals(this.Name, "Database", StringComparison.OrdinalIgnoreCase))
			{
				flag = ((EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-MailboxDatabase", new string[]
				{
					"Identity"
				}) && EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-MailboxDatabaseCopyStatus", new string[]
				{
					"Identity",
					"Server"
				})) || EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-PublicFolderDatabase", new string[]
				{
					"Identity",
					"Status"
				}));
			}
			else if (flag && string.Equals(this.Name, "DisconnectedMailbox", StringComparison.OrdinalIgnoreCase))
			{
				flag = EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope("Get-ExchangeServer", new string[]
				{
					"Identity"
				});
			}
			return flag;
		}

		// Token: 0x04000337 RID: 823
		private const string ColumnDoesnotExist = "The column {0} you try to access doesn't exist.";

		// Token: 0x04000338 RID: 824
		private DataTable inputTable;

		// Token: 0x04000339 RID: 825
		private DataTable dataTable;

		// Token: 0x0400033A RID: 826
		private string searchText;

		// Token: 0x0400033B RID: 827
		private object[] pipelineObjects;

		// Token: 0x0400033C RID: 828
		private object rootId;

		// Token: 0x0400033D RID: 829
		private string resolveProperty;

		// Token: 0x0400033E RID: 830
		private string nameProperty = "Name";

		// Token: 0x0400033F RID: 831
		private string wholeObjectProperty;

		// Token: 0x04000340 RID: 832
		private List<AbstractDataTableFiller> tableFillers;

		// Token: 0x04000341 RID: 833
		private Dictionary<AbstractDataTableFiller, string> runnableLambdaExpressions = new Dictionary<AbstractDataTableFiller, string>();

		// Token: 0x04000342 RID: 834
		private int batchSize = ResultsLoaderProfile.DefaultBatchSize;

		// Token: 0x04000343 RID: 835
		public static readonly int DefaultBatchSize = 100;

		// Token: 0x04000344 RID: 836
		private List<object> resolvedObjects = new List<object>();
	}
}
