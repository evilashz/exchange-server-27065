using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ACD RID: 2765
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PrivateKeyInformation
	{
		// Token: 0x0600648D RID: 25741 RVA: 0x001AA61C File Offset: 0x001A881C
		public PrivateKeyInformation(string keyId, string keyIdType, string keyContainerName, int keyNumber, string cSPName, int cSPType, string encryptedPrivateKeyBlob, bool isSLCKey)
		{
			this.KeyId = keyId;
			this.KeyIdType = keyIdType;
			this.KeyContainerName = keyContainerName;
			this.CSPName = cSPName;
			this.CSPType = cSPType;
			this.KeyNumber = keyNumber;
			this.Identity = this.KeyId + this.KeyIdType;
			this.EncryptedPrivateKeyBlob = encryptedPrivateKeyBlob;
			this.IsSLCKey = isSLCKey;
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001AA683 File Offset: 0x001A8883
		public static string GetIdentity(string keyId, string keyIdType)
		{
			return keyId + keyIdType;
		}

		// Token: 0x04003931 RID: 14641
		public readonly int KeyNumber;

		// Token: 0x04003932 RID: 14642
		public readonly int CSPType;

		// Token: 0x04003933 RID: 14643
		public readonly string CSPName;

		// Token: 0x04003934 RID: 14644
		public readonly string KeyContainerName;

		// Token: 0x04003935 RID: 14645
		public readonly string KeyId;

		// Token: 0x04003936 RID: 14646
		public readonly string KeyIdType;

		// Token: 0x04003937 RID: 14647
		public readonly string Identity;

		// Token: 0x04003938 RID: 14648
		public readonly string EncryptedPrivateKeyBlob;

		// Token: 0x04003939 RID: 14649
		public readonly bool IsSLCKey;
	}
}
