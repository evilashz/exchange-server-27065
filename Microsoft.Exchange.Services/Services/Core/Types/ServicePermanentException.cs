using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000587 RID: 1415
	internal abstract class ServicePermanentException : LocalizedException
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x000A7288 File Offset: 0x000A5488
		public ServicePermanentException(ResponseCodeType responseCode, LocalizedString message) : base(message)
		{
			this.responseCode = responseCode;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000A72A3 File Offset: 0x000A54A3
		public ServicePermanentException(ResponseCodeType responseCode, Enum messageId) : base(CoreResources.GetLocalizedString((CoreResources.IDs)messageId))
		{
			this.responseCode = responseCode;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000A72C8 File Offset: 0x000A54C8
		public ServicePermanentException(ResponseCodeType responseCode, Enum messageId, Exception innerException) : base(CoreResources.GetLocalizedString((CoreResources.IDs)messageId), innerException)
		{
			this.responseCode = responseCode;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000A72EE File Offset: 0x000A54EE
		public ServicePermanentException(Enum messageId) : base(CoreResources.GetLocalizedString((CoreResources.IDs)messageId))
		{
			this.responseCode = (ResponseCodeType)Enum.Parse(typeof(ResponseCodeType), messageId.ToString(), true);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000A732D File Offset: 0x000A552D
		public ServicePermanentException(Enum messageId, Exception innerException) : base(CoreResources.GetLocalizedString((CoreResources.IDs)messageId), innerException)
		{
			this.responseCode = (ResponseCodeType)Enum.Parse(typeof(ResponseCodeType), messageId.ToString(), true);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000A736D File Offset: 0x000A556D
		public ServicePermanentException(Enum messageId, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.responseCode = (ResponseCodeType)Enum.Parse(typeof(ResponseCodeType), messageId.ToString(), true);
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x000A73A3 File Offset: 0x000A55A3
		public ResponseCodeType ResponseCode
		{
			get
			{
				return this.responseCode;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x000A73AB File Offset: 0x000A55AB
		public IDictionary<string, string> ConstantValues
		{
			get
			{
				return this.constantValues;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600275A RID: 10074
		internal abstract ExchangeVersion EffectiveVersion { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000A73B3 File Offset: 0x000A55B3
		internal virtual bool StopsBatchProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000A73B8 File Offset: 0x000A55B8
		internal virtual ServiceError CreateServiceError(ExchangeVersion requestVersion)
		{
			IProvidePropertyPaths providePropertyPaths = this as IProvidePropertyPaths;
			IProvideXmlNodeArray provideXmlNodeArray = this as IProvideXmlNodeArray;
			return new ServiceError(base.LocalizedString, this.ResponseCode, 0, requestVersion, this.EffectiveVersion, this.StopsBatchProcessing, (providePropertyPaths != null) ? providePropertyPaths.PropertyPaths : null, this.ConstantValues, (provideXmlNodeArray != null) ? provideXmlNodeArray.NodeArray : null);
		}

		// Token: 0x040018E5 RID: 6373
		private readonly ResponseCodeType responseCode;

		// Token: 0x040018E6 RID: 6374
		private Dictionary<string, string> constantValues = new Dictionary<string, string>();
	}
}
