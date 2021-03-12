using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000CD3 RID: 3283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CmdletWriteVerboseTracer : ITracer
	{
		// Token: 0x06007E9A RID: 32410 RVA: 0x002059C1 File Offset: 0x00203BC1
		public CmdletWriteVerboseTracer(PSCmdlet cmdlet)
		{
			ArgumentValidator.ThrowIfNull("cmdlet", cmdlet);
			this.cmdlet = cmdlet;
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x002059DB File Offset: 0x00203BDB
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0));
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x002059F0 File Offset: 0x00203BF0
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06007E9D RID: 32413 RVA: 0x00205A0C File Offset: 0x00203C0C
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x00205A2F File Offset: 0x00203C2F
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, args));
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x00205A3F File Offset: 0x00203C3F
		public void TraceDebug(long id, string message)
		{
			this.WriteToVerboseStream(id, message);
		}

		// Token: 0x06007EA0 RID: 32416 RVA: 0x00205A49 File Offset: 0x00203C49
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0));
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x00205A5E File Offset: 0x00203C5E
		public void TraceWarning(long id, string message)
		{
			this.WriteToVerboseStream(id, message);
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x00205A68 File Offset: 0x00203C68
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, args));
		}

		// Token: 0x06007EA3 RID: 32419 RVA: 0x00205A78 File Offset: 0x00203C78
		public void TraceError(long id, string message)
		{
			this.WriteToVerboseStream(id, message);
		}

		// Token: 0x06007EA4 RID: 32420 RVA: 0x00205A82 File Offset: 0x00203C82
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, args));
		}

		// Token: 0x06007EA5 RID: 32421 RVA: 0x00205A92 File Offset: 0x00203C92
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0));
		}

		// Token: 0x06007EA6 RID: 32422 RVA: 0x00205AA7 File Offset: 0x00203CA7
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x00205AC3 File Offset: 0x00203CC3
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06007EA8 RID: 32424 RVA: 0x00205AE6 File Offset: 0x00203CE6
		public void TracePerformance(long id, string message)
		{
			this.WriteToVerboseStream(id, message);
		}

		// Token: 0x06007EA9 RID: 32425 RVA: 0x00205AF0 File Offset: 0x00203CF0
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, args));
		}

		// Token: 0x06007EAA RID: 32426 RVA: 0x00205B00 File Offset: 0x00203D00
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0));
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x00205B15 File Offset: 0x00203D15
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x00205B31 File Offset: 0x00203D31
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.WriteToVerboseStream(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06007EAD RID: 32429 RVA: 0x00205B54 File Offset: 0x00203D54
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007EAE RID: 32430 RVA: 0x00205B5B File Offset: 0x00203D5B
		public ITracer Compose(ITracer other)
		{
			return new CompositeTracer(this, other);
		}

		// Token: 0x06007EAF RID: 32431 RVA: 0x00205B64 File Offset: 0x00203D64
		public bool IsTraceEnabled(TraceType traceType)
		{
			return true;
		}

		// Token: 0x06007EB0 RID: 32432 RVA: 0x00205B67 File Offset: 0x00203D67
		private void WriteToVerboseStream(long id, string message)
		{
			this.cmdlet.WriteVerbose(message);
		}

		// Token: 0x04003E3D RID: 15933
		private readonly PSCmdlet cmdlet;
	}
}
