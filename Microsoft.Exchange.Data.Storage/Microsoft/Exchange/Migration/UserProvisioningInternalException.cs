using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016C RID: 364
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserProvisioningInternalException : MigrationPermanentException
	{
		// Token: 0x06001681 RID: 5761 RVA: 0x0006F7C0 File Offset: 0x0006D9C0
		public UserProvisioningInternalException() : base(Strings.UserProvisioningInternalError)
		{
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0006F7CD File Offset: 0x0006D9CD
		public UserProvisioningInternalException(Exception innerException) : base(Strings.UserProvisioningInternalError, innerException)
		{
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0006F7DB File Offset: 0x0006D9DB
		protected UserProvisioningInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0006F7E5 File Offset: 0x0006D9E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
