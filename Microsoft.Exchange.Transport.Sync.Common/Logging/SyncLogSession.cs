using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000080 RID: 128
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncLogSession
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00014184 File Offset: 0x00012384
		internal SyncLogSession(SyncLog syncLog, LogRowFormatter row)
		{
			if (syncLog == null)
			{
				throw new ArgumentNullException("syncLog");
			}
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			this.syncLog = syncLog;
			this.row = row;
			this.blackBox = new SyncLogBlackBox(true);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600033A RID: 826 RVA: 0x000141D8 File Offset: 0x000123D8
		internal static SyncLogSession InMemorySyncLogSession
		{
			get
			{
				return SyncLogSession.inmemorySyncLogSession;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000141DF File Offset: 0x000123DF
		// (set) Token: 0x0600033C RID: 828 RVA: 0x000141E7 File Offset: 0x000123E7
		internal bool SkipDebugAsserts
		{
			get
			{
				return this.skipDebugAsserts;
			}
			set
			{
				this.skipDebugAsserts = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000141F0 File Offset: 0x000123F0
		internal SyncLogBlackBox BlackBox
		{
			get
			{
				return this.blackBox;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000141F8 File Offset: 0x000123F8
		internal SyncLog SyncLog
		{
			get
			{
				return this.syncLog;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00014200 File Offset: 0x00012400
		public void SetBlackBoxCapacity(int capacity)
		{
			this.blackBox.ResizeAndClear(capacity);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001420E File Offset: 0x0001240E
		public string GetBlackBoxText()
		{
			return this.blackBox.ToString();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001421B File Offset: 0x0001241B
		public void ClearBlackBox()
		{
			this.blackBox.Clear();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00014228 File Offset: 0x00012428
		public void ReportWatson(string message)
		{
			this.ReportWatson(message, null);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00014232 File Offset: 0x00012432
		public void ReportWatson(Exception exception)
		{
			this.ReportWatson(null, exception);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001423C File Offset: 0x0001243C
		public void ReportWatson(string message, Exception exception)
		{
			this.syncLog.WatsonReporter.ReportWatson(this, message, exception);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014251 File Offset: 0x00012451
		[Conditional("DEBUG")]
		public void Assert(bool expression, string formatString, params object[] parameters)
		{
			if (expression)
			{
				return;
			}
			this.LogError((TSLID)0UL, "Assert: " + formatString, parameters);
			bool flag = this.skipDebugAsserts;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014277 File Offset: 0x00012477
		public void RetailAssert(bool expression, string formatString, params object[] parameters)
		{
			if (expression)
			{
				return;
			}
			this.LogError((TSLID)0UL, "Assert: " + formatString, parameters);
			ExAssert.RetailAssert(expression, formatString, parameters);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000142A0 File Offset: 0x000124A0
		public void AddBlackBoxLogToWatson()
		{
			SyncLog syncLog = this.syncLog;
			if (syncLog == null)
			{
				return;
			}
			ExWatson.AddExtraData(syncLog.GenerateBlackBoxLog());
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000142C4 File Offset: 0x000124C4
		public void LogConnect(TSLID logEntryId, string contextFormat, params object[] contextArguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, "+", null, contextFormat, contextArguments);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000142E8 File Offset: 0x000124E8
		public void LogDisconnect(TSLID logEntryId, string reason)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, "-", null, reason, new object[0]);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00014310 File Offset: 0x00012510
		public void LogSend(TSLID logEntryId, byte[] data)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, ">", data, null, new object[0]);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00014338 File Offset: 0x00012538
		public void LogSend(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, ">", null, null, arguments);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001435C File Offset: 0x0001255C
		public void LogConnectionInformation(TSLID logEntryId, string endpoint)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Information, Guid.Empty, null, null, null, "Destination Server: {0}", new object[]
			{
				endpoint
			});
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001438C File Offset: 0x0001258C
		public void LogReceive(TSLID logEntryId, byte[] data)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, "<", data, null, new object[0]);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000143B4 File Offset: 0x000125B4
		public void LogReceive(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, "<", null, format, arguments);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000143D8 File Offset: 0x000125D8
		public void LogInformation(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Information, Guid.Empty, null, null, null, format, arguments);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000143F7 File Offset: 0x000125F7
		public void LogInformation(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, long id, string format, params object[] arguments)
		{
			this.LogInformation(logEntryId, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeTracerMessage(format, arguments));
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001441E File Offset: 0x0001261E
		public void LogInformation(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, string format, params object[] arguments)
		{
			this.LogInformation(logEntryId, trace, 0L, format, arguments);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00014430 File Offset: 0x00012630
		public void LogVerbose(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Verbose, Guid.Empty, null, null, null, format, arguments);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001444F File Offset: 0x0001264F
		public void LogVerbose(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, long id, string format, params object[] arguments)
		{
			this.LogVerbose(logEntryId, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeTracerMessage(format, arguments));
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00014476 File Offset: 0x00012676
		public void LogVerbose(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, string format, params object[] arguments)
		{
			this.LogVerbose(logEntryId, trace, 0L, format, arguments);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00014488 File Offset: 0x00012688
		public void LogError(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Error, Guid.Empty, null, null, null, format, arguments);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000144A7 File Offset: 0x000126A7
		public void LogError(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, long id, string format, params object[] arguments)
		{
			this.LogError(logEntryId, format, arguments);
			if (trace.IsTraceEnabled(TraceType.ErrorTrace))
			{
				trace.TraceError(id, this.MakeTracerMessage(format, arguments));
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000144CE File Offset: 0x000126CE
		public void LogError(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, string format, params object[] arguments)
		{
			this.LogError(logEntryId, trace, 0L, format, arguments);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000144E0 File Offset: 0x000126E0
		public void LogRawData(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.RawData, Guid.Empty, null, null, null, format, arguments);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000144FF File Offset: 0x000126FF
		public void LogRawData(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, long id, string format, params object[] arguments)
		{
			this.LogRawData(logEntryId, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeTracerMessage(format, arguments));
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00014526 File Offset: 0x00012726
		public void LogRawData(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, string format, params object[] arguments)
		{
			this.LogRawData(logEntryId, trace, 0L, format, arguments);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00014538 File Offset: 0x00012738
		public void LogDebugging(TSLID logEntryId, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Debugging, Guid.Empty, null, null, null, format, arguments);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00014558 File Offset: 0x00012758
		public void LogDebugging(TSLID logEntryId, Guid subscriptionGuid, Guid mailboxGuid, string format, params object[] arguments)
		{
			this.LogEvent(logEntryId, SyncLoggingLevel.Debugging, subscriptionGuid, mailboxGuid, null, null, format, arguments);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001457A File Offset: 0x0001277A
		public void LogDebugging(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, long id, string format, params object[] arguments)
		{
			this.LogDebugging(logEntryId, format, arguments);
			if (trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				trace.TraceDebug(id, this.MakeTracerMessage(format, arguments));
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000145A1 File Offset: 0x000127A1
		public void LogDebugging(TSLID logEntryId, Microsoft.Exchange.Diagnostics.Trace trace, string format, params object[] arguments)
		{
			this.LogDebugging(logEntryId, trace, 0L, format, arguments);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000145B0 File Offset: 0x000127B0
		public void Log(TSLID logEntryId, SyncLoggingLevel loggingLevel, byte[] data, string context)
		{
			this.LogEvent(logEntryId, loggingLevel, Guid.Empty, null, null, data, context, new object[0]);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000145D8 File Offset: 0x000127D8
		public SyncLogSession OpenWithContext(Guid mailboxGuid, AggregationSubscription subscription)
		{
			SyncLogSession syncLogSession = this.syncLog.OpenSession(mailboxGuid, subscription.SubscriptionType, subscription.SubscriptionGuid);
			this.ShareBlackBoxWith(syncLogSession);
			return syncLogSession;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00014606 File Offset: 0x00012806
		public SyncLogSession OpenWithContext(Guid mailboxId, Guid subscriptionId)
		{
			return this.syncLog.OpenSession(mailboxId, subscriptionId);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00014618 File Offset: 0x00012818
		public SyncLogSession OpenWithContext(Guid subscriptionId)
		{
			SyncLogSession syncLogSession = this.syncLog.OpenSession(Guid.Empty, subscriptionId);
			this.ShareBlackBoxWith(syncLogSession);
			return syncLogSession;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001463F File Offset: 0x0001283F
		public void LogItemLevelError(TSLID logEntryId, params KeyValuePair<string, object>[] propertyBag)
		{
			this.LogEventWithProperyBag(logEntryId, SyncLoggingLevel.Error, Guid.Empty, null, "ItemLevelError", propertyBag);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00014658 File Offset: 0x00012858
		protected void LogEventWithProperyBag(TSLID logEntryId, SyncLoggingLevel loggingLevel, Guid subscriptionGuid, object user, string eventId, params KeyValuePair<string, object>[] propertyBag)
		{
			if (this.syncLog.LoggingLevel < loggingLevel)
			{
				return;
			}
			if (propertyBag != null)
			{
				for (int i = 0; i < propertyBag.Length; i++)
				{
					if (propertyBag[i].Value is string)
					{
						propertyBag.SetValue(new KeyValuePair<string, object>(propertyBag[i].Key, propertyBag[i].Value), i);
					}
				}
			}
			this.InternalLogEvent(logEntryId, loggingLevel, subscriptionGuid, user, null, null, eventId, propertyBag);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000146D8 File Offset: 0x000128D8
		protected void LogEvent(TSLID logEntryId, SyncLoggingLevel loggingLevel, Guid subscriptionGuid, object user, string prefix, byte[] data, string format, params object[] arguments)
		{
			if (this.syncLog.LoggingLevel < loggingLevel)
			{
				return;
			}
			string context = null;
			if (format != null)
			{
				if (arguments != null)
				{
					try
					{
						context = string.Format(CultureInfo.InvariantCulture, format, arguments);
						goto IL_3C;
					}
					catch (FormatException exception)
					{
						this.ReportWatson("Malformed logging format found.", exception);
						return;
					}
				}
				context = format;
			}
			IL_3C:
			this.InternalLogEvent(logEntryId, loggingLevel, subscriptionGuid, user, prefix, data, context, null);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00014744 File Offset: 0x00012944
		protected void InternalLogEvent(TSLID logEntryId, SyncLoggingLevel loggingLevel, Guid subscriptionGuid, object user, string prefix, byte[] data, string context, KeyValuePair<string, object>[] propertyBag)
		{
			if (this.syncLog.LoggingLevel < loggingLevel)
			{
				return;
			}
			lock (this.syncRoot)
			{
				object obj2 = null;
				object value = null;
				try
				{
					if (user != null)
					{
						obj2 = this.row[3];
						this.row[3] = user.ToString();
					}
					if (!object.Equals(subscriptionGuid, Guid.Empty))
					{
						value = this.row[4];
						this.row[4] = subscriptionGuid.ToString();
					}
					this.row[2] = (int)loggingLevel;
					this.row[1] = Thread.CurrentThread.ManagedThreadId;
					this.row[6] = logEntryId.ToString();
					this.row[8] = propertyBag;
					if (data == null)
					{
						this.row[7] = prefix + context;
						this.syncLog.Append(this.row);
						this.blackBox.Append(this.row);
					}
					else
					{
						int num = 0;
						string line;
						do
						{
							int num2 = data.Length + " - ".Length;
							if (context != null)
							{
								num2 += context.Length;
							}
							if (prefix != null)
							{
								num2 += prefix.Length;
							}
							StringBuilder stringBuilder = new StringBuilder(num2);
							line = SyncLogSession.GetLine(data, num);
							if (line != null)
							{
								num += line.Length + 2;
							}
							if (line != null || (line == null && num == 0))
							{
								stringBuilder.Append(prefix);
								stringBuilder.Append(line);
								stringBuilder.Append(" - ");
								stringBuilder.Append(context);
								this.row[7] = stringBuilder.ToString();
								this.syncLog.Append(this.row);
								this.blackBox.Append(this.row);
							}
						}
						while (line != null);
					}
				}
				finally
				{
					if (obj2 != null)
					{
						this.row[3] = obj2;
					}
					if (!object.Equals(subscriptionGuid, Guid.Empty))
					{
						this.row[4] = value;
					}
					this.row[6] = string.Empty;
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000149B8 File Offset: 0x00012BB8
		private static string GetLine(byte[] buffer, int start)
		{
			int i = start;
			int num = -1;
			byte[] array = null;
			if (buffer == null)
			{
				return null;
			}
			while (i < buffer.Length)
			{
				i = SyncLogSession.IndexOf(buffer, 10, i);
				if (i == -1)
				{
					num = buffer.Length - start;
					break;
				}
				if (i > start && buffer[i - 1] == 13)
				{
					num = i - start + 1 - 2;
					break;
				}
				i++;
			}
			if (num > 0)
			{
				if (start == 0 && num == buffer.Length)
				{
					array = buffer;
				}
				else
				{
					array = new byte[num];
					Buffer.BlockCopy(buffer, start, array, 0, num);
				}
			}
			if (array == null)
			{
				return null;
			}
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00014A39 File Offset: 0x00012C39
		private static int IndexOf(byte[] buffer, byte val, int offset)
		{
			return ExBuffer.IndexOf(buffer, val, offset, buffer.Length - offset);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00014A48 File Offset: 0x00012C48
		private string MakeTracerMessage(string format, object[] arguments)
		{
			string result;
			try
			{
				result = string.Format(SyncLogSession.TracerFormatProvider.Instance, format, arguments);
			}
			catch (FormatException exception)
			{
				this.ReportWatson("Malformed logging format found.", exception);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00014A8C File Offset: 0x00012C8C
		private void ShareBlackBoxWith(SyncLogSession newSession)
		{
			newSession.blackBox = this.blackBox;
		}

		// Token: 0x040001AC RID: 428
		private const string Connect = "+";

		// Token: 0x040001AD RID: 429
		private const string Disconnect = "-";

		// Token: 0x040001AE RID: 430
		private const string Send = ">";

		// Token: 0x040001AF RID: 431
		private const string Receive = "<";

		// Token: 0x040001B0 RID: 432
		private const string Separator = " - ";

		// Token: 0x040001B1 RID: 433
		private const string ItemLevelError = "ItemLevelError";

		// Token: 0x040001B2 RID: 434
		private static readonly SyncLogSession inmemorySyncLogSession = SyncLog.InMemorySyncLog.OpenSession();

		// Token: 0x040001B3 RID: 435
		private SyncLog syncLog;

		// Token: 0x040001B4 RID: 436
		private SyncLogBlackBox blackBox;

		// Token: 0x040001B5 RID: 437
		private LogRowFormatter row;

		// Token: 0x040001B6 RID: 438
		private object syncRoot = new object();

		// Token: 0x040001B7 RID: 439
		private bool skipDebugAsserts;

		// Token: 0x02000081 RID: 129
		internal sealed class TracerFormatProvider : IFormatProvider, ICustomFormatter
		{
			// Token: 0x0600036C RID: 876 RVA: 0x00014AAB File Offset: 0x00012CAB
			private TracerFormatProvider()
			{
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600036D RID: 877 RVA: 0x00014AB3 File Offset: 0x00012CB3
			public static SyncLogSession.TracerFormatProvider Instance
			{
				get
				{
					return SyncLogSession.TracerFormatProvider.instance;
				}
			}

			// Token: 0x0600036E RID: 878 RVA: 0x00014ABA File Offset: 0x00012CBA
			public object GetFormat(Type formatType)
			{
				if (formatType == typeof(ICustomFormatter))
				{
					return this;
				}
				return CultureInfo.InvariantCulture.GetFormat(formatType);
			}

			// Token: 0x0600036F RID: 879 RVA: 0x00014ADC File Offset: 0x00012CDC
			public string Format(string format, object argument, IFormatProvider formatProvider)
			{
				Exception ex = argument as Exception;
				if (ex != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "[{0}: {1}]", new object[]
					{
						ex.GetType().FullName,
						ex.Message
					});
				}
				IFormattable formattable = argument as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(format, formatProvider);
				}
				if (argument == null)
				{
					return "<null>";
				}
				return argument.ToString();
			}

			// Token: 0x040001B8 RID: 440
			private const string FormattedNull = "<null>";

			// Token: 0x040001B9 RID: 441
			private static SyncLogSession.TracerFormatProvider instance = new SyncLogSession.TracerFormatProvider();
		}
	}
}
