using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200021C RID: 540
	internal class StaticExceptionMapping : ExceptionMappingBase
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x00045143 File Offset: 0x00043343
		public StaticExceptionMapping(Type exceptionType, ExceptionMappingBase.Attributes attributes, ExchangeVersion effectiveVersion, ResponseCodeType responseCode, CoreResources.IDs messageId) : base(exceptionType, attributes)
		{
			this.effectiveVersion = effectiveVersion;
			this.responseCode = responseCode;
			this.messageId = messageId;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00045164 File Offset: 0x00043364
		public StaticExceptionMapping(Type exceptionType, ExchangeVersion effectiveVersion, ResponseCodeType responseCode, CoreResources.IDs messageId) : this(exceptionType, ExceptionMappingBase.Attributes.None, effectiveVersion, responseCode, messageId)
		{
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00045172 File Offset: 0x00043372
		public StaticExceptionMapping(Type exceptionType, ExceptionMappingBase.Attributes attributes, ResponseCodeType responseCode, CoreResources.IDs messageId) : this(exceptionType, attributes, ExchangeVersion.Exchange2007, responseCode, messageId)
		{
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00045184 File Offset: 0x00043384
		public StaticExceptionMapping(Type exceptionType, ResponseCodeType responseCode, CoreResources.IDs messageId) : this(exceptionType, ExceptionMappingBase.Attributes.None, ExchangeVersion.Exchange2007, responseCode, messageId)
		{
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00045195 File Offset: 0x00043395
		public ResponseCodeType ResponseCode
		{
			get
			{
				return this.responseCode;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0004519D File Offset: 0x0004339D
		public LocalizedString MessageText
		{
			get
			{
				return this.GetLocalizedMessage(this.messageId);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000451AB File Offset: 0x000433AB
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			return this.responseCode;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000451B3 File Offset: 0x000433B3
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			return this.effectiveVersion;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000451BB File Offset: 0x000433BB
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			return this.messageId;
		}

		// Token: 0x04000AEA RID: 2794
		private ExchangeVersion effectiveVersion;

		// Token: 0x04000AEB RID: 2795
		private ResponseCodeType responseCode;

		// Token: 0x04000AEC RID: 2796
		private CoreResources.IDs messageId;
	}
}
