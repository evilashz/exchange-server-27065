using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	public sealed class ADMobileVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x000A61B4 File Offset: 0x000A43B4
		internal static ExchangeObjectVersion MinimumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000A61BB File Offset: 0x000A43BB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADMobileVirtualDirectory.schema;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x000A61C2 File Offset: 0x000A43C2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMobileVirtualDirectory";
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000A61C9 File Offset: 0x000A43C9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000A61DC File Offset: 0x000A43DC
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000A61E3 File Offset: 0x000A43E3
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x000A61F5 File Offset: 0x000A43F5
		public MobileClientFlagsType MobileClientFlags
		{
			get
			{
				return (MobileClientFlagsType)this[ADMobileVirtualDirectorySchema.MobileClientFlags];
			}
			internal set
			{
				this[ADMobileVirtualDirectorySchema.MobileClientFlags] = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000A6208 File Offset: 0x000A4408
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000A621A File Offset: 0x000A441A
		public bool MobileClientCertificateProvisioningEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.MobileClientCertificateProvisioningEnabled];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.MobileClientCertificateProvisioningEnabled] = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000A622D File Offset: 0x000A442D
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x000A623F File Offset: 0x000A443F
		public bool BadItemReportingEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.BadItemReportingEnabled];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.BadItemReportingEnabled] = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000A6252 File Offset: 0x000A4452
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x000A6264 File Offset: 0x000A4464
		public bool SendWatsonReport
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.SendWatsonReport];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.SendWatsonReport] = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000A6277 File Offset: 0x000A4477
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x000A6289 File Offset: 0x000A4489
		public string MobileClientCertificateAuthorityURL
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.MobileClientCertificateAuthorityURL];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.MobileClientCertificateAuthorityURL] = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x000A6297 File Offset: 0x000A4497
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x000A62A9 File Offset: 0x000A44A9
		public string MobileClientCertTemplateName
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.MobileClientCertTemplateName];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.MobileClientCertTemplateName] = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000A62B7 File Offset: 0x000A44B7
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x000A62D8 File Offset: 0x000A44D8
		public string ActiveSyncServer
		{
			get
			{
				if (!(base.ExternalUrl == null))
				{
					return base.ExternalUrl.ToString();
				}
				return string.Empty;
			}
			set
			{
				try
				{
					base.ExternalUrl = new Uri(value);
				}
				catch (UriFormatException ex)
				{
					PropertyValidationError error = new PropertyValidationError(new LocalizedString(ex.Message), ADVirtualDirectorySchema.ExternalUrl, value);
					throw new DataValidationException(error, ex);
				}
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000A6324 File Offset: 0x000A4524
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000A6336 File Offset: 0x000A4536
		public RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
		{
			get
			{
				return (RemoteDocumentsActions?)this[ADMobileVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers] = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000A6349 File Offset: 0x000A4549
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x000A635B File Offset: 0x000A455B
		public MultiValuedProperty<string> RemoteDocumentsAllowedServers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADMobileVirtualDirectorySchema.RemoteDocumentsAllowedServers];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.RemoteDocumentsAllowedServers] = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x000A6369 File Offset: 0x000A4569
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x000A637B File Offset: 0x000A457B
		public MultiValuedProperty<string> RemoteDocumentsBlockedServers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADMobileVirtualDirectorySchema.RemoteDocumentsBlockedServers];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.RemoteDocumentsBlockedServers] = value;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000A6389 File Offset: 0x000A4589
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x000A639B File Offset: 0x000A459B
		public MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADMobileVirtualDirectorySchema.RemoteDocumentsInternalDomainSuffixList];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.RemoteDocumentsInternalDomainSuffixList] = value;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x000A63A9 File Offset: 0x000A45A9
		public new string MetabasePath
		{
			get
			{
				return (string)this[ExchangeVirtualDirectorySchema.MetabasePath];
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000A63BB File Offset: 0x000A45BB
		internal static object RemoteDocumentsInternalDomainSuffixListGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList], ADMobileVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000A63D7 File Offset: 0x000A45D7
		internal static void RemoteDocumentsInternalDomainSuffixListSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADMobileVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList, propertyBag);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000A63F5 File Offset: 0x000A45F5
		internal static object RemoteDocumentsAllowedServersGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsAllowedServers], ADMobileVirtualDirectorySchema.ADRemoteDocumentsAllowedServers);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000A6411 File Offset: 0x000A4611
		internal static void RemoteDocumentsAllowedServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsAllowedServers] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADMobileVirtualDirectorySchema.ADRemoteDocumentsAllowedServers, propertyBag);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000A642F File Offset: 0x000A462F
		internal static object RemoteDocumentsBlockedServersGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsBlockedServers], ADMobileVirtualDirectorySchema.ADRemoteDocumentsBlockedServers);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000A644B File Offset: 0x000A464B
		internal static void RemoteDocumentsBlockedServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADMobileVirtualDirectorySchema.ADRemoteDocumentsBlockedServers] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADMobileVirtualDirectorySchema.ADRemoteDocumentsBlockedServers, propertyBag);
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000A6469 File Offset: 0x000A4669
		// (set) Token: 0x06002765 RID: 10085 RVA: 0x000A647B File Offset: 0x000A467B
		public bool BasicAuthEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.BasicAuthEnabled];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.BasicAuthEnabled] = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000A648E File Offset: 0x000A468E
		// (set) Token: 0x06002767 RID: 10087 RVA: 0x000A64A0 File Offset: 0x000A46A0
		public bool WindowsAuthEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.WindowsAuthEnabled];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.WindowsAuthEnabled] = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x000A64B3 File Offset: 0x000A46B3
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x000A64C5 File Offset: 0x000A46C5
		public bool CompressionEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.CompressionEnabled];
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.CompressionEnabled] = value;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x000A64D8 File Offset: 0x000A46D8
		// (set) Token: 0x0600276B RID: 10091 RVA: 0x000A64EF File Offset: 0x000A46EF
		public ClientCertAuthTypes? ClientCertAuth
		{
			get
			{
				return new ClientCertAuthTypes?((ClientCertAuthTypes)this[ADMobileVirtualDirectorySchema.ClientCertAuth]);
			}
			set
			{
				this[ADMobileVirtualDirectorySchema.ClientCertAuth] = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000A6502 File Offset: 0x000A4702
		// (set) Token: 0x0600276D RID: 10093 RVA: 0x000A6514 File Offset: 0x000A4714
		public string WebsiteName
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.WebsiteName];
			}
			internal set
			{
				this[ADMobileVirtualDirectorySchema.WebsiteName] = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000A6522 File Offset: 0x000A4722
		// (set) Token: 0x0600276F RID: 10095 RVA: 0x000A6534 File Offset: 0x000A4734
		public bool WebSiteSSLEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.WebSiteSSLEnabled];
			}
			internal set
			{
				this[ADMobileVirtualDirectorySchema.WebSiteSSLEnabled] = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000A6547 File Offset: 0x000A4747
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x000A6559 File Offset: 0x000A4759
		public string VirtualDirectoryName
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.VirtualDirectoryName];
			}
			internal set
			{
				this[ADMobileVirtualDirectorySchema.VirtualDirectoryName] = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000A6567 File Offset: 0x000A4767
		internal ExchangeVirtualDirectory ProxyVirtualDirectoryObject
		{
			get
			{
				return this.proxyVirtualDirectoryObject;
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000A6570 File Offset: 0x000A4770
		internal void InitProxyVDirDataObject()
		{
			if (this.proxyVirtualDirectoryObject == null)
			{
				this.proxyVirtualDirectoryObject = new ExchangeVirtualDirectory();
				this.proxyVirtualDirectoryObject.SetExchangeVersion(base.ExchangeVersion);
				this.proxyVirtualDirectoryObject.MetabasePath = string.Format("{0}/{1}", this.MetabasePath, "Proxy");
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000A65C4 File Offset: 0x000A47C4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.MetabasePath.Length != 0)
			{
				if (this.MetabasePath.ToUpper().IndexOf("IIS://") != 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.MobileMetabasePathIsInvalid(base.Id.Name, this.MetabasePath), ExchangeVirtualDirectorySchema.MetabasePath, this.MetabasePath));
				}
				if (!base.ADPropertiesOnly)
				{
					bool flag;
					try
					{
						flag = DirectoryEntry.Exists(this.MetabasePath);
					}
					catch (COMException)
					{
						flag = false;
					}
					if (!flag)
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.MobileAdOrphanFound(base.Id.Name), ExchangeVirtualDirectorySchema.MetabasePath, this.MetabasePath));
					}
				}
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000A6680 File Offset: 0x000A4880
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADMobileVirtualDirectorySchema.ClientCertAuth))
			{
				this[ADMobileVirtualDirectorySchema.ClientCertAuth] = ClientCertAuthTypes.Ignore;
			}
			if (!base.IsModified(ADMobileVirtualDirectorySchema.WebsiteName))
			{
				this[ADMobileVirtualDirectorySchema.WebsiteName] = "Default Web Site";
			}
			if (!base.IsModified(ADMobileVirtualDirectorySchema.VirtualDirectoryName))
			{
				this[ADMobileVirtualDirectorySchema.VirtualDirectoryName] = "Microsoft-Server-ActiveSync";
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04001806 RID: 6150
		public const string IisProxySubDir = "Proxy";

		// Token: 0x04001807 RID: 6151
		public const string MostDerivedClass = "msExchMobileVirtualDirectory";

		// Token: 0x04001808 RID: 6152
		private static readonly ADMobileVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADMobileVirtualDirectorySchema>();

		// Token: 0x04001809 RID: 6153
		private ExchangeVirtualDirectory proxyVirtualDirectoryObject;
	}
}
