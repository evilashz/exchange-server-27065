using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Audit;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000CF RID: 207
	internal class WatsonExceptionReport : WatsonReport
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00017934 File Offset: 0x00015B34
		public WatsonExceptionReport(string eventType, Process process, Exception exception, ReportOptions reportOptions) : base(eventType, process)
		{
			this.exception = exception;
			base.ReportOptions = reportOptions;
			IntPtr exceptionPointers = Marshal.GetExceptionPointers();
			if ((base.ReportOptions & ReportOptions.DoNotLogProcessAndThreadIds) == ReportOptions.None)
			{
				base.LogExtraData("Process ID=" + base.ProcessId);
				if (!base.TargetExternalProcess)
				{
					base.LogExtraData(string.Concat(new object[]
					{
						"Managed Thread ID=",
						Environment.CurrentManagedThreadId,
						Environment.NewLine,
						"Native Thread ID=",
						DiagnosticsNativeMethods.GetCurrentThreadId()
					}));
				}
			}
			if (exceptionPointers == IntPtr.Zero)
			{
				this.exceptionInfo = null;
				this.exceptionEIP = IntPtr.Zero;
				return;
			}
			this.exceptionInfo = new DiagnosticsNativeMethods.WER_EXCEPTION_INFORMATION();
			this.exceptionInfo.exceptionPointers = exceptionPointers;
			this.exceptionInfo.clientPointers = false;
			this.exceptionEIP = ((DiagnosticsNativeMethods.ExceptionRecord)Marshal.PtrToStructure(((DiagnosticsNativeMethods.ExceptionPointers)Marshal.PtrToStructure(exceptionPointers, typeof(DiagnosticsNativeMethods.ExceptionPointers))).ExceptionRecord, typeof(DiagnosticsNativeMethods.ExceptionRecord))).ExceptionAddress;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00017A52 File Offset: 0x00015C52
		internal static int NativeReaperThreadId
		{
			get
			{
				return WatsonExceptionReport.nativeReaperThreadId;
			}
		}

		// Token: 0x170000EE RID: 238
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00017A59 File Offset: 0x00015C59
		internal static Action BeforeDumpCollectionAction
		{
			set
			{
				WatsonExceptionReport.beforeDumpCollectionAction = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00017A61 File Offset: 0x00015C61
		protected Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00017ABC File Offset: 0x00015CBC
		public override void Send()
		{
			if (base.IsFlagSet(ReportOptions.DoNotFreezeThreads) || base.IsFlagSet(ReportOptions.DoNotCollectDumps))
			{
				base.Send();
				return;
			}
			lock (WatsonReport.HeapDumpReportLockObject)
			{
				if (!ExWatson.ReaperThreadAllowed)
				{
					base.Send();
				}
				else
				{
					Thread thread = null;
					try
					{
						ManualResetEvent reaperThreadIsRunning = new ManualResetEvent(false);
						thread = new Thread(delegate()
						{
							try
							{
								WatsonExceptionReport.nativeReaperThreadId = DiagnosticsNativeMethods.GetCurrentThreadId();
								reaperThreadIsRunning.Set();
								Thread.Sleep(ExWatson.CrashReportTimeout);
								ExWatson.TerminateCurrentProcess();
							}
							catch
							{
							}
						});
						thread.Start();
						reaperThreadIsRunning.WaitOne();
						base.Send();
					}
					catch
					{
					}
					finally
					{
						if (thread != null)
						{
							thread.Abort();
							WatsonExceptionReport.nativeReaperThreadId = -1;
						}
					}
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00017B8C File Offset: 0x00015D8C
		internal static bool TryStringHashFromStackTrace(Exception exception, bool deep, out string stringHash)
		{
			bool result = false;
			int num = deep ? 50 : 10;
			stringHash = "0";
			try
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				while (exception != null)
				{
					string stackTrace = exception.StackTrace;
					if (!string.IsNullOrEmpty(exception.StackTrace))
					{
						MatchCollection matchCollection = WatsonExceptionReport.regexFunctions.Matches(stackTrace);
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							if (match.Groups.Count == 2)
							{
								string value = match.Groups[1].Value;
								if (num3 == 0)
								{
									num4 = WatsonReport.ComputeHash(value, num4);
								}
								if (!value.Contains("Microsoft"))
								{
									continue;
								}
								num3++;
								num2 = WatsonReport.ComputeHash(value, num2);
							}
							if (num3 >= num)
							{
								break;
							}
						}
						if (exception is SerializationException)
						{
							Match match2 = WatsonExceptionReport.regexSerialization.Match(exception.Message);
							while (match2.Success)
							{
								string value2 = match2.Groups[1].Value;
								num2 = WatsonReport.ComputeHash(value2, num2);
								match2 = match2.NextMatch();
							}
						}
					}
					exception = exception.InnerException;
				}
				if (num3 == 0)
				{
					num2 = num4;
				}
				stringHash = Convert.ToString(num2 & 65535, 16);
				result = true;
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00017D14 File Offset: 0x00015F14
		protected static void WriteReportFileLIDs(TextWriter reportFile, string lids)
		{
			if (lids != null)
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("------------------- Location IDs -------------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(lids);
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00017D50 File Offset: 0x00015F50
		protected static void WriteReportFileExceptionObjectInfo(TextWriter reportFile, Exception exceptionObject)
		{
			if (exceptionObject != null)
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("------------ exceptionObject.ToString() ------------");
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine(WatsonReport.SanitizeException(exceptionObject.ToString()));
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00017DA0 File Offset: 0x00015FA0
		protected static void WriteReportFileInnerExceptionObjectsInfo(TextWriter reportFile, Exception exception)
		{
			if (exception != null)
			{
				for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
				{
					reportFile.WriteLine(reportFile.NewLine);
					reportFile.WriteLine("-------- exception.InnerException.ToString() -------");
					reportFile.WriteLine(WatsonReport.SanitizeException(innerException.ToString()));
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00017DEC File Offset: 0x00015FEC
		protected static void WriteReportFileStackTrace(TextWriter reportFile, Exception exception)
		{
			if (exception != null)
			{
				reportFile.WriteLine(reportFile.NewLine);
				reportFile.WriteLine("----------------------------------------------------");
				reportFile.WriteLine("--------------- exception.StackTrace ---------------");
				reportFile.WriteLine("----------------------------------------------------");
				if (exception.StackTrace != null)
				{
					reportFile.WriteLine(WatsonReport.SanitizeException(exception.StackTrace));
				}
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00017E58 File Offset: 0x00016058
		protected override ProcSafeHandle GetProcessHandle()
		{
			ProcSafeHandle procSafeHandle = new ProcSafeHandle();
			ProcSafeHandle procSafeHandle2 = base.EvaluateOrDefault<ProcSafeHandle>(() => DiagnosticsNativeMethods.OpenProcess(DiagnosticsNativeMethods.ProcessAccess.VmRead | DiagnosticsNativeMethods.ProcessAccess.QueryInformation | DiagnosticsNativeMethods.ProcessAccess.StandardRightsRead, false, base.ProcessId), procSafeHandle);
			if (procSafeHandle2 != procSafeHandle && procSafeHandle != null)
			{
				procSafeHandle.Dispose();
			}
			return procSafeHandle2;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00017F9C File Offset: 0x0001619C
		protected override void PrepareBucketingParameters()
		{
			Exception baseException = null;
			string s = null;
			string text = null;
			string text2 = null;
			string text3 = null;
			Version v = null;
			int num = 0;
			int num2 = 0;
			string text4 = null;
			if (this.exception != null)
			{
				text2 = this.exception.GetType().ToString();
				WatsonExceptionReport.TryStringHashFromStackTrace(this.exception, base.IsFlagSet(ReportOptions.DeepStackTraceHash), out text3);
				baseException = this.exception.GetBaseException();
				try
				{
					string text5 = (baseException == null) ? null : baseException.StackTrace;
					if (!string.IsNullOrEmpty(text5))
					{
						MatchCollection matchCollection = WatsonExceptionReport.regexFunctions.Matches(text5);
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							if (match.Groups.Count == 2)
							{
								if (text == null)
								{
									text = match.Groups[1].Value;
								}
								if (match.Groups[1].Value.Contains("Microsoft."))
								{
									text = match.Groups[1].Value;
									if (text.IndexOf("throw", StringComparison.OrdinalIgnoreCase) <= 0 && text.IndexOf("failfast", StringComparison.OrdinalIgnoreCase) <= 0 && text.IndexOf("assert", StringComparison.OrdinalIgnoreCase) <= 0 && text.IndexOf("error", StringComparison.OrdinalIgnoreCase) <= 0)
									{
										break;
									}
								}
							}
						}
					}
					if (text != null)
					{
						string text6 = text;
						try
						{
							int num3 = text.LastIndexOf(Type.Delimiter + ".");
							if (num3 < 0)
							{
								num3 = text.LastIndexOf(Type.Delimiter);
							}
							text6 = text.Substring(0, num3);
						}
						catch
						{
						}
						Type type = Type.GetType(text6);
						if (type == null)
						{
							foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
							{
								type = assembly.GetType(text6);
								if (type != null)
								{
									break;
								}
							}
						}
						if (type != null)
						{
							if (WatsonReport.IsAssemblyDynamic(type.Assembly))
							{
								s = "[Dynamic Assembly]";
								v = new Version(0, 0, 0, 0);
							}
							else
							{
								try
								{
									s = Path.GetFileName(type.Assembly.Location);
								}
								catch
								{
								}
								try
								{
									object[] customAttributes = type.Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
									if (customAttributes.Length == 1)
									{
										AssemblyFileVersionAttribute assemblyFileVersionAttribute = customAttributes[0] as AssemblyFileVersionAttribute;
										if (assemblyFileVersionAttribute != null)
										{
											v = new Version(assemblyFileVersionAttribute.Version);
										}
									}
								}
								catch
								{
								}
							}
						}
					}
					PropertyInfo propertyInfo = (baseException == null) ? null : baseException.GetType().GetProperty("DiagCtx");
					if (propertyInfo != null)
					{
						MethodInfo getMethod = propertyInfo.GetGetMethod();
						object obj2 = getMethod.Invoke(baseException, null);
						this.lids = obj2.ToString();
						MatchCollection matchCollection2 = WatsonExceptionReport.regexLids.Matches(this.lids);
						foreach (object obj3 in matchCollection2)
						{
							Match match2 = (Match)obj3;
							if (match2.Groups.Count >= 2)
							{
								string value = match2.Groups[0].Value;
								num = WatsonReport.ComputeHash(value, num);
								num2 = Convert.ToInt32(match2.Groups[1].Value);
								if (match2.Groups.Count == 4)
								{
									text4 = match2.Groups[3].Value;
								}
							}
						}
						num &= 65535;
					}
					if (ExWatson.MsInternal)
					{
						base.CrashDetails = WatsonReport.GetValidString(this.GetExWatsonCrashDetailsString());
					}
				}
				catch
				{
				}
			}
			if (baseException != null && baseException.TargetSite != null)
			{
				this.baseExceptionTargetSite = base.EvaluateOrDefault<string>(delegate
				{
					RuntimeMethodHandle methodHandle = baseException.TargetSite.MethodHandle;
					IntPtr value2 = baseException.TargetSite.MethodHandle.Value;
					return baseException.TargetSite.MethodHandle.Value.ToString();
				}, "unknown");
				this.baseExceptionAssemblyName = base.EvaluateOrDefault<string>(delegate
				{
					if (baseException.TargetSite.Module == null)
					{
						return "unknown";
					}
					return baseException.TargetSite.Module.Name ?? "unknown";
				}, "unknown");
				this.baseExceptionMethodName = base.EvaluateOrDefault<string>(delegate
				{
					string text7 = null;
					if (baseException.TargetSite.ReflectedType != null)
					{
						text7 = baseException.TargetSite.ReflectedType.FullName;
					}
					string name = baseException.TargetSite.Name;
					if (text7 == null || name == null)
					{
						return "unknown";
					}
					return text7 + "." + name;
				}, "unknown");
			}
			else
			{
				this.baseExceptionTargetSite = "unknown";
				this.baseExceptionAssemblyName = "unknown";
				this.baseExceptionMethodName = "unknown";
			}
			try
			{
				if (this.lids != null)
				{
					text3 = text3 + "-" + Convert.ToString(num, 16);
					this.baseExceptionAssemblyName = this.baseExceptionAssemblyName + "-" + num2;
					if (text4 != null)
					{
						text2 = text2 + "-" + text4;
					}
				}
			}
			catch
			{
			}
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExceptionType, WatsonReport.GetValidString(text2));
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyName, WatsonReport.GetValidString(s));
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyVer, WatsonReport.ExchangeFormattedVersion(v));
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExMethodName, WatsonReport.GetValidString(text));
			base.SetBucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.CallstackHash, WatsonReport.GetValidString(text3));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00018564 File Offset: 0x00016764
		protected override WatsonIssueType GetIssueTypeCode()
		{
			if (ExWatson.IsObjectNotDisposedException(this.exception))
			{
				return WatsonIssueType.ManagedCodeDisposableLeak;
			}
			if (base.EventType.Equals("E12IIS", StringComparison.OrdinalIgnoreCase))
			{
				return WatsonIssueType.ManagedCodeIISException;
			}
			if (base.EventType.Equals("E12N", StringComparison.OrdinalIgnoreCase))
			{
				return WatsonIssueType.NativeCodeCrash;
			}
			return WatsonIssueType.ManagedCodeException;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000185A0 File Offset: 0x000167A0
		protected override string GetIssueDetails()
		{
			string text = base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExMethodName);
			if (this.exception == null)
			{
				return "Unknown error in " + text;
			}
			string name = this.exception.GetType().Name;
			string message = this.exception.Message;
			return string.Concat(new string[]
			{
				name,
				" (",
				message,
				") in ",
				text
			});
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001861C File Offset: 0x0001681C
		protected override void WriteReportTypeSpecificSection(XmlWriter reportFile)
		{
			using (new SafeXmlTag(reportFile, "exception-report").WithAttribute("is-clr-terminating", base.IsFlagSet(ReportOptions.ReportTerminateAfterSend) ? "1" : "0"))
			{
				using (new SafeXmlTag(reportFile, "application").WithAttribute("name", base.AppName).WithAttribute("version", WatsonReport.ExchangeFormattedVersion(base.AppVersion)))
				{
				}
				using (new SafeXmlTag(reportFile, "assembly").WithAttribute("name", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyName)).WithAttribute("version", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyVer)))
				{
				}
				using (SafeXmlTag safeXmlTag4 = new SafeXmlTag(reportFile, "exception"))
				{
					safeXmlTag4.SetContent(WatsonReport.SanitizeException(this.exception.ToString()));
				}
				using (new SafeXmlTag(reportFile, "base-exception").WithAttribute("target-site", this.baseExceptionTargetSite).WithAttribute("assembly-name", this.baseExceptionAssemblyName).WithAttribute("method-name", this.baseExceptionMethodName))
				{
				}
				using (SafeXmlTag safeXmlTag6 = new SafeXmlTag(reportFile, "process").WithAttribute("eip", "0x" + ((IntPtr.Size == 4) ? this.exceptionEIP.ToString("x8") : this.exceptionEIP.ToString("x16"))).WithAttribute("bitness", (IntPtr.Size * 8).ToString()).WithAttribute("pid", base.ProcessId.ToString()))
				{
					if (base.ProcessId == DiagnosticsNativeMethods.GetCurrentProcessId())
					{
						safeXmlTag6.WithAttribute("managed-tid", Environment.CurrentManagedThreadId.ToString()).WithAttribute("native-tid", DiagnosticsNativeMethods.GetCurrentThreadId().ToString());
					}
				}
				if (this.lids != null)
				{
					using (SafeXmlTag safeXmlTag7 = new SafeXmlTag(reportFile, "location-ids"))
					{
						safeXmlTag7.SetContent(this.lids);
					}
				}
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000188F8 File Offset: 0x00016AF8
		protected override void WriteSpecializedPartOfTextReport(TextWriter reportFile)
		{
			base.WriteReportFileHeader(reportFile, "CLR is " + (base.IsFlagSet(ReportOptions.ReportTerminateAfterSend) ? string.Empty : "not ") + "terminating");
			reportFile.WriteLine("P1(flavor)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.Flavor));
			reportFile.WriteLine("P2(appVersion)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AppVersion));
			reportFile.WriteLine("P3(appName)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AppName));
			reportFile.WriteLine("P4(assemblyName)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyName));
			reportFile.WriteLine("P5(exMethodName)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExMethodName));
			reportFile.WriteLine("P6(exceptionType)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.ExceptionType));
			reportFile.WriteLine("P7(callstackHash)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.CallstackHash));
			reportFile.WriteLine("P8(assemblyVer)={0}", base.BucketingParameter<WatsonExceptionReport.BucketParamId>(WatsonExceptionReport.BucketParamId.AssemblyVer));
			reportFile.WriteLine();
			reportFile.WriteLine("Exchange Version={0}", WatsonReport.ExchangeFormattedVersion(ExWatson.ExchangeVersion));
			reportFile.WriteLine("Default Assembly Version={0}", WatsonReport.ExchangeFormattedVersion(ExWatson.DefaultAssemblyVersion));
			reportFile.WriteLine("Executable Name={0}", WatsonReport.GetValidString(ExWatson.RealAppName));
			reportFile.WriteLine("Executable Version={0}", WatsonReport.ExchangeFormattedVersion(ExWatson.RealApplicationVersion));
			reportFile.WriteLine("Base Exception Target Site={0}", this.baseExceptionTargetSite);
			reportFile.WriteLine("Base Exception Assembly name={0}", this.baseExceptionAssemblyName);
			reportFile.WriteLine("Base Exception Method Name={0}", this.baseExceptionMethodName);
			reportFile.WriteLine("Exception Message={0}", this.exception.Message);
			reportFile.WriteLine("EIP=0x{0}", (IntPtr.Size == 4) ? this.exceptionEIP.ToString("x8") : this.exceptionEIP.ToString("x16"));
			reportFile.WriteLine("Build bit-size={0}", IntPtr.Size * 8);
			WatsonExceptionReport.WriteReportFileLIDs(reportFile, this.lids);
			WatsonExceptionReport.WriteReportFileExceptionObjectInfo(reportFile, this.exception);
			WatsonExceptionReport.WriteReportFileInnerExceptionObjectsInfo(reportFile, this.exception);
			WatsonExceptionReport.WriteReportFileStackTrace(reportFile, this.exception);
			WatsonExceptionReport.WriteReportFileLIDs(reportFile, this.lids);
			WatsonExceptionReport.WriteReportFileInnerExceptionObjectsInfo(reportFile, this.exception);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00018AF8 File Offset: 0x00016CF8
		protected override void BeforeSubmit()
		{
			if (base.ProcessId != DiagnosticsNativeMethods.GetCurrentProcessId())
			{
				try
				{
					this.debugPrivilege = new Privilege("SeDebugPrivilege");
					this.debugPrivilege.Enable();
				}
				catch
				{
				}
			}
			base.IsFlagSet(ReportOptions.DoNotFreezeThreads);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00018B4C File Offset: 0x00016D4C
		protected override void AfterSubmit()
		{
			if (this.debugPrivilege != null)
			{
				this.debugPrivilege.Revert();
				this.debugPrivilege.Dispose();
				this.debugPrivilege = null;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018B74 File Offset: 0x00016D74
		protected override void AddExtraFiles(WerSafeHandle watsonReportHandle)
		{
			if (base.ReportingAllowed && !base.ProcessHandle.IsInvalid && !base.IsFlagSet(ReportOptions.DoNotCollectDumps))
			{
				if (WatsonExceptionReport.beforeDumpCollectionAction != null)
				{
					WatsonExceptionReport.beforeDumpCollectionAction();
				}
				DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS wer_DUMP_CUSTOM_OPTIONS = new DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS();
				wer_DUMP_CUSTOM_OPTIONS.size = Marshal.SizeOf(typeof(DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS));
				wer_DUMP_CUSTOM_OPTIONS.mask = DiagnosticsNativeMethods.WER_DUMP_FLAGS.WER_DUMP_MASK_DUMPTYPE;
				wer_DUMP_CUSTOM_OPTIONS.dumpFlags = (DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithDataSegs | DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithHandleData | DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithUnloadedModules | DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithProcessThreadData | DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithPrivateReadWriteMemory);
				DiagnosticsNativeMethods.WerReportAddDump(watsonReportHandle, base.ProcessHandle, IntPtr.Zero, DiagnosticsNativeMethods.WER_DUMP_TYPE.WerDumpTypeMiniDump, this.exceptionInfo, wer_DUMP_CUSTOM_OPTIONS, 0U);
				DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS wer_DUMP_CUSTOM_OPTIONS2 = new DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS();
				wer_DUMP_CUSTOM_OPTIONS2.size = Marshal.SizeOf(typeof(DiagnosticsNativeMethods.WER_DUMP_CUSTOM_OPTIONS));
				wer_DUMP_CUSTOM_OPTIONS2.mask = DiagnosticsNativeMethods.WER_DUMP_FLAGS.WER_DUMP_MASK_DUMPTYPE;
				wer_DUMP_CUSTOM_OPTIONS2.dumpFlags = (wer_DUMP_CUSTOM_OPTIONS.dumpFlags | DiagnosticsNativeMethods.MINIDUMP_TYPE.MiniDumpWithFullMemory);
				DiagnosticsNativeMethods.WerReportAddDump(watsonReportHandle, base.ProcessHandle, IntPtr.Zero, DiagnosticsNativeMethods.WER_DUMP_TYPE.WerDumpTypeHeapDump, this.exceptionInfo, wer_DUMP_CUSTOM_OPTIONS2, 0U);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00018C4C File Offset: 0x00016E4C
		private string GetExWatsonCrashDetailsString()
		{
			Exception baseException = this.Exception.GetBaseException();
			if (baseException.StackTrace == null)
			{
				return string.Empty;
			}
			string text = (baseException.StackTrace.Length <= 10240) ? baseException.StackTrace : baseException.StackTrace.Substring(0, 10240);
			return string.Format("exData={0}|exHResult={1}|exStacktrace={2}|exTargetSite={3}|exSource={4}|exMessage={5}", new object[]
			{
				this.GetStringFromDictionary(baseException.Data),
				baseException.HResult,
				text,
				this.baseExceptionTargetSite,
				baseException.Source ?? string.Empty,
				baseException.Message ?? string.Empty
			});
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00018D04 File Offset: 0x00016F04
		private string GetStringFromDictionary(IDictionary dictionary)
		{
			if (dictionary == null || dictionary.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.AppendFormat("{0}:{1},", dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000427 RID: 1063
		private const int ShallowStackTraceHashDepth = 10;

		// Token: 0x04000428 RID: 1064
		private const int DeepStackTraceHashDepth = 50;

		// Token: 0x04000429 RID: 1065
		private const int MaxStackTraceLengthInBytes = 10240;

		// Token: 0x0400042A RID: 1066
		private static readonly Regex regexLids = new Regex("Lid:\\s+(\\d+)\\s+([a-zA-Z]+:\\s+([0-9A-Fx]+))?", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400042B RID: 1067
		private static readonly Regex regexFunctions = new Regex("   at ([^\\(]*?)\\(.*?\\)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400042C RID: 1068
		private static readonly Regex regexSerialization = new Regex("'([^']+)'", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400042D RID: 1069
		private static int nativeReaperThreadId = -1;

		// Token: 0x0400042E RID: 1070
		private static Action beforeDumpCollectionAction;

		// Token: 0x0400042F RID: 1071
		private Exception exception;

		// Token: 0x04000430 RID: 1072
		private DiagnosticsNativeMethods.WER_EXCEPTION_INFORMATION exceptionInfo;

		// Token: 0x04000431 RID: 1073
		private IntPtr exceptionEIP;

		// Token: 0x04000432 RID: 1074
		private string lids;

		// Token: 0x04000433 RID: 1075
		private string baseExceptionTargetSite;

		// Token: 0x04000434 RID: 1076
		private string baseExceptionAssemblyName;

		// Token: 0x04000435 RID: 1077
		private string baseExceptionMethodName;

		// Token: 0x04000436 RID: 1078
		private Privilege debugPrivilege;

		// Token: 0x020000D0 RID: 208
		internal new enum BucketParamId
		{
			// Token: 0x04000438 RID: 1080
			Flavor,
			// Token: 0x04000439 RID: 1081
			AppVersion,
			// Token: 0x0400043A RID: 1082
			AppName,
			// Token: 0x0400043B RID: 1083
			AssemblyName,
			// Token: 0x0400043C RID: 1084
			ExMethodName,
			// Token: 0x0400043D RID: 1085
			ExceptionType,
			// Token: 0x0400043E RID: 1086
			CallstackHash,
			// Token: 0x0400043F RID: 1087
			AssemblyVer,
			// Token: 0x04000440 RID: 1088
			_Count
		}
	}
}
