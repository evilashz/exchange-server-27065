using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000208 RID: 520
	[DataContract]
	public class MigrationCsvUploadParameters : WebServiceParameters
	{
		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000786EA File Offset: 0x000768EA
		public override string AssociatedCmdlet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000786F1 File Offset: 0x000768F1
		public override string RbacScope
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000786F8 File Offset: 0x000768F8
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x00078700 File Offset: 0x00076900
		[DataMember]
		public string MigrationBatchType { get; set; }

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00078709 File Offset: 0x00076909
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x00078711 File Offset: 0x00076911
		[DataMember]
		public string IsOffboarding { get; set; }

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0007871A File Offset: 0x0007691A
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00078722 File Offset: 0x00076922
		[DataMember]
		public bool AllowUnknownColumnsInCsv { get; set; }
	}
}
