using System;
using System.IO;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A4 RID: 1188
	internal class ExportUMCallDataRecordParameters : WebServiceParameters
	{
		// Token: 0x1700234D RID: 9037
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000B2858 File Offset: 0x000B0A58
		public override string AssociatedCmdlet
		{
			get
			{
				return "Export-UMCallDataRecord";
			}
		}

		// Token: 0x1700234E RID: 9038
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000B285F File Offset: 0x000B0A5F
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000B2866 File Offset: 0x000B0A66
		// (set) Token: 0x06003AFE RID: 15102 RVA: 0x000B2878 File Offset: 0x000B0A78
		public ExDateTime Date
		{
			get
			{
				return (ExDateTime)base["Date"];
			}
			set
			{
				base["Date"] = value;
			}
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x000B288B File Offset: 0x000B0A8B
		// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000B289D File Offset: 0x000B0A9D
		public string UMDialPlan
		{
			get
			{
				return (string)base["UMDialPlan"];
			}
			set
			{
				base["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000B28AB File Offset: 0x000B0AAB
		// (set) Token: 0x06003B02 RID: 15106 RVA: 0x000B28BD File Offset: 0x000B0ABD
		public string UMIPGateway
		{
			get
			{
				return (string)base["UMIPGateway"];
			}
			set
			{
				base["UMIPGateway"] = value;
			}
		}

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x000B28CB File Offset: 0x000B0ACB
		// (set) Token: 0x06003B04 RID: 15108 RVA: 0x000B28DD File Offset: 0x000B0ADD
		public Stream ClientStream
		{
			get
			{
				return (Stream)base["ClientStream"];
			}
			set
			{
				base["ClientStream"] = value;
			}
		}

		// Token: 0x04002742 RID: 10050
		public const string RbacParameters = "?Date&ClientStream";
	}
}
