using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x0200040F RID: 1039
	internal sealed class IisTaskTrace
	{
		// Token: 0x06002453 RID: 9299 RVA: 0x00090C2B File Offset: 0x0008EE2B
		private IisTaskTrace()
		{
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00090C33 File Offset: 0x0008EE33
		public static void InitializeTracing(Trace taskTracer, Trace nonTaskTracer)
		{
			IisTaskTrace.vDirTracer = taskTracer;
			IisTaskTrace.utilityTracer = nonTaskTracer;
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x00090C41 File Offset: 0x0008EE41
		public static Trace VDirTracer
		{
			get
			{
				return IisTaskTrace.vDirTracer;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x00090C48 File Offset: 0x0008EE48
		public static Trace IisUtilityTracer
		{
			get
			{
				return IisTaskTrace.utilityTracer;
			}
		}

		// Token: 0x04001CD9 RID: 7385
		private static Trace vDirTracer;

		// Token: 0x04001CDA RID: 7386
		private static Trace utilityTracer;
	}
}
