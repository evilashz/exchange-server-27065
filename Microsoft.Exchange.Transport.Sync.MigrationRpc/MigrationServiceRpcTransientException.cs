using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationServiceRpcTransientException : MigrationTransientException
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002B2D File Offset: 0x00000D2D
		internal MigrationServiceRpcTransientException(int rpcErrorCode, bool isConnectionError, Exception innerException) : base(Strings.MigrationRpcFailed(rpcErrorCode.ToString(CultureInfo.InvariantCulture)), innerException)
		{
			this.isConnectionError = isConnectionError;
			this.rpcErrorCode = new int?(rpcErrorCode);
			this.resultCode = MigrationServiceRpcResultCode.RpcTransientException;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B65 File Offset: 0x00000D65
		internal MigrationServiceRpcTransientException(MigrationServiceRpcResultCode resultCode, string message) : base(Strings.MigrationRpcFailed(resultCode.ToString()), message)
		{
			this.isConnectionError = false;
			this.rpcErrorCode = null;
			this.resultCode = resultCode;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002B98 File Offset: 0x00000D98
		public int? RpcErrorCode
		{
			get
			{
				return this.rpcErrorCode;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public bool IsConnectionError
		{
			get
			{
				return this.isConnectionError;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002BA8 File Offset: 0x00000DA8
		internal MigrationServiceRpcResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly int? rpcErrorCode;

		// Token: 0x0400006D RID: 109
		private readonly bool isConnectionError;

		// Token: 0x0400006E RID: 110
		private readonly MigrationServiceRpcResultCode resultCode;
	}
}
