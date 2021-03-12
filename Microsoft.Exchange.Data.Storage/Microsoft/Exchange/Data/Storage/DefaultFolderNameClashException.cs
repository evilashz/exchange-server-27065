using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000726 RID: 1830
	[Serializable]
	public sealed class DefaultFolderNameClashException : CorruptDataException
	{
		// Token: 0x060047C1 RID: 18369 RVA: 0x001304E0 File Offset: 0x0012E6E0
		public DefaultFolderNameClashException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x001304E9 File Offset: 0x0012E6E9
		private DefaultFolderNameClashException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
