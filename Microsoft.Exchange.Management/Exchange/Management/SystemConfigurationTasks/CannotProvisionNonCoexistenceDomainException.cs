using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F65 RID: 3941
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotProvisionNonCoexistenceDomainException : LocalizedException
	{
		// Token: 0x0600ABEC RID: 44012 RVA: 0x0028FB95 File Offset: 0x0028DD95
		public CannotProvisionNonCoexistenceDomainException(string domain) : base(Strings.CannotProvisionNonCoexistenceDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABED RID: 44013 RVA: 0x0028FBAA File Offset: 0x0028DDAA
		public CannotProvisionNonCoexistenceDomainException(string domain, Exception innerException) : base(Strings.CannotProvisionNonCoexistenceDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABEE RID: 44014 RVA: 0x0028FBC0 File Offset: 0x0028DDC0
		protected CannotProvisionNonCoexistenceDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABEF RID: 44015 RVA: 0x0028FBEA File Offset: 0x0028DDEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003775 RID: 14197
		// (get) Token: 0x0600ABF0 RID: 44016 RVA: 0x0028FC05 File Offset: 0x0028DE05
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060DB RID: 24795
		private readonly string domain;
	}
}
