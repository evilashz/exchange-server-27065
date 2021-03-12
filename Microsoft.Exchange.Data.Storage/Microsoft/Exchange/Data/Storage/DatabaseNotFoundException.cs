using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseNotFoundException : ObjectNotFoundException
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x00067976 File Offset: 0x00065B76
		public DatabaseNotFoundException(string databaseId) : base(ServerStrings.DatabaseNotFound(databaseId))
		{
			this.databaseId = databaseId;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006798B File Offset: 0x00065B8B
		public DatabaseNotFoundException(string databaseId, Exception innerException) : base(ServerStrings.DatabaseNotFound(databaseId), innerException)
		{
			this.databaseId = databaseId;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000679A1 File Offset: 0x00065BA1
		protected DatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseId = (string)info.GetValue("databaseId", typeof(string));
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000679CB File Offset: 0x00065BCB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseId", this.databaseId);
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x000679E6 File Offset: 0x00065BE6
		public string DatabaseId
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x04000958 RID: 2392
		private readonly string databaseId;
	}
}
