using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003F RID: 63
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceAccessDeniedException : TopologyServicePermanentException
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000F117 File Offset: 0x0000D317
		public ServiceAccessDeniedException() : base(Strings.AccessDenied)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F124 File Offset: 0x0000D324
		public ServiceAccessDeniedException(Exception innerException) : base(Strings.AccessDenied, innerException)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F132 File Offset: 0x0000D332
		protected ServiceAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F13C File Offset: 0x0000D33C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
