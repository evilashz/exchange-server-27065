using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7B RID: 2683
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveTenantNameException : DataSourceOperationException
	{
		// Token: 0x06007F49 RID: 32585 RVA: 0x001A3FB2 File Offset: 0x001A21B2
		public CannotResolveTenantNameException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x001A3FBB File Offset: 0x001A21BB
		public CannotResolveTenantNameException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x001A3FC5 File Offset: 0x001A21C5
		protected CannotResolveTenantNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x001A3FCF File Offset: 0x001A21CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
