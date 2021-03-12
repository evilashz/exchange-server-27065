using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000368 RID: 872
	[Serializable]
	public sealed class ADOwaVirtualDirectory : ExchangeWebAppVirtualDirectory
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000AB0E0 File Offset: 0x000A92E0
		internal static ExchangeObjectVersion MinimumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000AB0E7 File Offset: 0x000A92E7
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADOwaVirtualDirectory.schema;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x000AB0EE File Offset: 0x000A92EE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADOwaVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000AB0F5 File Offset: 0x000A92F5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x000AB0FC File Offset: 0x000A92FC
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000AB10F File Offset: 0x000A930F
		private static ITopologyConfigurationSession ConfigurationSession
		{
			get
			{
				if (ADOwaVirtualDirectory.configurationSession == null)
				{
					ADOwaVirtualDirectory.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1944, "ConfigurationSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADOWAVirtualDirectory.cs");
				}
				return ADOwaVirtualDirectory.configurationSession;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000AB144 File Offset: 0x000A9344
		private bool IsOnExchange2007RTM
		{
			get
			{
				if (this.isOnExchange2007RTM == null)
				{
					Server server = ADOwaVirtualDirectory.ConfigurationSession.FindServerByName(base.Server.Name);
					int versionNumber = server.VersionNumber;
					int num = versionNumber >> 16 & 63;
					int num2 = versionNumber >> 22 & 63;
					if (num2 == Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.Exchange2007MajorVersion && num == 0)
					{
						this.isOnExchange2007RTM = new bool?(true);
					}
					else
					{
						this.isOnExchange2007RTM = new bool?(false);
					}
				}
				return this.isOnExchange2007RTM.Value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000AB1BB File Offset: 0x000A93BB
		internal bool IsExchange2007OrLater
		{
			get
			{
				return this.OwaVersion >= OwaVersions.Exchange2007;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000AB1C9 File Offset: 0x000A93C9
		internal bool IsExchange2009OrLater
		{
			get
			{
				return this.OwaVersion >= OwaVersions.Exchange2010;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000AB1D7 File Offset: 0x000A93D7
		internal bool IsExchange2013OrLater
		{
			get
			{
				return this.OwaVersion >= OwaVersions.Exchange2013;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x000AB1E8 File Offset: 0x000A93E8
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x000AB21C File Offset: 0x000A941C
		[Parameter]
		public bool? DirectFileAccessOnPublicComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.DirectFileAccessOnPublicComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DirectFileAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000AB230 File Offset: 0x000A9430
		// (set) Token: 0x06002848 RID: 10312 RVA: 0x000AB264 File Offset: 0x000A9464
		[Parameter]
		public bool? DirectFileAccessOnPrivateComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.DirectFileAccessOnPrivateComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DirectFileAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x000AB278 File Offset: 0x000A9478
		// (set) Token: 0x0600284A RID: 10314 RVA: 0x000AB2AC File Offset: 0x000A94AC
		[Parameter]
		public bool? WebReadyDocumentViewingOnPublicComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingOnPublicComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x000AB2C0 File Offset: 0x000A94C0
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x000AB2F4 File Offset: 0x000A94F4
		[Parameter]
		public bool? WebReadyDocumentViewingOnPrivateComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingOnPrivateComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000AB308 File Offset: 0x000A9508
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x000AB33C File Offset: 0x000A953C
		[Parameter]
		public bool? ForceWebReadyDocumentViewingFirstOnPublicComputers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ForceWebReadyDocumentViewingFirstOnPublicComputers]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceWebReadyDocumentViewingFirstOnPublicComputers] = value;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000AB350 File Offset: 0x000A9550
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000AB384 File Offset: 0x000A9584
		[Parameter]
		public bool? ForceWebReadyDocumentViewingFirstOnPrivateComputers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ForceWebReadyDocumentViewingFirstOnPrivateComputers]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceWebReadyDocumentViewingFirstOnPrivateComputers] = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000AB398 File Offset: 0x000A9598
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x000AB3CC File Offset: 0x000A95CC
		[Parameter]
		public bool? WacViewingOnPublicComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WacViewingOnPublicComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WacViewingOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000AB3E0 File Offset: 0x000A95E0
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x000AB414 File Offset: 0x000A9614
		[Parameter]
		public bool? WacViewingOnPrivateComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WacViewingOnPrivateComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WacViewingOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000AB428 File Offset: 0x000A9628
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x000AB45C File Offset: 0x000A965C
		[Parameter]
		public bool? ForceWacViewingFirstOnPublicComputers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ForceWacViewingFirstOnPublicComputers]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceWacViewingFirstOnPublicComputers] = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000AB470 File Offset: 0x000A9670
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x000AB4A4 File Offset: 0x000A96A4
		[Parameter]
		public bool? ForceWacViewingFirstOnPrivateComputers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ForceWacViewingFirstOnPrivateComputers]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceWacViewingFirstOnPrivateComputers] = value;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000AB4B8 File Offset: 0x000A96B8
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000AB4EC File Offset: 0x000A96EC
		[Parameter]
		public RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new RemoteDocumentsActions?((RemoteDocumentsActions)this[ADOwaVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers] = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000AB500 File Offset: 0x000A9700
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000AB534 File Offset: 0x000A9734
		[Parameter]
		public AttachmentBlockingActions? ActionForUnknownFileAndMIMETypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new AttachmentBlockingActions?((AttachmentBlockingActions)this[ADOwaVirtualDirectorySchema.ActionForUnknownFileAndMIMETypes]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ActionForUnknownFileAndMIMETypes] = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000AB547 File Offset: 0x000A9747
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x000AB563 File Offset: 0x000A9763
		[Parameter]
		public MultiValuedProperty<string> WebReadyFileTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.WebReadyFileTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebReadyFileTypes] = value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x000AB571 File Offset: 0x000A9771
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x000AB58D File Offset: 0x000A978D
		[Parameter]
		public MultiValuedProperty<string> WebReadyMimeTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.WebReadyMimeTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebReadyMimeTypes] = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000AB59C File Offset: 0x000A979C
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x000AB5D0 File Offset: 0x000A97D0
		[Parameter]
		public bool? WebReadyDocumentViewingForAllSupportedTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingForAllSupportedTypes]);
			}
			set
			{
				if (value != null)
				{
					this[ADOwaVirtualDirectorySchema.WebReadyDocumentViewingForAllSupportedTypes] = value;
				}
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x000AB5EC File Offset: 0x000A97EC
		[Parameter]
		public MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				if (!this.IsOnExchange2007RTM)
				{
					return ADOwaVirtualDirectory.webReadyDocumentViewingSupportedMimeTypes;
				}
				return ADOwaVirtualDirectory.exchange2007RTMWebReadyDocumentViewingSupportedMimeTypes;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x000AB60B File Offset: 0x000A980B
		[Parameter]
		public MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				if (!this.IsOnExchange2007RTM)
				{
					return ADOwaVirtualDirectory.webReadyDocumentViewingSupportedFileTypes;
				}
				return ADOwaVirtualDirectory.exchange2007RTMWebReadyDocumentViewingSupportedFileTypes;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000AB62A File Offset: 0x000A982A
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x000AB646 File Offset: 0x000A9846
		[Parameter]
		public MultiValuedProperty<string> AllowedFileTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.AllowedFileTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AllowedFileTypes] = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x000AB654 File Offset: 0x000A9854
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x000AB670 File Offset: 0x000A9870
		[Parameter]
		public MultiValuedProperty<string> AllowedMimeTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.AllowedMimeTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AllowedMimeTypes] = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000AB67E File Offset: 0x000A987E
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x000AB69A File Offset: 0x000A989A
		[Parameter]
		public MultiValuedProperty<string> ForceSaveFileTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.ForceSaveFileTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceSaveFileTypes] = value;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000AB6A8 File Offset: 0x000A98A8
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x000AB6C4 File Offset: 0x000A98C4
		[Parameter]
		public MultiValuedProperty<string> ForceSaveMimeTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.ForceSaveMimeTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceSaveMimeTypes] = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000AB6D2 File Offset: 0x000A98D2
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x000AB6EE File Offset: 0x000A98EE
		[Parameter]
		public MultiValuedProperty<string> BlockedFileTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.BlockedFileTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.BlockedFileTypes] = value;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000AB6FC File Offset: 0x000A98FC
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x000AB718 File Offset: 0x000A9918
		[Parameter]
		public MultiValuedProperty<string> BlockedMimeTypes
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.BlockedMimeTypes];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.BlockedMimeTypes] = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000AB726 File Offset: 0x000A9926
		// (set) Token: 0x06002872 RID: 10354 RVA: 0x000AB742 File Offset: 0x000A9942
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsAllowedServers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.RemoteDocumentsAllowedServers];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RemoteDocumentsAllowedServers] = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000AB750 File Offset: 0x000A9950
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x000AB76C File Offset: 0x000A996C
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsBlockedServers
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.RemoteDocumentsBlockedServers];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RemoteDocumentsBlockedServers] = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000AB77A File Offset: 0x000A997A
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000AB796 File Offset: 0x000A9996
		[Parameter]
		public MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.RemoteDocumentsInternalDomainSuffixList];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RemoteDocumentsInternalDomainSuffixList] = value;
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000AB7A4 File Offset: 0x000A99A4
		internal static object WebReadyFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADWebReadyFileTypes], ADOwaVirtualDirectorySchema.ADWebReadyFileTypes);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000AB7C0 File Offset: 0x000A99C0
		internal static void WebReadyFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADWebReadyFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADWebReadyFileTypes, propertyBag);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000AB7DE File Offset: 0x000A99DE
		internal static object WebReadyMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes], ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000AB7FA File Offset: 0x000A99FA
		internal static void WebReadyMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes, propertyBag);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000AB818 File Offset: 0x000A9A18
		internal static object AllowedFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADAllowedFileTypes], ADOwaVirtualDirectorySchema.ADAllowedFileTypes);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000AB834 File Offset: 0x000A9A34
		internal static void AllowedFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADAllowedFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADAllowedFileTypes, propertyBag);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000AB852 File Offset: 0x000A9A52
		internal static object AllowedMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADAllowedMimeTypes], ADOwaVirtualDirectorySchema.ADAllowedMimeTypes);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000AB86E File Offset: 0x000A9A6E
		internal static void AllowedMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADAllowedMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADAllowedMimeTypes, propertyBag);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000AB88C File Offset: 0x000A9A8C
		internal static object ForceSaveFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADForceSaveFileTypes], ADOwaVirtualDirectorySchema.ADForceSaveFileTypes);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000AB8A8 File Offset: 0x000A9AA8
		internal static void ForceSaveFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADForceSaveFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADForceSaveFileTypes, propertyBag);
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000AB8C6 File Offset: 0x000A9AC6
		internal static object ForceSaveMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes], ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000AB8E2 File Offset: 0x000A9AE2
		internal static void ForceSaveMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes, propertyBag);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000AB900 File Offset: 0x000A9B00
		internal static object BlockedFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADBlockedFileTypes], ADOwaVirtualDirectorySchema.ADBlockedFileTypes);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000AB91C File Offset: 0x000A9B1C
		internal static void BlockedFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADBlockedFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADBlockedFileTypes, propertyBag);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000AB93A File Offset: 0x000A9B3A
		internal static object BlockedMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADBlockedMimeTypes], ADOwaVirtualDirectorySchema.ADBlockedMimeTypes);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000AB956 File Offset: 0x000A9B56
		internal static void BlockedMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADBlockedMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADBlockedMimeTypes, propertyBag);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000AB974 File Offset: 0x000A9B74
		internal static object RemoteDocumentsAllowedServersGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers], ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000AB990 File Offset: 0x000A9B90
		internal static void RemoteDocumentsAllowedServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers, propertyBag);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000AB9AE File Offset: 0x000A9BAE
		internal static object RemoteDocumentsBlockedServersGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers], ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000AB9CA File Offset: 0x000A9BCA
		internal static void RemoteDocumentsBlockedServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers, propertyBag);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000AB9E8 File Offset: 0x000A9BE8
		internal static object RemoteDocumentsInternalDomainSuffixListGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList], ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000ABA04 File Offset: 0x000A9C04
		internal static void RemoteDocumentsInternalDomainSuffixListSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList, propertyBag);
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000ABA22 File Offset: 0x000A9C22
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000ABA3E File Offset: 0x000A9C3E
		public string FolderPathname
		{
			get
			{
				if (this.IsExchange2007OrLater)
				{
					return null;
				}
				return (string)this[ADOwaVirtualDirectorySchema.FolderPathname];
			}
			internal set
			{
				this[ADOwaVirtualDirectorySchema.FolderPathname] = value;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000ABA4C File Offset: 0x000A9C4C
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x000ABA68 File Offset: 0x000A9C68
		public MultiValuedProperty<string> Url
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (MultiValuedProperty<string>)this[ADOwaVirtualDirectorySchema.Url];
			}
			internal set
			{
				this[ADOwaVirtualDirectorySchema.Url] = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000ABA76 File Offset: 0x000A9C76
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000ABA88 File Offset: 0x000A9C88
		[Parameter]
		public LogonFormats LogonFormat
		{
			get
			{
				return (LogonFormats)this[ADOwaVirtualDirectorySchema.LogonFormat];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.LogonFormat] = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000ABA9B File Offset: 0x000A9C9B
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x000ABAAD File Offset: 0x000A9CAD
		[Parameter]
		public ClientAuthCleanupLevels ClientAuthCleanupLevel
		{
			get
			{
				return (ClientAuthCleanupLevels)this[ADOwaVirtualDirectorySchema.ClientAuthCleanupLevel];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ClientAuthCleanupLevel] = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000ABAC0 File Offset: 0x000A9CC0
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x000ABAF4 File Offset: 0x000A9CF4
		[Parameter]
		public bool? LogonPagePublicPrivateSelectionEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.LogonPagePublicPrivateSelectionEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.LogonPagePublicPrivateSelectionEnabled] = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000ABB08 File Offset: 0x000A9D08
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x000ABB3C File Offset: 0x000A9D3C
		[Parameter]
		public bool? LogonPageLightSelectionEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.LogonPageLightSelectionEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.LogonPageLightSelectionEnabled] = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000ABB50 File Offset: 0x000A9D50
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x000ABB84 File Offset: 0x000A9D84
		[Parameter]
		public bool? IsPublic
		{
			get
			{
				if (!this.IsExchange2013OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.IsPublic]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.IsPublic] = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000ABB98 File Offset: 0x000A9D98
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x000ABBCC File Offset: 0x000A9DCC
		[Parameter]
		public WebBeaconFilterLevels? FilterWebBeaconsAndHtmlForms
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new WebBeaconFilterLevels?((WebBeaconFilterLevels)this[ADOwaVirtualDirectorySchema.FilterWebBeaconsAndHtmlForms]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.FilterWebBeaconsAndHtmlForms] = value;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000ABBE0 File Offset: 0x000A9DE0
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x000ABC0F File Offset: 0x000A9E0F
		[Parameter]
		public int? NotificationInterval
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (int?)this[ADOwaVirtualDirectorySchema.NotificationInterval];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.NotificationInterval] = value;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x000ABC22 File Offset: 0x000A9E22
		// (set) Token: 0x060028A0 RID: 10400 RVA: 0x000ABC3E File Offset: 0x000A9E3E
		[Parameter]
		public string DefaultTheme
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (string)this[ADOwaVirtualDirectorySchema.DefaultTheme];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DefaultTheme] = value;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x000ABC4C File Offset: 0x000A9E4C
		// (set) Token: 0x060028A2 RID: 10402 RVA: 0x000ABC7B File Offset: 0x000A9E7B
		[Parameter]
		public int? UserContextTimeout
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (int?)this[ADOwaVirtualDirectorySchema.UserContextTimeout];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.UserContextTimeout] = value;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000ABC90 File Offset: 0x000A9E90
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x000ABCC4 File Offset: 0x000A9EC4
		[Parameter]
		public ExchwebProxyDestinations? ExchwebProxyDestination
		{
			get
			{
				if (this.IsExchange2007OrLater)
				{
					return null;
				}
				return new ExchwebProxyDestinations?((ExchwebProxyDestinations)this[ADOwaVirtualDirectorySchema.ExchwebProxyDestination]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ExchwebProxyDestination] = value;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000ABCD8 File Offset: 0x000A9ED8
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000ABD0C File Offset: 0x000A9F0C
		[Parameter]
		public VirtualDirectoryTypes? VirtualDirectoryType
		{
			get
			{
				if (this.IsExchange2007OrLater)
				{
					return null;
				}
				return new VirtualDirectoryTypes?((VirtualDirectoryTypes)this[ADOwaVirtualDirectorySchema.VirtualDirectoryType]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.VirtualDirectoryType] = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000ABD20 File Offset: 0x000A9F20
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x000ABD59 File Offset: 0x000A9F59
		public OwaVersions OwaVersion
		{
			get
			{
				OwaVersions owaVersions = (OwaVersions)this[ADOwaVirtualDirectorySchema.OwaVersion];
				if (base.ExchangeVersion.ExchangeBuild.Major < 14 && owaVersions >= OwaVersions.Exchange2007)
				{
					return OwaVersions.Exchange2007;
				}
				return owaVersions;
			}
			internal set
			{
				this[ADOwaVirtualDirectorySchema.OwaVersion] = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000ABD6C File Offset: 0x000A9F6C
		public string ServerName
		{
			get
			{
				if (base.Server != null)
				{
					return base.Server.Name;
				}
				return null;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000ABD83 File Offset: 0x000A9F83
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000ABD95 File Offset: 0x000A9F95
		[Parameter]
		public string InstantMessagingCertificateThumbprint
		{
			get
			{
				return (string)this[ADOwaVirtualDirectorySchema.InstantMessagingCertificateThumbprint];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.InstantMessagingCertificateThumbprint] = value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000ABDA3 File Offset: 0x000A9FA3
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000ABDB5 File Offset: 0x000A9FB5
		[Parameter]
		public string InstantMessagingServerName
		{
			get
			{
				return (string)this[ADOwaVirtualDirectorySchema.InstantMessagingServerName];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.InstantMessagingServerName] = value;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000ABDC4 File Offset: 0x000A9FC4
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000ABDFB File Offset: 0x000A9FFB
		[Parameter]
		public bool? RedirectToOptimalOWAServer
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((RedirectToOptimalOWAServerOptions)this[ADOwaVirtualDirectorySchema.RedirectToOptimalOWAServer] == RedirectToOptimalOWAServerOptions.Enabled);
			}
			set
			{
				if (value != null)
				{
					this[ADOwaVirtualDirectorySchema.RedirectToOptimalOWAServer] = (value.Value ? RedirectToOptimalOWAServerOptions.Enabled : RedirectToOptimalOWAServerOptions.Disabled);
				}
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000ABE24 File Offset: 0x000AA024
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000ABE53 File Offset: 0x000AA053
		[Parameter]
		public int? DefaultClientLanguage
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return (int?)this[ADOwaVirtualDirectorySchema.DefaultClientLanguage];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DefaultClientLanguage] = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000ABE66 File Offset: 0x000AA066
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000ABE82 File Offset: 0x000AA082
		[Parameter]
		public int LogonAndErrorLanguage
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return 0;
				}
				return (int)this[ADOwaVirtualDirectorySchema.LogonAndErrorLanguage];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.LogonAndErrorLanguage] = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000ABE98 File Offset: 0x000AA098
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000ABEE1 File Offset: 0x000AA0E1
		[Parameter]
		public bool? UseGB18030
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((int?)this[ADOwaVirtualDirectorySchema.UseGB18030] == 1);
			}
			set
			{
				if (value != null)
				{
					this[ADOwaVirtualDirectorySchema.UseGB18030] = (value.Value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000ABF0C File Offset: 0x000AA10C
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x000ABF55 File Offset: 0x000AA155
		[Parameter]
		public bool? UseISO885915
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((int?)this[ADOwaVirtualDirectorySchema.UseISO885915] == 1);
			}
			set
			{
				if (value != null)
				{
					this[ADOwaVirtualDirectorySchema.UseISO885915] = (value.Value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000ABF80 File Offset: 0x000AA180
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x000ABFB4 File Offset: 0x000AA1B4
		[Parameter]
		public OutboundCharsetOptions? OutboundCharset
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new OutboundCharsetOptions?((OutboundCharsetOptions)this[ADOwaVirtualDirectorySchema.OutboundCharset]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.OutboundCharset] = value;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x000ABFC8 File Offset: 0x000AA1C8
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x000ABFFC File Offset: 0x000AA1FC
		[Parameter]
		public bool? GlobalAddressListEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.GlobalAddressListEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.GlobalAddressListEnabled] = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000AC010 File Offset: 0x000AA210
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x000AC044 File Offset: 0x000AA244
		[Parameter]
		public bool? OrganizationEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.OrganizationEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.OrganizationEnabled] = value;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000AC058 File Offset: 0x000AA258
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x000AC08C File Offset: 0x000AA28C
		[Parameter]
		public bool? ExplicitLogonEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ExplicitLogonEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ExplicitLogonEnabled] = value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000AC0A0 File Offset: 0x000AA2A0
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x000AC0D4 File Offset: 0x000AA2D4
		[Parameter]
		public bool? OWALightEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.OWALightEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.OWALightEnabled] = value;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000AC0E8 File Offset: 0x000AA2E8
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x000AC11C File Offset: 0x000AA31C
		[Parameter]
		public bool? DelegateAccessEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.DelegateAccessEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DelegateAccessEnabled] = value;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000AC130 File Offset: 0x000AA330
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x000AC164 File Offset: 0x000AA364
		[Parameter]
		public bool? IRMEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.IRMEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.IRMEnabled] = value;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000AC178 File Offset: 0x000AA378
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x000AC1AC File Offset: 0x000AA3AC
		[Parameter]
		public bool? CalendarEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.CalendarEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.CalendarEnabled] = value;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000AC1C0 File Offset: 0x000AA3C0
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x000AC1F4 File Offset: 0x000AA3F4
		[Parameter]
		public bool? ContactsEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ContactsEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ContactsEnabled] = value;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000AC208 File Offset: 0x000AA408
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x000AC23C File Offset: 0x000AA43C
		[Parameter]
		public bool? TasksEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.TasksEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.TasksEnabled] = value;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000AC250 File Offset: 0x000AA450
		// (set) Token: 0x060028CD RID: 10445 RVA: 0x000AC284 File Offset: 0x000AA484
		[Parameter]
		public bool? JournalEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.JournalEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.JournalEnabled] = value;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x000AC298 File Offset: 0x000AA498
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x000AC2CC File Offset: 0x000AA4CC
		[Parameter]
		public bool? NotesEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.NotesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.NotesEnabled] = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000AC2E0 File Offset: 0x000AA4E0
		// (set) Token: 0x060028D1 RID: 10449 RVA: 0x000AC314 File Offset: 0x000AA514
		[Parameter]
		public bool? RemindersAndNotificationsEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.RemindersAndNotificationsEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RemindersAndNotificationsEnabled] = value;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000AC328 File Offset: 0x000AA528
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x000AC35C File Offset: 0x000AA55C
		[Parameter]
		public bool? PremiumClientEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.PremiumClientEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.PremiumClientEnabled] = value;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x000AC370 File Offset: 0x000AA570
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x000AC3A4 File Offset: 0x000AA5A4
		[Parameter]
		public bool? SpellCheckerEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SpellCheckerEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SpellCheckerEnabled] = value;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000AC3B8 File Offset: 0x000AA5B8
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x000AC3EC File Offset: 0x000AA5EC
		[Parameter]
		public bool? SearchFoldersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SearchFoldersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SearchFoldersEnabled] = value;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000AC400 File Offset: 0x000AA600
		// (set) Token: 0x060028D9 RID: 10457 RVA: 0x000AC434 File Offset: 0x000AA634
		[Parameter]
		public bool? SignaturesEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SignaturesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SignaturesEnabled] = value;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060028DA RID: 10458 RVA: 0x000AC448 File Offset: 0x000AA648
		// (set) Token: 0x060028DB RID: 10459 RVA: 0x000AC47C File Offset: 0x000AA67C
		[Parameter]
		public bool? ThemeSelectionEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ThemeSelectionEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ThemeSelectionEnabled] = value;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000AC490 File Offset: 0x000AA690
		// (set) Token: 0x060028DD RID: 10461 RVA: 0x000AC4C4 File Offset: 0x000AA6C4
		[Parameter]
		public bool? JunkEmailEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.JunkEmailEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.JunkEmailEnabled] = value;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x000AC4D8 File Offset: 0x000AA6D8
		// (set) Token: 0x060028DF RID: 10463 RVA: 0x000AC50C File Offset: 0x000AA70C
		[Parameter]
		public bool? UMIntegrationEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.UMIntegrationEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.UMIntegrationEnabled] = value;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000AC520 File Offset: 0x000AA720
		// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000AC554 File Offset: 0x000AA754
		[Parameter]
		public bool? WSSAccessOnPublicComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WSSAccessOnPublicComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WSSAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000AC568 File Offset: 0x000AA768
		// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000AC59C File Offset: 0x000AA79C
		[Parameter]
		public bool? WSSAccessOnPrivateComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WSSAccessOnPrivateComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WSSAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000AC5B0 File Offset: 0x000AA7B0
		// (set) Token: 0x060028E5 RID: 10469 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		[Parameter]
		public bool? ChangePasswordEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ChangePasswordEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ChangePasswordEnabled] = value;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x000AC5F8 File Offset: 0x000AA7F8
		// (set) Token: 0x060028E7 RID: 10471 RVA: 0x000AC62C File Offset: 0x000AA82C
		[Parameter]
		public bool? UNCAccessOnPublicComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.UNCAccessOnPublicComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.UNCAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x000AC640 File Offset: 0x000AA840
		// (set) Token: 0x060028E9 RID: 10473 RVA: 0x000AC674 File Offset: 0x000AA874
		[Parameter]
		public bool? UNCAccessOnPrivateComputersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.UNCAccessOnPrivateComputersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.UNCAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000AC688 File Offset: 0x000AA888
		// (set) Token: 0x060028EB RID: 10475 RVA: 0x000AC6BC File Offset: 0x000AA8BC
		[Parameter]
		public bool? ActiveSyncIntegrationEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ActiveSyncIntegrationEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ActiveSyncIntegrationEnabled] = value;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x000AC6D0 File Offset: 0x000AA8D0
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x000AC704 File Offset: 0x000AA904
		[Parameter]
		public bool? AllAddressListsEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.AllAddressListsEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AllAddressListsEnabled] = value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000AC718 File Offset: 0x000AA918
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x000AC74C File Offset: 0x000AA94C
		[Parameter]
		public bool? RulesEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.RulesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RulesEnabled] = value;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000AC760 File Offset: 0x000AA960
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x000AC794 File Offset: 0x000AA994
		[Parameter]
		public bool? PublicFoldersEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.PublicFoldersEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.PublicFoldersEnabled] = value;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000AC7A8 File Offset: 0x000AA9A8
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000AC7DC File Offset: 0x000AA9DC
		[Parameter]
		public bool? SMimeEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SMimeEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SMimeEnabled] = value;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000AC7F0 File Offset: 0x000AA9F0
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000AC824 File Offset: 0x000AAA24
		[Parameter]
		public bool? RecoverDeletedItemsEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.RecoverDeletedItemsEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.RecoverDeletedItemsEnabled] = value;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000AC838 File Offset: 0x000AAA38
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x000AC86C File Offset: 0x000AAA6C
		[Parameter]
		public bool? InstantMessagingEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.InstantMessagingEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.InstantMessagingEnabled] = value;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000AC880 File Offset: 0x000AAA80
		// (set) Token: 0x060028F9 RID: 10489 RVA: 0x000AC8B4 File Offset: 0x000AAAB4
		[Parameter]
		public bool? TextMessagingEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.TextMessagingEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.TextMessagingEnabled] = value;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x000AC8C8 File Offset: 0x000AAAC8
		// (set) Token: 0x060028FB RID: 10491 RVA: 0x000AC8FC File Offset: 0x000AAAFC
		[Parameter]
		public bool? ForceSaveAttachmentFilteringEnabled
		{
			get
			{
				if (!this.IsExchange2007OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.ForceSaveAttachmentFilteringEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.ForceSaveAttachmentFilteringEnabled] = value;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000AC910 File Offset: 0x000AAB10
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000AC944 File Offset: 0x000AAB44
		[Parameter]
		public bool? SilverlightEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SilverlightEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SilverlightEnabled] = value;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000AC958 File Offset: 0x000AAB58
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x000AC98C File Offset: 0x000AAB8C
		[Parameter]
		public bool? PlacesEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.PlacesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.PlacesEnabled] = value;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000AC9A0 File Offset: 0x000AABA0
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x000AC9D4 File Offset: 0x000AABD4
		[Parameter]
		public bool? WeatherEnabled
		{
			get
			{
				if (!this.IsExchange2013OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.WeatherEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WeatherEnabled] = value;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000AC9E8 File Offset: 0x000AABE8
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x000ACA1C File Offset: 0x000AAC1C
		[Parameter]
		public bool? AllowCopyContactsToDeviceAddressBook
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.AllowCopyContactsToDeviceAddressBook]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AllowCopyContactsToDeviceAddressBook] = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000ACA30 File Offset: 0x000AAC30
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x000ACA64 File Offset: 0x000AAC64
		[Parameter]
		public bool? AnonymousFeaturesEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.AnonymousFeaturesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AnonymousFeaturesEnabled] = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000ACA78 File Offset: 0x000AAC78
		// (set) Token: 0x06002907 RID: 10503 RVA: 0x000ACAAC File Offset: 0x000AACAC
		[Parameter]
		public bool? IntegratedFeaturesEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.IntegratedFeaturesEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.IntegratedFeaturesEnabled] = value;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000ACAC0 File Offset: 0x000AACC0
		// (set) Token: 0x06002909 RID: 10505 RVA: 0x000ACAF4 File Offset: 0x000AACF4
		[Parameter]
		public bool? DisplayPhotosEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.DisplayPhotosEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.DisplayPhotosEnabled] = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x000ACB08 File Offset: 0x000AAD08
		// (set) Token: 0x0600290B RID: 10507 RVA: 0x000ACB3C File Offset: 0x000AAD3C
		[Parameter]
		public bool? SetPhotoEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.SetPhotoEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SetPhotoEnabled] = value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x000ACB50 File Offset: 0x000AAD50
		// (set) Token: 0x0600290D RID: 10509 RVA: 0x000ACB84 File Offset: 0x000AAD84
		[Parameter]
		public bool? PredictedActionsEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.PredictedActionsEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.PredictedActionsEnabled] = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000ACB98 File Offset: 0x000AAD98
		// (set) Token: 0x0600290F RID: 10511 RVA: 0x000ACBCC File Offset: 0x000AADCC
		[Parameter]
		public bool? UserDiagnosticEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.UserDiagnosticEnabled]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.UserDiagnosticEnabled] = value;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000ACBE0 File Offset: 0x000AADE0
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x000ACC14 File Offset: 0x000AAE14
		[Parameter(Mandatory = false)]
		public bool? ReportJunkEmailEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[OwaMailboxPolicySchema.ReportJunkEmailEnabled]);
			}
			set
			{
				this[OwaMailboxPolicySchema.ReportJunkEmailEnabled] = value;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x000ACC28 File Offset: 0x000AAE28
		// (set) Token: 0x06002913 RID: 10515 RVA: 0x000ACC5C File Offset: 0x000AAE5C
		[Parameter]
		public WebPartsFrameOptions? WebPartsFrameOptionsType
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new WebPartsFrameOptions?((WebPartsFrameOptions)this[ADOwaVirtualDirectorySchema.WebPartsFrameOptionsType]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebPartsFrameOptionsType] = (int)value.Value;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x000ACC75 File Offset: 0x000AAE75
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x000ACC87 File Offset: 0x000AAE87
		[Parameter(Mandatory = false)]
		public AllowOfflineOnEnum AllowOfflineOn
		{
			get
			{
				return (AllowOfflineOnEnum)this[ADOwaVirtualDirectorySchema.AllowOfflineOn];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.AllowOfflineOn] = value;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000ACC9A File Offset: 0x000AAE9A
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x000ACCB6 File Offset: 0x000AAEB6
		[Parameter]
		public string SetPhotoURL
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return (string)this[ADOwaVirtualDirectorySchema.SetPhotoURL];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.SetPhotoURL] = value;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x000ACCC4 File Offset: 0x000AAEC4
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x000ACCF8 File Offset: 0x000AAEF8
		[Parameter]
		public InstantMessagingTypeOptions? InstantMessagingType
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new InstantMessagingTypeOptions?((InstantMessagingTypeOptions)this[ADOwaVirtualDirectorySchema.InstantMessagingType]);
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.InstantMessagingType] = value;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x000ACD0B File Offset: 0x000AAF0B
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x000ACD27 File Offset: 0x000AAF27
		[Parameter]
		public Uri Exchange2003Url
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return (Uri)this[ADOwaVirtualDirectorySchema.Exchange2003Url];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.Exchange2003Url] = value;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x000ACD35 File Offset: 0x000AAF35
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x000ACD51 File Offset: 0x000AAF51
		[Parameter]
		public Uri FailbackUrl
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return (Uri)this[ADOwaVirtualDirectorySchema.FailbackUrl];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.FailbackUrl] = value;
			}
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000ACD60 File Offset: 0x000AAF60
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.LogonFormat == LogonFormats.UserName && (base.DefaultDomain == null || base.DefaultDomain.Length == 0) && !base.ADPropertiesOnly)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.OwaDefaultDomainRequiredWhenLogonFormatIsUserName, ADOwaVirtualDirectorySchema.LogonFormat, this.LogonFormat));
			}
			if (base.MetabasePath.Length != 0)
			{
				if (base.MetabasePath.ToUpper().IndexOf("IIS://") != 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.OwaMetabasePathIsInvalid(base.Id.Name, base.MetabasePath), ExchangeVirtualDirectorySchema.MetabasePath, base.MetabasePath));
				}
				if (!base.ADPropertiesOnly)
				{
					using (DirectoryEntry directoryEntry = new DirectoryEntry(base.MetabasePath))
					{
						if (directoryEntry == null)
						{
							errors.Add(new PropertyValidationError(DirectoryStrings.OwaAdOrphanFound(base.Id.Name), ExchangeVirtualDirectorySchema.MetabasePath, base.MetabasePath));
						}
					}
				}
				if (this.propertyBag.IsModified(ADVirtualDirectorySchema.InternalUrl) && !this.IsExchange2007OrLater)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("InternalUrl"), ADVirtualDirectorySchema.InternalUrl, base.InternalUrl));
				}
				if (this.propertyBag.IsModified(ADVirtualDirectorySchema.ExternalUrl) && !this.IsExchange2007OrLater)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("ExternalUrl"), ADVirtualDirectorySchema.ExternalUrl, base.ExternalUrl));
				}
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("RemoteDocumentsActionForUnknownServers"), ADOwaVirtualDirectorySchema.RemoteDocumentsActionForUnknownServers, this.RemoteDocumentsActionForUnknownServers));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.ActionForUnknownFileAndMIMETypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("ActionForUnknownFileAndMIMETypes"), ADOwaVirtualDirectorySchema.ActionForUnknownFileAndMIMETypes, this.ActionForUnknownFileAndMIMETypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADWebReadyFileTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("WebReadyFileTypes"), ADOwaVirtualDirectorySchema.WebReadyFileTypes, this.WebReadyFileTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADWebReadyMimeTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("WebReadyMimeTypes"), ADOwaVirtualDirectorySchema.WebReadyMimeTypes, this.WebReadyFileTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADAllowedFileTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("AllowedFileTypes"), ADOwaVirtualDirectorySchema.AllowedFileTypes, this.AllowedFileTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADAllowedMimeTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("AllowedMimeTypes"), ADOwaVirtualDirectorySchema.AllowedMimeTypes, this.AllowedMimeTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADForceSaveFileTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("ForceSaveFileTypes"), ADOwaVirtualDirectorySchema.ForceSaveFileTypes, this.ForceSaveFileTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADForceSaveMimeTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("ForceSaveMimeTypes"), ADOwaVirtualDirectorySchema.ForceSaveMimeTypes, this.ForceSaveMimeTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADBlockedFileTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("BlockedFileTypes"), ADOwaVirtualDirectorySchema.BlockedFileTypes, this.BlockedFileTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADBlockedMimeTypes) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("BlockedMimeTypes"), ADOwaVirtualDirectorySchema.BlockedMimeTypes, this.BlockedMimeTypes));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADRemoteDocumentsAllowedServers) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("RemoteDocumentsAllowedServers"), ADOwaVirtualDirectorySchema.RemoteDocumentsAllowedServers, this.RemoteDocumentsAllowedServers));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADRemoteDocumentsBlockedServers) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("RemoteDocumentsBlockedServers"), ADOwaVirtualDirectorySchema.RemoteDocumentsBlockedServers, this.RemoteDocumentsBlockedServers));
			}
			if (this.propertyBag.IsChanged(ADOwaVirtualDirectorySchema.ADRemoteDocumentsInternalDomainSuffixList) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("RemoteDocumentsInternalDomainSuffixList"), ADOwaVirtualDirectorySchema.RemoteDocumentsInternalDomainSuffixList, this.RemoteDocumentsInternalDomainSuffixList));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.FilterWebBeaconsAndHtmlForms) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("FilterWebBeaconsAndHtmlForms"), ADOwaVirtualDirectorySchema.FilterWebBeaconsAndHtmlForms, this.FilterWebBeaconsAndHtmlForms));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.NotificationInterval) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("NotificationInterval"), ADOwaVirtualDirectorySchema.NotificationInterval, this.NotificationInterval));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.DefaultTheme) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("DefaultTheme"), ADOwaVirtualDirectorySchema.DefaultTheme, this.DefaultTheme));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.UserContextTimeout) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("UserContextTimeout"), ADOwaVirtualDirectorySchema.UserContextTimeout, this.UserContextTimeout));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.RedirectToOptimalOWAServer) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("RedirectToOptimalOWAServer"), ADOwaVirtualDirectorySchema.RedirectToOptimalOWAServer, this.RedirectToOptimalOWAServer));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.DefaultClientLanguage) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("DefaultClientLanguage"), ADOwaVirtualDirectorySchema.DefaultClientLanguage, this.DefaultClientLanguage));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.UseGB18030) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("UseGB18030"), ADOwaVirtualDirectorySchema.UseGB18030, this.UseGB18030));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.UseISO885915) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("UseISO885915"), ADOwaVirtualDirectorySchema.UseISO885915, this.UseISO885915));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.OutboundCharset) && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("OutboundCharset"), ADOwaVirtualDirectorySchema.OutboundCharset, this.OutboundCharset));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.InstantMessagingType) && !this.IsExchange2009OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("InstantMessagingType"), ADOwaVirtualDirectorySchema.InstantMessagingType, this.InstantMessagingType));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.LogonAndErrorLanguage) && this.LogonAndErrorLanguage != 0 && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("LogonAndErrorLanguage"), ADOwaVirtualDirectorySchema.LogonAndErrorLanguage, this.LogonAndErrorLanguage));
			}
			if (this.propertyBag.IsModified(ExchangeWebAppVirtualDirectorySchema.DigestAuthentication) && base.DigestAuthentication && !this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnLegacyVirtualDirectory("DigestAuthentication"), ExchangeWebAppVirtualDirectorySchema.DigestAuthentication, base.DigestAuthentication));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.ExchwebProxyDestination) && !this.ExchwebProxyDestination.Equals(ExchwebProxyDestinations.NotSpecified) && this.VirtualDirectoryType != VirtualDirectoryTypes.Exchweb)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnVirtualDirectoryOtherThanExchweb, ADOwaVirtualDirectorySchema.ExchwebProxyDestination, this.ExchwebProxyDestination));
			}
			if (this.propertyBag.IsModified(ADOwaVirtualDirectorySchema.VirtualDirectoryType) && this.IsExchange2007OrLater)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnE12VirtualDirectory("VirtualDirectoryType"), ADOwaVirtualDirectorySchema.VirtualDirectoryType, this.VirtualDirectoryType));
			}
		}

		// Token: 0x040018E8 RID: 6376
		private static readonly ADOwaVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADOwaVirtualDirectorySchema>();

		// Token: 0x040018E9 RID: 6377
		private static readonly MultiValuedProperty<string> webReadyDocumentViewingSupportedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyFileTypes);

		// Token: 0x040018EA RID: 6378
		private static readonly MultiValuedProperty<string> exchange2007RTMWebReadyDocumentViewingSupportedFileTypes = new MultiValuedProperty<string>(ADOwaVirtualDirectorySchema.Exchange2007RTMWebReadyDocumentViewingSupportedFileTypes);

		// Token: 0x040018EB RID: 6379
		private static readonly MultiValuedProperty<string> webReadyDocumentViewingSupportedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyMimeTypes);

		// Token: 0x040018EC RID: 6380
		private static readonly MultiValuedProperty<string> exchange2007RTMWebReadyDocumentViewingSupportedMimeTypes = new MultiValuedProperty<string>(ADOwaVirtualDirectorySchema.Exchange2007RTMWebReadyDocumentViewingSupportedMimeTypes);

		// Token: 0x040018ED RID: 6381
		public static readonly string MostDerivedClass = "msExchOwaVirtualDirectory";

		// Token: 0x040018EE RID: 6382
		private static ITopologyConfigurationSession configurationSession;

		// Token: 0x040018EF RID: 6383
		private bool? isOnExchange2007RTM = null;
	}
}
