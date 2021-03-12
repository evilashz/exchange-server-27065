using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C1 RID: 449
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class MrsConnectionsLog : ILog
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x00027698 File Offset: 0x00025898
		public bool IsEnabled(LogLevel level)
		{
			if (level <= LogLevel.LogInfo)
			{
				switch (level)
				{
				case LogLevel.LogDebug:
					return MrsTracer.Provider.IsEnabled(TraceType.DebugTrace);
				case LogLevel.LogVerbose | LogLevel.LogDebug:
					break;
				case LogLevel.LogTrace:
					return MrsTracer.Provider.IsEnabled(TraceType.FunctionTrace);
				default:
					if (level == LogLevel.LogInfo)
					{
						return MrsTracer.Provider.IsEnabled(TraceType.DebugTrace);
					}
					break;
				}
			}
			else
			{
				if (level == LogLevel.LogWarn)
				{
					return MrsTracer.Provider.IsEnabled(TraceType.WarningTrace);
				}
				if (level == LogLevel.LogError)
				{
					return MrsTracer.Provider.IsEnabled(TraceType.ErrorTrace);
				}
			}
			return false;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0002770E File Offset: 0x0002590E
		public void Trace(string formatString, params object[] args)
		{
			MrsTracer.Provider.Function(string.Format(formatString, args), new object[0]);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00027727 File Offset: 0x00025927
		public void Debug(string formatString, params object[] args)
		{
			MrsTracer.Provider.Debug(formatString, args);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00027735 File Offset: 0x00025935
		public void Info(string formatString, params object[] args)
		{
			MrsTracer.Provider.Debug(formatString, args);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00027743 File Offset: 0x00025943
		public void Warn(string formatString, params object[] args)
		{
			MrsTracer.Provider.Warning(formatString, args);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00027751 File Offset: 0x00025951
		public void Error(string formatString, params object[] args)
		{
			MrsTracer.Provider.Error(formatString, args);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0002775F File Offset: 0x0002595F
		public void Fatal(string formatString, params object[] args)
		{
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00027761 File Offset: 0x00025961
		public void Fatal(Exception exception, string message = null)
		{
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00027763 File Offset: 0x00025963
		public void Assert(bool condition, string formatString, params object[] args)
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00027765 File Offset: 0x00025965
		public void RetailAssert(bool condition, string formatString, params object[] args)
		{
			ExAssert.RetailAssert(condition, formatString);
		}
	}
}
