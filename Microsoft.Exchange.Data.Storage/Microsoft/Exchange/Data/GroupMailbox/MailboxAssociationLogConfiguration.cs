using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007F3 RID: 2035
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxAssociationLogConfiguration : LogConfigurationBase
	{
		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x0013C2AD File Offset: 0x0013A4AD
		protected override string Component
		{
			get
			{
				return "MailboxAssociationLog";
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0013C2B4 File Offset: 0x0013A4B4
		protected override string Type
		{
			get
			{
				return "Mailbox Association Log";
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0013C2BB File Offset: 0x0013A4BB
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ModernGroupsTracer;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x0013C2C2 File Offset: 0x0013A4C2
		public static MailboxAssociationLogConfiguration Default
		{
			get
			{
				if (MailboxAssociationLogConfiguration.defaultInstance == null)
				{
					MailboxAssociationLogConfiguration.defaultInstance = new MailboxAssociationLogConfiguration();
				}
				return MailboxAssociationLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x04002971 RID: 10609
		private static MailboxAssociationLogConfiguration defaultInstance;
	}
}
