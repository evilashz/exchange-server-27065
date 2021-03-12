using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F50 RID: 3920
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CertificateDoesNotHavePKCS10Exception : LocalizedException
	{
		// Token: 0x0600AB86 RID: 43910 RVA: 0x0028F281 File Offset: 0x0028D481
		public CertificateDoesNotHavePKCS10Exception() : base(Strings.CertificateDoesNotHavePKCS10)
		{
		}

		// Token: 0x0600AB87 RID: 43911 RVA: 0x0028F28E File Offset: 0x0028D48E
		public CertificateDoesNotHavePKCS10Exception(Exception innerException) : base(Strings.CertificateDoesNotHavePKCS10, innerException)
		{
		}

		// Token: 0x0600AB88 RID: 43912 RVA: 0x0028F29C File Offset: 0x0028D49C
		protected CertificateDoesNotHavePKCS10Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB89 RID: 43913 RVA: 0x0028F2A6 File Offset: 0x0028D4A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
