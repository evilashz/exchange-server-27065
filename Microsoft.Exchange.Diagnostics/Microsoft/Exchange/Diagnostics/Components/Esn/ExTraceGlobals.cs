using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Esn
{
	// Token: 0x0200040A RID: 1034
	public static class ExTraceGlobals
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0005D430 File Offset: 0x0005B630
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0005D44E File Offset: 0x0005B64E
		public static Trace DataTracer
		{
			get
			{
				if (ExTraceGlobals.dataTracer == null)
				{
					ExTraceGlobals.dataTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dataTracer;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0005D46C File Offset: 0x0005B66C
		public static Trace PreProcessorTracer
		{
			get
			{
				if (ExTraceGlobals.preProcessorTracer == null)
				{
					ExTraceGlobals.preProcessorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.preProcessorTracer;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0005D48A File Offset: 0x0005B68A
		public static Trace ComposerTracer
		{
			get
			{
				if (ExTraceGlobals.composerTracer == null)
				{
					ExTraceGlobals.composerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.composerTracer;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005D4A8 File Offset: 0x0005B6A8
		public static Trace PostProcessorTracer
		{
			get
			{
				if (ExTraceGlobals.postProcessorTracer == null)
				{
					ExTraceGlobals.postProcessorTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.postProcessorTracer;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0005D4C6 File Offset: 0x0005B6C6
		public static Trace MailSenderTracer
		{
			get
			{
				if (ExTraceGlobals.mailSenderTracer == null)
				{
					ExTraceGlobals.mailSenderTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.mailSenderTracer;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005D4E4 File Offset: 0x0005B6E4
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001DAA RID: 7594
		private static Guid componentGuid = new Guid("A0D123B0-CF78-4BCA-AAC9-F892D98199F4");

		// Token: 0x04001DAB RID: 7595
		private static Trace generalTracer = null;

		// Token: 0x04001DAC RID: 7596
		private static Trace dataTracer = null;

		// Token: 0x04001DAD RID: 7597
		private static Trace preProcessorTracer = null;

		// Token: 0x04001DAE RID: 7598
		private static Trace composerTracer = null;

		// Token: 0x04001DAF RID: 7599
		private static Trace postProcessorTracer = null;

		// Token: 0x04001DB0 RID: 7600
		private static Trace mailSenderTracer = null;

		// Token: 0x04001DB1 RID: 7601
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
