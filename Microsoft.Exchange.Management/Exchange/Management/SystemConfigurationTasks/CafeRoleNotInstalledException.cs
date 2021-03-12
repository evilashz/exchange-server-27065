using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001173 RID: 4467
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CafeRoleNotInstalledException : LocalizedException
	{
		// Token: 0x0600B629 RID: 46633 RVA: 0x0029F5CC File Offset: 0x0029D7CC
		public CafeRoleNotInstalledException(string thumbprint, string serverName) : base(Strings.CafeRoleNotInstalled(thumbprint, serverName))
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600B62A RID: 46634 RVA: 0x0029F5E9 File Offset: 0x0029D7E9
		public CafeRoleNotInstalledException(string thumbprint, string serverName, Exception innerException) : base(Strings.CafeRoleNotInstalled(thumbprint, serverName), innerException)
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600B62B RID: 46635 RVA: 0x0029F608 File Offset: 0x0029D808
		protected CafeRoleNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B62C RID: 46636 RVA: 0x0029F65D File Offset: 0x0029D85D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700397A RID: 14714
		// (get) Token: 0x0600B62D RID: 46637 RVA: 0x0029F689 File Offset: 0x0029D889
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x1700397B RID: 14715
		// (get) Token: 0x0600B62E RID: 46638 RVA: 0x0029F691 File Offset: 0x0029D891
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040062E0 RID: 25312
		private readonly string thumbprint;

		// Token: 0x040062E1 RID: 25313
		private readonly string serverName;
	}
}
