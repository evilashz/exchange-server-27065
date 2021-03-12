using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Drain;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabaseProcessor
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000AE1A File Offset: 0x0000901A
		public DatabaseProcessor(ILoadBalanceSettings settings, DatabaseDrainControl drainControl, ILogger logger, DirectoryDatabase database)
		{
			this.settings = settings;
			this.drainControl = drainControl;
			this.logger = logger;
			this.database = database;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000AE40 File Offset: 0x00009040
		public void ProcessDatabase()
		{
			if (!this.settings.AutomaticDatabaseDrainEnabled)
			{
				return;
			}
			if (this.database == null)
			{
				return;
			}
			ByteQuantifiedSize currentPhysicalSize = this.database.GetSize().CurrentPhysicalSize;
			double num = 1.0 + (double)this.settings.AutomaticDrainStartFileSizePercent / 100.0;
			ulong bytesValue = (ulong)(this.database.MaximumSize.ToBytes() * num);
			if (currentPhysicalSize >= ByteQuantifiedSize.FromBytes(bytesValue))
			{
				this.logger.LogInformation("Database {0} has {1} EDB file size, and the maximum allowed size is {2} with a {3}% tolerance. Starting drain process.", new object[]
				{
					this.database.Identity,
					currentPhysicalSize,
					this.database.MaximumSize,
					this.settings.AutomaticDrainStartFileSizePercent
				});
				this.drainControl.BeginDrainDatabase(this.database);
			}
		}

		// Token: 0x04000140 RID: 320
		private readonly DirectoryDatabase database;

		// Token: 0x04000141 RID: 321
		private readonly DatabaseDrainControl drainControl;

		// Token: 0x04000142 RID: 322
		private readonly ILogger logger;

		// Token: 0x04000143 RID: 323
		private readonly ILoadBalanceSettings settings;
	}
}
