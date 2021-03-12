using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;
using Microsoft.RightsManagementServices.Online.Partner;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200070C RID: 1804
	[Cmdlet("Import", "RMSTrustedPublishingDomain", SupportsShouldProcess = true, DefaultParameterSetName = "IntranetLicensingUrl")]
	public sealed class ImportRmsTrustedPublishingDomain : NewMultitenancySystemConfigurationObjectTask<RMSTrustedPublishingDomain>
	{
		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x00105F08 File Offset: 0x00104108
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x00105F1F File Offset: 0x0010411F
		[Parameter(Mandatory = true, ParameterSetName = "ImportFromFile")]
		[Parameter(Mandatory = true, ParameterSetName = "IntranetLicensingUrl")]
		[Parameter(Mandatory = true, ParameterSetName = "RefreshTemplates")]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x00105F32 File Offset: 0x00104132
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x00105F49 File Offset: 0x00104149
		[Parameter(Mandatory = true, ParameterSetName = "RefreshTemplates")]
		[Parameter(Mandatory = true, ParameterSetName = "ImportFromFile")]
		[Parameter(Mandatory = true, ParameterSetName = "IntranetLicensingUrl")]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x00105F5C File Offset: 0x0010415C
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x00105F73 File Offset: 0x00104173
		[Parameter(Mandatory = true, ParameterSetName = "IntranetLicensingUrl")]
		[Parameter(Mandatory = true, ParameterSetName = "ImportFromFile")]
		public Uri IntranetLicensingUrl
		{
			get
			{
				return (Uri)base.Fields["IntranetLicensingUrl"];
			}
			set
			{
				base.Fields["IntranetLicensingUrl"] = value;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x00105F86 File Offset: 0x00104186
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x00105F9D File Offset: 0x0010419D
		[Parameter(Mandatory = true, ParameterSetName = "IntranetLicensingUrl")]
		[Parameter(Mandatory = true, ParameterSetName = "ImportFromFile")]
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return (Uri)base.Fields["ExtranetLicensingUrl"];
			}
			set
			{
				base.Fields["ExtranetLicensingUrl"] = value;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x00105FB0 File Offset: 0x001041B0
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x00105FC7 File Offset: 0x001041C7
		[Parameter(Mandatory = false, ParameterSetName = "ImportFromFile")]
		public Uri IntranetCertificationUrl
		{
			get
			{
				return (Uri)base.Fields["IntranetCertificationUrl"];
			}
			set
			{
				base.Fields["IntranetCertificationUrl"] = value;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x00105FDA File Offset: 0x001041DA
		// (set) Token: 0x0600401F RID: 16415 RVA: 0x00105FF1 File Offset: 0x001041F1
		[Parameter(Mandatory = false, ParameterSetName = "ImportFromFile")]
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return (Uri)base.Fields["ExtranetCertificationUrl"];
			}
			set
			{
				base.Fields["ExtranetCertificationUrl"] = value;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x00106004 File Offset: 0x00104204
		// (set) Token: 0x06004021 RID: 16417 RVA: 0x0010602A File Offset: 0x0010422A
		[Parameter(Mandatory = false)]
		public SwitchParameter Default
		{
			get
			{
				return (SwitchParameter)(base.Fields["Default"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Default"] = value;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x00106042 File Offset: 0x00104242
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x00106068 File Offset: 0x00104268
		[Parameter(Mandatory = false, ParameterSetName = "RMSOnline")]
		[Parameter(Mandatory = false, ParameterSetName = "RefreshTemplates")]
		public SwitchParameter RefreshTemplates
		{
			get
			{
				return (SwitchParameter)(base.Fields["RefreshTemplates"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RefreshTemplates"] = value;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x00106080 File Offset: 0x00104280
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x001060A6 File Offset: 0x001042A6
		[Parameter(Mandatory = true, ParameterSetName = "RMSOnline")]
		public SwitchParameter RMSOnline
		{
			get
			{
				return (SwitchParameter)(base.Fields["RMSOnline"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RMSOnline"] = value;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x001060BE File Offset: 0x001042BE
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x001060E3 File Offset: 0x001042E3
		[Parameter(Mandatory = false, ParameterSetName = "RMSOnline")]
		public Guid RMSOnlineOrgOverride
		{
			get
			{
				return (Guid)(base.Fields["RMSOnlineOrgOverride"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["RMSOnlineOrgOverride"] = value;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x001060FB File Offset: 0x001042FB
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x00106112 File Offset: 0x00104312
		[Parameter(Mandatory = false, ParameterSetName = "RMSOnline")]
		public string RMSOnlineAuthCertSubjectNameOverride
		{
			get
			{
				return (string)base.Fields["RMSOnlineAuthCertSubjectNameOverride"];
			}
			set
			{
				base.Fields["RMSOnlineAuthCertSubjectNameOverride"] = value;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x00106125 File Offset: 0x00104325
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x0010613C File Offset: 0x0010433C
		[Parameter(Mandatory = true, ParameterSetName = "RMSOnline2")]
		public byte[] RMSOnlineConfig
		{
			get
			{
				return (byte[])base.Fields["RMSOnlineConfig"];
			}
			set
			{
				base.Fields["RMSOnlineConfig"] = value;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x0010614F File Offset: 0x0010434F
		// (set) Token: 0x0600402D RID: 16429 RVA: 0x00106166 File Offset: 0x00104366
		[Parameter(Mandatory = true, ParameterSetName = "RMSOnline2")]
		public Hashtable RMSOnlineKeys
		{
			get
			{
				return (Hashtable)base.Fields["RMSOnlineKeys"];
			}
			set
			{
				base.Fields["RMSOnlineKeys"] = value;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x0010617C File Offset: 0x0010437C
		// (set) Token: 0x0600402F RID: 16431 RVA: 0x00106230 File Offset: 0x00104430
		[Parameter(Mandatory = false, ParameterSetName = "RMSOnline2")]
		public Hashtable RMSOnlineAuthorTest
		{
			get
			{
				Hashtable hashtable = new Hashtable
				{
					{
						"TrustedRootHierarchy",
						0
					},
					{
						"KeyProtectionCertificate",
						null
					},
					{
						"KeyProtectionCertificatePassword",
						null
					}
				};
				foreach (object obj in ((Hashtable)(base.Fields["RMSOnlineAuthorTest"] ?? new Hashtable())))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					hashtable[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
				return hashtable;
			}
			set
			{
				base.Fields["RMSOnlineAuthorTest"] = value;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x00106243 File Offset: 0x00104443
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportRMSTPD(base.Name);
			}
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x00106250 File Offset: 0x00104450
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception) || typeof(InvalidRpmsgFormatException).IsInstanceOfType(exception);
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x00106275 File Offset: 0x00104475
		private void ThrowIfExistingTpdNotFound(RMSTrustedPublishingDomain[] existingTpd)
		{
			if (existingTpd == null || existingTpd.Length == 0)
			{
				base.WriteError(new FailedToFindTPDForRefreshException(base.Name), ExchangeErrorCategory.Client, base.Name);
			}
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0010629C File Offset: 0x0010449C
		private void ThrowIfRmsOnlinePreRequisitesNotMet(IRMConfiguration irmConfiguration)
		{
			if (!RmsUtil.AreRmsOnlinePreRequisitesMet(irmConfiguration))
			{
				base.WriteError(new RmsOnlineUrlsNotPresentException(), ExchangeErrorCategory.Client, this.RMSOnline);
			}
			if (!string.IsNullOrEmpty(irmConfiguration.RMSOnlineVersion))
			{
				base.WriteError(new OldRmsOnlineImportAfterRmsOnlineForwardSync(), ExchangeErrorCategory.Client, this.RMSOnline);
			}
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x001062F4 File Offset: 0x001044F4
		private void ThrowIfOrganizationNotSpecified()
		{
			if (OrganizationId.ForestWideOrgId == base.CurrentOrganizationId)
			{
				base.WriteError(new ArgumentException(Strings.ErrorOrganizationParameterRequired), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x00106320 File Offset: 0x00104520
		private void UpdateTpdNameForRmsOnline()
		{
			RMSTrustedPublishingDomain[] array = ((IConfigurationSession)base.DataSession).Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, null, null, 0);
			string existingDefaultTpdName = null;
			foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in array)
			{
				if (rmstrustedPublishingDomain.Default)
				{
					existingDefaultTpdName = rmstrustedPublishingDomain.Name;
					break;
				}
			}
			base.Name = RmsUtil.GenerateRmsOnlineTpdName(existingDefaultTpdName, base.Name);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0010638C File Offset: 0x0010458C
		internal static void ChangeDefaultTPDAndUpdateIrmConfigData(IConfigurationSession session, IRMConfiguration irmConfiguration, RMSTrustedPublishingDomain trustedPublishingDomain, out RMSTrustedPublishingDomain oldDefaultTPD)
		{
			ADPagedReader<RMSTrustedPublishingDomain> source = session.FindPaged<RMSTrustedPublishingDomain>(trustedPublishingDomain.Id.Parent, QueryScope.OneLevel, null, null, 0);
			oldDefaultTPD = source.FirstOrDefault((RMSTrustedPublishingDomain tpd) => tpd.Default);
			ImportRmsTrustedPublishingDomain.ChangeDefaultTPDAndUpdateIrmConfigData(irmConfiguration, trustedPublishingDomain, oldDefaultTPD);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x001063E0 File Offset: 0x001045E0
		internal static void ChangeDefaultTPDAndUpdateIrmConfigData(IRMConfiguration irmConfiguration, RMSTrustedPublishingDomain trustedPublishingDomain, RMSTrustedPublishingDomain oldDefaultTPD)
		{
			if (oldDefaultTPD != null)
			{
				oldDefaultTPD.Default = false;
			}
			irmConfiguration.ServiceLocation = trustedPublishingDomain.ExtranetCertificationUrl;
			irmConfiguration.PublishingLocation = new Uri(RMUtil.ConvertUriToPublishUrl(trustedPublishingDomain.ExtranetCertificationUrl), UriKind.Absolute);
			if (!string.IsNullOrEmpty(trustedPublishingDomain.PrivateKey))
			{
				SharedServerBoxRacIdentityGenerator sharedServerBoxRacIdentityGenerator = new SharedServerBoxRacIdentityGenerator(trustedPublishingDomain.SLCCertChain, oldDefaultTPD, irmConfiguration.SharedServerBoxRacIdentity);
				irmConfiguration.SharedServerBoxRacIdentity = sharedServerBoxRacIdentityGenerator.GenerateSharedKey();
				return;
			}
			irmConfiguration.SharedServerBoxRacIdentity = null;
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x00106450 File Offset: 0x00104650
		private MultiValuedProperty<string> CompressAndUpdateTemplates(RMSTrustedPublishingDomain dataObject, string[] templatesFromTpd, bool refreshTemplates, RmsTemplateType templateType, out ImportRmsTrustedPublishingDomainResult rmsTpdResult)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			rmsTpdResult = new ImportRmsTrustedPublishingDomainResult(dataObject);
			if (refreshTemplates)
			{
				Dictionary<Guid, RmsTemplate> existingTemplateEntries = ImportRmsTrustedPublishingDomain.GetExistingTemplateEntries(dataObject.RMSTemplates);
				if (templatesFromTpd != null)
				{
					for (int i = 0; i < templatesFromTpd.Length; i++)
					{
						RmsTemplate rmsTemplate = RmsTemplate.CreateServerTemplateFromTemplateDefinition(templatesFromTpd[i], templateType);
						if (existingTemplateEntries.ContainsKey(rmsTemplate.Id))
						{
							templateType = existingTemplateEntries[rmsTemplate.Id].Type;
							existingTemplateEntries.Remove(rmsTemplate.Id);
							rmsTpdResult.UpdatedTemplates.Add(rmsTemplate.Name);
						}
						else
						{
							rmsTpdResult.AddedTemplates.Add(rmsTemplate.Name);
						}
						multiValuedProperty.Add(RMUtil.CompressTemplate(templatesFromTpd[i], templateType));
					}
				}
				foreach (KeyValuePair<Guid, RmsTemplate> keyValuePair in existingTemplateEntries)
				{
					rmsTpdResult.RemovedTemplates.Add(keyValuePair.Value.Name);
				}
				if (dataObject.Default && rmsTpdResult.RemovedTemplates.Count > 0)
				{
					this.WriteWarning(Strings.WarningDeleteTemplate);
				}
			}
			else if (templatesFromTpd != null)
			{
				for (int j = 0; j < templatesFromTpd.Length; j++)
				{
					RmsTemplate rmsTemplate2 = RmsTemplate.CreateServerTemplateFromTemplateDefinition(templatesFromTpd[j], templateType);
					rmsTpdResult.AddedTemplates.Add(rmsTemplate2.Name);
					multiValuedProperty.Add(RMUtil.CompressTemplate(templatesFromTpd[j], templateType));
				}
			}
			if (rmsTpdResult.AddedTemplates.Count > 0 && dataObject.Default && templateType == RmsTemplateType.Archived)
			{
				this.WriteWarning(Strings.WarningMarkNewTemplatesAsDistributedForCreatingProtectionRules(dataObject.Name));
			}
			if (multiValuedProperty.Count != 0)
			{
				return multiValuedProperty;
			}
			return null;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x001065FC File Offset: 0x001047FC
		private static Dictionary<Guid, RmsTemplate> GetExistingTemplateEntries(MultiValuedProperty<string> existingCompressedTemplates)
		{
			if (MultiValuedPropertyBase.IsNullOrEmpty(existingCompressedTemplates))
			{
				return new Dictionary<Guid, RmsTemplate>(0);
			}
			Dictionary<Guid, RmsTemplate> dictionary = new Dictionary<Guid, RmsTemplate>(existingCompressedTemplates.Count);
			foreach (string encodedTemplate in existingCompressedTemplates)
			{
				RmsTemplateType type;
				string templateXrml = RMUtil.DecompressTemplate(encodedTemplate, out type);
				RmsTemplate rmsTemplate = RmsTemplate.CreateServerTemplateFromTemplateDefinition(templateXrml, type);
				dictionary[rmsTemplate.Id] = rmsTemplate;
			}
			return dictionary;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x00106684 File Offset: 0x00104884
		private IRMConfiguration ReadIrmConfiguration()
		{
			return IRMConfiguration.Read((IConfigurationSession)base.DataSession);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x00106698 File Offset: 0x00104898
		private XrmlCertificateChain ReenrollSlcCertificateChain(XrmlCertificateChain slcCertificateChain, object errorTarget)
		{
			XrmlCertificateChain result;
			try
			{
				result = Enroller.Reenroll(slcCertificateChain);
			}
			catch (ArgumentException e)
			{
				ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(e);
				base.WriteError(new FailedToReEnrollTPDException(e), ExchangeErrorCategory.Client, errorTarget);
				result = null;
			}
			return result;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x001066DC File Offset: 0x001048DC
		private TrustedDocDomain ParseTpdFileData(SecureString password, byte[] fileData)
		{
			TrustedDocDomain result;
			try
			{
				result = TrustedPublishingDomainParser.Parse(password, fileData);
			}
			catch (TrustedPublishingDomainParser.ParseFailedException ex)
			{
				ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
				base.WriteError(new FailedToDeSerializeImportedTrustedPublishingDomainException(ex), ExchangeErrorCategory.Client, base.Name);
				result = null;
			}
			return result;
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x00106728 File Offset: 0x00104928
		private TrustedDocDomain GetTpdFromRmsOnline(Uri rmsOnlineKeySharingLocation)
		{
			RmsUtil.ThrowIfParameterNull(rmsOnlineKeySharingLocation, "rmsOnlineKeySharingLocation");
			this.ThrowIfOrganizationNotSpecified();
			RmsOnlineTpdImporter rmsOnlineTpdImporter = new RmsOnlineTpdImporter(rmsOnlineKeySharingLocation, this.RMSOnlineAuthCertSubjectNameOverride ?? RmsOnlineConstants.AuthenticationCertificateSubjectDistinguishedName);
			TrustedDocDomain result;
			try
			{
				Guid guid = this.RMSOnlineOrgOverride;
				if (Guid.Empty == guid)
				{
					guid = RmsUtil.GetExternalDirectoryOrgIdThrowOnFailure(this.ConfigurationSession, base.CurrentOrganizationId);
				}
				TrustedDocDomain trustedDocDomain = rmsOnlineTpdImporter.Import(guid);
				if (!this.RefreshTemplates)
				{
					this.IntranetLicensingUrl = rmsOnlineTpdImporter.IntranetLicensingUrl;
					this.ExtranetLicensingUrl = rmsOnlineTpdImporter.ExtranetLicensingUrl;
					this.IntranetCertificationUrl = rmsOnlineTpdImporter.IntranetCertificationUrl;
					this.ExtranetCertificationUrl = rmsOnlineTpdImporter.ExtranetCertificationUrl;
				}
				result = trustedDocDomain;
			}
			catch (ImportTpdException ex)
			{
				ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
				base.WriteError(new FailedToGetTrustedPublishingDomainFromRmsOnlineException(ex, ex.InnerException), ExchangeErrorCategory.Client, base.Name);
				result = null;
			}
			return result;
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x00106808 File Offset: 0x00104A08
		private static void WriteFailureToEventLog(Exception e)
		{
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ImportTpdFailure, new string[]
			{
				e.ToString()
			});
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x00106838 File Offset: 0x00104A38
		public ImportRmsTrustedPublishingDomain()
		{
			this.impl = new Lazy<ImportRmsTrustedPublishingDomain.Impl>(() => ImportRmsTrustedPublishingDomain.CreateImpl(this));
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x00106871 File Offset: 0x00104A71
		protected override IConfigurable PrepareDataObject()
		{
			return this.impl.Value.PrepareDataObject(() => this.<>n__FabricatedMethod6());
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x00106897 File Offset: 0x00104A97
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.impl.Value.InternalValidate(delegate
			{
				this.<>n__FabricatedMethod8();
			});
			TaskLogger.LogExit();
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x001068C7 File Offset: 0x00104AC7
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.impl.Value.InternalProcessRecord(delegate
			{
				this.<>n__FabricatedMethoda();
			});
			TaskLogger.LogExit();
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x001068F8 File Offset: 0x00104AF8
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			this.impl.Value.WriteResut(dataObject, delegate(IConfigurable da)
			{
				this.<>n__FabricatedMethodc(da);
			});
			TaskLogger.LogExit();
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x00106924 File Offset: 0x00104B24
		private static ImportRmsTrustedPublishingDomain.Impl CreateImpl(ImportRmsTrustedPublishingDomain cmdlet)
		{
			if (cmdlet.RMSOnlineConfig == null)
			{
				return new ImportRmsTrustedPublishingDomain.OnPremImpl
				{
					cmdlet = cmdlet
				};
			}
			ExAssert.RetailAssert(!cmdlet.RefreshTemplates, "When importing from RMS Online, RefreshTemplates should always be false");
			return new ImportRmsTrustedPublishingDomain.RMSOnlineImpl
			{
				cmdlet = cmdlet
			};
		}

		// Token: 0x040028B9 RID: 10425
		private const string ImportFromFileParameterSetName = "ImportFromFile";

		// Token: 0x040028BA RID: 10426
		private const string IntranetLicensingUrlParameterSetName = "IntranetLicensingUrl";

		// Token: 0x040028BB RID: 10427
		private const string RefreshTemplatesParameterSetName = "RefreshTemplates";

		// Token: 0x040028BC RID: 10428
		private const string ImportFromRMSOnlineParameterSetName = "RMSOnline";

		// Token: 0x040028BD RID: 10429
		private const string ImportFromRMSOnline2ParameterSetName = "RMSOnline2";

		// Token: 0x040028BE RID: 10430
		internal const string RMSOnlinePsuedoDataObjectName = "RMS Online";

		// Token: 0x040028BF RID: 10431
		internal const string RMSOnlineAuthorTest_TrustedRootHierarchy = "TrustedRootHierarchy";

		// Token: 0x040028C0 RID: 10432
		internal const string RMSOnlineAuthorTest_KeyProtectionCertificate = "KeyProtectionCertificate";

		// Token: 0x040028C1 RID: 10433
		internal const string RMSOnlineAuthorTest_KeyProtectionCertificatePassword = "KeyProtectionCertificatePassword";

		// Token: 0x040028C2 RID: 10434
		private Lazy<ImportRmsTrustedPublishingDomain.Impl> impl;

		// Token: 0x0200070D RID: 1805
		private abstract class Impl
		{
			// Token: 0x1700139E RID: 5022
			// (get) Token: 0x0600404F RID: 16463 RVA: 0x0010698F File Offset: 0x00104B8F
			// (set) Token: 0x06004050 RID: 16464 RVA: 0x00106997 File Offset: 0x00104B97
			internal ImportRmsTrustedPublishingDomain cmdlet { get; set; }

			// Token: 0x06004051 RID: 16465
			internal abstract IConfigurable PrepareDataObject(Func<IConfigurable> basePrepareDataObject);

			// Token: 0x06004052 RID: 16466
			internal abstract void InternalValidate(Action baseInternalValidate);

			// Token: 0x06004053 RID: 16467
			internal abstract void InternalProcessRecord(Action baseInternalProcessRecord);

			// Token: 0x06004054 RID: 16468
			internal abstract void WriteResut(IConfigurable dataObject, Action<IConfigurable> baseWriteResult);
		}

		// Token: 0x0200070E RID: 1806
		private class OnPremImpl : ImportRmsTrustedPublishingDomain.Impl
		{
			// Token: 0x06004056 RID: 16470 RVA: 0x001069A8 File Offset: 0x00104BA8
			internal override IConfigurable PrepareDataObject(Func<IConfigurable> basePrepareDataObject)
			{
				RMSTrustedPublishingDomain rmstrustedPublishingDomain;
				if (base.cmdlet.RefreshTemplates)
				{
					RMSTrustedPublishingDomain[] array = ((IConfigurationSession)base.cmdlet.DataSession).Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.cmdlet.Name), null, 1);
					base.cmdlet.ThrowIfExistingTpdNotFound(array);
					rmstrustedPublishingDomain = array[0];
				}
				else
				{
					if (base.cmdlet.RMSOnline)
					{
						base.cmdlet.UpdateTpdNameForRmsOnline();
					}
					rmstrustedPublishingDomain = (RMSTrustedPublishingDomain)basePrepareDataObject();
					rmstrustedPublishingDomain.SetId((IConfigurationSession)base.cmdlet.DataSession, base.cmdlet.Name);
				}
				return rmstrustedPublishingDomain;
			}

			// Token: 0x06004057 RID: 16471 RVA: 0x00106A54 File Offset: 0x00104C54
			internal override void InternalValidate(Action baseInternalValidate)
			{
				baseInternalValidate();
				if (base.cmdlet.HasErrors)
				{
					return;
				}
				IRMConfiguration irmconfiguration = base.cmdlet.ReadIrmConfiguration();
				if (base.cmdlet.RMSOnline)
				{
					base.cmdlet.ThrowIfRmsOnlinePreRequisitesNotMet(irmconfiguration);
					this.trustedDoc = base.cmdlet.GetTpdFromRmsOnline(irmconfiguration.RMSOnlineKeySharingLocation);
				}
				else
				{
					this.trustedDoc = base.cmdlet.ParseTpdFileData(base.cmdlet.Password, base.cmdlet.FileData);
				}
				object obj = null;
				try
				{
					TpdValidator tpdValidator = new TpdValidator(irmconfiguration.InternalLicensingEnabled, base.cmdlet.IntranetLicensingUrl, base.cmdlet.ExtranetLicensingUrl, base.cmdlet.IntranetCertificationUrl, base.cmdlet.ExtranetCertificationUrl, base.cmdlet.RMSOnline, base.cmdlet.Default, base.cmdlet.RefreshTemplates);
					this.dkmEncryptedPrivateKey = tpdValidator.ValidateTpdSuitableForImport(this.trustedDoc, base.cmdlet.Name, out obj, base.cmdlet.ConfigurationSession, base.cmdlet.DataObject.KeyId, base.cmdlet.DataObject.KeyIdType, base.cmdlet.DataObject.IntranetLicensingUrl, base.cmdlet.DataObject.ExtranetLicensingUrl, base.cmdlet.Password);
					if (base.cmdlet.RMSOnline && !base.cmdlet.RefreshTemplates && RmsUtil.TPDExists(base.cmdlet.ConfigurationSession, this.trustedDoc.m_ttdki.strID, this.trustedDoc.m_ttdki.strIDType))
					{
						base.cmdlet.WriteWarning(Strings.TpdAlreadyImported);
						this.SkipImport = true;
					}
				}
				catch (LocalizedException ex)
				{
					ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
					base.cmdlet.WriteError(ex, ExchangeErrorCategory.Client, obj ?? base.cmdlet.Name);
				}
			}

			// Token: 0x06004058 RID: 16472 RVA: 0x00106C64 File Offset: 0x00104E64
			internal override void InternalProcessRecord(Action baseInternalProcessRecord)
			{
				if (!this.SkipImport)
				{
					RMSTrustedPublishingDomain dataObject = base.cmdlet.DataObject;
					IRMConfiguration irmconfiguration = null;
					RMSTrustedPublishingDomain rmstrustedPublishingDomain = null;
					if (!base.cmdlet.RefreshTemplates)
					{
						if (base.cmdlet.Default)
						{
							dataObject.Default = true;
						}
						else
						{
							dataObject.Default = !RmsUtil.TPDExists(base.cmdlet.ConfigurationSession, null);
						}
						irmconfiguration = base.cmdlet.ReadIrmConfiguration();
						dataObject.CSPType = this.trustedDoc.m_ttdki.nCSPType;
						dataObject.CSPName = this.trustedDoc.m_ttdki.strCSPName;
						dataObject.KeyContainerName = this.trustedDoc.m_ttdki.strKeyContainerName;
						dataObject.KeyNumber = this.trustedDoc.m_ttdki.nKeyNumber;
						dataObject.KeyId = this.trustedDoc.m_ttdki.strID;
						dataObject.KeyIdType = this.trustedDoc.m_ttdki.strIDType;
						if (!string.IsNullOrEmpty(this.dkmEncryptedPrivateKey))
						{
							dataObject.PrivateKey = this.dkmEncryptedPrivateKey;
						}
						XrmlCertificateChain xrmlCertificateChain = base.cmdlet.ReenrollSlcCertificateChain(new XrmlCertificateChain(this.trustedDoc.m_strLicensorCertChain), base.cmdlet.Name);
						dataObject.SLCCertChain = RMUtil.CompressSLCCertificateChain(xrmlCertificateChain.ToStringArray());
						dataObject.IntranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(base.cmdlet.IntranetLicensingUrl);
						dataObject.ExtranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(base.cmdlet.ExtranetLicensingUrl);
						dataObject.IntranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(base.cmdlet.IntranetCertificationUrl ?? base.cmdlet.IntranetLicensingUrl);
						dataObject.ExtranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(base.cmdlet.ExtranetCertificationUrl ?? base.cmdlet.ExtranetLicensingUrl);
						if (dataObject.Default)
						{
							try
							{
								ImportRmsTrustedPublishingDomain.ChangeDefaultTPDAndUpdateIrmConfigData((IConfigurationSession)base.cmdlet.DataSession, irmconfiguration, dataObject, out rmstrustedPublishingDomain);
								irmconfiguration.ServerCertificatesVersion++;
							}
							catch (RightsManagementServerException ex)
							{
								ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
								base.cmdlet.WriteError(new FailedToGenerateSharedKeyException(ex), ExchangeErrorCategory.Client, base.cmdlet.Name);
							}
						}
						if (!irmconfiguration.LicensingLocation.Contains(dataObject.ExtranetLicensingUrl))
						{
							irmconfiguration.LicensingLocation.Add(dataObject.ExtranetLicensingUrl);
						}
						if (!irmconfiguration.LicensingLocation.Contains(dataObject.IntranetLicensingUrl))
						{
							irmconfiguration.LicensingLocation.Add(dataObject.IntranetLicensingUrl);
						}
					}
					dataObject.RMSTemplates = base.cmdlet.CompressAndUpdateTemplates(dataObject, this.trustedDoc.m_astrRightsTemplates, base.cmdlet.RefreshTemplates, base.cmdlet.RMSOnline ? RmsTemplateType.Distributed : RmsTemplateType.Archived, out this.rmsTpdResult);
					if (rmstrustedPublishingDomain != null)
					{
						base.cmdlet.WriteWarning(Strings.WarningChangeDefaultTPD(rmstrustedPublishingDomain.Name, dataObject.Name));
						base.cmdlet.DataSession.Save(rmstrustedPublishingDomain);
					}
					if (irmconfiguration != null)
					{
						base.cmdlet.DataSession.Save(irmconfiguration);
					}
					baseInternalProcessRecord();
				}
			}

			// Token: 0x06004059 RID: 16473 RVA: 0x00106F78 File Offset: 0x00105178
			internal override void WriteResut(IConfigurable dataObject, Action<IConfigurable> baseWriteResult)
			{
				baseWriteResult(this.rmsTpdResult);
			}

			// Token: 0x040028C5 RID: 10437
			private TrustedDocDomain trustedDoc;

			// Token: 0x040028C6 RID: 10438
			private string dkmEncryptedPrivateKey;

			// Token: 0x040028C7 RID: 10439
			private bool SkipImport;

			// Token: 0x040028C8 RID: 10440
			private ImportRmsTrustedPublishingDomainResult rmsTpdResult;
		}

		// Token: 0x0200070F RID: 1807
		private class RMSOnlineImpl : ImportRmsTrustedPublishingDomain.Impl
		{
			// Token: 0x0600405B RID: 16475 RVA: 0x00106F90 File Offset: 0x00105190
			internal override IConfigurable PrepareDataObject(Func<IConfigurable> basePrepareDataObject)
			{
				RMSTrustedPublishingDomain rmstrustedPublishingDomain = (RMSTrustedPublishingDomain)basePrepareDataObject();
				rmstrustedPublishingDomain.SetId((IConfigurationSession)base.cmdlet.DataSession, "RMS Online");
				return rmstrustedPublishingDomain;
			}

			// Token: 0x0600405C RID: 16476 RVA: 0x00107038 File Offset: 0x00105238
			internal override void InternalValidate(Action baseInternalValidate)
			{
				baseInternalValidate();
				if (base.cmdlet.HasErrors)
				{
					return;
				}
				IRMConfiguration irmconfiguration = base.cmdlet.ReadIrmConfiguration();
				Guid parsedResultNotUsed;
				if (base.cmdlet.RMSOnlineKeys.Keys.Cast<object>().All((object key) => key != null && Guid.TryParse(key.ToString(), out parsedResultNotUsed)))
				{
					if (base.cmdlet.RMSOnlineKeys.Values.Cast<object>().All((object value) => value is byte[] && value != null))
					{
						goto IL_A9;
					}
				}
				base.cmdlet.WriteError(new RmsOnlineFailedToValidateKeys(), ExchangeErrorCategory.Client, base.cmdlet.RMSOnlineKeys);
				IL_A9:
				ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOCompanyConfigurationKeyData trustedDocDomain = new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOCompanyConfigurationKeyData(RmsUtil.GetExternalDirectoryOrgIdThrowOnFailure(base.cmdlet.ConfigurationSession, base.cmdlet.CurrentOrganizationId), from DictionaryEntry de in base.cmdlet.RMSOnlineKeys
				select new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOTenantKey(Guid.Parse((string)de.Key), (byte[])de.Value), base.cmdlet.RMSOnlineConfig);
				this.trustedDocs = new List<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain>(from trustedDoc in this.GetTpdsFromRmsOnline(trustedDocDomain)
				select new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain
				{
					trustedDocDomain = trustedDoc
				});
				object obj = null;
				try
				{
					foreach (ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain intermediateTrustedDocDomain in this.trustedDocs)
					{
						TpdValidator tpdValidator = new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOnlineTpdValidator(irmconfiguration.InternalLicensingEnabled, this.configurationInfo, (TrustedRootHierarchy)base.cmdlet.RMSOnlineAuthorTest["TrustedRootHierarchy"]);
						intermediateTrustedDocDomain.dkmEncryptedPrivateKey = tpdValidator.ValidateTpdSuitableForImport(intermediateTrustedDocDomain.trustedDocDomain, "RMS Online", out obj, base.cmdlet.ConfigurationSession, null, null, null, null, null);
					}
				}
				catch (LocalizedException ex)
				{
					ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
					base.cmdlet.WriteError(ex, ExchangeErrorCategory.Client, obj ?? "RMS Online");
				}
			}

			// Token: 0x0600405D RID: 16477 RVA: 0x001072CC File Offset: 0x001054CC
			internal override void InternalProcessRecord(Action baseInternalProcessRecord)
			{
				if (this.configurationInfo == null)
				{
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TenantConfigurationIsNull, new string[0]);
					base.cmdlet.WriteWarning(Strings.TenantConfigurationInfoIsNull);
					return;
				}
				bool flag;
				IRMConfiguration irmconfiguration = IRMConfiguration.Read((IConfigurationSession)base.cmdlet.DataSession, out flag);
				RMSTrustedPublishingDomain[] array = ((IConfigurationSession)base.cmdlet.DataSession).Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, null, null, 0);
				RMSTrustedPublishingDomain defaultTrustedPublishingDomain = array.FirstOrDefault((RMSTrustedPublishingDomain tpd) => tpd.Default);
				bool flag2 = defaultTrustedPublishingDomain != null && defaultTrustedPublishingDomain.IsRMSOnline;
				LinkedList<RMSTrustedPublishingDomain> linkedList = new LinkedList<RMSTrustedPublishingDomain>(from tpd in array
				where tpd.IsRMSOnline && tpd != defaultTrustedPublishingDomain
				select tpd);
				LinkedList<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult> linkedList2 = new LinkedList<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult>();
				ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOnlineUniqueADNamesEnumerator rmsOnlineUniqueDataObjectNames = new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOnlineUniqueADNamesEnumerator(array);
				foreach (ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain intermediateTrustedDoc in this.trustedDocs)
				{
					this.InternalProcessRecord_ProcessIntermediateTrustedDoc(irmconfiguration, defaultTrustedPublishingDomain, flag2, linkedList, linkedList2, rmsOnlineUniqueDataObjectNames, intermediateTrustedDoc);
				}
				if (this.configurationInfo.FunctionalState == 1 || defaultTrustedPublishingDomain == null)
				{
					irmconfiguration.InternalLicensingEnabled = (irmconfiguration.ClientAccessServerEnabled = (this.configurationInfo.FunctionalState == 1));
				}
				if (flag && linkedList2.Any<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult>())
				{
					base.cmdlet.DataSession.Save(irmconfiguration);
				}
				linkedList.All(delegate(RMSTrustedPublishingDomain tpd)
				{
					base.cmdlet.DataSession.Delete(tpd);
					return true;
				});
				linkedList2.All(delegate(ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult tpd)
				{
					tpd.domain.OrganizationId = base.cmdlet.CurrentOrganizationId;
					base.cmdlet.DataSession.Save(tpd.domain);
					base.cmdlet.WriteResult(tpd.result);
					return true;
				});
				if (flag2 && !this.trustedDocs.Any<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain>())
				{
					irmconfiguration.InternalLicensingEnabled = false;
					irmconfiguration.SharedServerBoxRacIdentity = null;
					irmconfiguration.PublishingLocation = null;
					irmconfiguration.ServiceLocation = null;
					irmconfiguration.LicensingLocation = null;
					base.cmdlet.DataSession.Delete(defaultTrustedPublishingDomain);
				}
				irmconfiguration.RMSOnlineVersion = (this.trustedDocs.Any<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain>() ? this.configurationInfo.DataVersions.Item2 : string.Empty);
				base.cmdlet.DataSession.Save(irmconfiguration);
			}

			// Token: 0x0600405E RID: 16478 RVA: 0x00107508 File Offset: 0x00105708
			private void InternalProcessRecord_ProcessIntermediateTrustedDoc(IRMConfiguration irmConfiguration, RMSTrustedPublishingDomain defaultTrustedPublishingDomain, bool defaultTrustedPublishingDomainIsRMSOnline, LinkedList<RMSTrustedPublishingDomain> existingRMSOTrustedPublishingDomainsToReuse, LinkedList<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult> updatedTrustedPublishingDomains, ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOnlineUniqueADNamesEnumerator rmsOnlineUniqueDataObjectNames, ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain intermediateTrustedDoc)
			{
				RMSTrustedPublishingDomain rmstrustedPublishingDomain = this.InternalProcessRecord_GetRMSTrustedPublishingDomainToUse(existingRMSOTrustedPublishingDomainsToReuse, rmsOnlineUniqueDataObjectNames);
				rmstrustedPublishingDomain.Default = (this.configurationInfo.FunctionalState == 1 && intermediateTrustedDoc == this.trustedDocs.First<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain>());
				this.InternalProcessRecord_InitializeTrustedPublishingDomainGenericProperties(intermediateTrustedDoc, rmstrustedPublishingDomain);
				if (rmstrustedPublishingDomain.Default)
				{
					this.InternalProcessRecord_ChangeDefaultTPDAndUpdateIrmConfigData(irmConfiguration, rmstrustedPublishingDomain, defaultTrustedPublishingDomain);
				}
				ImportRmsTrustedPublishingDomain.RMSOnlineImpl.AddIfNotContains<Uri>(irmConfiguration.LicensingLocation, rmstrustedPublishingDomain.ExtranetLicensingUrl);
				ImportRmsTrustedPublishingDomain.RMSOnlineImpl.AddIfNotContains<Uri>(irmConfiguration.LicensingLocation, rmstrustedPublishingDomain.IntranetLicensingUrl);
				ImportRmsTrustedPublishingDomainResult result;
				rmstrustedPublishingDomain.RMSTemplates = base.cmdlet.CompressAndUpdateTemplates(rmstrustedPublishingDomain, intermediateTrustedDoc.trustedDocDomain.m_astrRightsTemplates, false, RmsTemplateType.Distributed, out result);
				if (rmstrustedPublishingDomain.Default)
				{
					if (defaultTrustedPublishingDomainIsRMSOnline)
					{
						base.cmdlet.DataSession.Delete(defaultTrustedPublishingDomain);
					}
					else if (defaultTrustedPublishingDomain != null)
					{
						base.cmdlet.DataSession.Save(defaultTrustedPublishingDomain);
					}
				}
				updatedTrustedPublishingDomains.AddLast(new ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSTrustedPublishingDomainAndResult
				{
					domain = rmstrustedPublishingDomain,
					result = result
				});
			}

			// Token: 0x0600405F RID: 16479 RVA: 0x001075F8 File Offset: 0x001057F8
			private RMSTrustedPublishingDomain InternalProcessRecord_GetRMSTrustedPublishingDomainToUse(LinkedList<RMSTrustedPublishingDomain> existingRMSOTrustedPublishingDomainsToReuse, ImportRmsTrustedPublishingDomain.RMSOnlineImpl.RMSOnlineUniqueADNamesEnumerator rmsOnlineUniqueDataObjectNames)
			{
				RMSTrustedPublishingDomain rmstrustedPublishingDomain;
				if (existingRMSOTrustedPublishingDomainsToReuse.Any<RMSTrustedPublishingDomain>())
				{
					rmstrustedPublishingDomain = existingRMSOTrustedPublishingDomainsToReuse.First<RMSTrustedPublishingDomain>();
					existingRMSOTrustedPublishingDomainsToReuse.RemoveFirst();
				}
				else
				{
					rmstrustedPublishingDomain = new RMSTrustedPublishingDomain();
					rmstrustedPublishingDomain.SetId((IConfigurationSession)base.cmdlet.DataSession, rmsOnlineUniqueDataObjectNames.GetNext());
					rmstrustedPublishingDomain.Name = rmstrustedPublishingDomain.Id.Name;
				}
				return rmstrustedPublishingDomain;
			}

			// Token: 0x06004060 RID: 16480 RVA: 0x00107654 File Offset: 0x00105854
			private void InternalProcessRecord_InitializeTrustedPublishingDomainGenericProperties(ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain intermediateTrustedDoc, RMSTrustedPublishingDomain trustedPublishingDomain)
			{
				trustedPublishingDomain.CSPType = intermediateTrustedDoc.trustedDocDomain.m_ttdki.nCSPType;
				trustedPublishingDomain.CSPName = intermediateTrustedDoc.trustedDocDomain.m_ttdki.strCSPName;
				trustedPublishingDomain.KeyContainerName = intermediateTrustedDoc.trustedDocDomain.m_ttdki.strKeyContainerName;
				trustedPublishingDomain.KeyNumber = intermediateTrustedDoc.trustedDocDomain.m_ttdki.nKeyNumber;
				trustedPublishingDomain.KeyId = intermediateTrustedDoc.trustedDocDomain.m_ttdki.strID;
				trustedPublishingDomain.KeyIdType = intermediateTrustedDoc.trustedDocDomain.m_ttdki.strIDType;
				if (!string.IsNullOrEmpty(intermediateTrustedDoc.dkmEncryptedPrivateKey))
				{
					trustedPublishingDomain.PrivateKey = intermediateTrustedDoc.dkmEncryptedPrivateKey;
				}
				trustedPublishingDomain.SLCCertChain = RMUtil.CompressSLCCertificateChain(new XrmlCertificateChain(intermediateTrustedDoc.trustedDocDomain.m_strLicensorCertChain).ToStringArray());
				trustedPublishingDomain.IntranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(this.configurationInfo.LicensingIntranetDistributionPointUrl);
				trustedPublishingDomain.ExtranetLicensingUrl = RMUtil.ConvertUriToLicenseLocationDistributionPoint(this.configurationInfo.LicensingExtranetDistributionPointUrl);
				trustedPublishingDomain.IntranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(this.configurationInfo.CertificationIntranetDistributionPointUrl ?? this.configurationInfo.LicensingIntranetDistributionPointUrl);
				trustedPublishingDomain.ExtranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(this.configurationInfo.CertificationExtranetDistributionPointUrl ?? this.configurationInfo.LicensingExtranetDistributionPointUrl);
			}

			// Token: 0x06004061 RID: 16481 RVA: 0x00107794 File Offset: 0x00105994
			private void InternalProcessRecord_ChangeDefaultTPDAndUpdateIrmConfigData(IRMConfiguration irmConfiguration, RMSTrustedPublishingDomain trustedPublishingDomain, RMSTrustedPublishingDomain oldDefaultTPD)
			{
				try
				{
					ImportRmsTrustedPublishingDomain.ChangeDefaultTPDAndUpdateIrmConfigData(irmConfiguration, trustedPublishingDomain, oldDefaultTPD);
					irmConfiguration.ServerCertificatesVersion++;
				}
				catch (RightsManagementServerException ex)
				{
					ImportRmsTrustedPublishingDomain.WriteFailureToEventLog(ex);
					base.cmdlet.WriteError(new FailedToGenerateSharedKeyException(ex), ExchangeErrorCategory.Client, "RMS Online");
				}
			}

			// Token: 0x06004062 RID: 16482 RVA: 0x001077F0 File Offset: 0x001059F0
			internal override void WriteResut(IConfigurable dataObject, Action<IConfigurable> baseWriteResult)
			{
				baseWriteResult(dataObject);
			}

			// Token: 0x06004063 RID: 16483 RVA: 0x00107AE8 File Offset: 0x00105CE8
			private IEnumerable<TrustedDocDomain> GetTpdsFromRmsOnline(ICompanyConfigurationKeyData trustedDocDomain)
			{
				ITenantTpdAndConfigurationAuthor author = (base.cmdlet.RMSOnlineAuthorTest["KeyProtectionCertificate"] != null) ? TenantTpdAndConfigurationAuthorFactory.CreateTenantTpdAndConfigurationAuthor(new X509Certificate2(Convert.FromBase64String((string)base.cmdlet.RMSOnlineAuthorTest["KeyProtectionCertificate"]), (string)base.cmdlet.RMSOnlineAuthorTest["KeyProtectionCertificatePassword"])) : TenantTpdAndConfigurationAuthorFactory.CreateTenantTpdAndConfigurationAuthor();
				try
				{
					this.configurationInfo = author.GetTrustedPublishingDomain(trustedDocDomain);
				}
				catch (HardwareSecurityModuleEncryptedKeyException ex)
				{
					this.configurationInfo = null;
					ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_SkipHSMEncryptedTpd, new string[]
					{
						ex.ToString()
					});
					base.cmdlet.WriteWarning(Strings.TpdIsHSMEncrypted);
					yield break;
				}
				if (this.configurationInfo.ActivePublishingDomain != null)
				{
					yield return ImportRmsTrustedPublishingDomain.RMSOnlineImpl.ConvertFromRmsOnlineTrustedDocDomain(this.configurationInfo.ActivePublishingDomain);
					if (this.configurationInfo.ArchivedPublishingDomains != null)
					{
						foreach (ITrustedDocDomain rmsoTpd in this.configurationInfo.ArchivedPublishingDomains)
						{
							yield return ImportRmsTrustedPublishingDomain.RMSOnlineImpl.ConvertFromRmsOnlineTrustedDocDomain(rmsoTpd);
						}
					}
				}
				yield break;
			}

			// Token: 0x06004064 RID: 16484 RVA: 0x00107B0C File Offset: 0x00105D0C
			private static TrustedDocDomain ConvertFromRmsOnlineTrustedDocDomain(ITrustedDocDomain rmsoTPD)
			{
				RmsUtil.ThrowIfParameterNull(rmsoTPD, "rmsoTPD");
				return new TrustedDocDomain
				{
					m_ttdki = ImportRmsTrustedPublishingDomain.RMSOnlineImpl.ConvertFromRmsOnlineKeyInformation(rmsoTPD.KeyInfo),
					m_strLicensorCertChain = rmsoTPD.LicensorCertChain,
					m_astrRightsTemplates = rmsoTPD.RightsTemplates
				};
			}

			// Token: 0x06004065 RID: 16485 RVA: 0x00107B54 File Offset: 0x00105D54
			private static KeyInformation ConvertFromRmsOnlineKeyInformation(IKeyInformation rmsoKeyInfo)
			{
				RmsUtil.ThrowIfParameterNull(rmsoKeyInfo, "rmsoKeyInfo");
				return new KeyInformation
				{
					strID = rmsoKeyInfo.ID,
					strIDType = rmsoKeyInfo.IdType,
					nCSPType = rmsoKeyInfo.CSPType,
					strCSPName = rmsoKeyInfo.CSPName,
					strKeyContainerName = rmsoKeyInfo.KeyContainerName,
					nKeyNumber = rmsoKeyInfo.KeyNumber,
					strEncryptedPrivateKey = Convert.ToBase64String(rmsoKeyInfo.PrivateKey)
				};
			}

			// Token: 0x06004066 RID: 16486 RVA: 0x00107BCC File Offset: 0x00105DCC
			private static void AddIfNotContains<T>(ICollection<T> collection, T item)
			{
				if (!collection.Contains(item))
				{
					collection.Add(item);
				}
			}

			// Token: 0x040028C9 RID: 10441
			private List<ImportRmsTrustedPublishingDomain.RMSOnlineImpl.IntermediateTrustedDocDomain> trustedDocs;

			// Token: 0x040028CA RID: 10442
			private ITenantTpdAndConfigurationInfo configurationInfo;

			// Token: 0x040028CB RID: 10443
			private static readonly Regex reRMSOnlineUniqueADName = new Regex("^RMS Online - (\\d{1,2})$", RegexOptions.Compiled);

			// Token: 0x02000710 RID: 1808
			private class IntermediateTrustedDocDomain
			{
				// Token: 0x040028D0 RID: 10448
				internal TrustedDocDomain trustedDocDomain;

				// Token: 0x040028D1 RID: 10449
				internal string dkmEncryptedPrivateKey;
			}

			// Token: 0x02000711 RID: 1809
			private class RMSOTenantKey : ITenantKey
			{
				// Token: 0x06004070 RID: 16496 RVA: 0x00107C00 File Offset: 0x00105E00
				internal RMSOTenantKey(Guid keyIdentifier, byte[] key)
				{
					this.KeyIdentifier = keyIdentifier;
					this.Key = key;
				}

				// Token: 0x1700139F RID: 5023
				// (get) Token: 0x06004071 RID: 16497 RVA: 0x00107C16 File Offset: 0x00105E16
				// (set) Token: 0x06004072 RID: 16498 RVA: 0x00107C1E File Offset: 0x00105E1E
				public Guid KeyIdentifier { get; private set; }

				// Token: 0x170013A0 RID: 5024
				// (get) Token: 0x06004073 RID: 16499 RVA: 0x00107C27 File Offset: 0x00105E27
				// (set) Token: 0x06004074 RID: 16500 RVA: 0x00107C2F File Offset: 0x00105E2F
				public byte[] Key { get; private set; }
			}

			// Token: 0x02000712 RID: 1810
			private class RMSOCompanyConfigurationKeyData : ICompanyConfigurationKeyData
			{
				// Token: 0x06004075 RID: 16501 RVA: 0x00107C38 File Offset: 0x00105E38
				internal RMSOCompanyConfigurationKeyData(Guid orgId, IEnumerable<ITenantKey> keys, byte[] configuration)
				{
					this.TenantMsodsId = orgId;
					this.TenantKeys = keys.ToArray<ITenantKey>();
					this.TenantConfiguration = configuration;
				}

				// Token: 0x170013A1 RID: 5025
				// (get) Token: 0x06004076 RID: 16502 RVA: 0x00107C5A File Offset: 0x00105E5A
				// (set) Token: 0x06004077 RID: 16503 RVA: 0x00107C62 File Offset: 0x00105E62
				public Guid TenantMsodsId { get; private set; }

				// Token: 0x170013A2 RID: 5026
				// (get) Token: 0x06004078 RID: 16504 RVA: 0x00107C6B File Offset: 0x00105E6B
				// (set) Token: 0x06004079 RID: 16505 RVA: 0x00107C73 File Offset: 0x00105E73
				public ITenantKey[] TenantKeys { get; private set; }

				// Token: 0x170013A3 RID: 5027
				// (get) Token: 0x0600407A RID: 16506 RVA: 0x00107C7C File Offset: 0x00105E7C
				// (set) Token: 0x0600407B RID: 16507 RVA: 0x00107C84 File Offset: 0x00105E84
				public byte[] TenantConfiguration { get; private set; }
			}

			// Token: 0x02000714 RID: 1812
			private class RMSOnlineTpdValidator : TpdValidator
			{
				// Token: 0x06004083 RID: 16515 RVA: 0x00107FD4 File Offset: 0x001061D4
				public RMSOnlineTpdValidator(bool internalLicensingEnabled, ITenantTpdAndConfigurationInfo configurationInfo, TrustedRootHierarchy trustedRootHierarchy) : base(internalLicensingEnabled, configurationInfo.LicensingIntranetDistributionPointUrl, configurationInfo.LicensingExtranetDistributionPointUrl, configurationInfo.CertificationIntranetDistributionPointUrl, configurationInfo.CertificationExtranetDistributionPointUrl, new SwitchParameter(true), new SwitchParameter(true), new SwitchParameter(false))
				{
					this.trustedRootHierarchy = trustedRootHierarchy;
				}

				// Token: 0x06004084 RID: 16516 RVA: 0x00108019 File Offset: 0x00106219
				protected override byte[] DecryptPrivateKey(KeyInformation keyInfo, SecureString tpdFilePassword)
				{
					return Convert.FromBase64String(keyInfo.strEncryptedPrivateKey);
				}

				// Token: 0x06004085 RID: 16517 RVA: 0x00108026 File Offset: 0x00106226
				protected override TrustedPublishingDomainImportUtilities CreateTpdImportUtilities(TrustedDocDomain tpd, TrustedPublishingDomainPrivateKeyProvider privateKeyProvider)
				{
					return new TrustedPublishingDomainImportUtilities(this.trustedRootHierarchy, new XrmlCertificateChain(tpd.m_strLicensorCertChain), privateKeyProvider, TraceLevel.Error, int.MaxValue);
				}

				// Token: 0x040028DF RID: 10463
				private TrustedRootHierarchy trustedRootHierarchy;
			}

			// Token: 0x02000715 RID: 1813
			private class RMSTrustedPublishingDomainAndResult
			{
				// Token: 0x040028E0 RID: 10464
				internal RMSTrustedPublishingDomain domain;

				// Token: 0x040028E1 RID: 10465
				internal ImportRmsTrustedPublishingDomainResult result;
			}

			// Token: 0x02000716 RID: 1814
			private class RMSOnlineUniqueADNamesEnumerator
			{
				// Token: 0x06004087 RID: 16519 RVA: 0x00108378 File Offset: 0x00106578
				internal RMSOnlineUniqueADNamesEnumerator(RMSTrustedPublishingDomain[] existingTrustedPublishingDomains)
				{
					this.existingIds = new Lazy<HashSet<int>>(() => new HashSet<int>(from tpd in existingTrustedPublishingDomains
					let m = ImportRmsTrustedPublishingDomain.RMSOnlineImpl.reRMSOnlineUniqueADName.Match(tpd.Name)
					where m.Success
					let num = int.Parse(m.Groups[1].Value)
					select num));
				}

				// Token: 0x06004088 RID: 16520 RVA: 0x001083C0 File Offset: 0x001065C0
				internal string GetNext()
				{
					while (this.existingIds.Value.Contains(this.index))
					{
						this.index++;
					}
					return "RMS Online - " + this.index++.ToString();
				}

				// Token: 0x040028E2 RID: 10466
				private Lazy<HashSet<int>> existingIds;

				// Token: 0x040028E3 RID: 10467
				private int index = 1;
			}
		}
	}
}
