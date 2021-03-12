using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000108 RID: 264
	[DataContract]
	public sealed class SyncCallerContext
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x00014F58 File Offset: 0x00013158
		private SyncCallerContext()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.context = string.Join("-", new string[]
				{
					Environment.MachineName,
					currentProcess.ProcessName,
					FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion,
					DateTime.UtcNow.ToString("u"),
					Guid.NewGuid().ToString("N")
				});
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00014FF8 File Offset: 0x000131F8
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00015000 File Offset: 0x00013200
		[DataMember]
		public string PartnerName { get; private set; }

		// Token: 0x06000716 RID: 1814 RVA: 0x0001500C File Offset: 0x0001320C
		public static SyncCallerContext Create(string partnerName)
		{
			if (string.IsNullOrWhiteSpace(partnerName))
			{
				throw new ArgumentException("partnerName");
			}
			return new SyncCallerContext
			{
				PartnerName = partnerName
			};
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001503C File Offset: 0x0001323C
		public override string ToString()
		{
			return string.Join("-", new string[]
			{
				this.PartnerName,
				this.context
			});
		}

		// Token: 0x04000403 RID: 1027
		private const string ContextSeparator = "-";

		// Token: 0x04000404 RID: 1028
		[DataMember]
		private readonly string context;
	}
}
