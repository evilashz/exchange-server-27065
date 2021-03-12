using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F56 RID: 3926
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateAcceptedDomainException : ADObjectAlreadyExistsException
	{
		// Token: 0x0600ABA5 RID: 43941 RVA: 0x0028F5B1 File Offset: 0x0028D7B1
		public DuplicateAcceptedDomainException(string domain) : base(Strings.DuplicateAcceptedDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABA6 RID: 43942 RVA: 0x0028F5C6 File Offset: 0x0028D7C6
		public DuplicateAcceptedDomainException(string domain, Exception innerException) : base(Strings.DuplicateAcceptedDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABA7 RID: 43943 RVA: 0x0028F5DC File Offset: 0x0028D7DC
		protected DuplicateAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABA8 RID: 43944 RVA: 0x0028F606 File Offset: 0x0028D806
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700376A RID: 14186
		// (get) Token: 0x0600ABA9 RID: 43945 RVA: 0x0028F621 File Offset: 0x0028D821
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060D0 RID: 24784
		private readonly string domain;
	}
}
