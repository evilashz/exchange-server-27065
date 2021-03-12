using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000565 RID: 1381
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ResourceBookingConfig : ADConfigurationObject
	{
		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000EBBC1 File Offset: 0x000E9DC1
		internal override ADObjectSchema Schema
		{
			get
			{
				return ResourceBookingConfig.schema;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000EBBC8 File Offset: 0x000E9DC8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ResourceBookingConfig.mostDerivedClass;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000EBBCF File Offset: 0x000E9DCF
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x000EBBD2 File Offset: 0x000E9DD2
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000EBBDA File Offset: 0x000E9DDA
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000EBBEC File Offset: 0x000E9DEC
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourcePropertySchema
		{
			get
			{
				return (MultiValuedProperty<string>)this[ResourceBookingConfigSchema.ResourcePropertySchema];
			}
			set
			{
				this[ResourceBookingConfigSchema.ResourcePropertySchema] = value;
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000EBBFC File Offset: 0x000E9DFC
		internal static ADObjectId GetWellKnownLocation(ADObjectId orgContainerId)
		{
			ADObjectId relativePath = new ADObjectId("CN=Resource Schema");
			return ResourceBookingConfig.GetWellKnownParentLocation(orgContainerId).GetDescendantId(relativePath);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000EBC20 File Offset: 0x000E9E20
		internal static ADObjectId GetWellKnownParentLocation(ADObjectId orgContainerId)
		{
			ADObjectId relativePath = new ADObjectId("CN=Global Settings");
			return orgContainerId.GetDescendantId(relativePath);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000EBC40 File Offset: 0x000E9E40
		internal static MultiValuedProperty<string> GetAllowedProperties(IConfigurationSession configSession, ExchangeResourceType? resourceType)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (resourceType != null)
			{
				string text = resourceType.ToString() + '/';
				ResourceBookingConfig resourceBookingConfig = configSession.Read<ResourceBookingConfig>(ResourceBookingConfig.GetWellKnownLocation(configSession.GetOrgContainerId()));
				foreach (string text2 in resourceBookingConfig.ResourcePropertySchema)
				{
					if (string.Compare(text, 0, text2, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						multiValuedProperty.Add(text2.Substring(text.Length));
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000EBCF0 File Offset: 0x000E9EF0
		internal bool IsPropAllowedOnResourceType(string resourceType, string resourceProperty)
		{
			string strB = resourceType + '/' + resourceProperty;
			foreach (string strA in this.ResourcePropertySchema)
			{
				if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002A0C RID: 10764
		public const char LocationDelimiter = '/';

		// Token: 0x04002A0D RID: 10765
		private static ResourceBookingConfigSchema schema = ObjectSchema.GetInstance<ResourceBookingConfigSchema>();

		// Token: 0x04002A0E RID: 10766
		private static string mostDerivedClass = "msExchResourceSchema";
	}
}
