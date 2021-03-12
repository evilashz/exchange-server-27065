using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	public interface ICspAsymmetricAlgorithm
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060021FF RID: 8703
		CspKeyContainerInfo CspKeyContainerInfo { get; }

		// Token: 0x06002200 RID: 8704
		byte[] ExportCspBlob(bool includePrivateParameters);

		// Token: 0x06002201 RID: 8705
		void ImportCspBlob(byte[] rawData);
	}
}
