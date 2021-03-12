using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000624 RID: 1572
	[Serializable]
	public sealed class CorruptedPerformanceCountersException : Exception
	{
		// Token: 0x060037A2 RID: 14242 RVA: 0x000E6C68 File Offset: 0x000E4E68
		public CorruptedPerformanceCountersException(Exception innerException) : base(string.Empty, innerException)
		{
		}
	}
}
