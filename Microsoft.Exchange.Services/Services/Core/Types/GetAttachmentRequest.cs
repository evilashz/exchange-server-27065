using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000428 RID: 1064
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetAttachmentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAttachmentRequest : BaseRequest
	{
		// Token: 0x06001F27 RID: 7975 RVA: 0x000A0997 File Offset: 0x0009EB97
		public GetAttachmentRequest()
		{
			this.Initialize();
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000A09A5 File Offset: 0x0009EBA5
		private void Initialize()
		{
			this.responseShape = new AttachmentResponseShape();
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x000A09B2 File Offset: 0x0009EBB2
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x000A09BA File Offset: 0x0009EBBA
		[XmlElement(ElementName = "AttachmentShape")]
		[DataMember]
		public AttachmentResponseShape AttachmentShape
		{
			get
			{
				return this.responseShape;
			}
			set
			{
				if (value != null)
				{
					this.responseShape = value;
				}
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x000A09C6 File Offset: 0x0009EBC6
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x000A09CE File Offset: 0x0009EBCE
		[DataMember(Name = "ShapeName", IsRequired = false)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x000A09D7 File Offset: 0x0009EBD7
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x000A09DF File Offset: 0x0009EBDF
		[DataMember(Name = "AttachmentIds")]
		[XmlArray("AttachmentIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "AttachmentId", Type = typeof(AttachmentIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public AttachmentIdType[] AttachmentIds { get; set; }

		// Token: 0x06001F2F RID: 7983 RVA: 0x000A09E8 File Offset: 0x0009EBE8
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetAttachment(callContext, this);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000A09F1 File Offset: 0x0009EBF1
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.AttachmentIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForAttachmentIdList(callContext, this.AttachmentIds);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000A0A09 File Offset: 0x0009EC09
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.AttachmentIds == null || this.AttachmentIds.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForAttachmentId(false, callContext, this.AttachmentIds[taskStep]);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000A0A30 File Offset: 0x0009EC30
		protected override List<ServiceObjectId> GetAllIds()
		{
			if (this.AttachmentIds == null)
			{
				return null;
			}
			return new List<ServiceObjectId>(this.AttachmentIds);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000A0A47 File Offset: 0x0009EC47
		[OnDeserializing]
		public void Initialize(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x040013CE RID: 5070
		internal const string ElementName = "GetAttachment";

		// Token: 0x040013CF RID: 5071
		internal const string AttachmentIdsElementName = "AttachmentIds";

		// Token: 0x040013D0 RID: 5072
		private AttachmentResponseShape responseShape;
	}
}
