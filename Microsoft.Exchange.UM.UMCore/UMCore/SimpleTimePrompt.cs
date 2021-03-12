using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D3 RID: 467
	internal class SimpleTimePrompt : VariablePrompt<ExDateTime>
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003C4C2 File Offset: 0x0003A6C2
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0003C4CA File Offset: 0x0003A6CA
		protected ExDateTime Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0003C4D4 File Offset: 0x0003A6D4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"time",
				base.Config.PromptName,
				string.Empty,
				this.time.ToString("t", base.Culture)
			});
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0003C52F File Offset: 0x0003A72F
		internal override string ToSsml()
		{
			return this.ssmlString;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003C538 File Offset: 0x0003A738
		protected override void InternalInitialize()
		{
			this.time = base.InitVal;
			this.ssmlString = SpokenDateTimeFormatter.ToSsml(base.Culture, this.time, SpokenDateTimeFormatter.GetSpokenTimeFormat(base.Culture), this.time.ToString("t", base.Culture));
		}

		// Token: 0x04000A9A RID: 2714
		private ExDateTime time;

		// Token: 0x04000A9B RID: 2715
		private string ssmlString;
	}
}
