using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C4 RID: 4292
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrgsStillUsingThisTrustException : FederationException
	{
		// Token: 0x0600B2CC RID: 45772 RVA: 0x0029A5D2 File Offset: 0x002987D2
		public OrgsStillUsingThisTrustException(string trust, string orgList) : base(Strings.ErrorOrgsStillUsingThisTrust(trust, orgList))
		{
			this.trust = trust;
			this.orgList = orgList;
		}

		// Token: 0x0600B2CD RID: 45773 RVA: 0x0029A5EF File Offset: 0x002987EF
		public OrgsStillUsingThisTrustException(string trust, string orgList, Exception innerException) : base(Strings.ErrorOrgsStillUsingThisTrust(trust, orgList), innerException)
		{
			this.trust = trust;
			this.orgList = orgList;
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x0029A610 File Offset: 0x00298810
		protected OrgsStillUsingThisTrustException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.trust = (string)info.GetValue("trust", typeof(string));
			this.orgList = (string)info.GetValue("orgList", typeof(string));
		}

		// Token: 0x0600B2CF RID: 45775 RVA: 0x0029A665 File Offset: 0x00298865
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("trust", this.trust);
			info.AddValue("orgList", this.orgList);
		}

		// Token: 0x170038D9 RID: 14553
		// (get) Token: 0x0600B2D0 RID: 45776 RVA: 0x0029A691 File Offset: 0x00298891
		public string Trust
		{
			get
			{
				return this.trust;
			}
		}

		// Token: 0x170038DA RID: 14554
		// (get) Token: 0x0600B2D1 RID: 45777 RVA: 0x0029A699 File Offset: 0x00298899
		public string OrgList
		{
			get
			{
				return this.orgList;
			}
		}

		// Token: 0x0400623F RID: 25151
		private readonly string trust;

		// Token: 0x04006240 RID: 25152
		private readonly string orgList;
	}
}
