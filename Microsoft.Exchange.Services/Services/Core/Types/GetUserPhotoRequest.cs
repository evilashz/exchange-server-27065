using System;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200045A RID: 1114
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUserPhotoType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public sealed class GetUserPhotoRequest : BaseRequest
	{
		// Token: 0x060020C7 RID: 8391 RVA: 0x000A1E9B File Offset: 0x000A009B
		public GetUserPhotoRequest()
		{
			this.isPostRequest = true;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A1EAA File Offset: 0x000A00AA
		public GetUserPhotoRequest(WebOperationContext webContext, string email, ADObjectId adObjectId, UserPhotoSize size, bool isPreview) : this(new OutgoingWebResponseContextWrapper(webContext.OutgoingResponse), email, size, isPreview)
		{
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000A1EC2 File Offset: 0x000A00C2
		public GetUserPhotoRequest(WebOperationContext webContext, string email, UserPhotoSize size, bool isPreview) : this(webContext, email, null, size, isPreview)
		{
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000A1ED0 File Offset: 0x000A00D0
		public GetUserPhotoRequest(IOutgoingWebResponseContext outgoingResponse, string email, ADObjectId adObjectId, UserPhotoSize size, bool isPreview)
		{
			this.isPostRequest = false;
			this.OutgoingResponse = outgoingResponse;
			this.Email = email;
			this.AdObjectId = adObjectId;
			this.SizeRequested = size;
			this.isPreview = isPreview;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000A1F04 File Offset: 0x000A0104
		public GetUserPhotoRequest(IOutgoingWebResponseContext outgoingResponse, string email, UserPhotoSize size, bool isPreview) : this(outgoingResponse, email, null, size, isPreview)
		{
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000A1F12 File Offset: 0x000A0112
		public GetUserPhotoRequest(IOutgoingWebResponseContext outgoingResponse, string email, UserPhotoSize size, bool isPreview, bool fallbackToClearImage) : this(outgoingResponse, email, null, size, isPreview)
		{
			this.fallbackToClearImage = fallbackToClearImage;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000A1F28 File Offset: 0x000A0128
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x000A1F30 File Offset: 0x000A0130
		internal IOutgoingWebResponseContext OutgoingResponse { get; set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000A1F39 File Offset: 0x000A0139
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x000A1F41 File Offset: 0x000A0141
		internal string CacheId { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x000A1F4A File Offset: 0x000A014A
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x000A1F52 File Offset: 0x000A0152
		internal ADObjectId AdObjectId { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000A1F5B File Offset: 0x000A015B
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x000A1F63 File Offset: 0x000A0163
		[XmlElement]
		[DataMember(Name = "Email", IsRequired = true)]
		public string Email { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000A1F6C File Offset: 0x000A016C
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x000A1F74 File Offset: 0x000A0174
		[XmlElement]
		[DataMember(Name = "SizeRequested", IsRequired = true)]
		public UserPhotoSize SizeRequested { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000A1F7D File Offset: 0x000A017D
		internal bool IsPreview
		{
			get
			{
				return this.isPreview;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000A1F85 File Offset: 0x000A0185
		internal bool FallbackToClearImage
		{
			get
			{
				return this.fallbackToClearImage;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000A1F8D File Offset: 0x000A018D
		internal bool IsPostRequest
		{
			get
			{
				return this.isPostRequest;
			}
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000A1F95 File Offset: 0x000A0195
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserPhoto(callContext, this, NullTracer.Instance);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000A1FA3 File Offset: 0x000A01A3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000A1FA6 File Offset: 0x000A01A6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000A1FA9 File Offset: 0x000A01A9
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this.Email) || !SmtpAddress.IsValidSmtpAddress(this.Email))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSmtpAddressException(), FaultParty.Sender);
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A1FD7 File Offset: 0x000A01D7
		internal UserPhotoSize GetConvertedSizeRequested()
		{
			return ServicePhotoSizeToStoragePhotoSizeConverter.Convert(this.SizeRequested);
		}

		// Token: 0x04001461 RID: 5217
		private readonly bool isPostRequest;

		// Token: 0x04001462 RID: 5218
		private readonly bool isPreview;

		// Token: 0x04001463 RID: 5219
		private readonly bool fallbackToClearImage;
	}
}
