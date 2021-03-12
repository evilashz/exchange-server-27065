using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D91 RID: 3473
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EncryptionResults
	{
		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x06007782 RID: 30594 RVA: 0x0020ECAC File Offset: 0x0020CEAC
		// (set) Token: 0x06007783 RID: 30595 RVA: 0x0020ECB4 File Offset: 0x0020CEB4
		public EncryptedSharedFolderData[] EncryptedSharedFolderDataCollection { get; private set; }

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x06007784 RID: 30596 RVA: 0x0020ECBD File Offset: 0x0020CEBD
		// (set) Token: 0x06007785 RID: 30597 RVA: 0x0020ECC5 File Offset: 0x0020CEC5
		public InvalidRecipient[] InvalidRecipients { get; private set; }

		// Token: 0x06007786 RID: 30598 RVA: 0x0020ECCE File Offset: 0x0020CECE
		internal EncryptionResults(EncryptedSharedFolderData[] encryptedSharedFolderDataCollection, InvalidRecipient[] invalidRecipients)
		{
			this.EncryptedSharedFolderDataCollection = encryptedSharedFolderDataCollection;
			this.InvalidRecipients = invalidRecipients;
		}
	}
}
