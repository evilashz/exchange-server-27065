using System;
using System.Runtime.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	public class Pop3Exception : Exception
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x000375F7 File Offset: 0x000357F7
		public Pop3Exception()
		{
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000375FF File Offset: 0x000357FF
		public Pop3Exception(string message) : base(message)
		{
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00037608 File Offset: 0x00035808
		public Pop3Exception(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00037612 File Offset: 0x00035812
		public Pop3Exception(string message, string response, string command) : base(message)
		{
			this.LastResponse = response;
			this.LastCommand = command;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00037629 File Offset: 0x00035829
		protected Pop3Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00037633 File Offset: 0x00035833
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0003763B File Offset: 0x0003583B
		public string LastResponse { get; private set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00037644 File Offset: 0x00035844
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0003764C File Offset: 0x0003584C
		public string LastCommand { get; private set; }
	}
}
