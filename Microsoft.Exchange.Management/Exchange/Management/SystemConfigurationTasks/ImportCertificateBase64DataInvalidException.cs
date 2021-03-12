using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F61 RID: 3937
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImportCertificateBase64DataInvalidException : LocalizedException
	{
		// Token: 0x0600ABDA RID: 43994 RVA: 0x0028FA47 File Offset: 0x0028DC47
		public ImportCertificateBase64DataInvalidException() : base(Strings.ImportCertificateBase64DataInvalid)
		{
		}

		// Token: 0x0600ABDB RID: 43995 RVA: 0x0028FA54 File Offset: 0x0028DC54
		public ImportCertificateBase64DataInvalidException(Exception innerException) : base(Strings.ImportCertificateBase64DataInvalid, innerException)
		{
		}

		// Token: 0x0600ABDC RID: 43996 RVA: 0x0028FA62 File Offset: 0x0028DC62
		protected ImportCertificateBase64DataInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABDD RID: 43997 RVA: 0x0028FA6C File Offset: 0x0028DC6C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
