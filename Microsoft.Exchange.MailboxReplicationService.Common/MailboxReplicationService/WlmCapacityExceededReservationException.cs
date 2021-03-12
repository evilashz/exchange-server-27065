using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000330 RID: 816
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WlmCapacityExceededReservationException : ResourceReservationException
	{
		// Token: 0x060025A2 RID: 9634 RVA: 0x00051BA6 File Offset: 0x0004FDA6
		public WlmCapacityExceededReservationException(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, int capacity) : base(MrsStrings.ErrorWlmCapacityExceeded3(resourceName, resourceType, wlmResourceKey, wlmResourceMetricType, capacity))
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.wlmResourceKey = wlmResourceKey;
			this.wlmResourceMetricType = wlmResourceMetricType;
			this.capacity = capacity;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00051BDF File Offset: 0x0004FDDF
		public WlmCapacityExceededReservationException(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, int capacity, Exception innerException) : base(MrsStrings.ErrorWlmCapacityExceeded3(resourceName, resourceType, wlmResourceKey, wlmResourceMetricType, capacity), innerException)
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.wlmResourceKey = wlmResourceKey;
			this.wlmResourceMetricType = wlmResourceMetricType;
			this.capacity = capacity;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00051C1C File Offset: 0x0004FE1C
		protected WlmCapacityExceededReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.resourceType = (string)info.GetValue("resourceType", typeof(string));
			this.wlmResourceKey = (string)info.GetValue("wlmResourceKey", typeof(string));
			this.wlmResourceMetricType = (int)info.GetValue("wlmResourceMetricType", typeof(int));
			this.capacity = (int)info.GetValue("capacity", typeof(int));
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00051CD4 File Offset: 0x0004FED4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("resourceType", this.resourceType);
			info.AddValue("wlmResourceKey", this.wlmResourceKey);
			info.AddValue("wlmResourceMetricType", this.wlmResourceMetricType);
			info.AddValue("capacity", this.capacity);
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00051D3E File Offset: 0x0004FF3E
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00051D46 File Offset: 0x0004FF46
		public string ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00051D4E File Offset: 0x0004FF4E
		public string WlmResourceKey
		{
			get
			{
				return this.wlmResourceKey;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x00051D56 File Offset: 0x0004FF56
		public int WlmResourceMetricType
		{
			get
			{
				return this.wlmResourceMetricType;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00051D5E File Offset: 0x0004FF5E
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x0400102B RID: 4139
		private readonly string resourceName;

		// Token: 0x0400102C RID: 4140
		private readonly string resourceType;

		// Token: 0x0400102D RID: 4141
		private readonly string wlmResourceKey;

		// Token: 0x0400102E RID: 4142
		private readonly int wlmResourceMetricType;

		// Token: 0x0400102F RID: 4143
		private readonly int capacity;
	}
}
