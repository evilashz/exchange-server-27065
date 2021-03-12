using System;
using System.Text;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000032 RID: 50
	internal class InferenceClassificationTracking
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0000345C File Offset: 0x0000165C
		private InferenceClassificationTracking(int maxTraceLength)
		{
			if (maxTraceLength < 0)
			{
				throw new ArgumentException("maxTraceLength");
			}
			this.traceBuilder = new StringBuilder(0);
			this.traceOverflow = false;
			this.maxTraceLength = maxTraceLength;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000348D File Offset: 0x0000168D
		public static InferenceClassificationTracking Create()
		{
			return new InferenceClassificationTracking(10240);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003499 File Offset: 0x00001699
		public static InferenceClassificationTracking Create(int maxTraceLength)
		{
			return new InferenceClassificationTracking(maxTraceLength);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000034A4 File Offset: 0x000016A4
		public override string ToString()
		{
			string text = this.traceBuilder.ToString();
			if (this.traceOverflow)
			{
				text = text.Substring(0, this.maxTraceLength - "...".Length);
				text += "...";
			}
			return text;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000034EB File Offset: 0x000016EB
		public void Trace(string key, string value)
		{
			Util.ThrowOnNullArgument(key, "key");
			this.Trace(string.Format("{0}={1}", key, value ?? "<null>"));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003513 File Offset: 0x00001713
		public void Trace(string key, object value)
		{
			this.Trace(key, (value == null) ? null : value.ToString());
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003528 File Offset: 0x00001728
		public void Trace(string value)
		{
			Util.ThrowOnNullArgument(value, "value");
			if (this.traceOverflow)
			{
				this.traceOverflow = true;
				return;
			}
			if (this.traceBuilder.Length > 0)
			{
				this.traceBuilder.Append(";");
			}
			this.traceBuilder.Append(value);
			if (this.traceBuilder.Length >= this.maxTraceLength)
			{
				this.traceOverflow = true;
			}
		}

		// Token: 0x040000C5 RID: 197
		public const string ServerVersion = "SV";

		// Token: 0x040000C6 RID: 198
		public const string ServerName = "SN";

		// Token: 0x040000C7 RID: 199
		public const string ModelVersion = "MV";

		// Token: 0x040000C8 RID: 200
		public const string MessageClassificationTime = "CT";

		// Token: 0x040000C9 RID: 201
		public const string TimeTakenToClassify = "TTC";

		// Token: 0x040000CA RID: 202
		public const string TimeTakenToInfer = "TI";

		// Token: 0x040000CB RID: 203
		public const string PredictedActions = "PA";

		// Token: 0x040000CC RID: 204
		public const string FeatureValuesWeights = "FVW";

		// Token: 0x040000CD RID: 205
		public const string FolderPredictionTimeTakenToInfer = "FPTI";

		// Token: 0x040000CE RID: 206
		public const string PredictedTopFolders = "PTF";

		// Token: 0x040000CF RID: 207
		public const string FolderPredictionFeatureValuesWeights = "FPFVW";

		// Token: 0x040000D0 RID: 208
		public const string PredictedActionsThresholds = "PAT";

		// Token: 0x040000D1 RID: 209
		public const string OriginalDeliveryFolder = "ODF";

		// Token: 0x040000D2 RID: 210
		public const string ConversationClutterState = "CCS";

		// Token: 0x040000D3 RID: 211
		public const string ConversationPreviousMessageCount = "CPMC";

		// Token: 0x040000D4 RID: 212
		public const string MarkedAsBulk = "MAB";

		// Token: 0x040000D5 RID: 213
		public const string ClutterValueBeforeOverrideRules = "CVBOR";

		// Token: 0x040000D6 RID: 214
		public const string ClutterValueAfterOverrideRules = "CVAOR";

		// Token: 0x040000D7 RID: 215
		private const int DefaultMaxTraceLength = 10240;

		// Token: 0x040000D8 RID: 216
		private const string Ellipsis = "...";

		// Token: 0x040000D9 RID: 217
		private StringBuilder traceBuilder;

		// Token: 0x040000DA RID: 218
		private bool traceOverflow;

		// Token: 0x040000DB RID: 219
		private readonly int maxTraceLength;
	}
}
