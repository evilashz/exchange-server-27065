using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Common
{
	// Token: 0x02000311 RID: 785
	public static class ExTraceGlobals
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0004A5FD File Offset: 0x000487FD
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0004A61B File Offset: 0x0004881B
		public static Trace EventLogTracer
		{
			get
			{
				if (ExTraceGlobals.eventLogTracer == null)
				{
					ExTraceGlobals.eventLogTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.eventLogTracer;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0004A639 File Offset: 0x00048839
		public static Trace ScheduleIntervalTracer
		{
			get
			{
				if (ExTraceGlobals.scheduleIntervalTracer == null)
				{
					ExTraceGlobals.scheduleIntervalTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.scheduleIntervalTracer;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0004A657 File Offset: 0x00048857
		public static Trace CertificateValidationTracer
		{
			get
			{
				if (ExTraceGlobals.certificateValidationTracer == null)
				{
					ExTraceGlobals.certificateValidationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.certificateValidationTracer;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0004A675 File Offset: 0x00048875
		public static Trace AuthorizationTracer
		{
			get
			{
				if (ExTraceGlobals.authorizationTracer == null)
				{
					ExTraceGlobals.authorizationTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.authorizationTracer;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0004A693 File Offset: 0x00048893
		public static Trace TracingTracer
		{
			get
			{
				if (ExTraceGlobals.tracingTracer == null)
				{
					ExTraceGlobals.tracingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.tracingTracer;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0004A6B1 File Offset: 0x000488B1
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0004A6CF File Offset: 0x000488CF
		public static Trace RpcTracer
		{
			get
			{
				if (ExTraceGlobals.rpcTracer == null)
				{
					ExTraceGlobals.rpcTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.rpcTracer;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0004A6ED File Offset: 0x000488ED
		public static Trace SqmTracer
		{
			get
			{
				if (ExTraceGlobals.sqmTracer == null)
				{
					ExTraceGlobals.sqmTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.sqmTracer;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0004A70B File Offset: 0x0004890B
		public static Trace TracingConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.tracingConfigurationTracer == null)
				{
					ExTraceGlobals.tracingConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.tracingConfigurationTracer;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0004A72A File Offset: 0x0004892A
		public static Trace FaultInjectionConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionConfigurationTracer == null)
				{
					ExTraceGlobals.faultInjectionConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.faultInjectionConfigurationTracer;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0004A749 File Offset: 0x00048949
		public static Trace AppConfigLoaderTracer
		{
			get
			{
				if (ExTraceGlobals.appConfigLoaderTracer == null)
				{
					ExTraceGlobals.appConfigLoaderTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.appConfigLoaderTracer;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0004A768 File Offset: 0x00048968
		public static Trace WebHealthTracer
		{
			get
			{
				if (ExTraceGlobals.webHealthTracer == null)
				{
					ExTraceGlobals.webHealthTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.webHealthTracer;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0004A787 File Offset: 0x00048987
		public static Trace VariantConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.variantConfigurationTracer == null)
				{
					ExTraceGlobals.variantConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.variantConfigurationTracer;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0004A7A6 File Offset: 0x000489A6
		public static Trace ClientAccessRulesTracer
		{
			get
			{
				if (ExTraceGlobals.clientAccessRulesTracer == null)
				{
					ExTraceGlobals.clientAccessRulesTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.clientAccessRulesTracer;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0004A7C5 File Offset: 0x000489C5
		public static Trace ConcurrencyGuardTracer
		{
			get
			{
				if (ExTraceGlobals.concurrencyGuardTracer == null)
				{
					ExTraceGlobals.concurrencyGuardTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.concurrencyGuardTracer;
			}
		}

		// Token: 0x040014F5 RID: 5365
		private static Guid componentGuid = new Guid("5948f08f-9d8f-11da-9575-00e08161165f");

		// Token: 0x040014F6 RID: 5366
		private static Trace commonTracer = null;

		// Token: 0x040014F7 RID: 5367
		private static Trace eventLogTracer = null;

		// Token: 0x040014F8 RID: 5368
		private static Trace scheduleIntervalTracer = null;

		// Token: 0x040014F9 RID: 5369
		private static Trace certificateValidationTracer = null;

		// Token: 0x040014FA RID: 5370
		private static Trace authorizationTracer = null;

		// Token: 0x040014FB RID: 5371
		private static Trace tracingTracer = null;

		// Token: 0x040014FC RID: 5372
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040014FD RID: 5373
		private static Trace rpcTracer = null;

		// Token: 0x040014FE RID: 5374
		private static Trace sqmTracer = null;

		// Token: 0x040014FF RID: 5375
		private static Trace tracingConfigurationTracer = null;

		// Token: 0x04001500 RID: 5376
		private static Trace faultInjectionConfigurationTracer = null;

		// Token: 0x04001501 RID: 5377
		private static Trace appConfigLoaderTracer = null;

		// Token: 0x04001502 RID: 5378
		private static Trace webHealthTracer = null;

		// Token: 0x04001503 RID: 5379
		private static Trace variantConfigurationTracer = null;

		// Token: 0x04001504 RID: 5380
		private static Trace clientAccessRulesTracer = null;

		// Token: 0x04001505 RID: 5381
		private static Trace concurrencyGuardTracer = null;
	}
}
