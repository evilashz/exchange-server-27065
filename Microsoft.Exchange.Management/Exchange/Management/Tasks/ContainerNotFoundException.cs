using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109D RID: 4253
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600B210 RID: 45584 RVA: 0x0029951C File Offset: 0x0029771C
		public ContainerNotFoundException(string domain) : base(Strings.ContainerNotFoundException(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B211 RID: 45585 RVA: 0x00299531 File Offset: 0x00297731
		public ContainerNotFoundException(string domain, Exception innerException) : base(Strings.ContainerNotFoundException(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B212 RID: 45586 RVA: 0x00299547 File Offset: 0x00297747
		protected ContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B213 RID: 45587 RVA: 0x00299571 File Offset: 0x00297771
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038B9 RID: 14521
		// (get) Token: 0x0600B214 RID: 45588 RVA: 0x0029958C File Offset: 0x0029778C
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x0400621F RID: 25119
		private readonly string domain;
	}
}
