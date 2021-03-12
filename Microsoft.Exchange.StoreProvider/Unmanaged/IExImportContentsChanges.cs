using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200029A RID: 666
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExImportContentsChanges : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C55 RID: 3157
		int Config(IStream iStream, int ulFlags);

		// Token: 0x06000C56 RID: 3158
		int UpdateState(IStream iStream);

		// Token: 0x06000C57 RID: 3159
		unsafe int ImportMessageChange(int cpvalChanges, SPropValue* ppvalChanges, int ulFlags, out SafeExMapiMessageHandle iMessage);

		// Token: 0x06000C58 RID: 3160
		unsafe int ImportMessageDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);

		// Token: 0x06000C59 RID: 3161
		unsafe int ImportPerUserReadStateChange(int cElements, _ReadState* lpReadState);

		// Token: 0x06000C5A RID: 3162
		int ImportMessageMove(int cbSourceKeySrcFolder, byte[] pbSourceKeySrcFolder, int cbSourceKeySrcMessage, byte[] pbSourceKeySrcMessage, int cbPCLMessage, byte[] pbPCLMessage, int cbSourceKeyDestMessage, byte[] pbSourceKeyDestMessage, int cbChangeNumDestMessage, byte[] pbChangeNumDestMessage);
	}
}
