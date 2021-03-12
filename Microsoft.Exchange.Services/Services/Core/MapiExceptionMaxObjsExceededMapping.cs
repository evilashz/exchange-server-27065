using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000221 RID: 545
	internal class MapiExceptionMaxObjsExceededMapping : ExceptionMappingBase
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x00045350 File Offset: 0x00043550
		public MapiExceptionMaxObjsExceededMapping() : base(typeof(MapiExceptionMaxObjsExceeded), ExceptionMappingBase.Attributes.None)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00045364 File Offset: 0x00043564
		protected override ResponseCodeType GetResponseCode(LocalizedException exception)
		{
			MapiExceptionMaxObjsExceeded mapiExceptionMaxObjsExceeded = exception as MapiExceptionMaxObjsExceeded;
			if (mapiExceptionMaxObjsExceeded != null && mapiExceptionMaxObjsExceeded.LowLevelError == 1252)
			{
				return ResponseCodeType.ErrorMessagePerFolderCountReceiveQuotaExceeded;
			}
			return ResponseCodeType.ErrorInternalServerError;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00045393 File Offset: 0x00043593
		protected override CoreResources.IDs GetResourceId(LocalizedException exception)
		{
			if (this.GetResponseCode(exception) == ResponseCodeType.ErrorMessagePerFolderCountReceiveQuotaExceeded)
			{
				return (CoreResources.IDs)2791864679U;
			}
			return CoreResources.IDs.ErrorInternalServerError;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000453AE File Offset: 0x000435AE
		protected override ExchangeVersion GetEffectiveVersion(LocalizedException exception)
		{
			if (this.GetResponseCode(exception) == ResponseCodeType.ErrorMessagePerFolderCountReceiveQuotaExceeded)
			{
				return ExchangeVersion.ExchangeV2_14;
			}
			return ExchangeVersion.Exchange2007;
		}
	}
}
