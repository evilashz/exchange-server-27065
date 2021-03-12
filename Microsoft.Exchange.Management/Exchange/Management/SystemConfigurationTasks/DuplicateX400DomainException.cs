using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5B RID: 3931
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateX400DomainException : LocalizedException
	{
		// Token: 0x0600ABBC RID: 43964 RVA: 0x0028F777 File Offset: 0x0028D977
		public DuplicateX400DomainException(X400Domain domain) : base(Strings.DuplicateX400Domain(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABBD RID: 43965 RVA: 0x0028F78C File Offset: 0x0028D98C
		public DuplicateX400DomainException(X400Domain domain, Exception innerException) : base(Strings.DuplicateX400Domain(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABBE RID: 43966 RVA: 0x0028F7A2 File Offset: 0x0028D9A2
		protected DuplicateX400DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (X400Domain)info.GetValue("domain", typeof(X400Domain));
		}

		// Token: 0x0600ABBF RID: 43967 RVA: 0x0028F7CC File Offset: 0x0028D9CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700376D RID: 14189
		// (get) Token: 0x0600ABC0 RID: 43968 RVA: 0x0028F7E7 File Offset: 0x0028D9E7
		public X400Domain Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060D3 RID: 24787
		private readonly X400Domain domain;
	}
}
