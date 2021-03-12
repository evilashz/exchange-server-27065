using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200060B RID: 1547
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class UMHuntGroup : ADConfigurationObject
	{
		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0010F76C File Offset: 0x0010D96C
		internal override ADObjectSchema Schema
		{
			get
			{
				return UMHuntGroup.schema;
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0010F773 File Offset: 0x0010D973
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UMHuntGroup.mostDerivedClass;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x0010F77A File Offset: 0x0010D97A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0010F781 File Offset: 0x0010D981
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x0010F793 File Offset: 0x0010D993
		[Parameter(Mandatory = false)]
		public string PilotIdentifier
		{
			get
			{
				return (string)this[UMHuntGroupSchema.PilotIdentifier];
			}
			set
			{
				this[UMHuntGroupSchema.PilotIdentifier] = value;
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x0010F7A1 File Offset: 0x0010D9A1
		// (set) Token: 0x0600494A RID: 18762 RVA: 0x0010F7B3 File Offset: 0x0010D9B3
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[UMHuntGroupSchema.UMDialPlan];
			}
			set
			{
				this[UMHuntGroupSchema.UMDialPlan] = value;
			}
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x0010F7C1 File Offset: 0x0010D9C1
		public ADObjectId UMIPGateway
		{
			get
			{
				return (ADObjectId)this[UMHuntGroupSchema.UMIPGateway];
			}
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0010F7D3 File Offset: 0x0010D9D3
		public override string ToString()
		{
			return this.PilotIdentifier + ":" + this.UMDialPlan.Name;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x0010F7F0 File Offset: 0x0010D9F0
		internal static object UMIPGatewayGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null && (ObjectState)propertyBag[ADObjectSchema.ObjectState] != ObjectState.New)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.IdIsNotSet, UMHuntGroupSchema.UMIPGateway, null));
			}
			if (adobjectId != null)
			{
				return adobjectId.Parent;
			}
			return null;
		}

		// Token: 0x040032C3 RID: 12995
		private static UMHuntGroupSchema schema = ObjectSchema.GetInstance<UMHuntGroupSchema>();

		// Token: 0x040032C4 RID: 12996
		private static string mostDerivedClass = "msExchUMHuntGroup";
	}
}
