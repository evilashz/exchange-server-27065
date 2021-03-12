using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class DirectoryObject
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0000A9F9 File Offset: 0x00008BF9
		public DirectoryObject(IDirectoryProvider directory, DirectoryIdentity identity)
		{
			AnchorUtil.ThrowOnNullArgument(directory, "directory");
			AnchorUtil.ThrowOnNullArgument(identity, "identity");
			this.directory = directory;
			this.Identity = identity;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000AA25 File Offset: 0x00008C25
		public IDirectoryProvider Directory
		{
			get
			{
				return this.directory ?? NullDirectory.Instance;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000AA36 File Offset: 0x00008C36
		public Guid Guid
		{
			get
			{
				return this.Identity.Guid;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000AA43 File Offset: 0x00008C43
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000AA4B File Offset: 0x00008C4B
		[DataMember(Name = "DirectoryObjectIdentity")]
		public DirectoryIdentity Identity { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000AA54 File Offset: 0x00008C54
		public string Name
		{
			get
			{
				return this.Identity.Name;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000AA61 File Offset: 0x00008C61
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000AA69 File Offset: 0x00008C69
		[DataMember]
		public DirectoryObject Parent { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000AA72 File Offset: 0x00008C72
		public virtual bool SupportsMoving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000AA75 File Offset: 0x00008C75
		public virtual IRequest CreateRequestToMove(DirectoryIdentity target, string batchName, ILogger logger)
		{
			throw new NotSupportedException("Directory objects of type " + base.GetType() + " can't be moved.");
		}

		// Token: 0x04000132 RID: 306
		[IgnoreDataMember]
		private readonly IDirectoryProvider directory;
	}
}
