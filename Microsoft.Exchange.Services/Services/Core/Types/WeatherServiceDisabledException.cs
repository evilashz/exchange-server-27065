using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C1 RID: 2241
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class WeatherServiceDisabledException : ServicePermanentException
	{
		// Token: 0x06003F72 RID: 16242 RVA: 0x000DB6D7 File Offset: 0x000D98D7
		public WeatherServiceDisabledException() : base(ResponseCodeType.ErrorWeatherServiceDisabled, (CoreResources.IDs)4210036349U)
		{
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000DB6EE File Offset: 0x000D98EE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Current;
			}
		}
	}
}
