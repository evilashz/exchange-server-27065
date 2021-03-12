using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x0200040E RID: 1038
	[Serializable]
	public class UnmatchedIisVersionException : LocalizedException
	{
		// Token: 0x0600244F RID: 9295 RVA: 0x00090C01 File Offset: 0x0008EE01
		public UnmatchedIisVersionException() : base(Strings.ExceptionInvalidIisVersion)
		{
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00090C0E File Offset: 0x0008EE0E
		public UnmatchedIisVersionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00090C17 File Offset: 0x0008EE17
		public UnmatchedIisVersionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00090C21 File Offset: 0x0008EE21
		protected UnmatchedIisVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
