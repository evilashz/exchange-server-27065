using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorLogger : DisposeTrackableBase, ILogger, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003E35 File Offset: 0x00002035
		public AnchorLogger(string applicationName, AnchorConfig config) : this(applicationName, config, AnchorLogger.AnchorEventLogger)
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003E44 File Offset: 0x00002044
		public AnchorLogger(string applicationName, AnchorConfig config, ExEventLog eventLogger)
		{
			this.Config = config;
			this.log = new AnchorLog(applicationName, this.Config);
			this.EventLogger = eventLogger;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003E6C File Offset: 0x0000206C
		public AnchorLogger(string applicationName, AnchorConfig config, Trace trace, ExEventLog eventLogger)
		{
			this.Config = config;
			this.log = new AnchorLog(applicationName, this.Config, trace);
			this.EventLogger = eventLogger;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003E96 File Offset: 0x00002096
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003E9E File Offset: 0x0000209E
		internal Action<string, MigrationEventType, object, string> InMemoryLogger { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003EA7 File Offset: 0x000020A7
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003EAF File Offset: 0x000020AF
		internal ExEventLog EventLogger { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003EB8 File Offset: 0x000020B8
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003EC0 File Offset: 0x000020C0
		private AnchorConfig Config { get; set; }

		// Token: 0x060000E1 RID: 225 RVA: 0x00003ECC File Offset: 0x000020CC
		public static string PropertyBagToString(PropertyBag bag)
		{
			AnchorUtil.ThrowOnNullArgument(bag, "bag");
			StringBuilder stringBuilder = new StringBuilder(bag.Count * 128);
			foreach (object obj in bag.Keys)
			{
				PropertyDefinition propertyDefinition = obj as PropertyDefinition;
				if (propertyDefinition != null)
				{
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "[{0}:{1}]", new object[]
					{
						propertyDefinition.Name,
						bag[propertyDefinition]
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003F80 File Offset: 0x00002180
		public static string GetDiagnosticInfo(Exception ex, string diagnosticInfo)
		{
			AnchorUtil.ThrowOnNullArgument(ex, "ex");
			Exception innerException = ex.InnerException;
			int num = 0;
			while (num < 10 && innerException != null)
			{
				MapiPermanentException ex2 = innerException as MapiPermanentException;
				MapiRetryableException ex3 = innerException as MapiRetryableException;
				string text = innerException.Message;
				if (ex2 != null)
				{
					text = ex2.DiagCtx.ToCompactString();
				}
				else if (ex3 != null)
				{
					text = ex3.DiagCtx.ToCompactString();
				}
				if (!string.IsNullOrEmpty(text))
				{
					if (diagnosticInfo == null)
					{
						diagnosticInfo = string.Format(CultureInfo.InvariantCulture, "InnerException:{0}:{1}", new object[]
						{
							innerException.GetType().Name,
							text
						});
					}
					else
					{
						diagnosticInfo = string.Format(CultureInfo.InvariantCulture, "{0} InnerException:{1}:{2}", new object[]
						{
							diagnosticInfo,
							innerException.GetType().Name,
							text
						});
					}
				}
				num++;
				innerException = innerException.InnerException;
			}
			string value = string.Empty;
			MigrationPermanentException ex4 = ex as MigrationPermanentException;
			MigrationTransientException ex5 = ex as MigrationTransientException;
			if (ex4 != null)
			{
				value = ex4.InternalError + ". ";
			}
			else if (ex5 != null)
			{
				value = ex5.InternalError + ". ";
			}
			StringBuilder stringBuilder = new StringBuilder(value);
			stringBuilder.Append(diagnosticInfo);
			stringBuilder.Append(ex.ToString());
			return AnchorLogger.SanitizeDiagnosticInfo(stringBuilder.ToString());
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000040DC File Offset: 0x000022DC
		public static string SanitizeDiagnosticInfo(string diagnosticInfo)
		{
			AnchorUtil.ThrowOnNullOrEmptyArgument(diagnosticInfo, "diagnosticInfo");
			diagnosticInfo = diagnosticInfo.Replace("  ", " ");
			diagnosticInfo = diagnosticInfo.Replace("\n", " ");
			diagnosticInfo = diagnosticInfo.Replace("\r", " ");
			diagnosticInfo = diagnosticInfo.Replace("\t", " ");
			diagnosticInfo = diagnosticInfo.Replace("{", "[");
			diagnosticInfo = diagnosticInfo.Replace("}", "]");
			if (diagnosticInfo.Length > 16384)
			{
				return diagnosticInfo.Substring(0, 16381) + "...";
			}
			return diagnosticInfo;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004188 File Offset: 0x00002388
		public void LogEvent(MigrationEventType eventType, params string[] args)
		{
			this.LogEventInternal(eventType, null, true, null, args);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000041A8 File Offset: 0x000023A8
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.LogEventInternal(eventType, new ExEventLog.EventTuple?(eventId), true, null, args);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000041BC File Offset: 0x000023BC
		public void LogEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.LogEventInternal(eventType, null, true, ex, args);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000041DC File Offset: 0x000023DC
		public void LogEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.LogEventInternal(eventType, new ExEventLog.EventTuple?(eventId), true, ex, args);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000041F0 File Offset: 0x000023F0
		public void LogTerseEvent(MigrationEventType eventType, params string[] args)
		{
			this.LogEventInternal(eventType, null, false, null, args);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004210 File Offset: 0x00002410
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, params string[] args)
		{
			this.LogEventInternal(eventType, new ExEventLog.EventTuple?(eventId), false, null, args);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004224 File Offset: 0x00002424
		public void LogTerseEvent(MigrationEventType eventType, Exception ex, params string[] args)
		{
			this.LogEventInternal(eventType, null, false, ex, args);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004244 File Offset: 0x00002444
		public void LogTerseEvent(MigrationEventType eventType, ExEventLog.EventTuple eventId, Exception ex, params string[] args)
		{
			this.LogEventInternal(eventType, new ExEventLog.EventTuple?(eventId), false, ex, args);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004257 File Offset: 0x00002457
		public void Log(MigrationEventType eventType, Exception exception, string format)
		{
			this.Log(eventType, exception, format, null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004264 File Offset: 0x00002464
		public void LogError(Exception exception, string formatString, params object[] formatArgs)
		{
			AnchorUtil.ThrowOnNullArgument(formatArgs, "formatArgs");
			AnchorUtil.ThrowOnNullArgument(formatString, "formatString");
			if (formatArgs.Length == 0)
			{
				this.Log(MigrationEventType.Error, exception, "{0}", new object[]
				{
					formatString
				});
				return;
			}
			this.Log(MigrationEventType.Error, exception, formatString, formatArgs);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000042B0 File Offset: 0x000024B0
		public void LogVerbose(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Verbose, formatString, formatArgs);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000042BB File Offset: 0x000024BB
		public void LogWarning(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Warning, formatString, formatArgs);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000042C6 File Offset: 0x000024C6
		public void LogInformation(string formatString, params object[] formatArgs)
		{
			this.Log(MigrationEventType.Information, formatString, formatArgs);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000042D1 File Offset: 0x000024D1
		public void Log(MigrationEventType eventType, Exception exception, string formatString, params object[] args)
		{
			if (exception != null)
			{
				this.Log(eventType, formatString + ", exception " + AnchorLogger.GetDiagnosticInfo(exception, null), args);
				return;
			}
			this.Log(eventType, formatString, args);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000042FC File Offset: 0x000024FC
		public void Log(MigrationEventType eventType, string format)
		{
			this.Log(eventType, format, null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004307 File Offset: 0x00002507
		public void Log(MigrationEventType eventType, string formatString, params object[] args)
		{
			this.Log(AnchorLogContext.Current.Source, eventType, AnchorLogContext.Current.ToString(), formatString, args);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004328 File Offset: 0x00002528
		public void Log(string source, MigrationEventType eventType, object context, string format, params object[] args)
		{
			if (this.log != null)
			{
				this.log.Log(source, eventType, context, format, args);
			}
			if (this.InMemoryLogger != null)
			{
				string arg = (args != null && args.Length > 0) ? string.Format(format, args) : format;
				this.InMemoryLogger(source, eventType, context, arg);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000437F File Offset: 0x0000257F
		internal static void LogEvent(ExEventLog.EventTuple eventId, params string[] messageArgs)
		{
			AnchorLogger.LogEvent(AnchorLogger.AnchorEventLogger, eventId, true, messageArgs);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000438E File Offset: 0x0000258E
		internal static void LogEvent(ExEventLog eventLogger, ExEventLog.EventTuple eventId, params string[] messageArgs)
		{
			AnchorLogger.LogEvent(eventLogger, eventId, true, messageArgs);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000439C File Offset: 0x0000259C
		internal static void LogEvent(ExEventLog eventLogger, ExEventLog.EventTuple eventId, bool includeAnchorContext, params string[] messageArgs)
		{
			AnchorUtil.ThrowOnNullArgument(eventLogger, "eventLogger");
			AnchorUtil.ThrowOnNullArgument(messageArgs, "messageArgs");
			if (messageArgs == null || messageArgs.Length <= 0)
			{
				return;
			}
			if (includeAnchorContext)
			{
				messageArgs[0] = AnchorLogContext.Current.ToString() + ":" + messageArgs[0];
			}
			eventLogger.LogEvent(eventId, AnchorLogContext.Current.Source, messageArgs);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000043FC File Offset: 0x000025FC
		protected void LogEventInternal(MigrationEventType eventType, ExEventLog.EventTuple? eventId, bool includeAnchorContext, Exception exception, params string[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (eventId != null)
			{
				stringBuilder.Append("Event " + (eventId.Value.EventId & 65535U).ToString() + " ");
			}
			else
			{
				eventId = this.EventIdFromLogLevel(eventType);
			}
			if (eventId == null)
			{
				return;
			}
			AnchorLogger.LogEvent(this.EventLogger, eventId.Value, includeAnchorContext, args);
			if (args != null)
			{
				foreach (string value in args)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(',');
				}
			}
			this.Log(eventType, exception, stringBuilder.ToString());
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000044B8 File Offset: 0x000026B8
		protected virtual ExEventLog.EventTuple? EventIdFromLogLevel(MigrationEventType eventType)
		{
			switch (eventType)
			{
			case MigrationEventType.Error:
			case MigrationEventType.Warning:
				return new ExEventLog.EventTuple?(MSExchangeAnchorServiceEventLogConstants.Tuple_CriticalError);
			case MigrationEventType.Information:
				return new ExEventLog.EventTuple?(MSExchangeAnchorServiceEventLogConstants.Tuple_Information);
			default:
				return null;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000044FC File Offset: 0x000026FC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorLogger>(this);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004504 File Offset: 0x00002704
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.log != null)
				{
					this.log.Close();
				}
				this.log = null;
			}
		}

		// Token: 0x04000055 RID: 85
		private const int MaxCharsPerEventLog = 16384;

		// Token: 0x04000056 RID: 86
		private static readonly ExEventLog AnchorEventLogger = new ExEventLog(new Guid("0218300d-40aa-4060-91b6-beccda131340"), "MSExchange Anchor Service");

		// Token: 0x04000057 RID: 87
		private AnchorLog log;
	}
}
