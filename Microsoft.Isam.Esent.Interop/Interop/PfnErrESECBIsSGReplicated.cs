using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000011 RID: 17
	// (Invoke) Token: 0x06000034 RID: 52
	public delegate JET_err PfnErrESECBIsSGReplicated(ESEBACK_CONTEXT pContext, JET_INSTANCE jetinst, out bool pfReplicated, out Guid wszSGGuid, out LOGSHIP_INFO[] prgInfo);
}
