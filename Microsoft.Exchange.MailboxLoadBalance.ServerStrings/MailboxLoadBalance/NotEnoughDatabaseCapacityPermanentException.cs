using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NotEnoughDatabaseCapacityPermanentException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000029BB File Offset: 0x00000BBB
		public NotEnoughDatabaseCapacityPermanentException(string databaseGuid, string capacityType, long requestedCapacityUnits, long availableCapacityUnits) : base(MigrationWorkflowServiceStrings.ErrorNotEnoughDatabaseCapacity(databaseGuid, capacityType, requestedCapacityUnits, availableCapacityUnits))
		{
			this.databaseGuid = databaseGuid;
			this.capacityType = capacityType;
			this.requestedCapacityUnits = requestedCapacityUnits;
			this.availableCapacityUnits = availableCapacityUnits;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000029EA File Offset: 0x00000BEA
		public NotEnoughDatabaseCapacityPermanentException(string databaseGuid, string capacityType, long requestedCapacityUnits, long availableCapacityUnits, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorNotEnoughDatabaseCapacity(databaseGuid, capacityType, requestedCapacityUnits, availableCapacityUnits), innerException)
		{
			this.databaseGuid = databaseGuid;
			this.capacityType = capacityType;
			this.requestedCapacityUnits = requestedCapacityUnits;
			this.availableCapacityUnits = availableCapacityUnits;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A1C File Offset: 0x00000C1C
		protected NotEnoughDatabaseCapacityPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseGuid = (string)info.GetValue("databaseGuid", typeof(string));
			this.capacityType = (string)info.GetValue("capacityType", typeof(string));
			this.requestedCapacityUnits = (long)info.GetValue("requestedCapacityUnits", typeof(long));
			this.availableCapacityUnits = (long)info.GetValue("availableCapacityUnits", typeof(long));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseGuid", this.databaseGuid);
			info.AddValue("capacityType", this.capacityType);
			info.AddValue("requestedCapacityUnits", this.requestedCapacityUnits);
			info.AddValue("availableCapacityUnits", this.availableCapacityUnits);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B0D File Offset: 0x00000D0D
		public string DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002B15 File Offset: 0x00000D15
		public string CapacityType
		{
			get
			{
				return this.capacityType;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002B1D File Offset: 0x00000D1D
		public long RequestedCapacityUnits
		{
			get
			{
				return this.requestedCapacityUnits;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002B25 File Offset: 0x00000D25
		public long AvailableCapacityUnits
		{
			get
			{
				return this.availableCapacityUnits;
			}
		}

		// Token: 0x04000026 RID: 38
		private readonly string databaseGuid;

		// Token: 0x04000027 RID: 39
		private readonly string capacityType;

		// Token: 0x04000028 RID: 40
		private readonly long requestedCapacityUnits;

		// Token: 0x04000029 RID: 41
		private readonly long availableCapacityUnits;
	}
}
