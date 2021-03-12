using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000019 RID: 25
	internal class PstTarget : ITarget
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00003786 File Offset: 0x00001986
		public PstTarget(IExportContext exportContext)
		{
			this.ExportContext = exportContext;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003795 File Offset: 0x00001995
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000379D File Offset: 0x0000199D
		public IExportContext ExportContext { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000037A6 File Offset: 0x000019A6
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000037AE File Offset: 0x000019AE
		public ExportSettings ExportSettings { get; set; }

		// Token: 0x060000CC RID: 204 RVA: 0x000037B7 File Offset: 0x000019B7
		public IStatusLog GetStatusLog()
		{
			return new PstStatusLog(this.GetStatusFilePath());
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000037C4 File Offset: 0x000019C4
		public IItemIdList CreateItemIdList(string mailboxId, bool isUnsearchable)
		{
			return new LocalFileItemIdList(mailboxId, this.GetItemIdListFilePath(mailboxId, isUnsearchable), isUnsearchable);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000037D5 File Offset: 0x000019D5
		public void RemoveItemIdList(string mailboxId, bool isUnsearchable)
		{
			LocalFileHelper.RemoveFile(this.GetItemIdListFilePath(mailboxId, isUnsearchable), ExportErrorType.FailedToRemoveItemIdList);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000037E6 File Offset: 0x000019E6
		public IContextualBatchDataWriter<List<ItemInformation>> CreateDataWriter(IProgressController progressController)
		{
			return new PstWriter(this, progressController);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000037EF File Offset: 0x000019EF
		public string GetStatusFilePath()
		{
			return Path.Combine(this.ExportContext.TargetLocation.WorkingLocation, Uri.EscapeDataString(this.ExportContext.ExportMetadata.ExportName) + ".status");
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003828 File Offset: 0x00001A28
		public string GetPstFilePath(string sourceName, bool isUnsearchable, int pstMBFileCount)
		{
			string text = string.IsNullOrEmpty(sourceName) ? string.Empty : ("-" + sourceName);
			string text2 = string.Empty;
			if (pstMBFileCount > 1)
			{
				text2 = string.Format(CultureInfo.CurrentCulture, "{0}{1}-{2}-{3}", new object[]
				{
					this.ExportContext.ExportMetadata.ExportName,
					text,
					this.ExportSettings.ExportTime.ToString("MM.dd.yyyy-HHmmtt", CultureInfo.InvariantCulture),
					pstMBFileCount
				});
			}
			else
			{
				text2 = string.Format(CultureInfo.CurrentCulture, "{0}{1}-{2}", new object[]
				{
					this.ExportContext.ExportMetadata.ExportName,
					text,
					this.ExportSettings.ExportTime.ToString("MM.dd.yyyy-HHmmtt", CultureInfo.InvariantCulture)
				});
			}
			string path = ((text2.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) ? Uri.EscapeDataString(text2) : text2) + (isUnsearchable ? "_unsearchable.pst" : ".pst");
			return Path.Combine(isUnsearchable ? this.ExportContext.TargetLocation.UnsearchableExportLocation : this.ExportContext.TargetLocation.ExportLocation, path);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003964 File Offset: 0x00001B64
		public string GetPFPstFilePath(bool isUnsearchable, int pstPFFileCount)
		{
			string text = "-Public Folders";
			string text2 = string.Format(CultureInfo.CurrentCulture, "{0}{1}-{2}-{3}", new object[]
			{
				this.ExportContext.ExportMetadata.ExportName,
				text,
				this.ExportSettings.ExportTime.ToString("MM.dd.yyyy-HHmmtt", CultureInfo.InvariantCulture),
				pstPFFileCount
			});
			string path = ((text2.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) ? Uri.EscapeDataString(text2) : text2) + (isUnsearchable ? "_unsearchable.pst" : ".pst");
			return Path.Combine(isUnsearchable ? this.ExportContext.TargetLocation.UnsearchableExportLocation : this.ExportContext.TargetLocation.ExportLocation, path);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003A28 File Offset: 0x00001C28
		public string GetItemIdListFilePath(string sourceId, bool isUnsearchable)
		{
			if (isUnsearchable)
			{
				return Path.Combine(this.ExportContext.TargetLocation.WorkingLocation, Uri.EscapeDataString(sourceId) + this.ExportSettings.ExportTime.Ticks + ".unsearchable.itemlist");
			}
			return Path.Combine(this.ExportContext.TargetLocation.WorkingLocation, Uri.EscapeDataString(sourceId) + this.ExportSettings.ExportTime.Ticks + ".itemlist");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public void Rollback(SourceInformationCollection allSourceInformation)
		{
			Tracer.TraceInformation("PstTarget.Rollback - Start", new object[0]);
			Tracer.TraceInformation("PstTarget.Rollback IncludeDuplicates {0}, IncludeSearchableItems {1}, IncludeUnsearchableItems {2}", new object[]
			{
				this.ExportContext.ExportMetadata.IncludeDuplicates ? "true" : "false",
				this.ExportContext.ExportMetadata.IncludeSearchableItems ? "true" : "false",
				this.ExportContext.ExportMetadata.IncludeUnsearchableItems ? "true" : "false"
			});
			foreach (SourceInformation sourceInformation in allSourceInformation.Values)
			{
				Tracer.TraceInformation("PstTarget.Rollback source Id {0}, source Name {1}", new object[]
				{
					sourceInformation.Configuration.Id,
					sourceInformation.Configuration.Name
				});
				bool isPublicFolder = sourceInformation.Configuration.Id.StartsWith("\\");
				if (this.ExportContext.ExportMetadata.IncludeDuplicates)
				{
					if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
					{
						this.RemoveFileForSource(sourceInformation.Configuration.Name, false, isPublicFolder);
					}
					if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
					{
						this.RemoveFileForSource(sourceInformation.Configuration.Name, true, isPublicFolder);
					}
				}
				if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
				{
					string itemIdListFilePath = this.GetItemIdListFilePath(sourceInformation.Configuration.Id, false);
					LocalFileHelper.RemoveFile(itemIdListFilePath, ExportErrorType.FailedToRollbackResultsInTargetLocation);
				}
				if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
				{
					string itemIdListFilePath2 = this.GetItemIdListFilePath(sourceInformation.Configuration.Id, true);
					LocalFileHelper.RemoveFile(itemIdListFilePath2, ExportErrorType.FailedToRollbackResultsInTargetLocation);
				}
			}
			if (!this.ExportContext.ExportMetadata.IncludeDuplicates)
			{
				if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
				{
					this.RemoveFileForSource(null, false, false);
					this.RemoveFileForSource(null, false, true);
				}
				if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
				{
					this.RemoveFileForSource(null, true, false);
					this.RemoveFileForSource(null, true, true);
				}
			}
			Tracer.TraceInformation("PstTarget.Rollback - End", new object[0]);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003D0C File Offset: 0x00001F0C
		public void CheckLocation()
		{
			if (string.IsNullOrEmpty(this.ExportContext.TargetLocation.WorkingLocation))
			{
				throw new ArgumentNullException("WorkingLocation");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(this.ExportContext.TargetLocation.WorkingLocation);
			if (!directoryInfo.Exists)
			{
				Directory.CreateDirectory(directoryInfo.FullName);
			}
			if (string.IsNullOrEmpty(this.ExportContext.TargetLocation.ExportLocation) && this.ExportContext.ExportMetadata.IncludeSearchableItems)
			{
				throw new ArgumentNullException("ExportLocation");
			}
			if (string.IsNullOrEmpty(this.ExportContext.TargetLocation.UnsearchableExportLocation) && this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
			{
				throw new ArgumentNullException("UnsearchableExportLocation");
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public void CheckInitialStatus(SourceInformationCollection allSourceInformation, OperationStatus status)
		{
			foreach (SourceInformation sourceInformation in allSourceInformation.Values)
			{
				bool isPublicFolder = sourceInformation.Configuration.Id.StartsWith("\\");
				if (this.ExportContext.ExportMetadata.IncludeDuplicates)
				{
					if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
					{
						this.ValidateDataFile(sourceInformation.Configuration.Name, false, isPublicFolder);
					}
					if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
					{
						this.ValidateDataFile(sourceInformation.Configuration.Name, true, isPublicFolder);
					}
				}
				if (!this.ExportContext.IsResume || !sourceInformation.Status.IsSearchCompleted(this.ExportContext.ExportMetadata.IncludeSearchableItems, this.ExportContext.ExportMetadata.IncludeUnsearchableItems))
				{
					if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
					{
						string itemIdListFilePath = this.GetItemIdListFilePath(sourceInformation.Configuration.Id, false);
						LocalFileHelper.RemoveFile(itemIdListFilePath, ExportErrorType.FailedToCleanupCorruptedStatusLog);
					}
					if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
					{
						string itemIdListFilePath2 = this.GetItemIdListFilePath(sourceInformation.Configuration.Id, true);
						LocalFileHelper.RemoveFile(itemIdListFilePath2, ExportErrorType.FailedToCleanupCorruptedStatusLog);
					}
				}
			}
			if (!this.ExportContext.ExportMetadata.IncludeDuplicates)
			{
				if (this.ExportContext.ExportMetadata.IncludeSearchableItems)
				{
					this.ValidateDataFile(null, false, false);
				}
				if (this.ExportContext.ExportMetadata.IncludeUnsearchableItems)
				{
					this.ValidateDataFile(null, true, false);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003F84 File Offset: 0x00002184
		private void ValidateDataFile(string sourceName, bool isUnsearchable, bool isPublicFolder)
		{
			string text = string.Empty;
			if (isPublicFolder)
			{
				text = this.GetPFPstFilePath(isUnsearchable, 1);
			}
			else
			{
				text = this.GetPstFilePath(sourceName, isUnsearchable, 1);
			}
			if (File.Exists(text))
			{
				try
				{
					PstWriter.CreatePstSession(text).Close();
				}
				catch (ExportException ex)
				{
					Tracer.TraceError("PstTarget.ValidateDateFile: Failed to create PST session. Exception: {0}", new object[]
					{
						ex
					});
					throw new ExportException(ExportErrorType.FailedToOpenExistingPstFile, text);
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003FF4 File Offset: 0x000021F4
		private void RemoveFileForSource(string source, bool isUnsearchable, bool isPublicFolder)
		{
			Tracer.TraceInformation("PstTarget.RemoveFileForSource ", new object[0]);
			int num = 1000;
			string text = string.Empty;
			bool flag = true;
			int num2 = 1;
			while (flag)
			{
				if (isPublicFolder)
				{
					text = this.GetPFPstFilePath(isUnsearchable, num2++);
				}
				else
				{
					text = this.GetPstFilePath(source, isUnsearchable, num2++);
				}
				if (File.Exists(text))
				{
					try
					{
						LocalFileHelper.RemoveFile(text, ExportErrorType.FailedToRollbackResultsInTargetLocation);
						goto IL_81;
					}
					catch (ExportException ex)
					{
						Tracer.TraceError("PstTarget.RemoveFileForSource: Failed FileName: {0}, Exception: {1}", new object[]
						{
							text,
							ex.ToString()
						});
						goto IL_81;
					}
					goto IL_7F;
				}
				goto IL_7F;
				IL_81:
				if (num2 > num)
				{
					Tracer.TraceError("PstTarget.RemoveFileForSource: Exceeded fileCount limit of {0} files for source {1}.", new object[]
					{
						num2,
						source
					});
					return;
				}
				continue;
				IL_7F:
				flag = false;
				goto IL_81;
			}
		}
	}
}
