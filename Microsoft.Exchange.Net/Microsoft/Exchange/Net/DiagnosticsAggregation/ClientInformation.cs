using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200083E RID: 2110
	[DataContract]
	internal class ClientInformation
	{
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x00065502 File Offset: 0x00063702
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0006550A File Offset: 0x0006370A
		[DataMember(IsRequired = true)]
		public uint SessionId { get; private set; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x00065513 File Offset: 0x00063713
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x0006551B File Offset: 0x0006371B
		[DataMember(IsRequired = true)]
		public string ClientProcessName { get; private set; }

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x00065524 File Offset: 0x00063724
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x0006552C File Offset: 0x0006372C
		[DataMember(IsRequired = true)]
		public int ClientProcessId { get; private set; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x00065535 File Offset: 0x00063735
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0006553D File Offset: 0x0006373D
		[DataMember(IsRequired = true)]
		public string ClientMachineName { get; private set; }

		// Token: 0x06002D1B RID: 11547 RVA: 0x00065546 File Offset: 0x00063746
		public void SetClientInformation()
		{
			this.ClientProcessId = ClientInformation.currentProcess.Id;
			this.ClientProcessName = ClientInformation.currentProcess.ProcessName;
			this.ClientMachineName = ClientInformation.machineName;
			this.SessionId = (uint)ClientInformation.random.Next();
		}

		// Token: 0x04002723 RID: 10019
		private static readonly Process currentProcess = Process.GetCurrentProcess();

		// Token: 0x04002724 RID: 10020
		private static readonly string machineName = Environment.MachineName;

		// Token: 0x04002725 RID: 10021
		private static readonly Random random = new Random();
	}
}
