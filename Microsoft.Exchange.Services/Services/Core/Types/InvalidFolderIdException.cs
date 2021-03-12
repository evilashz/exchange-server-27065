using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AE RID: 1966
	[Serializable]
	internal sealed class InvalidFolderIdException : InvalidStoreIdException
	{
		// Token: 0x06003AA9 RID: 15017 RVA: 0x000CF3C6 File Offset: 0x000CD5C6
		public InvalidFolderIdException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000CF3CF File Offset: 0x000CD5CF
		public InvalidFolderIdException(Enum messageId, Exception innerException) : base(messageId, innerException)
		{
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000CF3D9 File Offset: 0x000CD5D9
		public InvalidFolderIdException(ResponseCodeType responseCode, Enum messageId) : base(responseCode, messageId)
		{
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000CF3E3 File Offset: 0x000CD5E3
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
