using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000195 RID: 405
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MaximumConcurrentMigrationLimitExceededException : MigrationPermanentException
	{
		// Token: 0x06001737 RID: 5943 RVA: 0x00070488 File Offset: 0x0006E688
		public MaximumConcurrentMigrationLimitExceededException(string endpointValue, string limitValue, string migrationType) : base(Strings.ErrorMaximumConcurrentMigrationLimitExceeded(endpointValue, limitValue, migrationType))
		{
			this.endpointValue = endpointValue;
			this.limitValue = limitValue;
			this.migrationType = migrationType;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000704AD File Offset: 0x0006E6AD
		public MaximumConcurrentMigrationLimitExceededException(string endpointValue, string limitValue, string migrationType, Exception innerException) : base(Strings.ErrorMaximumConcurrentMigrationLimitExceeded(endpointValue, limitValue, migrationType), innerException)
		{
			this.endpointValue = endpointValue;
			this.limitValue = limitValue;
			this.migrationType = migrationType;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000704D4 File Offset: 0x0006E6D4
		protected MaximumConcurrentMigrationLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointValue = (string)info.GetValue("endpointValue", typeof(string));
			this.limitValue = (string)info.GetValue("limitValue", typeof(string));
			this.migrationType = (string)info.GetValue("migrationType", typeof(string));
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00070549 File Offset: 0x0006E749
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointValue", this.endpointValue);
			info.AddValue("limitValue", this.limitValue);
			info.AddValue("migrationType", this.migrationType);
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00070586 File Offset: 0x0006E786
		public string EndpointValue
		{
			get
			{
				return this.endpointValue;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0007058E File Offset: 0x0006E78E
		public string LimitValue
		{
			get
			{
				return this.limitValue;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00070596 File Offset: 0x0006E796
		public string MigrationType
		{
			get
			{
				return this.migrationType;
			}
		}

		// Token: 0x04000B0B RID: 2827
		private readonly string endpointValue;

		// Token: 0x04000B0C RID: 2828
		private readonly string limitValue;

		// Token: 0x04000B0D RID: 2829
		private readonly string migrationType;
	}
}
