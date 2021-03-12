using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000AFD RID: 2813
	[Serializable]
	internal class EnumArgumentException : ExArgumentException
	{
		// Token: 0x06003C70 RID: 15472 RVA: 0x0009CF63 File Offset: 0x0009B163
		public EnumArgumentException()
		{
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x0009CF6B File Offset: 0x0009B16B
		public EnumArgumentException(string message) : base(message)
		{
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x0009CF74 File Offset: 0x0009B174
		public EnumArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x0009CF7E File Offset: 0x0009B17E
		public EnumArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
		{
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x0009CF89 File Offset: 0x0009B189
		public EnumArgumentException(string message, string paramName) : base(message, paramName)
		{
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x0009CF93 File Offset: 0x0009B193
		protected EnumArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
