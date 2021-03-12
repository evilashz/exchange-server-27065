using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000777 RID: 1911
	[Serializable]
	public class InvalidLogCollectionException : ApplicationException
	{
		// Token: 0x06004388 RID: 17288 RVA: 0x001151C6 File Offset: 0x001133C6
		public InvalidLogCollectionException()
		{
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x001151CE File Offset: 0x001133CE
		public InvalidLogCollectionException(string message) : base(message)
		{
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x001151D7 File Offset: 0x001133D7
		public InvalidLogCollectionException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x001151E1 File Offset: 0x001133E1
		protected InvalidLogCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
