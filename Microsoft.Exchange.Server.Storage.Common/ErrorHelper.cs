using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000032 RID: 50
	public static class ErrorHelper
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000B6D0 File Offset: 0x000098D0
		private static Queue<Breadcrumb> Breadcrumbs
		{
			get
			{
				if (ErrorHelper.breadcrumbs == null)
				{
					ErrorHelper.breadcrumbs = new Queue<Breadcrumb>(ConfigurationSchema.MaxBreadcrumbs.Value);
				}
				return ErrorHelper.breadcrumbs;
			}
		}

		// Token: 0x0600040E RID: 1038
		[DllImport("kernel32.dll")]
		public static extern void OutputDebugString(string str);

		// Token: 0x0600040F RID: 1039
		[DllImport("kernel32.dll")]
		private static extern bool DebugBreak();

		// Token: 0x06000410 RID: 1040 RVA: 0x0000B6F2 File Offset: 0x000098F2
		internal static void Initialize(Guid? databaseGuid)
		{
			if (databaseGuid != null)
			{
				ErrorHelper.InitializeCrashOnLID(databaseGuid.Value);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B744 File Offset: 0x00009944
		internal static void InitializeCrashOnLID(Guid databaseGuid)
		{
			string crashOnLIDKeyName = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}", Environment.MachineName, databaseGuid);
			int crashOnLIDValue = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, crashOnLIDKeyName, "CrashOnLID", 0);
			if (crashOnLIDValue != 0)
			{
				DiagnosticContext.SetOnLIDCallback(delegate(LID lid)
				{
					if (lid.Value == (uint)crashOnLIDValue)
					{
						RegistryWriter.Instance.DeleteValue(Registry.LocalMachine, crashOnLIDKeyName, "CrashOnLID");
						throw new CrashOnLIDException(lid);
					}
				});
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000B7B4 File Offset: 0x000099B4
		private static ErrorHelper.AttachedDebuggerType GetAttachedDebuggerType()
		{
			ErrorHelper.AttachedDebuggerType result = ErrorHelper.AttachedDebuggerType.None;
			if (Debugger.IsAttached)
			{
				result = ErrorHelper.AttachedDebuggerType.Managed;
			}
			else if (ErrorHelper.IsDebuggerPresent())
			{
				result = ErrorHelper.AttachedDebuggerType.Native;
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043
		[DllImport("kernel32.dll")]
		private static extern bool IsDebuggerPresent();

		// Token: 0x06000414 RID: 1044 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public static void ClearBreadcrumbsForTest()
		{
			using (LockManager.Lock(ErrorHelper.Breadcrumbs, LockManager.LockType.Breadcrumbs))
			{
				ErrorHelper.Breadcrumbs.Clear();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B81C File Offset: 0x00009A1C
		public static IReadOnlyList<Breadcrumb> GetBreadcrumbsHistorySnapshot()
		{
			IReadOnlyList<Breadcrumb> result;
			using (LockManager.Lock(ErrorHelper.Breadcrumbs, LockManager.LockType.Breadcrumbs))
			{
				result = new List<Breadcrumb>(ErrorHelper.Breadcrumbs);
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B864 File Offset: 0x00009A64
		public static void AddBreadcrumb(BreadcrumbKind kind, byte operationSource, byte operationNumber, byte clientType, int databaseNumber, int mailboxNumber, int associatedValue, object associatedObject)
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.BreadCrumbs);
			if (associatedObject == null)
			{
				int size = DiagnosticContext.Size;
				byte[] array = new byte[6 + size];
				int num = 6;
				DiagnosticContext.PackInfo(array, ref num, size);
				ParseSerialize.SerializeInt32(size, array, 2);
				associatedObject = array;
			}
			using (LockManager.Lock(ErrorHelper.Breadcrumbs, LockManager.LockType.Breadcrumbs))
			{
				Breadcrumb breadcrumb;
				if (ErrorHelper.Breadcrumbs.Count == ConfigurationSchema.MaxBreadcrumbs.Value)
				{
					breadcrumb = ErrorHelper.Breadcrumbs.Dequeue();
				}
				else
				{
					breadcrumb = new Breadcrumb();
				}
				breadcrumb.Kind = kind;
				breadcrumb.Source = operationSource;
				breadcrumb.Operation = operationNumber;
				breadcrumb.Client = clientType;
				breadcrumb.Mailbox = mailboxNumber;
				breadcrumb.Database = databaseNumber;
				breadcrumb.Time = DateTime.UtcNow;
				breadcrumb.DataValue = associatedValue;
				breadcrumb.DataObject = associatedObject;
				ErrorHelper.Breadcrumbs.Enqueue(breadcrumb);
			}
			if (logger != null && logger.IsLoggingEnabled)
			{
				string strValue = string.Empty;
				string empty = string.Empty;
				string empty2 = string.Empty;
				Exception ex = associatedObject as Exception;
				if (ex == null)
				{
					Tuple<Exception, object> tuple = associatedObject as Tuple<Exception, object>;
					if (tuple != null)
					{
						ex = tuple.Item1;
					}
				}
				if (associatedObject is byte[])
				{
					strValue = "0x" + BitConverter.ToString((byte[])associatedObject).Replace("-", string.Empty);
				}
				else if (ex != null)
				{
					ErrorHelper.GetExceptionSummary(ex, out strValue, out empty, out empty2);
				}
				else if (associatedObject != null)
				{
					strValue = associatedObject.ToString();
				}
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.BreadCrumbs, true, false, (byte)kind, operationSource, operationNumber, clientType, databaseNumber, mailboxNumber, associatedValue, strValue, empty, empty2))
				{
					logger.TryWrite(traceBuffer);
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000BA38 File Offset: 0x00009C38
		public static bool ShouldSkipBreadcrumb(byte operationSource, byte operationNumber, ErrorCodeValue error, uint lid)
		{
			if (lid <= 46439U)
			{
				if (lid != 33639U && lid != 46439U)
				{
					goto IL_2E;
				}
			}
			else if (lid != 54361U && lid != 62152U)
			{
				goto IL_2E;
			}
			return true;
			IL_2E:
			if (operationSource == 0)
			{
				if (error == ErrorCodeValue.NullObject)
				{
					return true;
				}
				if (operationNumber <= 68)
				{
					if (operationNumber <= 21)
					{
						switch (operationNumber)
						{
						case 0:
							return error == ErrorCodeValue.MdbNotInitialized;
						case 1:
							return false;
						case 2:
							return error == ErrorCodeValue.NoAccess || error == ErrorCodeValue.NotFound;
						case 3:
							return error == ErrorCodeValue.NoAccess || error == ErrorCodeValue.NotFound;
						default:
							if (operationNumber == 12)
							{
								return error == ErrorCodeValue.ObjectChanged;
							}
							switch (operationNumber)
							{
							case 20:
								return error == ErrorCodeValue.NotSupported;
							case 21:
								return error == ErrorCodeValue.NotSupported;
							default:
								return false;
							}
							break;
						}
					}
					else
					{
						if (operationNumber == 43)
						{
							return error == ErrorCodeValue.NotFound || error == ErrorCodeValue.NotSupported;
						}
						switch (operationNumber)
						{
						case 49:
							return error == ErrorCodeValue.NotInitialized;
						case 50:
							return false;
						case 51:
							break;
						default:
							switch (operationNumber)
							{
							case 67:
								return error == ErrorCodeValue.NotFound;
							case 68:
								return error == ErrorCodeValue.InvalidParameter;
							default:
								return false;
							}
							break;
						}
					}
				}
				else if (operationNumber <= 115)
				{
					if (operationNumber == 79)
					{
						return error == ErrorCodeValue.NotFound;
					}
					switch (operationNumber)
					{
					case 97:
						return error == ErrorCodeValue.NotFound;
					case 98:
						return false;
					case 99:
						return error == ErrorCodeValue.NoReplicaHere;
					default:
						switch (operationNumber)
						{
						case 114:
							return error == ErrorCodeValue.SyncIgnore || error == ErrorCodeValue.SyncObjectDeleted;
						case 115:
							return error == ErrorCodeValue.SyncIgnore;
						default:
							return false;
						}
						break;
					}
				}
				else
				{
					if (operationNumber == 120)
					{
						return error == ErrorCodeValue.SyncClientChangeNewer || error == ErrorCodeValue.NotFound || error == ErrorCodeValue.SyncObjectDeleted;
					}
					switch (operationNumber)
					{
					case 156:
						return error == ErrorCodeValue.NoAccess;
					case 157:
						return error == ErrorCodeValue.ShutoffQuotaExceeded;
					case 158:
						return error == ErrorCodeValue.DuplicateDelivery;
					case 159:
						return false;
					case 160:
						break;
					case 161:
						return error == ErrorCodeValue.DuplicateDelivery;
					default:
						if (operationNumber != 254)
						{
							return false;
						}
						return error == ErrorCodeValue.ADPropertyError || error == ErrorCodeValue.AdUnavailable || error == ErrorCodeValue.DisabledMailbox || error == ErrorCodeValue.MailboxInTransit || error == ErrorCodeValue.MailboxQuarantined || error == ErrorCodeValue.MaxObjectsExceeded || error == ErrorCodeValue.WrongServer || error == ErrorCodeValue.LogonFailed || error == ErrorCodeValue.UnknownUser || error == ErrorCodeValue.MdbNotInitialized;
					}
				}
				return error == ErrorCodeValue.ShutoffQuotaExceeded;
			}
			else if (operationSource == 1)
			{
				if (operationNumber == 34)
				{
					return error == ErrorCodeValue.PartialCompletion;
				}
				if (operationNumber == 48)
				{
					return error == ErrorCodeValue.MdbNotInitialized;
				}
			}
			return false;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		public static void OnExceptionCatch(byte operationSource, byte operationNumber, byte clientType, int databaseNumber, int mailboxNumber, Exception exception, object diagnosticData)
		{
			if (exception != null)
			{
				if (ExTraceGlobals.ExceptionHandlerTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ExceptionHandlerTracer.TraceError<string, string>(0, 0L, "Exception: {0} Text: {1}", exception.GetType().Name, exception.ToString());
				}
				int associatedValue = 0;
				if (exception is StoreException)
				{
					StoreException ex = (StoreException)exception;
					if (ErrorHelper.ShouldSkipBreadcrumb(operationSource, operationNumber, ex.Error, ex.Lid))
					{
						return;
					}
					associatedValue = (int)ex.Error;
				}
				object associatedObject;
				if (diagnosticData == null)
				{
					associatedObject = exception;
				}
				else
				{
					associatedObject = new Tuple<Exception, object>(exception, diagnosticData);
				}
				ErrorHelper.AddBreadcrumb(BreadcrumbKind.Exception, operationSource, operationNumber, clientType, databaseNumber, mailboxNumber, associatedValue, associatedObject);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public static void TraceException(Microsoft.Exchange.Diagnostics.Trace tracer, LID lid, Exception exception)
		{
			StoreException ex = exception as StoreException;
			if (ex != null)
			{
				DiagnosticContext.TraceStoreError(lid, (uint)ex.Error);
			}
			else
			{
				RopExecutionException ex2 = exception as RopExecutionException;
				if (ex2 != null)
				{
					DiagnosticContext.TraceStoreError(lid, (uint)ex2.ErrorCode);
				}
				else
				{
					DiagnosticContext.TraceStoreError(lid, 5000U);
				}
			}
			if (!ExTraceGlobals.ExceptionHandlerTracer.IsTraceEnabled(TraceType.ErrorTrace) && tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				tracer.TraceError<string, string>((int)lid.Value, 0L, "Exception: {0} Text: {1}", exception.GetType().Name, exception.ToString());
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		[Conditional("DEBUG")]
		public static void Assert(bool assertCondition, string message)
		{
			if (!assertCondition && !ErrorHelper.disableDebugAsserts)
			{
				ErrorHelper.OutputDebugString(message);
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_InternalLogicError, new object[]
				{
					message,
					new StackTrace(true).ToString()
				});
				ErrorHelper.CheckForDebugger();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000BE38 File Offset: 0x0000A038
		public static void AssertRetail(bool assertCondition, string message)
		{
			if (!assertCondition)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_InternalLogicError, new object[]
				{
					message,
					new StackTrace(true).ToString()
				});
				if (ErrorHelper.CheckForDebugger())
				{
					WatsonOnUnhandledException.KillCurrentProcess();
					return;
				}
				ExAssert.RetailAssert(false, message);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000BE80 File Offset: 0x0000A080
		internal static bool CheckForDebugger()
		{
			bool result = false;
			switch (ErrorHelper.GetAttachedDebuggerType())
			{
			case ErrorHelper.AttachedDebuggerType.None:
				result = false;
				break;
			case ErrorHelper.AttachedDebuggerType.Managed:
				result = true;
				ErrorHelper.OutputDebugString("Forcing managed DebugBreak...");
				Debugger.Break();
				break;
			case ErrorHelper.AttachedDebuggerType.Native:
				result = true;
				ErrorHelper.OutputDebugString("Forcing unmanaged DebugBreak...");
				ErrorHelper.DebugBreak();
				break;
			default:
				Globals.AssertRetail(false, "Unexpected debugger type.");
				break;
			}
			return result;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		internal static bool IsDebuggerAttached()
		{
			bool result = false;
			switch (ErrorHelper.GetAttachedDebuggerType())
			{
			case ErrorHelper.AttachedDebuggerType.None:
				result = false;
				break;
			case ErrorHelper.AttachedDebuggerType.Managed:
			case ErrorHelper.AttachedDebuggerType.Native:
				result = true;
				break;
			default:
				Globals.AssertRetail(false, "Unexpected debugger type.");
				break;
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000BF23 File Offset: 0x0000A123
		internal static IDisposable DisableDebugAsserts()
		{
			return new ErrorHelper.DisableDebugAssertsFrame();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000BF2C File Offset: 0x0000A12C
		internal static void GetExceptionSummary(Exception e, out string exceptionType, out string exceptionMessage, out string exceptionStack)
		{
			exceptionType = string.Empty;
			exceptionMessage = string.Empty;
			exceptionStack = string.Empty;
			if (e != null)
			{
				exceptionType = e.GetType().Name;
				if (e.Message != null && e.Message.Length > 0)
				{
					string text = ErrorHelper.whiteSpaceRegex.Replace(e.Message, " ");
					exceptionMessage = text.Substring(0, Math.Min(256, text.Length));
				}
				if (e.StackTrace != null && e.StackTrace.Length > 0)
				{
					exceptionStack = ErrorHelper.GetCondensedCallStack(e.StackTrace);
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		internal static string GetCondensedCallStack(string stackTrace)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (stackTrace != null && stackTrace.Length > 0)
			{
				foreach (object obj in ErrorHelper.thrownFromRegex.Matches(stackTrace))
				{
					Match match = (Match)obj;
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					if (match.Groups.Count == 2)
					{
						int num = match.Groups[1].Captures.Count - 2;
						for (int i = 0; i < match.Groups[1].Captures.Count; i++)
						{
							if (i >= num)
							{
								if (i > 0)
								{
									stringBuilder.Append(".");
								}
								stringBuilder.Append(match.Groups[1].Captures[i].Value);
							}
							else
							{
								string value = match.Groups[1].Captures[i].Value;
								if (value != null && value.Length > 0)
								{
									foreach (object obj2 in ErrorHelper.upperRegex.Matches(value))
									{
										Match match2 = (Match)obj2;
										stringBuilder.Append(match2.Value);
									}
								}
							}
						}
					}
					if (stringBuilder.Length > 256)
					{
						break;
					}
				}
			}
			return stringBuilder.ToString().Substring(0, Math.Min(256, stringBuilder.Length));
		}

		// Token: 0x040004B1 RID: 1201
		private const string KeyNameFormatCrashOnLIDRoot = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\Private-{1}";

		// Token: 0x040004B2 RID: 1202
		private const string ValueNameCrashOnLID = "CrashOnLID";

		// Token: 0x040004B3 RID: 1203
		private static bool disableDebugAsserts = false;

		// Token: 0x040004B4 RID: 1204
		private static Queue<Breadcrumb> breadcrumbs;

		// Token: 0x040004B5 RID: 1205
		private static Regex thrownFromRegex = new Regex("at (?:([A-Z][A-Za-z]+)[\\.\\(])+");

		// Token: 0x040004B6 RID: 1206
		private static Regex upperRegex = new Regex("[A-Z]");

		// Token: 0x040004B7 RID: 1207
		private static Regex whiteSpaceRegex = new Regex("\\s+");

		// Token: 0x02000033 RID: 51
		private enum AttachedDebuggerType
		{
			// Token: 0x040004B9 RID: 1209
			None,
			// Token: 0x040004BA RID: 1210
			Managed,
			// Token: 0x040004BB RID: 1211
			Native
		}

		// Token: 0x02000034 RID: 52
		private class DisableDebugAssertsFrame : IDisposable
		{
			// Token: 0x06000422 RID: 1058 RVA: 0x0000C1E9 File Offset: 0x0000A3E9
			public DisableDebugAssertsFrame()
			{
				ErrorHelper.disableDebugAsserts = true;
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000C1F7 File Offset: 0x0000A3F7
			public void Dispose()
			{
				ErrorHelper.disableDebugAsserts = false;
			}
		}
	}
}
