using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F54 RID: 3924
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToAddCertificateToRootStoreException : LocalizedException
	{
		// Token: 0x0600AB9A RID: 43930 RVA: 0x0028F46D File Offset: 0x0028D66D
		public UnableToAddCertificateToRootStoreException(string thumbprint, string serverName) : base(Strings.UnableToAddCertificateToRootStore(thumbprint, serverName))
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600AB9B RID: 43931 RVA: 0x0028F48A File Offset: 0x0028D68A
		public UnableToAddCertificateToRootStoreException(string thumbprint, string serverName, Exception innerException) : base(Strings.UnableToAddCertificateToRootStore(thumbprint, serverName), innerException)
		{
			this.thumbprint = thumbprint;
			this.serverName = serverName;
		}

		// Token: 0x0600AB9C RID: 43932 RVA: 0x0028F4A8 File Offset: 0x0028D6A8
		protected UnableToAddCertificateToRootStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AB9D RID: 43933 RVA: 0x0028F4FD File Offset: 0x0028D6FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003767 RID: 14183
		// (get) Token: 0x0600AB9E RID: 43934 RVA: 0x0028F529 File Offset: 0x0028D729
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x17003768 RID: 14184
		// (get) Token: 0x0600AB9F RID: 43935 RVA: 0x0028F531 File Offset: 0x0028D731
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060CD RID: 24781
		private readonly string thumbprint;

		// Token: 0x040060CE RID: 24782
		private readonly string serverName;
	}
}
