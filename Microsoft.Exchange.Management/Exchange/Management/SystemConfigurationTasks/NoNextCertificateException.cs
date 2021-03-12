using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B2 RID: 4274
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoNextCertificateException : FederationException
	{
		// Token: 0x0600B26B RID: 45675 RVA: 0x00299AF7 File Offset: 0x00297CF7
		public NoNextCertificateException() : base(Strings.ErrorNoNextCertificate)
		{
		}

		// Token: 0x0600B26C RID: 45676 RVA: 0x00299B04 File Offset: 0x00297D04
		public NoNextCertificateException(Exception innerException) : base(Strings.ErrorNoNextCertificate, innerException)
		{
		}

		// Token: 0x0600B26D RID: 45677 RVA: 0x00299B12 File Offset: 0x00297D12
		protected NoNextCertificateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B26E RID: 45678 RVA: 0x00299B1C File Offset: 0x00297D1C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
