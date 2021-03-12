using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200007B RID: 123
	internal class StatusManager : IStatusManager, IDisposable
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001E7E1 File Offset: 0x0001C9E1
		public StatusManager(ITarget target)
		{
			this.target = target;
			this.EnsureStatusLogValid();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001E7F6 File Offset: 0x0001C9F6
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001E7FE File Offset: 0x0001C9FE
		public OperationStatus CurrentStatus { get; internal set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001E807 File Offset: 0x0001CA07
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001E80F File Offset: 0x0001CA0F
		public SourceInformationCollection AllSourceInformation { get; internal set; }

		// Token: 0x060007FD RID: 2045 RVA: 0x0001E818 File Offset: 0x0001CA18
		public void Dispose()
		{
			this.CloseStatusLog();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001E820 File Offset: 0x0001CA20
		public void Rollback(bool removeStatusLog)
		{
			lock (this)
			{
				this.EnsureStatusLogValid();
				Tracer.TraceInformation("StatusManager.Rollback: removeStatusLog: {0}; CurrentStatus: {1}", new object[]
				{
					removeStatusLog,
					this.CurrentStatus
				});
				if (this.CurrentStatus != OperationStatus.Rollbacking)
				{
					throw new ArgumentException("IStatusManager.Rollback(bool) should be only called when the status is Rollbacking");
				}
				this.target.Rollback(this.AllSourceInformation);
				if (removeStatusLog)
				{
					if (this.statusLog != null)
					{
						this.statusLog.Delete();
						this.statusLog.Dispose();
						this.statusLog = null;
					}
					this.CurrentStatus = OperationStatus.None;
					this.AllSourceInformation = null;
				}
				else
				{
					this.ResetStatusToDefault();
				}
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001E924 File Offset: 0x0001CB24
		public bool BeginProcedure(ProcedureType procedureRequest)
		{
			bool flag = false;
			lock (this)
			{
				this.EnsureStatusLogValid();
				Tracer.TraceInformation("StatusManager.BeginProcedure: procedureRequest: {0}; CurrentStatus: {1}", new object[]
				{
					procedureRequest,
					this.CurrentStatus
				});
				bool flag3 = true;
				OperationStatus operationStatus = this.CurrentStatus;
				switch (procedureRequest)
				{
				case ProcedureType.Prepare:
				{
					OperationStatus currentStatus = this.CurrentStatus;
					if (currentStatus != OperationStatus.Pending)
					{
						switch (currentStatus)
						{
						case OperationStatus.SearchCompleted:
							operationStatus = (this.AllSourceInformation.Values.Any((SourceInformation source) => !source.Status.IsSearchCompleted(this.target.ExportContext.ExportMetadata.IncludeSearchableItems, this.target.ExportContext.ExportMetadata.IncludeUnsearchableItems)) ? OperationStatus.Searching : OperationStatus.SearchCompleted);
							goto IL_185;
						case OperationStatus.PartiallyProcessed:
							operationStatus = OperationStatus.RetrySearching;
							goto IL_185;
						case OperationStatus.Processed:
							goto IL_185;
						}
						flag3 = false;
					}
					else
					{
						operationStatus = OperationStatus.Searching;
					}
					break;
				}
				case ProcedureType.Export:
					switch (this.CurrentStatus)
					{
					case OperationStatus.SearchCompleted:
					case OperationStatus.PartiallyProcessed:
						operationStatus = OperationStatus.Processing;
						goto IL_185;
					case OperationStatus.Processed:
						goto IL_185;
					}
					flag3 = false;
					break;
				case ProcedureType.Stop:
					switch (this.CurrentStatus)
					{
					case OperationStatus.Searching:
						operationStatus = OperationStatus.Rollbacking;
						break;
					case OperationStatus.RetrySearching:
					case OperationStatus.Processing:
						operationStatus = OperationStatus.Stopping;
						break;
					}
					break;
				case ProcedureType.Rollback:
					switch (this.CurrentStatus)
					{
					case OperationStatus.Pending:
						goto IL_185;
					case OperationStatus.Searching:
					case OperationStatus.RetrySearching:
					case OperationStatus.Stopping:
					case OperationStatus.Processing:
					case OperationStatus.Rollbacking:
						flag3 = false;
						goto IL_185;
					}
					operationStatus = OperationStatus.Rollbacking;
					break;
				default:
					throw new NotSupportedException();
				}
				IL_185:
				if (!flag3)
				{
					Tracer.TraceError("StatusManager.BeginProcedure: !valid: throwing exception", new object[0]);
					throw new ExportException(ExportErrorType.OperationNotSupportedWithCurrentStatus, string.Format(CultureInfo.CurrentCulture, "Procedure '{0}' is not supported when the operation status is '{1}' while 'IsResume' flag is '{2}'", new object[]
					{
						procedureRequest,
						this.CurrentStatus,
						this.target.ExportContext.IsResume
					}));
				}
				if (this.CurrentStatus != operationStatus)
				{
					this.CurrentStatus = operationStatus;
					flag = (operationStatus != OperationStatus.Stopping);
					Tracer.TraceInformation("StatusManager.BeginProcedure: shouldContinue: {0}", new object[]
					{
						flag ? "true" : "false"
					});
					this.Checkpoint(null);
				}
			}
			return flag;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public void EndProcedure()
		{
			lock (this)
			{
				Tracer.TraceInformation("StatusManager.EndProcedure: status log empty?: {0}; CurrentStatus: {1}", new object[]
				{
					this.statusLog == null,
					this.CurrentStatus
				});
				if (this.statusLog != null)
				{
					OperationStatus operationStatus = this.CurrentStatus;
					switch (this.CurrentStatus)
					{
					case OperationStatus.None:
						throw new NotSupportedException("Bug : EndProcedure shouldn't be called when the status is None");
					case OperationStatus.Searching:
						operationStatus = OperationStatus.SearchCompleted;
						break;
					case OperationStatus.RetrySearching:
					case OperationStatus.Stopping:
						operationStatus = OperationStatus.PartiallyProcessed;
						break;
					case OperationStatus.Processing:
						operationStatus = (this.AllSourceInformation.Values.Any((SourceInformation source) => !source.Status.IsSearchCompleted(this.target.ExportContext.ExportMetadata.IncludeSearchableItems, this.target.ExportContext.ExportMetadata.IncludeUnsearchableItems) || source.Status.ItemCount > source.Status.ProcessedItemCount) ? OperationStatus.PartiallyProcessed : OperationStatus.Processed);
						break;
					case OperationStatus.Rollbacking:
						operationStatus = OperationStatus.Pending;
						break;
					}
					if (this.CurrentStatus != operationStatus)
					{
						this.CurrentStatus = operationStatus;
						this.Checkpoint(null);
					}
				}
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		public void Checkpoint(string sourceId)
		{
			lock (this)
			{
				Tracer.TraceInformation("StatusManager.Checkpoint: sourceId: {0}", new object[]
				{
					sourceId
				});
				this.EnsureStatusLogValid();
				if (string.IsNullOrEmpty(sourceId))
				{
					this.statusLog.UpdateStatus(this.AllSourceInformation, this.CurrentStatus);
				}
				else
				{
					this.statusLog.UpdateSourceStatus(this.AllSourceInformation[sourceId], this.AllSourceInformation.GetSourceIndex(sourceId));
				}
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001ED94 File Offset: 0x0001CF94
		private static bool IsTransientStatus(OperationStatus status)
		{
			switch (status)
			{
			case OperationStatus.Searching:
			case OperationStatus.RetrySearching:
			case OperationStatus.Stopping:
			case OperationStatus.Processing:
			case OperationStatus.Rollbacking:
				return true;
			}
			return false;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001EDD0 File Offset: 0x0001CFD0
		private void RollbackToCheckpoint()
		{
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		private void CloseStatusLog()
		{
			Tracer.TraceInformation("StatusManager.CloseStatusLog: status log empty?: {0}", new object[]
			{
				this.statusLog == null
			});
			if (this.statusLog != null)
			{
				this.statusLog.Dispose();
				this.statusLog = null;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001EE20 File Offset: 0x0001D020
		private void EnsureStatusLogValid()
		{
			if (this.statusLog == null)
			{
				Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: initializing status log in memory", new object[0]);
				try
				{
					this.statusLog = this.target.GetStatusLog();
					SourceInformationCollection sourceInformationCollection = null;
					OperationStatus currentStatus = OperationStatus.None;
					ExportSettings exportSettings = this.statusLog.LoadStatus(out sourceInformationCollection, out currentStatus);
					if (sourceInformationCollection == null)
					{
						Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: creating status log since no existing log found", new object[0]);
						if (this.target.ExportContext.IsResume)
						{
							throw new ExportException(ExportErrorType.StatusNotFoundToResume, "This is a resume request of previous job. But there is no corresponding previous job found.");
						}
						this.ResetStatusToDefault();
						this.target.ExportSettings = new ExportSettings
						{
							ExportTime = this.target.ExportContext.ExportMetadata.ExportStartTime,
							IncludeDuplicates = this.target.ExportContext.ExportMetadata.IncludeDuplicates,
							IncludeSearchableItems = this.target.ExportContext.ExportMetadata.IncludeSearchableItems,
							IncludeUnsearchableItems = this.target.ExportContext.ExportMetadata.IncludeUnsearchableItems,
							RemoveRms = this.target.ExportContext.ExportMetadata.RemoveRms
						};
						this.statusLog.ResetStatusLog(this.AllSourceInformation, this.CurrentStatus, this.target.ExportSettings);
					}
					else
					{
						Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: Using existing status log", new object[0]);
						if (!this.target.ExportContext.IsResume)
						{
							throw new ExportException(ExportErrorType.ExistingStatusMustBeAResumeOperation, "This operation has previous job status so it must be a resume operation this time.");
						}
						if (exportSettings == null)
						{
							throw new ExportException(ExportErrorType.CorruptedStatus, "The export settings in the status log is missing or corrupted.");
						}
						this.target.ExportSettings = exportSettings;
						bool flag = false;
						if (this.target.ExportContext.ExportMetadata.IncludeDuplicates == this.target.ExportSettings.IncludeDuplicates && this.target.ExportContext.ExportMetadata.IncludeSearchableItems == this.target.ExportSettings.IncludeSearchableItems && this.target.ExportContext.ExportMetadata.IncludeUnsearchableItems == this.target.ExportSettings.IncludeUnsearchableItems && this.target.ExportContext.ExportMetadata.RemoveRms == this.target.ExportSettings.RemoveRms && this.target.ExportContext.Sources.Count == sourceInformationCollection.Count)
						{
							using (IEnumerator<ISource> enumerator = this.target.ExportContext.Sources.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ISource source = enumerator.Current;
									try
									{
										SourceInformation sourceInformation = sourceInformationCollection[source.Id];
										if (sourceInformation.Configuration.Name != source.Name || sourceInformation.Configuration.SourceFilter != source.SourceFilter || sourceInformation.Configuration.LegacyExchangeDN != source.LegacyExchangeDN)
										{
											Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: Configuration changed for source '{0}'", new object[]
											{
												source.Id
											});
											flag = true;
											break;
										}
									}
									catch (KeyNotFoundException)
									{
										Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: Configuration changed(new source detected) for source '{0}'", new object[]
										{
											source.Id
										});
										flag = true;
										break;
									}
								}
								goto IL_37E;
							}
						}
						Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: Configuration changed(source count doesn't match). Original source count: {0}; New source count: {1}", new object[]
						{
							sourceInformationCollection.Count,
							this.target.ExportContext.Sources.Count
						});
						flag = true;
						IL_37E:
						if (flag)
						{
							throw new ExportException(ExportErrorType.CannotResumeWithConfigurationChange);
						}
						this.AllSourceInformation = sourceInformationCollection;
						this.CurrentStatus = currentStatus;
						if (StatusManager.IsTransientStatus(this.CurrentStatus))
						{
							Tracer.TraceInformation("StatusManager.EnsureStatusLogValid: Recovering on status: {0}", new object[]
							{
								this.CurrentStatus
							});
							this.Recover();
						}
					}
					this.target.CheckInitialStatus(this.AllSourceInformation, this.CurrentStatus);
				}
				catch (Exception ex)
				{
					Tracer.TraceError("StatusManager.EnsureStatusLogValid: Exception happend, closing status log. Exception: {0}", new object[]
					{
						ex
					});
					this.CloseStatusLog();
					throw;
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001F284 File Offset: 0x0001D484
		private void ResetStatusToDefault()
		{
			Tracer.TraceInformation("StatusManager.ResetStatusToDefault", new object[0]);
			this.AllSourceInformation = new SourceInformationCollection(this.target.ExportContext.Sources.Count);
			foreach (ISource source in this.target.ExportContext.Sources)
			{
				SourceInformation value = new SourceInformation(source.Name, source.Id, source.SourceFilter, source.ServiceEndpoint, source.LegacyExchangeDN);
				this.AllSourceInformation[source.Id] = value;
			}
			this.CurrentStatus = OperationStatus.Pending;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001F344 File Offset: 0x0001D544
		private void Recover()
		{
			switch (this.CurrentStatus)
			{
			case OperationStatus.Searching:
			case OperationStatus.Rollbacking:
				this.CurrentStatus = OperationStatus.Rollbacking;
				this.Rollback(false);
				this.CurrentStatus = OperationStatus.Pending;
				this.Checkpoint(null);
				return;
			case OperationStatus.RetrySearching:
			case OperationStatus.Stopping:
			case OperationStatus.Processing:
				this.RollbackToCheckpoint();
				this.CurrentStatus = OperationStatus.PartiallyProcessed;
				this.Checkpoint(null);
				break;
			case OperationStatus.SearchCompleted:
			case OperationStatus.PartiallyProcessed:
			case OperationStatus.Processed:
				break;
			default:
				return;
			}
		}

		// Token: 0x040002F1 RID: 753
		private ITarget target;

		// Token: 0x040002F2 RID: 754
		private IStatusLog statusLog;
	}
}
