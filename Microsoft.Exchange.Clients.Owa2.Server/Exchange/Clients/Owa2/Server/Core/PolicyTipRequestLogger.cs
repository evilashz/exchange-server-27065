using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200023D RID: 573
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PolicyTipRequestLogger
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x0004D07B File Offset: 0x0004B27B
		private PolicyTipRequestLogger(string correlationId)
		{
			this.correlationId = (correlationId ?? string.Empty);
			this.creationTime = DateTime.UtcNow;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004D09E File Offset: 0x0004B29E
		internal static PolicyTipRequestLogger CreateInstance(string correlationId)
		{
			return new PolicyTipRequestLogger(correlationId);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004D0A8 File Offset: 0x0004B2A8
		internal static List<string> MarkAsPII(List<string> data)
		{
			if (data == null)
			{
				return data;
			}
			for (int i = 0; i < data.Count; i++)
			{
				data[i] = PolicyTipRequestLogger.MarkAsPII(data[i]);
			}
			return data;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0004D0DF File Offset: 0x0004B2DF
		internal static string MarkAsPII(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return string.Empty;
			}
			return string.Format("<PII>{0}</PII>", data);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0004D0FA File Offset: 0x0004B2FA
		internal void StartStage(LogStage stage)
		{
			this.StartStage(stage, DateTime.UtcNow);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0004D108 File Offset: 0x0004B308
		internal void StartStage(LogStage stage, DateTime creationTime)
		{
			this.currentStageLogEntry = new PolicyTipRequestLogger.LogEntry(stage, creationTime);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0004D117 File Offset: 0x0004B317
		internal void EndStageAndTransitionToStage(LogStage stage)
		{
			this.EndStage();
			if (stage == LogStage.SendResponse)
			{
				this.StartStage(stage, this.creationTime);
				return;
			}
			this.StartStage(stage);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0004D13C File Offset: 0x0004B33C
		internal TimeSpan EndStage()
		{
			TimeSpan elapsed = this.currentStageLogEntry.GetElapsed();
			PolicyTipProtocolLog.WriteToLog(this.correlationId, this.currentStageLogEntry.Stage.ToString(), this.currentStageLogEntry.Data, this.currentStageLogEntry.ExtraData, elapsed, this.currentStageLogEntry.OuterExceptionType, this.currentStageLogEntry.OuterExceptionMessage, this.currentStageLogEntry.InnerExceptionType, this.currentStageLogEntry.InnerExceptionMessage, this.currentStageLogEntry.ExceptionChain);
			return elapsed;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0004D1C4 File Offset: 0x0004B3C4
		internal void AppendData(string key, string value)
		{
			this.currentStageLogEntry.AppendData(key, value);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0004D1D3 File Offset: 0x0004B3D3
		internal void AppendExtraData(string key, string value)
		{
			this.currentStageLogEntry.AppendExtraData(key, value);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0004D1E2 File Offset: 0x0004B3E2
		internal void SetException(Exception e)
		{
			this.currentStageLogEntry.SetException(e);
		}

		// Token: 0x04000BEA RID: 3050
		private const string PIIFormat = "<PII>{0}</PII>";

		// Token: 0x04000BEB RID: 3051
		internal const string TrueOr1 = "1";

		// Token: 0x04000BEC RID: 3052
		internal const string FalseOr0 = "0";

		// Token: 0x04000BED RID: 3053
		internal readonly string correlationId;

		// Token: 0x04000BEE RID: 3054
		internal readonly DateTime creationTime;

		// Token: 0x04000BEF RID: 3055
		private PolicyTipRequestLogger.LogEntry currentStageLogEntry;

		// Token: 0x0200023E RID: 574
		private sealed class LogEntry
		{
			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x06001597 RID: 5527 RVA: 0x0004D1F0 File Offset: 0x0004B3F0
			// (set) Token: 0x06001598 RID: 5528 RVA: 0x0004D1F8 File Offset: 0x0004B3F8
			internal LogStage Stage { get; private set; }

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x06001599 RID: 5529 RVA: 0x0004D201 File Offset: 0x0004B401
			// (set) Token: 0x0600159A RID: 5530 RVA: 0x0004D209 File Offset: 0x0004B409
			internal string OuterExceptionType { get; private set; }

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x0600159B RID: 5531 RVA: 0x0004D212 File Offset: 0x0004B412
			// (set) Token: 0x0600159C RID: 5532 RVA: 0x0004D21A File Offset: 0x0004B41A
			internal string OuterExceptionMessage { get; private set; }

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x0600159D RID: 5533 RVA: 0x0004D223 File Offset: 0x0004B423
			// (set) Token: 0x0600159E RID: 5534 RVA: 0x0004D22B File Offset: 0x0004B42B
			internal string InnerExceptionType { get; private set; }

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x0600159F RID: 5535 RVA: 0x0004D234 File Offset: 0x0004B434
			// (set) Token: 0x060015A0 RID: 5536 RVA: 0x0004D23C File Offset: 0x0004B43C
			internal string InnerExceptionMessage { get; private set; }

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0004D245 File Offset: 0x0004B445
			// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0004D24D File Offset: 0x0004B44D
			internal string ExceptionChain { get; private set; }

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0004D256 File Offset: 0x0004B456
			internal string Data
			{
				get
				{
					return this.data.ToString();
				}
			}

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0004D263 File Offset: 0x0004B463
			internal string ExtraData
			{
				get
				{
					if (this.extraData == null)
					{
						return string.Empty;
					}
					return this.extraData.ToString();
				}
			}

			// Token: 0x060015A5 RID: 5541 RVA: 0x0004D27E File Offset: 0x0004B47E
			internal LogEntry(LogStage stage) : this(stage, DateTime.UtcNow)
			{
			}

			// Token: 0x060015A6 RID: 5542 RVA: 0x0004D28C File Offset: 0x0004B48C
			internal LogEntry(LogStage stage, DateTime creationTime)
			{
				this.Stage = stage;
				this.creationTime = creationTime;
			}

			// Token: 0x060015A7 RID: 5543 RVA: 0x0004D2B0 File Offset: 0x0004B4B0
			internal void AppendData(string key, string value)
			{
				if (key != null)
				{
					this.data.Append(key);
					this.data.Append(":");
					this.data.Append(value ?? string.Empty);
					this.data.Append(";");
				}
			}

			// Token: 0x060015A8 RID: 5544 RVA: 0x0004D308 File Offset: 0x0004B508
			internal void AppendExtraData(string key, string value)
			{
				if (key != null)
				{
					if (this.extraData == null)
					{
						this.extraData = new StringBuilder();
					}
					this.extraData.Append(key);
					this.extraData.Append(":");
					this.extraData.Append(value ?? string.Empty);
					this.extraData.Append(";");
				}
			}

			// Token: 0x060015A9 RID: 5545 RVA: 0x0004D370 File Offset: 0x0004B570
			internal void SetException(Exception e)
			{
				if (e != null)
				{
					List<string> list = null;
					List<string> list2 = null;
					string exceptionChain = null;
					PolicyTipProtocolLog.GetExceptionTypeAndDetails(e, out list, out list2, out exceptionChain, false);
					this.OuterExceptionType = list[0];
					this.OuterExceptionMessage = list2[0];
					if (list.Count > 1)
					{
						this.InnerExceptionType = list[list.Count - 1];
						this.InnerExceptionMessage = list2[list2.Count - 1];
					}
					this.ExceptionChain = exceptionChain;
				}
			}

			// Token: 0x060015AA RID: 5546 RVA: 0x0004D3E5 File Offset: 0x0004B5E5
			internal TimeSpan GetElapsed()
			{
				return DateTime.UtcNow - this.creationTime;
			}

			// Token: 0x04000BF0 RID: 3056
			private const string delimiter = ";";

			// Token: 0x04000BF1 RID: 3057
			private const string keyValueDelimiter = ":";

			// Token: 0x04000BF2 RID: 3058
			private readonly DateTime creationTime;

			// Token: 0x04000BF3 RID: 3059
			private StringBuilder data = new StringBuilder();

			// Token: 0x04000BF4 RID: 3060
			private StringBuilder extraData;
		}
	}
}
