using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F18 RID: 3864
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthMailboxServerNotFoundException : LocalizedException
	{
		// Token: 0x0600AA66 RID: 43622 RVA: 0x0028D531 File Offset: 0x0028B731
		public CasHealthMailboxServerNotFoundException(string serverName) : base(Strings.CasHealthMailboxServerNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AA67 RID: 43623 RVA: 0x0028D546 File Offset: 0x0028B746
		public CasHealthMailboxServerNotFoundException(string serverName, Exception innerException) : base(Strings.CasHealthMailboxServerNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AA68 RID: 43624 RVA: 0x0028D55C File Offset: 0x0028B75C
		protected CasHealthMailboxServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AA69 RID: 43625 RVA: 0x0028D586 File Offset: 0x0028B786
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003723 RID: 14115
		// (get) Token: 0x0600AA6A RID: 43626 RVA: 0x0028D5A1 File Offset: 0x0028B7A1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006089 RID: 24713
		private readonly string serverName;
	}
}
