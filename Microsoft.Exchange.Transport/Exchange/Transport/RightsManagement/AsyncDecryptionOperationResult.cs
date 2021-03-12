using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D2 RID: 978
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AsyncDecryptionOperationResult<TData> : AsyncOperationResult<TData>
	{
		// Token: 0x06002CC3 RID: 11459 RVA: 0x000B2540 File Offset: 0x000B0740
		public AsyncDecryptionOperationResult(TData data, Exception exception) : base(data, exception)
		{
			if (base.Exception != null)
			{
				if (base.Exception is StorageTransientException)
				{
					this.isTransientException = true;
					this.isKnownException = true;
					return;
				}
				if (base.Exception is StoragePermanentException)
				{
					this.isTransientException = false;
					this.isKnownException = true;
					return;
				}
				if (base.Exception is RightsManagementException)
				{
					this.isTransientException = !(base.Exception as RightsManagementException).IsPermanent;
					this.isKnownException = true;
					return;
				}
				if (base.Exception is ExchangeConfigurationException)
				{
					this.isTransientException = true;
					this.isKnownException = true;
					return;
				}
				this.isTransientException = false;
				this.isKnownException = false;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000B25F0 File Offset: 0x000B07F0
		public override bool IsTransientException
		{
			get
			{
				return this.isTransientException;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000B25F8 File Offset: 0x000B07F8
		public bool IsKnownException
		{
			get
			{
				return this.isKnownException;
			}
		}

		// Token: 0x04001656 RID: 5718
		private readonly bool isTransientException;

		// Token: 0x04001657 RID: 5719
		private readonly bool isKnownException;
	}
}
