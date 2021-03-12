using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationServiceRpcResult
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002901 File Offset: 0x00000B01
		internal MigrationServiceRpcResult(MigrationServiceRpcMethodCode methodCode) : this(methodCode, MigrationServiceRpcResultCode.Success, null)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002910 File Offset: 0x00000B10
		internal MigrationServiceRpcResult(MigrationServiceRpcMethodCode methodCode, MigrationServiceRpcResultCode resultCode, string errorDetails)
		{
			this.methodCode = methodCode;
			this.resultCode = resultCode;
			this.message = errorDetails;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002930 File Offset: 0x00000B30
		internal MigrationServiceRpcResult(MdbefPropertyCollection inputArgs)
		{
			object obj;
			if (!inputArgs.TryGetValue(2936012803U, out obj) || !(obj is int))
			{
				throw new MigrationCommunicationException(MigrationServiceRpcResultCode.ResultParseError, "Result Code not found in return MbdefPropertyCollection");
			}
			this.resultCode = (MigrationServiceRpcResultCode)((int)obj);
			if (inputArgs.TryGetValue(2684420099U, out obj))
			{
				this.methodCode = (MigrationServiceRpcMethodCode)((int)obj);
			}
			else
			{
				this.methodCode = MigrationServiceRpcMethodCode.None;
			}
			if (inputArgs.TryGetValue(2936340511U, out obj))
			{
				this.message = (string)obj;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000029B5 File Offset: 0x00000BB5
		internal MigrationServiceRpcResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029BD File Offset: 0x00000BBD
		internal MigrationServiceRpcMethodCode MethodCode
		{
			get
			{
				return this.methodCode;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000029C5 File Offset: 0x00000BC5
		internal string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029CD File Offset: 0x00000BCD
		internal bool IsSuccess
		{
			get
			{
				return this.ResultCode == MigrationServiceRpcResultCode.Success;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029DC File Offset: 0x00000BDC
		internal MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2936012803U] = (int)this.ResultCode;
			mdbefPropertyCollection[2684420099U] = (int)this.MethodCode;
			if (this.message != null)
			{
				mdbefPropertyCollection[2936340511U] = this.message;
			}
			this.WriteTo(mdbefPropertyCollection);
			return mdbefPropertyCollection;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A3C File Offset: 0x00000C3C
		protected void ThrowIfVerifyFails(MigrationServiceRpcMethodCode expectedMethod)
		{
			if (this.MethodCode != MigrationServiceRpcMethodCode.None && this.MethodCode != expectedMethod)
			{
				throw new MigrationCommunicationException(MigrationServiceRpcResultCode.IncorrectMethodInvokedError, string.Format("Client requested method {0} to be invoked, but {1} was invoked", expectedMethod, this.MethodCode));
			}
			int num = (int)this.ResultCode;
			if ((num & 4096) != 0)
			{
				return;
			}
			if ((num & 256) != 0)
			{
				if (num == 16644)
				{
					throw new MigrationSubscriptionNotFoundException(this.ResultCode, this.message);
				}
				throw new MigrationObjectNotHostedException(this.ResultCode, this.message);
			}
			else
			{
				if ((num & 32768) != 0)
				{
					throw new MigrationServiceRpcTransientException(this.ResultCode, this.message);
				}
				if ((num & 8192) != 0)
				{
					throw new MigrationCommunicationException(this.ResultCode, this.message);
				}
				if ((num & 16384) != 0)
				{
					throw new MigrationTargetInvocationException(this.ResultCode, this.message);
				}
				throw new InvalidOperationException(string.Format("uknown rpc result code {0}", num));
			}
		}

		// Token: 0x06000033 RID: 51
		protected abstract void WriteTo(MdbefPropertyCollection collection);

		// Token: 0x04000062 RID: 98
		private readonly MigrationServiceRpcResultCode resultCode;

		// Token: 0x04000063 RID: 99
		private readonly MigrationServiceRpcMethodCode methodCode;

		// Token: 0x04000064 RID: 100
		private readonly string message;
	}
}
