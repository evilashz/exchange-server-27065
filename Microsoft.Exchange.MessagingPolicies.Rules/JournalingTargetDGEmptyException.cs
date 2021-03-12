using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000B0 RID: 176
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JournalingTargetDGEmptyException : LocalizedException
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00018277 File Offset: 0x00016477
		public JournalingTargetDGEmptyException(string distributionGroup) : base(TransportRulesStrings.JournalingTargetDGEmptyDescription(distributionGroup))
		{
			this.distributionGroup = distributionGroup;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001828C File Offset: 0x0001648C
		public JournalingTargetDGEmptyException(string distributionGroup, Exception innerException) : base(TransportRulesStrings.JournalingTargetDGEmptyDescription(distributionGroup), innerException)
		{
			this.distributionGroup = distributionGroup;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000182A2 File Offset: 0x000164A2
		protected JournalingTargetDGEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.distributionGroup = (string)info.GetValue("distributionGroup", typeof(string));
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000182CC File Offset: 0x000164CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("distributionGroup", this.distributionGroup);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000182E7 File Offset: 0x000164E7
		public string DistributionGroup
		{
			get
			{
				return this.distributionGroup;
			}
		}

		// Token: 0x040002E4 RID: 740
		private readonly string distributionGroup;
	}
}
