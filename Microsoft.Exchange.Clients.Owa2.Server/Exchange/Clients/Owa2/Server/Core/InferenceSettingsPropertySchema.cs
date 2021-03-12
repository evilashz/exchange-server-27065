using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D9 RID: 217
	internal sealed class InferenceSettingsPropertySchema : UserConfigurationPropertySchemaBase
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001B25E File Offset: 0x0001945E
		private InferenceSettingsPropertySchema()
		{
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001B266 File Offset: 0x00019466
		internal static InferenceSettingsPropertySchema Instance
		{
			get
			{
				if (InferenceSettingsPropertySchema.instance == null)
				{
					InferenceSettingsPropertySchema.instance = new InferenceSettingsPropertySchema();
				}
				return InferenceSettingsPropertySchema.instance;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001B27E File Offset: 0x0001947E
		internal override UserConfigurationPropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return InferenceSettingsPropertySchema.propertyDefinitions;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001B285 File Offset: 0x00019485
		internal override UserConfigurationPropertyId PropertyDefinitionsBaseId
		{
			get
			{
				return UserConfigurationPropertyId.IsClutterUIEnabled;
			}
		}

		// Token: 0x040004FD RID: 1277
		private static readonly UserConfigurationPropertyDefinition[] propertyDefinitions = new UserConfigurationPropertyDefinition[]
		{
			new UserConfigurationPropertyDefinition("IsClutterUIEnabled", typeof(bool?), new UserConfigurationPropertyDefinition.Validate(UserConfigurationPropertyValidationUtility.ValidateIsClutterUIEnabledCallback))
		};

		// Token: 0x040004FE RID: 1278
		private static InferenceSettingsPropertySchema instance;
	}
}
