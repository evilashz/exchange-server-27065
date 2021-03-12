using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x0200000C RID: 12
	public class QueueViewerQueuesResultPane : QueueViewerResultPaneBase
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00004970 File Offset: 0x00002B70
		static QueueViewerQueuesResultPane()
		{
			QueueViewerQueuesResultPane.iconLibrary = new IconLibrary();
			QueueViewerQueuesResultPane.iconLibrary.Icons.Add(QueueStatus.Active, Icons.QueuesActive);
			QueueViewerQueuesResultPane.iconLibrary.Icons.Add(QueueStatus.Suspended, Icons.QueuesFrozen);
			QueueViewerQueuesResultPane.iconLibrary.Icons.Add(QueueStatus.Ready, Icons.QueuesReady);
			QueueViewerQueuesResultPane.iconLibrary.Icons.Add(QueueStatus.Retry, Icons.QueuesRetry);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004A2C File Offset: 0x00002C2C
		public QueueViewerQueuesResultPane()
		{
			base.Name = "QueueViewerQueuesResultPane";
			base.ObjectList.Name = "queueList";
			base.ObjectList.ListView.AutoGenerateColumns = false;
			ExchangeColumnHeader exchangeColumnHeader = new ExchangeColumnHeader();
			exchangeColumnHeader.Name = "NextHopDomain";
			exchangeColumnHeader.Text = Strings.NextHopDomainColumn;
			exchangeColumnHeader.Default = true;
			ExchangeColumnHeader exchangeColumnHeader2 = new ExchangeColumnHeader();
			exchangeColumnHeader2.Name = "DeliveryType";
			exchangeColumnHeader2.Text = Strings.DeliveryTypeColumn;
			exchangeColumnHeader2.Default = true;
			ExchangeColumnHeader exchangeColumnHeader3 = new ExchangeColumnHeader();
			exchangeColumnHeader3.Name = "Status";
			exchangeColumnHeader3.Text = Strings.StatusColumn;
			exchangeColumnHeader3.Default = true;
			ExchangeColumnHeader exchangeColumnHeader4 = new ExchangeColumnHeader();
			exchangeColumnHeader4.Name = "MessageCount";
			exchangeColumnHeader4.Text = Strings.MessageCountColumn;
			exchangeColumnHeader4.Default = true;
			ExchangeColumnHeader exchangeColumnHeader5 = new ExchangeColumnHeader();
			exchangeColumnHeader5.Name = "NextRetryTime";
			exchangeColumnHeader5.Text = Strings.NextRetryTimeColumn;
			exchangeColumnHeader5.Default = true;
			ExchangeColumnHeader exchangeColumnHeader6 = new ExchangeColumnHeader();
			exchangeColumnHeader6.Name = "LastRetryTime";
			exchangeColumnHeader6.Text = Strings.LastRetryTimeColumn;
			ExchangeColumnHeader exchangeColumnHeader7 = new ExchangeColumnHeader();
			exchangeColumnHeader7.Name = "LastError";
			exchangeColumnHeader7.Text = Strings.LastErrorColumn;
			exchangeColumnHeader7.Default = true;
			base.ObjectList.ListView.AvailableColumns.AddRange(new ExchangeColumnHeader[]
			{
				exchangeColumnHeader,
				exchangeColumnHeader2,
				exchangeColumnHeader3,
				exchangeColumnHeader4,
				exchangeColumnHeader5,
				exchangeColumnHeader6,
				exchangeColumnHeader7
			});
			base.ObjectList.FilterControl.ObjectSchema = ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>();
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.NextHopDomain, Strings.NextHopDomainColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.Contains
			}));
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.DeliveryType, Strings.DeliveryTypeColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual
			}));
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.Status, Strings.StatusColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual
			}));
			FilterablePropertyDescription filterablePropertyDescription = new FilterablePropertyDescription(ExtensibleQueueInfoSchema.MessageCount, Strings.MessageCountColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual
			});
			filterablePropertyDescription.ColumnType = typeof(uint);
			base.ObjectList.FilterControl.PropertiesToFilter.Add(filterablePropertyDescription);
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.NextRetryTime, Strings.NextRetryTimeColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual,
				PropertyFilterOperator.Present,
				PropertyFilterOperator.NotPresent
			}));
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.LastRetryTime, Strings.LastRetryTimeColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.GreaterThan,
				PropertyFilterOperator.GreaterThanOrEqual,
				PropertyFilterOperator.LessThan,
				PropertyFilterOperator.LessThanOrEqual,
				PropertyFilterOperator.Present,
				PropertyFilterOperator.NotPresent
			}));
			base.ObjectList.FilterControl.PropertiesToFilter.Add(new FilterablePropertyDescription(ExtensibleQueueInfoSchema.LastError, Strings.LastErrorColumn, new PropertyFilterOperator[]
			{
				PropertyFilterOperator.Equal,
				PropertyFilterOperator.NotEqual,
				PropertyFilterOperator.Contains,
				PropertyFilterOperator.Present,
				PropertyFilterOperator.NotPresent
			}));
			base.ObjectList.ListView.SelectionNameProperty = "NextHopDomain";
			base.ObjectList.ListView.SortProperty = "NextHopDomain";
			base.ObjectList.ListView.ImagePropertyName = "Status";
			base.ListControl.IconLibrary = QueueViewerQueuesResultPane.iconLibrary;
			base.SubscribedRefreshCategories.Add(RefreshCategories.Message);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004E40 File Offset: 0x00003040
		protected override void SetupCommandsProfile()
		{
			base.SetupCommandsProfile();
			ResultsCommandProfile resultsCommandProfile = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandViewMessages",
					Icon = Icons.MessagesActive,
					Text = Strings.ViewMessagesCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true,
					RequiresSingleSelection = true,
					IsPropertiesCommand = true
				},
				Action = new ViewQueueMessagesCommandAction()
			};
			ResultsCommandProfile resultsCommandProfile2 = ResultsCommandProfile.CreateSeparator();
			ResultsCommandProfile resultsCommandProfile3 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandFreezeQueue",
					Icon = Icons.QueuesFrozen,
					Text = Strings.SuspendCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true
				},
				Action = new QueuesTaskCommandAction
				{
					CommandText = "Suspend-Queue"
				},
				UpdatingUtil = new SelectionCommandVisibilityBindingUtil
				{
					PropertyName = QueueViewerQueuesResultPane.CanSuspendString,
					TrueValue = true,
					AllowMixedValues = true
				}
			};
			ResultsCommandProfile resultsCommandProfile4 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandUnfreezeQueue",
					Icon = Icons.QueuesActive,
					Text = Strings.ResumeCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true
				},
				Action = new QueuesTaskCommandAction
				{
					CommandText = "Resume-Queue"
				},
				UpdatingUtil = new SelectionCommandVisibilityBindingUtil
				{
					PropertyName = QueueViewerQueuesResultPane.CanResumeString,
					TrueValue = true,
					AllowMixedValues = true
				}
			};
			ResultsCommandProfile resultsCommandProfile5 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandRetryQueue",
					Icon = Icons.QueuesRetry,
					Text = Strings.RetryQueueCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Update,
					IsSelectionCommand = true
				},
				Action = new QueuesTaskCommandAction
				{
					CommandText = "Retry-Queue"
				},
				UpdatingUtil = new SelectionCommandVisibilityBindingUtil
				{
					PropertyName = QueueViewerQueuesResultPane.CanForceRetryString,
					TrueValue = true,
					AllowMixedValues = true
				}
			};
			ResultsCommandProfile resultsCommandProfile6 = ResultsCommandProfile.CreateSeparator();
			ResultsCommandProfile resultsCommandProfile7 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandRemoveMessagesNDR",
					Icon = Icons.MessagesPendingDelete,
					Text = Strings.RemoveQueueMessagesNDRCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Delete,
					IsSelectionCommand = true,
					RequiresSingleSelection = true,
					UseSingleRowRefresh = false
				},
				Action = new RemoveQueueMessagesTaskCommandAction
				{
					CommandText = "Remove-Message",
					Parameters = new MonadParameterCollection
					{
						new MonadParameter
						{
							ParameterName = "WithNDR",
							Value = true
						}
					},
					SingleSelectionConfirmation = new SingleSelectionMessageDelegate(Strings.RemoveQueueMessagesNDRConfirmSingleText),
					MultipleSelectionConfirmation = new MultipleSelectionMessageDelegate(Strings.RemoveQueueMessagesNDRConfirmMultiText),
					UseCustomInputRequestedHandler = true
				},
				UpdatingUtil = new SelectionCommandVisibilityBindingUtil
				{
					PropertyName = QueueViewerQueuesResultPane.CanRemoveString,
					TrueValue = true
				}
			};
			ResultsCommandProfile resultsCommandProfile8 = new ResultsCommandProfile
			{
				Command = new Command
				{
					Name = "commandRemoveMessages",
					Icon = Icons.MessagesPendingDelete,
					Text = Strings.RemoveQueueMessagesNoNDRCommandText,
					Description = LocalizedString.Empty
				},
				Setting = new ResultsCommandSetting
				{
					Operation = CommandOperation.Delete,
					IsSelectionCommand = true,
					RequiresSingleSelection = true,
					UseSingleRowRefresh = false
				},
				Action = new RemoveQueueMessagesTaskCommandAction
				{
					CommandText = "Remove-Message",
					Parameters = new MonadParameterCollection
					{
						new MonadParameter
						{
							ParameterName = "WithNDR",
							Value = false
						}
					},
					SingleSelectionConfirmation = new SingleSelectionMessageDelegate(Strings.RemoveQueueMessagesNoNDRConfirmSingleText),
					MultipleSelectionConfirmation = new MultipleSelectionMessageDelegate(Strings.RemoveQueueMessagesNoNDRConfirmMultiText),
					UseCustomInputRequestedHandler = true
				},
				UpdatingUtil = new SelectionCommandVisibilityBindingUtil
				{
					PropertyName = QueueViewerQueuesResultPane.CanRemoveString,
					TrueValue = true
				}
			};
			base.CommandsProfile.CustomSelectionCommands.AddRange(new ResultsCommandProfile[]
			{
				resultsCommandProfile,
				resultsCommandProfile2,
				resultsCommandProfile3,
				resultsCommandProfile4,
				resultsCommandProfile5,
				resultsCommandProfile6,
				resultsCommandProfile7,
				resultsCommandProfile8
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000053E0 File Offset: 0x000035E0
		protected override void SetUpDatasourceColumns()
		{
			DataColumn dataColumn = new DataColumn("NextHopDomain", typeof(string));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn("DeliveryType", typeof(string));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn(QueueViewerQueuesResultPane.StatusString, typeof(EnumObject));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn("MessageCount", typeof(int));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn("NextRetryTime", typeof(DateTime));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn("LastRetryTime", typeof(DateTime));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn("LastError", typeof(string));
			base.Datasource.Columns.Add(dataColumn);
			dataColumn = new DataColumn(QueueViewerQueuesResultPane.IdentityString);
			base.Datasource.Columns.Add(dataColumn);
			base.Datasource.Table.PrimaryKey = new DataColumn[]
			{
				dataColumn
			};
			base.Datasource.Columns.Add(new DataColumn(QueueViewerQueuesResultPane.CanSuspendString, typeof(bool)));
			base.Datasource.Columns.Add(new DataColumn(QueueViewerQueuesResultPane.CanResumeString, typeof(bool)));
			base.Datasource.Columns.Add(new DataColumn(QueueViewerQueuesResultPane.CanRemoveString, typeof(bool)));
			base.Datasource.Columns.Add(new DataColumn(QueueViewerQueuesResultPane.CanForceRetryString, typeof(bool)));
			base.Datasource.Table.RowChanged += this.Table_RowChanged;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000055DC File Offset: 0x000037DC
		private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add || e.Action == DataRowAction.Change)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = true;
				DataRow row = e.Row;
				QueueStatus queueStatus = (QueueStatus)((EnumObject)row[QueueViewerQueuesResultPane.StatusString]);
				QueueIdentity queueIdentity = QueueIdentity.Parse((string)row[QueueViewerQueuesResultPane.IdentityString]);
				QueueType type = queueIdentity.Type;
				if (type == QueueType.Submission)
				{
					flag4 = false;
				}
				if (QueueStatus.Retry == queueStatus && type != QueueType.Submission && type != QueueType.Poison)
				{
					flag = true;
				}
				if (type != QueueType.Poison)
				{
					if (QueueStatus.Suspended == queueStatus)
					{
						flag3 = true;
					}
					else
					{
						flag2 = true;
					}
				}
				QueueViewerQueuesResultPane.UpdateColumnIfChanged(row, QueueViewerQueuesResultPane.CanSuspendString, flag2);
				QueueViewerQueuesResultPane.UpdateColumnIfChanged(row, QueueViewerQueuesResultPane.CanResumeString, flag3);
				QueueViewerQueuesResultPane.UpdateColumnIfChanged(row, QueueViewerQueuesResultPane.CanForceRetryString, flag);
				QueueViewerQueuesResultPane.UpdateColumnIfChanged(row, QueueViewerQueuesResultPane.CanRemoveString, flag4);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000056BD File Offset: 0x000038BD
		private static void UpdateColumnIfChanged(DataRow row, string columnName, object value)
		{
			if (!value.Equals(row[columnName]))
			{
				row[columnName] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000056D6 File Offset: 0x000038D6
		public override string SelectionHelpTopic
		{
			get
			{
				return SelectionHelpTopics.QueueInfo;
			}
		}

		// Token: 0x0400001D RID: 29
		private static readonly IconLibrary iconLibrary;

		// Token: 0x0400001E RID: 30
		private static readonly string StatusString = "Status";

		// Token: 0x0400001F RID: 31
		private static readonly string IdentityString = "Identity";

		// Token: 0x04000020 RID: 32
		private static readonly string CanRemoveString = "CanRemove";

		// Token: 0x04000021 RID: 33
		private static readonly string CanSuspendString = "CanSuspend";

		// Token: 0x04000022 RID: 34
		private static readonly string CanResumeString = "CanResume";

		// Token: 0x04000023 RID: 35
		private static readonly string CanForceRetryString = "CanForceRetry";
	}
}
