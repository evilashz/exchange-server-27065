using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000202 RID: 514
	[DataContract]
	public class FingerprintUploadParameters : WebServiceParameters
	{
		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x0007817C File Offset: 0x0007637C
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-Fingerprint";
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x00078183 File Offset: 0x00076383
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0007818A File Offset: 0x0007638A
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x0007819C File Offset: 0x0007639C
		public byte[] FileData
		{
			get
			{
				return (byte[])base["FileData"];
			}
			set
			{
				base["FileData"] = value;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000781AA File Offset: 0x000763AA
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x000781BC File Offset: 0x000763BC
		public string Description
		{
			get
			{
				return (string)base["Description"];
			}
			set
			{
				base["Description"] = value;
			}
		}
	}
}
