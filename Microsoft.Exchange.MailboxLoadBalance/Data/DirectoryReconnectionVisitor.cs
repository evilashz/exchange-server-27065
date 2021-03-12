using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DirectoryReconnectionVisitor : ILoadEntityVisitor
	{
		// Token: 0x06000279 RID: 633 RVA: 0x000081E0 File Offset: 0x000063E0
		public DirectoryReconnectionVisitor(IDirectoryProvider directory, ILogger logger)
		{
			this.directory = directory;
			this.logger = logger;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000081F6 File Offset: 0x000063F6
		public bool Visit(LoadContainer container)
		{
			this.PopulateDirectoryObjectFromIdentity(container);
			return true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008200 File Offset: 0x00006400
		public bool Visit(LoadEntity entity)
		{
			this.PopulateDirectoryObjectFromIdentity(entity);
			return true;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000820C File Offset: 0x0000640C
		private void PopulateDirectoryObjectFromIdentity(LoadEntity entity)
		{
			using (OperationTracker.Create(this.logger, "Re-hydrating directory object of type {0} on load entity.", new object[]
			{
				entity.DirectoryObjectIdentity.ObjectType
			}))
			{
				if (entity.DirectoryObjectIdentity != null)
				{
					try
					{
						entity.DirectoryObject = this.directory.GetDirectoryObject(entity.DirectoryObjectIdentity);
					}
					catch (LocalizedException exception)
					{
						this.logger.LogError(exception, "Failed to rehydrate object with identity '{0}'.", new object[]
						{
							entity.DirectoryObjectIdentity
						});
					}
				}
			}
		}

		// Token: 0x040000B4 RID: 180
		private readonly IDirectoryProvider directory;

		// Token: 0x040000B5 RID: 181
		private readonly ILogger logger;
	}
}
