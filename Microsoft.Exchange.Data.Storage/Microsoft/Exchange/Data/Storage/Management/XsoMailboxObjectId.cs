using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7B RID: 2683
	[Serializable]
	public abstract class XsoMailboxObjectId : ObjectId, IEquatable<XsoMailboxObjectId>
	{
		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x0019F311 File Offset: 0x0019D511
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x0019F319 File Offset: 0x0019D519
		public ADObjectId MailboxOwnerId { get; private set; }

		// Token: 0x06006236 RID: 25142 RVA: 0x0019F322 File Offset: 0x0019D522
		public static bool operator ==(XsoMailboxObjectId operand1, XsoMailboxObjectId operand2)
		{
			return object.Equals(operand1, operand2);
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x0019F32B File Offset: 0x0019D52B
		public static bool operator !=(XsoMailboxObjectId operand1, XsoMailboxObjectId operand2)
		{
			return !object.Equals(operand1, operand2);
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x0019F337 File Offset: 0x0019D537
		public virtual bool Equals(XsoMailboxObjectId other)
		{
			return !(null == other) && ADObjectId.Equals(this.MailboxOwnerId, other.MailboxOwnerId);
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x0019F355 File Offset: 0x0019D555
		public override bool Equals(object obj)
		{
			return this.Equals(obj as XsoMailboxObjectId);
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x0019F363 File Offset: 0x0019D563
		public override int GetHashCode()
		{
			return this.MailboxOwnerId.GetHashCode();
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x0019F370 File Offset: 0x0019D570
		public override byte[] GetBytes()
		{
			return this.MailboxOwnerId.GetBytes();
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x0019F37D File Offset: 0x0019D57D
		internal XsoMailboxObjectId(ADObjectId mailboxOwnerId)
		{
			if (mailboxOwnerId == null)
			{
				throw new ArgumentNullException("mailboxOwnerId");
			}
			this.MailboxOwnerId = mailboxOwnerId;
		}
	}
}
