using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.RedirectionModule.LocStrings;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToResolveTargetSite : RedirectionLogicException
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003E74 File Offset: 0x00002074
		public FailedToResolveTargetSite(string domainName, int minorPartnerId) : base(Strings.FailedToResolveTargetSite(domainName, minorPartnerId))
		{
			this.domainName = domainName;
			this.minorPartnerId = minorPartnerId;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003E91 File Offset: 0x00002091
		public FailedToResolveTargetSite(string domainName, int minorPartnerId, Exception innerException) : base(Strings.FailedToResolveTargetSite(domainName, minorPartnerId), innerException)
		{
			this.domainName = domainName;
			this.minorPartnerId = minorPartnerId;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003EB0 File Offset: 0x000020B0
		protected FailedToResolveTargetSite(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domainName = (string)info.GetValue("domainName", typeof(string));
			this.minorPartnerId = (int)info.GetValue("minorPartnerId", typeof(int));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003F05 File Offset: 0x00002105
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domainName", this.domainName);
			info.AddValue("minorPartnerId", this.minorPartnerId);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003F31 File Offset: 0x00002131
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003F39 File Offset: 0x00002139
		public int MinorPartnerId
		{
			get
			{
				return this.minorPartnerId;
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly string domainName;

		// Token: 0x04000042 RID: 66
		private readonly int minorPartnerId;
	}
}
