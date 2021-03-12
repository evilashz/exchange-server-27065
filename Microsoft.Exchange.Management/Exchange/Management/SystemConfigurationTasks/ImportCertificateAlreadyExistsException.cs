using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F60 RID: 3936
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImportCertificateAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600ABD5 RID: 43989 RVA: 0x0028F9CF File Offset: 0x0028DBCF
		public ImportCertificateAlreadyExistsException(string thumbprint) : base(Strings.ImportCertificateAlreadyExists(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600ABD6 RID: 43990 RVA: 0x0028F9E4 File Offset: 0x0028DBE4
		public ImportCertificateAlreadyExistsException(string thumbprint, Exception innerException) : base(Strings.ImportCertificateAlreadyExists(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600ABD7 RID: 43991 RVA: 0x0028F9FA File Offset: 0x0028DBFA
		protected ImportCertificateAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600ABD8 RID: 43992 RVA: 0x0028FA24 File Offset: 0x0028DC24
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x17003772 RID: 14194
		// (get) Token: 0x0600ABD9 RID: 43993 RVA: 0x0028FA3F File Offset: 0x0028DC3F
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060D8 RID: 24792
		private readonly string thumbprint;
	}
}
