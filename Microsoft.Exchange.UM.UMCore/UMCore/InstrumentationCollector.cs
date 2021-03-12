using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Win32;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200015E RID: 350
	internal static class InstrumentationCollector
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002BC25 File Offset: 0x00029E25
		internal static InstrumentationBaseStrategy CurrentStrategy
		{
			get
			{
				return InstrumentationCollector.collectionStrategy;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002BC2C File Offset: 0x00029E2C
		internal static bool Start(InstrumentationBaseStrategy strategy)
		{
			ValidateArgument.NotNull(strategy, "strategy");
			bool result = false;
			try
			{
				InstrumentationCollector.TraceDebug("In InstrumentationCollector: Start", new object[0]);
				if (InstrumentationCollector.IsDartMachine())
				{
					lock (InstrumentationCollector.synclock)
					{
						if (InstrumentationCollector.workerThread == null)
						{
							InstrumentationCollector.collectionStrategy = strategy;
							InstrumentationCollector.workerThread = new Thread(new ParameterizedThreadStart(InstrumentationCollector.Run));
							InstrumentationCollector.workerThread.IsBackground = true;
							InstrumentationCollector.workerThread.Start();
							InstrumentationCollector.TraceDebug("InstrumentationCollector: Successfully Started.", new object[0]);
							result = true;
						}
						goto IL_9A;
					}
				}
				InstrumentationCollector.TraceDebug("InstrumentationCollector: Not a dart box.", new object[0]);
				IL_9A:;
			}
			catch (Exception ex)
			{
				InstrumentationCollector.TraceDebug("InstrumentationCollector: Start encountered error={0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002BD10 File Offset: 0x00029F10
		internal static bool Stop()
		{
			bool result = false;
			lock (InstrumentationCollector.synclock)
			{
				try
				{
					if (InstrumentationCollector.workerThread != null)
					{
						InstrumentationCollector.workerThread.Abort();
						InstrumentationCollector.TraceDebug("InstrumentationCollector: Successfully Stopped ", new object[0]);
						result = true;
					}
				}
				catch (Exception ex)
				{
					InstrumentationCollector.TraceDebug("InstrumentationCollector: Stop encountered error={0}", new object[]
					{
						ex
					});
				}
				finally
				{
					InstrumentationCollector.workerThread = null;
					InstrumentationCollector.collectionStrategy = null;
				}
			}
			return result;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002BDB4 File Offset: 0x00029FB4
		private static void Run(object state)
		{
			try
			{
				InstrumentationCollector.collectionStrategy.Initialize();
				StringBuilder stringBuilder = new StringBuilder(150);
				for (;;)
				{
					Thread.Sleep(InstrumentationCollector.Interval);
					stringBuilder.Length = 0;
					stringBuilder.AppendFormat("{0},", ExDateTime.UtcNow.ToString("T"));
					InstrumentationCollector.collectionStrategy.CollectData(stringBuilder);
					stringBuilder.Length--;
					InstrumentationCollector.TraceDebug(stringBuilder.ToString(), new object[0]);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				InstrumentationCollector.TraceDebug("InstrumentationCollector: Run encountered error={0}. Stopping the thread", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002BE6C File Offset: 0x0002A06C
		private static bool IsDartMachine()
		{
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\DART\\ClientRecovery"))
			{
				result = (registryKey != null && registryKey.GetValue("Alias") != null);
			}
			return result;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		private static void TraceDebug(string message, params object[] args)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, message, args);
		}

		// Token: 0x04000955 RID: 2389
		private static readonly TimeSpan Interval = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000956 RID: 2390
		private static object synclock = new object();

		// Token: 0x04000957 RID: 2391
		private static Thread workerThread;

		// Token: 0x04000958 RID: 2392
		private static InstrumentationBaseStrategy collectionStrategy;
	}
}
