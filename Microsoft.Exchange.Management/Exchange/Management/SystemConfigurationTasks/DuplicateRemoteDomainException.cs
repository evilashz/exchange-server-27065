using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F58 RID: 3928
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateRemoteDomainException : LocalizedException
	{
		// Token: 0x0600ABAE RID: 43950 RVA: 0x0028F658 File Offset: 0x0028D858
		public DuplicateRemoteDomainException(string domain) : base(Strings.DuplicateRemoteDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABAF RID: 43951 RVA: 0x0028F66D File Offset: 0x0028D86D
		public DuplicateRemoteDomainException(string domain, Exception innerException) : base(Strings.DuplicateRemoteDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABB0 RID: 43952 RVA: 0x0028F683 File Offset: 0x0028D883
		protected DuplicateRemoteDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABB1 RID: 43953 RVA: 0x0028F6AD File Offset: 0x0028D8AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700376B RID: 14187
		// (get) Token: 0x0600ABB2 RID: 43954 RVA: 0x0028F6C8 File Offset: 0x0028D8C8
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060D1 RID: 24785
		private readonly string domain;
	}
}
