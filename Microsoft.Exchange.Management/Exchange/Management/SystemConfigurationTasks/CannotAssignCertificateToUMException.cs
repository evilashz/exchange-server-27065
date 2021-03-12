using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F48 RID: 3912
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotAssignCertificateToUMException : LocalizedException
	{
		// Token: 0x0600AB61 RID: 43873 RVA: 0x0028EF8F File Offset: 0x0028D18F
		public CannotAssignCertificateToUMException(string thumbprint) : base(Strings.CannotAssignCertificateToUM(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB62 RID: 43874 RVA: 0x0028EFA4 File Offset: 0x0028D1A4
		public CannotAssignCertificateToUMException(string thumbprint, Exception innerException) : base(Strings.CannotAssignCertificateToUM(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB63 RID: 43875 RVA: 0x0028EFBA File Offset: 0x0028D1BA
		protected CannotAssignCertificateToUMException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB64 RID: 43876 RVA: 0x0028EFE4 File Offset: 0x0028D1E4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x1700375E RID: 14174
		// (get) Token: 0x0600AB65 RID: 43877 RVA: 0x0028EFFF File Offset: 0x0028D1FF
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C4 RID: 24772
		private readonly string thumbprint;
	}
}
