using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4C RID: 3916
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveFederatedAcceptedDomainException : LocalizedException
	{
		// Token: 0x0600AB75 RID: 43893 RVA: 0x0028F17C File Offset: 0x0028D37C
		public CannotRemoveFederatedAcceptedDomainException(string domain) : base(Strings.CannotRemoveFederatedAcceptedDomain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600AB76 RID: 43894 RVA: 0x0028F191 File Offset: 0x0028D391
		public CannotRemoveFederatedAcceptedDomainException(string domain, Exception innerException) : base(Strings.CannotRemoveFederatedAcceptedDomain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600AB77 RID: 43895 RVA: 0x0028F1A7 File Offset: 0x0028D3A7
		protected CannotRemoveFederatedAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600AB78 RID: 43896 RVA: 0x0028F1D1 File Offset: 0x0028D3D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003762 RID: 14178
		// (get) Token: 0x0600AB79 RID: 43897 RVA: 0x0028F1EC File Offset: 0x0028D3EC
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060C8 RID: 24776
		private readonly string domain;
	}
}
