using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ReportTransportSyncWatsonException : Exception
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000781E File Offset: 0x00005A1E
		public ReportTransportSyncWatsonException(string message, Exception innerException, string stackTrace) : base(message, innerException)
		{
			this.stackTrace = stackTrace;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000782F File Offset: 0x00005A2F
		public ReportTransportSyncWatsonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00007839 File Offset: 0x00005A39
		public override string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
		}

		// Token: 0x0400011E RID: 286
		private readonly string stackTrace;
	}
}
