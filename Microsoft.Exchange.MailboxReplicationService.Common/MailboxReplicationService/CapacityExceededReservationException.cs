using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032E RID: 814
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CapacityExceededReservationException : ResourceReservationException
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x000519C1 File Offset: 0x0004FBC1
		public CapacityExceededReservationException(string resourceName, int capacity) : base(MrsStrings.ErrorStaticCapacityExceeded(resourceName, capacity))
		{
			this.resourceName = resourceName;
			this.capacity = capacity;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000519DE File Offset: 0x0004FBDE
		public CapacityExceededReservationException(string resourceName, int capacity, Exception innerException) : base(MrsStrings.ErrorStaticCapacityExceeded(resourceName, capacity), innerException)
		{
			this.resourceName = resourceName;
			this.capacity = capacity;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000519FC File Offset: 0x0004FBFC
		protected CapacityExceededReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.capacity = (int)info.GetValue("capacity", typeof(int));
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00051A51 File Offset: 0x0004FC51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("capacity", this.capacity);
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x00051A7D File Offset: 0x0004FC7D
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x00051A85 File Offset: 0x0004FC85
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x04001026 RID: 4134
		private readonly string resourceName;

		// Token: 0x04001027 RID: 4135
		private readonly int capacity;
	}
}
