using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000109 RID: 265
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AnchorServerNotFoundTransientException : MigrationTransientException
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x00069B12 File Offset: 0x00067D12
		public AnchorServerNotFoundTransientException(string mdbGuid) : base(ServerStrings.AnchorServerNotFound(mdbGuid))
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00069B27 File Offset: 0x00067D27
		public AnchorServerNotFoundTransientException(string mdbGuid, Exception innerException) : base(ServerStrings.AnchorServerNotFound(mdbGuid), innerException)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00069B3D File Offset: 0x00067D3D
		protected AnchorServerNotFoundTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (string)info.GetValue("mdbGuid", typeof(string));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00069B67 File Offset: 0x00067D67
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00069B82 File Offset: 0x00067D82
		public string MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x04000998 RID: 2456
		private readonly string mdbGuid;
	}
}
