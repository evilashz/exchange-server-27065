using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000BC RID: 188
	internal sealed class RpcHangDetector : HangDetector
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0001B64D File Offset: 0x0001984D
		private RpcHangDetector()
		{
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001B655 File Offset: 0x00019855
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001B65D File Offset: 0x0001985D
		private string CallStack { get; set; }

		// Token: 0x06000594 RID: 1428 RVA: 0x0001B668 File Offset: 0x00019868
		public static RpcHangDetector Create()
		{
			return new RpcHangDetector
			{
				Timeout = Configuration.HangDetectionTimeout,
				Period = Configuration.HangDetectionPeriod
			};
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001B694 File Offset: 0x00019894
		protected override void OnHangDetected(int hangsDetected)
		{
			if (hangsDetected == 0)
			{
				try
				{
					base.MonitoredThread.Suspend();
					try
					{
						this.CallStack = new StackTrace(base.MonitoredThread, true).ToString();
					}
					finally
					{
						base.MonitoredThread.Resume();
					}
				}
				catch
				{
					this.CallStack = "Unknown";
				}
				SingletonEventLogger.Logger.LogEvent(AssistantsEventLogConstants.Tuple_ShutdownAssistantsThreadHanging, null, new object[]
				{
					base.DatabaseName,
					base.AssistantName,
					base.InvokeTime.ToLocalTime(),
					this.CallStack
				});
				return;
			}
			SingletonEventLogger.Logger.LogEvent(AssistantsEventLogConstants.Tuple_ShutdownAssistantsThreadHangPersisted, null, new object[]
			{
				base.DatabaseName,
				base.AssistantName,
				base.InvokeTime.ToLocalTime(),
				this.CallStack
			});
			string text = string.Format("Hung detected for assistant {0} on database {1}. Shutting down service by calling Process.Kill.", base.AssistantName, base.DatabaseName);
			StackTrace stackTrace = new StackTrace(1);
			MethodBase method = stackTrace.GetFrame(0).GetMethod();
			AssemblyName name = method.DeclaringType.Assembly.GetName();
			int hashCode = (method.Name + text + base.AssistantName).GetHashCode();
			Thread.Sleep(TimeSpan.FromSeconds(10.0));
			ExWatson.SendGenericWatsonReport("E12", ExWatson.RealApplicationVersion.ToString(), ExWatson.RealAppName, name.Version.ToString(), name.Name, text, this.CallStack, hashCode.ToString("x"), method.Name, this.GetCrashingMessage());
			Process.GetCurrentProcess().Kill();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001B860 File Offset: 0x00019A60
		private string GetCrashingMessage()
		{
			StringBuilder stringBuilder = new StringBuilder("Assistant ");
			stringBuilder.Append(base.AssistantName ?? "Unknown");
			stringBuilder.Append(" timed out during shutdown\r\n");
			stringBuilder.Append(AIBreadcrumbs.Instance.GenerateReport());
			return stringBuilder.ToString();
		}
	}
}
