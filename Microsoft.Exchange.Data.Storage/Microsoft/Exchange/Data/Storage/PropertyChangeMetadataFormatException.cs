using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000764 RID: 1892
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyChangeMetadataFormatException : CorruptDataException
	{
		// Token: 0x06004878 RID: 18552 RVA: 0x00131084 File Offset: 0x0012F284
		public PropertyChangeMetadataFormatException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x0013108D File Offset: 0x0012F28D
		public PropertyChangeMetadataFormatException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x00131097 File Offset: 0x0012F297
		protected PropertyChangeMetadataFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
