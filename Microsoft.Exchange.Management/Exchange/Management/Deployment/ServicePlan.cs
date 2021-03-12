using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000241 RID: 577
	[XmlRoot(ElementName = "Configuration", IsNullable = false)]
	[Serializable]
	public sealed class ServicePlan
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x00055DB5 File Offset: 0x00053FB5
		internal ServicePlan()
		{
			this.mailboxPlans = new List<ServicePlan.MailboxPlan>();
			this.organization = new ServicePlan.OrganizationSettings();
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00055DD3 File Offset: 0x00053FD3
		internal bool IsValid
		{
			get
			{
				return this.Validate().Count == 0;
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00055DE4 File Offset: 0x00053FE4
		public List<ValidationError> Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			list.AddRange(this.ValidateFeaturesAllowedForSKU());
			list.AddRange(this.ValidateDependencies());
			return list;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00055E18 File Offset: 0x00054018
		internal static List<ValidationError> ValidateFileSchema(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("FileName");
			}
			List<ValidationError> list = new List<ValidationError>();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			List<ValidationError> result;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ServicePlans.xsd"))
			{
				xmlReaderSettings.Schemas.Add(null, XmlReader.Create(manifestResourceStream));
				xmlReaderSettings.ValidationEventHandler += delegate(object _, ValidationEventArgs e)
				{
					throw e.Exception;
				};
				string path = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\ServicePlans");
				string path2 = Path.Combine(path, fileName + ".servicePlan");
				using (Stream stream = File.Open(path2, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
					{
						try
						{
							while (xmlReader.Read())
							{
							}
						}
						catch (XmlSchemaException ex)
						{
							list.Add(new ServicePlanSchemaValidationError(ex.Message));
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00055F54 File Offset: 0x00054154
		internal List<ValidationError> ValidateFeaturesAllowedForSKU()
		{
			List<ValidationError> list = new List<ValidationError>();
			list.AddRange(this.Organization.ValidateFeaturesAllowedForSKU());
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				list.AddRange(mailboxPlan.ValidateFeaturesAllowedForSKU());
			}
			return list;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00055FC4 File Offset: 0x000541C4
		internal List<ValidationError> ValidateDependencies()
		{
			List<ValidationError> list = new List<ValidationError>();
			list.AddRange(this.Organization.ValidateDependencies(this));
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				list.AddRange(mailboxPlan.ValidateDependencies(this));
			}
			return list;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00056038 File Offset: 0x00054238
		internal void FixDependencies()
		{
			int num = 0;
			while (!this.IsValid && num++ < 10)
			{
				this.Organization.FixDependencies(this);
				foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
				{
					mailboxPlan.FixDependencies(this);
				}
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000560AC File Offset: 0x000542AC
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000560B4 File Offset: 0x000542B4
		[XmlAttribute]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000560BD File Offset: 0x000542BD
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x000560C5 File Offset: 0x000542C5
		public ServicePlan.OrganizationSettings Organization
		{
			get
			{
				return this.organization;
			}
			set
			{
				this.organization = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x000560CE File Offset: 0x000542CE
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x000560D6 File Offset: 0x000542D6
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000560DF File Offset: 0x000542DF
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x000560E7 File Offset: 0x000542E7
		public List<ServicePlan.MailboxPlan> MailboxPlans
		{
			get
			{
				return this.mailboxPlans;
			}
			set
			{
				this.mailboxPlans = value;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000560F0 File Offset: 0x000542F0
		public ServicePlan.MailboxPlan GetMailboxPlanByName(string name)
		{
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				if (mailboxPlan.Name == name)
				{
					return mailboxPlan;
				}
			}
			return null;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00056154 File Offset: 0x00054354
		public static ServicePlan LoadFromFile(string filePath)
		{
			ServicePlan result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServicePlan));
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					result = (ServicePlan)xmlSerializer.Deserialize(fileStream);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new XmlDeserializationException(filePath, ex.Message, (ex.InnerException == null) ? string.Empty : ex.InnerException.Message);
			}
			return result;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000561DC File Offset: 0x000543DC
		internal static bool CompareAndCalculateDelta(ServicePlan servicePlanFrom, ServicePlan servicePlanTo, bool isCrossSKUMigration, out ServicePlan servicePlanDelta, out List<string> featuresToApply)
		{
			bool result = true;
			bool flag = false;
			servicePlanDelta = new ServicePlan();
			servicePlanDelta.Name = "delta";
			featuresToApply = new List<string>();
			bool flag2 = servicePlanFrom.Organization.CommonHydrateableObjectsSharedEnabled && !servicePlanTo.Organization.CommonHydrateableObjectsSharedEnabled;
			bool flag3 = servicePlanFrom.Organization.AdvancedHydrateableObjectsSharedEnabled && !servicePlanTo.Organization.AdvancedHydrateableObjectsSharedEnabled;
			if (!servicePlanFrom.Organization.PilotEnabled)
			{
				bool pilotEnabled = servicePlanTo.Organization.PilotEnabled;
			}
			bool flag4 = servicePlanFrom.Organization.PilotEnabled && !servicePlanTo.Organization.PilotEnabled;
			foreach (object obj in ((IEnumerable)servicePlanFrom.Organization.Schema))
			{
				FeatureDefinition featureDefinition = (FeatureDefinition)obj;
				bool flag5 = false;
				if (isCrossSKUMigration)
				{
					flag5 = (featureDefinition == OrganizationSettingsSchema.RecipientMailSubmissionRateQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.ReducedOutOfTheBoxMrmTagsEnabled);
				}
				if (flag2)
				{
					flag5 |= (featureDefinition == OrganizationSettingsSchema.ReducedOutOfTheBoxMrmTagsEnabled);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.OwaInstantMessagingType);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.PublicFoldersEnabled);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.MalwareFilteringPolicyCustomizationEnabled);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.HostedSpamFilteringPolicyCustomizationEnabled);
				}
				if (flag3)
				{
					flag5 |= (featureDefinition == OrganizationSettingsSchema.DistributionListCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.MailboxCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.MailUserCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.ContactCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.TeamMailboxCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.PublicFolderMailboxCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.MailPublicFolderCountQuota);
					flag5 |= (featureDefinition == OrganizationSettingsSchema.RecipientMailSubmissionRateQuota);
				}
				if (!featureDefinition.IsValueEqual(servicePlanFrom.Organization[featureDefinition], servicePlanTo.Organization[featureDefinition]) || flag5)
				{
					result = false;
					servicePlanDelta.Organization[featureDefinition] = servicePlanTo.Organization[featureDefinition];
					featuresToApply.Add(featureDefinition.Name);
				}
			}
			foreach (ServicePlan.MailboxPlan mailboxPlan in servicePlanTo.MailboxPlans)
			{
				if (servicePlanFrom.GetMailboxPlanByName(mailboxPlan.Name) == null)
				{
					servicePlanDelta.MailboxPlans.Add(mailboxPlan);
					flag = true;
				}
			}
			if (!isCrossSKUMigration)
			{
				for (int i = 0; i < servicePlanFrom.MailboxPlans.Count; i++)
				{
					ServicePlan.MailboxPlan mailboxPlan2 = servicePlanFrom.MailboxPlans[i];
					ServicePlan.MailboxPlan mailboxPlanByName = servicePlanTo.GetMailboxPlanByName(mailboxPlan2.Name);
					if (!flag4 || mailboxPlanByName != null || !mailboxPlan2.IsPilotMailboxPlan)
					{
						foreach (object obj2 in ((IEnumerable)mailboxPlan2.Schema))
						{
							FeatureDefinition featureDefinition2 = (FeatureDefinition)obj2;
							if (flag || !featureDefinition2.IsValueEqual(mailboxPlan2[featureDefinition2], mailboxPlanByName[featureDefinition2]))
							{
								result = false;
								ServicePlan.MailboxPlan mailboxPlan3 = servicePlanDelta.GetMailboxPlanByName(mailboxPlanByName.Name);
								if (mailboxPlan3 == null)
								{
									mailboxPlan3 = new ServicePlan.MailboxPlan();
									mailboxPlan3.Name = mailboxPlanByName.Name;
									mailboxPlan3.MailboxPlanIndex = mailboxPlanByName.MailboxPlanIndex;
									servicePlanDelta.MailboxPlans.Add(mailboxPlan3);
								}
								mailboxPlan3[featureDefinition2] = mailboxPlanByName[featureDefinition2];
								if (!featuresToApply.Contains(featureDefinition2.Name))
								{
									featuresToApply.Add(featureDefinition2.Name);
								}
							}
						}
					}
				}
			}
			else
			{
				foreach (object obj3 in ((IEnumerable)new MailboxPlanSchema()))
				{
					FeatureDefinition featureDefinition3 = (FeatureDefinition)obj3;
					if (!featuresToApply.Contains(featureDefinition3.Name))
					{
						featuresToApply.Add(featureDefinition3.Name);
					}
				}
			}
			return result;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0005663C File Offset: 0x0005483C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("\r\n\tName={0}, IsValid={1}", this.Name, this.IsValid));
			stringBuilder.Append("\r\n\tOrganization Settings:");
			foreach (object obj in ((IEnumerable)this.Organization.Schema))
			{
				FeatureDefinition featureDefinition = (FeatureDefinition)obj;
				stringBuilder.Append(string.Format("\r\n\t\t{0}={1}", featureDefinition.Name, this.Organization[featureDefinition]));
			}
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				stringBuilder.Append(string.Format("\r\n\tMailboxPlan {0}:", mailboxPlan.Name));
				foreach (object obj2 in ((IEnumerable)mailboxPlan.Schema))
				{
					FeatureDefinition featureDefinition2 = (FeatureDefinition)obj2;
					stringBuilder.Append(string.Format("\r\n\t\t{0}={1}", featureDefinition2.Name, mailboxPlan[featureDefinition2]));
				}
				Microsoft.Exchange.Data.Directory.Management.MailboxPlan mailboxPlan2 = mailboxPlan.Instance as Microsoft.Exchange.Data.Directory.Management.MailboxPlan;
				if (mailboxPlan2 != null)
				{
					stringBuilder.Append(string.Format("\r\n\t\tInstance={0}", mailboxPlan2.Identity));
				}
				else
				{
					stringBuilder.Append(string.Format("\r\n\t\tInstance={0}", mailboxPlan.Instance));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000567F8 File Offset: 0x000549F8
		internal List<string> GetAggregatedMailboxPlanPermissions()
		{
			List<string> list = new List<string>();
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				list.AddRange(mailboxPlan.GetEnabledPermissionFeatures());
			}
			return list;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00056858 File Offset: 0x00054A58
		internal List<string> GetAggregatedMailboxPlanRoleAssignmentFeatures()
		{
			List<string> list = new List<string>();
			foreach (ServicePlan.MailboxPlan mailboxPlan in this.MailboxPlans)
			{
				list.AddRange(mailboxPlan.GetEnabledMailboxPlanRoleAssignmentFeatures());
			}
			return list;
		}

		// Token: 0x040008EC RID: 2284
		private string name;

		// Token: 0x040008ED RID: 2285
		private List<ServicePlan.MailboxPlan> mailboxPlans;

		// Token: 0x040008EE RID: 2286
		private ServicePlan.OrganizationSettings organization;

		// Token: 0x040008EF RID: 2287
		private string version;

		// Token: 0x02000243 RID: 579
		public sealed class OrganizationSettings : BooleanFeatureBag
		{
			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x0600135A RID: 4954 RVA: 0x00056C28 File Offset: 0x00054E28
			internal override ServicePlanElementSchema Schema
			{
				get
				{
					return ServicePlan.OrganizationSettings.schema;
				}
			}

			// Token: 0x0600135B RID: 4955 RVA: 0x00056C2F File Offset: 0x00054E2F
			internal OrganizationSettings()
			{
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x00056C38 File Offset: 0x00054E38
			private bool GetUnifiedRoleAssignmentPolicyModeValue(ServicePlan sp, FeatureDefinition feature)
			{
				foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
				{
					if ((bool)mailboxPlan[feature])
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600135D RID: 4957 RVA: 0x00056C9C File Offset: 0x00054E9C
			private void SetUnifiedRoleAssignmentPolicyModeValue(ServicePlan sp, FeatureDefinition feature, bool value)
			{
				sp.MailboxPlans[0][feature] = value;
				for (int i = 1; i < sp.MailboxPlans.Count; i++)
				{
					sp.MailboxPlans[i][feature] = !value;
				}
			}

			// Token: 0x0600135E RID: 4958 RVA: 0x00057CDC File Offset: 0x00055EDC
			protected override void InitializeDependencies()
			{
				base.Dependencies.Add(new DependencyEntry("CommonConfiguration", "MailboxPlans", () => true, (ServicePlan sp) => sp.MailboxPlans.Count > 0, delegate(ServicePlan sp, bool value)
				{
					if (sp.MailboxPlans.Count == 0)
					{
						sp.MailboxPlans.Add(new ServicePlan.MailboxPlan());
						sp.MailboxPlans[0].Name = "DefaultMailboxPlan";
						sp.MailboxPlans[0].MailboxPlanIndex = "0";
					}
				}));
				base.Dependencies.Add(new DependencyEntry("CommonConfiguration", "ProvisionAsDefault", () => true, delegate(ServicePlan sp)
				{
					int num = 0;
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						if (mailboxPlan.ProvisionAsDefault)
						{
							num++;
						}
					}
					return num == 1;
				}, delegate(ServicePlan sp, bool value)
				{
					for (int i = 0; i < sp.MailboxPlans.Count; i++)
					{
						sp.MailboxPlans[i].ProvisionAsDefault = (i == 0);
					}
				}));
				base.Dependencies.Add(new DependencyEntry("SkuCapability", "MailboxPlans", () => true, delegate(ServicePlan sp)
				{
					int mbxPlanWithCapability = 0;
					sp.MailboxPlans.ForEach(delegate(ServicePlan.MailboxPlan x)
					{
						mbxPlanWithCapability = ((x.SkuCapability != Capability.None) ? (mbxPlanWithCapability + 1) : mbxPlanWithCapability);
					});
					return mbxPlanWithCapability == 0 || (mbxPlanWithCapability != 0 && mbxPlanWithCapability == sp.MailboxPlans.Count);
				}, delegate(ServicePlan sp, bool value)
				{
					sp.MailboxPlans.ForEach(delegate(ServicePlan.MailboxPlan x)
					{
						x.SkuCapability = Capability.None;
					});
				}));
				base.Dependencies.Add(new DependencyEntry("ApplicationImpersonationEnabled", "PrivacyFeaturesAllowed", () => this.ApplicationImpersonationEnabled, (ServicePlan sp) => sp.organization.ApplicationImpersonationEnabled == this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.organization.PrivacyFeaturesAllowed = sp.organization.ApplicationImpersonationEnabled;
				}));
				base.Dependencies.Add(new DependencyEntry("ResetUserPasswordManagementPermissions", "PrivacyFeaturesAllowed", () => this.ResetUserPasswordManagementPermissions, (ServicePlan sp) => sp.organization.ResetUserPasswordManagementPermissions == sp.organization.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.organization.PrivacyFeaturesAllowed = sp.organization.ResetUserPasswordManagementPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("ResetUserPasswordManagementPermissions", "SkipResetPasswordOnFirstLogonEnabled", () => this.ResetUserPasswordManagementPermissions, delegate(ServicePlan sp)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						if (!mailboxPlan.SkipResetPasswordOnFirstLogonEnabled)
						{
							return false;
						}
					}
					return true;
				}, delegate(ServicePlan sp, bool value)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						mailboxPlan.SkipResetPasswordOnFirstLogonEnabled = value;
					}
				}));
				base.Dependencies.Add(new DependencyEntry("UMPBXPermissions", "UMPermissions", () => this.UMPBXPermissions, (ServicePlan sp) => sp.Organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMAutoAttendantPermissions", "UMPermissions", () => this.UMAutoAttendantPermissions, (ServicePlan sp) => sp.Organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMSMSMsgWaitingPermissions", "UMPermissions", () => this.UMSMSMsgWaitingPermissions, (ServicePlan sp) => sp.Organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMCloudServicePermissions", "UMPermissions", () => this.UMCloudServicePermissions, (ServicePlan sp) => sp.Organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("OpenDomainProfileUpdatePermissions", "!ProfileUpdatePermissions", () => this.OpenDomainProfileUpdatePermissions, (ServicePlan sp) => sp.Organization.OpenDomainProfileUpdatePermissions != sp.Organization.ProfileUpdatePermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.ProfileUpdatePermissions = !sp.Organization.OpenDomainProfileUpdatePermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("!PerMBXPlanOWAPolicyEnabled", "OWAPermissions", () => !this.PerMBXPlanOWAPolicyEnabled, (ServicePlan sp) => sp.Organization.OWAPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.OWAPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("OWAMailboxPolicyPermissions", "!PerMBXPlanOWAPolicyEnabled", () => this.OWAMailboxPolicyPermissions, (ServicePlan sp) => !sp.Organization.PerMBXPlanOWAPolicyEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PerMBXPlanOWAPolicyEnabled = !value;
				}));
				base.Dependencies.Add(new DependencyEntry("OfflineAddressBookEnabled", "OutlookAnywhereEnabled", () => this.OfflineAddressBookEnabled, delegate(ServicePlan sp)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						if (mailboxPlan.OutlookAnywhereEnabled)
						{
							return true;
						}
					}
					return false;
				}, delegate(ServicePlan sp, bool value)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						mailboxPlan.OutlookAnywhereEnabled = value;
					}
				}));
				base.Dependencies.Add(new DependencyEntry("SearchMessagePermissions", "PrivacyFeaturesAllowed", () => this.SearchMessagePermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.SearchMessagePermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("SearchMessageEnabled", "SearchMessagePermissions", () => this.SearchMessageEnabled, (ServicePlan sp) => sp.organization.SearchMessagePermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.SearchMessagePermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("TransportRulesPermissions", "PrivacyFeaturesAllowed", () => this.TransportRulesPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.TransportRulesPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("UserMailboxAccessPermissions", "PrivacyFeaturesAllowed", () => this.UserMailboxAccessPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.UserMailboxAccessPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("MailboxRecoveryPermissions", "PrivacyFeaturesAllowed", () => this.MailboxRecoveryPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.MailboxRecoveryPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("JournalingRulesPermissions", "PrivacyFeaturesAllowed", () => this.JournalingRulesPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.JournalingRulesPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("MessageTrackingPermissions", "PrivacyFeaturesAllowed", () => this.MessageTrackingPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.MessageTrackingPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("ActiveSyncDeviceDataAccessPermissions", "PrivacyFeaturesAllowed", () => this.ActiveSyncDeviceDataAccessPermissions, (ServicePlan sp) => this.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PrivacyFeaturesAllowed = sp.Organization.ActiveSyncDeviceDataAccessPermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("OutlookAnywherePermissions", "OutlookAnywhereEnabled", () => this.OutlookAnywherePermissions, delegate(ServicePlan sp)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						if (mailboxPlan.OutlookAnywhereEnabled)
						{
							return true;
						}
					}
					return false;
				}, delegate(ServicePlan sp, bool value)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						mailboxPlan.OutlookAnywhereEnabled = value;
					}
				}));
				base.Dependencies.Add(new DependencyEntry("UMOutDialingPermissions", "UMPermissions", () => this.UMOutDialingPermissions, (ServicePlan sp) => sp.organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMPersonalAutoAttendantPermissions", "UMPermissions", () => this.UMPersonalAutoAttendantPermissions, (ServicePlan sp) => sp.organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMFaxPermissions", "UMPermissions", () => this.UMFaxPermissions, (ServicePlan sp) => sp.organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMSMSMsgWaitingPermissions", "UMPermissions", () => this.UMSMSMsgWaitingPermissions, (ServicePlan sp) => sp.organization.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SupervisionPermissions", "SupervisionEnabled", () => this.SupervisionPermissions, (ServicePlan sp) => sp.organization.SupervisionEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.organization.SupervisionEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SupervisionEnabled", "SupervisionPermissions", () => this.SupervisionEnabled, (ServicePlan sp) => sp.Organization.SupervisionPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.SupervisionPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SupervisionEnabled", "ViewSupervisionListPermissions", () => this.SupervisionEnabled, delegate(ServicePlan sp)
				{
					if (this.PerMBXPlanRoleAssignmentPolicyEnabled)
					{
						foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
						{
							if (!mailboxPlan.ViewSupervisionListPermissions)
							{
								return false;
							}
						}
						return true;
					}
					return this.GetUnifiedRoleAssignmentPolicyModeValue(sp, MailboxPlanSchema.ViewSupervisionListPermissions);
				}, delegate(ServicePlan sp, bool value)
				{
					if (this.PerMBXPlanRoleAssignmentPolicyEnabled)
					{
						using (List<ServicePlan.MailboxPlan>.Enumerator enumerator = sp.MailboxPlans.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ServicePlan.MailboxPlan mailboxPlan = enumerator.Current;
								mailboxPlan.ViewSupervisionListPermissions = value;
							}
							return;
						}
					}
					this.SetUnifiedRoleAssignmentPolicyModeValue(sp, MailboxPlanSchema.ViewSupervisionListPermissions, value);
				}));
				base.Dependencies.Add(new DependencyEntry("RecipientMailSubmissionRateQuota", "MailboxPlans", () => true, (ServicePlan sp) => this.RecipientMailSubmissionRateQuota != null, delegate(ServicePlan sp, bool value)
				{
					this.RecipientMailSubmissionRateQuota = Unlimited<int>.UnlimitedString;
				}));
				base.Dependencies.Add(new DependencyEntry("MailTipsPermissions", "MailTipsEnabled", () => this.MailTipsPermissions, (ServicePlan sp) => sp.organization.MailTipsEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.organization.MailTipsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("DistributionListCountQuota", "MailboxPlans", () => true, (ServicePlan sp) => sp.organization.DistributionListCountQuota != null, delegate(ServicePlan sp, bool value)
				{
					sp.organization.DistributionListCountQuota = "30";
				}));
				base.Dependencies.Add(new DependencyEntry("MailboxCountQuota", "MailboxPlans", () => true, (ServicePlan sp) => sp.organization.MailboxCountQuota != null, delegate(ServicePlan sp, bool value)
				{
					sp.organization.MailboxCountQuota = "60";
				}));
				base.Dependencies.Add(new DependencyEntry("MailUserCountQuota", "MailboxPlans", () => true, (ServicePlan sp) => sp.organization.MailUserCountQuota != null, delegate(ServicePlan sp, bool value)
				{
					sp.organization.MailUserCountQuota = "30";
				}));
				base.Dependencies.Add(new DependencyEntry("ContactCountQuota", "MailboxPlans", () => true, (ServicePlan sp) => sp.organization.ContactCountQuota != null, delegate(ServicePlan sp, bool value)
				{
					sp.organization.ContactCountQuota = "60";
				}));
				base.Dependencies.Add(new DependencyEntry("ImapMigrationPermissions", "ImapSyncPermissions", () => this.ImapMigrationPermissions, (ServicePlan sp) => sp.Organization.ImapSyncPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.ImapSyncPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("HotmailMigrationPermissions", "HotmailSyncPermissions", () => this.HotmailMigrationPermissions, (ServicePlan sp) => sp.Organization.HotmailSyncPermissions, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.HotmailSyncPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("GroupAsGroupSyncPermissions", "GALSyncEnabled", () => this.GroupAsGroupSyncPermissions, (ServicePlan sp) => sp.Organization.GALSyncEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.GALSyncEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("GALSyncEnabled", "!ShareableConfigurationEnabled", () => this.GALSyncEnabled, (ServicePlan sp) => !sp.Organization.ShareableConfigurationEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.ShareableConfigurationEnabled = !value;
				}));
				base.Dependencies.Add(new DependencyEntry("GALSyncEnabled", "!AdvancedHydrateableObjectsSharedEnabled", () => this.GALSyncEnabled, (ServicePlan sp) => !sp.Organization.AdvancedHydrateableObjectsSharedEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.AdvancedHydrateableObjectsSharedEnabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("AdvancedHydrateableObjectsSharedEnabled", "ShareableConfigurationEnabled", () => this.AdvancedHydrateableObjectsSharedEnabled, (ServicePlan sp) => sp.Organization.ShareableConfigurationEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.ShareableConfigurationEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("CommonHydrateableObjectsSharedEnabled", "ShareableConfigurationEnabled", () => this.CommonHydrateableObjectsSharedEnabled, (ServicePlan sp) => sp.Organization.ShareableConfigurationEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.ShareableConfigurationEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("CommonHydrateableObjectsSharedEnabled", "AdvancedHydrateableObjectsSharedEnabled", () => this.CommonHydrateableObjectsSharedEnabled, (ServicePlan sp) => sp.Organization.AdvancedHydrateableObjectsSharedEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.AdvancedHydrateableObjectsSharedEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("PerMBXPlanOWAPolicyEnabled", "!CommonHydrateableObjectsSharedEnabled", () => this.PerMBXPlanOWAPolicyEnabled, (ServicePlan sp) => !sp.Organization.CommonHydrateableObjectsSharedEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.CommonHydrateableObjectsSharedEnabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("RoleAssignmentPolicyPermissions", "!PerMBXPlanRoleAssignmentPolicyEnabled", () => this.RoleAssignmentPolicyPermissions, (ServicePlan sp) => !sp.Organization.PerMBXPlanRoleAssignmentPolicyEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.PerMBXPlanRoleAssignmentPolicyEnabled = !value;
				}));
				base.Dependencies.Add(new DependencyEntry("!PerMBXPlanRoleAssignmentPolicyEnabled", "MailboxPlans", () => !this.PerMBXPlanRoleAssignmentPolicyEnabled, delegate(ServicePlan sp)
				{
					if (sp.MailboxPlans.Count == 0)
					{
						return true;
					}
					int num = 0;
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						List<string> enabledMailboxPlanPermissionsFeatures = mailboxPlan.GetEnabledMailboxPlanPermissionsFeatures();
						enabledMailboxPlanPermissionsFeatures.Remove("*");
						if (enabledMailboxPlanPermissionsFeatures.Count > 0)
						{
							num++;
						}
					}
					return num <= 1;
				}, delegate(ServicePlan sp, bool value)
				{
					if (sp.MailboxPlans.Count == 0)
					{
						return;
					}
					List<string> aggregatedMailboxPlanPermissions = sp.GetAggregatedMailboxPlanPermissions();
					ServicePlan.MailboxPlan mailboxPlan = sp.MailboxPlans[0];
					foreach (object obj in ((IEnumerable)mailboxPlan.Schema))
					{
						FeatureDefinition featureDefinition = (FeatureDefinition)obj;
						if (aggregatedMailboxPlanPermissions.Contains(featureDefinition.Name, StringComparer.OrdinalIgnoreCase))
						{
							mailboxPlan[featureDefinition] = value;
							for (int i = 1; i < sp.MailboxPlans.Count; i++)
							{
								sp.MailboxPlans[i][featureDefinition] = !value;
							}
						}
					}
				}));
				base.Dependencies.Add(new DependencyEntry("PermissionManagementEnabled", "RBACManagementPermissions", () => this.PermissionManagementEnabled, (ServicePlan sp) => sp.Organization.RBACManagementPermissions == sp.Organization.PermissionManagementEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.Organization.RBACManagementPermissions = sp.Organization.PermissionManagementEnabled;
				}));
				base.Dependencies.Add(new DependencyEntry("SetHiddenFromAddressListPermissions", "ShowInAddressListsEnabled", () => this.SetHiddenFromAddressListPermissions, delegate(ServicePlan sp)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						if (!mailboxPlan.ShowInAddressListsEnabled)
						{
							return false;
						}
					}
					return true;
				}, delegate(ServicePlan sp, bool value)
				{
					foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
					{
						mailboxPlan.ShowInAddressListsEnabled = value;
					}
				}));
				base.Dependencies.Add(new DependencyEntry("PerimeterSafelistingUIMode", "ExchangeHostedFilteringAdminCenterAvailabilityEnabled", () => this.PerimeterSafelistingUIMode == "EhfAC", (ServicePlan ac) => ac.Organization.ExchangeHostedFilteringAdminCenterAvailabilityEnabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.ExchangeHostedFilteringAdminCenterAvailabilityEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("ContactCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> contactCountQuota = this.GetContactCountQuota();
					return contactCountQuota.IsUnlimited || contactCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("DistributionListCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> distributionListCountQuota = this.GetDistributionListCountQuota();
					return distributionListCountQuota.IsUnlimited || distributionListCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("MailboxCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> mailboxCountQuota = this.GetMailboxCountQuota();
					return mailboxCountQuota.IsUnlimited || mailboxCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("MailUserCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> mailUserCountQuota = this.GetMailUserCountQuota();
					return mailUserCountQuota.IsUnlimited || mailUserCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("TeamMailboxCountQuota", "TeamMailboxPermissions", () => 0 != this.GetTeamMailboxCountQuota(), (ServicePlan ac) => ac.Organization.TeamMailboxPermissions, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.TeamMailboxPermissions = true;
				}));
				base.Dependencies.Add(new DependencyEntry("TeamMailboxCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> teamMailboxCountQuota = this.GetTeamMailboxCountQuota();
					return teamMailboxCountQuota.IsUnlimited || teamMailboxCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("PublicFolderMailboxCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> publicFolderMailboxCountQuota = this.GetPublicFolderMailboxCountQuota();
					return publicFolderMailboxCountQuota.IsUnlimited || publicFolderMailboxCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("MailPublicFolderCountQuota", "!FastRecipientCountingDisabled", delegate()
				{
					Unlimited<int> mailPublicFolderCountQuota = this.GetMailPublicFolderCountQuota();
					return mailPublicFolderCountQuota.IsUnlimited || mailPublicFolderCountQuota.Value >= 1001;
				}, (ServicePlan ac) => !ac.Organization.FastRecipientCountingDisabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.FastRecipientCountingDisabled = false;
				}));
				base.Dependencies.Add(new DependencyEntry("SupervisionEnabled", "TransportRulesCollectionsEnabled", () => Datacenter.IsMicrosoftHostedOnly(true) && this.SupervisionEnabled, (ServicePlan ac) => ac.Organization.TransportRulesCollectionsEnabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.TransportRulesCollectionsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("TransportRulesPermissions", "TransportRulesCollectionsEnabled", () => Datacenter.IsMicrosoftHostedOnly(true) && this.TransportRulesPermissions, (ServicePlan ac) => ac.Organization.TransportRulesCollectionsEnabled, delegate(ServicePlan ac, bool value)
				{
					ac.Organization.TransportRulesCollectionsEnabled = value;
				}));
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x0600135F RID: 4959 RVA: 0x00059325 File Offset: 0x00057525
			// (set) Token: 0x06001360 RID: 4960 RVA: 0x00059337 File Offset: 0x00057537
			public bool AcceptedDomains
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AcceptedDomains];
				}
				set
				{
					base[OrganizationSettingsSchema.AcceptedDomains] = value;
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001361 RID: 4961 RVA: 0x0005934A File Offset: 0x0005754A
			// (set) Token: 0x06001362 RID: 4962 RVA: 0x0005935C File Offset: 0x0005755C
			public bool AddressListsEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AddressListsEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.AddressListsEnabled] = value;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001363 RID: 4963 RVA: 0x0005936F File Offset: 0x0005756F
			// (set) Token: 0x06001364 RID: 4964 RVA: 0x00059381 File Offset: 0x00057581
			public bool AdvancedHydrateableObjectsSharedEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AdvancedHydrateableObjectsSharedEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.AdvancedHydrateableObjectsSharedEnabled] = value;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001365 RID: 4965 RVA: 0x00059394 File Offset: 0x00057594
			// (set) Token: 0x06001366 RID: 4966 RVA: 0x000593A6 File Offset: 0x000575A6
			public bool AllowDeleteOfExternalIdentityUponRemove
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AllowDeleteOfExternalIdentityUponRemove];
				}
				set
				{
					base[OrganizationSettingsSchema.AllowDeleteOfExternalIdentityUponRemove] = value;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001367 RID: 4967 RVA: 0x000593B9 File Offset: 0x000575B9
			// (set) Token: 0x06001368 RID: 4968 RVA: 0x000593CB File Offset: 0x000575CB
			public bool ApplicationImpersonationEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ApplicationImpersonationEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ApplicationImpersonationEnabled] = value;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x000593DE File Offset: 0x000575DE
			// (set) Token: 0x0600136A RID: 4970 RVA: 0x000593F0 File Offset: 0x000575F0
			public bool ApplicationImpersonationRegularRoleAssignmentEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ApplicationImpersonationRegularRoleAssignmentEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ApplicationImpersonationRegularRoleAssignmentEnabled] = value;
				}
			}

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x0600136B RID: 4971 RVA: 0x00059403 File Offset: 0x00057603
			// (set) Token: 0x0600136C RID: 4972 RVA: 0x00059415 File Offset: 0x00057615
			public bool AutoForwardEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AutoForwardEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.AutoForwardEnabled] = value;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x0600136D RID: 4973 RVA: 0x00059428 File Offset: 0x00057628
			// (set) Token: 0x0600136E RID: 4974 RVA: 0x0005943A File Offset: 0x0005763A
			public bool AutoReplyEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AutoReplyEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.AutoReplyEnabled] = value;
				}
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x0005944D File Offset: 0x0005764D
			// (set) Token: 0x06001370 RID: 4976 RVA: 0x0005945F File Offset: 0x0005765F
			public bool CalendarVersionStoreEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.CalendarVersionStoreEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.CalendarVersionStoreEnabled] = value;
				}
			}

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001371 RID: 4977 RVA: 0x00059472 File Offset: 0x00057672
			// (set) Token: 0x06001372 RID: 4978 RVA: 0x00059484 File Offset: 0x00057684
			public bool CommonConfiguration
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.CommonConfiguration];
				}
				set
				{
					base[OrganizationSettingsSchema.CommonConfiguration] = value;
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001373 RID: 4979 RVA: 0x00059497 File Offset: 0x00057697
			// (set) Token: 0x06001374 RID: 4980 RVA: 0x000594A9 File Offset: 0x000576A9
			public bool CommonHydrateableObjectsSharedEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.CommonHydrateableObjectsSharedEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.CommonHydrateableObjectsSharedEnabled] = value;
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001375 RID: 4981 RVA: 0x000594BC File Offset: 0x000576BC
			// (set) Token: 0x06001376 RID: 4982 RVA: 0x000594CE File Offset: 0x000576CE
			public bool DataLossPreventionEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.DataLossPreventionEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.DataLossPreventionEnabled] = value;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001377 RID: 4983 RVA: 0x000594E1 File Offset: 0x000576E1
			// (set) Token: 0x06001378 RID: 4984 RVA: 0x000594F3 File Offset: 0x000576F3
			public bool DeviceFiltersSetupEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.DeviceFiltersSetupEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.DeviceFiltersSetupEnabled] = value;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001379 RID: 4985 RVA: 0x00059506 File Offset: 0x00057706
			// (set) Token: 0x0600137A RID: 4986 RVA: 0x00059518 File Offset: 0x00057718
			public bool ExchangeHostedFilteringAdminCenterAvailabilityEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ExchangeHostedFilteringAdminCenterAvailabilityEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ExchangeHostedFilteringAdminCenterAvailabilityEnabled] = value;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005952B File Offset: 0x0005772B
			// (set) Token: 0x0600137C RID: 4988 RVA: 0x0005953D File Offset: 0x0005773D
			public bool ExchangeHostedFilteringPerimeterEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ExchangeHostedFilteringPerimeterEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ExchangeHostedFilteringPerimeterEnabled] = value;
				}
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x0600137D RID: 4989 RVA: 0x00059550 File Offset: 0x00057750
			// (set) Token: 0x0600137E RID: 4990 RVA: 0x00059562 File Offset: 0x00057762
			public bool EXOCoreFeatures
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.EXOCoreFeatures];
				}
				set
				{
					base[OrganizationSettingsSchema.EXOCoreFeatures] = value;
				}
			}

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x0600137F RID: 4991 RVA: 0x00059575 File Offset: 0x00057775
			// (set) Token: 0x06001380 RID: 4992 RVA: 0x00059587 File Offset: 0x00057787
			public bool FastRecipientCountingDisabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.FastRecipientCountingDisabled];
				}
				set
				{
					base[OrganizationSettingsSchema.FastRecipientCountingDisabled] = value;
				}
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001381 RID: 4993 RVA: 0x0005959A File Offset: 0x0005779A
			// (set) Token: 0x06001382 RID: 4994 RVA: 0x000595AC File Offset: 0x000577AC
			public bool GALSyncEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.GALSyncEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.GALSyncEnabled] = value;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001383 RID: 4995 RVA: 0x000595BF File Offset: 0x000577BF
			// (set) Token: 0x06001384 RID: 4996 RVA: 0x000595D1 File Offset: 0x000577D1
			public bool HideAdminAccessWarningEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.HideAdminAccessWarningEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.HideAdminAccessWarningEnabled] = value;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001385 RID: 4997 RVA: 0x000595E4 File Offset: 0x000577E4
			// (set) Token: 0x06001386 RID: 4998 RVA: 0x000595F6 File Offset: 0x000577F6
			public bool HostedSpamFilteringPolicyCustomizationEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.HostedSpamFilteringPolicyCustomizationEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.HostedSpamFilteringPolicyCustomizationEnabled] = value;
				}
			}

			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x06001387 RID: 4999 RVA: 0x00059609 File Offset: 0x00057809
			// (set) Token: 0x06001388 RID: 5000 RVA: 0x0005961B File Offset: 0x0005781B
			public bool MSOSyncEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MSOSyncEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.MSOSyncEnabled] = value;
				}
			}

			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x06001389 RID: 5001 RVA: 0x0005962E File Offset: 0x0005782E
			// (set) Token: 0x0600138A RID: 5002 RVA: 0x00059640 File Offset: 0x00057840
			public bool LicenseEnforcementEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.LicenseEnforcementEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.LicenseEnforcementEnabled] = value;
				}
			}

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x0600138B RID: 5003 RVA: 0x00059653 File Offset: 0x00057853
			// (set) Token: 0x0600138C RID: 5004 RVA: 0x00059665 File Offset: 0x00057865
			public bool MailboxImportExportRegularRoleAssignmentEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailboxImportExportRegularRoleAssignmentEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.MailboxImportExportRegularRoleAssignmentEnabled] = value;
				}
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x0600138D RID: 5005 RVA: 0x00059678 File Offset: 0x00057878
			// (set) Token: 0x0600138E RID: 5006 RVA: 0x0005968A File Offset: 0x0005788A
			public bool MailTipsEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailTipsEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.MailTipsEnabled] = value;
				}
			}

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x0600138F RID: 5007 RVA: 0x0005969D File Offset: 0x0005789D
			// (set) Token: 0x06001390 RID: 5008 RVA: 0x000596AF File Offset: 0x000578AF
			public bool MalwareFilteringPolicyCustomizationEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MalwareFilteringPolicyCustomizationEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.MalwareFilteringPolicyCustomizationEnabled] = value;
				}
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001391 RID: 5009 RVA: 0x000596C2 File Offset: 0x000578C2
			// (set) Token: 0x06001392 RID: 5010 RVA: 0x000596D4 File Offset: 0x000578D4
			public bool MessageTrace
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MessageTrace];
				}
				set
				{
					base[OrganizationSettingsSchema.MessageTrace] = value;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001393 RID: 5011 RVA: 0x000596E7 File Offset: 0x000578E7
			// (set) Token: 0x06001394 RID: 5012 RVA: 0x000596F9 File Offset: 0x000578F9
			public bool MultiEngineAVEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MultiEngineAVEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.MultiEngineAVEnabled] = value;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001395 RID: 5013 RVA: 0x0005970C File Offset: 0x0005790C
			// (set) Token: 0x06001396 RID: 5014 RVA: 0x0005971E File Offset: 0x0005791E
			public bool OfflineAddressBookEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OfflineAddressBookEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.OfflineAddressBookEnabled] = value;
				}
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001397 RID: 5015 RVA: 0x00059731 File Offset: 0x00057931
			// (set) Token: 0x06001398 RID: 5016 RVA: 0x00059743 File Offset: 0x00057943
			public bool OpenDomainRoutingEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OpenDomainRoutingEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.OpenDomainRoutingEnabled] = value;
				}
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x00059756 File Offset: 0x00057956
			// (set) Token: 0x0600139A RID: 5018 RVA: 0x00059768 File Offset: 0x00057968
			public bool AddOutlookAcceptedDomains
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AddOutlookAcceptedDomains];
				}
				set
				{
					base[OrganizationSettingsSchema.AddOutlookAcceptedDomains] = value;
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x0600139B RID: 5019 RVA: 0x0005977B File Offset: 0x0005797B
			// (set) Token: 0x0600139C RID: 5020 RVA: 0x0005978D File Offset: 0x0005798D
			public string OwaInstantMessagingType
			{
				get
				{
					return (string)base[OrganizationSettingsSchema.OwaInstantMessagingType];
				}
				set
				{
					base[OrganizationSettingsSchema.OwaInstantMessagingType] = value;
				}
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x0600139D RID: 5021 RVA: 0x0005979C File Offset: 0x0005799C
			// (set) Token: 0x0600139E RID: 5022 RVA: 0x000597C0 File Offset: 0x000579C0
			public string PerimeterSafelistingUIMode
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.PerimeterSafelistingUIMode];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.PerimeterSafelistingUIMode] = value;
				}
			}

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x0600139F RID: 5023 RVA: 0x000597CE File Offset: 0x000579CE
			// (set) Token: 0x060013A0 RID: 5024 RVA: 0x000597E0 File Offset: 0x000579E0
			public bool PerMBXPlanOWAPolicyEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PerMBXPlanOWAPolicyEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PerMBXPlanOWAPolicyEnabled] = value;
				}
			}

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x060013A1 RID: 5025 RVA: 0x000597F3 File Offset: 0x000579F3
			// (set) Token: 0x060013A2 RID: 5026 RVA: 0x00059805 File Offset: 0x00057A05
			public bool PerMBXPlanRetentionPolicyEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PerMBXPlanRetentionPolicyEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PerMBXPlanRetentionPolicyEnabled] = value;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00059818 File Offset: 0x00057A18
			// (set) Token: 0x060013A4 RID: 5028 RVA: 0x0005982A File Offset: 0x00057A2A
			public bool PerMBXPlanRoleAssignmentPolicyEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PerMBXPlanRoleAssignmentPolicyEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PerMBXPlanRoleAssignmentPolicyEnabled] = value;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0005983D File Offset: 0x00057A3D
			// (set) Token: 0x060013A6 RID: 5030 RVA: 0x0005984F File Offset: 0x00057A4F
			public bool PermissionManagementEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PermissionManagementEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PermissionManagementEnabled] = value;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00059862 File Offset: 0x00057A62
			// (set) Token: 0x060013A8 RID: 5032 RVA: 0x00059874 File Offset: 0x00057A74
			public bool PilotEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PilotEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PilotEnabled] = value;
				}
			}

			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00059887 File Offset: 0x00057A87
			// (set) Token: 0x060013AA RID: 5034 RVA: 0x00059899 File Offset: 0x00057A99
			public string PrivacyLink
			{
				get
				{
					return (string)base[OrganizationSettingsSchema.PrivacyLink];
				}
				set
				{
					base[OrganizationSettingsSchema.PrivacyLink] = value;
				}
			}

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x000598A7 File Offset: 0x00057AA7
			// (set) Token: 0x060013AC RID: 5036 RVA: 0x000598B9 File Offset: 0x00057AB9
			public bool PublicFoldersEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PublicFoldersEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.PublicFoldersEnabled] = value;
				}
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x000598CC File Offset: 0x00057ACC
			// (set) Token: 0x060013AE RID: 5038 RVA: 0x000598DE File Offset: 0x00057ADE
			public bool QuarantineEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.QuarantineEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.QuarantineEnabled] = value;
				}
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x060013AF RID: 5039 RVA: 0x000598F1 File Offset: 0x00057AF1
			// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00059903 File Offset: 0x00057B03
			public bool ReducedOutOfTheBoxMrmTagsEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ReducedOutOfTheBoxMrmTagsEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ReducedOutOfTheBoxMrmTagsEnabled] = value;
				}
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00059916 File Offset: 0x00057B16
			// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00059928 File Offset: 0x00057B28
			public bool SearchMessageEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SearchMessageEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.SearchMessageEnabled] = value;
				}
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0005993B File Offset: 0x00057B3B
			// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0005994D File Offset: 0x00057B4D
			public bool ServiceConnectors
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ServiceConnectors];
				}
				set
				{
					base[OrganizationSettingsSchema.ServiceConnectors] = value;
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00059960 File Offset: 0x00057B60
			// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00059972 File Offset: 0x00057B72
			public bool ShareableConfigurationEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ShareableConfigurationEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.ShareableConfigurationEnabled] = value;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00059985 File Offset: 0x00057B85
			// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00059997 File Offset: 0x00057B97
			public bool SkipToUAndParentalControlCheckEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SkipToUAndParentalControlCheckEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.SkipToUAndParentalControlCheckEnabled] = value;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000599AA File Offset: 0x00057BAA
			// (set) Token: 0x060013BA RID: 5050 RVA: 0x000599BC File Offset: 0x00057BBC
			public bool SMTPAddressCheckWithAcceptedDomainEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SMTPAddressCheckWithAcceptedDomainEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.SMTPAddressCheckWithAcceptedDomainEnabled] = value;
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x060013BB RID: 5051 RVA: 0x000599CF File Offset: 0x00057BCF
			// (set) Token: 0x060013BC RID: 5052 RVA: 0x000599E1 File Offset: 0x00057BE1
			public bool SupervisionEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SupervisionEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.SupervisionEnabled] = value;
				}
			}

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x000599F4 File Offset: 0x00057BF4
			// (set) Token: 0x060013BE RID: 5054 RVA: 0x00059A06 File Offset: 0x00057C06
			public bool SyncAccountsEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SyncAccountsEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.SyncAccountsEnabled] = value;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x060013BF RID: 5055 RVA: 0x00059A19 File Offset: 0x00057C19
			// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00059A2B File Offset: 0x00057C2B
			public bool TemplateTenant
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.TemplateTenant];
				}
				set
				{
					base[OrganizationSettingsSchema.TemplateTenant] = value;
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00059A3E File Offset: 0x00057C3E
			// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00059A50 File Offset: 0x00057C50
			public bool TransportRulesCollectionsEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.TransportRulesCollectionsEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.TransportRulesCollectionsEnabled] = value;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00059A63 File Offset: 0x00057C63
			// (set) Token: 0x060013C4 RID: 5060 RVA: 0x00059A75 File Offset: 0x00057C75
			public bool UseServicePlanAsCounterInstanceName
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UseServicePlanAsCounterInstanceName];
				}
				set
				{
					base[OrganizationSettingsSchema.UseServicePlanAsCounterInstanceName] = value;
				}
			}

			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00059A88 File Offset: 0x00057C88
			// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00059A9A File Offset: 0x00057C9A
			public bool RIMRoleGroupEnabled
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.RIMRoleGroupEnabled];
				}
				set
				{
					base[OrganizationSettingsSchema.RIMRoleGroupEnabled] = value;
				}
			}

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00059AAD File Offset: 0x00057CAD
			// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00059ABF File Offset: 0x00057CBF
			public bool ActiveSyncDeviceDataAccessPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ActiveSyncDeviceDataAccessPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ActiveSyncDeviceDataAccessPermissions] = value;
				}
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00059AD2 File Offset: 0x00057CD2
			// (set) Token: 0x060013CA RID: 5066 RVA: 0x00059AE4 File Offset: 0x00057CE4
			public bool ActiveSyncPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ActiveSyncPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ActiveSyncPermissions] = value;
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x060013CB RID: 5067 RVA: 0x00059AF7 File Offset: 0x00057CF7
			// (set) Token: 0x060013CC RID: 5068 RVA: 0x00059B09 File Offset: 0x00057D09
			public bool AddressBookPolicyPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AddressBookPolicyPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.AddressBookPolicyPermissions] = value;
				}
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x060013CD RID: 5069 RVA: 0x00059B1C File Offset: 0x00057D1C
			// (set) Token: 0x060013CE RID: 5070 RVA: 0x00059B2E File Offset: 0x00057D2E
			public bool AddSecondaryDomainPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.AddSecondaryDomainPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.AddSecondaryDomainPermissions] = value;
				}
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x060013CF RID: 5071 RVA: 0x00059B41 File Offset: 0x00057D41
			// (set) Token: 0x060013D0 RID: 5072 RVA: 0x00059B53 File Offset: 0x00057D53
			public bool ArchivePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ArchivePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ArchivePermissions] = value;
				}
			}

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00059B66 File Offset: 0x00057D66
			// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00059B78 File Offset: 0x00057D78
			public bool ChangeMailboxPlanAssignmentPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ChangeMailboxPlanAssignmentPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ChangeMailboxPlanAssignmentPermissions] = value;
				}
			}

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00059B8B File Offset: 0x00057D8B
			// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00059B9D File Offset: 0x00057D9D
			public bool ConfigCustomizationsPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ConfigCustomizationsPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ConfigCustomizationsPermissions] = value;
				}
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00059BB0 File Offset: 0x00057DB0
			// (set) Token: 0x060013D6 RID: 5078 RVA: 0x00059BC2 File Offset: 0x00057DC2
			public bool EwsPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.EwsPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.EwsPermissions] = value;
				}
			}

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00059BD5 File Offset: 0x00057DD5
			// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00059BE7 File Offset: 0x00057DE7
			public bool ExchangeMigrationPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ExchangeMigrationPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ExchangeMigrationPermissions] = value;
				}
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00059BFA File Offset: 0x00057DFA
			// (set) Token: 0x060013DA RID: 5082 RVA: 0x00059C0C File Offset: 0x00057E0C
			public bool GroupAsGroupSyncPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.GroupAsGroupSyncPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.GroupAsGroupSyncPermissions] = value;
				}
			}

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x060013DB RID: 5083 RVA: 0x00059C1F File Offset: 0x00057E1F
			// (set) Token: 0x060013DC RID: 5084 RVA: 0x00059C31 File Offset: 0x00057E31
			public bool HotmailMigrationPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.HotmailMigrationPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.HotmailMigrationPermissions] = value;
				}
			}

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060013DD RID: 5085 RVA: 0x00059C44 File Offset: 0x00057E44
			// (set) Token: 0x060013DE RID: 5086 RVA: 0x00059C56 File Offset: 0x00057E56
			public bool HotmailSyncPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.HotmailSyncPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.HotmailSyncPermissions] = value;
				}
			}

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x00059C69 File Offset: 0x00057E69
			// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00059C7B File Offset: 0x00057E7B
			public bool ImapMigrationPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ImapMigrationPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ImapMigrationPermissions] = value;
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00059C8E File Offset: 0x00057E8E
			// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00059CA0 File Offset: 0x00057EA0
			public bool ImapPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ImapPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ImapPermissions] = value;
				}
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00059CB3 File Offset: 0x00057EB3
			// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00059CC5 File Offset: 0x00057EC5
			public bool ImapSyncPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ImapSyncPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ImapSyncPermissions] = value;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00059CD8 File Offset: 0x00057ED8
			// (set) Token: 0x060013E6 RID: 5094 RVA: 0x00059CEA File Offset: 0x00057EEA
			public bool IRMPremiumFeaturesPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.IRMPremiumFeaturesPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.IRMPremiumFeaturesPermissions] = value;
				}
			}

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00059CFD File Offset: 0x00057EFD
			// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00059D0F File Offset: 0x00057F0F
			public bool JournalingRulesPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.JournalingRulesPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.JournalingRulesPermissions] = value;
				}
			}

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00059D22 File Offset: 0x00057F22
			// (set) Token: 0x060013EA RID: 5098 RVA: 0x00059D34 File Offset: 0x00057F34
			public bool LitigationHoldPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.LitigationHoldPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.LitigationHoldPermissions] = value;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x060013EB RID: 5099 RVA: 0x00059D47 File Offset: 0x00057F47
			// (set) Token: 0x060013EC RID: 5100 RVA: 0x00059D59 File Offset: 0x00057F59
			public bool MOWADeviceDataAccessPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MOWADeviceDataAccessPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MOWADeviceDataAccessPermissions] = value;
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x060013ED RID: 5101 RVA: 0x00059D6C File Offset: 0x00057F6C
			// (set) Token: 0x060013EE RID: 5102 RVA: 0x00059D7E File Offset: 0x00057F7E
			public bool MSOIdPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MSOIdPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MSOIdPermissions] = value;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x060013EF RID: 5103 RVA: 0x00059D91 File Offset: 0x00057F91
			// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00059DA3 File Offset: 0x00057FA3
			public bool MailboxQuotaPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailboxQuotaPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MailboxQuotaPermissions] = value;
				}
			}

			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00059DB6 File Offset: 0x00057FB6
			// (set) Token: 0x060013F2 RID: 5106 RVA: 0x00059DC8 File Offset: 0x00057FC8
			public bool MailboxSIRPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailboxSIRPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MailboxSIRPermissions] = value;
				}
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00059DDB File Offset: 0x00057FDB
			// (set) Token: 0x060013F4 RID: 5108 RVA: 0x00059DED File Offset: 0x00057FED
			public bool MailboxRecoveryPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailboxRecoveryPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MailboxRecoveryPermissions] = value;
				}
			}

			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00059E00 File Offset: 0x00058000
			// (set) Token: 0x060013F6 RID: 5110 RVA: 0x00059E12 File Offset: 0x00058012
			public bool MailTipsPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MailTipsPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MailTipsPermissions] = value;
				}
			}

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00059E25 File Offset: 0x00058025
			// (set) Token: 0x060013F8 RID: 5112 RVA: 0x00059E37 File Offset: 0x00058037
			public bool ManagedFoldersPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ManagedFoldersPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ManagedFoldersPermissions] = value;
				}
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00059E4A File Offset: 0x0005804A
			// (set) Token: 0x060013FA RID: 5114 RVA: 0x00059E5C File Offset: 0x0005805C
			public bool MessageTrackingPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.MessageTrackingPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.MessageTrackingPermissions] = value;
				}
			}

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x060013FB RID: 5115 RVA: 0x00059E6F File Offset: 0x0005806F
			// (set) Token: 0x060013FC RID: 5116 RVA: 0x00059E81 File Offset: 0x00058081
			public bool ModeratedRecipientsPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ModeratedRecipientsPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ModeratedRecipientsPermissions] = value;
				}
			}

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x060013FD RID: 5117 RVA: 0x00059E94 File Offset: 0x00058094
			// (set) Token: 0x060013FE RID: 5118 RVA: 0x00059EA6 File Offset: 0x000580A6
			public bool NewUserPasswordManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.NewUserPasswordManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.NewUserPasswordManagementPermissions] = value;
				}
			}

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x060013FF RID: 5119 RVA: 0x00059EB9 File Offset: 0x000580B9
			// (set) Token: 0x06001400 RID: 5120 RVA: 0x00059ECB File Offset: 0x000580CB
			public bool NewUserResetPasswordOnNextLogonPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.NewUserResetPasswordOnNextLogonPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.NewUserResetPasswordOnNextLogonPermissions] = value;
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06001401 RID: 5121 RVA: 0x00059EDE File Offset: 0x000580DE
			// (set) Token: 0x06001402 RID: 5122 RVA: 0x00059EF0 File Offset: 0x000580F0
			public bool OpenDomainProfileUpdatePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OpenDomainProfileUpdatePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.OpenDomainProfileUpdatePermissions] = value;
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x06001403 RID: 5123 RVA: 0x00059F03 File Offset: 0x00058103
			// (set) Token: 0x06001404 RID: 5124 RVA: 0x00059F15 File Offset: 0x00058115
			public bool OrganizationalAffinityPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OrganizationalAffinityPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.OrganizationalAffinityPermissions] = value;
				}
			}

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x06001405 RID: 5125 RVA: 0x00059F28 File Offset: 0x00058128
			// (set) Token: 0x06001406 RID: 5126 RVA: 0x00059F3A File Offset: 0x0005813A
			public bool OutlookAnywherePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OutlookAnywherePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.OutlookAnywherePermissions] = value;
				}
			}

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x06001407 RID: 5127 RVA: 0x00059F4D File Offset: 0x0005814D
			// (set) Token: 0x06001408 RID: 5128 RVA: 0x00059F5F File Offset: 0x0005815F
			public bool OWAMailboxPolicyPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OWAMailboxPolicyPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.OWAMailboxPolicyPermissions] = value;
				}
			}

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001409 RID: 5129 RVA: 0x00059F72 File Offset: 0x00058172
			// (set) Token: 0x0600140A RID: 5130 RVA: 0x00059F84 File Offset: 0x00058184
			public bool OWAPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.OWAPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.OWAPermissions] = value;
				}
			}

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x0600140B RID: 5131 RVA: 0x00059F97 File Offset: 0x00058197
			// (set) Token: 0x0600140C RID: 5132 RVA: 0x00059FA9 File Offset: 0x000581A9
			public bool PopPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PopPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.PopPermissions] = value;
				}
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x0600140D RID: 5133 RVA: 0x00059FBC File Offset: 0x000581BC
			// (set) Token: 0x0600140E RID: 5134 RVA: 0x00059FCE File Offset: 0x000581CE
			public bool PopSyncPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.PopSyncPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.PopSyncPermissions] = value;
				}
			}

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x0600140F RID: 5135 RVA: 0x00059FE1 File Offset: 0x000581E1
			// (set) Token: 0x06001410 RID: 5136 RVA: 0x00059FF3 File Offset: 0x000581F3
			public bool ProfileUpdatePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ProfileUpdatePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ProfileUpdatePermissions] = value;
				}
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x06001411 RID: 5137 RVA: 0x0005A006 File Offset: 0x00058206
			// (set) Token: 0x06001412 RID: 5138 RVA: 0x0005A018 File Offset: 0x00058218
			public bool RBACManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.RBACManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.RBACManagementPermissions] = value;
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06001413 RID: 5139 RVA: 0x0005A02B File Offset: 0x0005822B
			// (set) Token: 0x06001414 RID: 5140 RVA: 0x0005A03D File Offset: 0x0005823D
			public bool RecipientManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.RecipientManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.RecipientManagementPermissions] = value;
				}
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001415 RID: 5141 RVA: 0x0005A050 File Offset: 0x00058250
			// (set) Token: 0x06001416 RID: 5142 RVA: 0x0005A062 File Offset: 0x00058262
			public bool ResetUserPasswordManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.ResetUserPasswordManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.ResetUserPasswordManagementPermissions] = value;
				}
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001417 RID: 5143 RVA: 0x0005A075 File Offset: 0x00058275
			// (set) Token: 0x06001418 RID: 5144 RVA: 0x0005A087 File Offset: 0x00058287
			public bool RoleAssignmentPolicyPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.RoleAssignmentPolicyPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.RoleAssignmentPolicyPermissions] = value;
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x0005A09A File Offset: 0x0005829A
			// (set) Token: 0x0600141A RID: 5146 RVA: 0x0005A0AC File Offset: 0x000582AC
			public bool SearchMessagePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SearchMessagePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.SearchMessagePermissions] = value;
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x0600141B RID: 5147 RVA: 0x0005A0BF File Offset: 0x000582BF
			// (set) Token: 0x0600141C RID: 5148 RVA: 0x0005A0D1 File Offset: 0x000582D1
			public bool SetHiddenFromAddressListPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SetHiddenFromAddressListPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.SetHiddenFromAddressListPermissions] = value;
				}
			}

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x0600141D RID: 5149 RVA: 0x0005A0E4 File Offset: 0x000582E4
			// (set) Token: 0x0600141E RID: 5150 RVA: 0x0005A0F6 File Offset: 0x000582F6
			public bool TeamMailboxPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.TeamMailboxPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.TeamMailboxPermissions] = value;
				}
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x0600141F RID: 5151 RVA: 0x0005A109 File Offset: 0x00058309
			// (set) Token: 0x06001420 RID: 5152 RVA: 0x0005A11B File Offset: 0x0005831B
			public bool SMSPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SMSPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.SMSPermissions] = value;
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001421 RID: 5153 RVA: 0x0005A12E File Offset: 0x0005832E
			// (set) Token: 0x06001422 RID: 5154 RVA: 0x0005A140 File Offset: 0x00058340
			public bool SupervisionPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SupervisionPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.SupervisionPermissions] = value;
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001423 RID: 5155 RVA: 0x0005A153 File Offset: 0x00058353
			// (set) Token: 0x06001424 RID: 5156 RVA: 0x0005A165 File Offset: 0x00058365
			public bool TransportRulesPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.TransportRulesPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.TransportRulesPermissions] = value;
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001425 RID: 5157 RVA: 0x0005A178 File Offset: 0x00058378
			// (set) Token: 0x06001426 RID: 5158 RVA: 0x0005A18A File Offset: 0x0005838A
			public bool UMAutoAttendantPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMAutoAttendantPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMAutoAttendantPermissions] = value;
				}
			}

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06001427 RID: 5159 RVA: 0x0005A19D File Offset: 0x0005839D
			// (set) Token: 0x06001428 RID: 5160 RVA: 0x0005A1AF File Offset: 0x000583AF
			public bool UMCloudServicePermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMCloudServicePermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMCloudServicePermissions] = value;
				}
			}

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005A1C2 File Offset: 0x000583C2
			// (set) Token: 0x0600142A RID: 5162 RVA: 0x0005A1D4 File Offset: 0x000583D4
			public bool UMFaxPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMFaxPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMFaxPermissions] = value;
				}
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x0600142B RID: 5163 RVA: 0x0005A1E7 File Offset: 0x000583E7
			// (set) Token: 0x0600142C RID: 5164 RVA: 0x0005A1F9 File Offset: 0x000583F9
			public bool UMOutDialingPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMOutDialingPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMOutDialingPermissions] = value;
				}
			}

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x0600142D RID: 5165 RVA: 0x0005A20C File Offset: 0x0005840C
			// (set) Token: 0x0600142E RID: 5166 RVA: 0x0005A21E File Offset: 0x0005841E
			public bool UMPBXPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMPBXPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMPBXPermissions] = value;
				}
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x0600142F RID: 5167 RVA: 0x0005A231 File Offset: 0x00058431
			// (set) Token: 0x06001430 RID: 5168 RVA: 0x0005A243 File Offset: 0x00058443
			public bool UMPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMPermissions] = value;
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005A256 File Offset: 0x00058456
			// (set) Token: 0x06001432 RID: 5170 RVA: 0x0005A268 File Offset: 0x00058468
			public bool UMPersonalAutoAttendantPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMPersonalAutoAttendantPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMPersonalAutoAttendantPermissions] = value;
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005A27B File Offset: 0x0005847B
			// (set) Token: 0x06001434 RID: 5172 RVA: 0x0005A28D File Offset: 0x0005848D
			public bool UMSMSMsgWaitingPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UMSMSMsgWaitingPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UMSMSMsgWaitingPermissions] = value;
				}
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x06001435 RID: 5173 RVA: 0x0005A2A0 File Offset: 0x000584A0
			// (set) Token: 0x06001436 RID: 5174 RVA: 0x0005A2B2 File Offset: 0x000584B2
			public bool UserLiveIdManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UserLiveIdManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UserLiveIdManagementPermissions] = value;
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x06001437 RID: 5175 RVA: 0x0005A2C5 File Offset: 0x000584C5
			// (set) Token: 0x06001438 RID: 5176 RVA: 0x0005A2D7 File Offset: 0x000584D7
			public bool UserMailboxAccessPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.UserMailboxAccessPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.UserMailboxAccessPermissions] = value;
				}
			}

			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x0005A2EA File Offset: 0x000584EA
			// (set) Token: 0x0600143A RID: 5178 RVA: 0x0005A2FC File Offset: 0x000584FC
			public bool SoftDeletedFeatureManagementPermissions
			{
				get
				{
					return (bool)base[OrganizationSettingsSchema.SoftDeletedFeatureManagementPermissions];
				}
				set
				{
					base[OrganizationSettingsSchema.SoftDeletedFeatureManagementPermissions] = value;
				}
			}

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x0600143B RID: 5179 RVA: 0x0005A310 File Offset: 0x00058510
			// (set) Token: 0x0600143C RID: 5180 RVA: 0x0005A334 File Offset: 0x00058534
			public string ContactCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.ContactCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.ContactCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x0600143D RID: 5181 RVA: 0x0005A34C File Offset: 0x0005854C
			// (set) Token: 0x0600143E RID: 5182 RVA: 0x0005A370 File Offset: 0x00058570
			public string DistributionListCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.DistributionListCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.DistributionListCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005A388 File Offset: 0x00058588
			// (set) Token: 0x06001440 RID: 5184 RVA: 0x0005A3AC File Offset: 0x000585AC
			public string MailboxCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.MailboxCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.MailboxCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005A3C4 File Offset: 0x000585C4
			// (set) Token: 0x06001442 RID: 5186 RVA: 0x0005A3E8 File Offset: 0x000585E8
			public string MailPublicFolderCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.MailPublicFolderCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.MailPublicFolderCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x06001443 RID: 5187 RVA: 0x0005A400 File Offset: 0x00058600
			// (set) Token: 0x06001444 RID: 5188 RVA: 0x0005A424 File Offset: 0x00058624
			public string MailUserCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.MailUserCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.MailUserCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x06001445 RID: 5189 RVA: 0x0005A43C File Offset: 0x0005863C
			// (set) Token: 0x06001446 RID: 5190 RVA: 0x0005A460 File Offset: 0x00058660
			public string PublicFolderMailboxCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.PublicFolderMailboxCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.PublicFolderMailboxCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x06001447 RID: 5191 RVA: 0x0005A478 File Offset: 0x00058678
			// (set) Token: 0x06001448 RID: 5192 RVA: 0x0005A49C File Offset: 0x0005869C
			public string RecipientMailSubmissionRateQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.RecipientMailSubmissionRateQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.RecipientMailSubmissionRateQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06001449 RID: 5193 RVA: 0x0005A4B4 File Offset: 0x000586B4
			// (set) Token: 0x0600144A RID: 5194 RVA: 0x0005A4D8 File Offset: 0x000586D8
			public string TeamMailboxCountQuota
			{
				get
				{
					object obj = base[OrganizationSettingsSchema.TeamMailboxCountQuota];
					if (obj != null)
					{
						return obj.ToString();
					}
					return null;
				}
				set
				{
					base[OrganizationSettingsSchema.TeamMailboxCountQuota] = Unlimited<int>.Parse(value);
				}
			}

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x0600144B RID: 5195 RVA: 0x0005A4F0 File Offset: 0x000586F0
			// (set) Token: 0x0600144C RID: 5196 RVA: 0x0005A505 File Offset: 0x00058705
			internal bool PrivacyFeaturesAllowed
			{
				get
				{
					return this.SkipToUAndParentalControlCheckEnabled || !this.HideAdminAccessWarningEnabled;
				}
				set
				{
					if (!value)
					{
						throw new NotImplementedException();
					}
					this.SkipToUAndParentalControlCheckEnabled = value;
				}
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x0005A518 File Offset: 0x00058718
			private Unlimited<int> GetCountQuota(FeatureDefinition countQuotaFeature)
			{
				object obj = base[countQuotaFeature];
				if (obj != null)
				{
					return (Unlimited<int>)obj;
				}
				return new Unlimited<int>(0);
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x0005A53D File Offset: 0x0005873D
			public Unlimited<int> GetDistributionListCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.DistributionListCountQuota);
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x0005A54A File Offset: 0x0005874A
			public Unlimited<int> GetMailboxCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.MailboxCountQuota);
			}

			// Token: 0x06001450 RID: 5200 RVA: 0x0005A557 File Offset: 0x00058757
			public Unlimited<int> GetMailUserCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.MailUserCountQuota);
			}

			// Token: 0x06001451 RID: 5201 RVA: 0x0005A564 File Offset: 0x00058764
			public Unlimited<int> GetContactCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.ContactCountQuota);
			}

			// Token: 0x06001452 RID: 5202 RVA: 0x0005A571 File Offset: 0x00058771
			public Unlimited<int> GetTeamMailboxCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.TeamMailboxCountQuota);
			}

			// Token: 0x06001453 RID: 5203 RVA: 0x0005A57E File Offset: 0x0005877E
			public Unlimited<int> GetPublicFolderMailboxCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.PublicFolderMailboxCountQuota);
			}

			// Token: 0x06001454 RID: 5204 RVA: 0x0005A58B File Offset: 0x0005878B
			public Unlimited<int> GetMailPublicFolderCountQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.MailPublicFolderCountQuota);
			}

			// Token: 0x06001455 RID: 5205 RVA: 0x0005A598 File Offset: 0x00058798
			public Unlimited<int> GetRecipientMailSubmissionRateQuota()
			{
				return this.GetCountQuota(OrganizationSettingsSchema.RecipientMailSubmissionRateQuota);
			}

			// Token: 0x040008F3 RID: 2291
			private static OrganizationSettingsSchema schema = new OrganizationSettingsSchema();
		}

		// Token: 0x02000244 RID: 580
		public sealed class MailboxPlan : BooleanFeatureBag
		{
			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x0600150C RID: 5388 RVA: 0x0005A5B1 File Offset: 0x000587B1
			internal override ServicePlanElementSchema Schema
			{
				get
				{
					return ServicePlan.MailboxPlan.schema;
				}
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x0005A5B8 File Offset: 0x000587B8
			internal MailboxPlan()
			{
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x0005A928 File Offset: 0x00058B28
			protected override void InitializeDependencies()
			{
				base.Dependencies.Add(new DependencyEntry("OpenDomainProfileUpdatePermissions", "!ProfileUpdatePermissions", () => this.OpenDomainProfileUpdatePermissions, (ServicePlan sp) => this.OpenDomainProfileUpdatePermissions != this.ProfileUpdatePermissions, delegate(ServicePlan sp, bool value)
				{
					this.ProfileUpdatePermissions = !this.OpenDomainProfileUpdatePermissions;
				}));
				base.Dependencies.Add(new DependencyEntry("OutlookAnywhereEnabled", "ShowInAddressListsEnabled", () => this.OutlookAnywhereEnabled, (ServicePlan sp) => this.ShowInAddressListsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.ShowInAddressListsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsEnabled", "SyncAccountsMaxAccountsQuota", () => this.SyncAccountsEnabled, (ServicePlan sp) => this.SyncAccountsMaxAccountsQuota != null, delegate(ServicePlan sp, bool value)
				{
					string syncAccountsMaxAccountsQuota = ((ADPropertyDefinition)RemoteAccountPolicySchema.MaxSyncAccounts).DefaultValue.ToString();
					this.SyncAccountsMaxAccountsQuota = syncAccountsMaxAccountsQuota;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsEnabled", "SyncAccountsPollingInterval", () => this.SyncAccountsEnabled, (ServicePlan sp) => this.SyncAccountsPollingInterval != null, delegate(ServicePlan sp, bool value)
				{
					string syncAccountsPollingInterval = ((ADPropertyDefinition)RemoteAccountPolicySchema.PollingInterval).DefaultValue.ToString();
					this.SyncAccountsPollingInterval = syncAccountsPollingInterval;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsEnabled", "SyncAccountsTimeBeforeInactive", () => this.SyncAccountsEnabled, (ServicePlan sp) => this.SyncAccountsTimeBeforeInactive != null, delegate(ServicePlan sp, bool value)
				{
					string syncAccountsTimeBeforeInactive = ((ADPropertyDefinition)RemoteAccountPolicySchema.TimeBeforeInactive).DefaultValue.ToString();
					this.SyncAccountsTimeBeforeInactive = syncAccountsTimeBeforeInactive;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsEnabled", "SyncAccountsTimeBeforeDormant", () => this.SyncAccountsEnabled, (ServicePlan sp) => this.SyncAccountsTimeBeforeDormant != null, delegate(ServicePlan sp, bool value)
				{
					string syncAccountsTimeBeforeDormant = ((ADPropertyDefinition)RemoteAccountPolicySchema.TimeBeforeDormant).DefaultValue.ToString();
					this.SyncAccountsTimeBeforeDormant = syncAccountsTimeBeforeDormant;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsSyncNowEnabled", "SyncAccountsEnabled", () => this.SyncAccountsSyncNowEnabled, (ServicePlan sp) => this.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsMaxAccountsQuota", "SyncAccountsEnabled", () => this.SyncAccountsMaxAccountsQuota != null, (ServicePlan sp) => this.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsPollingInterval", "SyncAccountsEnabled", () => this.SyncAccountsPollingInterval != null, (ServicePlan sp) => this.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsTimeBeforeInactive", "SyncAccountsEnabled", () => this.SyncAccountsTimeBeforeInactive != null, (ServicePlan sp) => this.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsTimeBeforeDormant", "SyncAccountsEnabled", () => this.SyncAccountsTimeBeforeDormant != null, (ServicePlan sp) => this.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					this.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SyncAccountsEnabled", "SyncAccountsEnabled", () => this.SyncAccountsEnabled, (ServicePlan sp) => sp.organization.SyncAccountsEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.organization.SyncAccountsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("MailboxPlanIndex", "ProhibitSendReceiveMaiboxQuota", () => true, (ServicePlan sp) => !string.IsNullOrEmpty(this.ProhibitSendReceiveMaiboxQuota), delegate(ServicePlan sp, bool value)
				{
					this.ProhibitSendReceiveMaiboxQuota = "10GB";
				}));
				base.Dependencies.Add(new DependencyEntry("UniqueMailboxPlanIndex", "MailboxPlanIndex", () => true, (ServicePlan sp) => this.MailboxPlanIndexSetAndUnique(sp), delegate(ServicePlan sp, bool value)
				{
					Random random = new Random();
					this.MailboxPlanIndex = random.Next(0, 65535).ToString();
				}));
				base.Dependencies.Add(new DependencyEntry("ModeratedRecipientsPermissions", "AutoGroupPermissions", () => this.ModeratedRecipientsPermissions, (ServicePlan sp) => this.AutoGroupPermissions, delegate(ServicePlan sp, bool value)
				{
					this.AutoGroupPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("ActiveSyncDeviceDataAccessPermissions", "ActiveSyncEnabled", () => this.ActiveSyncDeviceDataAccessPermissions, (ServicePlan sp) => this.ActiveSyncEnabled, delegate(ServicePlan sp, bool value)
				{
					this.ActiveSyncEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("MOWADeviceDataAccessPermissions", "MOWAEnabled", () => this.MOWADeviceDataAccessPermissions, (ServicePlan sp) => this.MOWAEnabled, delegate(ServicePlan sp, bool value)
				{
					this.MOWAEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMSMSMsgWaitingPermissions", "UMPermissions", () => this.UMSMSMsgWaitingPermissions, (ServicePlan sp) => this.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					this.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("UMCloudServicePermissions", "UMPermissions", () => this.UMCloudServicePermissions, (ServicePlan sp) => this.UMPermissions, delegate(ServicePlan sp, bool value)
				{
					this.UMPermissions = value;
				}));
				base.Dependencies.Add(new DependencyEntry("MailTipsPermissions", "MailTipsEnabled", () => this.MailTipsPermissions, (ServicePlan sp) => sp.organization.MailTipsEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.organization.MailTipsEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("ViewSupervisionListPermissions", "SupervisionEnabled", () => this.ViewSupervisionListPermissions, (ServicePlan sp) => sp.organization.SupervisionEnabled, delegate(ServicePlan sp, bool value)
				{
					sp.organization.SupervisionEnabled = value;
				}));
				base.Dependencies.Add(new DependencyEntry("SkipResetPasswordOnFirstLogonEnabled", "PrivacyFeaturesAllowed", () => this.SkipResetPasswordOnFirstLogonEnabled, (ServicePlan sp) => sp.organization.PrivacyFeaturesAllowed, delegate(ServicePlan sp, bool value)
				{
					sp.organization.PrivacyFeaturesAllowed = value;
				}));
				base.Dependencies.Add(new DependencyEntry("ArchiveQuota", "ArchivePermissions", () => this.ArchiveQuota != null, (ServicePlan sp) => sp.organization.ArchivePermissions, delegate(ServicePlan sp, bool value)
				{
					sp.organization.ArchivePermissions = value;
				}));
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x0005AF94 File Offset: 0x00059194
			private bool MailboxPlanIndexSetAndUnique(ServicePlan sp)
			{
				if (string.IsNullOrEmpty(this.MailboxPlanIndex))
				{
					return false;
				}
				foreach (ServicePlan.MailboxPlan mailboxPlan in sp.MailboxPlans)
				{
					if (this != mailboxPlan && this.MailboxPlanIndex.Equals(mailboxPlan.MailboxPlanIndex, StringComparison.InvariantCultureIgnoreCase))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x06001510 RID: 5392 RVA: 0x0005B010 File Offset: 0x00059210
			// (set) Token: 0x06001511 RID: 5393 RVA: 0x0005B018 File Offset: 0x00059218
			public object Instance { get; set; }

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06001512 RID: 5394 RVA: 0x0005B021 File Offset: 0x00059221
			// (set) Token: 0x06001513 RID: 5395 RVA: 0x0005B029 File Offset: 0x00059229
			[XmlAttribute]
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x06001514 RID: 5396 RVA: 0x0005B032 File Offset: 0x00059232
			// (set) Token: 0x06001515 RID: 5397 RVA: 0x0005B03A File Offset: 0x0005923A
			[XmlAttribute]
			public string MailboxPlanIndex
			{
				get
				{
					return this.mailboxPlanIndex;
				}
				set
				{
					this.mailboxPlanIndex = value;
				}
			}

			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x06001516 RID: 5398 RVA: 0x0005B043 File Offset: 0x00059243
			// (set) Token: 0x06001517 RID: 5399 RVA: 0x0005B04B File Offset: 0x0005924B
			[XmlAttribute]
			public bool ProvisionAsDefault { get; set; }

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x06001518 RID: 5400 RVA: 0x0005B054 File Offset: 0x00059254
			// (set) Token: 0x06001519 RID: 5401 RVA: 0x0005B05C File Offset: 0x0005925C
			[XmlAttribute]
			public bool IsPilotMailboxPlan { get; set; }

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x0600151A RID: 5402 RVA: 0x0005B065 File Offset: 0x00059265
			// (set) Token: 0x0600151B RID: 5403 RVA: 0x0005B077 File Offset: 0x00059277
			public bool ActiveSyncEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ActiveSyncEnabled];
				}
				set
				{
					base[MailboxPlanSchema.ActiveSyncEnabled] = value;
				}
			}

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x0600151C RID: 5404 RVA: 0x0005B08A File Offset: 0x0005928A
			// (set) Token: 0x0600151D RID: 5405 RVA: 0x0005B09C File Offset: 0x0005929C
			public bool EwsEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.EwsEnabled];
				}
				set
				{
					base[MailboxPlanSchema.EwsEnabled] = value;
				}
			}

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x0600151E RID: 5406 RVA: 0x0005B0AF File Offset: 0x000592AF
			// (set) Token: 0x0600151F RID: 5407 RVA: 0x0005B0C1 File Offset: 0x000592C1
			public bool ImapEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ImapEnabled];
				}
				set
				{
					base[MailboxPlanSchema.ImapEnabled] = value;
				}
			}

			// Token: 0x17000664 RID: 1636
			// (get) Token: 0x06001520 RID: 5408 RVA: 0x0005B0D4 File Offset: 0x000592D4
			// (set) Token: 0x06001521 RID: 5409 RVA: 0x0005B0E6 File Offset: 0x000592E6
			public bool MOWAEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.MOWAEnabled];
				}
				set
				{
					base[MailboxPlanSchema.MOWAEnabled] = value;
				}
			}

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005B0F9 File Offset: 0x000592F9
			// (set) Token: 0x06001523 RID: 5411 RVA: 0x0005B10B File Offset: 0x0005930B
			public bool OrganizationalQueryBaseDNEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.OrganizationalQueryBaseDNEnabled];
				}
				set
				{
					base[MailboxPlanSchema.OrganizationalQueryBaseDNEnabled] = value;
				}
			}

			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x06001524 RID: 5412 RVA: 0x0005B11E File Offset: 0x0005931E
			// (set) Token: 0x06001525 RID: 5413 RVA: 0x0005B130 File Offset: 0x00059330
			public bool OutlookAnywhereEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.OutlookAnywhereEnabled];
				}
				set
				{
					base[MailboxPlanSchema.OutlookAnywhereEnabled] = value;
				}
			}

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005B143 File Offset: 0x00059343
			// (set) Token: 0x06001527 RID: 5415 RVA: 0x0005B155 File Offset: 0x00059355
			public bool PopEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.PopEnabled];
				}
				set
				{
					base[MailboxPlanSchema.PopEnabled] = value;
				}
			}

			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x06001528 RID: 5416 RVA: 0x0005B168 File Offset: 0x00059368
			// (set) Token: 0x06001529 RID: 5417 RVA: 0x0005B17A File Offset: 0x0005937A
			public bool ShowInAddressListsEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ShowInAddressListsEnabled];
				}
				set
				{
					base[MailboxPlanSchema.ShowInAddressListsEnabled] = value;
				}
			}

			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x0600152A RID: 5418 RVA: 0x0005B18D File Offset: 0x0005938D
			// (set) Token: 0x0600152B RID: 5419 RVA: 0x0005B19F File Offset: 0x0005939F
			public bool SingleItemRecoveryEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.SingleItemRecoveryEnabled];
				}
				set
				{
					base[MailboxPlanSchema.SingleItemRecoveryEnabled] = value;
				}
			}

			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005B1B2 File Offset: 0x000593B2
			// (set) Token: 0x0600152D RID: 5421 RVA: 0x0005B1C4 File Offset: 0x000593C4
			public bool SkipResetPasswordOnFirstLogonEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.SkipResetPasswordOnFirstLogonEnabled];
				}
				set
				{
					base[MailboxPlanSchema.SkipResetPasswordOnFirstLogonEnabled] = value;
				}
			}

			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x0600152E RID: 5422 RVA: 0x0005B1D7 File Offset: 0x000593D7
			// (set) Token: 0x0600152F RID: 5423 RVA: 0x0005B1E9 File Offset: 0x000593E9
			public bool SyncAccountsEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.SyncAccountsEnabled];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsEnabled] = value;
				}
			}

			// Token: 0x1700066C RID: 1644
			// (get) Token: 0x06001530 RID: 5424 RVA: 0x0005B1FC File Offset: 0x000593FC
			// (set) Token: 0x06001531 RID: 5425 RVA: 0x0005B20E File Offset: 0x0005940E
			public bool SyncAccountsSyncNowEnabled
			{
				get
				{
					return (bool)base[MailboxPlanSchema.SyncAccountsSyncNowEnabled];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsSyncNowEnabled] = value;
				}
			}

			// Token: 0x1700066D RID: 1645
			// (get) Token: 0x06001532 RID: 5426 RVA: 0x0005B221 File Offset: 0x00059421
			// (set) Token: 0x06001533 RID: 5427 RVA: 0x0005B234 File Offset: 0x00059434
			public Capability SkuCapability
			{
				get
				{
					return (Capability)base[MailboxPlanSchema.SkuCapability];
				}
				set
				{
					if (value != Capability.None && !CapabilityHelper.AllowedSKUCapabilities.Contains(value))
					{
						throw new XmlException(Strings.ErrorServicePlanMailboxPlanInvalidSkuCapability(value.ToString(), MultiValuedPropertyBase.FormatMultiValuedProperty(CapabilityHelper.AllowedSKUCapabilities)));
					}
					base[MailboxPlanSchema.SkuCapability] = value;
				}
			}

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x06001534 RID: 5428 RVA: 0x0005B287 File Offset: 0x00059487
			// (set) Token: 0x06001535 RID: 5429 RVA: 0x0005B299 File Offset: 0x00059499
			public UMDeploymentModeOptions UMEnabled
			{
				get
				{
					return (UMDeploymentModeOptions)base[MailboxPlanSchema.UMEnabled];
				}
				set
				{
					base[MailboxPlanSchema.UMEnabled] = value;
				}
			}

			// Token: 0x1700066F RID: 1647
			// (get) Token: 0x06001536 RID: 5430 RVA: 0x0005B2AC File Offset: 0x000594AC
			// (set) Token: 0x06001537 RID: 5431 RVA: 0x0005B2BE File Offset: 0x000594BE
			public bool ActiveSyncDeviceDataAccessPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ActiveSyncDeviceDataAccessPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ActiveSyncDeviceDataAccessPermissions] = value;
				}
			}

			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x06001538 RID: 5432 RVA: 0x0005B2D1 File Offset: 0x000594D1
			// (set) Token: 0x06001539 RID: 5433 RVA: 0x0005B2E3 File Offset: 0x000594E3
			public bool ActiveSyncPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ActiveSyncPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ActiveSyncPermissions] = value;
				}
			}

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005B2F6 File Offset: 0x000594F6
			// (set) Token: 0x0600153B RID: 5435 RVA: 0x0005B308 File Offset: 0x00059508
			public bool AutoGroupPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.AutoGroupPermissions];
				}
				set
				{
					base[MailboxPlanSchema.AutoGroupPermissions] = value;
				}
			}

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x0600153C RID: 5436 RVA: 0x0005B31B File Offset: 0x0005951B
			// (set) Token: 0x0600153D RID: 5437 RVA: 0x0005B32D File Offset: 0x0005952D
			public bool EXOCoreFeatures
			{
				get
				{
					return (bool)base[MailboxPlanSchema.EXOCoreFeatures];
				}
				set
				{
					base[MailboxPlanSchema.EXOCoreFeatures] = value;
				}
			}

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x0600153E RID: 5438 RVA: 0x0005B340 File Offset: 0x00059540
			// (set) Token: 0x0600153F RID: 5439 RVA: 0x0005B352 File Offset: 0x00059552
			public bool HotmailSyncPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.HotmailSyncPermissions];
				}
				set
				{
					base[MailboxPlanSchema.HotmailSyncPermissions] = value;
				}
			}

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06001540 RID: 5440 RVA: 0x0005B365 File Offset: 0x00059565
			// (set) Token: 0x06001541 RID: 5441 RVA: 0x0005B377 File Offset: 0x00059577
			public bool ImapPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ImapPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ImapPermissions] = value;
				}
			}

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001542 RID: 5442 RVA: 0x0005B38A File Offset: 0x0005958A
			// (set) Token: 0x06001543 RID: 5443 RVA: 0x0005B39C File Offset: 0x0005959C
			public bool ImapSyncPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ImapSyncPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ImapSyncPermissions] = value;
				}
			}

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06001544 RID: 5444 RVA: 0x0005B3AF File Offset: 0x000595AF
			// (set) Token: 0x06001545 RID: 5445 RVA: 0x0005B3C1 File Offset: 0x000595C1
			public bool MailTipsPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.MailTipsPermissions];
				}
				set
				{
					base[MailboxPlanSchema.MailTipsPermissions] = value;
				}
			}

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06001546 RID: 5446 RVA: 0x0005B3D4 File Offset: 0x000595D4
			// (set) Token: 0x06001547 RID: 5447 RVA: 0x0005B3E6 File Offset: 0x000595E6
			public bool MessageTrackingPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.MessageTrackingPermissions];
				}
				set
				{
					base[MailboxPlanSchema.MessageTrackingPermissions] = value;
				}
			}

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06001548 RID: 5448 RVA: 0x0005B3F9 File Offset: 0x000595F9
			// (set) Token: 0x06001549 RID: 5449 RVA: 0x0005B40B File Offset: 0x0005960B
			public bool ModeratedRecipientsPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ModeratedRecipientsPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ModeratedRecipientsPermissions] = value;
				}
			}

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x0600154A RID: 5450 RVA: 0x0005B41E File Offset: 0x0005961E
			// (set) Token: 0x0600154B RID: 5451 RVA: 0x0005B430 File Offset: 0x00059630
			public bool MOWADeviceDataAccessPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.MOWADeviceDataAccessPermissions];
				}
				set
				{
					base[MailboxPlanSchema.MOWADeviceDataAccessPermissions] = value;
				}
			}

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x0600154C RID: 5452 RVA: 0x0005B443 File Offset: 0x00059643
			// (set) Token: 0x0600154D RID: 5453 RVA: 0x0005B455 File Offset: 0x00059655
			public bool OpenDomainProfileUpdatePermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.OpenDomainProfileUpdatePermissions];
				}
				set
				{
					base[MailboxPlanSchema.OpenDomainProfileUpdatePermissions] = value;
				}
			}

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x0600154E RID: 5454 RVA: 0x0005B468 File Offset: 0x00059668
			// (set) Token: 0x0600154F RID: 5455 RVA: 0x0005B47A File Offset: 0x0005967A
			public bool OrganizationalAffinityPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.OrganizationalAffinityPermissions];
				}
				set
				{
					base[MailboxPlanSchema.OrganizationalAffinityPermissions] = value;
				}
			}

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06001550 RID: 5456 RVA: 0x0005B48D File Offset: 0x0005968D
			// (set) Token: 0x06001551 RID: 5457 RVA: 0x0005B49F File Offset: 0x0005969F
			public bool PopPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.PopPermissions];
				}
				set
				{
					base[MailboxPlanSchema.PopPermissions] = value;
				}
			}

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06001552 RID: 5458 RVA: 0x0005B4B2 File Offset: 0x000596B2
			// (set) Token: 0x06001553 RID: 5459 RVA: 0x0005B4C4 File Offset: 0x000596C4
			public bool PopSyncPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.PopSyncPermissions];
				}
				set
				{
					base[MailboxPlanSchema.PopSyncPermissions] = value;
				}
			}

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x06001554 RID: 5460 RVA: 0x0005B4D7 File Offset: 0x000596D7
			// (set) Token: 0x06001555 RID: 5461 RVA: 0x0005B4E9 File Offset: 0x000596E9
			public bool ProfileUpdatePermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ProfileUpdatePermissions];
				}
				set
				{
					base[MailboxPlanSchema.ProfileUpdatePermissions] = value;
				}
			}

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x06001556 RID: 5462 RVA: 0x0005B4FC File Offset: 0x000596FC
			// (set) Token: 0x06001557 RID: 5463 RVA: 0x0005B50E File Offset: 0x0005970E
			public bool ResetUserPasswordManagementPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ResetUserPasswordManagementPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ResetUserPasswordManagementPermissions] = value;
				}
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x06001558 RID: 5464 RVA: 0x0005B521 File Offset: 0x00059721
			// (set) Token: 0x06001559 RID: 5465 RVA: 0x0005B533 File Offset: 0x00059733
			public bool TeamMailboxPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.TeamMailboxPermissions];
				}
				set
				{
					base[MailboxPlanSchema.TeamMailboxPermissions] = value;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x0600155A RID: 5466 RVA: 0x0005B546 File Offset: 0x00059746
			// (set) Token: 0x0600155B RID: 5467 RVA: 0x0005B558 File Offset: 0x00059758
			public bool SMSPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.SMSPermissions];
				}
				set
				{
					base[MailboxPlanSchema.SMSPermissions] = value;
				}
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x0600155C RID: 5468 RVA: 0x0005B56B File Offset: 0x0005976B
			// (set) Token: 0x0600155D RID: 5469 RVA: 0x0005B57D File Offset: 0x0005977D
			public bool UMCloudServicePermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.UMCloudServicePermissions];
				}
				set
				{
					base[MailboxPlanSchema.UMCloudServicePermissions] = value;
				}
			}

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x0600155E RID: 5470 RVA: 0x0005B590 File Offset: 0x00059790
			// (set) Token: 0x0600155F RID: 5471 RVA: 0x0005B5A2 File Offset: 0x000597A2
			public bool UMPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.UMPermissions];
				}
				set
				{
					base[MailboxPlanSchema.UMPermissions] = value;
				}
			}

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x06001560 RID: 5472 RVA: 0x0005B5B5 File Offset: 0x000597B5
			// (set) Token: 0x06001561 RID: 5473 RVA: 0x0005B5C7 File Offset: 0x000597C7
			public bool UMSMSMsgWaitingPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.UMSMSMsgWaitingPermissions];
				}
				set
				{
					base[MailboxPlanSchema.UMSMSMsgWaitingPermissions] = value;
				}
			}

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x06001562 RID: 5474 RVA: 0x0005B5DA File Offset: 0x000597DA
			// (set) Token: 0x06001563 RID: 5475 RVA: 0x0005B5EC File Offset: 0x000597EC
			public bool UserMailboxAccessPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.UserMailboxAccessPermissions];
				}
				set
				{
					base[MailboxPlanSchema.UserMailboxAccessPermissions] = value;
				}
			}

			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005B5FF File Offset: 0x000597FF
			// (set) Token: 0x06001565 RID: 5477 RVA: 0x0005B611 File Offset: 0x00059811
			public bool ViewSupervisionListPermissions
			{
				get
				{
					return (bool)base[MailboxPlanSchema.ViewSupervisionListPermissions];
				}
				set
				{
					base[MailboxPlanSchema.ViewSupervisionListPermissions] = value;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x06001566 RID: 5478 RVA: 0x0005B624 File Offset: 0x00059824
			// (set) Token: 0x06001567 RID: 5479 RVA: 0x0005B636 File Offset: 0x00059836
			public string MaxReceiveTransportQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.MaxReceiveTransportQuota];
				}
				set
				{
					base[MailboxPlanSchema.MaxReceiveTransportQuota] = value;
				}
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x06001568 RID: 5480 RVA: 0x0005B644 File Offset: 0x00059844
			// (set) Token: 0x06001569 RID: 5481 RVA: 0x0005B656 File Offset: 0x00059856
			public string MaxRecipientsTransportQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.MaxRecipientsTransportQuota];
				}
				set
				{
					base[MailboxPlanSchema.MaxRecipientsTransportQuota] = value;
				}
			}

			// Token: 0x17000689 RID: 1673
			// (get) Token: 0x0600156A RID: 5482 RVA: 0x0005B664 File Offset: 0x00059864
			// (set) Token: 0x0600156B RID: 5483 RVA: 0x0005B676 File Offset: 0x00059876
			public string MaxSendTransportQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.MaxSendTransportQuota];
				}
				set
				{
					base[MailboxPlanSchema.MaxSendTransportQuota] = value;
				}
			}

			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x0600156C RID: 5484 RVA: 0x0005B684 File Offset: 0x00059884
			// (set) Token: 0x0600156D RID: 5485 RVA: 0x0005B696 File Offset: 0x00059896
			public string ProhibitSendReceiveMaiboxQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.ProhibitSendReceiveMaiboxQuota];
				}
				set
				{
					base[MailboxPlanSchema.ProhibitSendReceiveMaiboxQuota] = value;
				}
			}

			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x0600156E RID: 5486 RVA: 0x0005B6A4 File Offset: 0x000598A4
			// (set) Token: 0x0600156F RID: 5487 RVA: 0x0005B6B6 File Offset: 0x000598B6
			public string SyncAccountsMaxAccountsQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.SyncAccountsMaxAccountsQuota];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsMaxAccountsQuota] = value;
				}
			}

			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x06001570 RID: 5488 RVA: 0x0005B6C4 File Offset: 0x000598C4
			// (set) Token: 0x06001571 RID: 5489 RVA: 0x0005B6D6 File Offset: 0x000598D6
			public string ArchiveQuota
			{
				get
				{
					return (string)base[MailboxPlanSchema.ArchiveQuota];
				}
				set
				{
					base[MailboxPlanSchema.ArchiveQuota] = value;
				}
			}

			// Token: 0x1700068D RID: 1677
			// (get) Token: 0x06001572 RID: 5490 RVA: 0x0005B6E4 File Offset: 0x000598E4
			// (set) Token: 0x06001573 RID: 5491 RVA: 0x0005B6F6 File Offset: 0x000598F6
			public string SyncAccountsPollingInterval
			{
				get
				{
					return (string)base[MailboxPlanSchema.SyncAccountsPollingInterval];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsPollingInterval] = value;
				}
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x06001574 RID: 5492 RVA: 0x0005B704 File Offset: 0x00059904
			// (set) Token: 0x06001575 RID: 5493 RVA: 0x0005B716 File Offset: 0x00059916
			public string SyncAccountsTimeBeforeDormant
			{
				get
				{
					return (string)base[MailboxPlanSchema.SyncAccountsTimeBeforeDormant];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsTimeBeforeDormant] = value;
				}
			}

			// Token: 0x1700068F RID: 1679
			// (get) Token: 0x06001576 RID: 5494 RVA: 0x0005B724 File Offset: 0x00059924
			// (set) Token: 0x06001577 RID: 5495 RVA: 0x0005B736 File Offset: 0x00059936
			public string SyncAccountsTimeBeforeInactive
			{
				get
				{
					return (string)base[MailboxPlanSchema.SyncAccountsTimeBeforeInactive];
				}
				set
				{
					base[MailboxPlanSchema.SyncAccountsTimeBeforeInactive] = value;
				}
			}

			// Token: 0x04000969 RID: 2409
			private static MailboxPlanSchema schema = new MailboxPlanSchema();

			// Token: 0x0400096A RID: 2410
			private string name;

			// Token: 0x0400096B RID: 2411
			private string mailboxPlanIndex;
		}
	}
}
