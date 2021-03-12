using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020009AE RID: 2478
	[Serializable]
	public class MailboxStoreIdentity : ObjectId, ISerializable, IEquatable<MailboxStoreIdentity>
	{
		// Token: 0x06005B63 RID: 23395 RVA: 0x0017D930 File Offset: 0x0017BB30
		public MailboxStoreIdentity(ADObjectId mailboxOwnerId)
		{
			this.mailboxOwnerId = mailboxOwnerId;
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0017D93F File Offset: 0x0017BB3F
		public MailboxStoreIdentity() : this(null)
		{
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x0017D948 File Offset: 0x0017BB48
		// (set) Token: 0x06005B66 RID: 23398 RVA: 0x0017D950 File Offset: 0x0017BB50
		public ADObjectId MailboxOwnerId
		{
			get
			{
				return this.mailboxOwnerId;
			}
			internal set
			{
				this.mailboxOwnerId = value;
			}
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0017D959 File Offset: 0x0017BB59
		public override byte[] GetBytes()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0017D960 File Offset: 0x0017BB60
		public override string ToString()
		{
			if (this.mailboxOwnerId != null)
			{
				return this.mailboxOwnerId.ToString();
			}
			return null;
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0017D977 File Offset: 0x0017BB77
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MailboxStoreIdentity);
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0017D985 File Offset: 0x0017BB85
		public virtual bool Equals(MailboxStoreIdentity other)
		{
			return other != null && this.mailboxOwnerId != null && this.mailboxOwnerId.Equals(other.mailboxOwnerId);
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0017D9A5 File Offset: 0x0017BBA5
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0017D9A7 File Offset: 0x0017BBA7
		public override int GetHashCode()
		{
			if (this.mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			return this.mailboxOwnerId.GetHashCode();
		}

		// Token: 0x04003265 RID: 12901
		private ADObjectId mailboxOwnerId;
	}
}
