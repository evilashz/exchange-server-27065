using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F3 RID: 4595
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_CertNotFound : LocalizedException
	{
		// Token: 0x0600B99B RID: 47515 RVA: 0x002A62F0 File Offset: 0x002A44F0
		public TUC_CertNotFound() : base(Strings.CertNotFound)
		{
		}

		// Token: 0x0600B99C RID: 47516 RVA: 0x002A62FD File Offset: 0x002A44FD
		public TUC_CertNotFound(Exception innerException) : base(Strings.CertNotFound, innerException)
		{
		}

		// Token: 0x0600B99D RID: 47517 RVA: 0x002A630B File Offset: 0x002A450B
		protected TUC_CertNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B99E RID: 47518 RVA: 0x002A6315 File Offset: 0x002A4515
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
