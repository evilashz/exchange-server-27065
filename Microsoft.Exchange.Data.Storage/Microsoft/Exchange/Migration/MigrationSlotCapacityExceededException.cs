using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000196 RID: 406
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationSlotCapacityExceededException : MigrationTransientException
	{
		// Token: 0x0600173E RID: 5950 RVA: 0x0007059E File Offset: 0x0006E79E
		public MigrationSlotCapacityExceededException(Unlimited<int> availableCapacity, int requestedCapacity) : base(Strings.ErrorMigrationSlotCapacityExceeded(availableCapacity, requestedCapacity))
		{
			this.availableCapacity = availableCapacity;
			this.requestedCapacity = requestedCapacity;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000705BB File Offset: 0x0006E7BB
		public MigrationSlotCapacityExceededException(Unlimited<int> availableCapacity, int requestedCapacity, Exception innerException) : base(Strings.ErrorMigrationSlotCapacityExceeded(availableCapacity, requestedCapacity), innerException)
		{
			this.availableCapacity = availableCapacity;
			this.requestedCapacity = requestedCapacity;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000705DC File Offset: 0x0006E7DC
		protected MigrationSlotCapacityExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.availableCapacity = (Unlimited<int>)info.GetValue("availableCapacity", typeof(Unlimited<int>));
			this.requestedCapacity = (int)info.GetValue("requestedCapacity", typeof(int));
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00070631 File Offset: 0x0006E831
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("availableCapacity", this.availableCapacity);
			info.AddValue("requestedCapacity", this.requestedCapacity);
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00070662 File Offset: 0x0006E862
		public Unlimited<int> AvailableCapacity
		{
			get
			{
				return this.availableCapacity;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0007066A File Offset: 0x0006E86A
		public int RequestedCapacity
		{
			get
			{
				return this.requestedCapacity;
			}
		}

		// Token: 0x04000B0E RID: 2830
		private readonly Unlimited<int> availableCapacity;

		// Token: 0x04000B0F RID: 2831
		private readonly int requestedCapacity;
	}
}
