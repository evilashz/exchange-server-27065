using System;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000011 RID: 17
	internal class AdminRpcListMdbStatus : AdminRpc
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003135 File Offset: 0x00001335
		public AdminRpcListMdbStatus(ClientSecurityContext callerSecurityContext, Guid[] mdbGuids, byte[] auxiliaryIn) : base(AdminMethod.EcListMdbStatus50, callerSecurityContext, auxiliaryIn)
		{
			this.mdbGuids = mdbGuids;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003148 File Offset: 0x00001348
		public uint[] MdbStatus
		{
			get
			{
				return this.mdbStatus;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003150 File Offset: 0x00001350
		protected override ErrorCode EcExecuteRpc(MapiContext context)
		{
			this.mdbStatus = new uint[this.mdbGuids.Length];
			for (int i = 0; i < this.mdbGuids.Length; i++)
			{
				StoreDatabase storeDatabase = Storage.FindDatabase(this.mdbGuids[i]);
				if (storeDatabase == null)
				{
					this.mdbStatus[i] = 0U;
				}
				else
				{
					storeDatabase.GetSharedLock(context.Diagnostics);
					try
					{
						this.mdbStatus[i] = storeDatabase.ExternalMdbStatus;
					}
					finally
					{
						storeDatabase.ReleaseSharedLock();
					}
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x04000059 RID: 89
		private Guid[] mdbGuids;

		// Token: 0x0400005A RID: 90
		private uint[] mdbStatus;
	}
}
