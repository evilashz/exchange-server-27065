using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200000C RID: 12
	internal class JobManager : IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003EC0 File Offset: 0x000020C0
		internal JobManager(JobConfigurationCollection configuredJobs, Dictionary<string, OutputStream> outputStreams, string watermarkDirectory)
		{
			this.syncLock = new object();
			this.configuredJobs = configuredJobs;
			this.outputStreams = outputStreams;
			this.jobs = new List<KeyValuePair<Thread, WatermarkedJob>>();
			this.filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			this.watermarks = new Watermarks(watermarkDirectory);
			this.datacenterMode = DiagnosticsConfiguration.GetIsDatacenterMode();
			this.poisonManager = PoisonManager.Instance;
			this.poisonManager.Initialize();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003F3C File Offset: 0x0000213C
		public void Dispose()
		{
			foreach (KeyValuePair<Thread, WatermarkedJob> keyValuePair in this.jobs)
			{
				keyValuePair.Value.Dispose();
			}
			this.jobs.Clear();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003FA0 File Offset: 0x000021A0
		internal static void JobRunner(object state)
		{
			WatermarkedJob watermarkedJob = (WatermarkedJob)state;
			try
			{
				watermarkedJob.WaitHandle = watermarkedJob.LogSource.WaitHandle;
				watermarkedJob.Run();
			}
			catch (ThreadAbortException)
			{
				Log.LogInformationMessage("Job '{0}': Abort exception caught.", new object[]
				{
					watermarkedJob.Name
				});
				Thread.ResetAbort();
			}
			catch (Exception e)
			{
				string message = "The current watermark is: " + watermarkedJob.Watermark.Timestamp.ToString("MMddHHmmss");
				PoisonManager.Instance.Crash(watermarkedJob.Name, message, e);
				throw;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004048 File Offset: 0x00002248
		internal void StopJobs()
		{
			lock (this.syncLock)
			{
				foreach (KeyValuePair<Thread, WatermarkedJob> keyValuePair in this.jobs)
				{
					keyValuePair.Key.Abort();
					keyValuePair.Key.Join();
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000040D8 File Offset: 0x000022D8
		internal void CreateAndStartJobsAsync()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.CreateAndStartJobs));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000040EC File Offset: 0x000022EC
		internal void CreateAndStartJobs(object stateInfo)
		{
			lock (this.syncLock)
			{
				this.CreateAndStartJobs();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000412C File Offset: 0x0000232C
		internal void CreateAndStartJobs()
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			foreach (object obj in this.configuredJobs)
			{
				JobConfigurationElement jobConfigurationElement = (JobConfigurationElement)obj;
				Logger.LogInformationMessage("JobManager: Job '{0}' is being loaded", new object[]
				{
					jobConfigurationElement.Name
				});
				if (string.IsNullOrEmpty(jobConfigurationElement.Name))
				{
					Logger.LogErrorMessage("JobManager: The job's name is null or empty, skipping. Assembly: '{0}', Class: '{1}'", new object[]
					{
						jobConfigurationElement.Assembly,
						jobConfigurationElement.Class
					});
					stringBuilder3.AppendLine();
					stringBuilder3.AppendFormat("Job was added with a null or empty name, skipping. Assembly: '{0}', Class: '{1}'", jobConfigurationElement.Assembly, jobConfigurationElement.Class);
				}
				else if (list.Contains(jobConfigurationElement.Name, StringComparer.OrdinalIgnoreCase))
				{
					Logger.LogInformationMessage("JobManager: Job '{0}' has a duplicate name, it will be skipped.", new object[]
					{
						jobConfigurationElement.Name
					});
					stringBuilder3.AppendLine();
					stringBuilder3.AppendFormat("Job: '{0}' has a duplicate name, it will be skipped.", jobConfigurationElement.Name);
				}
				else
				{
					list.Add(jobConfigurationElement.Name);
					if (!this.IsRole(jobConfigurationElement.Role))
					{
						stringBuilder2.AppendLine();
						stringBuilder2.AppendFormat("Job: '{0}' is not being added because the server is not the configured role.", jobConfigurationElement.Name);
					}
					else if (!jobConfigurationElement.Enabled)
					{
						Logger.LogInformationMessage("JobManager: Job '{0}' is not being added because it is not enabled.", new object[]
						{
							jobConfigurationElement.Name
						});
						stringBuilder2.AppendLine();
						stringBuilder2.AppendFormat("Job: '{0}' is not being added because it is not enabled.", jobConfigurationElement.Name);
					}
					else if (!this.datacenterMode && jobConfigurationElement.Datacenter)
					{
						Logger.LogInformationMessage("JobManager: Job '{0}' is not being added because it is datacenter only.", new object[]
						{
							jobConfigurationElement.Name
						});
					}
					else if (this.poisonManager.IsPoisoned(jobConfigurationElement.Name))
					{
						Logger.LogErrorMessage("JobManager: Job: '{0}' is poisoned.", new object[]
						{
							jobConfigurationElement.Name
						});
						stringBuilder3.AppendLine();
						stringBuilder3.AppendFormat("Job: '{0}' is poisoned.", jobConfigurationElement.Name);
					}
					else
					{
						Logger.LogInformationMessage("JobManager: Job '{0}' is enabled, the server a supported role and the job is not poisoned. Adding the job.", new object[]
						{
							jobConfigurationElement.Name
						});
						WatermarkedJob watermarkedJob;
						bool flag = this.CreateJob(jobConfigurationElement, out watermarkedJob);
						if (watermarkedJob == null)
						{
							if (!flag)
							{
								Logger.LogErrorMessage("JobManager: Job: '{0}' creation failed.", new object[]
								{
									jobConfigurationElement.Name
								});
								stringBuilder3.AppendLine();
								stringBuilder3.AppendFormat("Job: '{0}' creation failed.", jobConfigurationElement.Name);
							}
							else
							{
								Logger.LogInformationMessage("JobManager: Job: '{0}' is not being added because it is self-disabled.", new object[]
								{
									jobConfigurationElement.Name
								});
								stringBuilder2.AppendLine();
								stringBuilder2.AppendFormat("Job: '{0}' is not being added because it is self-disabled.", jobConfigurationElement.Name);
							}
						}
						else
						{
							StringBuilder stringBuilder5 = new StringBuilder();
							stringBuilder5.AppendLine();
							stringBuilder5.AppendFormat("Job: '{0}' will be started and have the following analyzers enabled: ", jobConfigurationElement.Name);
							stringBuilder5.AppendLine();
							List<LogAnalyzer> list2 = new List<LogAnalyzer>(jobConfigurationElement.Analyzers.Count);
							foreach (object obj2 in jobConfigurationElement.Analyzers)
							{
								AnalyzerConfigurationElement analyzerConfigurationElement = (AnalyzerConfigurationElement)obj2;
								if (analyzerConfigurationElement.Enabled && this.IsRole(analyzerConfigurationElement.Role) && (this.datacenterMode || this.datacenterMode || !analyzerConfigurationElement.Datacenter))
								{
									LogAnalyzer logAnalyzer = this.CreateAnalyzer(analyzerConfigurationElement, watermarkedJob);
									if (logAnalyzer != null)
									{
										stringBuilder5.AppendFormat("   Analyzer: '{0}' ", analyzerConfigurationElement.Name);
										stringBuilder5.AppendLine();
										list2.Add(logAnalyzer);
									}
									else
									{
										stringBuilder4.AppendLine().AppendFormat("Analyzer '{0}' could not be created. It will not be added to Job '{1}'. Enabled is '{2}'. Analyzer Role is '{3}'.", new object[]
										{
											analyzerConfigurationElement.Name,
											watermarkedJob.Name,
											analyzerConfigurationElement.Enabled,
											analyzerConfigurationElement.Role
										});
									}
								}
								else
								{
									stringBuilder4.AppendLine().AppendFormat("Analyzer '{0}' is not being added to Job '{1}'. Enabled is '{2}'. Analyzer Role is '{3}'.", new object[]
									{
										analyzerConfigurationElement.Name,
										watermarkedJob.Name,
										analyzerConfigurationElement.Enabled,
										analyzerConfigurationElement.Role
									}).AppendLine();
									Log.LogInformationMessage("JobManager:  Analyzer '{0}' is not being added to Job '{1}'. Enabled is '{2}'. Analyzer Role is '{3}'.", new object[]
									{
										analyzerConfigurationElement.Name,
										watermarkedJob.Name,
										analyzerConfigurationElement.Enabled,
										analyzerConfigurationElement.Role
									});
								}
							}
							watermarkedJob.AddLogAnalyzers(list2);
							if (watermarkedJob.GetLogAnalyzers<LogAnalyzer>().Count<LogAnalyzer>() > 0)
							{
								stringBuilder.Append(stringBuilder5);
								Thread thread = new Thread(new ParameterizedThreadStart(JobManager.JobRunner));
								thread.Name = jobConfigurationElement.Name;
								thread.IsBackground = true;
								this.jobs.Add(new KeyValuePair<Thread, WatermarkedJob>(thread, watermarkedJob));
							}
							else
							{
								stringBuilder2.AppendLine();
								stringBuilder2.AppendFormat("Job: '{0}' is NOT being added because no analyzers were configured for it.", jobConfigurationElement.Name);
								watermarkedJob.Dispose();
							}
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_JobManagerStarted, new object[]
				{
					stringBuilder.ToString()
				});
			}
			if (stringBuilder2.Length > 0)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_JobManagerNotStarted, new object[]
				{
					stringBuilder2.ToString()
				});
			}
			if (stringBuilder3.Length > 0)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_JobManagerStartupFailures, new object[]
				{
					stringBuilder3.ToString()
				});
			}
			if (stringBuilder4.Length > 0)
			{
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_AnalyzerNotAdded, new object[]
				{
					stringBuilder4.ToString()
				});
			}
			foreach (KeyValuePair<Thread, WatermarkedJob> keyValuePair in this.jobs)
			{
				keyValuePair.Key.Start(keyValuePair.Value);
				Logger.LogInformationMessage("Started '{0}' job.", new object[]
				{
					keyValuePair.Value.Name
				});
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000047DC File Offset: 0x000029DC
		private static bool HandleException(Exception e)
		{
			return e is ArgumentNullException || e is FileNotFoundException || e is FileLoadException || e is ArgumentException || e is BadImageFormatException || e is AmbiguousMatchException;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004814 File Offset: 0x00002A14
		private bool CreateJob(JobConfigurationElement configuredJob, out WatermarkedJob job)
		{
			job = null;
			OutputStream outputStream;
			if (!this.outputStreams.TryGetValue(configuredJob.OutputStream, out outputStream))
			{
				try
				{
					Type type = Type.GetType(configuredJob.OutputStream, true, true);
					ConstructorInfo constructor = type.GetConstructor(new Type[]
					{
						typeof(OutputStream)
					});
					OutputStream outputStream2 = this.outputStreams["default"];
					outputStream = (OutputStream)constructor.Invoke(new object[]
					{
						outputStream2
					});
					this.outputStreams.Add(configuredJob.OutputStream, outputStream);
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to find output stream '{1}' for the the job '{0}', Exception: {2}.", new object[]
					{
						configuredJob.Name,
						configuredJob.OutputStream,
						ex
					});
					return false;
				}
			}
			object[] parameters = new object[]
			{
				outputStream,
				this.watermarks,
				configuredJob.Name
			};
			Logger.LogInformationMessage("JobManager:  The '{0}' job is being created", new object[]
			{
				configuredJob.Name
			});
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.LoadFrom(Path.Combine(this.filePath, configuredJob.Assembly));
				Type type2 = assembly.GetType(configuredJob.Class, false, true);
				if (type2 == null)
				{
					Logger.LogErrorMessage("Unable to create the job '{0}' because the class '{1}' was not found.", new object[]
					{
						configuredJob.Name,
						configuredJob.Class
					});
					return false;
				}
				method = type2.GetMethod(configuredJob.Method, new Type[]
				{
					typeof(OutputStream),
					typeof(Watermarks),
					typeof(string)
				});
				if (method == null)
				{
					Logger.LogErrorMessage("Unable to create the job '{0}' because the method '{1}' was not found.", new object[]
					{
						configuredJob.Name,
						configuredJob.Method
					});
					return false;
				}
			}
			catch (Exception ex2)
			{
				if (JobManager.HandleException(ex2))
				{
					Logger.LogErrorMessage("Unable to create the job '{0}'. Exception: '{1}'", new object[]
					{
						configuredJob.Name,
						ex2
					});
					return false;
				}
				throw;
			}
			bool result;
			try
			{
				job = (WatermarkedJob)method.Invoke(null, parameters);
				result = true;
			}
			catch (Exception ex3)
			{
				Logger.LogErrorMessage("Unable to create job '{0}'. Exception: '{1}'.", new object[]
				{
					configuredJob.Name,
					ex3
				});
				this.poisonManager.Crash(configuredJob.Name, "Job crashed during construction", ex3);
				throw;
			}
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004ABC File Offset: 0x00002CBC
		private LogAnalyzer CreateAnalyzer(AnalyzerConfigurationElement configuredAnalyzer, WatermarkedJob newJob)
		{
			Logger.LogInformationMessage("The '{0}' analyzer is being added to job '{1}'.", new object[]
			{
				configuredAnalyzer.Name,
				newJob.Name
			});
			ConstructorInfo constructor;
			object[] parameters;
			try
			{
				Assembly assembly = Assembly.LoadFrom(Path.Combine(this.filePath, configuredAnalyzer.Assembly));
				Type type = assembly.GetType(configuredAnalyzer.Name);
				if (type == null)
				{
					Logger.LogErrorMessage("Unable to create the analyzer '{0}' for job '{1}' because the class was not found.", new object[]
					{
						configuredAnalyzer.Name,
						newJob.Name
					});
					return null;
				}
				Type[] types = new Type[]
				{
					typeof(Job)
				};
				constructor = type.GetConstructor(types);
				if (constructor == null)
				{
					Logger.LogErrorMessage("Unable to create the analyzer '{0}' for job '{1}' because the constructor was not found.", new object[]
					{
						configuredAnalyzer.Name,
						newJob.Name
					});
					return null;
				}
				parameters = new object[]
				{
					newJob
				};
			}
			catch (Exception ex)
			{
				if (JobManager.HandleException(ex))
				{
					Logger.LogErrorMessage("Unable to create the analyzer '{0}' for job '{1}'. Exception: '{2}'", new object[]
					{
						configuredAnalyzer.Name,
						newJob.Name,
						ex
					});
					return null;
				}
				throw;
			}
			LogAnalyzer result;
			try
			{
				LogAnalyzer logAnalyzer = (LogAnalyzer)constructor.Invoke(parameters);
				Type type2 = logAnalyzer.GetType();
				string key;
				if (type2.IsNested)
				{
					key = string.Format("{0}+{1}", type2.BaseType.Name, type2.Name);
				}
				else
				{
					key = type2.Name;
				}
				newJob.OutputFormats.Add(key, configuredAnalyzer.OutputFormat);
				result = logAnalyzer;
			}
			catch (Exception ex2)
			{
				Logger.LogErrorMessage("Unable to create analyzer'{0}' for job '{1}'. Exception: '{2}'", new object[]
				{
					configuredAnalyzer.Name,
					newJob.Name,
					ex2
				});
				this.poisonManager.Crash(newJob.Name, "Failed to create analyzer " + configuredAnalyzer.Name, ex2);
				throw;
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004CD8 File Offset: 0x00002ED8
		private bool IsRole(string input)
		{
			string[] array = input.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				bool result;
				if (string.Equals(text, "All", StringComparison.OrdinalIgnoreCase))
				{
					result = true;
				}
				else
				{
					if (!ServerRole.IsRole(text))
					{
						i++;
						continue;
					}
					result = true;
				}
				return result;
			}
			return false;
		}

		// Token: 0x0400002A RID: 42
		private const string AddLogAnalyzerMethodName = "AddLogAnalyzer";

		// Token: 0x0400002B RID: 43
		private readonly Dictionary<string, OutputStream> outputStreams;

		// Token: 0x0400002C RID: 44
		private readonly JobConfigurationCollection configuredJobs;

		// Token: 0x0400002D RID: 45
		private readonly string filePath;

		// Token: 0x0400002E RID: 46
		private readonly List<KeyValuePair<Thread, WatermarkedJob>> jobs;

		// Token: 0x0400002F RID: 47
		private readonly Watermarks watermarks;

		// Token: 0x04000030 RID: 48
		private readonly PoisonManager poisonManager;

		// Token: 0x04000031 RID: 49
		private readonly object syncLock;

		// Token: 0x04000032 RID: 50
		private readonly bool datacenterMode;
	}
}
