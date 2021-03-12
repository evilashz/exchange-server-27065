using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200004B RID: 75
	internal sealed class RulesTrackingLog
	{
		// Token: 0x060002DD RID: 733 RVA: 0x000109FC File Offset: 0x0000EBFC
		private RulesTrackingLog()
		{
			string[] array = new string[Enum.GetValues(typeof(RulesTrackingLog.Field)).Length];
			array[0] = "date-time";
			array[1] = "message-id";
			array[2] = "rule-name";
			array[3] = "details";
			array[4] = "action";
			array[5] = "from-address";
			array[6] = "recipient-address";
			this.rulesTrackingSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Rules Tracking Log", array);
			this.log = new Log("RULESTRK", new LogHeaderFormatter(this.rulesTrackingSchema), "RulesTrackingLogs");
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		internal static RulesTrackingLog GetLog()
		{
			if (RulesTrackingLog.trackingLog != null)
			{
				return RulesTrackingLog.trackingLog;
			}
			RulesTrackingLog result;
			lock (RulesTrackingLog.lockVar)
			{
				if (RulesTrackingLog.trackingLog == null)
				{
					RulesTrackingLog.trackingLog = new RulesTrackingLog();
				}
				result = RulesTrackingLog.trackingLog;
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010B40 File Offset: 0x0000ED40
		internal void Close()
		{
			this.log.Close();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010B50 File Offset: 0x0000ED50
		internal void TrackRuleAction(string actionType, string ruleName, string details, MailMessage message)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.rulesTrackingSchema);
			logRowFormatter[1] = message.MessageId;
			logRowFormatter[2] = ruleName;
			logRowFormatter[3] = details;
			logRowFormatter[4] = actionType;
			logRowFormatter[5] = message.EnvelopeFrom;
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			foreach (string value in message.EnvelopeRecipients)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			if (stringBuilder.Length > 0)
			{
				logRowFormatter[6] = stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			else
			{
				logRowFormatter[6] = string.Empty;
			}
			if (this.rulesTrackingLogPath != Configuration.TransportServer.PipelineTracingPath.PathName)
			{
				this.rulesTrackingLogPath = Configuration.TransportServer.PipelineTracingPath.PathName;
				this.Configure();
			}
			this.Append(logRowFormatter);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010C68 File Offset: 0x0000EE68
		private void Append(LogRowFormatter row)
		{
			this.log.Append(row, 0);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010C78 File Offset: 0x0000EE78
		private void Configure()
		{
			this.log.Configure(Path.Combine(this.rulesTrackingLogPath, "RulesTracking\\"), this.maxAgeInDays, (long)this.maxDirectorySizeInBytes.Value.ToBytes(), (long)this.maxPerFileSizeInBytes.Value.ToBytes());
		}

		// Token: 0x040001D3 RID: 467
		private static readonly object lockVar = new object();

		// Token: 0x040001D4 RID: 468
		private static RulesTrackingLog trackingLog;

		// Token: 0x040001D5 RID: 469
		private TimeSpan maxAgeInDays = new TimeSpan(30, 0, 0, 0);

		// Token: 0x040001D6 RID: 470
		private Unlimited<ByteQuantifiedSize> maxDirectorySizeInBytes = ByteQuantifiedSize.FromMB(25UL);

		// Token: 0x040001D7 RID: 471
		private Unlimited<ByteQuantifiedSize> maxPerFileSizeInBytes = ByteQuantifiedSize.FromMB(2UL);

		// Token: 0x040001D8 RID: 472
		private string rulesTrackingLogPath;

		// Token: 0x040001D9 RID: 473
		private LogSchema rulesTrackingSchema;

		// Token: 0x040001DA RID: 474
		private Log log;

		// Token: 0x0200004C RID: 76
		private enum Field
		{
			// Token: 0x040001DC RID: 476
			DateTime,
			// Token: 0x040001DD RID: 477
			MessageID,
			// Token: 0x040001DE RID: 478
			RuleName,
			// Token: 0x040001DF RID: 479
			Details,
			// Token: 0x040001E0 RID: 480
			Action,
			// Token: 0x040001E1 RID: 481
			FromAddress,
			// Token: 0x040001E2 RID: 482
			RecipientAddress
		}
	}
}
