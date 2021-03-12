using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000161 RID: 353
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingRootFolderMappingInCSVException : MigrationPermanentException
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x0006F3E9 File Offset: 0x0006D5E9
		public MissingRootFolderMappingInCSVException(string hierarchyMailboxName) : base(Strings.MissingRootFolderMappingInCSVError(hierarchyMailboxName))
		{
			this.hierarchyMailboxName = hierarchyMailboxName;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006F3FE File Offset: 0x0006D5FE
		public MissingRootFolderMappingInCSVException(string hierarchyMailboxName, Exception innerException) : base(Strings.MissingRootFolderMappingInCSVError(hierarchyMailboxName), innerException)
		{
			this.hierarchyMailboxName = hierarchyMailboxName;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0006F414 File Offset: 0x0006D614
		protected MissingRootFolderMappingInCSVException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hierarchyMailboxName = (string)info.GetValue("hierarchyMailboxName", typeof(string));
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0006F43E File Offset: 0x0006D63E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hierarchyMailboxName", this.hierarchyMailboxName);
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0006F459 File Offset: 0x0006D659
		public string HierarchyMailboxName
		{
			get
			{
				return this.hierarchyMailboxName;
			}
		}

		// Token: 0x04000AF3 RID: 2803
		private readonly string hierarchyMailboxName;
	}
}
