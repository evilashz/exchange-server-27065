using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200005E RID: 94
	internal class DiagnosticHelper
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000D0C2 File Offset: 0x0000B2C2
		public DiagnosticHelper(object owner, Trace tracer)
		{
			this.owner = owner;
			this.tracer = tracer;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public void Trace(string format, params object[] args)
		{
			CallIdTracer.TraceDebug(this.tracer, this.owner, format, args);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D0ED File Offset: 0x0000B2ED
		public void Trace(PIIMessage[] piiData, string format, params object[] args)
		{
			CallIdTracer.TraceDebug(this.tracer, this.owner, piiData, format, args);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D103 File Offset: 0x0000B303
		public void Trace(PIIMessage pii, string format, params object[] args)
		{
			CallIdTracer.TraceDebug(this.tracer, this.owner, pii, format, args);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D119 File Offset: 0x0000B319
		public void TraceError(string format, params object[] args)
		{
			CallIdTracer.TraceError(this.tracer, this.owner, format, args);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D12E File Offset: 0x0000B32E
		public void Assert(bool condition)
		{
			ExAssert.RetailAssert(condition, "(no message)");
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D13B File Offset: 0x0000B33B
		public void Assert(bool condition, string msg)
		{
			ExAssert.RetailAssert(condition, msg);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D144 File Offset: 0x0000B344
		public void Assert(bool condition, string msg, params object[] args)
		{
			ExAssert.RetailAssert(condition, msg, args);
		}

		// Token: 0x04000294 RID: 660
		private object owner;

		// Token: 0x04000295 RID: 661
		private Trace tracer;
	}
}
