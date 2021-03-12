using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F55 RID: 3925
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConflictingAcceptedDomainException : LocalizedException
	{
		// Token: 0x0600ABA0 RID: 43936 RVA: 0x0028F539 File Offset: 0x0028D739
		public ConflictingAcceptedDomainException(string domain) : base(Strings.ConflictingAcceptedDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABA1 RID: 43937 RVA: 0x0028F54E File Offset: 0x0028D74E
		public ConflictingAcceptedDomainException(string domain, Exception innerException) : base(Strings.ConflictingAcceptedDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABA2 RID: 43938 RVA: 0x0028F564 File Offset: 0x0028D764
		protected ConflictingAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABA3 RID: 43939 RVA: 0x0028F58E File Offset: 0x0028D78E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003769 RID: 14185
		// (get) Token: 0x0600ABA4 RID: 43940 RVA: 0x0028F5A9 File Offset: 0x0028D7A9
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060CF RID: 24783
		private readonly string domain;
	}
}
