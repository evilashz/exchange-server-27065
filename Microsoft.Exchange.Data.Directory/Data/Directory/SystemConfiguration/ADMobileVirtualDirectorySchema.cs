using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000353 RID: 851
	internal sealed class ADMobileVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000A5996 File Offset: 0x000A3B96
		private static MobileClientFlagsType DefaultMobileClientFlags
		{
			get
			{
				return MobileClientFlagsType.BadItemReportingEnabled | MobileClientFlagsType.SendWatsonReport;
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000A599C File Offset: 0x000A3B9C
		internal static object MobileClientCertProvisioningEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				return false;
			}
			MobileClientFlagsType mobileClientFlagsType = (MobileClientFlagsType)obj;
			return (mobileClientFlagsType & MobileClientFlagsType.ClientCertProvisionEnabled) == MobileClientFlagsType.ClientCertProvisionEnabled;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000A59D4 File Offset: 0x000A3BD4
		internal static void MobileClientCertProvisioningEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				obj = ADMobileVirtualDirectorySchema.DefaultMobileClientFlags;
			}
			if (flag)
			{
				propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj | MobileClientFlagsType.ClientCertProvisionEnabled);
				return;
			}
			propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj & ~MobileClientFlagsType.ClientCertProvisionEnabled);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000A5A38 File Offset: 0x000A3C38
		internal static void BadItemReportingEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				obj = ADMobileVirtualDirectorySchema.DefaultMobileClientFlags;
			}
			if (flag)
			{
				propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj | MobileClientFlagsType.BadItemReportingEnabled);
				return;
			}
			propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj & ~MobileClientFlagsType.BadItemReportingEnabled);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000A5A9C File Offset: 0x000A3C9C
		internal static object BadItemReportingEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				return true;
			}
			MobileClientFlagsType mobileClientFlagsType = (MobileClientFlagsType)obj;
			return (mobileClientFlagsType & MobileClientFlagsType.BadItemReportingEnabled) == MobileClientFlagsType.BadItemReportingEnabled;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000A5AD4 File Offset: 0x000A3CD4
		internal static void SendWatsonReportSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				obj = ADMobileVirtualDirectorySchema.DefaultMobileClientFlags;
			}
			if (flag)
			{
				propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj | MobileClientFlagsType.SendWatsonReport);
				return;
			}
			propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj & ~MobileClientFlagsType.SendWatsonReport);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000A5B38 File Offset: 0x000A3D38
		internal static object SendWatsonReportGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				return (ADMobileVirtualDirectorySchema.DefaultMobileClientFlags & MobileClientFlagsType.SendWatsonReport) == MobileClientFlagsType.SendWatsonReport;
			}
			MobileClientFlagsType mobileClientFlagsType = (MobileClientFlagsType)obj;
			return (mobileClientFlagsType & MobileClientFlagsType.SendWatsonReport) == MobileClientFlagsType.SendWatsonReport;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000A5B78 File Offset: 0x000A3D78
		internal static void RemoteDocumentsActionForUnknownServersSetter(object value, IPropertyBag propertyBag)
		{
			RemoteDocumentsActions remoteDocumentsActions = (RemoteDocumentsActions)value;
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				obj = ADMobileVirtualDirectorySchema.DefaultMobileClientFlags;
			}
			if (remoteDocumentsActions == RemoteDocumentsActions.Block)
			{
				propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj | MobileClientFlagsType.RemoteDocumentsActionForUnknownServers);
				return;
			}
			if (remoteDocumentsActions == RemoteDocumentsActions.Allow)
			{
				propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags] = ((MobileClientFlagsType)obj & ~MobileClientFlagsType.RemoteDocumentsActionForUnknownServers);
				return;
			}
			throw new ArgumentException("value can only be Allow or Block");
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000A5BEC File Offset: 0x000A3DEC
		internal static object RemoteDocumentsActionForUnknownServersGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[ADMobileVirtualDirectorySchema.MobileClientFlags];
			if (obj == null)
			{
				return RemoteDocumentsActions.Allow;
			}
			MobileClientFlagsType mobileClientFlagsType = (MobileClientFlagsType)obj;
			return ((mobileClientFlagsType & MobileClientFlagsType.RemoteDocumentsActionForUnknownServers) == MobileClientFlagsType.RemoteDocumentsActionForUnknownServers) ? RemoteDocumentsActions.Block : RemoteDocumentsActions.Allow;
		}

		// Token: 0x040017F2 RID: 6130
		public static readonly ADPropertyDefinition MobileClientFlags = new ADPropertyDefinition("MobileClientFlags", ExchangeObjectVersion.Exchange2007, typeof(MobileClientFlagsType), "msExchMobileClientFlags", ADPropertyDefinitionFlags.None, ADMobileVirtualDirectorySchema.DefaultMobileClientFlags, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017F3 RID: 6131
		public static readonly ADPropertyDefinition MobileClientCertificateAuthorityURL = new ADPropertyDefinition("MobileClientCertificateAuthorityURL", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileClientCertificateAuthorityURL", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017F4 RID: 6132
		public static readonly ADPropertyDefinition MobileClientCertTemplateName = new ADPropertyDefinition("MobileClientCertTemplateName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileClientCertTemplateName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017F5 RID: 6133
		public static readonly ADPropertyDefinition MobileClientCertificateProvisioningEnabled = new ADPropertyDefinition("MobileClientCertificateProvisioningEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.MobileClientFlags
		}, null, new GetterDelegate(ADMobileVirtualDirectorySchema.MobileClientCertProvisioningEnabledGetter), new SetterDelegate(ADMobileVirtualDirectorySchema.MobileClientCertProvisioningEnabledSetter), null, null);

		// Token: 0x040017F6 RID: 6134
		public static readonly ADPropertyDefinition BadItemReportingEnabled = new ADPropertyDefinition("BadItemReportingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.MobileClientFlags
		}, null, new GetterDelegate(ADMobileVirtualDirectorySchema.BadItemReportingEnabledGetter), new SetterDelegate(ADMobileVirtualDirectorySchema.BadItemReportingEnabledSetter), null, null);

		// Token: 0x040017F7 RID: 6135
		public static readonly ADPropertyDefinition SendWatsonReport = new ADPropertyDefinition("SendWatsonReport", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.MobileClientFlags
		}, null, new GetterDelegate(ADMobileVirtualDirectorySchema.SendWatsonReportGetter), new SetterDelegate(ADMobileVirtualDirectorySchema.SendWatsonReportSetter), null, null);

		// Token: 0x040017F8 RID: 6136
		public static readonly ADPropertyDefinition RemoteDocumentsActionForUnknownServers = new ADPropertyDefinition("RemoteDocumentsActionForUnknownServers", ExchangeObjectVersion.Exchange2007, typeof(RemoteDocumentsActions), null, ADPropertyDefinitionFlags.Calculated, RemoteDocumentsActions.Allow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.MobileClientFlags
		}, null, new GetterDelegate(ADMobileVirtualDirectorySchema.RemoteDocumentsActionForUnknownServersGetter), new SetterDelegate(ADMobileVirtualDirectorySchema.RemoteDocumentsActionForUnknownServersSetter), null, null);

		// Token: 0x040017F9 RID: 6137
		internal static readonly ADPropertyDefinition ADRemoteDocumentsAllowedServers = new ADPropertyDefinition("ADRemoteDocumentsAllowedServers", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileRemoteDocumentsAllowedServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017FA RID: 6138
		public static readonly ADPropertyDefinition RemoteDocumentsAllowedServers = new ADPropertyDefinition("RemoteDocumentsAllowedServers", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.ADRemoteDocumentsAllowedServers,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsAllowedServersGetter), new SetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsAllowedServersSetter), null, null);

		// Token: 0x040017FB RID: 6139
		internal static readonly ADPropertyDefinition ADRemoteDocumentsBlockedServers = new ADPropertyDefinition("ADRemoteDocumentsBlockedServers", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileRemoteDocumentsBlockedServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017FC RID: 6140
		public static readonly ADPropertyDefinition RemoteDocumentsBlockedServers = new ADPropertyDefinition("RemoteDocumentsBlockedServers", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		}, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.ADRemoteDocumentsBlockedServers,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsBlockedServersGetter), new SetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsBlockedServersSetter), null, null);

		// Token: 0x040017FD RID: 6141
		internal static readonly ADPropertyDefinition ADRemoteDocumentsInternalDomainSuffixList = new ADPropertyDefinition("ADRemoteDocumentsInternalDomainSuffixList", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMobileRemoteDocumentsInternalDomainSuffixList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017FE RID: 6142
		public static readonly ADPropertyDefinition RemoteDocumentsInternalDomainSuffixList = new ADPropertyDefinition("RemoteDocumentsInternalDomainSuffixList", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidDomainConstraint()
		}, new ProviderPropertyDefinition[]
		{
			ADMobileVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList,
			ADObjectSchema.Id
		}, null, new GetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsInternalDomainSuffixListGetter), new SetterDelegate(ADMobileVirtualDirectory.RemoteDocumentsInternalDomainSuffixListSetter), null, null);

		// Token: 0x040017FF RID: 6143
		public static readonly ADPropertyDefinition BasicAuthEnabled = new ADPropertyDefinition("BasicAuthEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001800 RID: 6144
		public static readonly ADPropertyDefinition WindowsAuthEnabled = new ADPropertyDefinition("WindowsAuthEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001801 RID: 6145
		public static readonly ADPropertyDefinition CompressionEnabled = new ADPropertyDefinition("CompressionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001802 RID: 6146
		public static readonly ADPropertyDefinition ClientCertAuth = new ADPropertyDefinition("ClientCertAuth", ExchangeObjectVersion.Exchange2007, typeof(ClientCertAuthTypes), null, ADPropertyDefinitionFlags.TaskPopulated, ClientCertAuthTypes.Ignore, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001803 RID: 6147
		public static readonly ADPropertyDefinition WebsiteName = new ADPropertyDefinition("WebsiteName", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001804 RID: 6148
		public static readonly ADPropertyDefinition WebSiteSSLEnabled = new ADPropertyDefinition("WebSiteSSLEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001805 RID: 6149
		public static readonly ADPropertyDefinition VirtualDirectoryName = new ADPropertyDefinition("VirtualDirectoryName", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
