using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000158 RID: 344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotLoadMigrationPersistedItemTransientException : MigrationTransientException
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x0006EEAA File Offset: 0x0006D0AA
		public CouldNotLoadMigrationPersistedItemTransientException(string itemId) : base(Strings.CouldNotLoadMigrationPersistedItem(itemId))
		{
			this.itemId = itemId;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0006EEBF File Offset: 0x0006D0BF
		public CouldNotLoadMigrationPersistedItemTransientException(string itemId, Exception innerException) : base(Strings.CouldNotLoadMigrationPersistedItem(itemId), innerException)
		{
			this.itemId = itemId;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0006EED5 File Offset: 0x0006D0D5
		protected CouldNotLoadMigrationPersistedItemTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.itemId = (string)info.GetValue("itemId", typeof(string));
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0006EEFF File Offset: 0x0006D0FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("itemId", this.itemId);
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0006EF1A File Offset: 0x0006D11A
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x04000AE7 RID: 2791
		private readonly string itemId;
	}
}
