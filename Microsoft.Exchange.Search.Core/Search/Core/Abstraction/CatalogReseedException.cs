using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class CatalogReseedException : ComponentFailedException
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022DE File Offset: 0x000004DE
		public CatalogReseedException(IndexStatusErrorCode errorCode) : base(Strings.OperationFailure)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F2 File Offset: 0x000004F2
		public CatalogReseedException(Exception innerException) : base(Strings.OperationFailure, innerException)
		{
			this.errorCode = IndexStatusErrorCode.CatalogReseed;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002308 File Offset: 0x00000508
		public CatalogReseedException(LocalizedString message, IndexStatusErrorCode errorCode) : base(message)
		{
			this.errorCode = errorCode;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002318 File Offset: 0x00000518
		public CatalogReseedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.errorCode = IndexStatusErrorCode.CatalogReseed;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000232A File Offset: 0x0000052A
		protected CatalogReseedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorCode = IndexStatusErrorCode.CatalogReseed;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000233C File Offset: 0x0000053C
		internal IndexStatusErrorCode OriginalErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002344 File Offset: 0x00000544
		internal override void RethrowNewInstance()
		{
			throw new ComponentFailedPermanentException(this);
		}

		// Token: 0x0400000D RID: 13
		private IndexStatusErrorCode errorCode;
	}
}
