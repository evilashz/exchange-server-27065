using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F62 RID: 3938
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImportCertificateDataInvalidException : LocalizedException
	{
		// Token: 0x0600ABDE RID: 43998 RVA: 0x0028FA76 File Offset: 0x0028DC76
		public ImportCertificateDataInvalidException() : base(Strings.ImportCertificateDataInvalid)
		{
		}

		// Token: 0x0600ABDF RID: 43999 RVA: 0x0028FA83 File Offset: 0x0028DC83
		public ImportCertificateDataInvalidException(Exception innerException) : base(Strings.ImportCertificateDataInvalid, innerException)
		{
		}

		// Token: 0x0600ABE0 RID: 44000 RVA: 0x0028FA91 File Offset: 0x0028DC91
		protected ImportCertificateDataInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABE1 RID: 44001 RVA: 0x0028FA9B File Offset: 0x0028DC9B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
