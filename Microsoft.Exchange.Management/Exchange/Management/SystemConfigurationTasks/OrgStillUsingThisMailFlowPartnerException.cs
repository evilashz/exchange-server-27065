using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001016 RID: 4118
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrgStillUsingThisMailFlowPartnerException : LocalizedException
	{
		// Token: 0x0600AF28 RID: 44840 RVA: 0x00293F2D File Offset: 0x0029212D
		public OrgStillUsingThisMailFlowPartnerException(string trust, string org) : base(Strings.ErrorOrgStillUsingThisMailFlowPartner(trust, org))
		{
			this.trust = trust;
			this.org = org;
		}

		// Token: 0x0600AF29 RID: 44841 RVA: 0x00293F4A File Offset: 0x0029214A
		public OrgStillUsingThisMailFlowPartnerException(string trust, string org, Exception innerException) : base(Strings.ErrorOrgStillUsingThisMailFlowPartner(trust, org), innerException)
		{
			this.trust = trust;
			this.org = org;
		}

		// Token: 0x0600AF2A RID: 44842 RVA: 0x00293F68 File Offset: 0x00292168
		protected OrgStillUsingThisMailFlowPartnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.trust = (string)info.GetValue("trust", typeof(string));
			this.org = (string)info.GetValue("org", typeof(string));
		}

		// Token: 0x0600AF2B RID: 44843 RVA: 0x00293FBD File Offset: 0x002921BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("trust", this.trust);
			info.AddValue("org", this.org);
		}

		// Token: 0x170037ED RID: 14317
		// (get) Token: 0x0600AF2C RID: 44844 RVA: 0x00293FE9 File Offset: 0x002921E9
		public string Trust
		{
			get
			{
				return this.trust;
			}
		}

		// Token: 0x170037EE RID: 14318
		// (get) Token: 0x0600AF2D RID: 44845 RVA: 0x00293FF1 File Offset: 0x002921F1
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x04006153 RID: 24915
		private readonly string trust;

		// Token: 0x04006154 RID: 24916
		private readonly string org;
	}
}
