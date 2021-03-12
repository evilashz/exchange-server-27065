using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001D4 RID: 468
	internal class UMPartnerMinimalTranscriptionContext : UMPartnerContext
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00031B04 File Offset: 0x0002FD04
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x00031B1B File Offset: 0x0002FD1B
		public string TimeoutMessageId
		{
			get
			{
				return (string)base[UMPartnerMinimalTranscriptionContext.Schema.TimeoutMessageId];
			}
			set
			{
				base[UMPartnerMinimalTranscriptionContext.Schema.TimeoutMessageId] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00031B2E File Offset: 0x0002FD2E
		protected override UMPartnerContext.UMPartnerContextSchema ContextSchema
		{
			get
			{
				return UMPartnerMinimalTranscriptionContext.Schema;
			}
		}

		// Token: 0x040009BA RID: 2490
		private static readonly UMPartnerMinimalTranscriptionContext.UMPartnerMinimalTranscriptionContextSchema Schema = new UMPartnerMinimalTranscriptionContext.UMPartnerMinimalTranscriptionContextSchema();

		// Token: 0x020001D5 RID: 469
		private class UMPartnerMinimalTranscriptionContextSchema : UMPartnerContext.UMPartnerContextSchema
		{
			// Token: 0x040009BB RID: 2491
			public readonly UMPartnerContext.UMPartnerContextPropertyDefinition TimeoutMessageId = new UMPartnerContext.UMPartnerContextPropertyDefinition("TimeoutMessageId", typeof(string), string.Empty);
		}
	}
}
