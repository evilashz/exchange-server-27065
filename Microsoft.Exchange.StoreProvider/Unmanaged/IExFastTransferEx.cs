using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200029D RID: 669
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExFastTransferEx : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C62 RID: 3170
		int Config(int ulFlags, int ulTransferMethod);

		// Token: 0x06000C63 RID: 3171
		int TransferBuffer(int cbData, byte[] data, out int cbProcessed);

		// Token: 0x06000C64 RID: 3172
		int IsInterfaceOk(int ulTransferMethod, ref Guid refiid, IntPtr lpPropTagArray, int ulFlags);

		// Token: 0x06000C65 RID: 3173
		int GetObjectType(out Guid iid);

		// Token: 0x06000C66 RID: 3174
		int GetLastLowLevelError(out int lowLevelError);

		// Token: 0x06000C67 RID: 3175
		unsafe int GetServerVersion(int cbBufferSize, byte* pBuffer, out int cbBuffer);

		// Token: 0x06000C68 RID: 3176
		unsafe int TellPartnerVersion(int cbBuffer, byte* pBuffer);

		// Token: 0x06000C69 RID: 3177
		int IsPrivateLogon();

		// Token: 0x06000C6A RID: 3178
		int StartMdbEventsImport();

		// Token: 0x06000C6B RID: 3179
		int FinishMdbEventsImport(bool bSuccess);

		// Token: 0x06000C6C RID: 3180
		int SetWatermarks(int cWMs, MDBEVENTWMRAW[] WMs);

		// Token: 0x06000C6D RID: 3181
		int AddMdbEvents(int cbRequest, byte[] pbRequest);

		// Token: 0x06000C6E RID: 3182
		int SetReceiveFolder(int cbEntryId, byte[] entryId, string messageClass);

		// Token: 0x06000C6F RID: 3183
		unsafe int SetPerUser(ref MapiLtidNative pltid, Guid* guidReplica, int lib, byte[] pb, int cb, bool fLast);

		// Token: 0x06000C70 RID: 3184
		unsafe int SetProps(int cValues, SPropValue* lpPropArray);
	}
}
