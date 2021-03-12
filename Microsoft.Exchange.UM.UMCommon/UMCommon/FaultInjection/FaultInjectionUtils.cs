using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x02000076 RID: 118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FaultInjectionUtils
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0000F048 File Offset: 0x0000D248
		private static void RegisterCallback()
		{
			FaultInjectionUtils.createExceptions = new List<CreateException>();
			FaultInjectionUtils.createExceptions.Add(new CreateException(RMSFaultInjection.TryCreateException));
			FaultInjectionUtils.createExceptions.Add(new CreateException(UMLicensingFaultInjection.TryCreateException));
			FaultInjectionUtils.createExceptions.Add(new CreateException(DiagnosticFaultInjection.TryCreateException));
			FaultInjectionUtils.createExceptions.Add(new CreateException(UMReportingFaultInjection.TryCreateException));
			FaultInjectionUtils.createExceptions.Add(new CreateException(UMGrammarGeneratorFaultInjection.TryCreateException));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			foreach (CreateException ex in FaultInjectionUtils.createExceptions)
			{
				if (ex(exceptionType, ref result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000F130 File Offset: 0x0000D330
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (FaultInjectionUtils.faultInjectionTracer == null)
				{
					lock (FaultInjectionUtils.lockObject)
					{
						if (FaultInjectionUtils.faultInjectionTracer == null)
						{
							FaultInjectionTrace faultInjectionTrace = ExTraceGlobals.FaultInjectionTracer;
							FaultInjectionUtils.RegisterCallback();
							faultInjectionTrace.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjectionUtils.Callback));
							FaultInjectionUtils.faultInjectionTracer = faultInjectionTrace;
						}
					}
				}
				return FaultInjectionUtils.faultInjectionTracer;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public static void FaultInjectException(uint lid)
		{
			if (Utils.RunningInTestMode)
			{
				FaultInjectionUtils.FaultInjectionTracer.TraceTest(lid);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		public static void FaultInjectChangeValue<T>(uint lid, ref T objectToChange)
		{
			if (Utils.RunningInTestMode)
			{
				FaultInjectionUtils.FaultInjectionTracer.TraceTest<T>(lid, ref objectToChange);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F1C9 File Offset: 0x0000D3C9
		public static void FaultInjectCompare<T>(uint lid, T objectToCompare)
		{
			if (Utils.RunningInTestMode)
			{
				FaultInjectionUtils.FaultInjectionTracer.TraceTest<T>(lid, objectToCompare);
			}
		}

		// Token: 0x040002DC RID: 732
		private static List<CreateException> createExceptions;

		// Token: 0x040002DD RID: 733
		private static object lockObject = new object();

		// Token: 0x040002DE RID: 734
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
