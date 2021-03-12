using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.RedirectionModule.LocStrings;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmbiguousTargetSite : RedirectionLogicException
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003F41 File Offset: 0x00002141
		public AmbiguousTargetSite(string domainName, int minorPartnerId, string identities) : base(Strings.AmbiguousTargetSite(domainName, minorPartnerId, identities))
		{
			this.domainName = domainName;
			this.minorPartnerId = minorPartnerId;
			this.identities = identities;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003F66 File Offset: 0x00002166
		public AmbiguousTargetSite(string domainName, int minorPartnerId, string identities, Exception innerException) : base(Strings.AmbiguousTargetSite(domainName, minorPartnerId, identities), innerException)
		{
			this.domainName = domainName;
			this.minorPartnerId = minorPartnerId;
			this.identities = identities;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003F90 File Offset: 0x00002190
		protected AmbiguousTargetSite(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domainName = (string)info.GetValue("domainName", typeof(string));
			this.minorPartnerId = (int)info.GetValue("minorPartnerId", typeof(int));
			this.identities = (string)info.GetValue("identities", typeof(string));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004005 File Offset: 0x00002205
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domainName", this.domainName);
			info.AddValue("minorPartnerId", this.minorPartnerId);
			info.AddValue("identities", this.identities);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004042 File Offset: 0x00002242
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000404A File Offset: 0x0000224A
		public int MinorPartnerId
		{
			get
			{
				return this.minorPartnerId;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004052 File Offset: 0x00002252
		public string Identities
		{
			get
			{
				return this.identities;
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly string domainName;

		// Token: 0x04000044 RID: 68
		private readonly int minorPartnerId;

		// Token: 0x04000045 RID: 69
		private readonly string identities;
	}
}
