using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OWAExtensionDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004D8E File Offset: 0x00002F8E
		public bool IsDebug
		{
			get
			{
				return this.isDebug;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004D96 File Offset: 0x00002F96
		public string RawMasterTableXml
		{
			get
			{
				return this.rawMasterTableXml;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004D9E File Offset: 0x00002F9E
		public string RawOrgMasterTableXml
		{
			get
			{
				return this.rawOrgMasterTableXml;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public OWAExtensionDataProvider(string domain, IRecipientSession adRecipientSession, ADSessionSettings adSessionSettings, bool isUserScope, ADUser mailboxOwner, string action, bool isDebug) : base(adSessionSettings, mailboxOwner, action)
		{
			this.domain = domain;
			this.adRecipientSession = adRecipientSession;
			this.isUserScope = isUserScope;
			this.isDebug = isDebug;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004DD1 File Offset: 0x00002FD1
		internal OWAExtensionDataProvider()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000532C File Offset: 0x0000352C
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			AppId owaExtensionId = rootId as AppId;
			if (sortBy != null)
			{
				throw new NotSupportedException("sortBy");
			}
			if (rootId != null && owaExtensionId == null)
			{
				throw new NotSupportedException("rootId");
			}
			InstalledExtensionTable installedList = null;
			OWAExtensionDataProvider.RunAction(delegate
			{
				installedList = InstalledExtensionTable.CreateInstalledExtensionTable(this.domain, this.isUserScope, null, this.MailboxSession);
			});
			if (owaExtensionId == null || (owaExtensionId.DisplayName == null && owaExtensionId.AppIdValue == null))
			{
				List<ExtensionData> extensions = null;
				OWAExtensionDataProvider.RunAction(delegate
				{
					extensions = installedList.GetExtensions(null, false, this.isDebug, out this.rawOrgMasterTableXml);
					this.rawMasterTableXml = (this.isDebug ? installedList.MasterTableXml.InnerXml : string.Empty);
				});
				foreach (ExtensionData extensionData2 in extensions)
				{
					yield return (T)((object)this.ConvertStoreObjectToPresentationObject(extensionData2));
				}
			}
			else if (!string.IsNullOrEmpty(owaExtensionId.AppIdValue))
			{
				ExtensionData extensionData = null;
				OWAExtensionDataProvider.RunAction(delegate
				{
					installedList.TryGetExtension(owaExtensionId.AppIdValue, out extensionData, this.isDebug, out this.rawOrgMasterTableXml);
					this.rawMasterTableXml = (this.isDebug ? installedList.MasterTableXml.InnerXml : string.Empty);
				});
				if (extensionData != null)
				{
					yield return (T)((object)this.ConvertStoreObjectToPresentationObject(extensionData));
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000054D4 File Offset: 0x000036D4
		protected override void InternalSave(ConfigurableObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			App owaExtension = instance as App;
			if (owaExtension == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			OWAExtensionDataProvider.RunAction(delegate
			{
				using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(this.domain, this.isUserScope, null, this.MailboxSession))
				{
					switch (owaExtension.ObjectState)
					{
					case ObjectState.New:
						if (!owaExtension.IsDownloadOnly)
						{
							installedExtensionTable.InstallExtension(owaExtension.GetExtensionDataForInstall(this.adRecipientSession), false);
						}
						break;
					case ObjectState.Changed:
						if (this.isUserScope)
						{
							installedExtensionTable.ConfigureUserExtension(owaExtension.AppId, owaExtension.Enabled);
						}
						else
						{
							OrgApp orgApp = instance as OrgApp;
							if (orgApp == null)
							{
								throw new NotSupportedException("Save: " + instance.GetType().FullName);
							}
							installedExtensionTable.ConfigureOrgExtension(orgApp.AppId, orgApp.Enabled, orgApp.DefaultStateForUser == DefaultStateForUser.AlwaysEnabled, orgApp.DefaultStateForUser == DefaultStateForUser.Enabled, orgApp.ProvidedTo, OrgApp.ConvertPresentationFormatToWireUserList(this.adRecipientSession, orgApp.UserList));
						}
						break;
					}
				}
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000568C File Offset: 0x0000388C
		protected override void InternalDelete(ConfigurableObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			App owaExtension = instance as App;
			if (owaExtension == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			OWAExtensionDataProvider.RunAction(delegate
			{
				using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(this.domain, this.isUserScope, null, this.MailboxSession))
				{
					if (this.isUserScope)
					{
						if (ExtensionInstallScope.User != owaExtension.Scope)
						{
							throw new OwaExtensionOperationException(Strings.ErrorUninstallProvidedExtension(owaExtension.DisplayName));
						}
						if (this.TryRemovePerExtensionFai(owaExtension.AppId, owaExtension.AppVersion))
						{
							installedExtensionTable.UninstallExtension(owaExtension.AppId);
						}
						else
						{
							installedExtensionTable.ConfigureUserExtension(owaExtension.AppId, false);
						}
					}
					else
					{
						if (ExtensionInstallScope.Default == owaExtension.Scope)
						{
							throw new OwaExtensionOperationException(Strings.ErrorUninstallDefaultExtension(owaExtension.DisplayName));
						}
						installedExtensionTable.UninstallExtension(owaExtension.AppId);
					}
				}
			});
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000056F4 File Offset: 0x000038F4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OWAExtensionDataProvider>(this);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000056FC File Offset: 0x000038FC
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005708 File Offset: 0x00003908
		internal static void RunAction(Action action)
		{
			Exception ex = InstalledExtensionTable.RunClientExtensionAction(action);
			if (ex != null)
			{
				throw (ex is LocalizedException) ? ex : new OwaExtensionOperationException(new LocalizedString(ex.Message), ex);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000573C File Offset: 0x0000393C
		private bool TryRemovePerExtensionFai(string extensionId, string version)
		{
			StoreObjectId extensionFolderId = ExtensionPackageManager.GetExtensionFolderId(base.MailboxSession);
			bool result;
			using (UserConfiguration folderConfiguration = UserConfigurationHelper.GetFolderConfiguration(base.MailboxSession, extensionFolderId, ExtensionPackageManager.GetFaiName(extensionId, version), UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, false, false))
			{
				if (folderConfiguration != null && OperationResult.Succeeded != base.MailboxSession.UserConfigurationManager.DeleteFolderConfigurations(extensionFolderId, new string[]
				{
					ExtensionPackageManager.GetFaiName(extensionId, version)
				}))
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000057B8 File Offset: 0x000039B8
		private App ConvertStoreObjectToPresentationObject(ExtensionData extensionData)
		{
			DefaultStateForUser? defaultStateForUser = new DefaultStateForUser?(extensionData.IsMandatory ? DefaultStateForUser.AlwaysEnabled : (extensionData.IsEnabledByDefault ? DefaultStateForUser.Enabled : DefaultStateForUser.Disabled));
			Uri uri = extensionData.IconURL;
			string uriString;
			Exception ex;
			if (null != uri && ExtensionInstallScope.Default == extensionData.Scope.GetValueOrDefault() && DefaultExtensionTable.TryConvertToCompleteUrl(base.MailboxSession.MailboxOwner, uri.OriginalString, extensionData.ExtensionId, out uriString, out ex))
			{
				uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
			}
			if (this.isUserScope)
			{
				return new App((ExtensionInstallScope.User == extensionData.Scope.GetValueOrDefault()) ? null : defaultStateForUser, extensionData.MarketplaceAssetID, extensionData.MarketplaceContentMarket, extensionData.ProviderName, uri, extensionData.ExtensionId, extensionData.VersionAsString, extensionData.Type, extensionData.Scope, extensionData.RequestedCapabilities, extensionData.DisplayName, extensionData.Description, extensionData.Enabled, extensionData.Manifest.OuterXml, base.MailboxOwner.ObjectId, extensionData.Etoken, extensionData.EtokenData, extensionData.AppStatus);
			}
			return new OrgApp(defaultStateForUser, extensionData.ProvidedTo, OrgApp.ConvertWireUserListToPresentationFormat(this.adRecipientSession, extensionData.SpecificUsers), extensionData.MarketplaceAssetID, extensionData.MarketplaceContentMarket, extensionData.ProviderName, uri, extensionData.ExtensionId, extensionData.VersionAsString, extensionData.Type, extensionData.Scope, extensionData.RequestedCapabilities, extensionData.DisplayName, extensionData.Description, extensionData.Enabled, extensionData.Manifest.OuterXml, extensionData.Etoken, extensionData.EtokenData, extensionData.AppStatus, base.MailboxOwner.ObjectId);
		}

		// Token: 0x0400003D RID: 61
		private readonly bool isUserScope;

		// Token: 0x0400003E RID: 62
		private readonly string domain;

		// Token: 0x0400003F RID: 63
		private readonly bool isDebug;

		// Token: 0x04000040 RID: 64
		private string rawMasterTableXml;

		// Token: 0x04000041 RID: 65
		private string rawOrgMasterTableXml;

		// Token: 0x04000042 RID: 66
		private readonly IRecipientSession adRecipientSession;
	}
}
