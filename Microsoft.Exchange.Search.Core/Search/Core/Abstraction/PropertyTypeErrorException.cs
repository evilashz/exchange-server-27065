using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal class PropertyTypeErrorException : PropertyErrorException
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000025C6 File Offset: 0x000007C6
		public PropertyTypeErrorException(string property) : this(property, null)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025D0 File Offset: 0x000007D0
		public PropertyTypeErrorException(string property, Exception innerException) : base(property, Strings.PropertyTypeError(property), innerException)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000025E0 File Offset: 0x000007E0
		protected PropertyTypeErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
