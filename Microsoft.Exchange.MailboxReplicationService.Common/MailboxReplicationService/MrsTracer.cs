using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxReplicationService;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001AC RID: 428
	internal static class MrsTracer
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x00026164 File Offset: 0x00024364
		static MrsTracer()
		{
			MrsTracer.instanceCommon = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationCommonTracer, "Common");
			MrsTracer.instanceResourceHealth = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationResourceHealthTracer, "ResourceHealth");
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00026239 File Offset: 0x00024439
		public static MrsTracer.MrsTracerInstance Service
		{
			get
			{
				return MrsTracer.instanceService;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00026240 File Offset: 0x00024440
		public static MrsTracer.MrsTracerInstance Provider
		{
			get
			{
				return MrsTracer.instanceProvider;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00026247 File Offset: 0x00024447
		public static MrsTracer.MrsTracerInstance Authorization
		{
			get
			{
				return MrsTracer.instanceAuthorization;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0002624E File Offset: 0x0002444E
		public static MrsTracer.MrsTracerInstance ProxyService
		{
			get
			{
				return MrsTracer.instanceProxyService;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00026255 File Offset: 0x00024455
		public static MrsTracer.MrsTracerInstance ProxyClient
		{
			get
			{
				return MrsTracer.instanceProxyClient;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0002625C File Offset: 0x0002445C
		public static MrsTracer.MrsTracerInstance Cmdlet
		{
			get
			{
				return MrsTracer.instanceCmdlet;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00026263 File Offset: 0x00024463
		public static MrsTracer.MrsTracerInstance UpdateMovedMailbox
		{
			get
			{
				return MrsTracer.instanceUpdateMovedMailbox;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0002626A File Offset: 0x0002446A
		public static MrsTracer.MrsTracerInstance Throttling
		{
			get
			{
				return MrsTracer.instanceThrottling;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00026271 File Offset: 0x00024471
		public static MrsTracer.MrsTracerInstance Common
		{
			get
			{
				return MrsTracer.instanceCommon;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x00026278 File Offset: 0x00024478
		public static MrsTracer.MrsTracerInstance ResourceHealth
		{
			get
			{
				return MrsTracer.instanceResourceHealth;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0002627F File Offset: 0x0002447F
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x00026286 File Offset: 0x00024486
		public static int ActivityID
		{
			get
			{
				return MrsTracer.activityID;
			}
			set
			{
				MrsTracer.activityID = value;
			}
		}

		// Token: 0x04000955 RID: 2389
		public const string MrsDbgCategory = "Microsoft.Exchange.MailboxReplicationService";

		// Token: 0x04000956 RID: 2390
		[ThreadStatic]
		private static int activityID;

		// Token: 0x04000957 RID: 2391
		private static MrsTracer.MrsTracerInstance instanceService = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationServiceTracer, "Service");

		// Token: 0x04000958 RID: 2392
		private static MrsTracer.MrsTracerInstance instanceProvider = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationServiceProviderTracer, "Provider");

		// Token: 0x04000959 RID: 2393
		private static MrsTracer.MrsTracerInstance instanceAuthorization = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationAuthorizationTracer, "Authorization");

		// Token: 0x0400095A RID: 2394
		private static MrsTracer.MrsTracerInstance instanceProxyService = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationProxyServiceTracer, "ProxyService");

		// Token: 0x0400095B RID: 2395
		private static MrsTracer.MrsTracerInstance instanceProxyClient = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationProxyClientTracer, "ProxyClient");

		// Token: 0x0400095C RID: 2396
		private static MrsTracer.MrsTracerInstance instanceUpdateMovedMailbox = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationUpdateMovedMailboxTracer, "UpdateMovedMailbox");

		// Token: 0x0400095D RID: 2397
		private static MrsTracer.MrsTracerInstance instanceThrottling = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationServiceThrottlingTracer, "Throttling");

		// Token: 0x0400095E RID: 2398
		private static MrsTracer.MrsTracerInstance instanceCommon;

		// Token: 0x0400095F RID: 2399
		private static MrsTracer.MrsTracerInstance instanceCmdlet = new MrsTracer.MrsTracerInstance(ExTraceGlobals.MailboxReplicationCmdletTracer, "Cmdlet");

		// Token: 0x04000960 RID: 2400
		private static MrsTracer.MrsTracerInstance instanceResourceHealth;

		// Token: 0x020001AD RID: 429
		public class MrsTracerInstance
		{
			// Token: 0x06001039 RID: 4153 RVA: 0x0002628E File Offset: 0x0002448E
			public MrsTracerInstance(Microsoft.Exchange.Diagnostics.Trace traceObj, string name)
			{
				this.Tracer = traceObj;
				this.Name = name;
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x0600103A RID: 4154 RVA: 0x000262A4 File Offset: 0x000244A4
			// (set) Token: 0x0600103B RID: 4155 RVA: 0x000262AC File Offset: 0x000244AC
			public Microsoft.Exchange.Diagnostics.Trace Tracer { get; private set; }

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x0600103C RID: 4156 RVA: 0x000262B5 File Offset: 0x000244B5
			// (set) Token: 0x0600103D RID: 4157 RVA: 0x000262BD File Offset: 0x000244BD
			public string Name { get; private set; }

			// Token: 0x0600103E RID: 4158 RVA: 0x000262C6 File Offset: 0x000244C6
			public void Debug(string formatString, params object[] args)
			{
				this.TraceMessage(TraceType.DebugTrace, new Action<long, string, object[]>(this.Tracer.TraceDebug), formatString, args);
			}

			// Token: 0x0600103F RID: 4159 RVA: 0x000262E2 File Offset: 0x000244E2
			public void Warning(string formatString, params object[] args)
			{
				this.TraceMessage(TraceType.WarningTrace, new Action<long, string, object[]>(this.Tracer.TraceWarning), formatString, args);
			}

			// Token: 0x06001040 RID: 4160 RVA: 0x000262FE File Offset: 0x000244FE
			public void Error(string formatString, params object[] args)
			{
				this.TraceMessage(TraceType.ErrorTrace, new Action<long, string, object[]>(this.Tracer.TraceError), formatString, args);
			}

			// Token: 0x06001041 RID: 4161 RVA: 0x0002631A File Offset: 0x0002451A
			public void Function(string formatString, params object[] args)
			{
				this.TraceMessage(TraceType.FunctionTrace, new Action<long, string, object[]>(this.Tracer.TraceFunction), formatString, args);
			}

			// Token: 0x06001042 RID: 4162 RVA: 0x00026336 File Offset: 0x00024536
			public bool IsEnabled(TraceType traceType)
			{
				return this.Tracer.IsTraceEnabled(traceType) || this.IsTraceLoggingEnabled(traceType);
			}

			// Token: 0x06001043 RID: 4163 RVA: 0x00026350 File Offset: 0x00024550
			private bool IsTraceLoggingEnabled(TraceType traceType)
			{
				string config = ConfigBase<MRSConfigSchema>.GetConfig<string>("TraceLogLevels");
				if (!CommonUtils.IsValueInWildcardedList(traceType.ToString(), config))
				{
					return false;
				}
				string config2 = ConfigBase<MRSConfigSchema>.GetConfig<string>("TraceLogTracers");
				return CommonUtils.IsValueInWildcardedList(this.Name, config2);
			}

			// Token: 0x06001044 RID: 4164 RVA: 0x00026399 File Offset: 0x00024599
			private void TraceMessage(TraceType traceType, Action<long, string, object[]> traceFunction, string formatString, object[] args)
			{
				traceFunction((long)MrsTracer.ActivityID, formatString, args);
				this.TraceToDebugger(traceType, formatString, args);
				if (this.IsTraceLoggingEnabled(traceType))
				{
					TraceLog.Write(this.Name, traceType, string.Format(formatString, args));
				}
			}

			// Token: 0x06001045 RID: 4165 RVA: 0x000263D4 File Offset: 0x000245D4
			private void TraceToDebugger(TraceType traceType, string formatString, object[] args)
			{
				if (!Debugger.IsAttached)
				{
					return;
				}
				ExDateTime now = ExDateTime.Now;
				string text;
				switch (traceType)
				{
				case TraceType.DebugTrace:
					text = "D";
					goto IL_5E;
				case TraceType.WarningTrace:
					text = "W";
					goto IL_5E;
				case TraceType.ErrorTrace:
					text = "E";
					goto IL_5E;
				case TraceType.FunctionTrace:
					text = "F";
					goto IL_5E;
				}
				text = "x";
				IL_5E:
				string message = string.Format("[{0:X8}] {1:D2}:{2:D2}:{3:D2}.{4:D3} {5} ", new object[]
				{
					MrsTracer.ActivityID,
					now.Hour,
					now.Minute,
					now.Second,
					now.Millisecond,
					text
				}) + string.Format(formatString, args) + "\n";
				lock (MrsTracer.MrsTracerInstance.locker)
				{
					Debugger.Log((int)traceType, "Microsoft.Exchange.MailboxReplicationService", message);
				}
			}

			// Token: 0x04000961 RID: 2401
			private static object locker = new object();
		}
	}
}
