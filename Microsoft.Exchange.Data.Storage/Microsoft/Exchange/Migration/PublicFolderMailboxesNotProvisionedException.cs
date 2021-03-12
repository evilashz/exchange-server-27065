using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000163 RID: 355
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PublicFolderMailboxesNotProvisionedException : MigrationPermanentException
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x0006F52D File Offset: 0x0006D72D
		public PublicFolderMailboxesNotProvisionedException() : base(Strings.PublicFolderMailboxesNotProvisionedError)
		{
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0006F53A File Offset: 0x0006D73A
		public PublicFolderMailboxesNotProvisionedException(Exception innerException) : base(Strings.PublicFolderMailboxesNotProvisionedError, innerException)
		{
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0006F548 File Offset: 0x0006D748
		protected PublicFolderMailboxesNotProvisionedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0006F552 File Offset: 0x0006D752
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
