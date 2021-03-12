using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFD RID: 3837
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMdbForOperationException : LocalizedException
	{
		// Token: 0x0600A9DA RID: 43482 RVA: 0x0028C6AD File Offset: 0x0028A8AD
		public NoMdbForOperationException(string serverName) : base(Strings.NoMdbForOperation(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9DB RID: 43483 RVA: 0x0028C6C2 File Offset: 0x0028A8C2
		public NoMdbForOperationException(string serverName, Exception innerException) : base(Strings.NoMdbForOperation(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9DC RID: 43484 RVA: 0x0028C6D8 File Offset: 0x0028A8D8
		protected NoMdbForOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600A9DD RID: 43485 RVA: 0x0028C702 File Offset: 0x0028A902
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003703 RID: 14083
		// (get) Token: 0x0600A9DE RID: 43486 RVA: 0x0028C71D File Offset: 0x0028A91D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006069 RID: 24681
		private readonly string serverName;
	}
}
