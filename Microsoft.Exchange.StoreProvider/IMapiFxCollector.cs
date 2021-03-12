using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiFxCollector
	{
		// Token: 0x0600025E RID: 606
		Guid GetObjectType();

		// Token: 0x0600025F RID: 607
		byte[] GetServerVersion();

		// Token: 0x06000260 RID: 608
		bool IsPrivateLogon();

		// Token: 0x06000261 RID: 609
		void Config(int ulFlags, int ulTransferMethod);

		// Token: 0x06000262 RID: 610
		void TransferBuffer(byte[] data);

		// Token: 0x06000263 RID: 611
		void IsInterfaceOk(int ulTransferMethod, Guid refiid, int ulFlags);

		// Token: 0x06000264 RID: 612
		void TellPartnerVersion(byte[] versionData);

		// Token: 0x06000265 RID: 613
		void StartMdbEventsImport();

		// Token: 0x06000266 RID: 614
		void FinishMdbEventsImport(bool success);

		// Token: 0x06000267 RID: 615
		void AddMdbEvents(byte[] request);

		// Token: 0x06000268 RID: 616
		void SetWatermarks(MDBEVENTWMRAW[] WMs);

		// Token: 0x06000269 RID: 617
		void SetReceiveFolder(byte[] entryId, string messageClass);

		// Token: 0x0600026A RID: 618
		void SetPerUser(MapiLtidNative ltid, Guid pguidReplica, int lib, byte[] pb, bool fLast);

		// Token: 0x0600026B RID: 619
		void SetProps(PropValue[] pva);
	}
}
