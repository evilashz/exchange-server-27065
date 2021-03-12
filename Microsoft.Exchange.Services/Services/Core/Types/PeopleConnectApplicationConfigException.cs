using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083F RID: 2111
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleConnectApplicationConfigException : ServicePermanentException
	{
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000D6F6A File Offset: 0x000D516A
		public PeopleConnectApplicationConfigException(Exception innerException) : base((CoreResources.IDs)2869245557U, innerException)
		{
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000D6F7D File Offset: 0x000D517D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
