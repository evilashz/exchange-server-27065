using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationServiceRpcException : MigrationPermanentException
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000020D0 File Offset: 0x000002D0
		internal MigrationServiceRpcException(MigrationServiceRpcResultCode resultCode, string message) : base(Strings.MigrationRpcFailed(resultCode.ToString()), message)
		{
			this.ResultCode = resultCode;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020F0 File Offset: 0x000002F0
		internal MigrationServiceRpcException(MigrationServiceRpcResultCode resultCode, string message, Exception innerException) : base(Strings.MigrationRpcFailed(resultCode.ToString()), message, innerException)
		{
			this.ResultCode = resultCode;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002111 File Offset: 0x00000311
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002128 File Offset: 0x00000328
		internal MigrationServiceRpcResultCode ResultCode
		{
			get
			{
				return (MigrationServiceRpcResultCode)this.Data["ResultCode"];
			}
			private set
			{
				this.Data["ResultCode"] = value;
			}
		}
	}
}
