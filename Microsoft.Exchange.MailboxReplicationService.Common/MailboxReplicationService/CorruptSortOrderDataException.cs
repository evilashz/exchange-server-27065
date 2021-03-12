using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000362 RID: 866
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptSortOrderDataException : MailboxReplicationPermanentException
	{
		// Token: 0x060026A8 RID: 9896 RVA: 0x000537BB File Offset: 0x000519BB
		public CorruptSortOrderDataException(int flags) : base(MrsStrings.CorruptSortOrderData(flags))
		{
			this.flags = flags;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000537D0 File Offset: 0x000519D0
		public CorruptSortOrderDataException(int flags, Exception innerException) : base(MrsStrings.CorruptSortOrderData(flags), innerException)
		{
			this.flags = flags;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000537E6 File Offset: 0x000519E6
		protected CorruptSortOrderDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.flags = (int)info.GetValue("flags", typeof(int));
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00053810 File Offset: 0x00051A10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("flags", this.flags);
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x0005382B File Offset: 0x00051A2B
		public int Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x04001069 RID: 4201
		private readonly int flags;
	}
}
