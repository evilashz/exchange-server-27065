using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F63 RID: 3939
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateOrganizationRelationshipDomainException : LocalizedException
	{
		// Token: 0x0600ABE2 RID: 44002 RVA: 0x0028FAA5 File Offset: 0x0028DCA5
		public DuplicateOrganizationRelationshipDomainException(string domain) : base(Strings.DuplicateOrganizationRelationshipDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABE3 RID: 44003 RVA: 0x0028FABA File Offset: 0x0028DCBA
		public DuplicateOrganizationRelationshipDomainException(string domain, Exception innerException) : base(Strings.DuplicateOrganizationRelationshipDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABE4 RID: 44004 RVA: 0x0028FAD0 File Offset: 0x0028DCD0
		protected DuplicateOrganizationRelationshipDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABE5 RID: 44005 RVA: 0x0028FAFA File Offset: 0x0028DCFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003773 RID: 14195
		// (get) Token: 0x0600ABE6 RID: 44006 RVA: 0x0028FB15 File Offset: 0x0028DD15
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060D9 RID: 24793
		private readonly string domain;
	}
}
