using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000183 RID: 387
	internal class ItemStateTransition
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x0004BD59 File Offset: 0x00049F59
		public ItemStateTransition(MigrationUserStatus status, SubscriptionStatusChangedResponse response, LocalizedException error) : this(status, response, error, true)
		{
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004BD65 File Offset: 0x00049F65
		public ItemStateTransition(MigrationUserStatus status, SubscriptionStatusChangedResponse response, LocalizedException error, bool supportsIncrementalSync)
		{
			this.response = response;
			this.status = status;
			this.error = error;
			this.SupportsIncrementalSync = supportsIncrementalSync;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004BD8A File Offset: 0x00049F8A
		public MigrationUserStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004BD92 File Offset: 0x00049F92
		public SubscriptionStatusChangedResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004BD9A File Offset: 0x00049F9A
		public LocalizedException Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004BDA2 File Offset: 0x00049FA2
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0004BDAA File Offset: 0x00049FAA
		public bool SupportsIncrementalSync { get; private set; }

		// Token: 0x06001220 RID: 4640 RVA: 0x0004BDB4 File Offset: 0x00049FB4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ItemTransition: Status: {0}, Response: {1}, Message: {2}", new object[]
			{
				this.Status,
				this.Response,
				this.Error
			});
		}

		// Token: 0x04000650 RID: 1616
		private readonly SubscriptionStatusChangedResponse response;

		// Token: 0x04000651 RID: 1617
		private readonly MigrationUserStatus status;

		// Token: 0x04000652 RID: 1618
		private readonly LocalizedException error;
	}
}
