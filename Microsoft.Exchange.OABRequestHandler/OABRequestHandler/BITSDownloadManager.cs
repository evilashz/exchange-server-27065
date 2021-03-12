using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.BITS;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.OAB;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.OABRequestHandler
{
	// Token: 0x02000002 RID: 2
	internal class BITSDownloadManager : IBackgroundCopyCallback
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected BITSDownloadManager()
		{
			this.poisonJobList = new TimeoutCache<Guid, BITSDownloadManager.JobInfo>(1, 1000, false);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		internal static BITSDownloadManager Instance
		{
			get
			{
				if (BITSDownloadManager.instance == null)
				{
					lock (BITSDownloadManager.concurrentAccessLock)
					{
						if (BITSDownloadManager.instance == null)
						{
							BITSDownloadManager bitsdownloadManager = new BITSDownloadManager();
							BITSDownloadManager.instance = bitsdownloadManager;
						}
					}
				}
				return BITSDownloadManager.instance;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002174 File Offset: 0x00000374
		void IBackgroundCopyCallback.JobError(IBackgroundCopyJob job, IBackgroundCopyError error)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobError: enter");
			Guid oabGuid = this.GetOABGuidFromJob(job);
			this.ExecuteWithErrorHandling(oabGuid, delegate
			{
				this.InternalJobError(job, error, oabGuid);
			});
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobError: exit");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000220C File Offset: 0x0000040C
		void IBackgroundCopyCallback.JobModification(IBackgroundCopyJob job, uint reserved)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobModification: enter");
			Guid oabguidFromJob = this.GetOABGuidFromJob(job);
			this.ExecuteWithErrorHandling(oabguidFromJob, delegate
			{
				this.InternalJobModification(job, reserved);
			});
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobModification: exit");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002298 File Offset: 0x00000498
		void IBackgroundCopyCallback.JobTransferred(IBackgroundCopyJob job)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobTransferred: enter");
			Guid oabGuid = this.GetOABGuidFromJob(job);
			this.ExecuteWithErrorHandling(oabGuid, delegate
			{
				this.InternalJobTransferred(job, oabGuid);
			});
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.JobTransferred: exit");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002308 File Offset: 0x00000508
		internal void InitializeIfNecessary()
		{
			lock (BITSDownloadManager.concurrentAccessLock)
			{
				if (!BITSDownloadManager.initialized)
				{
					this.InternalInitialize();
				}
				BITSDownloadManager.initialized = true;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000237C File Offset: 0x0000057C
		internal void StartOABDownloadFromRemoteServer(string physicalApplicationPath, string serverFqdn, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: enter");
			lock (BITSDownloadManager.concurrentAccessLock)
			{
				BITSDownloadManager.JobInfo value;
				if (this.poisonJobList.TryGetValue(oabGuid, out value))
				{
					BITSDownloadManager.Tracer.TraceDebug<Guid, int>(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: found JobInfo for {0} in poison job list; PoisonCount is {1}", oabGuid, value.PoisonCount);
					if (value.PoisonCount >= 3)
					{
						BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadJobIsPoison, oabGuid.ToString(), new object[]
						{
							oabGuid.ToString()
						});
						BITSDownloadManager.Tracer.TraceError<Guid>(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: job {0} is poison", oabGuid);
						BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: exit");
						return;
					}
				}
				else
				{
					BITSDownloadManager.Tracer.TraceDebug<Guid>(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: adding new JobInfo for {0} to poison job list", oabGuid);
					value = new BITSDownloadManager.JobInfo
					{
						PhysicalApplicationPath = physicalApplicationPath,
						ServerFQDN = serverFqdn,
						PoisonCount = 0
					};
					this.poisonJobList.InsertAbsolute(oabGuid, value, BITSDownloadManager.PoisonListEntryTTL, null);
				}
			}
			this.ExecuteWithErrorHandling(oabGuid, delegate
			{
				this.InternalStartOABDownloadFromRemoteServer(physicalApplicationPath, serverFqdn, oabGuid);
			});
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.StartOABDownloadFromRemoteServer: exit");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000252C File Offset: 0x0000072C
		protected virtual IBackgroundCopyManager GetBackgroundCopyManager()
		{
			return (IBackgroundCopyManager)new BackgroundCopyManager();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002538 File Offset: 0x00000738
		protected virtual bool SuppressAllWatsons()
		{
			return false;
		}

		// Token: 0x0600000A RID: 10
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern long FileTimeToSystemTime(ref _FILETIME fileTime, ref BITSDownloadManager.SYSTEMTIME systemTime);

		// Token: 0x0600000B RID: 11 RVA: 0x0000253C File Offset: 0x0000073C
		private static int GetRandomNumber()
		{
			int result;
			lock (BITSDownloadManager.RandomNumberGenerator)
			{
				result = BITSDownloadManager.RandomNumberGenerator.Next();
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002584 File Offset: 0x00000784
		private string GetJobName(string prefix, Guid oabGuid)
		{
			return string.Format("{0}{1}-{2}", prefix, oabGuid, BITSDownloadManager.GetRandomNumber());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025D8 File Offset: 0x000007D8
		private void ExecuteWithErrorHandling(Guid oabGuid, Action method)
		{
			BITSDownloadManager.Tracer.TraceFunction<Guid>(0L, "BITSDownloadManager.ExecuteWithErrorHandling(oabGuid = {0}): enter", oabGuid);
			if (oabGuid == Guid.Empty)
			{
				BITSDownloadManager.Tracer.TraceError(0L, "BITSDownloadManager.ExecuteWithErrorHandling: cannot continue because oabGuid is Guid.Empty");
				BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.ExecuteWithErrorHandling: exit");
				return;
			}
			Exception ex = null;
			try
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					method();
				}, (object e) => !this.SuppressAllWatsons() && !(e is ThreadAbortException) && !(e is OutOfMemoryException), ReportOptions.None);
			}
			catch (COMException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				ex = ex5;
			}
			catch (NotSupportedException ex6)
			{
				ex = ex6;
			}
			catch (FormatException ex7)
			{
				ex = ex7;
			}
			catch (OverflowException ex8)
			{
				ex = ex8;
			}
			catch (InvalidDataException ex9)
			{
				ex = ex9;
			}
			if (ex != null)
			{
				BITSDownloadManager.Tracer.TraceError<Exception>(0L, "BITSDownloadManager.ExecuteWithErrorHandling: method threw an exception: {0}", ex);
				this.ResubmitFailedJob(oabGuid, ex.ToString());
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.ExecuteWithErrorHandling: exit");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000294C File Offset: 0x00000B4C
		private IEnumerable<IBackgroundCopyJob> EnumerateJobs(Guid? oabGuid)
		{
			IBackgroundCopyManager backgroundCopyManager = this.GetBackgroundCopyManager();
			IEnumBackgroundCopyJobs enumBackgroundCopyJobs = null;
			backgroundCopyManager.EnumJobs(0U, out enumBackgroundCopyJobs);
			uint numberOfJobs = 0U;
			enumBackgroundCopyJobs.GetCount(out numberOfJobs);
			string oabGuidString = (oabGuid == null) ? string.Empty : oabGuid.ToString();
			string manifestJobNameStub = string.Format("{0}{1}", "OABRequestHandler-Manifest-", oabGuidString);
			string filesetJobNameStub = string.Format("{0}{1}", "OABRequestHandler-Fileset-", oabGuidString);
			for (uint i = 0U; i < numberOfJobs; i += 1U)
			{
				IBackgroundCopyJob job = null;
				uint jobsReturned = 0U;
				enumBackgroundCopyJobs.Next(1U, out job, out jobsReturned);
				string jobName = null;
				job.GetDisplayName(out jobName);
				if (jobName.StartsWith(manifestJobNameStub, StringComparison.OrdinalIgnoreCase) || jobName.StartsWith(filesetJobNameStub, StringComparison.OrdinalIgnoreCase))
				{
					yield return job;
				}
			}
			yield break;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002970 File Offset: 0x00000B70
		private bool FindAndMaintainExistingJobs(Guid oabGuid, out string existingJobName)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.FindAndMaintainExistingJobs: enter");
			bool result = false;
			existingJobName = null;
			foreach (IBackgroundCopyJob backgroundCopyJob in this.EnumerateJobs(new Guid?(oabGuid)))
			{
				BG_JOB_STATE bg_JOB_STATE;
				backgroundCopyJob.GetState(out bg_JOB_STATE);
				string text = null;
				backgroundCopyJob.GetDisplayName(out text);
				if (bg_JOB_STATE != BG_JOB_STATE.BG_JOB_STATE_ACKNOWLEDGED && bg_JOB_STATE != BG_JOB_STATE.BG_JOB_STATE_CANCELLED)
				{
					if (bg_JOB_STATE == BG_JOB_STATE.BG_JOB_STATE_ERROR)
					{
						BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.FindAndMaintainExistingJobs: cancelling error BITS job: {0}", text);
						backgroundCopyJob.Cancel();
					}
					else
					{
						_BG_JOB_TIMES bg_JOB_TIMES;
						backgroundCopyJob.GetTimes(out bg_JOB_TIMES);
						BITSDownloadManager.SYSTEMTIME systemtime = default(BITSDownloadManager.SYSTEMTIME);
						BITSDownloadManager.FileTimeToSystemTime(ref bg_JOB_TIMES.CreationTime, ref systemtime);
						DateTime dateTime = new DateTime((int)systemtime.Year, (int)systemtime.Month, (int)systemtime.Day, (int)systemtime.Hour, (int)systemtime.Minute, (int)systemtime.Second, (int)systemtime.Milliseconds, DateTimeKind.Utc);
						if (DateTime.UtcNow.Subtract(dateTime.ToUniversalTime()) >= BITSDownloadManager.JobTimeout)
						{
							BITSDownloadManager.Tracer.TraceDebug<string, string>(0L, "BITSDownloadManager.FindAndMaintainExistingJobs: cancelling timed out (creation date UTC:{0}) BITS job: {1}", dateTime.ToUniversalTime().ToString(), text);
							backgroundCopyJob.Cancel();
						}
						else
						{
							BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.FindAndMaintainExistingJobs: found in progress BITS job: {0}", text);
							existingJobName = text;
							result = true;
						}
					}
				}
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.FindAndMaintainExistingJobs: exit");
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002AFC File Offset: 0x00000CFC
		private void ResubmitFailedJob(Guid oabGuid, string errorDescription)
		{
			BITSDownloadManager.Tracer.TraceFunction<Guid>(0L, "BITSDownloadManager.ResubmitFailedJob(oabGuid = {0}): enter", oabGuid);
			BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.ResubmitFailedJob: resubmitting failed job because of error: {0}", errorDescription);
			try
			{
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadFailedResubmitting, oabGuid.ToString(), new object[]
				{
					oabGuid.ToString(),
					errorDescription
				});
				BITSDownloadManager.JobInfo value = new BITSDownloadManager.JobInfo
				{
					PoisonCount = 3
				};
				lock (BITSDownloadManager.concurrentAccessLock)
				{
					foreach (IBackgroundCopyJob backgroundCopyJob in this.EnumerateJobs(new Guid?(oabGuid)))
					{
						string arg = null;
						backgroundCopyJob.GetDisplayName(out arg);
						BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.ResubmitFailedJob: cancelling existing BITS job: {0}", arg);
						backgroundCopyJob.Cancel();
					}
					if (this.poisonJobList.Contains(oabGuid))
					{
						value = this.poisonJobList.Get(oabGuid);
					}
					value.PoisonCount++;
					this.poisonJobList.InsertAbsolute(oabGuid, value, BITSDownloadManager.PoisonListEntryTTL, null);
				}
				this.StartOABDownloadFromRemoteServer(value.PhysicalApplicationPath, value.ServerFQDN, oabGuid);
			}
			catch (COMException ex)
			{
				BITSDownloadManager.Tracer.TraceDebug<COMException>(0L, "BITSDownloadManager.ResubmitFailedJob: caught exception {0}", ex);
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadOtherError, oabGuid.ToString(), new object[]
				{
					ex.ToString()
				});
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.ResubmitFailedJob: exit");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002CEC File Offset: 0x00000EEC
		private IBackgroundCopyJob CreateBackgroundCopyJob(string jobName)
		{
			BITSDownloadManager.Tracer.TraceFunction<string>(0L, "BITSDownloadManager.CreateBackgroundCopyJob(jobName = {0}): enter", jobName);
			IBackgroundCopyManager backgroundCopyManager = this.GetBackgroundCopyManager();
			Guid empty = Guid.Empty;
			IBackgroundCopyJob backgroundCopyJob = null;
			backgroundCopyManager.CreateJob(jobName, BG_JOB_TYPE.BG_JOB_TYPE_DOWNLOAD, out empty, out backgroundCopyJob);
			BG_AUTH_CREDENTIALS bg_AUTH_CREDENTIALS = default(BG_AUTH_CREDENTIALS);
			bg_AUTH_CREDENTIALS.Target = BG_AUTH_TARGET.BG_AUTH_TARGET_SERVER;
			bg_AUTH_CREDENTIALS.Scheme = BG_AUTH_SCHEME.BG_AUTH_SCHEME_NTLM;
			bg_AUTH_CREDENTIALS.Credentials.Basic.UserName = null;
			bg_AUTH_CREDENTIALS.Credentials.Basic.Password = null;
			IBackgroundCopyJob2 backgroundCopyJob2 = backgroundCopyJob as IBackgroundCopyJob2;
			backgroundCopyJob2.SetCredentials(ref bg_AUTH_CREDENTIALS);
			IBackgroundCopyJobHttpOptions backgroundCopyJobHttpOptions = backgroundCopyJob as IBackgroundCopyJobHttpOptions;
			backgroundCopyJobHttpOptions.SetSecurityFlags(10UL);
			backgroundCopyJob.SetNotifyFlags(11U);
			backgroundCopyJob.SetNotifyInterface(this);
			backgroundCopyJob.SetNoProgressTimeout((uint)BITSDownloadManager.NoProgressTimeout.TotalSeconds);
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.CreateBackgroundCopyJob: exit");
			return backgroundCopyJob;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002DBC File Offset: 0x00000FBC
		private Guid GetOABGuidFromJob(IBackgroundCopyJob job)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.GetOABGuidFromJob: enter");
			Guid guid = Guid.Empty;
			string text = null;
			try
			{
				job.GetDisplayName(out text);
				if (text.StartsWith("OABRequestHandler-Manifest-", StringComparison.OrdinalIgnoreCase))
				{
					guid = new Guid(text.Substring("OABRequestHandler-Manifest-".Length, 36));
				}
				else if (text.StartsWith("OABRequestHandler-Fileset-", StringComparison.OrdinalIgnoreCase))
				{
					guid = new Guid(text.Substring("OABRequestHandler-Fileset-".Length, 36));
				}
			}
			catch (COMException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			if (guid == Guid.Empty)
			{
				BITSDownloadManager.Tracer.TraceError<string>(0L, "BITSDownloadManager.GetOABGuidFromJob: could not parse guid from string {0}", text ?? string.Empty);
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadGuidParsingError, guid.ToString(), new object[]
				{
					text ?? string.Empty
				});
			}
			BITSDownloadManager.Tracer.TraceFunction<Guid>(0L, "BITSDownloadManager.GetOABGuidFromJob: exit (guid = {0})", guid);
			return guid;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002EE8 File Offset: 0x000010E8
		private void InternalInitialize()
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalInitialize: enter");
			try
			{
				foreach (IBackgroundCopyJob backgroundCopyJob in this.EnumerateJobs(null))
				{
					BG_JOB_STATE bg_JOB_STATE;
					backgroundCopyJob.GetState(out bg_JOB_STATE);
					string arg = null;
					backgroundCopyJob.GetDisplayName(out arg);
					BITSDownloadManager.Tracer.TraceDebug<string, BG_JOB_STATE>(0L, "BITSDownloadManager.InternalInitialize: job {0} in in state state {0}", arg, bg_JOB_STATE);
					if (bg_JOB_STATE != BG_JOB_STATE.BG_JOB_STATE_ACKNOWLEDGED && bg_JOB_STATE != BG_JOB_STATE.BG_JOB_STATE_CANCELLED)
					{
						backgroundCopyJob.SetNotifyFlags(11U);
						backgroundCopyJob.SetNotifyInterface(this);
					}
					if (bg_JOB_STATE == BG_JOB_STATE.BG_JOB_STATE_SUSPENDED)
					{
						BITSDownloadManager.Tracer.TraceDebug(0L, "BITSDownloadManager.InternalInitialize: calling Resume() on suspended job");
						this.TryToResumeJob(backgroundCopyJob);
					}
				}
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_BITSDownloadManagerInitialized, "Initialization", new object[0]);
			}
			catch (COMException ex)
			{
				BITSDownloadManager.Tracer.TraceError<COMException>(0L, "BITSDownloadManager.InternalInitialize: caught an exception: {0}", ex);
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_BITSDownloadManagerInitializationFailed, "Initialization", new object[]
				{
					ex.ToString()
				});
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalInitialize: exit");
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003020 File Offset: 0x00001220
		private void InternalStartOABDownloadFromRemoteServer(string physicalApplicationPath, string serverFqdn, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction<string, string, Guid>(0L, "BITSDownloadManager.InternalStartOABDownloadFromRemoteServer(physicalApplicationPath = {0}, serverFqdn = {1}, oabGuid = {2}): enter", physicalApplicationPath, serverFqdn, oabGuid);
			IBackgroundCopyJob backgroundCopyJob = null;
			string text = string.Empty;
			lock (BITSDownloadManager.concurrentAccessLock)
			{
				if (this.FindAndMaintainExistingJobs(oabGuid, out text))
				{
					BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.InternalStartOABDownloadFromRemoteServer: there is already an existing job for this OAB: {0}.", text);
					BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalStartOABDownloadFromRemoteServer: exit");
					return;
				}
				text = this.GetJobName("OABRequestHandler-Manifest-", oabGuid);
				backgroundCopyJob = this.CreateBackgroundCopyJob(text);
			}
			string remoteUrl = string.Format("https://{0}:444/OAB/{1}/oab.xml", serverFqdn, oabGuid);
			string text2 = Path.Combine(Path.Combine(physicalApplicationPath, "Temp"), oabGuid.ToString());
			Directory.CreateDirectory(text2);
			string localName = Path.Combine(text2, "oab.xml");
			backgroundCopyJob.AddFile(remoteUrl, localName);
			BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadStarted, oabGuid.ToString(), new object[]
			{
				oabGuid.ToString(),
				serverFqdn
			});
			BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.InternalStartOABDownloadFromRemoteServer: starting new manifest download job {0}", text);
			this.TryToResumeJob(backgroundCopyJob);
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalStartOABDownloadFromRemoteServer: exit");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003178 File Offset: 0x00001378
		private void InternalJobError(IBackgroundCopyJob job, IBackgroundCopyError error, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobError: enter");
			string arg = null;
			job.GetDisplayName(out arg);
			BG_ERROR_CONTEXT bg_ERROR_CONTEXT = BG_ERROR_CONTEXT.BG_ERROR_CONTEXT_NONE;
			int num = 0;
			if (error != null)
			{
				error.GetError(out bg_ERROR_CONTEXT, out num);
				BITSDownloadManager.Tracer.TraceError<string, BG_ERROR_CONTEXT, int>(0L, "BITSDownloadManager.InternalJobError: jobName = {0}, error context = {1}, error code = 0x{2:x8}", arg, bg_ERROR_CONTEXT, num);
			}
			else
			{
				BITSDownloadManager.Tracer.TraceError<string>(0L, "BITSDownloadManager.InternalJobError: jobName = {0}, no error information available", arg);
			}
			this.ResubmitFailedJob(oabGuid, string.Format("BG_ERROR_CONTEXT = {0}; error code = 0x{1:x8}", bg_ERROR_CONTEXT, num));
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobError: exit");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003208 File Offset: 0x00001408
		private void InternalJobModification(IBackgroundCopyJob job, uint reserved)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobModification: enter");
			string arg = null;
			job.GetDisplayName(out arg);
			BG_JOB_STATE bg_JOB_STATE;
			job.GetState(out bg_JOB_STATE);
			BITSDownloadManager.Tracer.TraceDebug<string, BG_JOB_STATE>(0L, "BITSDownloadManager.InternalJobModification: job {0} is in state {1}", arg, bg_JOB_STATE);
			if (bg_JOB_STATE == BG_JOB_STATE.BG_JOB_STATE_SUSPENDED)
			{
				BITSDownloadManager.Tracer.TraceDebug(0L, "BITSDownloadManager.InternalJobModification: calling Resume() on suspended job");
				this.TryToResumeJob(job);
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobModification: exit");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003278 File Offset: 0x00001478
		private void TryToResumeJob(IBackgroundCopyJob job)
		{
			try
			{
				IEnumBackgroundCopyFiles enumBackgroundCopyFiles;
				job.EnumFiles(out enumBackgroundCopyFiles);
				if (enumBackgroundCopyFiles != null)
				{
					uint num;
					enumBackgroundCopyFiles.GetCount(out num);
					if (num > 0U)
					{
						job.Resume();
					}
				}
			}
			catch (COMException ex)
			{
				BITSDownloadManager.Tracer.TraceError<COMException>(0L, "BITSDownloadManager.TryToResumeJob: caught an exception: {0}", ex);
				BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadOtherError, "TryToResumeJob", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000032F0 File Offset: 0x000014F0
		private void InternalJobTransferred(IBackgroundCopyJob job, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobTransferred: enter");
			string text = null;
			job.GetDisplayName(out text);
			if (text.StartsWith("OABRequestHandler-Manifest-", StringComparison.OrdinalIgnoreCase))
			{
				this.OABManifestTransferred(job, oabGuid);
			}
			else if (text.StartsWith("OABRequestHandler-Fileset-", StringComparison.OrdinalIgnoreCase))
			{
				this.OABFilesetTransferred(job, oabGuid);
			}
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.InternalJobTransferred: exit");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003358 File Offset: 0x00001558
		private void OABManifestTransferred(IBackgroundCopyJob job, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABManifestTransferred: enter");
			string arg = null;
			job.GetDisplayName(out arg);
			IBackgroundCopyJob backgroundCopyJob = null;
			List<BG_FILE_INFO> list = new List<BG_FILE_INFO>(500);
			string jobName = this.GetJobName("OABRequestHandler-Fileset-", oabGuid);
			lock (BITSDownloadManager.concurrentAccessLock)
			{
				job.Complete();
				BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABManifestTransferred: job {0} has been marked completed", arg);
				IEnumBackgroundCopyFiles enumBackgroundCopyFiles;
				job.EnumFiles(out enumBackgroundCopyFiles);
				uint num;
				enumBackgroundCopyFiles.GetCount(out num);
				if (num != 1U)
				{
					BITSDownloadManager.Tracer.TraceError<uint>(0L, "BITSDownloadManager.OABManifestTransferred: expected exactly one file in the job, but there were {0}", num);
					throw new InvalidDataException(string.Format("Expected exactly one file in the job {0}, but there were {1}", arg, num));
				}
				IBackgroundCopyFile backgroundCopyFile;
				uint num2;
				enumBackgroundCopyFiles.Next(1U, out backgroundCopyFile, out num2);
				string text;
				backgroundCopyFile.GetLocalName(out text);
				string text2;
				backgroundCopyFile.GetRemoteName(out text2);
				if (!text.EndsWith("oab.xml", StringComparison.OrdinalIgnoreCase) || !text2.EndsWith("oab.xml", StringComparison.OrdinalIgnoreCase))
				{
					BITSDownloadManager.Tracer.TraceError<string, string>(0L, "BITSDownloadManager.OABManifestTransferred: expected both the local and remote file names to end in oab.xml; instead they are {0} and {1}", text, text2);
					throw new InvalidDataException(string.Format("Expected both the local and remote file names to end in oab.xml; instead they are {0} and {1}", text, text2));
				}
				string directoryName = Path.GetDirectoryName(text);
				string arg2 = text2.Substring(0, text2.Length - "oab.xml".Length);
				OABManifest oabmanifest = OABManifest.LoadFromFile(text);
				foreach (OABManifestAddressList oabmanifestAddressList in oabmanifest.AddressLists)
				{
					foreach (OABManifestFile oabmanifestFile in oabmanifestAddressList.Files)
					{
						if (!OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled || (oabmanifestFile.Type != OABDataFileType.TemplateMac && oabmanifestFile.Type != OABDataFileType.TemplateWin))
						{
							BG_FILE_INFO item = default(BG_FILE_INFO);
							item.RemoteName = string.Format("{0}{1}", arg2, oabmanifestFile.FileName);
							item.LocalName = Path.Combine(directoryName, oabmanifestFile.FileName);
							BITSDownloadManager.Tracer.TraceDebug<string, string>(0L, "BITSDownloadManager.OABManifestTransferred: adding file {0} -> {1} to file set", item.RemoteName, item.LocalName);
							list.Add(item);
						}
					}
				}
				if (list.Count == 0)
				{
					BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABManifestTransferred: no oab files found in manifest file: {0}", text);
					return;
				}
				backgroundCopyJob = this.CreateBackgroundCopyJob(jobName);
			}
			backgroundCopyJob.AddFileSet((uint)list.Count, list.ToArray());
			BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABManifestTransferred: starting new job {0}", jobName);
			this.TryToResumeJob(backgroundCopyJob);
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABManifestTransferred: exit");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000036B8 File Offset: 0x000018B8
		private void OABFilesetTransferred(IBackgroundCopyJob job, Guid oabGuid)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABFilesetTransferred: enter");
			string arg = null;
			job.GetDisplayName(out arg);
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = BITSDownloadManager.concurrentAccessLock, ref flag);
				job.Complete();
				BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABFilesetTransferred: job {0} has been marked completed", arg);
				IEnumBackgroundCopyFiles enumBackgroundCopyFiles;
				job.EnumFiles(out enumBackgroundCopyFiles);
				uint num;
				enumBackgroundCopyFiles.GetCount(out num);
				if (num == 0U)
				{
					BITSDownloadManager.Tracer.TraceError<uint>(0L, "BITSDownloadManager.OABFilesetTransferred: expected at least one file in the job, but there were {0}", num);
					throw new InvalidDataException(string.Format("Expected at least one file in the job {0}, but there were {1}", arg, num));
				}
				IBackgroundCopyFile backgroundCopyFile;
				uint num2;
				enumBackgroundCopyFiles.Next(1U, out backgroundCopyFile, out num2);
				string path;
				backgroundCopyFile.GetLocalName(out path);
				string tempFolder = Path.GetDirectoryName(path);
				string destinationFolder = tempFolder.Replace(string.Format("{0}{1}", "Temp", Path.DirectorySeparatorChar), string.Empty);
				BITSDownloadManager.Tracer.TraceDebug<string, string>(0L, "BITSDownloadManager.OABFilesetTransferred: tempFolder = {0}, destinationFolder = {1}", tempFolder, destinationFolder);
				if (!this.ValidateDownloadedManifestAndFiles(tempFolder, destinationFolder))
				{
					BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABFilesetTransferred: downloaded oab files are either old or invalid in {0}.", tempFolder);
					throw new InvalidDataException(string.Format("Downloaded oab files are either old or invalid in {0}", tempFolder));
				}
				BITSDownloadManager.Tracer.TraceDebug(0L, "BITSDownloadManager.OABFilesetTransferred: deleting files in destination directory");
				this.SafeCallIOMethod(true, delegate
				{
					Directory.CreateDirectory(destinationFolder);
					foreach (string path2 in Directory.EnumerateFiles(destinationFolder))
					{
						File.Delete(path2);
					}
				});
				BITSDownloadManager.Tracer.TraceDebug(0L, "BITSDownloadManager.OABFilesetTransferred: moving files");
				string[] newFiles = null;
				this.SafeCallIOMethod(false, delegate
				{
					newFiles = Directory.GetFiles(tempFolder);
				});
				if (newFiles != null && newFiles.Length > 0)
				{
					string[] newFiles2 = newFiles;
					for (int i = 0; i < newFiles2.Length; i++)
					{
						string filename = newFiles2[i];
						BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.OABFilesetTransferred: moving file {0}", filename);
						this.SafeCallIOMethod(false, delegate
						{
							string destFileName = Path.Combine(destinationFolder, Path.GetFileName(filename));
							File.Move(filename, destFileName);
						});
					}
				}
				BITSDownloadManager.Tracer.TraceDebug(0L, "BITSDownloadManager.OABFilesetTransferred: deleting temp folder");
				this.SafeCallIOMethod(true, delegate
				{
					Directory.Delete(tempFolder, true);
				});
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
			BITSDownloadManager.EventLogger.LogEvent(ServicesEventLogConstants.Tuple_OABDownloadFinished, oabGuid.ToString(), new object[]
			{
				oabGuid.ToString()
			});
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABFilesetTransferred: exit");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003974 File Offset: 0x00001B74
		private bool VerifyManifestVersion(string downloadedManifest, string diskManifest)
		{
			OfflineAddressBookManifestVersion oabmanifestVersion = this.GetOABManifestVersion(downloadedManifest);
			OfflineAddressBookManifestVersion oabmanifestVersion2 = this.GetOABManifestVersion(diskManifest);
			bool result;
			if (oabmanifestVersion2 != null && oabmanifestVersion2.HasValue && oabmanifestVersion != null && oabmanifestVersion.HasValue)
			{
				bool flag = false;
				if (oabmanifestVersion.AddressLists.Length == oabmanifestVersion2.AddressLists.Length)
				{
					foreach (AddressListSequence addressListSequence in oabmanifestVersion.AddressLists)
					{
						foreach (AddressListSequence addressListSequence2 in oabmanifestVersion2.AddressLists)
						{
							if (string.Compare(addressListSequence.AddressListId, addressListSequence2.AddressListId, StringComparison.OrdinalIgnoreCase) == 0 && addressListSequence.Sequence < addressListSequence2.Sequence)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.VerifyManifestVersion: found older address lists in the downloaded manifest {0}.", downloadedManifest);
							break;
						}
					}
				}
				else
				{
					BITSDownloadManager.Tracer.TraceDebug<string, int, int>(0L, "BITSDownloadManager.VerifyManifestVersion: found different number of address lists in the downloaded manifest {0}, assuming the downloaded is newer. Downloaded addressList count={1}, on disk address list count={2}", downloadedManifest, oabmanifestVersion.AddressLists.Length, oabmanifestVersion2.AddressLists.Length);
				}
				result = !flag;
			}
			else
			{
				BITSDownloadManager.Tracer.TraceDebug<string, string>(0L, "BITSDownloadManager.VerifyManifestVersion: we cannot compare version numbers between the downloaded and the on disk manifest files, assuming the downloaded is newer. Downloaded manifest={0}, on disk manifest={1}", downloadedManifest, diskManifest);
				result = true;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003A94 File Offset: 0x00001C94
		private OfflineAddressBookManifestVersion GetOABManifestVersion(string manifestPath)
		{
			OABManifest oabmanifest = OABManifest.LoadFromFile(manifestPath);
			if (oabmanifest != null)
			{
				return oabmanifest.GetVersion();
			}
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003B9C File Offset: 0x00001D9C
		private bool ValidateDownloadedManifestAndFiles(string tempFolder, string destinationFolder)
		{
			bool result = false;
			string text = Path.Combine(tempFolder, "oab.xml");
			string diskManifest = Path.Combine(destinationFolder, "oab.xml");
			if (this.VerifyManifestVersion(text, diskManifest))
			{
				new List<BG_FILE_INFO>(500);
				OABManifest oabmanifest = OABManifest.LoadFromFile(text);
				if (oabmanifest != null)
				{
					bool flag = false;
					bool sharedTemplateFilesEnabled = OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled;
					foreach (OABManifestAddressList oabmanifestAddressList in oabmanifest.AddressLists)
					{
						flag = oabmanifestAddressList.Files.AsParallel<OABManifestFile>().Any(delegate(OABManifestFile file)
						{
							if (sharedTemplateFilesEnabled && (file.Type == OABDataFileType.TemplateMac || file.Type == OABDataFileType.TemplateWin))
							{
								return false;
							}
							try
							{
								FileInfo fileInfo = new FileInfo(Path.Combine(tempFolder, file.FileName));
								if (!fileInfo.Exists || fileInfo.Length != (long)((ulong)file.CompressedSize))
								{
									return true;
								}
								using (FileStream fileStream = File.OpenRead(fileInfo.FullName))
								{
									if (!OABFileHash.GetHash(fileStream).Equals(file.Hash, StringComparison.OrdinalIgnoreCase))
									{
										return true;
									}
								}
							}
							catch (Exception ex)
							{
								BITSDownloadManager.Tracer.TraceError<string, Type, string>(0L, "BITSDownloadManager.ValidateManifestAndFiles: failed to validate file {0} due to {1}:{2}", file.FileName, ex.GetType(), ex.Message);
							}
							return false;
						});
						if (flag)
						{
							BITSDownloadManager.Tracer.TraceDebug<string>(0L, "BITSDownloadManager.ValidateManifestAndFiles: found downloaded oab file that doesn't match what's in the manifest {0}.", text);
							break;
						}
					}
					result = !flag;
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003C88 File Offset: 0x00001E88
		private void SafeCallIOMethod(bool ignoreErrors, Action ioMethod)
		{
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: enter");
			int num = 0;
			bool flag = false;
			do
			{
				try
				{
					BITSDownloadManager.Tracer.TraceDebug<int>(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: retry {0}", num);
					if (num > 0)
					{
						Thread.Sleep(500);
					}
					num++;
					ioMethod();
					flag = true;
				}
				catch (IOException arg)
				{
					BITSDownloadManager.Tracer.TraceError<IOException>(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: caught exception: {0}", arg);
					if (num >= 3 && !ignoreErrors)
					{
						throw;
					}
				}
				catch (ArgumentException arg2)
				{
					BITSDownloadManager.Tracer.TraceError<ArgumentException>(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: caught exception: {0}", arg2);
					if (num >= 3 && !ignoreErrors)
					{
						throw;
					}
				}
				catch (UnauthorizedAccessException arg3)
				{
					BITSDownloadManager.Tracer.TraceError<UnauthorizedAccessException>(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: caught exception: {0}", arg3);
					if (num >= 3 && !ignoreErrors)
					{
						throw;
					}
				}
				catch (NotSupportedException arg4)
				{
					BITSDownloadManager.Tracer.TraceError<NotSupportedException>(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: caught exception: {0}", arg4);
					if (num >= 3 && !ignoreErrors)
					{
						throw;
					}
				}
			}
			while (!flag && num < 3);
			BITSDownloadManager.Tracer.TraceFunction(0L, "BITSDownloadManager.OABFilesetCopyCallback.SafeCallIOMethod: exit");
		}

		// Token: 0x04000001 RID: 1
		private const string ManifestJobNamePrefix = "OABRequestHandler-Manifest-";

		// Token: 0x04000002 RID: 2
		private const string FilesetJobNamePrefix = "OABRequestHandler-Fileset-";

		// Token: 0x04000003 RID: 3
		private const string BackendOABManifestURLFormat = "https://{0}:444/OAB/{1}/oab.xml";

		// Token: 0x04000004 RID: 4
		private const string TempFolderName = "Temp";

		// Token: 0x04000005 RID: 5
		private const string OABManifestFileName = "oab.xml";

		// Token: 0x04000006 RID: 6
		private const string BITSErrorFormatString = "BG_ERROR_CONTEXT = {0}; error code = 0x{1:x8}";

		// Token: 0x04000007 RID: 7
		private const int MaxPoisonCount = 3;

		// Token: 0x04000008 RID: 8
		protected static readonly Random RandomNumberGenerator = new Random();

		// Token: 0x04000009 RID: 9
		private static readonly Trace Tracer = ExTraceGlobals.HttpHandlerTracer;

		// Token: 0x0400000A RID: 10
		private static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.HttpHandlerTracer.Category, "MSExchange OABRequestHandler");

		// Token: 0x0400000B RID: 11
		private static readonly TimeSpan PoisonListEntryTTL = TimeSpan.FromMinutes(60.0);

		// Token: 0x0400000C RID: 12
		private static readonly TimeSpan NoProgressTimeout = TimeSpan.FromHours(8.0);

		// Token: 0x0400000D RID: 13
		private static readonly TimeSpan JobTimeout = TimeSpan.FromHours(24.0);

		// Token: 0x0400000E RID: 14
		private static object concurrentAccessLock = new object();

		// Token: 0x0400000F RID: 15
		private static volatile BITSDownloadManager instance;

		// Token: 0x04000010 RID: 16
		private static bool initialized = false;

		// Token: 0x04000011 RID: 17
		private TimeoutCache<Guid, BITSDownloadManager.JobInfo> poisonJobList;

		// Token: 0x02000003 RID: 3
		private struct JobInfo
		{
			// Token: 0x04000012 RID: 18
			public string PhysicalApplicationPath;

			// Token: 0x04000013 RID: 19
			public string ServerFQDN;

			// Token: 0x04000014 RID: 20
			public int PoisonCount;
		}

		// Token: 0x02000004 RID: 4
		private struct SYSTEMTIME
		{
			// Token: 0x04000015 RID: 21
			internal short Year;

			// Token: 0x04000016 RID: 22
			internal short Month;

			// Token: 0x04000017 RID: 23
			internal short DayOfWeek;

			// Token: 0x04000018 RID: 24
			internal short Day;

			// Token: 0x04000019 RID: 25
			internal short Hour;

			// Token: 0x0400001A RID: 26
			internal short Minute;

			// Token: 0x0400001B RID: 27
			internal short Second;

			// Token: 0x0400001C RID: 28
			internal short Milliseconds;
		}
	}
}
