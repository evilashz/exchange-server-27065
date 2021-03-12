using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000225 RID: 549
	[Serializable]
	internal class XsoManagerProperty : XsoStringProperty
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x000786BD File Offset: 0x000768BD
		public XsoManagerProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000786C8 File Offset: 0x000768C8
		public override string StringData
		{
			get
			{
				string text = base.XsoItem.TryGetProperty(base.PropertyDef) as string;
				if (text == null)
				{
					return null;
				}
				if (text.Trim().StartsWith("/o="))
				{
					string text2 = (string)XsoManagerProperty.GetObjectFromLegacyDN(text, ADRecipientSchema.DisplayName);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
					}
				}
				return text;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00078720 File Offset: 0x00076920
		private static object GetObjectFromLegacyDN(string legacyDN, PropertyDefinition propDef)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(Command.CurrentOrganizationId), 84, "GetObjectFromLegacyDN", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\SchemaConverter\\XSO\\XsoManagerProperty.cs");
			ADRecipient adrecipient = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(legacyDN);
			Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, tenantOrRootOrgRecipientSession.LastUsedDc);
			if (adrecipient == null)
			{
				return null;
			}
			return adrecipient[propDef];
		}
	}
}
