using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000012 RID: 18
	[DataContract(Name = "BlackoutInterval", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class BlackoutInterval : IExtensibleDataObject
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000028D8 File Offset: 0x00000AD8
		public BlackoutInterval(DateTime startDate, DateTime endDate, string reason)
		{
			this.StartDate = startDate;
			this.EndDate = endDate;
			this.Reason = reason;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000028F5 File Offset: 0x00000AF5
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000028FD File Offset: 0x00000AFD
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002906 File Offset: 0x00000B06
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000290E File Offset: 0x00000B0E
		[DataMember]
		public DateTime EndDate
		{
			get
			{
				return this.EndDateField;
			}
			set
			{
				this.EndDateField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002917 File Offset: 0x00000B17
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000291F File Offset: 0x00000B1F
		[DataMember]
		public string Reason
		{
			get
			{
				return this.ReasonField;
			}
			set
			{
				this.ReasonField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002930 File Offset: 0x00000B30
		[DataMember]
		public DateTime StartDate
		{
			get
			{
				return this.StartDateField;
			}
			set
			{
				this.StartDateField = value;
			}
		}

		// Token: 0x0400002C RID: 44
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400002D RID: 45
		private DateTime EndDateField;

		// Token: 0x0400002E RID: 46
		private string ReasonField;

		// Token: 0x0400002F RID: 47
		private DateTime StartDateField;
	}
}
