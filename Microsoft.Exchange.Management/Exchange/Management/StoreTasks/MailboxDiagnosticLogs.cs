using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079D RID: 1949
	[Serializable]
	public sealed class MailboxDiagnosticLogs : ConfigurableObject
	{
		// Token: 0x060044AF RID: 17583 RVA: 0x0011A01F File Offset: 0x0011821F
		public MailboxDiagnosticLogs() : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.ResetChangeTracking();
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x0011A041 File Offset: 0x00118241
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x0011A053 File Offset: 0x00118253
		public string MailboxLog
		{
			get
			{
				return (string)this[MailboxDiagnosticLogsSchema.MailboxLog];
			}
			set
			{
				this[MailboxDiagnosticLogsSchema.MailboxLog] = value;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x0011A061 File Offset: 0x00118261
		// (set) Token: 0x060044B3 RID: 17587 RVA: 0x0011A073 File Offset: 0x00118273
		public string LogName
		{
			get
			{
				return (string)this[MailboxDiagnosticLogsSchema.LogName];
			}
			set
			{
				this[MailboxDiagnosticLogsSchema.LogName] = value;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0011A081 File Offset: 0x00118281
		public override ObjectId Identity
		{
			get
			{
				return (ObjectId)this[SimpleProviderObjectSchema.Identity];
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x0011A093 File Offset: 0x00118293
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxDiagnosticLogs.schema;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x0011A09A File Offset: 0x0011829A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04002A77 RID: 10871
		private static MailboxDiagnosticLogsSchema schema = ObjectSchema.GetInstance<MailboxDiagnosticLogsSchema>();
	}
}
