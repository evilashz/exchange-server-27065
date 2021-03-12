using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	internal class AirSyncFatalException : LocalizedException
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000CB76 File Offset: 0x0000AD76
		internal AirSyncFatalException(LocalizedString errorMessage, string loggerString, bool watsonReportEnabled) : this(errorMessage, loggerString, watsonReportEnabled, null)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CB82 File Offset: 0x0000AD82
		internal AirSyncFatalException(LocalizedString errorMessage, string loggerString, bool watsonReportEnabled, Exception innerException) : base(errorMessage, innerException)
		{
			if (string.IsNullOrEmpty(loggerString))
			{
				throw new ArgumentException("loggerstring cannot be null or empty");
			}
			this.loggerString = loggerString;
			this.watsonReportEnabled = watsonReportEnabled;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		protected AirSyncFatalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.watsonReportEnabled = info.GetBoolean("watsonReportEnabled");
			this.loggerString = info.GetString("loggerString");
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		internal string LoggerString
		{
			get
			{
				return this.loggerString;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000CBE2 File Offset: 0x0000ADE2
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		internal bool WatsonReportEnabled
		{
			get
			{
				return this.watsonReportEnabled;
			}
			set
			{
				this.watsonReportEnabled = value;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("watsonReportEnabled", this.watsonReportEnabled);
			info.AddValue("loggerString", this.loggerString);
		}

		// Token: 0x04000209 RID: 521
		private string loggerString;

		// Token: 0x0400020A RID: 522
		private bool watsonReportEnabled;
	}
}
