using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000070 RID: 112
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal abstract class DirectoryContainerParent : DirectoryObject
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000AF2A File Offset: 0x0000912A
		protected DirectoryContainerParent(IDirectoryProvider directory, DirectoryIdentity identity) : base(directory, identity)
		{
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000AF40 File Offset: 0x00009140
		public IEnumerable<DirectoryObject> Children
		{
			get
			{
				if (this.ShouldRetrieveChildren)
				{
					List<DirectoryObject> list = new List<DirectoryObject>();
					foreach (DirectoryObject directoryObject in this.FetchChildren())
					{
						directoryObject.Parent = this;
						list.Add(directoryObject);
					}
					this.children = list;
					this.childrenRetrievalTimestamp = ExDateTime.UtcNow;
				}
				return this.children;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000AFBC File Offset: 0x000091BC
		protected virtual bool ShouldRetrieveChildren
		{
			get
			{
				return this.children == null || this.childrenRetrievalTimestamp.Add(LoadBalanceADSettings.Instance.Value.LocalCacheRefreshPeriod) < ExDateTime.UtcNow;
			}
		}

		// Token: 0x060003DC RID: 988
		protected abstract IEnumerable<DirectoryObject> FetchChildren();

		// Token: 0x04000144 RID: 324
		private IList<DirectoryObject> children;

		// Token: 0x04000145 RID: 325
		private ExDateTime childrenRetrievalTimestamp = ExDateTime.MinValue;
	}
}
