using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Service.Common;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200001D RID: 29
	internal class RelogManager : IDisposable
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00006A54 File Offset: 0x00004C54
		public RelogManager(string watchedDirectory, TimeSpan minExecutionTime)
		{
			Logger.LogInformationMessage("RelogManager: A RelogManager instance is created - watchedDirectory: '{0}' minExecutionTime: '{1}'", new object[]
			{
				watchedDirectory,
				minExecutionTime
			});
			this.watchedDirectory = watchedDirectory;
			this.minExecutionTime = minExecutionTime;
			this.lockObject = new object();
			this.avgRelogExecuteTime = new SampleAverage(100);
			this.fileToBeDeleted = new HashSet<string>();
			this.watcher = new FileSystemWatcher();
			this.watcher.Path = this.watchedDirectory;
			this.watcher.NotifyFilter = NotifyFilters.FileName;
			this.watcher.Filter = "*.blg";
			this.watcher.Created += this.OnInputFileMovedToWatchedFolder;
			this.watcher.EnableRaisingEvents = true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006B12 File Offset: 0x00004D12
		public void Dispose()
		{
			if (this.watcher != null)
			{
				this.watcher.Dispose();
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006B30 File Offset: 0x00004D30
		private void OnInputFileMovedToWatchedFolder(object source, FileSystemEventArgs arg)
		{
			Logger.LogInformationMessage("RelogManager: Checking the watchedDirectory: '{0}'", new object[]
			{
				this.watchedDirectory
			});
			lock (this.lockObject)
			{
				try
				{
					FileInfo[] files = new DirectoryInfo(this.watchedDirectory).GetFiles("*.blg");
					if (files.Length > 1)
					{
						FileInfo[] array = (from file in files
						orderby file.CreationTimeUtc
						select file).ToArray<FileInfo>();
						for (int i = 0; i < array.Length - 1; i++)
						{
							this.ProcessBlg(array[i].FullName);
						}
					}
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("RelogManager: Unhandled Exception. Watched Directory: '{0}' '{1}'", new object[]
					{
						this.watchedDirectory,
						ex
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_RelogManagerUnhandledException, new object[]
					{
						ex
					});
					throw;
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006C40 File Offset: 0x00004E40
		private void ProcessBlg(string blgFullPath)
		{
			Logger.LogInformationMessage("RelogManager: Relogging a blg file: '{0}'", new object[]
			{
				blgFullPath
			});
			this.DeleteProcessedFiles();
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(blgFullPath);
			string text = Path.Combine(Path.GetDirectoryName(blgFullPath), fileNameWithoutExtension);
			using (Process process = new Process())
			{
				process.StartInfo.FileName = "relog.exe";
				process.StartInfo.Arguments = string.Format("\"{0}.blg\" -f csv -o \"{1}.csvtmp\" -y", text, text);
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					process.Start();
				}
				catch (Win32Exception ex)
				{
					Logger.LogErrorMessage("RelogManager: Exception when trying to start relog.exe: '{0}'", new object[]
					{
						ex
					});
					return;
				}
				int num = (int)Math.Max(this.minExecutionTime.TotalMilliseconds, this.avgRelogExecuteTime.AverageValue * 5.0);
				if (process.WaitForExit(num))
				{
					TimeSpan timeSpan = DateTime.UtcNow - utcNow;
					if (process.ExitCode == 0)
					{
						this.avgRelogExecuteTime.AddNewSample(timeSpan.TotalMilliseconds);
					}
					Logger.LogInformationMessage("RelogManager: relog.exe exits. Elapsed time: '{0}' Avg time msec (last 100): '{1}'", new object[]
					{
						timeSpan,
						this.avgRelogExecuteTime.AverageValue
					});
				}
				else
				{
					try
					{
						Logger.LogErrorMessage("RelogManager: Killing relog.exe since it takes too long. Elapsed time: '{0}'", new object[]
						{
							num
						});
						process.Kill();
					}
					catch (Win32Exception ex2)
					{
						Logger.LogErrorMessage("RelogManager: Exception when trying to kill relog.exe: '{0}'", new object[]
						{
							ex2
						});
					}
					catch (InvalidOperationException)
					{
					}
				}
				this.ProcessResult(process, utcNow, blgFullPath, text);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006E64 File Offset: 0x00005064
		private void ProcessResult(Process process, DateTime startTime, string blgFullPath, string fullPathWithoutExtension)
		{
			bool flag = false;
			if (process.HasExited)
			{
				flag = (process.ExitCode == 0);
				string text = process.StandardOutput.ReadToEnd();
				string text2 = process.StandardError.ReadToEnd();
				if (flag)
				{
					Logger.LogInformationMessage("RelogManager: relog.exe exits cleanly. A csv is created. Relog output: '{0}'", new object[]
					{
						text
					});
				}
				else
				{
					if (!string.IsNullOrEmpty(text))
					{
						Logger.LogErrorMessage("RelogManager: relog.exe exits with error. The blg file will remain in the folder. blg file: '{0}' Relog output: '{1}'", new object[]
						{
							blgFullPath,
							text
						});
					}
					if (!string.IsNullOrEmpty(text2))
					{
						Logger.LogErrorMessage("RelogManager: relog.exe crashes! The blg file will remain in the folder. blg file: '{0}' Relog output: '{1}'", new object[]
						{
							blgFullPath,
							text2
						});
					}
				}
			}
			else
			{
				Logger.LogErrorMessage("RelogManager: relog.exe process is not responding. The blg file will remain in the folder. blg file: '{0}'", new object[]
				{
					blgFullPath
				});
			}
			if (flag)
			{
				string text3 = fullPathWithoutExtension + ".csvtmp";
				try
				{
					File.Move(text3, fullPathWithoutExtension + ".csv");
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("RelogManager: Unable to rename .csvtmp to .csv. File: '{0}' Exception: '{1}'", new object[]
					{
						text3,
						ex
					});
				}
				this.fileToBeDeleted.Add(blgFullPath);
			}
			else
			{
				try
				{
					File.Move(blgFullPath, blgFullPath + ".error");
				}
				catch (Exception ex2)
				{
					Logger.LogErrorMessage("RelogManager: Unable to rename .blg to .error. Will delete it. File: '{0}' Exception: '{1}'", new object[]
					{
						blgFullPath,
						ex2
					});
					this.fileToBeDeleted.Add(blgFullPath);
				}
			}
			this.DeleteProcessedFiles();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006FE0 File Offset: 0x000051E0
		private void DeleteProcessedFiles()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in this.fileToBeDeleted)
			{
				try
				{
					File.Delete(text);
					Logger.LogInformationMessage("RelogManager: A blg file is deleted: '{0}'", new object[]
					{
						text
					});
				}
				catch (Exception ex)
				{
					hashSet.Add(text);
					Logger.LogErrorMessage("RelogManager: Unable to delete this file. Will try again later. File: '{0}' Exception: '{1}'", new object[]
					{
						text,
						ex
					});
				}
			}
			this.fileToBeDeleted = hashSet;
		}

		// Token: 0x0400005A RID: 90
		private const string WatchedFileType = "*.blg";

		// Token: 0x0400005B RID: 91
		private readonly FileSystemWatcher watcher;

		// Token: 0x0400005C RID: 92
		private readonly string watchedDirectory;

		// Token: 0x0400005D RID: 93
		private readonly TimeSpan minExecutionTime;

		// Token: 0x0400005E RID: 94
		private readonly object lockObject;

		// Token: 0x0400005F RID: 95
		private HashSet<string> fileToBeDeleted;

		// Token: 0x04000060 RID: 96
		private SampleAverage avgRelogExecuteTime;
	}
}
