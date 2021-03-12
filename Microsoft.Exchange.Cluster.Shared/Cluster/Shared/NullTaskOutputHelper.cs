using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000012 RID: 18
	internal class NullTaskOutputHelper : NullLogTraceHelper, ITaskOutputHelper, ILogTraceHelper
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000035EE File Offset: 0x000017EE
		public new static ITaskOutputHelper GetNullLogger()
		{
			return NullTaskOutputHelper.s_nullLogger;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000035F8 File Offset: 0x000017F8
		public void WriteErrorSimple(Exception error)
		{
			base.AppendLogMessage("Exception! {0}", new object[]
			{
				error
			});
			throw error;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000361D File Offset: 0x0000181D
		public void WriteWarning(LocalizedString locString)
		{
			base.AppendLogMessage(locString);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003626 File Offset: 0x00001826
		public void WriteProgressSimple(LocalizedString locString)
		{
			base.AppendLogMessage(locString);
		}

		// Token: 0x04000020 RID: 32
		private static NullTaskOutputHelper s_nullLogger = new NullTaskOutputHelper();
	}
}
