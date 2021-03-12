using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F53 RID: 3923
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CertificateExpiredException : LocalizedException
	{
		// Token: 0x0600AB95 RID: 43925 RVA: 0x0028F3F5 File Offset: 0x0028D5F5
		public CertificateExpiredException(string thumbprint) : base(Strings.CertificateExpired(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB96 RID: 43926 RVA: 0x0028F40A File Offset: 0x0028D60A
		public CertificateExpiredException(string thumbprint, Exception innerException) : base(Strings.CertificateExpired(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB97 RID: 43927 RVA: 0x0028F420 File Offset: 0x0028D620
		protected CertificateExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB98 RID: 43928 RVA: 0x0028F44A File Offset: 0x0028D64A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x17003766 RID: 14182
		// (get) Token: 0x0600AB99 RID: 43929 RVA: 0x0028F465 File Offset: 0x0028D665
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060CC RID: 24780
		private readonly string thumbprint;
	}
}
