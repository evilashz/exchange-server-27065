using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000727 RID: 1831
	[Serializable]
	public sealed class DefaultFolderPropertyValidationException : CorruptDataException
	{
		// Token: 0x060047C3 RID: 18371 RVA: 0x001304F3 File Offset: 0x0012E6F3
		public DefaultFolderPropertyValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x001304FC File Offset: 0x0012E6FC
		private DefaultFolderPropertyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
