using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001017 RID: 4119
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrgsStillUsingThisMailFlowPartnerException : LocalizedException
	{
		// Token: 0x0600AF2E RID: 44846 RVA: 0x00293FF9 File Offset: 0x002921F9
		public OrgsStillUsingThisMailFlowPartnerException(string trust, string org, string remainingCount) : base(Strings.ErrorOrgsStillUsingThisMailFlowPartner(trust, org, remainingCount))
		{
			this.trust = trust;
			this.org = org;
			this.remainingCount = remainingCount;
		}

		// Token: 0x0600AF2F RID: 44847 RVA: 0x0029401E File Offset: 0x0029221E
		public OrgsStillUsingThisMailFlowPartnerException(string trust, string org, string remainingCount, Exception innerException) : base(Strings.ErrorOrgsStillUsingThisMailFlowPartner(trust, org, remainingCount), innerException)
		{
			this.trust = trust;
			this.org = org;
			this.remainingCount = remainingCount;
		}

		// Token: 0x0600AF30 RID: 44848 RVA: 0x00294048 File Offset: 0x00292248
		protected OrgsStillUsingThisMailFlowPartnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.trust = (string)info.GetValue("trust", typeof(string));
			this.org = (string)info.GetValue("org", typeof(string));
			this.remainingCount = (string)info.GetValue("remainingCount", typeof(string));
		}

		// Token: 0x0600AF31 RID: 44849 RVA: 0x002940BD File Offset: 0x002922BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("trust", this.trust);
			info.AddValue("org", this.org);
			info.AddValue("remainingCount", this.remainingCount);
		}

		// Token: 0x170037EF RID: 14319
		// (get) Token: 0x0600AF32 RID: 44850 RVA: 0x002940FA File Offset: 0x002922FA
		public string Trust
		{
			get
			{
				return this.trust;
			}
		}

		// Token: 0x170037F0 RID: 14320
		// (get) Token: 0x0600AF33 RID: 44851 RVA: 0x00294102 File Offset: 0x00292302
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x170037F1 RID: 14321
		// (get) Token: 0x0600AF34 RID: 44852 RVA: 0x0029410A File Offset: 0x0029230A
		public string RemainingCount
		{
			get
			{
				return this.remainingCount;
			}
		}

		// Token: 0x04006155 RID: 24917
		private readonly string trust;

		// Token: 0x04006156 RID: 24918
		private readonly string org;

		// Token: 0x04006157 RID: 24919
		private readonly string remainingCount;
	}
}
