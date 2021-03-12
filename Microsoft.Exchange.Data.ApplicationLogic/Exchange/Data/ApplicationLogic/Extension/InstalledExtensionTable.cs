using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.ApplicationLogic.Owa;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000FD RID: 253
	internal class InstalledExtensionTable : DisposeTrackableBase
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002A070 File Offset: 0x00028270
		internal static bool IsMultiTenancyEnabled
		{
			get
			{
				return InstalledExtensionTable.isMultiTenancyEnabled.Member;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0002A07C File Offset: 0x0002827C
		internal SafeXmlDocument MasterTableXml
		{
			get
			{
				this.LoadXML();
				return this.masterTableXml;
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002A08A File Offset: 0x0002828A
		private InstalledExtensionTable(bool retrieveOnly1_0)
		{
			this.retrieveOnly1_0 = retrieveOnly1_0;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002A0A4 File Offset: 0x000282A4
		private static int UpdateCheckFrequencySeconds
		{
			get
			{
				if (InstalledExtensionTable.updateCheckFrequencySeconds == 0)
				{
					string text = ConfigurationManager.AppSettings["UpdateCheckFrequencySeconds"];
					int num = 0;
					if (text != null && int.TryParse(text, out num) && num > 0)
					{
						InstalledExtensionTable.updateCheckFrequencySeconds = num;
					}
					else
					{
						InstalledExtensionTable.updateCheckFrequencySeconds = 259200;
					}
					InstalledExtensionTable.Tracer.TraceDebug<int>(0L, "Agave Update Check Frequency: {0} seconds", InstalledExtensionTable.updateCheckFrequencySeconds);
				}
				return InstalledExtensionTable.updateCheckFrequencySeconds;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002A108 File Offset: 0x00028308
		internal Dictionary<string, string> RequestData
		{
			get
			{
				return this.requestData;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002A110 File Offset: 0x00028310
		internal static InstalledExtensionTable CreateInstalledExtensionTable(string domain, bool isUserScope, OrgEmptyMasterTableCache orgEmptyMasterTableCache, MailboxSession mailboxSession)
		{
			return InstalledExtensionTable.CreateInstalledExtensionTable(domain, isUserScope, orgEmptyMasterTableCache, null, mailboxSession, false);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002A120 File Offset: 0x00028320
		internal static InstalledExtensionTable CreateInstalledExtensionTable(string domain, bool isUserScope, OrgEmptyMasterTableCache orgEmptyMasterTableCache, ExtensionsCache extensionsCache, MailboxSession mailboxSession, bool retrieveOnly1_0 = false)
		{
			StoreId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			InstalledExtensionTable installedExtensionTable = new InstalledExtensionTable(retrieveOnly1_0);
			bool flag = false;
			try
			{
				installedExtensionTable.masterTable = UserConfigurationHelper.GetFolderConfiguration(mailboxSession, defaultFolderId, "ExtensionMasterTable", UserConfigurationTypes.XML, true, false);
				if (isUserScope)
				{
					installedExtensionTable.userId = InstalledExtensionTable.GetWireUserId(InstalledExtensionTable.IsMultiTenancyEnabled ? DirectoryHelper.ReadADRecipient(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.IsArchive, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)) : null, mailboxSession.MailboxOwner.ObjectId);
				}
				installedExtensionTable.domain = (domain ?? mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.Domain);
				installedExtensionTable.orgEmptyMasterTableCache = orgEmptyMasterTableCache;
				installedExtensionTable.isOrgMailboxSession = (orgEmptyMasterTableCache != null);
				installedExtensionTable.isUserScope = isUserScope;
				installedExtensionTable.masterTableXml = null;
				installedExtensionTable.extensionsCache = extensionsCache;
				installedExtensionTable.sessionMailboxOwner = mailboxSession.MailboxOwner;
				installedExtensionTable.sessionPreferedCulture = mailboxSession.PreferedCulture;
				installedExtensionTable.sessionClientInfoString = mailboxSession.ClientInfoString;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					installedExtensionTable.Dispose();
					installedExtensionTable = null;
				}
			}
			return installedExtensionTable;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002A240 File Offset: 0x00028440
		internal static Exception RunClientExtensionAction(Action action)
		{
			Exception result = null;
			try
			{
				action();
			}
			catch (OwaExtensionOperationException ex)
			{
				result = ex;
			}
			catch (StoragePermanentException ex2)
			{
				result = ex2;
			}
			catch (StorageTransientException ex3)
			{
				result = ex3;
			}
			return result;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002A290 File Offset: 0x00028490
		internal static Dictionary<string, ExtensionData> GetDefaultExtensions(IExchangePrincipal mailboxOwner)
		{
			if (InstalledExtensionTable.defaultExtensionTable == null)
			{
				lock (InstalledExtensionTable.defaultExtensionTableLock)
				{
					if (InstalledExtensionTable.defaultExtensionTable == null)
					{
						InstalledExtensionTable.defaultExtensionTable = new DefaultExtensionTable(mailboxOwner, "GetDefaultExtensions");
					}
				}
			}
			return InstalledExtensionTable.defaultExtensionTable.DefaultExtensions;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002A2FC File Offset: 0x000284FC
		public static string GetWireUserId(ADRawEntry adRawEntry, ADObjectId adObjectId)
		{
			if (!InstalledExtensionTable.IsMultiTenancyEnabled)
			{
				return adObjectId.ObjectGuid.ToString();
			}
			string text = adRawEntry[ADRecipientSchema.ExternalDirectoryObjectId] as string;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			InstalledExtensionTable.Tracer.TraceDebug(0L, "ExternalDirectoryObjectId is not configured for user " + adObjectId.ObjectGuid.ToString());
			return string.Empty;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002A370 File Offset: 0x00028570
		public void InstallExtension(ExtensionData extensionData, bool overwriteConflict)
		{
			if (extensionData.Type == ExtensionType.MarketPlace && !this.isOrgMailboxSession)
			{
				InstalledExtensionTable.ValidateAndRemoveManifestSignature(extensionData.Manifest, extensionData.ExtensionId, true);
			}
			else
			{
				InstalledExtensionTable.Tracer.TraceDebug<ExtensionType?, string>((long)this.GetHashCode(), "Skipping Signature Validation and Removal as this is only done for Marketplace Apps. Type: {0}, Id: {1}", extensionData.Type, extensionData.ExtensionId);
			}
			ExtensionData.ValidateManifestSize((long)extensionData.Manifest.OuterXml.Length, true);
			if (this.isUserScope || this.isOrgMailboxSession)
			{
				this.AddExtension(extensionData, overwriteConflict);
				this.SaveXML();
				return;
			}
			OrgExtensionTable.SetOrgExtension(this.domain, 0, null, extensionData);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002A420 File Offset: 0x00028620
		internal void AddExtension(ExtensionData extensionData, bool overwriteConflict)
		{
			string text = ExtensionDataHelper.FormatExtensionId(extensionData.ExtensionId);
			if (extensionData.Version == null)
			{
				throw new OwaExtensionOperationException(Strings.ErrorExtensionVersionInvalid);
			}
			bool flag = false;
			ExtensionData.ParseEtoken(extensionData.Etoken, extensionData.ExtensionId, this.domain, extensionData.MarketplaceAssetID, false, this.isOrgMailboxSession);
			ExtensionData extensionData2;
			if (this.TryGetExtension(text, out extensionData2))
			{
				if (ExtensionInstallScope.Default == extensionData2.Scope)
				{
					throw new OwaExtensionOperationException(Strings.ErrorCantOverwriteDefaultExtension);
				}
				if (overwriteConflict)
				{
					if (ExtensionInstallScope.User == extensionData2.Scope)
					{
						flag = true;
					}
				}
				else if (this.isUserScope && ExtensionInstallScope.Organization == extensionData2.Scope)
				{
					if (ExtensionType.MarketPlace != extensionData.Type)
					{
						throw new OwaExtensionOperationException(Strings.ErrorExtensionWithIdAlreadyInstalledForOrg);
					}
					throw new OwaExtensionOperationException(Strings.ErrorExtensionAlreadyInstalledForOrg);
				}
				else
				{
					if (extensionData2.Version == null)
					{
						throw new OwaExtensionOperationException(Strings.ErrorExtensionUnableToUpgrade(extensionData.DisplayName));
					}
					if (extensionData.Version >= extensionData2.Version)
					{
						InstalledExtensionTable.Tracer.TraceInformation(this.GetHashCode(), 0L, string.Format("Removing the old version extension {0} {1}.", extensionData.DisplayName, extensionData2.VersionAsString));
						flag = true;
					}
					else
					{
						if (ExtensionType.MarketPlace != extensionData.Type)
						{
							throw new OwaExtensionOperationException(Strings.ErrorExtensionWithIdAlreadyInstalled);
						}
						throw new OwaExtensionOperationException(Strings.ErrorExtensionAlreadyInstalled);
					}
				}
			}
			this.ReplaceExtension(extensionData, flag ? extensionData2 : null, text);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002A5DB File Offset: 0x000287DB
		public void UninstallExtension(string id)
		{
			if (this.isUserScope || this.isOrgMailboxSession)
			{
				this.RemoveExtension(id, true, null);
				return;
			}
			OrgExtensionTable.SetOrgExtension(this.domain, 1, id, null);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002A698 File Offset: 0x00028898
		internal void RemoveExtension(string id, bool shouldSave, Version schemaVersion = null)
		{
			string formattedId = ExtensionDataHelper.FormatExtensionId(id);
			Guid guid;
			if (!GuidHelper.TryParseGuid(formattedId, out guid))
			{
				throw new OwaExtensionOperationException(Strings.ErrorReasonInvalidID);
			}
			if (!this.TryConfigureExistingRecord(formattedId, delegate(XmlNode extensionNode)
			{
				ExtensionData extensionData;
				if (this.TryGetProvidedExtension(formattedId, out extensionData))
				{
					throw new OwaExtensionOperationException(Strings.ErrorCannotUninstallProvidedExtension(formattedId));
				}
				if (schemaVersion != null)
				{
					XmlNode xmlNode = extensionNode.SelectSingleNode("manifest");
					string text;
					string input;
					Version v;
					if (xmlNode == null || !ExtensionDataHelper.TryGetOfficeAppSchemaInfo(xmlNode, "http://schemas.microsoft.com/office/appforoffice/", out text, out input) || !Version.TryParse(input, out v) || schemaVersion != v)
					{
						return;
					}
				}
				extensionNode.ParentNode.RemoveChild(extensionNode);
			}, shouldSave))
			{
				throw new OwaExtensionOperationException(Strings.ErrorExtensionNotFound(formattedId));
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002A778 File Offset: 0x00028978
		public void DisableExtension(string id, DisableReasonType disableReason)
		{
			string text = ExtensionDataHelper.FormatExtensionId(id);
			Guid guid;
			if (!GuidHelper.TryParseGuid(text, out guid))
			{
				throw new OwaExtensionOperationException(Strings.ErrorReasonInvalidID);
			}
			this.ConfigureLocalExtension(text, false, delegate(XmlNode extensionNode)
			{
				this.SetMetaDataNodeValue(extensionNode, "disablereason", disableReason.ToString());
				if (disableReason == DisableReasonType.OutlookClientPerformance)
				{
					this.SetMetaDataNodeValue(extensionNode, "appstatus", "4.1");
					return;
				}
				this.SetMetaDataNodeValue(extensionNode, "appstatus", "4.0");
			});
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002A7CC File Offset: 0x000289CC
		public bool TryGetExtension(string extensionId, out ExtensionData extensionData)
		{
			string text;
			return this.TryGetExtension(extensionId, out extensionData, false, out text);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002A7E4 File Offset: 0x000289E4
		public bool TryGetExtension(string extensionId, out ExtensionData extensionData, bool isDebug, out string rawOrgMasterTableXml)
		{
			string item = ExtensionDataHelper.FormatExtensionId(extensionId);
			List<ExtensionData> extensions = this.GetExtensions(new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				item
			}, false, isDebug, out rawOrgMasterTableXml);
			if (extensions.Count == 1)
			{
				extensionData = extensions[0];
				return true;
			}
			extensionData = null;
			return false;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002A830 File Offset: 0x00028A30
		internal List<ExtensionData> GetExtensions(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly)
		{
			string text;
			return this.GetExtensions(formattedRequestedExtensionIds, shouldReturnEnabledOnly, true, false, out text, true);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002A84A File Offset: 0x00028A4A
		internal List<ExtensionData> GetExtensions(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly, bool isDebug, out string orgMasterTableRawXml)
		{
			return this.GetExtensions(formattedRequestedExtensionIds, shouldReturnEnabledOnly, true, isDebug, out orgMasterTableRawXml, true);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002A85C File Offset: 0x00028A5C
		internal List<ExtensionData> GetExtensions(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly, bool shouldFailOnGetOrgExtensionsTimeout, bool isDebug, out string orgMasterTableRawXml, bool filterOutDuplicateMasterTableExtensions = true)
		{
			List<ExtensionData> list = new List<ExtensionData>();
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				List<KeyValuePair<string, ExtensionData>> masterTableExtensions = this.GetMasterTableExtensions(formattedRequestedExtensionIds);
				Dictionary<string, ExtensionData> dictionary = this.CreateDictionaryFromExtensionList(masterTableExtensions);
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				Dictionary<string, ExtensionData> providedExtensions;
				if (shouldFailOnGetOrgExtensionsTimeout)
				{
					providedExtensions = this.GetProvidedExtensions(formattedRequestedExtensionIds, shouldReturnEnabledOnly, dictionary, isDebug, out orgMasterTableRawXml);
				}
				else
				{
					providedExtensions = this.GetProvidedExtensionsHandleTimeout(formattedRequestedExtensionIds, shouldReturnEnabledOnly, dictionary, isDebug, out orgMasterTableRawXml);
				}
				long num = stopwatch.ElapsedMilliseconds - elapsedMilliseconds;
				List<ExtensionData> mergedList = list;
				IEnumerable<KeyValuePair<string, ExtensionData>> masterTableExtensions2;
				if (!filterOutDuplicateMasterTableExtensions)
				{
					IEnumerable<KeyValuePair<string, ExtensionData>> enumerable = masterTableExtensions;
					masterTableExtensions2 = enumerable;
				}
				else
				{
					masterTableExtensions2 = dictionary;
				}
				this.AddMasterTableExtensionsToMergedList(mergedList, masterTableExtensions2, providedExtensions, shouldReturnEnabledOnly);
				this.AddProvidedExtensionsToMergedList(list, providedExtensions, dictionary, shouldReturnEnabledOnly);
				stopwatch.Stop();
				this.AddRequestData("GE", stopwatch.ElapsedMilliseconds.ToString());
				this.AddRequestData("GM", elapsedMilliseconds.ToString());
				this.AddRequestData("GP", num.ToString());
			}
			catch (Exception exception)
			{
				ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_GetExtensionsFailed, null, new object[]
				{
					this.isOrgMailboxSession ? "GetOrgExtensions" : "GetExtensions",
					ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
					ExtensionDiagnostics.GetLoggedExceptionString(exception)
				});
				throw;
			}
			if (!this.isOrgMailboxSession || !this.isUserScope)
			{
				ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_GetExtensionsSuccess, null, new object[]
				{
					this.isOrgMailboxSession ? "GetOrgExtensions" : "GetExtensions"
				});
			}
			return list;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002A9C4 File Offset: 0x00028BC4
		internal Dictionary<string, ExtensionData> TestGetExtensionsFromUserFai()
		{
			return this.CreateDictionaryFromExtensionList(this.GetMasterTableExtensions(null));
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		private Dictionary<string, ExtensionData> CreateDictionaryFromExtensionList(List<KeyValuePair<string, ExtensionData>> masterTableExtensions)
		{
			Dictionary<string, ExtensionData> dictionary = new Dictionary<string, ExtensionData>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, ExtensionData> keyValuePair in masterTableExtensions)
			{
				Version schemaVersion = keyValuePair.Value.SchemaVersion;
				ExtensionData extensionData;
				if ((!this.retrieveOnly1_0 || !(schemaVersion != null) || !(schemaVersion != SchemaConstants.SchemaVersion1_0)) && (!dictionary.TryGetValue(keyValuePair.Key, out extensionData) || extensionData == null || (!(schemaVersion == null) && (!(extensionData.SchemaVersion != null) || !(extensionData.SchemaVersion > schemaVersion)))))
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		private void CopyExtensionMetaData(ExtensionData source, ExtensionData destination)
		{
			destination.IsMandatory = source.IsMandatory;
			destination.IsEnabledByDefault = source.IsEnabledByDefault;
			destination.Enabled = source.Enabled;
			destination.DisableReason = source.DisableReason;
			destination.SpecificUsers = source.SpecificUsers;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		private void AddProvidedExtensionsToMergedList(List<ExtensionData> mergedList, Dictionary<string, ExtensionData> providedExtensions, Dictionary<string, ExtensionData> masterTableExtensions, bool shouldReturnEnabledOnly)
		{
			foreach (KeyValuePair<string, ExtensionData> keyValuePair in providedExtensions)
			{
				ExtensionData value = keyValuePair.Value;
				string key = ExtensionDataHelper.FormatExtensionId(value.ExtensionId);
				ExtensionData extensionData = value;
				ExtensionData extensionData2;
				if (masterTableExtensions.TryGetValue(key, out extensionData2))
				{
					if (this.isOrgMailboxSession)
					{
						extensionData = (value.Clone() as ExtensionData);
						this.CopyExtensionMetaData(extensionData2, extensionData);
					}
					else
					{
						extensionData.Enabled = (value.IsMandatory || extensionData2.Enabled);
					}
				}
				else if (this.isUserScope && !this.isOrgMailboxSession && extensionData.Enabled != (value.IsMandatory || value.IsEnabledByDefault))
				{
					extensionData = (value.Clone() as ExtensionData);
					extensionData.Enabled = (value.IsMandatory || value.IsEnabledByDefault);
					extensionData.EtokenData = value.EtokenData;
				}
				this.AddToMergedList(mergedList, shouldReturnEnabledOnly, extensionData);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002ABFC File Offset: 0x00028DFC
		private void AddMasterTableExtensionsToMergedList(List<ExtensionData> mergedList, IEnumerable<KeyValuePair<string, ExtensionData>> masterTableExtensions, Dictionary<string, ExtensionData> providedExtensions, bool shouldReturnEnabledOnly)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Dictionary<string, ExtensionData> dictionary = new Dictionary<string, ExtensionData>(StringComparer.OrdinalIgnoreCase);
			Dictionary<string, ExtensionData> dictionary2 = new Dictionary<string, ExtensionData>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool flag = false;
			foreach (KeyValuePair<string, ExtensionData> keyValuePair in masterTableExtensions)
			{
				ExtensionData extensionData = keyValuePair.Value;
				if (KillBitList.Singleton.IsExtensionKilled(extensionData.ExtensionId))
				{
					XmlNode masterTableNode = extensionData.MasterTableNode;
					masterTableNode.ParentNode.RemoveChild(masterTableNode);
					flag = true;
					InstalledExtensionTable.Tracer.TraceInformation(this.GetHashCode(), 0L, string.Format("The extension {0} has been removed by killbit.", extensionData.DisplayName));
					ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_AppInKillbitListRemoved, null, new object[]
					{
						"KillAppFromMailbox",
						extensionData.ExtensionId,
						ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
						extensionData.DisplayName
					});
				}
				else
				{
					string item = (extensionData.Version == null) ? extensionData.ExtensionId : (extensionData.ExtensionId + extensionData.Version.ToString());
					if (hashSet.Contains(item))
					{
						XmlNode masterTableNode2 = extensionData.MasterTableNode;
						masterTableNode2.ParentNode.RemoveChild(masterTableNode2);
						flag = true;
						InstalledExtensionTable.Tracer.TraceInformation(this.GetHashCode(), 0L, string.Format("The extension {0} has been removed because it's a duplicate entry in master table.", extensionData.DisplayName));
					}
					else
					{
						hashSet.Add(item);
						string key = ExtensionDataHelper.FormatExtensionId(extensionData.ExtensionId);
						if (!providedExtensions.ContainsKey(key) && extensionData.Manifest != null)
						{
							if (this.extensionsCache != null && extensionData.Type != null && extensionData.Type == ExtensionType.MarketPlace)
							{
								ExtensionData extensionData2;
								if (!dictionary.TryGetValue(extensionData.ExtensionId, out extensionData2))
								{
									byte[] manifestBytes = null;
									if (this.extensionsCache.TryGetExtensionUpdate(extensionData, out manifestBytes))
									{
										ExtensionData extensionData3;
										if (this.TryInstallUpdate(extensionData, manifestBytes, out extensionData3))
										{
											flag = true;
											extensionData = extensionData3;
										}
									}
									else
									{
										dictionary[extensionData.ExtensionId] = extensionData;
									}
								}
								else if (extensionData2.Version < extensionData.Version)
								{
									dictionary[extensionData.ExtensionId] = extensionData;
								}
								if (extensionData.EtokenData != null && extensionData.EtokenData.IsRenewalNeeded && !"2.1".Equals(extensionData.AppStatus, StringComparison.OrdinalIgnoreCase))
								{
									dictionary2[extensionData.ExtensionId] = extensionData;
								}
							}
							this.AddToMergedList(mergedList, shouldReturnEnabledOnly, extensionData);
						}
						extensionData.MasterTableNode = null;
					}
				}
			}
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			this.TokenRenewCheck(dictionary2.Values);
			if (this.UpdateCheck(dictionary.Values))
			{
				flag = true;
			}
			long num = stopwatch.ElapsedMilliseconds - elapsedMilliseconds;
			if (flag)
			{
				this.SaveXmlIfNoConflict();
			}
			stopwatch.Stop();
			long num2 = stopwatch.ElapsedMilliseconds - num;
			this.AddRequestData("AM", elapsedMilliseconds.ToString());
			this.AddRequestData("CU", num.ToString());
			this.AddRequestData("SU", num2.ToString());
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002AF64 File Offset: 0x00029164
		private bool TryInstallUpdate(ExtensionData masterTableExtension, byte[] manifestBytes, out ExtensionData updatedExtension)
		{
			bool result = false;
			updatedExtension = null;
			InstalledExtensionTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Installing update to extension {0}", masterTableExtension.MarketplaceAssetID);
			try
			{
				updatedExtension = ExtensionData.ParseOsfManifest(manifestBytes, manifestBytes.Length, masterTableExtension.MarketplaceAssetID, masterTableExtension.MarketplaceContentMarket, ExtensionType.MarketPlace, masterTableExtension.Scope.Value, masterTableExtension.Enabled, masterTableExtension.DisableReason, string.Empty, masterTableExtension.Etoken);
				this.ReplaceExtension(updatedExtension, masterTableExtension, ExtensionDataHelper.FormatExtensionId(masterTableExtension.ExtensionId));
				result = true;
			}
			catch (OwaExtensionOperationException ex)
			{
				InstalledExtensionTable.Tracer.TraceError<string, OwaExtensionOperationException>((long)this.GetHashCode(), "Update of extension {0} failed. Exception: {1}", masterTableExtension.MarketplaceAssetID, ex);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_ExtensionUpdateFailed, null, new object[]
				{
					"UpdateExtensionFromCache",
					ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
					masterTableExtension.MarketplaceAssetID,
					ExtensionDiagnostics.GetLoggedExceptionString(ex)
				});
			}
			ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_ExtensionUpdateSuccess, null, new object[]
			{
				"UpdateExtensionFromCache",
				ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
				masterTableExtension.MarketplaceAssetID
			});
			return result;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002B090 File Offset: 0x00029290
		private void ReplaceExtension(ExtensionData extensionData, ExtensionData extensionDataToReplace, string formattedId)
		{
			if (extensionDataToReplace != null)
			{
				this.CopyExtensionMetaData(extensionDataToReplace, extensionData);
				bool flag = false;
				if (extensionData.SchemaVersion != null && extensionData.SchemaVersion == SchemaConstants.SchemaVersion1_0)
				{
					this.RemoveExtension(formattedId, false, null);
				}
				else if (extensionData.SchemaVersion != null && extensionData.SchemaVersion >= SchemaConstants.SchemaVersion1_1 && extensionDataToReplace.SchemaVersion != null && extensionDataToReplace.SchemaVersion == SchemaConstants.SchemaVersion1_0)
				{
					flag = true;
				}
				else
				{
					this.RemoveExtension(formattedId, false, extensionDataToReplace.SchemaVersion);
					flag = true;
				}
				if (flag && !string.IsNullOrEmpty(extensionData.Etoken))
				{
					this.ConfigureEtoken(extensionDataToReplace.ExtensionId, extensionData.Etoken, false);
				}
			}
			extensionData.InstalledByVersion = ExchangeSetupContext.InstalledVersion;
			this.masterTableXml.FirstChild.AppendChild(this.masterTableXml.ImportNode(extensionData.ConvertToXml(true, this.isOrgMailboxSession), true));
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002B184 File Offset: 0x00029384
		public static bool IsUpdateCheckTimeExpired(DateTime lastUpdateCheckTime)
		{
			return lastUpdateCheckTime.AddSeconds((double)InstalledExtensionTable.UpdateCheckFrequencySeconds) < DateTime.UtcNow;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002B1A0 File Offset: 0x000293A0
		private bool UpdateCheck(ICollection<ExtensionData> marketplaceExtensionQueryList)
		{
			bool result = false;
			if (this.extensionsCache != null)
			{
				XmlNode xmlNode;
				DateTime lastUpdateCheckTime = this.GetLastUpdateCheckTime(out xmlNode);
				if (InstalledExtensionTable.IsUpdateCheckTimeExpired(lastUpdateCheckTime))
				{
					if (marketplaceExtensionQueryList.Count > 0)
					{
						InstalledExtensionTable.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Submitting update query for {0} extensions", marketplaceExtensionQueryList.Count);
						UpdateQueryContext queryContext = new UpdateQueryContext
						{
							Domain = this.domain,
							OrgEmptyMasterTableCache = this.orgEmptyMasterTableCache,
							IsUserScope = this.isUserScope,
							ExchangePrincipal = this.sessionMailboxOwner,
							CultureInfo = this.sessionPreferedCulture,
							ClientInfoString = ExtensionsCache.BuildClientInfoString(this.sessionClientInfoString)
						};
						this.extensionsCache.SubmitUpdateQuery(marketplaceExtensionQueryList, queryContext);
					}
					xmlNode.InnerText = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
					InstalledExtensionTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LastUpdateCheckTime set to {0}", xmlNode.InnerText);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002B298 File Offset: 0x00029498
		private void TokenRenewCheck(ICollection<ExtensionData> marketplaceTokenRenewList)
		{
			if (marketplaceTokenRenewList.Count > 0 && this.extensionsCache != null)
			{
				InstalledExtensionTable.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Submitting token renew query for {0} extensions", marketplaceTokenRenewList.Count);
				TokenRenewQueryContext queryContext = new TokenRenewQueryContext
				{
					Domain = this.domain,
					OrgEmptyMasterTableCache = this.orgEmptyMasterTableCache,
					IsUserScope = this.isUserScope,
					ExchangePrincipal = this.sessionMailboxOwner,
					CultureInfo = this.sessionPreferedCulture,
					ClientInfoString = ExtensionsCache.BuildClientInfoString(this.sessionClientInfoString)
				};
				this.extensionsCache.TokenRenewSubmitter.SubmitRenewQuery(marketplaceTokenRenewList, queryContext);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002B3E8 File Offset: 0x000295E8
		public void ConfigureOrgExtension(string extensionId, bool isEnabled, bool isMandatory, bool isEnabledByDefault, ClientExtensionProvidedTo providedTo, string[] specificUsers)
		{
			if (this.isOrgMailboxSession)
			{
				this.ConfigureLocalExtension(extensionId, isEnabled, delegate(XmlNode extensionNode)
				{
					this.SetMetaDataNodeValue(extensionNode, "isMandatory", isMandatory.ToString());
					this.SetMetaDataNodeValue(extensionNode, "isEnabledByDefault", isEnabledByDefault.ToString());
					this.SetMetaDataNodeValue(extensionNode, "providedTo", providedTo.ToString());
					XmlNode xmlNode = extensionNode.SelectSingleNode("users");
					if (xmlNode != null)
					{
						extensionNode.RemoveChild(xmlNode);
					}
					ExtensionData.AppendXmlElement(this.masterTableXml, extensionNode, "users", "user", specificUsers);
				});
				return;
			}
			OrgExtensionTable.ConfigureOrgExtension(this.domain, extensionId, isEnabled, isMandatory, isEnabledByDefault, providedTo, specificUsers);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002B46A File Offset: 0x0002966A
		public void ConfigureUserExtension(string extensionId, bool isEnabled)
		{
			this.ConfigureLocalExtension(extensionId, isEnabled, null);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002B4D8 File Offset: 0x000296D8
		private void ConfigureLocalExtension(string extensionId, bool isEnabled, Action<XmlNode> configurationAction)
		{
			string text = ExtensionDataHelper.FormatExtensionId(extensionId);
			ExtensionData extensionData;
			bool flag = this.TryGetProvidedExtension(text, out extensionData);
			if (this.isUserScope && flag && extensionData.IsMandatory && !isEnabled)
			{
				throw new CannotDisableMandatoryExtensionException();
			}
			if (this.isUserScope)
			{
				string disableReasonString = isEnabled ? DisableReasonType.NotDisabled.ToString() : DisableReasonType.NoReason.ToString();
				Action<XmlNode> configurationAction2 = delegate(XmlNode extensionNode)
				{
					this.SetMetaDataNodeValue(extensionNode, "disablereason", disableReasonString);
				};
				if (!this.TryConfigureExistingRecord(text, configurationAction2, true))
				{
					if (extensionData == null)
					{
						throw new ExtensionNotFoundException(text);
					}
					this.AddConfigurationRecord(text, isEnabled, configurationAction2, extensionData);
				}
			}
			if (!this.TryConfigureExistingRecord(text, delegate(XmlNode extensionNode)
			{
				this.SetMetaDataNodeValue(extensionNode, "enabled", isEnabled.ToString());
				if (configurationAction != null)
				{
					configurationAction(extensionNode);
				}
			}, true))
			{
				this.AddConfigurationRecord(text, isEnabled, configurationAction, extensionData);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002B5F8 File Offset: 0x000297F8
		public void ConfigureAppStatus(string appId, string appStatus)
		{
			Action<XmlNode> configurationAction = delegate(XmlNode extensionNode)
			{
				this.SetMetaDataNodeValue(extensionNode, "appstatus", appStatus);
			};
			this.TryConfigureExistingRecord(appId, configurationAction, true);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002B654 File Offset: 0x00029854
		public void ConfigureEtoken(string appId, string etoken, bool shouldSave = true)
		{
			Action<XmlNode> configurationAction = delegate(XmlNode extensionNode)
			{
				this.SetMetaDataNodeValue(extensionNode, "entitlementToken", etoken);
			};
			this.TryConfigureExistingRecord(appId, configurationAction, shouldSave);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002B68C File Offset: 0x0002988C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.masterTable != null)
			{
				this.masterTable.Dispose();
				this.masterTable = null;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002B6AB File Offset: 0x000298AB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InstalledExtensionTable>(this);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002B6B4 File Offset: 0x000298B4
		private bool TryConfigureExistingRecord(string formattedId, Action<XmlNode> configurationAction, bool shouldSave = true)
		{
			this.LoadXML();
			bool flag = false;
			using (XmlNodeList xmlNodeList = this.masterTableXml.SelectNodes("/ExtensionList/Extension"))
			{
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("ExtensionId");
						if (xmlNode2 != null && string.Equals(formattedId, xmlNode2.InnerText, StringComparison.OrdinalIgnoreCase))
						{
							configurationAction(xmlNode);
							flag = true;
						}
					}
					if (shouldSave && flag)
					{
						this.SaveXML();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B770 File Offset: 0x00029970
		private void AddConfigurationRecord(string formattedId, bool isEnabled, Action<XmlNode> configurationAction, ExtensionData providedExtension)
		{
			if (providedExtension != null)
			{
				if (this.isUserScope && isEnabled == (providedExtension.IsMandatory || providedExtension.IsEnabledByDefault))
				{
					return;
				}
				XmlNode xmlNode = this.masterTableXml.ImportNode(providedExtension.ConvertToXml(false, this.isOrgMailboxSession), true);
				xmlNode.SelectSingleNode("enabled").InnerText = isEnabled.ToString();
				if (configurationAction != null)
				{
					configurationAction(xmlNode);
				}
				this.masterTableXml.FirstChild.AppendChild(xmlNode);
				this.SaveXML();
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002B7F4 File Offset: 0x000299F4
		private void AddToMergedList(List<ExtensionData> list, bool shouldReturnEnabledOnly, ExtensionData extensionData)
		{
			if (!this.isUserScope || !shouldReturnEnabledOnly || extensionData.Enabled)
			{
				list.Add(extensionData);
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002B810 File Offset: 0x00029A10
		private void SetMetaDataNodeValue(XmlNode extensionNode, string nodeName, string value)
		{
			XmlNode xmlNode = extensionNode.SelectSingleNode(nodeName);
			if (xmlNode == null)
			{
				xmlNode = this.masterTableXml.CreateElement(nodeName);
				extensionNode.AppendChild(xmlNode);
			}
			xmlNode.InnerText = value;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002B844 File Offset: 0x00029A44
		private List<KeyValuePair<string, ExtensionData>> GetMasterTableExtensions(HashSet<string> formattedRequestedExtensionIds)
		{
			List<KeyValuePair<string, ExtensionData>> list = new List<KeyValuePair<string, ExtensionData>>();
			if (this.isUserScope || this.isOrgMailboxSession)
			{
				this.LoadXML();
				using (XmlNodeList extensionNodes = this.GetExtensionNodes())
				{
					if (extensionNodes != null && extensionNodes.Count > 0)
					{
						bool flag = false;
						int i = extensionNodes.Count - 1;
						while (i >= 0)
						{
							XmlNode xmlNode = extensionNodes.Item(i);
							ExtensionData extensionData;
							try
							{
								extensionData = ExtensionData.ConvertFromMasterTableXml(xmlNode, this.isOrgMailboxSession, this.domain);
								extensionData.MasterTableNode = xmlNode;
							}
							catch (OwaExtensionOperationException ex)
							{
								InstalledExtensionTable.Tracer.TraceError<string, string, string>((long)this.GetHashCode(), "Master table extension data is invalid:{0}{1}{0}Removing the invalid node:{0}{2}", Environment.NewLine, ex.ToString(), xmlNode.OuterXml);
								ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_InvalidExtensionRemoved, null, new object[]
								{
									"RemoveInvalidExtension",
									ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
									ExtensionDiagnostics.GetLoggedExceptionString(ex),
									xmlNode.OuterXml,
									Environment.NewLine + Environment.NewLine
								});
								xmlNode.ParentNode.RemoveChild(xmlNode);
								flag = true;
								goto IL_138;
							}
							goto IL_10E;
							IL_138:
							i--;
							continue;
							IL_10E:
							string text = ExtensionDataHelper.FormatExtensionId(extensionData.ExtensionId);
							if (formattedRequestedExtensionIds == null || formattedRequestedExtensionIds.Contains(text))
							{
								list.Add(new KeyValuePair<string, ExtensionData>(text, extensionData));
								goto IL_138;
							}
							goto IL_138;
						}
						if (flag)
						{
							this.SaveXmlIfNoConflict();
						}
					}
					else if (this.isOrgMailboxSession)
					{
						this.orgEmptyMasterTableCache.Update(this.sessionMailboxOwner.MailboxInfo.OrganizationId, true);
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002BA08 File Offset: 0x00029C08
		private DateTime GetLastUpdateCheckTime(out XmlNode lastUpdateCheckTimeXmlNode)
		{
			DateTime minValue = DateTime.MinValue;
			lastUpdateCheckTimeXmlNode = null;
			lastUpdateCheckTimeXmlNode = this.masterTableXml.SelectSingleNode("/ExtensionList/LastUpdateCheckTime");
			if (lastUpdateCheckTimeXmlNode == null)
			{
				InstalledExtensionTable.Tracer.TraceDebug((long)this.GetHashCode(), "lastUpdateCheckTimeXmlNode is null. Adding node.");
				XmlNode documentElement = this.masterTableXml.DocumentElement;
				lastUpdateCheckTimeXmlNode = this.masterTableXml.CreateElement("LastUpdateCheckTime");
				documentElement.AppendChild(lastUpdateCheckTimeXmlNode);
			}
			else if (lastUpdateCheckTimeXmlNode.InnerText != null && !DateTime.TryParse(lastUpdateCheckTimeXmlNode.InnerText, CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue))
			{
				InstalledExtensionTable.Tracer.TraceDebug<string>((long)this.GetHashCode(), "lastUpdateCheckTimeXmlNode '{0}' parse failed.", lastUpdateCheckTimeXmlNode.InnerText);
			}
			return minValue;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002BAB0 File Offset: 0x00029CB0
		private bool TryGetProvidedExtension(string formattedRequestedExtensionId, out ExtensionData providedExtension)
		{
			string text;
			Dictionary<string, ExtensionData> providedExtensions = this.GetProvidedExtensions(new HashSet<string>(StringComparer.OrdinalIgnoreCase)
			{
				formattedRequestedExtensionId
			}, false, null, false, out text);
			return providedExtensions.TryGetValue(formattedRequestedExtensionId, out providedExtension);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002BAE8 File Offset: 0x00029CE8
		private Dictionary<string, ExtensionData> GetProvidedExtensionsHandleTimeout(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly, Dictionary<string, ExtensionData> masterTableExtensions, bool isDebug, out string orgMasterTableRawXml)
		{
			Dictionary<string, ExtensionData> result;
			try
			{
				result = this.GetProvidedExtensions(formattedRequestedExtensionIds, shouldReturnEnabledOnly, masterTableExtensions, isDebug, out orgMasterTableRawXml);
			}
			catch (OwaExtensionOperationException ex)
			{
				InstalledExtensionTable.Tracer.TraceError<OwaExtensionOperationException>((long)this.GetHashCode(), "Exception thrown in InstalledExtensionTable.GetProvidedExtensions. Exception: {0}", ex);
				WebException ex2 = null;
				if (ex.InnerException != null)
				{
					ex2 = (ex.InnerException.InnerException as WebException);
				}
				if (ex2 == null || ex2.Status != WebExceptionStatus.Timeout)
				{
					throw;
				}
				InstalledExtensionTable.Tracer.TraceDebug((long)this.GetHashCode(), "Timeout in InstalledExtensionTable.GetProvidedExtensions. Returning empty list.");
				string loggedMailboxIdentifier = ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_GetOrgExtensionsTimedOut, null, new object[]
				{
					"GetExtensionsHandleTimeout",
					loggedMailboxIdentifier,
					loggedMailboxIdentifier,
					ex2
				});
				orgMasterTableRawXml = string.Empty;
				result = new Dictionary<string, ExtensionData>();
			}
			return result;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		private Dictionary<string, ExtensionData> GetProvidedExtensions(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly, Dictionary<string, ExtensionData> masterTableExtensions, bool isDebug, out string orgMasterTableRawXml)
		{
			if (!this.isOrgMailboxSession)
			{
				return this.GetOrgProvidedExtensions(formattedRequestedExtensionIds, shouldReturnEnabledOnly, masterTableExtensions, isDebug, out orgMasterTableRawXml);
			}
			orgMasterTableRawXml = (isDebug ? this.MasterTableXml.InnerXml : string.Empty);
			if (formattedRequestedExtensionIds == null)
			{
				return InstalledExtensionTable.GetDefaultExtensions(this.sessionMailboxOwner);
			}
			foreach (string key in formattedRequestedExtensionIds)
			{
				ExtensionData value;
				if (InstalledExtensionTable.GetDefaultExtensions(this.sessionMailboxOwner).TryGetValue(key, out value))
				{
					return new Dictionary<string, ExtensionData>(StringComparer.OrdinalIgnoreCase)
					{
						{
							key,
							value
						}
					};
				}
			}
			return new Dictionary<string, ExtensionData>();
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002BC7C File Offset: 0x00029E7C
		private Dictionary<string, ExtensionData> GetOrgProvidedExtensions(HashSet<string> formattedRequestedExtensionIds, bool shouldReturnEnabledOnly, Dictionary<string, ExtensionData> masterTableExtensions, bool isDebug, out string orgMasterTableRawXml)
		{
			StringList stringList = null;
			StringList stringList2 = null;
			if (this.isUserScope && shouldReturnEnabledOnly)
			{
				foreach (KeyValuePair<string, ExtensionData> keyValuePair in masterTableExtensions)
				{
					if (keyValuePair.Value.Enabled)
					{
						if (stringList == null)
						{
							stringList = new StringList();
						}
						stringList.Add(keyValuePair.Key);
					}
					else
					{
						if (stringList2 == null)
						{
							stringList2 = new StringList();
						}
						stringList2.Add(keyValuePair.Key);
					}
				}
			}
			StringList requestedExtensionIds = null;
			if (formattedRequestedExtensionIds != null)
			{
				requestedExtensionIds = new StringList(formattedRequestedExtensionIds);
			}
			OrgExtensionTable.RequestData requestData;
			Dictionary<string, ExtensionData> orgExtensions = OrgExtensionTable.GetOrgExtensions(requestedExtensionIds, this.domain, shouldReturnEnabledOnly, this.isUserScope, this.userId, stringList, stringList2, out requestData, isDebug, out orgMasterTableRawXml, this.retrieveOnly1_0);
			if (requestData.ExchangeServiceUri != null)
			{
				this.AddRequestData("OrgHost", requestData.ExchangeServiceUri.Host);
				this.AddRequestData("EWSReqId", requestData.EwsRequestId);
				this.AddRequestData("GCE", requestData.GetClientExtensionTime.ToString());
			}
			this.AddRequestData("CES", requestData.CreateExchangeServiceTime.ToString());
			return orgExtensions;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002BDAC File Offset: 0x00029FAC
		private void LoadXML()
		{
			if (this.masterTableXml == null)
			{
				this.masterTableXml = new SafeXmlDocument();
				this.masterTableXml.PreserveWhitespace = true;
				using (Stream xmlStream = this.masterTable.GetXmlStream())
				{
					try
					{
						if (xmlStream != null && xmlStream.Length > 0L)
						{
							this.masterTableXml.Load(xmlStream);
						}
						else
						{
							InstalledExtensionTable.Tracer.TraceDebug((long)this.GetHashCode(), "The manifest xml is empty.");
						}
						if (string.IsNullOrEmpty(this.masterTableXml.InnerXml))
						{
							this.masterTableXml.InnerXml = "<ExtensionList />";
						}
					}
					catch (XmlException arg)
					{
						InstalledExtensionTable.Tracer.TraceDebug<XmlException>((long)this.GetHashCode(), "The manifest xml is corrupted.", arg);
						throw;
					}
				}
				return;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002BE80 File Offset: 0x0002A080
		private void AddRequestData(string key, string value)
		{
			if (this.requestData.ContainsKey(key))
			{
				Dictionary<string, string> dictionary;
				(dictionary = this.requestData)[key] = dictionary[key] + "," + value;
				return;
			}
			this.requestData.Add(key, value);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002BECC File Offset: 0x0002A0CC
		internal static int GetElapsedTime(DateTime startTime, DateTime endTime)
		{
			return endTime.Subtract(startTime).Milliseconds;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		internal static bool ValidateAndRemoveManifestSignature(SafeXmlDocument safeXmlDocument, string extensionId, bool shouldThrowOnFailure = true)
		{
			if (InstalledExtensionTable.IsAppSignatureValidationDisabled())
			{
				InstalledExtensionTable.Tracer.TraceDebug<string>(0L, "App Signature Validation Disabled. Skipping Validation and Removal of Signature. App Id: {0}", extensionId);
				return true;
			}
			InstalledExtensionTable.Tracer.TraceDebug<string>(0L, "Do Signature Validation. Id: {0}", extensionId);
			if (SignedXMLVerifier.VerifySignedXml(safeXmlDocument))
			{
				InstalledExtensionTable.Tracer.TraceDebug<string, int>(0L, "Signature Validation succeeded. Id: {0}, XML Length: {1}", extensionId, safeXmlDocument.OuterXml.Length);
				SignedXMLVerifier.RemoveSignature(safeXmlDocument);
				InstalledExtensionTable.Tracer.TraceDebug<string, int>(0L, "Signature Removed. Id: {0}, XML Length: {1}", extensionId, safeXmlDocument.OuterXml.Length);
				return true;
			}
			if (shouldThrowOnFailure)
			{
				throw new OwaExtensionOperationException(Strings.ErrorManifestSignatureInvalidExtension);
			}
			return false;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002BF80 File Offset: 0x0002A180
		internal ConflictResolutionResult SaveXmlIfNoConflict()
		{
			using (Stream xmlStream = this.masterTable.GetXmlStream())
			{
				xmlStream.SetLength(0L);
				this.masterTableXml.Save(xmlStream);
			}
			ConflictResolutionResult conflictResolutionResult = this.masterTable.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				string text = null;
				if (conflictResolutionResult.PropertyConflicts != null)
				{
					InstalledExtensionTable.Tracer.TraceError<int>((long)this.GetHashCode(), "UserConfiguration.Save returned '{0}' property conflicts.", conflictResolutionResult.PropertyConflicts.Length);
					StringBuilder stringBuilder = new StringBuilder();
					foreach (PropertyConflict propertyConflict in conflictResolutionResult.PropertyConflicts)
					{
						stringBuilder.AppendLine(string.Format("Property conflict: DisplayName: '{0}', Resolvable: '{1}', OriginalValue: '{2}', ClientValue: '{3}', ServerValue: '{4}'", new object[]
						{
							(propertyConflict.PropertyDefinition != null) ? propertyConflict.PropertyDefinition.Name : ExtensionDiagnostics.HandleNullObjectTrace(propertyConflict.PropertyDefinition),
							propertyConflict.ConflictResolvable,
							ExtensionDiagnostics.HandleNullObjectTrace(propertyConflict.OriginalValue),
							ExtensionDiagnostics.HandleNullObjectTrace(propertyConflict.ClientValue),
							ExtensionDiagnostics.HandleNullObjectTrace(propertyConflict.ServerValue)
						}));
					}
					text = stringBuilder.ToString();
					InstalledExtensionTable.Tracer.TraceError((long)this.GetHashCode(), text);
				}
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_MastertableSaveFailedSaveConflict, null, new object[]
				{
					"UpdateMasterTable",
					ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.sessionMailboxOwner),
					text
				});
			}
			else
			{
				InstalledExtensionTable.Tracer.Information(0L, "The app master table was saved successfully.");
				if (this.isOrgMailboxSession)
				{
					using (XmlNodeList extensionNodes = this.GetExtensionNodes())
					{
						bool isEmpty = extensionNodes == null || extensionNodes.Count == 0;
						this.orgEmptyMasterTableCache.Update(this.sessionMailboxOwner.MailboxInfo.OrganizationId, isEmpty);
					}
				}
			}
			return conflictResolutionResult;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002C17C File Offset: 0x0002A37C
		internal void SaveXML()
		{
			ConflictResolutionResult conflictResolutionResult = this.SaveXmlIfNoConflict();
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(Strings.ErrorMasterTableSaveConflict, conflictResolutionResult);
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002C1A5 File Offset: 0x0002A3A5
		private XmlNodeList GetExtensionNodes()
		{
			return this.masterTableXml.SelectNodes("/ExtensionList/Extension");
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		private static bool IsAppSignatureValidationDisabled()
		{
			if (InstalledExtensionTable.registryChangeListener == null)
			{
				InstalledExtensionTable.Tracer.TraceDebug(0L, "Setting Registry Change Listener for DisableAppValidation Key.");
				InstalledExtensionTable.registryChangeListener = new RegistryChangeListener(OwaConstants.OwaSetupInstallKey, new EventArrivedEventHandler(InstalledExtensionTable.DisableAppValidationRegistryKeyChangeHandler));
			}
			if (InstalledExtensionTable.disableAppValidation == null)
			{
				InstalledExtensionTable.disableAppValidation = new bool?(InstalledExtensionTable.GetAppSignatureValidationDisabledValueFromRegistry());
			}
			return InstalledExtensionTable.disableAppValidation.Value;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002C220 File Offset: 0x0002A420
		private static bool GetAppSignatureValidationDisabledValueFromRegistry()
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(OwaConstants.OwaSetupInstallKey))
				{
					flag = RegistryReader.Instance.GetValue<bool>(registryKey, null, "DisableAppValidation", false);
					InstalledExtensionTable.Tracer.TraceDebug<bool>(0L, "App Signature Validation Disabled From Registry. Value: {0}", flag);
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				InstalledExtensionTable.Tracer.TraceError<string, string>(0L, "Cannot Read Value: {0} from Registry due to Exception {1}. Using false.", "DisableAppValidation", ex.ToString());
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		private static void DisableAppValidationRegistryKeyChangeHandler(object sender, EventArrivedEventArgs e)
		{
			InstalledExtensionTable.Tracer.TraceDebug(0L, "Registry Change Event Occurred. Get DisableAppValidation Key.");
			InstalledExtensionTable.disableAppValidation = new bool?(InstalledExtensionTable.GetAppSignatureValidationDisabledValueFromRegistry());
		}

		// Token: 0x04000549 RID: 1353
		private const string UpdateCheckFrequencySecondsKey = "UpdateCheckFrequencySeconds";

		// Token: 0x0400054A RID: 1354
		private const string ScenarioNameForGetExtensions = "GetExtensions";

		// Token: 0x0400054B RID: 1355
		private const string ScenarioNameForGetExtensionsHandleTimeout = "GetExtensionsHandleTimeout";

		// Token: 0x0400054C RID: 1356
		private const string ScenarioNameForGetOrgExtensions = "GetOrgExtensions";

		// Token: 0x0400054D RID: 1357
		private const string ScenarioNameForRemoveInvalidExtension = "RemoveInvalidExtension";

		// Token: 0x0400054E RID: 1358
		private const string ScenarioUpdateMasterTable = "UpdateMasterTable";

		// Token: 0x0400054F RID: 1359
		private const string ScenarioNameForKillApp = "KillAppFromMailbox";

		// Token: 0x04000550 RID: 1360
		private const string ScenarioNameForUpdateFromCache = "UpdateExtensionFromCache";

		// Token: 0x04000551 RID: 1361
		private const string ExtensionXpath = "/ExtensionList/Extension";

		// Token: 0x04000552 RID: 1362
		public const string LastUpdateCheckTimeXpath = "/ExtensionList/LastUpdateCheckTime";

		// Token: 0x04000553 RID: 1363
		public const int UpdateCheckFrequencyHoursDefault = 72;

		// Token: 0x04000554 RID: 1364
		public const string ExtensionMasterTableName = "ExtensionMasterTable";

		// Token: 0x04000555 RID: 1365
		internal const string LastUpdateCheckTimeElementName = "LastUpdateCheckTime";

		// Token: 0x04000556 RID: 1366
		private static bool? disableAppValidation = null;

		// Token: 0x04000557 RID: 1367
		private UserConfiguration masterTable;

		// Token: 0x04000558 RID: 1368
		private SafeXmlDocument masterTableXml;

		// Token: 0x04000559 RID: 1369
		private string userId;

		// Token: 0x0400055A RID: 1370
		private string domain;

		// Token: 0x0400055B RID: 1371
		private bool isOrgMailboxSession;

		// Token: 0x0400055C RID: 1372
		private bool isUserScope;

		// Token: 0x0400055D RID: 1373
		private ExtensionsCache extensionsCache;

		// Token: 0x0400055E RID: 1374
		private OrgEmptyMasterTableCache orgEmptyMasterTableCache;

		// Token: 0x0400055F RID: 1375
		private IExchangePrincipal sessionMailboxOwner;

		// Token: 0x04000560 RID: 1376
		private CultureInfo sessionPreferedCulture;

		// Token: 0x04000561 RID: 1377
		private string sessionClientInfoString;

		// Token: 0x04000562 RID: 1378
		private Dictionary<string, string> requestData = new Dictionary<string, string>();

		// Token: 0x04000563 RID: 1379
		private readonly bool retrieveOnly1_0;

		// Token: 0x04000564 RID: 1380
		private static int updateCheckFrequencySeconds;

		// Token: 0x04000565 RID: 1381
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x04000566 RID: 1382
		private static volatile DefaultExtensionTable defaultExtensionTable;

		// Token: 0x04000567 RID: 1383
		private static RegistryChangeListener registryChangeListener;

		// Token: 0x04000568 RID: 1384
		private static object defaultExtensionTableLock = new object();

		// Token: 0x04000569 RID: 1385
		private static LazyMember<bool> isMultiTenancyEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled);

		// Token: 0x020000FE RID: 254
		internal static class RequestDataKey
		{
			// Token: 0x0400056B RID: 1387
			internal const string GetExtensionsTime = "GE";

			// Token: 0x0400056C RID: 1388
			internal const string GetMasterTableTime = "GM";

			// Token: 0x0400056D RID: 1389
			internal const string GetProvidedExtensionsTime = "GP";

			// Token: 0x0400056E RID: 1390
			internal const string AddMasterTableTime = "AM";

			// Token: 0x0400056F RID: 1391
			internal const string CheckUpdatesTime = "CU";

			// Token: 0x04000570 RID: 1392
			internal const string SaveMasterTableTime = "SU";

			// Token: 0x04000571 RID: 1393
			internal const string OrgMailboxEwsUrlHost = "OrgHost";

			// Token: 0x04000572 RID: 1394
			internal const string OrgMailboxEwsRequestId = "EWSReqId";

			// Token: 0x04000573 RID: 1395
			internal const string GetOrgExtensionsTime = "GO";

			// Token: 0x04000574 RID: 1396
			internal const string GetExtensionsTotalTime = "GET";

			// Token: 0x04000575 RID: 1397
			internal const string CreateExchangeServiceTime = "CES";

			// Token: 0x04000576 RID: 1398
			internal const string GetClientExtensionTime = "GCE";

			// Token: 0x04000577 RID: 1399
			internal const string OrgMailboxAdUserLookupTime = "OAD";

			// Token: 0x04000578 RID: 1400
			internal const string WebServiceUrlLookupTime = "WSUrl";

			// Token: 0x04000579 RID: 1401
			internal const string CreateExtensionsTime = "CET";

			// Token: 0x0400057A RID: 1402
			internal const string GetMarketplaceUrlTime = "GMUT";
		}
	}
}
