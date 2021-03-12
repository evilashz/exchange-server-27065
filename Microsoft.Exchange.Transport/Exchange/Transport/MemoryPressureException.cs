using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	internal sealed class MemoryPressureException : SystemException
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000F6D7 File Offset: 0x0000D8D7
		public MemoryPressureException() : base("The Process Manager indicated that process is under memory pressure.")
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public MemoryPressureException(uint percentLoad, ulong totalMemory) : base(string.Format(CultureInfo.InvariantCulture, "The Process Manager indicated that process is under memory pressure. The Percentage of physical memory in use is {0}%. The Total installed memory is bytes is {1}.", new object[]
		{
			percentLoad,
			totalMemory
		}))
		{
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F720 File Offset: 0x0000D920
		public MemoryPressureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040001DD RID: 477
		private const string MemoryPressureMessage = "The Process Manager indicated that process is under memory pressure.";

		// Token: 0x040001DE RID: 478
		private const string MemoryPressureMessageWithStats = "The Process Manager indicated that process is under memory pressure. The Percentage of physical memory in use is {0}%. The Total installed memory is bytes is {1}.";
	}
}
