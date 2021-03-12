using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000061 RID: 97
	internal class ProgressController : IExportHandler, IDisposable, IProgressController
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0001996C File Offset: 0x00017B6C
		public ProgressController(ITarget target, IServiceClientFactory serviceClientFactory)
		{
			ProgressController.TargetWrapper targetWrapper = new ProgressController.TargetWrapper(target);
			this.target = targetWrapper;
			if (target.ExportContext.ExportMetadata.IncludeUnsearchableItems && !Util.IncludeUnsearchableItems(target.ExportContext))
			{
				targetWrapper.ExportContextInternal.ExportMetadataInternal.IncludeUnsearchableItemsInternal = false;
				Tracer.TraceInformation("ProgressController.ProgressController: IncludeUnsearchableItems is disabled. search query={0}.", new object[]
				{
					target.ExportContext.Sources[0].SourceFilter
				});
			}
			this.abortTokenSourceForTasks = new CancellationTokenSource();
			this.progressAvailable = new Semaphore(0, int.MaxValue);
			this.progressQueue = new ConcurrentQueue<ProgressRecord>();
			this.isDocIdHintFlightingEnabled = false;
			this.StatusManager = new StatusManager(this.target);
			this.ItemListGenerator = new ItemListGenerator(this.StatusManager.AllSourceInformation, this.target, this);
			this.SearchResults = new SearchResults(this.StatusManager.AllSourceInformation, this.target);
			this.sourceDataProviderManager = new SourceDataProviderManager(serviceClientFactory, this.abortTokenSourceForTasks.Token);
			this.sourceDataProviderManager.ProgressController = this;
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x0600070C RID: 1804 RVA: 0x00019A84 File Offset: 0x00017C84
		// (remove) Token: 0x0600070D RID: 1805 RVA: 0x00019ABC File Offset: 0x00017CBC
		public event EventHandler<ExportStatusEventArgs> OnReportStatistics;

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00019AF1 File Offset: 0x00017CF1
		public IExportContext ExportContext
		{
			get
			{
				return this.target.ExportContext;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00019AFE File Offset: 0x00017CFE
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00019B06 File Offset: 0x00017D06
		public bool IsStopRequested { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00019B0F File Offset: 0x00017D0F
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00019B17 File Offset: 0x00017D17
		public ISearchResults SearchResults { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00019B20 File Offset: 0x00017D20
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00019B28 File Offset: 0x00017D28
		public ItemListGenerator ItemListGenerator { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00019B31 File Offset: 0x00017D31
		public OperationStatus CurrentStatus
		{
			get
			{
				return this.StatusManager.CurrentStatus;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00019B3E File Offset: 0x00017D3E
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00019B46 File Offset: 0x00017D46
		public bool IsDocIdHintFlightingEnabled
		{
			get
			{
				return this.isDocIdHintFlightingEnabled;
			}
			set
			{
				this.isDocIdHintFlightingEnabled = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00019B4F File Offset: 0x00017D4F
		public bool IsDocumentIdHintFlightingEnabled
		{
			get
			{
				return this.IsDocIdHintFlightingEnabled;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00019B57 File Offset: 0x00017D57
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00019B5F File Offset: 0x00017D5F
		internal IStatusManager StatusManager { get; set; }

		// Token: 0x0600071B RID: 1819 RVA: 0x00019B68 File Offset: 0x00017D68
		public void Dispose()
		{
			if (this.progressAvailable != null)
			{
				this.progressAvailable.Close();
				this.progressAvailable = null;
			}
			if (this.StatusManager != null)
			{
				this.StatusManager.Dispose();
				this.StatusManager = null;
			}
			if (this.abortTokenSourceForTasks != null)
			{
				this.abortTokenSourceForTasks.Dispose();
				this.abortTokenSourceForTasks = null;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00019BC3 File Offset: 0x00017DC3
		public void EnsureAuthentication(ICredentialHandler credentialHandler, Uri configurationServiceEndpoint = null)
		{
			if (credentialHandler != null)
			{
				this.sourceDataProviderManager.AutoDiscoverSourceServiceEndpoints(this.StatusManager.AllSourceInformation, configurationServiceEndpoint, credentialHandler);
				this.sourceDataProviderManager.CreateSourceServiceClients(this.StatusManager.AllSourceInformation);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00019BF6 File Offset: 0x00017DF6
		public void Prepare()
		{
			this.ExecuteProcedureWithProgress(ProcedureType.Prepare, new Action(this.InternalPrepare));
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00019C0B File Offset: 0x00017E0B
		public void Export()
		{
			this.ExecuteProcedureWithProgress(ProcedureType.Export, new Action(this.InternalExport));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00019C20 File Offset: 0x00017E20
		public void Stop()
		{
			this.IsStopRequested = true;
			Tracer.TraceInformation("ProgressController.Stop: calling abortTokenSourceForTasks.Cancel()", new object[0]);
			this.abortTokenSourceForTasks.Cancel();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00019C44 File Offset: 0x00017E44
		public void Rollback()
		{
			try
			{
				if (this.StatusManager.BeginProcedure(ProcedureType.Rollback))
				{
					Tracer.TraceInformation("ProgressController.Rollback: Rollbacking and removing status log.", new object[0]);
					this.StatusManager.Rollback(true);
				}
			}
			finally
			{
				this.StatusManager.EndProcedure();
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00019C9C File Offset: 0x00017E9C
		public void ReportProgress(ProgressRecord progressRecord)
		{
			Tracer.TraceInformation("ProgressController.ReportProgress: ProgressRecord reported. Queue count before enqueue: {0}", new object[]
			{
				this.progressQueue.Count
			});
			this.progressQueue.Enqueue(progressRecord);
			this.progressAvailable.Release(1);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019D30 File Offset: 0x00017F30
		private void ExecuteProcedureWithProgress(ProcedureType procedureType, Action action)
		{
			if (this.IsStopRequested)
			{
				Tracer.TraceInformation("ProgressController.ExecuteProcedureWithProgress: Stop requested. Procedure type: {0}", new object[]
				{
					procedureType
				});
				return;
			}
			try
			{
				if (this.StatusManager.BeginProcedure(procedureType))
				{
					ScenarioData scenarioData = ScenarioData.Current;
					Task task = Task.Factory.StartNew(delegate()
					{
						ScenarioData scenarioData;
						using (new ScenarioData(scenarioData))
						{
							this.WaitForAndProcessProgress();
						}
					});
					Exception ex = null;
					try
					{
						action();
					}
					finally
					{
						this.progressAvailable.Release(1);
						ex = AsynchronousTaskHandler.WaitForAsynchronousTask(task);
					}
					if (ex != null)
					{
						Tracer.TraceError("ProgressController.ExecuteProcedureWithProgress: Hitting exception in progress task. Exception: {0}", new object[]
						{
							ex
						});
						throw ex;
					}
				}
			}
			finally
			{
				this.StatusManager.EndProcedure();
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00019E08 File Offset: 0x00018008
		private void InternalPrepare()
		{
			this.ItemListGenerator.AllSourceInformation = this.StatusManager.AllSourceInformation;
			if (this.StatusManager.AllSourceInformation != null)
			{
				ScenarioData.Current["M"] = this.StatusManager.AllSourceInformation.Count.ToString();
			}
			this.ItemListGenerator.DataRetriever = null;
			this.ItemListGenerator.InitItemList();
			if (this.IsStopRequested)
			{
				Tracer.TraceInformation("ProgressController.InternalPrepare: Stop requested.", new object[0]);
				if (this.StatusManager.BeginProcedure(ProcedureType.Stop))
				{
					this.StatusManager.Rollback(false);
				}
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019EA8 File Offset: 0x000180A8
		private void InternalExport()
		{
			using (IContextualBatchDataWriter<List<ItemInformation>> contextualBatchDataWriter = this.target.CreateDataWriter(this))
			{
				bool errorHappened = false;
				try
				{
					if (this.StatusManager.AllSourceInformation != null)
					{
						ScenarioData.Current["M"] = this.StatusManager.AllSourceInformation.Count.ToString();
					}
					this.sourceDataProviderManager.CreateSourceServiceClients(this.StatusManager.AllSourceInformation);
					this.ItemListGenerator.AllSourceInformation = this.StatusManager.AllSourceInformation;
					int num = 0;
					this.ReportStatistics(new ExportStatusEventArgs
					{
						ActualBytes = 0L,
						ActualCount = 0,
						ActualMailboxesProcessed = 0,
						ActualMailboxesTotal = this.StatusManager.AllSourceInformation.Count,
						TotalDuration = TimeSpan.Zero
					});
					foreach (SourceInformation sourceInformation in this.StatusManager.AllSourceInformation.Values)
					{
						if (sourceInformation.Configuration != null && sourceInformation.Configuration.SourceFilter != null)
						{
							ScenarioData.Current["QL"] = sourceInformation.Configuration.SourceFilter.Length.ToString();
						}
						Tracer.TraceInformation("ProgressController.InternalExport: Exporting source '{0}'. ItemCount: {1}; ProcessedItemCount: {2}; UnsearchableItemCount: {3}; ProcessedUnsearchableItemCount: {4}; DuplicateItemCount: {5}; UnsearchableDuplicateItemCount: {6}; ErrorItemCount: {7}", new object[]
						{
							sourceInformation.Configuration.Id,
							sourceInformation.Status.ItemCount,
							sourceInformation.Status.ProcessedItemCount,
							sourceInformation.Status.UnsearchableItemCount,
							sourceInformation.Status.ProcessedUnsearchableItemCount,
							sourceInformation.Status.DuplicateItemCount,
							sourceInformation.Status.UnsearchableDuplicateItemCount,
							sourceInformation.Status.ErrorItemCount
						});
						if (this.IsStopRequested)
						{
							Tracer.TraceInformation("ProgressController.InternalExport: Stop requested.", new object[0]);
							this.StatusManager.BeginProcedure(ProcedureType.Stop);
							break;
						}
						try
						{
							num++;
							this.ExportSourceMailbox(contextualBatchDataWriter, sourceInformation, false);
							this.ExportSourceMailbox(contextualBatchDataWriter, sourceInformation, true);
							if (sourceInformation.Configuration.Id.StartsWith("\\"))
							{
								errorHappened = true;
							}
							Tracer.TraceInformation("ProgressController.InternalExport: Exporting source '{0}'. ItemCount: {1}; ProcessedItemCount: {2}; UnsearchableItemCount: {3}; ProcessedUnsearchableItemCount: {4}; DuplicateItemCount: {5}; UnsearchableDuplicateItemCount: {6}; ErrorItemCount: {7}", new object[]
							{
								sourceInformation.Configuration.Id,
								sourceInformation.Status.ItemCount,
								sourceInformation.Status.ProcessedItemCount,
								sourceInformation.Status.UnsearchableItemCount,
								sourceInformation.Status.ProcessedUnsearchableItemCount,
								sourceInformation.Status.DuplicateItemCount,
								sourceInformation.Status.UnsearchableDuplicateItemCount,
								sourceInformation.Status.ErrorItemCount
							});
						}
						catch (ExportException ex)
						{
							if (ex.ErrorType == ExportErrorType.TargetOutOfSpace)
							{
								Tracer.TraceInformation("ProgressController.InternalExport: Target level error occurs during export.: {0}", new object[]
								{
									ex
								});
								throw;
							}
							Tracer.TraceInformation("ProgressController.InternalExport: Source level error occurs during export: {0}", new object[]
							{
								ex
							});
							ProgressRecord progressRecord = new ProgressRecord();
							progressRecord.ReportSourceError(new ErrorRecord
							{
								Item = null,
								ErrorType = ex.ErrorType,
								DiagnosticMessage = ex.Message,
								SourceId = sourceInformation.Configuration.Id,
								Time = DateTime.UtcNow
							});
							this.ReportProgress(progressRecord);
						}
						finally
						{
							this.ReportStatistics(new ExportStatusEventArgs
							{
								ActualBytes = 0L,
								ActualCount = 0,
								ActualMailboxesProcessed = num,
								ActualMailboxesTotal = this.StatusManager.AllSourceInformation.Count,
								TotalDuration = TimeSpan.Zero
							});
						}
					}
					errorHappened = true;
				}
				finally
				{
					contextualBatchDataWriter.ExitPFDataContext(errorHappened);
				}
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001A334 File Offset: 0x00018534
		private void ExportSourceMailbox(IContextualBatchDataWriter<List<ItemInformation>> dataWriter, SourceInformation source, bool isUnsearchable)
		{
			Tracer.TraceInformation("ProgressController.ExportSourceMailbox: Source Id: {0}; isUnsearchable: {1}", new object[]
			{
				source.Configuration.Id,
				isUnsearchable
			});
			this.ItemListGenerator.DoUnSearchable = isUnsearchable;
			bool flag = (!isUnsearchable && source.Status.ItemCount <= 0) || (isUnsearchable && source.Status.UnsearchableItemCount <= 0);
			if ((!isUnsearchable && source.Status.ItemCount > source.Status.ProcessedItemCount) || (isUnsearchable && source.Status.UnsearchableItemCount > source.Status.ProcessedUnsearchableItemCount) || !this.target.ExportContext.IsResume || flag)
			{
				IItemIdList itemIdList = this.target.CreateItemIdList(source.Configuration.Id, isUnsearchable);
				DataContext dataContext = new DataContext(source, itemIdList);
				DataRetriever dataRetriever = new DataRetriever(dataContext, this);
				dataRetriever.DataWriter = dataWriter;
				this.ItemListGenerator.DataRetriever = dataRetriever;
				this.ItemListGenerator.DataBatchRead += this.ItemListGenerator.WriteDataBatchItemListGen;
				dataRetriever.DataBatchRead += this.ItemListGenerator.WriteDataBatchDataRetriever;
				ExportException ex = null;
				try
				{
					this.ItemListGenerator.DoExportForSourceMailbox(dataRetriever.DataContext.SourceInformation);
				}
				finally
				{
					ex = AsynchronousTaskHandler.WaitForAsynchronousTask(this.ItemListGenerator.WritingTask);
					this.ItemListGenerator.WritingTask = null;
					this.ItemListGenerator.DataRetriever = null;
				}
				if (ex != null)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001A4D0 File Offset: 0x000186D0
		private void WaitForAndProcessProgress()
		{
			ProgressRecord progressRecord = null;
			bool flag = true;
			while (flag)
			{
				this.progressAvailable.WaitOne();
				Tracer.TraceInformation("ProgressController.WaitForAndProcessProgress: Progress signaled.", new object[0]);
				flag = this.progressQueue.TryDequeue(out progressRecord);
				if (progressRecord != null)
				{
					try
					{
						if (progressRecord.RootExportedRecord != null)
						{
							this.target.ExportContext.WriteResultManifest(new ExportRecord[]
							{
								progressRecord.RootExportedRecord
							});
						}
						else if (progressRecord.SourceErrorRecord != null)
						{
							this.target.ExportContext.WriteErrorLog(new ErrorRecord[]
							{
								progressRecord.SourceErrorRecord
							});
						}
						else
						{
							if (progressRecord.ItemExportedRecords != null && progressRecord.ItemExportedRecords.Count > 0)
							{
								this.target.ExportContext.WriteResultManifest(progressRecord.ItemExportedRecords);
							}
							if (progressRecord.ItemErrorRecords != null && progressRecord.ItemErrorRecords.Count > 0)
							{
								this.target.ExportContext.WriteErrorLog(progressRecord.ItemErrorRecords);
								foreach (ErrorRecord errorRecord in progressRecord.ItemErrorRecords)
								{
									this.SearchResults.IncrementErrorItemCount(errorRecord.Item.SourceId);
								}
							}
							this.ReportStatistics(new ExportStatusEventArgs
							{
								ActualBytes = progressRecord.Size,
								ActualCount = progressRecord.ItemExportedRecords.Count + progressRecord.ItemErrorRecords.Count,
								ActualMailboxesProcessed = 0,
								ActualMailboxesTotal = 0,
								TotalDuration = progressRecord.Duration
							});
							progressRecord.DataContext.ProcessedItemCount += progressRecord.ItemExportedRecords.Count + progressRecord.ItemErrorRecords.Count;
							this.StatusManager.Checkpoint(progressRecord.DataContext.SourceId);
						}
					}
					catch (Exception ex)
					{
						Tracer.TraceError("ProgressController.WaitForAndProcessProgress: Progress reporting thread error: {0}", new object[]
						{
							ex
						});
						this.Stop();
						throw;
					}
				}
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001A708 File Offset: 0x00018908
		private void ReportStatistics(ExportStatusEventArgs args)
		{
			EventHandler<ExportStatusEventArgs> onReportStatistics = this.OnReportStatistics;
			if (onReportStatistics != null)
			{
				onReportStatistics(this, args);
			}
		}

		// Token: 0x04000268 RID: 616
		private Semaphore progressAvailable;

		// Token: 0x04000269 RID: 617
		private ConcurrentQueue<ProgressRecord> progressQueue;

		// Token: 0x0400026A RID: 618
		private ITarget target;

		// Token: 0x0400026B RID: 619
		private SourceDataProviderManager sourceDataProviderManager;

		// Token: 0x0400026C RID: 620
		private bool isDocIdHintFlightingEnabled;

		// Token: 0x0400026D RID: 621
		private CancellationTokenSource abortTokenSourceForTasks;

		// Token: 0x02000062 RID: 98
		private class TargetWrapper : ITarget
		{
			// Token: 0x06000728 RID: 1832 RVA: 0x0001A727 File Offset: 0x00018927
			internal TargetWrapper(ITarget target)
			{
				this.ExportContextInternal = new ProgressController.ExportContextWrapper(target.ExportContext);
				this.TargetInternal = target;
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001A747 File Offset: 0x00018947
			public IExportContext ExportContext
			{
				get
				{
					return this.ExportContextInternal;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001A74F File Offset: 0x0001894F
			// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001A75C File Offset: 0x0001895C
			public ExportSettings ExportSettings
			{
				get
				{
					return this.TargetInternal.ExportSettings;
				}
				set
				{
					this.TargetInternal.ExportSettings = value;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001A76A File Offset: 0x0001896A
			// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001A772 File Offset: 0x00018972
			internal ProgressController.ExportContextWrapper ExportContextInternal { get; private set; }

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001A77B File Offset: 0x0001897B
			// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001A783 File Offset: 0x00018983
			private ITarget TargetInternal { get; set; }

			// Token: 0x06000730 RID: 1840 RVA: 0x0001A78C File Offset: 0x0001898C
			public IItemIdList CreateItemIdList(string mailboxId, bool isUnsearchable)
			{
				return this.TargetInternal.CreateItemIdList(mailboxId, isUnsearchable);
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x0001A79B File Offset: 0x0001899B
			public void RemoveItemIdList(string mailboxId, bool isUnsearchable)
			{
				this.TargetInternal.RemoveItemIdList(mailboxId, isUnsearchable);
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x0001A7AA File Offset: 0x000189AA
			public IContextualBatchDataWriter<List<ItemInformation>> CreateDataWriter(IProgressController progressController)
			{
				return this.TargetInternal.CreateDataWriter(progressController);
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x0001A7B8 File Offset: 0x000189B8
			public void Rollback(SourceInformationCollection allSourceInformation)
			{
				this.TargetInternal.Rollback(allSourceInformation);
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x0001A7C6 File Offset: 0x000189C6
			public IStatusLog GetStatusLog()
			{
				return this.TargetInternal.GetStatusLog();
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x0001A7D3 File Offset: 0x000189D3
			public void CheckInitialStatus(SourceInformationCollection allSourceInformation, OperationStatus status)
			{
				this.TargetInternal.CheckInitialStatus(allSourceInformation, status);
			}
		}

		// Token: 0x02000063 RID: 99
		private class ExportContextWrapper : IExportContext
		{
			// Token: 0x06000736 RID: 1846 RVA: 0x0001A7E2 File Offset: 0x000189E2
			internal ExportContextWrapper(IExportContext exportContext)
			{
				this.ExportMetadataInternal = new ProgressController.ExportMetadataWrapper(exportContext.ExportMetadata);
				this.ExportContextInternal = exportContext;
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001A802 File Offset: 0x00018A02
			public bool IsResume
			{
				get
				{
					return this.ExportContextInternal.IsResume;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001A80F File Offset: 0x00018A0F
			public IExportMetadata ExportMetadata
			{
				get
				{
					return this.ExportMetadataInternal;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001A817 File Offset: 0x00018A17
			public IList<ISource> Sources
			{
				get
				{
					return this.ExportContextInternal.Sources;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001A824 File Offset: 0x00018A24
			public ITargetLocation TargetLocation
			{
				get
				{
					return this.ExportContextInternal.TargetLocation;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001A831 File Offset: 0x00018A31
			// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001A839 File Offset: 0x00018A39
			internal ProgressController.ExportMetadataWrapper ExportMetadataInternal { get; private set; }

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001A842 File Offset: 0x00018A42
			// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001A84A File Offset: 0x00018A4A
			private IExportContext ExportContextInternal { get; set; }

			// Token: 0x0600073F RID: 1855 RVA: 0x0001A853 File Offset: 0x00018A53
			public void WriteResultManifest(IEnumerable<ExportRecord> records)
			{
				this.ExportContextInternal.WriteResultManifest(records);
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x0001A861 File Offset: 0x00018A61
			public void WriteErrorLog(IEnumerable<ErrorRecord> errorRecords)
			{
				this.ExportContextInternal.WriteErrorLog(errorRecords);
			}
		}

		// Token: 0x02000064 RID: 100
		private class ExportMetadataWrapper : IExportMetadata
		{
			// Token: 0x06000741 RID: 1857 RVA: 0x0001A86F File Offset: 0x00018A6F
			internal ExportMetadataWrapper(IExportMetadata exportMetadata)
			{
				this.ExportMetadataInternal = exportMetadata;
				this.IncludeUnsearchableItemsInternal = exportMetadata.IncludeUnsearchableItems;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001A88A File Offset: 0x00018A8A
			public string ExportName
			{
				get
				{
					return this.ExportMetadataInternal.ExportName;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001A897 File Offset: 0x00018A97
			public string ExportId
			{
				get
				{
					return this.ExportMetadataInternal.ExportId;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001A8A4 File Offset: 0x00018AA4
			public DateTime ExportStartTime
			{
				get
				{
					return this.ExportMetadataInternal.ExportStartTime;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001A8B1 File Offset: 0x00018AB1
			public bool RemoveRms
			{
				get
				{
					return this.ExportMetadataInternal.RemoveRms;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001A8BE File Offset: 0x00018ABE
			public bool IncludeDuplicates
			{
				get
				{
					return this.ExportMetadataInternal.IncludeDuplicates;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001A8CB File Offset: 0x00018ACB
			// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001A8D3 File Offset: 0x00018AD3
			public bool IncludeUnsearchableItems
			{
				get
				{
					return this.IncludeUnsearchableItemsInternal;
				}
				set
				{
					this.IncludeUnsearchableItemsInternal = value;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001A8DC File Offset: 0x00018ADC
			public bool IncludeSearchableItems
			{
				get
				{
					return this.ExportMetadataInternal.IncludeSearchableItems;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001A8E9 File Offset: 0x00018AE9
			public int EstimateItems
			{
				get
				{
					return this.ExportMetadataInternal.EstimateItems;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001A8F6 File Offset: 0x00018AF6
			public ulong EstimateBytes
			{
				get
				{
					return this.ExportMetadataInternal.EstimateBytes;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001A903 File Offset: 0x00018B03
			public string Language
			{
				get
				{
					return this.ExportMetadataInternal.Language;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001A910 File Offset: 0x00018B10
			// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001A918 File Offset: 0x00018B18
			internal bool IncludeUnsearchableItemsInternal { private get; set; }

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001A921 File Offset: 0x00018B21
			// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001A929 File Offset: 0x00018B29
			private IExportMetadata ExportMetadataInternal { get; set; }
		}
	}
}
