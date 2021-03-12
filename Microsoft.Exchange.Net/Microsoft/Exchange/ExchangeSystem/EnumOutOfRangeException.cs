using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000AFE RID: 2814
	[Serializable]
	internal class EnumOutOfRangeException : ExArgumentOutOfRangeException
	{
		// Token: 0x06003C76 RID: 15478 RVA: 0x0009CF9D File Offset: 0x0009B19D
		public EnumOutOfRangeException()
		{
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x0009CFA5 File Offset: 0x0009B1A5
		public EnumOutOfRangeException(string paramName, string message) : base(paramName, message)
		{
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x0009CFAF File Offset: 0x0009B1AF
		public EnumOutOfRangeException(string paramName) : base(paramName)
		{
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0009CFB8 File Offset: 0x0009B1B8
		public EnumOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x0009CFC2 File Offset: 0x0009B1C2
		public EnumOutOfRangeException(string paramName, object actualValue, string message) : base(paramName, actualValue, message)
		{
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x0009CFCD File Offset: 0x0009B1CD
		protected EnumOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
