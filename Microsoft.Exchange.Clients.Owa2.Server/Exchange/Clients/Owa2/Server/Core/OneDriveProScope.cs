using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200040E RID: 1038
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OneDriveProScope : AttachmentDataProviderScope
	{
		// Token: 0x0600228A RID: 8842 RVA: 0x0007F02A File Offset: 0x0007D22A
		public OneDriveProScope(OneDriveProScopeType type, string displayName, string ariaLabel) : base(displayName, ariaLabel)
		{
			this.Type = type;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x0007F03B File Offset: 0x0007D23B
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x0007F043 File Offset: 0x0007D243
		[DataMember]
		[SimpleConfigurationProperty("type")]
		public OneDriveProScopeType Type { get; set; }
	}
}
