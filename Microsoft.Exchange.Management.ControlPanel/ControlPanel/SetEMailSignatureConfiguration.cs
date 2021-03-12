using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000091 RID: 145
	[DataContract]
	public class SetEMailSignatureConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x00057715 File Offset: 0x00055915
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x00057727 File Offset: 0x00055927
		[DataMember]
		public string SignatureHtml
		{
			get
			{
				return (string)base["SignatureHtml"];
			}
			set
			{
				base["SignatureHtml"] = value;
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00057735 File Offset: 0x00055935
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00057751 File Offset: 0x00055951
		[DataMember]
		public bool AutoAddSignature
		{
			get
			{
				return (bool)(base["AutoAddSignature"] ?? false);
			}
			set
			{
				base["AutoAddSignature"] = value;
			}
		}
	}
}
