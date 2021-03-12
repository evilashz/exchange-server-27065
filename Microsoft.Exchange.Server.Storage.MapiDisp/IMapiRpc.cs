using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000006 RID: 6
	internal interface IMapiRpc : IDisposable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000169 RID: 361
		Dictionary<int, MapiSession> SessionsHash { get; }

		// Token: 0x0600016A RID: 362
		int Initialize();

		// Token: 0x0600016B RID: 363
		int DoConnect(IExecutionDiagnostics executionDiagnostics, out IntPtr contextHandle, string userDn, ClientSecurityContext callerSecurityContext, byte[] sessionSecurityContext, int flags, int connectionMode, int codePageId, int localeIdString, int localeIdSort, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string distinguishedNamePrefix, out string displayName, short[] clientVersion, ArraySegment<byte> auxIn, ref byte[] auxOut, out int sizeAuxOut, Action<int> notificationPendingCallback);

		// Token: 0x0600016C RID: 364
		int DoDisconnect(IExecutionDiagnostics executionDiagnostics, ref IntPtr contextHandle);

		// Token: 0x0600016D RID: 365
		int DoRpc(IExecutionDiagnostics executionDiagnostics, ref IntPtr contextHandle, IList<ArraySegment<byte>> ropInArray, ArraySegment<byte> ropOut, out int sizeRopOut, bool internalAccessPrivileges, ArraySegment<byte> auxIn, ArraySegment<byte> auxOut, out int sizeAuxOut, bool fakeRequest, out byte[] fakeOut);

		// Token: 0x0600016E RID: 366
		MapiSession SessionFromSessionId(int sessionId);

		// Token: 0x0600016F RID: 367
		void DeregisterSession(MapiContext context, MapiSession session);

		// Token: 0x06000170 RID: 368
		IEnumerable<MapiSession> GetSessionListSnapshot();
	}
}
