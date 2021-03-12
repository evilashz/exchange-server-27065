using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000071 RID: 113
	[DataContract]
	internal class DirectoryDatabase : DirectoryObject
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000AFEC File Offset: 0x000091EC
		public DirectoryDatabase(IDirectoryProvider directory, DirectoryIdentity identity, IClientFactory clientFactory, bool isExcludedFromProvisioning, bool isExcludedFromInitialProvisioning, MailboxProvisioningAttributes mailboxProvisioningAttributes = null) : base(directory, identity)
		{
			this.clientFactory = clientFactory;
			this.IsExcludedFromProvisioning = isExcludedFromProvisioning;
			this.IsExcludedFromInitialProvisioning = isExcludedFromInitialProvisioning;
			this.MailboxProvisioningAttributes = mailboxProvisioningAttributes;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B015 File Offset: 0x00009215
		public IEnumerable<DirectoryServer> ActivationOrder
		{
			get
			{
				return base.Directory.GetActivationPreferenceForDatabase(this);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000B023 File Offset: 0x00009223
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000B02B File Offset: 0x0000922B
		[DataMember]
		public bool IsExcludedFromInitialProvisioning { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000B034 File Offset: 0x00009234
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000B03C File Offset: 0x0000923C
		[DataMember]
		public bool IsExcludedFromProvisioning { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000B045 File Offset: 0x00009245
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000B04D File Offset: 0x0000924D
		[DataMember]
		public MailboxProvisioningAttributes MailboxProvisioningAttributes { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000B056 File Offset: 0x00009256
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000B05E File Offset: 0x0000925E
		[IgnoreDataMember]
		public ByteQuantifiedSize MaximumSize { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B067 File Offset: 0x00009267
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000B06F File Offset: 0x0000926F
		[DataMember]
		public int RelativeLoadCapacity { get; set; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B078 File Offset: 0x00009278
		public virtual IEnumerable<NonConnectedMailbox> GetDisconnectedMailboxes()
		{
			return base.Directory.GetDisconnectedMailboxesForDatabase(this);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000B086 File Offset: 0x00009286
		public virtual DirectoryMailbox GetMailbox(DirectoryIdentity identity)
		{
			return base.Directory.GetDirectoryObject(identity) as DirectoryMailbox;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000B099 File Offset: 0x00009299
		public virtual IEnumerable<DirectoryMailbox> GetMailboxes()
		{
			return base.Directory.GetMailboxesForDatabase(this);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public virtual DatabaseSizeInfo GetSize()
		{
			DatabaseSizeInfo databaseSizeInformation;
			using (ILoadBalanceService loadBalanceClientForDatabase = this.clientFactory.GetLoadBalanceClientForDatabase(this))
			{
				databaseSizeInformation = loadBalanceClientForDatabase.GetDatabaseSizeInformation(base.Identity);
			}
			return databaseSizeInformation;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000B0EC File Offset: 0x000092EC
		public LoadContainer ToLoadContainer()
		{
			DatabaseSizeInfo size = this.GetSize();
			LoadContainer loadContainer = new LoadContainer(this, ContainerType.Database);
			loadContainer.RelativeLoadWeight = this.RelativeLoadCapacity;
			loadContainer.CanAcceptRegularLoad = (!this.IsExcludedFromProvisioning && size.CurrentPhysicalSize < this.MaximumSize);
			loadContainer.CanAcceptBalancingLoad = (!this.IsExcludedFromInitialProvisioning && loadContainer.CanAcceptRegularLoad);
			loadContainer.MaximumLoad[PhysicalSize.Instance] = (long)this.MaximumSize.ToBytes();
			loadContainer.MaximumLoad[LogicalSize.Instance] = (long)this.MaximumSize.ToBytes();
			loadContainer.ReusableCapacity[LogicalSize.Instance] = (long)size.AvailableWhitespace.ToBytes();
			loadContainer.ReusableCapacity[PhysicalSize.Instance] = (long)size.AvailableWhitespace.ToBytes();
			ByteQuantifiedSize byteQuantifiedSize = size.CurrentPhysicalSize - size.AvailableWhitespace;
			loadContainer.ConsumedLoad[LogicalSize.Instance] = (long)byteQuantifiedSize.ToBytes();
			loadContainer.ConsumedLoad[PhysicalSize.Instance] = (long)byteQuantifiedSize.ToBytes();
			return loadContainer;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000B20C File Offset: 0x0000940C
		public virtual bool IsOwnedBy(DirectoryIdentity directoryIdentity)
		{
			DirectoryServer directoryServer = this.ActivationOrder.First<DirectoryServer>();
			return directoryServer != null && directoryServer.Identity.Equals(directoryIdentity);
		}

		// Token: 0x04000146 RID: 326
		private readonly IClientFactory clientFactory;
	}
}
