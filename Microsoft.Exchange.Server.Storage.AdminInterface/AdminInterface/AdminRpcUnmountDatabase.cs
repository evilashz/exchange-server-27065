using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000013 RID: 19
	internal class AdminRpcUnmountDatabase : AdminRpc
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000033C0 File Offset: 0x000015C0
		public AdminRpcUnmountDatabase(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn) : base(AdminMethod.EcUnmountDatabase50, callerSecurityContext, auxiliaryIn)
		{
			base.MdbGuid = new Guid?(mdbGuid);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000033D9 File Offset: 0x000015D9
		protected override ErrorCode EcCheckPermissions(MapiContext context)
		{
			return AdminRpcPermissionChecks.EcCheckConstrainedDelegationRights(context, base.DatabaseInfo);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000033E8 File Offset: 0x000015E8
		protected override ErrorCode EcExecuteRpc(MapiContext context)
		{
			ErrorCode errorCode = Storage.DismountDatabase(context, base.MdbGuid.Value);
			if (errorCode != ErrorCode.NoError)
			{
				errorCode = errorCode.Propagate((LID)17312U);
			}
			return errorCode;
		}
	}
}
