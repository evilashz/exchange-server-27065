using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BitlockerCertificatesNotFoundException : BitlockerUtilException
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004DFC File Offset: 0x00002FFC
		public BitlockerCertificatesNotFoundException() : base(DiskManagementStrings.BitlockerCertificatesNotFoundError)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E0E File Offset: 0x0000300E
		public BitlockerCertificatesNotFoundException(Exception innerException) : base(DiskManagementStrings.BitlockerCertificatesNotFoundError, innerException)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004E21 File Offset: 0x00003021
		protected BitlockerCertificatesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004E2B File Offset: 0x0000302B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
