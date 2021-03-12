using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000168 RID: 360
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OnlyOnePublicFolderBatchIsAllowedException : MigrationPermanentException
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x0006F6B6 File Offset: 0x0006D8B6
		public OnlyOnePublicFolderBatchIsAllowedException() : base(Strings.OnlyOnePublicFolderBatchIsAllowedError)
		{
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0006F6C3 File Offset: 0x0006D8C3
		public OnlyOnePublicFolderBatchIsAllowedException(Exception innerException) : base(Strings.OnlyOnePublicFolderBatchIsAllowedError, innerException)
		{
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0006F6D1 File Offset: 0x0006D8D1
		protected OnlyOnePublicFolderBatchIsAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0006F6DB File Offset: 0x0006D8DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
