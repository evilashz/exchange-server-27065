using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F49 RID: 3913
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMRoleNotInstalledException : LocalizedException
	{
		// Token: 0x0600AB66 RID: 43878 RVA: 0x0028F007 File Offset: 0x0028D207
		public UMRoleNotInstalledException(string thumbprint, string serverName) : base(Strings.UMRoleNotInstalled(thumbprint, serverName))
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600AB67 RID: 43879 RVA: 0x0028F024 File Offset: 0x0028D224
		public UMRoleNotInstalledException(string thumbprint, string serverName, Exception innerException) : base(Strings.UMRoleNotInstalled(thumbprint, serverName), innerException)
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600AB68 RID: 43880 RVA: 0x0028F044 File Offset: 0x0028D244
		protected UMRoleNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AB69 RID: 43881 RVA: 0x0028F099 File Offset: 0x0028D299
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700375F RID: 14175
		// (get) Token: 0x0600AB6A RID: 43882 RVA: 0x0028F0C5 File Offset: 0x0028D2C5
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x17003760 RID: 14176
		// (get) Token: 0x0600AB6B RID: 43883 RVA: 0x0028F0CD File Offset: 0x0028D2CD
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060C5 RID: 24773
		private readonly string thumbprint;

		// Token: 0x040060C6 RID: 24774
		private readonly string serverName;
	}
}
