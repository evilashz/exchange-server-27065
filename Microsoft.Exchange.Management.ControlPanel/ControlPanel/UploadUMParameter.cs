using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AD RID: 1197
	[DataContract]
	public class UploadUMParameter : WebServiceParameters
	{
		// Token: 0x17002364 RID: 9060
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000B32B1 File Offset: 0x000B14B1
		public override string AssociatedCmdlet
		{
			get
			{
				return "Import-UMPrompt";
			}
		}

		// Token: 0x17002365 RID: 9061
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x000B32B8 File Offset: 0x000B14B8
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17002366 RID: 9062
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000B32BF File Offset: 0x000B14BF
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x000B32D1 File Offset: 0x000B14D1
		public Stream PromptFileStream
		{
			get
			{
				return (Stream)base["PromptFileStream"];
			}
			set
			{
				base["PromptFileStream"] = value;
			}
		}

		// Token: 0x17002367 RID: 9063
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000B32DF File Offset: 0x000B14DF
		// (set) Token: 0x06003B41 RID: 15169 RVA: 0x000B32F1 File Offset: 0x000B14F1
		[DataMember]
		public Identity UMDialPlan
		{
			get
			{
				return (Identity)base["UMDialPlan"];
			}
			set
			{
				base["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000B32FF File Offset: 0x000B14FF
		// (set) Token: 0x06003B43 RID: 15171 RVA: 0x000B3311 File Offset: 0x000B1511
		[DataMember]
		public Identity UMAutoAttendant
		{
			get
			{
				return (Identity)base["UMAutoAttendant"];
			}
			set
			{
				base["UMAutoAttendant"] = value;
			}
		}

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000B331F File Offset: 0x000B151F
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x000B3331 File Offset: 0x000B1531
		[DataMember]
		public string PromptFileName
		{
			get
			{
				return (string)base["PromptFileName"];
			}
			set
			{
				base["PromptFileName"] = value;
			}
		}
	}
}
