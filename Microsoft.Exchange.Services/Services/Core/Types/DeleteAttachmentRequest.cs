using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200040F RID: 1039
	[XmlType("DeleteAttachmentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteAttachmentRequest : BaseRequest
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0009F5AE File Offset: 0x0009D7AE
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0009F5B6 File Offset: 0x0009D7B6
		[XmlArrayItem("AttachmentId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public AttachmentIdType[] AttachmentIds { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0009F5BF File Offset: 0x0009D7BF
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0009F5C7 File Offset: 0x0009D7C7
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public bool ClientSupportsIrm { get; set; }

		// Token: 0x06001DAC RID: 7596 RVA: 0x0009F5D0 File Offset: 0x0009D7D0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DeleteAttachment(callContext, this);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0009F5D9 File Offset: 0x0009D7D9
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.AttachmentIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForAttachmentIdList(callContext, this.AttachmentIds);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0009F5F1 File Offset: 0x0009D7F1
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.AttachmentIds == null || this.AttachmentIds.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForAttachmentId(true, callContext, this.AttachmentIds[taskStep]);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0009F618 File Offset: 0x0009D818
		protected override List<ServiceObjectId> GetAllIds()
		{
			if (this.AttachmentIds == null)
			{
				return null;
			}
			return new List<ServiceObjectId>(this.AttachmentIds);
		}

		// Token: 0x04001341 RID: 4929
		internal const string ElementName = "DeleteAttachment";

		// Token: 0x04001342 RID: 4930
		internal const string AttachmentsElementName = "AttachmentIds";
	}
}
