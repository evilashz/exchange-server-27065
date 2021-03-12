using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class TransientException : LocalizedException
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000034E4 File Offset: 0x000016E4
		public TransientException(LocalizedString localizedString) : this(localizedString, null)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034EE File Offset: 0x000016EE
		public TransientException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
			this.localizedString = localizedString;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000034FF File Offset: 0x000016FF
		protected TransientException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003509 File Offset: 0x00001709
		public new LocalizedString LocalizedString
		{
			get
			{
				return this.localizedString;
			}
		}

		// Token: 0x04000021 RID: 33
		private LocalizedString localizedString;
	}
}
