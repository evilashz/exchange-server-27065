using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200046B RID: 1131
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ReferenceAttachment")]
	[Serializable]
	public class ReferenceAttachmentType : AttachmentType
	{
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000A253B File Offset: 0x000A073B
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x000A2543 File Offset: 0x000A0743
		[DataMember(EmitDefaultValue = false)]
		public string AttachLongPathName { get; set; }

		// Token: 0x06002167 RID: 8551 RVA: 0x000A254C File Offset: 0x000A074C
		public static bool IsReferenceAttachmentSupported()
		{
			ExTraceGlobals.GetItemCallTracer.TraceDebug<ExchangeVersion, bool>(0L, "[ReferenceAttachmentType.IsReferenceAttachmentSupported] Exchange Version: {0}, RefAttachmentDisabled: {1}", ExchangeVersion.Current, EWSSettings.DisableReferenceAttachment);
			return ExchangeVersion.Current > ExchangeVersion.ExchangeV2_4 && !EWSSettings.DisableReferenceAttachment;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000A2584 File Offset: 0x000A0784
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x000A258C File Offset: 0x000A078C
		[DataMember(EmitDefaultValue = false)]
		public string ProviderType { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000A2595 File Offset: 0x000A0795
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x000A259D File Offset: 0x000A079D
		[DataMember(EmitDefaultValue = false)]
		public string ProviderEndpointUrl { get; set; }
	}
}
