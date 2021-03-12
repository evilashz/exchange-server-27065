using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119B RID: 4507
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoWebServicesExternalUrlFoundException : LocalizedException
	{
		// Token: 0x0600B6FC RID: 46844 RVA: 0x002A0C7A File Offset: 0x0029EE7A
		public NoWebServicesExternalUrlFoundException() : base(Strings.NoWebServicesExternalUrlFoundException)
		{
		}

		// Token: 0x0600B6FD RID: 46845 RVA: 0x002A0C87 File Offset: 0x0029EE87
		public NoWebServicesExternalUrlFoundException(Exception innerException) : base(Strings.NoWebServicesExternalUrlFoundException, innerException)
		{
		}

		// Token: 0x0600B6FE RID: 46846 RVA: 0x002A0C95 File Offset: 0x0029EE95
		protected NoWebServicesExternalUrlFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B6FF RID: 46847 RVA: 0x002A0C9F File Offset: 0x0029EE9F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
