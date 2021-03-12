using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200032F RID: 815
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StaticCapacityExceededReservationException : ResourceReservationException
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x00051A8D File Offset: 0x0004FC8D
		public StaticCapacityExceededReservationException(string resourceName, string resourceType, int capacity) : base(MrsStrings.ErrorStaticCapacityExceeded1(resourceName, resourceType, capacity))
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.capacity = capacity;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00051AB2 File Offset: 0x0004FCB2
		public StaticCapacityExceededReservationException(string resourceName, string resourceType, int capacity, Exception innerException) : base(MrsStrings.ErrorStaticCapacityExceeded1(resourceName, resourceType, capacity), innerException)
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.capacity = capacity;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00051ADC File Offset: 0x0004FCDC
		protected StaticCapacityExceededReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.resourceType = (string)info.GetValue("resourceType", typeof(string));
			this.capacity = (int)info.GetValue("capacity", typeof(int));
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x00051B51 File Offset: 0x0004FD51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("resourceType", this.resourceType);
			info.AddValue("capacity", this.capacity);
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00051B8E File Offset: 0x0004FD8E
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00051B96 File Offset: 0x0004FD96
		public string ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x00051B9E File Offset: 0x0004FD9E
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x04001028 RID: 4136
		private readonly string resourceName;

		// Token: 0x04001029 RID: 4137
		private readonly string resourceType;

		// Token: 0x0400102A RID: 4138
		private readonly int capacity;
	}
}
