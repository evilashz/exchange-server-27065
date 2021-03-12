using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000AB RID: 171
	public static class SystemTraceControl
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000F9A6 File Offset: 0x0000DBA6
		private static SourceLevels GetCurrentTraceLevel(BitArray enabledTypes)
		{
			if (enabledTypes[1] || enabledTypes[7])
			{
				return SourceLevels.Verbose;
			}
			if (enabledTypes[5])
			{
				return SourceLevels.Information;
			}
			if (enabledTypes[2])
			{
				return SourceLevels.Warning;
			}
			if (enabledTypes[3])
			{
				return SourceLevels.Error;
			}
			return SourceLevels.Off;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		public static void Update(Dictionary<Guid, BitArray> componentDictionary, BitArray enabledTypes, bool anyExchangeTracingProvidersEnabled)
		{
			BitArray bitArray;
			if (componentDictionary.TryGetValue(SystemLoggingTags.guid, out bitArray))
			{
				SourceLevels currentTraceLevel = SystemTraceControl.GetCurrentTraceLevel(enabledTypes);
				SystemNetLogging.Instance.SourceLevels = currentTraceLevel;
				SystemNetLogging.Instance.Enabled = ((bitArray.Get(0) || bitArray.Get(1) || bitArray.Get(2)) && anyExchangeTracingProvidersEnabled);
				DiagnosticTraceLogging.SystemIdentityModel.SourceLevels = currentTraceLevel;
				DiagnosticTraceLogging.SystemIdentityModel.Enabled = (bitArray.Get(3) && anyExchangeTracingProvidersEnabled);
				DiagnosticTraceLogging.SystemServiceModel.SourceLevels = currentTraceLevel;
				DiagnosticTraceLogging.SystemServiceModel.Enabled = (bitArray.Get(4) && anyExchangeTracingProvidersEnabled);
				SystemServiceModelMessageLogging.Instance.SourceLevels = currentTraceLevel;
				SystemServiceModelMessageLogging.Instance.Enabled = anyExchangeTracingProvidersEnabled;
				SystemServiceModelMessageLogging.Instance.LogMalformedMessages = bitArray.Get(6);
				SystemServiceModelMessageLogging.Instance.LogMessagesAtServiceLevel = bitArray.Get(7);
				SystemServiceModelMessageLogging.Instance.LogMessagesAtTransportLevel = bitArray.Get(8);
				SystemServiceModelMessageLogging.Instance.LogMessageBody = bitArray.Get(9);
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
		public static void AddExtendedErrorListener(string source, TraceListener extendedErrorListener)
		{
			if (source != SystemTraceControl.SourceHttpListener)
			{
				throw new ArgumentException("source");
			}
			SystemNetLogging.Instance.AddHttpListenerExtendedErrorListener(extendedErrorListener);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		public static void RemoveExtendedErrorListener(string source, TraceListener extendedErrorListener)
		{
			if (source != SystemTraceControl.SourceHttpListener)
			{
				throw new ArgumentException("source");
			}
			SystemNetLogging.Instance.RemoveHttpListenerExtendedErrorListener(extendedErrorListener);
		}

		// Token: 0x0400035A RID: 858
		public static readonly string SourceHttpListener = "System.Net.HttpListener";

		// Token: 0x0400035B RID: 859
		public static readonly string SourceSockets = "System.Net.Sockets";

		// Token: 0x0400035C RID: 860
		public static readonly string SourceSystemNet = "System.Net";
	}
}
