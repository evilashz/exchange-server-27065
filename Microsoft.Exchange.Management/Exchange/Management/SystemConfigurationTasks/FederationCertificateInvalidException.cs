using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010AC RID: 4268
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FederationCertificateInvalidException : FederationException
	{
		// Token: 0x0600B252 RID: 45650 RVA: 0x002999A4 File Offset: 0x00297BA4
		public FederationCertificateInvalidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B253 RID: 45651 RVA: 0x002999AD File Offset: 0x00297BAD
		public FederationCertificateInvalidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B254 RID: 45652 RVA: 0x002999B7 File Offset: 0x00297BB7
		protected FederationCertificateInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B255 RID: 45653 RVA: 0x002999C1 File Offset: 0x00297BC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
