using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200001E RID: 30
	internal sealed class AssistantsRpcServer : AssistantsRpcServerBase
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00005420 File Offset: 0x00003620
		public static void StartServer(SecurityIdentifier exchangeServersSid)
		{
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			FileSystemAccessRule rule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
			FileSystemAccessRule rule2 = new FileSystemAccessRule(exchangeServersSid, FileSystemRights.ReadData, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			fileSecurity.AddAccessRule(rule);
			fileSecurity.AddAccessRule(rule2);
			RpcServerBase.RegisterServer(typeof(AssistantsRpcServer), fileSecurity, 1);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005498 File Offset: 0x00003698
		public static void RegisterAssistant(string assistantName, TimeBasedAssistantController controller)
		{
			AssistantsRpcServer.Tracer.TraceDebug<string>(0L, "Assistant {0} registered", assistantName);
			lock (AssistantsRpcServer.timeBasedAssistants)
			{
				AssistantsRpcServer.timeBasedAssistants.Add(assistantName, controller);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000054F0 File Offset: 0x000036F0
		public static void DeregisterAssistant(string assistantName)
		{
			AssistantsRpcServer.Tracer.TraceDebug<string>(0L, "Assistant {0} deregistered", assistantName);
			lock (AssistantsRpcServer.timeBasedAssistants)
			{
				AssistantsRpcServer.timeBasedAssistants.Remove(assistantName);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005548 File Offset: 0x00003748
		public override void RunNow(string assistantName, ValueType mailboxGuid, ValueType mdbGuid)
		{
			ExAssert.RetailAssert(false, "RunNow must not be invoked. Use RunNowHR instead.");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005555 File Offset: 0x00003755
		public override void Halt(string assistantName)
		{
			ExAssert.RetailAssert(false, "Halt must not be invoked. Use HaltHR instead.");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005562 File Offset: 0x00003762
		public override int RunNowHR(string assistantName, ValueType mailboxGuid, ValueType mdbGuid)
		{
			AssistantsRpcServer.Tracer.TraceDebug<string, ValueType, ValueType>((long)this.GetHashCode(), "RunNowHR requested for assistant={0}, mailbox={1}, database={2}", assistantName, mailboxGuid, mdbGuid);
			return this.RunNowWithParamsHR(assistantName, mailboxGuid, mdbGuid, null);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000055CC File Offset: 0x000037CC
		public override int RunNowWithParamsHR(string assistantName, ValueType mailboxGuid, ValueType mdbGuid, string parameters)
		{
			AssistantsRpcServer.Tracer.TraceDebug((long)this.GetHashCode(), "RunNowWithParamsHR requested for assistant={0}, mailbox={1}, database={2}, parameters={3}", new object[]
			{
				assistantName,
				mailboxGuid,
				mdbGuid,
				string.IsNullOrEmpty(parameters) ? "<null>" : parameters
			});
			return AssistantsRpcServer.Execute(delegate
			{
				TimeBasedAssistantController controller = AssistantsRpcServer.GetController(assistantName);
				controller.RunNow((Guid)mailboxGuid, (Guid)mdbGuid, parameters);
			}, assistantName);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005694 File Offset: 0x00003894
		public override int HaltHR(string assistantName)
		{
			AssistantsRpcServer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "HaltHR requested for assistant={0}", assistantName);
			return AssistantsRpcServer.Execute(delegate
			{
				TimeBasedAssistantController controller = AssistantsRpcServer.GetController(assistantName);
				controller.Halt();
			}, assistantName);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000056E4 File Offset: 0x000038E4
		private static TimeBasedAssistantController GetController(string assistantName)
		{
			TimeBasedAssistantController timeBasedAssistantController;
			AssistantsRpcServer.timeBasedAssistants.TryGetValue(assistantName, out timeBasedAssistantController);
			if (timeBasedAssistantController == null)
			{
				AssistantsRpcServer.Tracer.TraceError<string>(0L, "Assistant {0} unknown", assistantName);
				throw new UnknownAssistantException(assistantName);
			}
			return timeBasedAssistantController;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000057CC File Offset: 0x000039CC
		private static int Execute(GrayException.UserCodeDelegate function, string assistantName)
		{
			Exception exception = null;
			AssistantsRpcServer.Tracer.TraceDebug<string>(0L, "Executing the RPC request for assistant {0}.", assistantName);
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						function();
					}
					catch (MapiExceptionMdbOffline exception3)
					{
						exception = exception3;
					}
					catch (MapiExceptionNotFound exception4)
					{
						exception = exception4;
					}
					catch (MailboxOrDatabaseNotSpecifiedException exception5)
					{
						exception = exception5;
					}
					catch (UnknownAssistantException exception6)
					{
						exception = exception6;
					}
					catch (UnknownDatabaseException exception7)
					{
						exception = exception7;
					}
					catch (TransientException exception8)
					{
						exception = exception8;
					}
				});
			}
			catch (GrayException exception)
			{
				GrayException exception9;
				exception = exception9;
			}
			catch (Exception exception2)
			{
				exception = exception2;
				ExWatson.SendReportAndCrashOnAnotherThread(exception2);
			}
			if (exception != null)
			{
				return AssistantsRpcServer.LogExceptionAndGetHR(exception, assistantName);
			}
			return 0;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005868 File Offset: 0x00003A68
		private static int LogExceptionAndGetHR(Exception ex, string assistantName)
		{
			AssistantsRpcServer.Tracer.TraceError<string, Exception>(0L, "LogExceptionAndGetHR: RPC request failed. for assistant={0}, exception={1}", assistantName, ex);
			SingletonEventLogger.Logger.LogEvent(AssistantsEventLogConstants.Tuple_RpcError, null, new object[]
			{
				"Execute",
				ex
			});
			return AssistantsRpcErrorCode.GetHRFromException(ex);
		}

		// Token: 0x040000EF RID: 239
		private static readonly Trace Tracer = ExTraceGlobals.AssistantsRpcServerTracer;

		// Token: 0x040000F0 RID: 240
		private static Dictionary<string, TimeBasedAssistantController> timeBasedAssistants = new Dictionary<string, TimeBasedAssistantController>();
	}
}
