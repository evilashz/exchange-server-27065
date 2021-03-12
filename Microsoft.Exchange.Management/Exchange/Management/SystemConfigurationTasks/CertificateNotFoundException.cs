using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F51 RID: 3921
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CertificateNotFoundException : LocalizedException
	{
		// Token: 0x0600AB8A RID: 43914 RVA: 0x0028F2B0 File Offset: 0x0028D4B0
		public CertificateNotFoundException(string thumbprint) : base(Strings.CertificateNotFound(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB8B RID: 43915 RVA: 0x0028F2C5 File Offset: 0x0028D4C5
		public CertificateNotFoundException(string thumbprint, Exception innerException) : base(Strings.CertificateNotFound(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB8C RID: 43916 RVA: 0x0028F2DB File Offset: 0x0028D4DB
		protected CertificateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB8D RID: 43917 RVA: 0x0028F305 File Offset: 0x0028D505
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x17003763 RID: 14179
		// (get) Token: 0x0600AB8E RID: 43918 RVA: 0x0028F320 File Offset: 0x0028D520
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C9 RID: 24777
		private readonly string thumbprint;
	}
}
