using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000011 RID: 17
	internal class NullLogTraceHelper : ILogTraceHelper
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000356E File Offset: 0x0000176E
		public static ILogTraceHelper GetNullLogger()
		{
			return NullLogTraceHelper.s_nullLogger;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003578 File Offset: 0x00001778
		public void AppendLogMessage(LocalizedString locMessage)
		{
			string arg = DateTime.UtcNow.ToString("s");
			ExTraceGlobals.ClusterTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[{0}] {1}", arg, locMessage.ToString());
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035BC File Offset: 0x000017BC
		public void AppendLogMessage(string englishMessage, params object[] args)
		{
			ExTraceGlobals.ClusterTracer.TraceDebug((long)this.GetHashCode(), englishMessage, args);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035D1 File Offset: 0x000017D1
		public void WriteVerbose(LocalizedString locString)
		{
			this.AppendLogMessage(locString);
		}

		// Token: 0x0400001F RID: 31
		private static NullLogTraceHelper s_nullLogger = new NullLogTraceHelper();
	}
}
