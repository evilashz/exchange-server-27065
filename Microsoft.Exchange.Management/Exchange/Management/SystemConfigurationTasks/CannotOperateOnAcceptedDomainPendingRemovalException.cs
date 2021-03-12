using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F64 RID: 3940
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotOperateOnAcceptedDomainPendingRemovalException : LocalizedException
	{
		// Token: 0x0600ABE7 RID: 44007 RVA: 0x0028FB1D File Offset: 0x0028DD1D
		public CannotOperateOnAcceptedDomainPendingRemovalException(string domain) : base(Strings.CannotOperateOnAcceptedDomainPendingRemoval(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600ABE8 RID: 44008 RVA: 0x0028FB32 File Offset: 0x0028DD32
		public CannotOperateOnAcceptedDomainPendingRemovalException(string domain, Exception innerException) : base(Strings.CannotOperateOnAcceptedDomainPendingRemoval(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600ABE9 RID: 44009 RVA: 0x0028FB48 File Offset: 0x0028DD48
		protected CannotOperateOnAcceptedDomainPendingRemovalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600ABEA RID: 44010 RVA: 0x0028FB72 File Offset: 0x0028DD72
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003774 RID: 14196
		// (get) Token: 0x0600ABEB RID: 44011 RVA: 0x0028FB8D File Offset: 0x0028DD8D
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060DA RID: 24794
		private readonly string domain;
	}
}
