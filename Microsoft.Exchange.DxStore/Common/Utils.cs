using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using FUSE.Weld.Base;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Audit;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008B RID: 139
	public static class Utils
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000120DF File Offset: 0x000102DF
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.UtilsTracer;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000120E8 File Offset: 0x000102E8
		public static ProcessBasicInfo CurrentProcessInfo
		{
			get
			{
				if (Utils.currentProcessInfo == null)
				{
					lock (Utils.locker)
					{
						if (Utils.currentProcessInfo == null)
						{
							Utils.currentProcessInfo = new ProcessBasicInfo(true);
						}
					}
				}
				return Utils.currentProcessInfo;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00012140 File Offset: 0x00010340
		public static RegistryValueKind GetValueKind(object value)
		{
			RegistryValueKind result = RegistryValueKind.Unknown;
			if (value is string)
			{
				result = RegistryValueKind.String;
			}
			else if (value is string[])
			{
				result = RegistryValueKind.MultiString;
			}
			else if (value is byte[])
			{
				result = RegistryValueKind.Binary;
			}
			else if (value is int || value is uint)
			{
				result = RegistryValueKind.DWord;
			}
			else if (value is long || value is ulong)
			{
				result = RegistryValueKind.QWord;
			}
			return result;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000121BC File Offset: 0x000103BC
		public static string[] GetMultistring(XElement element)
		{
			if (element.HasElements)
			{
				return (from el in element.Elements()
				where Utils.IsEqual(el.Name.LocalName, "String", StringComparison.OrdinalIgnoreCase)
				select el.Value).ToArray<string>();
			}
			return Utils.EmptyArray<string>();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00012228 File Offset: 0x00010428
		public static T WithReadLock<T>(this ReaderWriterLockSlim locker, Func<T> func)
		{
			T result;
			try
			{
				locker.EnterReadLock();
				result = func();
			}
			finally
			{
				locker.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001225C File Offset: 0x0001045C
		public static T WithWriteLock<T>(this ReaderWriterLockSlim locker, Func<T> func)
		{
			T result;
			try
			{
				locker.EnterWriteLock();
				result = func();
			}
			finally
			{
				locker.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00012290 File Offset: 0x00010490
		public static void WithReadLock(this ReaderWriterLockSlim locker, Action action)
		{
			try
			{
				locker.EnterReadLock();
				action();
			}
			finally
			{
				locker.ExitReadLock();
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000122C4 File Offset: 0x000104C4
		public static void WithWriteLock(this ReaderWriterLockSlim locker, Action action)
		{
			try
			{
				locker.EnterWriteLock();
				action();
			}
			finally
			{
				locker.ExitWriteLock();
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00012484 File Offset: 0x00010684
		public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
		{
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					yield return Utils.YieldBatchElements<T>(enumerator, batchSize - 1);
				}
			}
			yield break;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000124A8 File Offset: 0x000106A8
		public static void DisposeBestEffort(IDisposable disposable)
		{
			if (disposable != null)
			{
				try
				{
					disposable.Dispose();
				}
				catch (Exception ex)
				{
					Utils.Tracer.TraceError<string>(0L, "DisposeBestEffort() failed with {0}", ex.Message);
				}
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000124EC File Offset: 0x000106EC
		public static void AbortBestEffort<T>(ChannelFactory<T> factoryToAbort)
		{
			if (factoryToAbort != null)
			{
				try
				{
					factoryToAbort.Abort();
				}
				catch (Exception ex)
				{
					Utils.Tracer.TraceError<string>(0L, "ChannelFactory.Abort() failed with {0}", ex.Message);
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00012530 File Offset: 0x00010730
		public static Exception RunWithPrivilege(string privilege, Action action)
		{
			Exception result = null;
			try
			{
				using (Privilege privilege2 = new Privilege(privilege))
				{
					privilege2.Enable();
					action();
				}
			}
			catch (PrivilegeNotHeldException ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00012584 File Offset: 0x00010784
		public static Exception RunOperation(string identity, string label, Action action, IDxStoreEventLogger logger, LogOptions options, bool isBestEffort = false, TimeSpan? timeout = null, TimeSpan? periodicDuration = null, string periodicKey = null, Action<Exception> exitAction = null, string context = null)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool isPeriodic = false;
			if ((options & LogOptions.LogNever) != LogOptions.LogNever)
			{
				flag = ((options & LogOptions.LogStart) == LogOptions.LogStart);
				flag2 = ((options & LogOptions.LogException) == LogOptions.LogException);
				flag3 = ((options & LogOptions.LogSuccess) == LogOptions.LogSuccess);
				if ((options & LogOptions.LogAlways) == LogOptions.LogAlways)
				{
					isPeriodic = ((options & LogOptions.LogPeriodic) == LogOptions.LogPeriodic);
				}
			}
			periodicKey = (periodicKey ?? string.Format("{0}-{1}", identity, label));
			Exception ex = null;
			try
			{
				ExTraceGlobals.RunOperationTracer.TraceDebug<string, string>((long)identity.GetHashCode(), "{0}: Attempting to run {1}", identity, label);
				if (flag)
				{
					Utils.LogStartingEvent(identity, label, logger, isPeriodic, periodicKey, periodicDuration, context);
				}
				if (timeout != null)
				{
					Task task = Task.Factory.StartNew(action);
					if (!task.Wait(timeout.Value))
					{
						ExTraceGlobals.RunOperationTracer.TraceError<string, string, TimeSpan?>((long)identity.GetHashCode(), "{0}: Operation {1} timed out after {2}", identity, label, timeout);
						throw new TimeoutException();
					}
				}
				else
				{
					action();
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				ExTraceGlobals.RunOperationTracer.TraceError<string, string, Exception>((long)identity.GetHashCode(), "{0}: Operation {1} failed with {2}", identity, label, ex2);
				if (flag2)
				{
					Utils.LogErrorEvent(identity, label, ex, options, logger, isPeriodic, periodicKey, periodicDuration, context);
				}
				if (!isBestEffort)
				{
					throw;
				}
			}
			finally
			{
				if (ex == null)
				{
					ExTraceGlobals.RunOperationTracer.TraceDebug<string, string>((long)identity.GetHashCode(), "{0}: Operation {1} successful", identity, label);
					if (flag3)
					{
						Utils.LogFinishedEvent(identity, label, logger, isPeriodic, periodicKey, periodicDuration, context);
					}
				}
				if (exitAction != null)
				{
					exitAction(ex);
				}
			}
			return ex;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00012708 File Offset: 0x00010908
		public static void LogErrorEvent(string identity, string label, Exception exception, LogOptions logOptions, IDxStoreEventLogger logger, bool isPeriodic, string periodicKey, TimeSpan? periodicDuration, string context)
		{
			if (logger != null)
			{
				string name = exception.GetType().Name;
				string message = exception.Message;
				string text = string.Empty;
				if ((logOptions & LogOptions.LogExceptionFull) == LogOptions.LogExceptionFull)
				{
					text = exception.ToString();
				}
				logger.Log((isPeriodic && periodicDuration != null) ? (periodicKey + "Exception") : null, periodicDuration, DxEventSeverity.Error, 0, "{0}: Operation {1} ({2}) failed. (Category: {3}, Message: {4}, Full: {5})", new object[]
				{
					identity,
					label,
					context ?? string.Empty,
					name,
					message,
					text
				});
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000127A8 File Offset: 0x000109A8
		public static void LogStartingEvent(string identity, string label, IDxStoreEventLogger logger, bool isPeriodic, string periodicKey, TimeSpan? periodicDuration, string context)
		{
			if (logger != null)
			{
				logger.Log((isPeriodic && periodicDuration != null) ? (periodicKey + "Starting") : null, periodicDuration, DxEventSeverity.Info, 0, "{0}: Starting operation {1} ({2})", new object[]
				{
					identity,
					label,
					context ?? string.Empty
				});
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00012808 File Offset: 0x00010A08
		public static void LogFinishedEvent(string identity, string label, IDxStoreEventLogger logger, bool isPeriodic, string periodicKey, TimeSpan? periodicDuration, string context)
		{
			if (logger != null)
			{
				logger.Log((isPeriodic && periodicDuration != null) ? (periodicKey + "Finished") : null, periodicDuration, DxEventSeverity.Info, 0, "{0}: Operation {1} ({2}) successfuly completed", new object[]
				{
					identity,
					label,
					context ?? string.Empty
				});
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00012868 File Offset: 0x00010A68
		public static void RunWithTimeoutToken(TimeSpan timeout, Func<CancellationToken, Task> asyncOperation)
		{
			using (Concurrency.TimeoutToken timeoutToken = new Concurrency.TimeoutToken(timeout))
			{
				CancellationToken token = timeoutToken.Token;
				using (Task task = asyncOperation(token))
				{
					task.Wait(token);
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000128C8 File Offset: 0x00010AC8
		public static Process GetMatchingProcess(int pid, DateTimeOffset startTime, bool isBestEffort = true)
		{
			try
			{
				Process processById = Process.GetProcessById(pid);
				if (processById.StartTime == startTime)
				{
					return processById;
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ArgumentException))
				{
					Utils.Tracer.TraceError<int, DateTimeOffset, Exception>(0L, "GetMatchingProcess(pid: {0}, startTime: {1}) failed with {2}", pid, startTime, ex);
					if (!isBestEffort)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001292C File Offset: 0x00010B2C
		public static Exception RunBestEffort(Action action)
		{
			Exception result = null;
			try
			{
				action();
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001295C File Offset: 0x00010B5C
		public static T[] EmptyArray<T>()
		{
			return new T[0];
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00012964 File Offset: 0x00010B64
		public static string CombinePathNullSafe(string path1, string path2)
		{
			path1 = (path1 ?? string.Empty);
			path2 = (path2 ?? string.Empty);
			return Path.Combine(path1, path2);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00012988 File Offset: 0x00010B88
		public static string SerializeObjectToJsonString<T>(T obj, bool isPrettyFormat = false, bool isBestEffort = true)
		{
			try
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
				using (MemoryStream memoryStream = new MemoryStream())
				{
					dataContractJsonSerializer.WriteObject(memoryStream, obj);
					string text = Encoding.Default.GetString(memoryStream.ToArray());
					if (isPrettyFormat)
					{
						text = Utils.FormatJson(text, 3);
					}
					return text;
				}
			}
			catch
			{
				if (!isBestEffort)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static string FormatJson(string str, int indentSize = 3)
		{
			int num = 0;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < str.Length)
			{
				char c = str[i];
				char c2 = c;
				if (c2 <= ',')
				{
					if (c2 != '"')
					{
						if (c2 != ',')
						{
							goto IL_130;
						}
						stringBuilder.Append(c);
						if (!flag)
						{
							stringBuilder.AppendLine();
							stringBuilder.AppendSpaces(num * indentSize);
						}
					}
					else
					{
						stringBuilder.Append(c);
						bool flag2 = false;
						int num2 = i;
						while (num2 > 0 && str[--num2] == '\\')
						{
							flag2 = !flag2;
						}
						if (!flag2)
						{
							flag = !flag;
						}
					}
				}
				else if (c2 != ':')
				{
					switch (c2)
					{
					case '[':
						break;
					case '\\':
						goto IL_130;
					case ']':
						goto IL_9D;
					default:
						switch (c2)
						{
						case '{':
							break;
						case '|':
							goto IL_130;
						case '}':
							goto IL_9D;
						default:
							goto IL_130;
						}
						break;
					}
					stringBuilder.Append(c);
					if (!flag)
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendSpaces(++num * indentSize);
						goto IL_139;
					}
					goto IL_139;
					IL_9D:
					if (!flag)
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendSpaces(--num * indentSize);
					}
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(c);
					if (!flag)
					{
						stringBuilder.Append(" ");
					}
				}
				IL_139:
				i++;
				continue;
				IL_130:
				stringBuilder.Append(c);
				goto IL_139;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00012B68 File Offset: 0x00010D68
		public static bool IsEqual(string a, string b, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
		{
			return string.Equals(a, b, comparison);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00012B74 File Offset: 0x00010D74
		public static Tuple<T[], T[], T[]> DiffArrays<T>(T[] left, T[] right)
		{
			left = (left ?? Utils.EmptyArray<T>());
			right = (right ?? Utils.EmptyArray<T>());
			T[] array = left.Intersect(right).ToArray<T>();
			T[] item = right.Except(array).ToArray<T>();
			T[] item2 = left.Except(array).ToArray<T>();
			return new Tuple<T[], T[], T[]>(array, item, item2);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00012BE0 File Offset: 0x00010DE0
		public static void ExitProcessDelayed(TimeSpan delay, int exitCode = 0)
		{
			if (delay == TimeSpan.Zero)
			{
				Utils.TerminateSelf(exitCode);
				return;
			}
			TaskFactoryExtensions.StartNewDelayed(Task.Factory, (int)delay.TotalMilliseconds, delegate()
			{
				Utils.TerminateSelf(exitCode);
			});
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012C39 File Offset: 0x00010E39
		private static void TerminateSelf(int exitCode)
		{
			DiagnosticsNativeMethods.TerminateProcess(Process.GetCurrentProcess().Handle, exitCode);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00012D80 File Offset: 0x00010F80
		private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
		{
			yield return source.Current;
			int i = 0;
			while (i < batchSize && source.MoveNext())
			{
				yield return source.Current;
				i++;
			}
			yield break;
		}

		// Token: 0x04000259 RID: 601
		private static object locker = new object();

		// Token: 0x0400025A RID: 602
		private static ProcessBasicInfo currentProcessInfo;
	}
}
