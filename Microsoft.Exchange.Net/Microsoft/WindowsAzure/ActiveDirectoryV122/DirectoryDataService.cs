﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x0200058C RID: 1420
	[CLSCompliant(false)]
	public class DirectoryDataService : DataServiceContext
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x0002AFC5 File Offset: 0x000291C5
		public DirectoryDataService(string graphBaseURL, string tenantDomainOrContext, AADJWTToken token) : this(new Uri(new Uri(graphBaseURL), tenantDomainOrContext))
		{
			this.authenticationToken = token;
			base.BuildingRequest += this.OnBuildingRequest;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0002AFF2 File Offset: 0x000291F2
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x0002AFFA File Offset: 0x000291FA
		public string TenantName { get; private set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0002B003 File Offset: 0x00029203
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0002B00B File Offset: 0x0002920B
		public bool UsePermissiveReferenceUpdates { get; set; }

		// Token: 0x0600130F RID: 4879 RVA: 0x0002B014 File Offset: 0x00029214
		internal void OnBuildingRequest(object sender, BuildingRequestEventArgs args)
		{
			args.RequestUri = DirectoryDataService.GetRequestUriWithAdditionalArguments(args.RequestUri, "1.22-preview", this.UsePermissiveReferenceUpdates);
			if (this.authenticationToken != null)
			{
				string value = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					this.authenticationToken.TokenType,
					" ",
					this.authenticationToken.AccessToken
				});
				args.Headers.Add("Authorization", value);
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0002B094 File Offset: 0x00029294
		private static Uri GetRequestUriWithAdditionalArguments(Uri origRequestUri, string apiVersion, bool usePermissiveReferenceUpdates)
		{
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(origRequestUri.Query);
			if (string.IsNullOrEmpty(nameValueCollection["api-version"]))
			{
				nameValueCollection["api-version"] = apiVersion;
			}
			if (usePermissiveReferenceUpdates)
			{
				nameValueCollection["permissive-reference-updates"] = "true";
			}
			return new UriBuilder(origRequestUri)
			{
				Query = nameValueCollection.ToString()
			}.Uri;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0002B0F7 File Offset: 0x000292F7
		public DataServiceQuery<User> users
		{
			get
			{
				return this.directoryObjects.OfType<User>() as DataServiceQuery<User>;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0002B109 File Offset: 0x00029309
		public DataServiceQuery<Group> groups
		{
			get
			{
				return this.directoryObjects.OfType<Group>() as DataServiceQuery<Group>;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0002B11B File Offset: 0x0002931B
		public DataServiceQuery<Contact> contacts
		{
			get
			{
				return this.directoryObjects.OfType<Contact>() as DataServiceQuery<Contact>;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0002B12D File Offset: 0x0002932D
		public DataServiceQuery<ServicePrincipal> servicePrincipals
		{
			get
			{
				return this.directoryObjects.OfType<ServicePrincipal>() as DataServiceQuery<ServicePrincipal>;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0002B13F File Offset: 0x0002933F
		public DataServiceQuery<Role> roles
		{
			get
			{
				return this.directoryObjects.OfType<Role>() as DataServiceQuery<Role>;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0002B151 File Offset: 0x00029351
		public DataServiceQuery<TenantDetail> tenantDetails
		{
			get
			{
				return this.directoryObjects.OfType<TenantDetail>() as DataServiceQuery<TenantDetail>;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0002B163 File Offset: 0x00029363
		public DataServiceQuery<Application> applications
		{
			get
			{
				return this.directoryObjects.OfType<Application>() as DataServiceQuery<Application>;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0002B175 File Offset: 0x00029375
		public DataServiceQuery<Device> devices
		{
			get
			{
				return this.directoryObjects.OfType<Device>() as DataServiceQuery<Device>;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0002B187 File Offset: 0x00029387
		public void AddTogroups(Group group)
		{
			base.AddObject("directoryObjects", group);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0002B195 File Offset: 0x00029395
		public void AddTousers(User user)
		{
			base.AddObject("directoryObjects", user);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0002B1A3 File Offset: 0x000293A3
		public void AddTocontacts(Contact contact)
		{
			base.AddObject("directoryObjectss", contact);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0002B1B1 File Offset: 0x000293B1
		public void AddToroles(Role role)
		{
			base.AddObject("directoryObjects", role);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0002B1BF File Offset: 0x000293BF
		public void AddToserviceprincipals(ServicePrincipal servicePrincipal)
		{
			base.AddObject("directoryObjects", servicePrincipal);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0002B1CD File Offset: 0x000293CD
		public void AddTotenantDetails(TenantDetail tenantDetail)
		{
			base.AddObject("directoryObjects", tenantDetail);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0002B1DC File Offset: 0x000293DC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DirectoryDataService(Uri serviceRoot) : base(serviceRoot, 2)
		{
			base.ResolveName = new Func<Type, string>(this.ResolveNameFromType);
			base.ResolveType = new Func<string, Type>(this.ResolveTypeFromName);
			base.UrlConventions = DataServiceUrlConventions.KeyAsSegment;
			base.Format.LoadServiceModel = new Func<IEdmModel>(DirectoryDataService.GeneratedEdmModel.GetInstance);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0002B237 File Offset: 0x00029437
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		protected Type ResolveTypeFromName(string typeName)
		{
			if (typeName.StartsWith("Microsoft.WindowsAzure.ActiveDirectory", StringComparison.Ordinal))
			{
				return base.GetType().Assembly.GetType("Microsoft.WindowsAzure.ActiveDirectoryV122" + typeName.Substring(38), false);
			}
			return null;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0002B26C File Offset: 0x0002946C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		protected string ResolveNameFromType(Type clientType)
		{
			if (clientType.Namespace.Equals("Microsoft.WindowsAzure.ActiveDirectoryV122", StringComparison.Ordinal))
			{
				return "Microsoft.WindowsAzure.ActiveDirectory." + clientType.Name;
			}
			return clientType.FullName;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0002B298 File Offset: 0x00029498
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceQuery<AppLocalizedBranding> appLocalizedBranding
		{
			get
			{
				if (this._appLocalizedBranding == null)
				{
					this._appLocalizedBranding = base.CreateQuery<AppLocalizedBranding>("appLocalizedBranding");
				}
				return this._appLocalizedBranding;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0002B2B9 File Offset: 0x000294B9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceQuery<AppNonLocalizedBranding> appNonLocalizedBranding
		{
			get
			{
				if (this._appNonLocalizedBranding == null)
				{
					this._appNonLocalizedBranding = base.CreateQuery<AppNonLocalizedBranding>("appNonLocalizedBranding");
				}
				return this._appNonLocalizedBranding;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0002B2DA File Offset: 0x000294DA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceQuery<DirectoryObject> directoryObjects
		{
			get
			{
				if (this._directoryObjects == null)
				{
					this._directoryObjects = base.CreateQuery<DirectoryObject>("directoryObjects");
				}
				return this._directoryObjects;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0002B2FB File Offset: 0x000294FB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceQuery<Permission> permissions
		{
			get
			{
				if (this._permissions == null)
				{
					this._permissions = base.CreateQuery<Permission>("permissions");
				}
				return this._permissions;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0002B31C File Offset: 0x0002951C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceQuery<SubscribedSku> subscribedSkus
		{
			get
			{
				if (this._subscribedSkus == null)
				{
					this._subscribedSkus = base.CreateQuery<SubscribedSku>("subscribedSkus");
				}
				return this._subscribedSkus;
			}
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0002B33D File Offset: 0x0002953D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public void AddToappLocalizedBranding(AppLocalizedBranding appLocalizedBranding)
		{
			base.AddObject("appLocalizedBranding", appLocalizedBranding);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0002B34B File Offset: 0x0002954B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public void AddToappNonLocalizedBranding(AppNonLocalizedBranding appNonLocalizedBranding)
		{
			base.AddObject("appNonLocalizedBranding", appNonLocalizedBranding);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0002B359 File Offset: 0x00029559
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public void AddTodirectoryObjects(DirectoryObject directoryObject)
		{
			base.AddObject("directoryObjects", directoryObject);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0002B367 File Offset: 0x00029567
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public void AddTopermissions(Permission permission)
		{
			base.AddObject("permissions", permission);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0002B375 File Offset: 0x00029575
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public void AddTosubscribedSkus(SubscribedSku subscribedSku)
		{
			base.AddObject("subscribedSkus", subscribedSku);
		}

		// Token: 0x0400189A RID: 6298
		internal const string GraphServiceVersion = "1.22-preview";

		// Token: 0x0400189B RID: 6299
		private AADJWTToken authenticationToken;

		// Token: 0x0400189C RID: 6300
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceQuery<AppLocalizedBranding> _appLocalizedBranding;

		// Token: 0x0400189D RID: 6301
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceQuery<AppNonLocalizedBranding> _appNonLocalizedBranding;

		// Token: 0x0400189E RID: 6302
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceQuery<DirectoryObject> _directoryObjects;

		// Token: 0x0400189F RID: 6303
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceQuery<Permission> _permissions;

		// Token: 0x040018A0 RID: 6304
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceQuery<SubscribedSku> _subscribedSkus;

		// Token: 0x0200058D RID: 1421
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private abstract class GeneratedEdmModel
		{
			// Token: 0x0600132C RID: 4908 RVA: 0x0002B384 File Offset: 0x00029584
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private static string GetConcatenatedEdmxString()
			{
				return string.Concat(new string[]
				{
					"<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edmx\"><edmx:DataServices m:DataServiceVersion=\"3.0\" m:MaxDataServiceVersion=\"3.0\" xmlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema Namespace=\"Microsoft.WindowsAzure.ActiveDirectory\" xmlns=\"http://schemas.microsoft.com/ado/2009/11/edm\"><EntityType Name=\"DirectoryObject\" OpenType=\"true\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"objectType\" Type=\"Edm.String\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><NavigationProperty Name=\"createdOnBehalfOf\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdOnBehalfOf\" ToRole=\"createdOnBehalfOf\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"createdObjects\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdObjects\" ToRole=\"createdObjects\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"manager\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_manager\" ToRole=\"manager\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"directReports\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_directReports\" ToRole=\"directReports\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"members\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_members\" ToRole=\"members\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"memberOf\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_memberOf\" ToRole=\"memberOf\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"owners\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_owners\" ToRole=\"owners\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"ownedObjects\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_ownedObjects\" ToRole=\"ownedObjects\" FromRole=\"DirectoryObject\" /></EntityType><EntityType Name=\"User\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"assignedLicenses\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedLicense)\" Nullable=\"false\" /><Property Name=\"assignedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedPlan)\" Nullable=\"false\" /><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"department\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"facsimileTelephoneNumber\" Type=\"Edm.String\" /><Property Name=\"givenName\" Type=\"Edm.String\" /><Property Name=\"immutableId\" Type=\"Edm.String\" /><Property Name=\"jobTitle\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mobile\" Type=\"Edm.String\" /><Property Name=\"otherMails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"passwordPolicies\" Type=\"Edm.String\" /><Property Name=\"passwordProfile\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.PasswordProfile\" /><Property Name=\"physicalDeliveryOfficeName\" Type=\"Edm.String\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"preferredLanguage\" Type=\"Edm.String\" /><Property Name=\"provisionedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisionedPlan)\" Nullable=\"false\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"streetAddress\" Type=\"Edm.String\" /><Property Name=\"surname\" Type=\"Edm.String\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"thumbnailPhoto\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"usageLocation\" Type=\"Edm.String\" /><Property Name=\"userPrincipalName\" Type=\"Edm.String\" /><Property Name=\"userType\" Type=\"Edm.String\" /><NavigationProperty Name=\"permissions\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_permissions\" ToRole=\"permissions\" FromRole=\"User\" /><NavigationProperty Name=\"registeredDevices\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_registeredDevices\" ToRole=\"registeredDevices\" FromRole=\"User\" /><NavigationProperty Name=\"ownedDevices\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_ownedDevices\" ToRole=\"ownedDevices\" FromRole=\"User\" /></EntityType><ComplexType Name=\"AssignedLicense\"><Property Name=\"disabledPlans\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" /><Property Name=\"skuId\" Type=\"Edm.Guid\" /></ComplexType><EntityType Name=\"AppLocalizedBranding\"><Key><PropertyRef Name=\"locale\" /></Key><Property Name=\"appBannerLogo\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"appBannerLogoUrl\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"heroIllustration\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"heroIllustrationUrl\" Type=\"Edm.String\" /><Property Name=\"locale\" Type=\"Edm.String\" Nullable=\"false\" /></EntityType><EntityType Name=\"AppNonLocalizedBranding\"><Key><PropertyRef Name=\"locale\" /></Key><Property Name=\"heroBackgroundColor\" Type=\"Edm.String\" /><Property Name=\"locale\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"preloadUrl\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"ExtensionProperty\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"name\" Type=\"Edm.String\" /><Property Name=\"dataType\" Type=\"Edm.String\" /><Property Name=\"targetObjects\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /></EntityType><EntityType Name=\"Application\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"allowActAsForAllClients\" Type=\"Edm.Boolean\" /><Property Name=\"appId\" Type=\"Edm.Guid\" /><Property Name=\"availableToOtherTenants\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"errorUrl\" Type=\"Edm.String\" /><Property Name=\"groupMembershipClaims\" Type=\"Edm.String\" /><Property Name=\"homepage\" Type=\"Edm.String\" /><Property Name=\"identifierUris\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"keyCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.KeyCredential)\" Nullable=\"false\" /><Property Name=\"mainLogo\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"logoutUrl\" Type=\"Edm.String\" /><Property Name=\"passwordCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.PasswordCredential)\" Nullable=\"false\" /><Property Name=\"publicClient\" Type=\"Edm.Boolean\" /><Property Name=\"replyUrls\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"samlMetadataUrl\" Type=\"Edm.String\" /><NavigationProperty Name=\"appLocalizedBranding\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appLocalizedBranding\" ToRole=\"appLocalizedBranding\" FromRole=\"Application\" /><NavigationProperty Name=\"appNonLocalizedBranding\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appNonLocalizedBranding\" ToRole=\"appNonLocalizedBranding\" FromRole=\"Application\" /><NavigationProperty Name=\"extensionProperties\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_extensionProperties\" ToRole=\"extensionProperties\" FromRole=\"Application\" /></EntityType><ComplexType Name=\"KeyCredential\"><Pr",
					"operty Name=\"customKeyIdentifier\" Type=\"Edm.Binary\" /><Property Name=\"endDate\" Type=\"Edm.DateTime\" /><Property Name=\"keyId\" Type=\"Edm.Guid\" /><Property Name=\"startDate\" Type=\"Edm.DateTime\" /><Property Name=\"type\" Type=\"Edm.String\" /><Property Name=\"usage\" Type=\"Edm.String\" /><Property Name=\"value\" Type=\"Edm.Binary\" /></ComplexType><ComplexType Name=\"PasswordCredential\"><Property Name=\"customKeyIdentifier\" Type=\"Edm.Binary\" /><Property Name=\"endDate\" Type=\"Edm.DateTime\" /><Property Name=\"keyId\" Type=\"Edm.Guid\" /><Property Name=\"startDate\" Type=\"Edm.DateTime\" /><Property Name=\"value\" Type=\"Edm.String\" /></ComplexType><EntityType Name=\"Contact\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"department\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"facsimileTelephoneNumber\" Type=\"Edm.String\" /><Property Name=\"givenName\" Type=\"Edm.String\" /><Property Name=\"jobTitle\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mobile\" Type=\"Edm.String\" /><Property Name=\"physicalDeliveryOfficeName\" Type=\"Edm.String\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"streetAddress\" Type=\"Edm.String\" /><Property Name=\"surname\" Type=\"Edm.String\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"thumbnailPhoto\" Type=\"Edm.Stream\" Nullable=\"false\" /></EntityType><ComplexType Name=\"ProvisioningError\"><Property Name=\"errorDetail\" Type=\"Edm.String\" /><Property Name=\"resolved\" Type=\"Edm.Boolean\" /><Property Name=\"service\" Type=\"Edm.String\" /><Property Name=\"timestamp\" Type=\"Edm.DateTime\" /></ComplexType><EntityType Name=\"Device\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"alternativeSecurityIds\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AlternativeSecurityId)\" Nullable=\"false\" /><Property Name=\"approximateLastLogonTimestamp\" Type=\"Edm.DateTime\" /><Property Name=\"deviceId\" Type=\"Edm.Guid\" /><Property Name=\"deviceObjectVersion\" Type=\"Edm.Int32\" /><Property Name=\"deviceOSType\" Type=\"Edm.String\" /><Property Name=\"deviceOSVersion\" Type=\"Edm.String\" /><Property Name=\"devicePhysicalIds\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"exchangeActiveSyncId\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"isCompliant\" Type=\"Edm.Boolean\" /><Property Name=\"isManaged\" Type=\"Edm.Boolean\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><NavigationProperty Name=\"registeredOwners\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredOwners\" ToRole=\"registeredOwners\" FromRole=\"Device\" /><NavigationProperty Name=\"registeredUsers\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredUsers\" ToRole=\"registeredUsers\" FromRole=\"Device\" /></EntityType><ComplexType Name=\"AlternativeSecurityId\"><Property Name=\"type\" Type=\"Edm.Int32\" /><Property Name=\"identityProvider\" Type=\"Edm.String\" /><Property Name=\"key\" Type=\"Edm.Binary\" /></ComplexType><EntityType Name=\"DeviceConfiguration\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"publicIssuerCertificates\" Type=\"Collection(Edm.Binary)\" Nullable=\"false\" /><Property Name=\"cloudPublicIssuerCertificates\" Type=\"Collection(Edm.Binary)\" Nullable=\"false\" /><Property Name=\"registrationQuota\" Type=\"Edm.Int32\" /><Property Name=\"maximumRegistrationInactivityPeriod\" Type=\"Edm.Int32\" /></EntityType><EntityType Name=\"DirectoryLinkChange\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"associationType\" Type=\"Edm.String\" /><Property Name=\"sourceObjectId\" Type=\"Edm.String\" /><Property Name=\"sourceObjectType\" Type=\"Edm.String\" /><Property Name=\"sourceObjectUri\" Type=\"Edm.String\" /><Property Name=\"targetObjectId\" Type=\"Edm.String\" /><Property Name=\"targetObjectType\" Type=\"Edm.String\" /><Property Name=\"targetObjectUri\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"Group\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mailEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"securityEnabled\" Type=\"Edm.Boolean\" /></EntityType><EntityType Name=\"Role\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"isSystem\" Type=\"Edm.Boolean\" /><Property Name=\"roleDisabled\" Type=\"Edm.Boolean\" /></EntityType><EntityType Name=\"RoleTemplate\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"ServicePrincipal\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"appId\" Type=\"Edm.Guid\" /><Property Name=\"appOwnerTenantId\" Type=\"Edm.Guid\" /><Property Name=\"authenticationPolicy\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipalAuthenticationPolicy\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"errorUrl\" Type=\"Edm.String\" /><Property Name=\"homepage\" Type=\"Edm.String\" /><Property Name=\"keyCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.KeyCredential)\" Nullable=\"false\" /><Property Name=\"logoutUrl\" Type=\"Edm.String\" /><Property Name=\"passwordCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.PasswordCredential)\" Nullable=\"false\" /><Property Name=\"preferredTokenSigningKeyThumbprint\" Type=\"Edm.String\" /><Property Name=\"publisherName\" Type=\"Edm.String\" /><Property Name=\"replyUrls\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"samlMetadataUrl\" Type=\"Edm.String\" /><Property Name=\"servicePrincipalNames\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"tags\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><NavigationProperty Name=\"permissions\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal_permissions\" ToRole=\"permissions\" FromRole=\"ServicePrincipal\" /></EntityType><ComplexType Name=\"ServicePrincipalAuthenticationPolicy\"><Property Name=\"defaultPolicy\" Type=\"Edm.String\" /><Property Name=\"allowedPolicies\" Type=\"Collecti",
					"on(Edm.String)\" Nullable=\"false\" /></ComplexType><EntityType Name=\"TenantDetail\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"assignedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedPlan)\" Nullable=\"false\" /><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"companyLastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"countryLetterCode\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"marketingNotificationEmails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"preferredLanguage\" Type=\"Edm.String\" /><Property Name=\"provisionedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisionedPlan)\" Nullable=\"false\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"street\" Type=\"Edm.String\" /><Property Name=\"technicalNotificationMails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"tenantType\" Type=\"Edm.String\" /><Property Name=\"verifiedDomains\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.VerifiedDomain)\" Nullable=\"false\" /></EntityType><ComplexType Name=\"AssignedPlan\"><Property Name=\"assignedTimestamp\" Type=\"Edm.DateTime\" /><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"service\" Type=\"Edm.String\" /><Property Name=\"servicePlanId\" Type=\"Edm.Guid\" /></ComplexType><ComplexType Name=\"ProvisionedPlan\"><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"provisioningStatus\" Type=\"Edm.String\" /><Property Name=\"service\" Type=\"Edm.String\" /></ComplexType><ComplexType Name=\"VerifiedDomain\"><Property Name=\"capabilities\" Type=\"Edm.String\" /><Property Name=\"default\" Type=\"Edm.Boolean\" /><Property Name=\"id\" Type=\"Edm.String\" /><Property Name=\"initial\" Type=\"Edm.Boolean\" /><Property Name=\"name\" Type=\"Edm.String\" /><Property Name=\"type\" Type=\"Edm.String\" /></ComplexType><ComplexType Name=\"PasswordProfile\"><Property Name=\"password\" Type=\"Edm.String\" /><Property Name=\"forceChangePasswordNextLogin\" Type=\"Edm.Boolean\" /></ComplexType><EntityType Name=\"Permission\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"clientId\" Type=\"Edm.String\" /><Property Name=\"consentType\" Type=\"Edm.String\" /><Property Name=\"expiryTime\" Type=\"Edm.DateTime\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"principalId\" Type=\"Edm.String\" /><Property Name=\"resourceId\" Type=\"Edm.String\" /><Property Name=\"scope\" Type=\"Edm.String\" /><Property Name=\"startTime\" Type=\"Edm.DateTime\" /></EntityType><EntityType Name=\"SubscribedSku\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"consumedUnits\" Type=\"Edm.Int32\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"prepaidUnits\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.LicenseUnitsDetail\" /><Property Name=\"servicePlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ServicePlanInfo)\" Nullable=\"false\" /><Property Name=\"skuId\" Type=\"Edm.Guid\" /><Property Name=\"skuPartNumber\" Type=\"Edm.String\" /></EntityType><ComplexType Name=\"LicenseUnitsDetail\"><Property Name=\"enabled\" Type=\"Edm.Int32\" /><Property Name=\"suspended\" Type=\"Edm.Int32\" /><Property Name=\"warning\" Type=\"Edm.Int32\" /></ComplexType><ComplexType Name=\"ServicePlanInfo\"><Property Name=\"servicePlanId\" Type=\"Edm.Guid\" /><Property Name=\"servicePlanName\" Type=\"Edm.String\" /></ComplexType><Association Name=\"DirectoryObject_createdOnBehalfOf\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"createdOnBehalfOf\" Multiplicity=\"0..1\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_createdObjects\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"createdObjects\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_manager\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"manager\" Multiplicity=\"0..1\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_directReports\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"directReports\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_members\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"members\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_memberOf\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"memberOf\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_owners\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"owners\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_ownedObjects\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"ownedObjects\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"User_permissions\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" Role=\"permissions\" Multiplicity=\"*\" /></Association><Association Name=\"User_registeredDevices\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredDevices\" Multiplicity=\"*\" /></Association><Association Name=\"User_ownedDevices\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"ownedDevices\" Multiplicity=\"*\" /></Association><Association Name=\"Application_appLocalizedBranding\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.AppLocalizedBranding\" Role=\"appLocalizedBranding\" Multiplicity=\"*\" /></Association><Association Name=\"Application_appNonLocalizedBranding\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.AppNonLocalizedBranding\" Role=\"appNonLocalizedBranding\" Multiplicity=\"*\" /></Association><Association Name=\"Application_extensionProperties\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.ExtensionProperty\" Role=\"extensionProperties\" Multiplicity=\"*\" /><End Type=\"Microsof",
					"t.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /></Association><Association Name=\"Device_registeredOwners\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredOwners\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Device\" Role=\"Device\" Multiplicity=\"*\" /></Association><Association Name=\"Device_registeredUsers\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredUsers\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Device\" Role=\"Device\" Multiplicity=\"*\" /></Association><Association Name=\"ServicePrincipal_permissions\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal\" Role=\"ServicePrincipal\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" Role=\"permissions\" Multiplicity=\"*\" /></Association><EntityContainer Name=\"DirectoryDataService\" m:IsDefaultEntityContainer=\"true\"><EntitySet Name=\"appLocalizedBranding\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.AppLocalizedBranding\" /><EntitySet Name=\"appNonLocalizedBranding\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.AppNonLocalizedBranding\" /><EntitySet Name=\"directoryObjects\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><EntitySet Name=\"permissions\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" /><EntitySet Name=\"subscribedSkus\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.SubscribedSku\" /><FunctionImport Name=\"checkMemberGroups\" ReturnType=\"Collection(Edm.String)\" IsBindable=\"true\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"DirectoryObject\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><Parameter Name=\"groupIds\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /></FunctionImport><FunctionImport Name=\"getMemberGroups\" ReturnType=\"Collection(Edm.String)\" IsBindable=\"true\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"DirectoryObject\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><Parameter Name=\"securityEnabledOnly\" Type=\"Edm.Boolean\" /></FunctionImport><FunctionImport Name=\"assignLicense\" ReturnType=\"Microsoft.WindowsAzure.ActiveDirectory.User\" IsBindable=\"true\" EntitySet=\"directoryObjects\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"User\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" /><Parameter Name=\"addLicenses\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedLicense)\" Nullable=\"false\" /><Parameter Name=\"removeLicenses\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" /></FunctionImport><FunctionImport Name=\"isMemberOf\" ReturnType=\"Edm.Boolean\"><Parameter Name=\"groupId\" Type=\"Edm.String\" /><Parameter Name=\"memberId\" Type=\"Edm.String\" /></FunctionImport><AssociationSet Name=\"Application_appLocalizedBranding_appLocalizedBranding\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appLocalizedBranding\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"appLocalizedBranding\" EntitySet=\"appLocalizedBranding\" /></AssociationSet><AssociationSet Name=\"Application_appNonLocalizedBranding_appNonLocalizedBranding\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appNonLocalizedBranding\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"appNonLocalizedBranding\" EntitySet=\"appNonLocalizedBranding\" /></AssociationSet><AssociationSet Name=\"Application_extensionProperties_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_extensionProperties\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"extensionProperties\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"Device_registeredOwners_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredOwners\"><End Role=\"Device\" EntitySet=\"directoryObjects\" /><End Role=\"registeredOwners\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"Device_registeredUsers_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredUsers\"><End Role=\"Device\" EntitySet=\"directoryObjects\" /><End Role=\"registeredUsers\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"ServicePrincipal_permissions_permissions\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal_permissions\"><End Role=\"ServicePrincipal\" EntitySet=\"directoryObjects\" /><End Role=\"permissions\" EntitySet=\"permissions\" /></AssociationSet><AssociationSet Name=\"User_permissions_permissions\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_permissions\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"permissions\" EntitySet=\"permissions\" /></AssociationSet><AssociationSet Name=\"User_registeredDevices_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_registeredDevices\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"registeredDevices\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"User_ownedDevices_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_ownedDevices\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"ownedDevices\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_createdOnBehalfOf_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdOnBehalfOf\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"createdOnBehalfOf\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_createdObjects_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdObjects\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"createdObjects\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_manager_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_manager\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"manager\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_directReports_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_directReports\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"directReports\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_members_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_members\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"members\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_memberOf_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_memberOf\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"memberOf\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_owners_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_owners\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"owners\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_ownedObjects_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_ownedObjects\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"ownedObjects\" EntitySet=\"directoryObjects\" /></AssociationSet></EntityContainer><Annotations Target=\"Microsoft.WindowsAzure.Ac",
					"tiveDirectory.DirectoryDataService\"><ValueAnnotation Term=\"Com.Microsoft.Data.Services.Conventions.V1.UrlConventions\" String=\"KeyAsSegment\" /></Annotations></Schema></edmx:DataServices></edmx:Edmx>"
				});
			}

			// Token: 0x0600132D RID: 4909 RVA: 0x0002B3C6 File Offset: 0x000295C6
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			public static IEdmModel GetInstance()
			{
				return DirectoryDataService.GeneratedEdmModel.ParsedModel;
			}

			// Token: 0x0600132E RID: 4910 RVA: 0x0002B3D0 File Offset: 0x000295D0
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private static IEdmModel LoadModelFromString()
			{
				string concatenatedEdmxString = DirectoryDataService.GeneratedEdmModel.GetConcatenatedEdmxString();
				XmlReader xmlReader = DirectoryDataService.GeneratedEdmModel.CreateXmlReader(concatenatedEdmxString);
				IEdmModel result;
				try
				{
					result = EdmxReader.Parse(xmlReader);
				}
				finally
				{
					((IDisposable)xmlReader).Dispose();
				}
				return result;
			}

			// Token: 0x0600132F RID: 4911 RVA: 0x0002B40C File Offset: 0x0002960C
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private static XmlReader CreateXmlReader(string edmxToParse)
			{
				return XmlReader.Create(new StringReader(edmxToParse));
			}

			// Token: 0x040018A3 RID: 6307
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private const string ModelPart0 = "<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edmx\"><edmx:DataServices m:DataServiceVersion=\"3.0\" m:MaxDataServiceVersion=\"3.0\" xmlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema Namespace=\"Microsoft.WindowsAzure.ActiveDirectory\" xmlns=\"http://schemas.microsoft.com/ado/2009/11/edm\"><EntityType Name=\"DirectoryObject\" OpenType=\"true\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"objectType\" Type=\"Edm.String\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><NavigationProperty Name=\"createdOnBehalfOf\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdOnBehalfOf\" ToRole=\"createdOnBehalfOf\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"createdObjects\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdObjects\" ToRole=\"createdObjects\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"manager\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_manager\" ToRole=\"manager\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"directReports\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_directReports\" ToRole=\"directReports\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"members\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_members\" ToRole=\"members\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"memberOf\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_memberOf\" ToRole=\"memberOf\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"owners\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_owners\" ToRole=\"owners\" FromRole=\"DirectoryObject\" /><NavigationProperty Name=\"ownedObjects\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_ownedObjects\" ToRole=\"ownedObjects\" FromRole=\"DirectoryObject\" /></EntityType><EntityType Name=\"User\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"assignedLicenses\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedLicense)\" Nullable=\"false\" /><Property Name=\"assignedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedPlan)\" Nullable=\"false\" /><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"department\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"facsimileTelephoneNumber\" Type=\"Edm.String\" /><Property Name=\"givenName\" Type=\"Edm.String\" /><Property Name=\"immutableId\" Type=\"Edm.String\" /><Property Name=\"jobTitle\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mobile\" Type=\"Edm.String\" /><Property Name=\"otherMails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"passwordPolicies\" Type=\"Edm.String\" /><Property Name=\"passwordProfile\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.PasswordProfile\" /><Property Name=\"physicalDeliveryOfficeName\" Type=\"Edm.String\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"preferredLanguage\" Type=\"Edm.String\" /><Property Name=\"provisionedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisionedPlan)\" Nullable=\"false\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"streetAddress\" Type=\"Edm.String\" /><Property Name=\"surname\" Type=\"Edm.String\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"thumbnailPhoto\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"usageLocation\" Type=\"Edm.String\" /><Property Name=\"userPrincipalName\" Type=\"Edm.String\" /><Property Name=\"userType\" Type=\"Edm.String\" /><NavigationProperty Name=\"permissions\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_permissions\" ToRole=\"permissions\" FromRole=\"User\" /><NavigationProperty Name=\"registeredDevices\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_registeredDevices\" ToRole=\"registeredDevices\" FromRole=\"User\" /><NavigationProperty Name=\"ownedDevices\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.User_ownedDevices\" ToRole=\"ownedDevices\" FromRole=\"User\" /></EntityType><ComplexType Name=\"AssignedLicense\"><Property Name=\"disabledPlans\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" /><Property Name=\"skuId\" Type=\"Edm.Guid\" /></ComplexType><EntityType Name=\"AppLocalizedBranding\"><Key><PropertyRef Name=\"locale\" /></Key><Property Name=\"appBannerLogo\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"appBannerLogoUrl\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"heroIllustration\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"heroIllustrationUrl\" Type=\"Edm.String\" /><Property Name=\"locale\" Type=\"Edm.String\" Nullable=\"false\" /></EntityType><EntityType Name=\"AppNonLocalizedBranding\"><Key><PropertyRef Name=\"locale\" /></Key><Property Name=\"heroBackgroundColor\" Type=\"Edm.String\" /><Property Name=\"locale\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"preloadUrl\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"ExtensionProperty\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"name\" Type=\"Edm.String\" /><Property Name=\"dataType\" Type=\"Edm.String\" /><Property Name=\"targetObjects\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /></EntityType><EntityType Name=\"Application\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"allowActAsForAllClients\" Type=\"Edm.Boolean\" /><Property Name=\"appId\" Type=\"Edm.Guid\" /><Property Name=\"availableToOtherTenants\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"errorUrl\" Type=\"Edm.String\" /><Property Name=\"groupMembershipClaims\" Type=\"Edm.String\" /><Property Name=\"homepage\" Type=\"Edm.String\" /><Property Name=\"identifierUris\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"keyCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.KeyCredential)\" Nullable=\"false\" /><Property Name=\"mainLogo\" Type=\"Edm.Stream\" Nullable=\"false\" /><Property Name=\"logoutUrl\" Type=\"Edm.String\" /><Property Name=\"passwordCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.PasswordCredential)\" Nullable=\"false\" /><Property Name=\"publicClient\" Type=\"Edm.Boolean\" /><Property Name=\"replyUrls\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"samlMetadataUrl\" Type=\"Edm.String\" /><NavigationProperty Name=\"appLocalizedBranding\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appLocalizedBranding\" ToRole=\"appLocalizedBranding\" FromRole=\"Application\" /><NavigationProperty Name=\"appNonLocalizedBranding\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appNonLocalizedBranding\" ToRole=\"appNonLocalizedBranding\" FromRole=\"Application\" /><NavigationProperty Name=\"extensionProperties\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Application_extensionProperties\" ToRole=\"extensionProperties\" FromRole=\"Application\" /></EntityType><ComplexType Name=\"KeyCredential\"><Pr";

			// Token: 0x040018A4 RID: 6308
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private const string ModelPart1 = "operty Name=\"customKeyIdentifier\" Type=\"Edm.Binary\" /><Property Name=\"endDate\" Type=\"Edm.DateTime\" /><Property Name=\"keyId\" Type=\"Edm.Guid\" /><Property Name=\"startDate\" Type=\"Edm.DateTime\" /><Property Name=\"type\" Type=\"Edm.String\" /><Property Name=\"usage\" Type=\"Edm.String\" /><Property Name=\"value\" Type=\"Edm.Binary\" /></ComplexType><ComplexType Name=\"PasswordCredential\"><Property Name=\"customKeyIdentifier\" Type=\"Edm.Binary\" /><Property Name=\"endDate\" Type=\"Edm.DateTime\" /><Property Name=\"keyId\" Type=\"Edm.Guid\" /><Property Name=\"startDate\" Type=\"Edm.DateTime\" /><Property Name=\"value\" Type=\"Edm.String\" /></ComplexType><EntityType Name=\"Contact\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"department\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"facsimileTelephoneNumber\" Type=\"Edm.String\" /><Property Name=\"givenName\" Type=\"Edm.String\" /><Property Name=\"jobTitle\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mobile\" Type=\"Edm.String\" /><Property Name=\"physicalDeliveryOfficeName\" Type=\"Edm.String\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"streetAddress\" Type=\"Edm.String\" /><Property Name=\"surname\" Type=\"Edm.String\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"thumbnailPhoto\" Type=\"Edm.Stream\" Nullable=\"false\" /></EntityType><ComplexType Name=\"ProvisioningError\"><Property Name=\"errorDetail\" Type=\"Edm.String\" /><Property Name=\"resolved\" Type=\"Edm.Boolean\" /><Property Name=\"service\" Type=\"Edm.String\" /><Property Name=\"timestamp\" Type=\"Edm.DateTime\" /></ComplexType><EntityType Name=\"Device\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"alternativeSecurityIds\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AlternativeSecurityId)\" Nullable=\"false\" /><Property Name=\"approximateLastLogonTimestamp\" Type=\"Edm.DateTime\" /><Property Name=\"deviceId\" Type=\"Edm.Guid\" /><Property Name=\"deviceObjectVersion\" Type=\"Edm.Int32\" /><Property Name=\"deviceOSType\" Type=\"Edm.String\" /><Property Name=\"deviceOSVersion\" Type=\"Edm.String\" /><Property Name=\"devicePhysicalIds\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"exchangeActiveSyncId\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"isCompliant\" Type=\"Edm.Boolean\" /><Property Name=\"isManaged\" Type=\"Edm.Boolean\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><NavigationProperty Name=\"registeredOwners\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredOwners\" ToRole=\"registeredOwners\" FromRole=\"Device\" /><NavigationProperty Name=\"registeredUsers\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredUsers\" ToRole=\"registeredUsers\" FromRole=\"Device\" /></EntityType><ComplexType Name=\"AlternativeSecurityId\"><Property Name=\"type\" Type=\"Edm.Int32\" /><Property Name=\"identityProvider\" Type=\"Edm.String\" /><Property Name=\"key\" Type=\"Edm.Binary\" /></ComplexType><EntityType Name=\"DeviceConfiguration\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"publicIssuerCertificates\" Type=\"Collection(Edm.Binary)\" Nullable=\"false\" /><Property Name=\"cloudPublicIssuerCertificates\" Type=\"Collection(Edm.Binary)\" Nullable=\"false\" /><Property Name=\"registrationQuota\" Type=\"Edm.Int32\" /><Property Name=\"maximumRegistrationInactivityPeriod\" Type=\"Edm.Int32\" /></EntityType><EntityType Name=\"DirectoryLinkChange\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"associationType\" Type=\"Edm.String\" /><Property Name=\"sourceObjectId\" Type=\"Edm.String\" /><Property Name=\"sourceObjectType\" Type=\"Edm.String\" /><Property Name=\"sourceObjectUri\" Type=\"Edm.String\" /><Property Name=\"targetObjectId\" Type=\"Edm.String\" /><Property Name=\"targetObjectType\" Type=\"Edm.String\" /><Property Name=\"targetObjectUri\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"Group\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"lastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"mail\" Type=\"Edm.String\" /><Property Name=\"mailNickname\" Type=\"Edm.String\" /><Property Name=\"mailEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"proxyAddresses\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"securityEnabled\" Type=\"Edm.Boolean\" /></EntityType><EntityType Name=\"Role\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"isSystem\" Type=\"Edm.Boolean\" /><Property Name=\"roleDisabled\" Type=\"Edm.Boolean\" /></EntityType><EntityType Name=\"RoleTemplate\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"description\" Type=\"Edm.String\" /><Property Name=\"displayName\" Type=\"Edm.String\" /></EntityType><EntityType Name=\"ServicePrincipal\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"accountEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"appId\" Type=\"Edm.Guid\" /><Property Name=\"appOwnerTenantId\" Type=\"Edm.Guid\" /><Property Name=\"authenticationPolicy\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipalAuthenticationPolicy\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"errorUrl\" Type=\"Edm.String\" /><Property Name=\"homepage\" Type=\"Edm.String\" /><Property Name=\"keyCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.KeyCredential)\" Nullable=\"false\" /><Property Name=\"logoutUrl\" Type=\"Edm.String\" /><Property Name=\"passwordCredentials\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.PasswordCredential)\" Nullable=\"false\" /><Property Name=\"preferredTokenSigningKeyThumbprint\" Type=\"Edm.String\" /><Property Name=\"publisherName\" Type=\"Edm.String\" /><Property Name=\"replyUrls\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"samlMetadataUrl\" Type=\"Edm.String\" /><Property Name=\"servicePrincipalNames\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"tags\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><NavigationProperty Name=\"permissions\" Relationship=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal_permissions\" ToRole=\"permissions\" FromRole=\"ServicePrincipal\" /></EntityType><ComplexType Name=\"ServicePrincipalAuthenticationPolicy\"><Property Name=\"defaultPolicy\" Type=\"Edm.String\" /><Property Name=\"allowedPolicies\" Type=\"Collecti";

			// Token: 0x040018A5 RID: 6309
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private const string ModelPart2 = "on(Edm.String)\" Nullable=\"false\" /></ComplexType><EntityType Name=\"TenantDetail\" BaseType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" OpenType=\"true\"><Property Name=\"assignedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedPlan)\" Nullable=\"false\" /><Property Name=\"city\" Type=\"Edm.String\" /><Property Name=\"companyLastDirSyncTime\" Type=\"Edm.DateTime\" /><Property Name=\"country\" Type=\"Edm.String\" /><Property Name=\"countryLetterCode\" Type=\"Edm.String\" /><Property Name=\"dirSyncEnabled\" Type=\"Edm.Boolean\" /><Property Name=\"displayName\" Type=\"Edm.String\" /><Property Name=\"marketingNotificationEmails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"postalCode\" Type=\"Edm.String\" /><Property Name=\"preferredLanguage\" Type=\"Edm.String\" /><Property Name=\"provisionedPlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisionedPlan)\" Nullable=\"false\" /><Property Name=\"provisioningErrors\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ProvisioningError)\" Nullable=\"false\" /><Property Name=\"state\" Type=\"Edm.String\" /><Property Name=\"street\" Type=\"Edm.String\" /><Property Name=\"technicalNotificationMails\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /><Property Name=\"telephoneNumber\" Type=\"Edm.String\" /><Property Name=\"tenantType\" Type=\"Edm.String\" /><Property Name=\"verifiedDomains\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.VerifiedDomain)\" Nullable=\"false\" /></EntityType><ComplexType Name=\"AssignedPlan\"><Property Name=\"assignedTimestamp\" Type=\"Edm.DateTime\" /><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"service\" Type=\"Edm.String\" /><Property Name=\"servicePlanId\" Type=\"Edm.Guid\" /></ComplexType><ComplexType Name=\"ProvisionedPlan\"><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"provisioningStatus\" Type=\"Edm.String\" /><Property Name=\"service\" Type=\"Edm.String\" /></ComplexType><ComplexType Name=\"VerifiedDomain\"><Property Name=\"capabilities\" Type=\"Edm.String\" /><Property Name=\"default\" Type=\"Edm.Boolean\" /><Property Name=\"id\" Type=\"Edm.String\" /><Property Name=\"initial\" Type=\"Edm.Boolean\" /><Property Name=\"name\" Type=\"Edm.String\" /><Property Name=\"type\" Type=\"Edm.String\" /></ComplexType><ComplexType Name=\"PasswordProfile\"><Property Name=\"password\" Type=\"Edm.String\" /><Property Name=\"forceChangePasswordNextLogin\" Type=\"Edm.Boolean\" /></ComplexType><EntityType Name=\"Permission\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"clientId\" Type=\"Edm.String\" /><Property Name=\"consentType\" Type=\"Edm.String\" /><Property Name=\"expiryTime\" Type=\"Edm.DateTime\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"principalId\" Type=\"Edm.String\" /><Property Name=\"resourceId\" Type=\"Edm.String\" /><Property Name=\"scope\" Type=\"Edm.String\" /><Property Name=\"startTime\" Type=\"Edm.DateTime\" /></EntityType><EntityType Name=\"SubscribedSku\"><Key><PropertyRef Name=\"objectId\" /></Key><Property Name=\"capabilityStatus\" Type=\"Edm.String\" /><Property Name=\"consumedUnits\" Type=\"Edm.Int32\" /><Property Name=\"objectId\" Type=\"Edm.String\" Nullable=\"false\" /><Property Name=\"prepaidUnits\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.LicenseUnitsDetail\" /><Property Name=\"servicePlans\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.ServicePlanInfo)\" Nullable=\"false\" /><Property Name=\"skuId\" Type=\"Edm.Guid\" /><Property Name=\"skuPartNumber\" Type=\"Edm.String\" /></EntityType><ComplexType Name=\"LicenseUnitsDetail\"><Property Name=\"enabled\" Type=\"Edm.Int32\" /><Property Name=\"suspended\" Type=\"Edm.Int32\" /><Property Name=\"warning\" Type=\"Edm.Int32\" /></ComplexType><ComplexType Name=\"ServicePlanInfo\"><Property Name=\"servicePlanId\" Type=\"Edm.Guid\" /><Property Name=\"servicePlanName\" Type=\"Edm.String\" /></ComplexType><Association Name=\"DirectoryObject_createdOnBehalfOf\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"createdOnBehalfOf\" Multiplicity=\"0..1\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_createdObjects\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"createdObjects\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_manager\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"manager\" Multiplicity=\"0..1\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_directReports\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"directReports\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_members\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"members\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_memberOf\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"memberOf\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_owners\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"owners\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"DirectoryObject_ownedObjects\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"ownedObjects\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"DirectoryObject\" Multiplicity=\"*\" /></Association><Association Name=\"User_permissions\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" Role=\"permissions\" Multiplicity=\"*\" /></Association><Association Name=\"User_registeredDevices\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredDevices\" Multiplicity=\"*\" /></Association><Association Name=\"User_ownedDevices\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" Role=\"User\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"ownedDevices\" Multiplicity=\"*\" /></Association><Association Name=\"Application_appLocalizedBranding\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.AppLocalizedBranding\" Role=\"appLocalizedBranding\" Multiplicity=\"*\" /></Association><Association Name=\"Application_appNonLocalizedBranding\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.AppNonLocalizedBranding\" Role=\"appNonLocalizedBranding\" Multiplicity=\"*\" /></Association><Association Name=\"Application_extensionProperties\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.ExtensionProperty\" Role=\"extensionProperties\" Multiplicity=\"*\" /><End Type=\"Microsof";

			// Token: 0x040018A6 RID: 6310
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private const string ModelPart3 = "t.WindowsAzure.ActiveDirectory.Application\" Role=\"Application\" Multiplicity=\"*\" /></Association><Association Name=\"Device_registeredOwners\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredOwners\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Device\" Role=\"Device\" Multiplicity=\"*\" /></Association><Association Name=\"Device_registeredUsers\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" Role=\"registeredUsers\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Device\" Role=\"Device\" Multiplicity=\"*\" /></Association><Association Name=\"ServicePrincipal_permissions\"><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal\" Role=\"ServicePrincipal\" Multiplicity=\"*\" /><End Type=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" Role=\"permissions\" Multiplicity=\"*\" /></Association><EntityContainer Name=\"DirectoryDataService\" m:IsDefaultEntityContainer=\"true\"><EntitySet Name=\"appLocalizedBranding\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.AppLocalizedBranding\" /><EntitySet Name=\"appNonLocalizedBranding\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.AppNonLocalizedBranding\" /><EntitySet Name=\"directoryObjects\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><EntitySet Name=\"permissions\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.Permission\" /><EntitySet Name=\"subscribedSkus\" EntityType=\"Microsoft.WindowsAzure.ActiveDirectory.SubscribedSku\" /><FunctionImport Name=\"checkMemberGroups\" ReturnType=\"Collection(Edm.String)\" IsBindable=\"true\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"DirectoryObject\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><Parameter Name=\"groupIds\" Type=\"Collection(Edm.String)\" Nullable=\"false\" /></FunctionImport><FunctionImport Name=\"getMemberGroups\" ReturnType=\"Collection(Edm.String)\" IsBindable=\"true\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"DirectoryObject\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject\" /><Parameter Name=\"securityEnabledOnly\" Type=\"Edm.Boolean\" /></FunctionImport><FunctionImport Name=\"assignLicense\" ReturnType=\"Microsoft.WindowsAzure.ActiveDirectory.User\" IsBindable=\"true\" EntitySet=\"directoryObjects\" m:IsAlwaysBindable=\"true\"><Parameter Name=\"User\" Type=\"Microsoft.WindowsAzure.ActiveDirectory.User\" /><Parameter Name=\"addLicenses\" Type=\"Collection(Microsoft.WindowsAzure.ActiveDirectory.AssignedLicense)\" Nullable=\"false\" /><Parameter Name=\"removeLicenses\" Type=\"Collection(Edm.Guid)\" Nullable=\"false\" /></FunctionImport><FunctionImport Name=\"isMemberOf\" ReturnType=\"Edm.Boolean\"><Parameter Name=\"groupId\" Type=\"Edm.String\" /><Parameter Name=\"memberId\" Type=\"Edm.String\" /></FunctionImport><AssociationSet Name=\"Application_appLocalizedBranding_appLocalizedBranding\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appLocalizedBranding\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"appLocalizedBranding\" EntitySet=\"appLocalizedBranding\" /></AssociationSet><AssociationSet Name=\"Application_appNonLocalizedBranding_appNonLocalizedBranding\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_appNonLocalizedBranding\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"appNonLocalizedBranding\" EntitySet=\"appNonLocalizedBranding\" /></AssociationSet><AssociationSet Name=\"Application_extensionProperties_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Application_extensionProperties\"><End Role=\"Application\" EntitySet=\"directoryObjects\" /><End Role=\"extensionProperties\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"Device_registeredOwners_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredOwners\"><End Role=\"Device\" EntitySet=\"directoryObjects\" /><End Role=\"registeredOwners\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"Device_registeredUsers_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.Device_registeredUsers\"><End Role=\"Device\" EntitySet=\"directoryObjects\" /><End Role=\"registeredUsers\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"ServicePrincipal_permissions_permissions\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.ServicePrincipal_permissions\"><End Role=\"ServicePrincipal\" EntitySet=\"directoryObjects\" /><End Role=\"permissions\" EntitySet=\"permissions\" /></AssociationSet><AssociationSet Name=\"User_permissions_permissions\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_permissions\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"permissions\" EntitySet=\"permissions\" /></AssociationSet><AssociationSet Name=\"User_registeredDevices_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_registeredDevices\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"registeredDevices\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"User_ownedDevices_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.User_ownedDevices\"><End Role=\"User\" EntitySet=\"directoryObjects\" /><End Role=\"ownedDevices\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_createdOnBehalfOf_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdOnBehalfOf\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"createdOnBehalfOf\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_createdObjects_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_createdObjects\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"createdObjects\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_manager_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_manager\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"manager\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_directReports_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_directReports\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"directReports\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_members_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_members\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"members\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_memberOf_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_memberOf\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"memberOf\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_owners_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_owners\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"owners\" EntitySet=\"directoryObjects\" /></AssociationSet><AssociationSet Name=\"DirectoryObject_ownedObjects_directoryObjects\" Association=\"Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject_ownedObjects\"><End Role=\"DirectoryObject\" EntitySet=\"directoryObjects\" /><End Role=\"ownedObjects\" EntitySet=\"directoryObjects\" /></AssociationSet></EntityContainer><Annotations Target=\"Microsoft.WindowsAzure.Ac";

			// Token: 0x040018A7 RID: 6311
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private const string ModelPart4 = "tiveDirectory.DirectoryDataService\"><ValueAnnotation Term=\"Com.Microsoft.Data.Services.Conventions.V1.UrlConventions\" String=\"KeyAsSegment\" /></Annotations></Schema></edmx:DataServices></edmx:Edmx>";

			// Token: 0x040018A8 RID: 6312
			[GeneratedCode("System.Data.Services.Design", "1.0.0")]
			private static IEdmModel ParsedModel = DirectoryDataService.GeneratedEdmModel.LoadModelFromString();
		}
	}
}
