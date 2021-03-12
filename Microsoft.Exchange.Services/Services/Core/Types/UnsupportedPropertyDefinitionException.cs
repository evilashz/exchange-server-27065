using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B4 RID: 2228
	internal sealed class UnsupportedPropertyDefinitionException : ServicePermanentException
	{
		// Token: 0x06003F44 RID: 16196 RVA: 0x000DB2B9 File Offset: 0x000D94B9
		public UnsupportedPropertyDefinitionException(string propertyDefinition) : base(CoreResources.IDs.ErrorUnsupportedPropertyDefinition)
		{
			base.ConstantValues.Add("PropertyDefinition", propertyDefinition);
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000DB2DC File Offset: 0x000D94DC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x0400243F RID: 9279
		private const string PropertyDefinitionKey = "PropertyDefinition";
	}
}
