using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000248 RID: 584
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MissingCertificateException : TransientDALException
	{
		// Token: 0x06001735 RID: 5941 RVA: 0x0004796E File Offset: 0x00045B6E
		public MissingCertificateException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00047977 File Offset: 0x00045B77
		public MissingCertificateException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00047981 File Offset: 0x00045B81
		protected MissingCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0004798B File Offset: 0x00045B8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
