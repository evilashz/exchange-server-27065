using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE4 RID: 2788
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FederatedIdentityMisconfiguredException : ADOperationException
	{
		// Token: 0x06008129 RID: 33065 RVA: 0x001A63C8 File Offset: 0x001A45C8
		public FederatedIdentityMisconfiguredException() : base(DirectoryStrings.FederatedIdentityMisconfigured)
		{
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x001A63D5 File Offset: 0x001A45D5
		public FederatedIdentityMisconfiguredException(Exception innerException) : base(DirectoryStrings.FederatedIdentityMisconfigured, innerException)
		{
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x001A63E3 File Offset: 0x001A45E3
		protected FederatedIdentityMisconfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x001A63ED File Offset: 0x001A45ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
