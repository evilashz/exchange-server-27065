using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE3 RID: 2787
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserWithoutFederatedProxyAddressException : ADOperationException
	{
		// Token: 0x06008125 RID: 33061 RVA: 0x001A6399 File Offset: 0x001A4599
		public UserWithoutFederatedProxyAddressException() : base(DirectoryStrings.UserHasNoSmtpProxyAddressWithFederatedDomain)
		{
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x001A63A6 File Offset: 0x001A45A6
		public UserWithoutFederatedProxyAddressException(Exception innerException) : base(DirectoryStrings.UserHasNoSmtpProxyAddressWithFederatedDomain, innerException)
		{
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x001A63B4 File Offset: 0x001A45B4
		protected UserWithoutFederatedProxyAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x001A63BE File Offset: 0x001A45BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
