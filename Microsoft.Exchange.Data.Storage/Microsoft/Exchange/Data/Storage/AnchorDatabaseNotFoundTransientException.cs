using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000108 RID: 264
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AnchorDatabaseNotFoundTransientException : MigrationTransientException
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x00069A9A File Offset: 0x00067C9A
		public AnchorDatabaseNotFoundTransientException(string mdbGuid) : base(ServerStrings.AnchorDatabaseNotFound(mdbGuid))
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00069AAF File Offset: 0x00067CAF
		public AnchorDatabaseNotFoundTransientException(string mdbGuid, Exception innerException) : base(ServerStrings.AnchorDatabaseNotFound(mdbGuid), innerException)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00069AC5 File Offset: 0x00067CC5
		protected AnchorDatabaseNotFoundTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (string)info.GetValue("mdbGuid", typeof(string));
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00069AEF File Offset: 0x00067CEF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00069B0A File Offset: 0x00067D0A
		public string MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x04000997 RID: 2455
		private readonly string mdbGuid;
	}
}
