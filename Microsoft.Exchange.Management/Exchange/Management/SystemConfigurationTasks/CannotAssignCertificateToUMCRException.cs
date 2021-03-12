using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001172 RID: 4466
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotAssignCertificateToUMCRException : LocalizedException
	{
		// Token: 0x0600B624 RID: 46628 RVA: 0x0029F554 File Offset: 0x0029D754
		public CannotAssignCertificateToUMCRException(string thumbprint) : base(Strings.CannotAssignCertificateToUMCR(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600B625 RID: 46629 RVA: 0x0029F569 File Offset: 0x0029D769
		public CannotAssignCertificateToUMCRException(string thumbprint, Exception innerException) : base(Strings.CannotAssignCertificateToUMCR(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600B626 RID: 46630 RVA: 0x0029F57F File Offset: 0x0029D77F
		protected CannotAssignCertificateToUMCRException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600B627 RID: 46631 RVA: 0x0029F5A9 File Offset: 0x0029D7A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x17003979 RID: 14713
		// (get) Token: 0x0600B628 RID: 46632 RVA: 0x0029F5C4 File Offset: 0x0029D7C4
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040062DF RID: 25311
		private readonly string thumbprint;
	}
}
