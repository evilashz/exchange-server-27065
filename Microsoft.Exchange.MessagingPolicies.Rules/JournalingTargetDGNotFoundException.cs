using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000B1 RID: 177
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JournalingTargetDGNotFoundException : LocalizedException
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x000182EF File Offset: 0x000164EF
		public JournalingTargetDGNotFoundException(string distributionGroup) : base(TransportRulesStrings.JournalingTargetDGNotFoundDescription(distributionGroup))
		{
			this.distributionGroup = distributionGroup;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00018304 File Offset: 0x00016504
		public JournalingTargetDGNotFoundException(string distributionGroup, Exception innerException) : base(TransportRulesStrings.JournalingTargetDGNotFoundDescription(distributionGroup), innerException)
		{
			this.distributionGroup = distributionGroup;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001831A File Offset: 0x0001651A
		protected JournalingTargetDGNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.distributionGroup = (string)info.GetValue("distributionGroup", typeof(string));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00018344 File Offset: 0x00016544
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("distributionGroup", this.distributionGroup);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001835F File Offset: 0x0001655F
		public string DistributionGroup
		{
			get
			{
				return this.distributionGroup;
			}
		}

		// Token: 0x040002E5 RID: 741
		private readonly string distributionGroup;
	}
}
