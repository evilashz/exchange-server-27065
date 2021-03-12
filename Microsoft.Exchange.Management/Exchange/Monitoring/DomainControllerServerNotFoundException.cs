using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F24 RID: 3876
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainControllerServerNotFoundException : LocalizedException
	{
		// Token: 0x0600AAA8 RID: 43688 RVA: 0x0028DCDE File Offset: 0x0028BEDE
		public DomainControllerServerNotFoundException(string serverName) : base(Strings.DomainControllerServerNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AAA9 RID: 43689 RVA: 0x0028DCF3 File Offset: 0x0028BEF3
		public DomainControllerServerNotFoundException(string serverName, Exception innerException) : base(Strings.DomainControllerServerNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AAAA RID: 43690 RVA: 0x0028DD09 File Offset: 0x0028BF09
		protected DomainControllerServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AAAB RID: 43691 RVA: 0x0028DD33 File Offset: 0x0028BF33
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003735 RID: 14133
		// (get) Token: 0x0600AAAC RID: 43692 RVA: 0x0028DD4E File Offset: 0x0028BF4E
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400609B RID: 24731
		private readonly string serverName;
	}
}
