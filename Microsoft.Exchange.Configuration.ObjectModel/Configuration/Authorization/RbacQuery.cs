using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000249 RID: 585
	internal struct RbacQuery
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x0004CEDC File Offset: 0x0004B0DC
		static RbacQuery()
		{
			foreach (object obj in Enum.GetValues(typeof(RoleType)))
			{
				RoleType roleType = (RoleType)obj;
				RbacQuery.WellKnownQueryProcessors.Add(roleType.ToString(), new RbacQuery.RoleTypeQueryProcessor(roleType));
			}
			List<string> list = new List<string>(RbacQuery.WellKnownQueryProcessors.Keys);
			foreach (string text in list)
			{
				RbacQuery.RbacQueryProcessor predicate = RbacQuery.WellKnownQueryProcessors[text];
				RbacQuery.WellKnownQueryProcessors.Add("!" + text, new RbacQuery.NotQueryProcessor(predicate));
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0004D3D8 File Offset: 0x0004B5D8
		public RbacQuery(string rbacQuery)
		{
			this = new RbacQuery(rbacQuery, null);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0004D3E4 File Offset: 0x0004B5E4
		public RbacQuery(string rbacQuery, ADRawEntry adRawEntry)
		{
			if (string.IsNullOrEmpty(rbacQuery))
			{
				throw new ArgumentNullException("rbacQuery");
			}
			if (!RbacQuery.WellKnownQueryProcessors.TryGetValue(rbacQuery, out this.queryProcessor) && !RbacQuery.ConditionalQueryProcessors.TryParse(rbacQuery, out this.queryProcessor) && !RbacQuery.CmdletQueryProcessor.TryParse(rbacQuery, out this.queryProcessor))
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid RBAC query.", rbacQuery));
			}
			if (adRawEntry != null)
			{
				RbacQuery.CmdletQueryProcessor cmdletQueryProcessor = this.queryProcessor as RbacQuery.CmdletQueryProcessor;
				if (cmdletQueryProcessor != null)
				{
					cmdletQueryProcessor.TargetObject = adRawEntry;
				}
			}
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0004D45D File Offset: 0x0004B65D
		public static void RegisterQueryProcessor(string role, RbacQuery.RbacQueryProcessor processor)
		{
			RbacQuery.WellKnownQueryProcessors.Add(role, processor);
			RbacQuery.WellKnownQueryProcessors.Add("!" + role, new RbacQuery.NotQueryProcessor(processor));
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0004D486 File Offset: 0x0004B686
		public bool IsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return this.queryProcessor.IsInRole(rbacConfiguration);
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004D494 File Offset: 0x0004B694
		public bool CanCache
		{
			get
			{
				return this.queryProcessor.CanCache;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0004D4A1 File Offset: 0x0004B6A1
		public bool IsCmdletQuery
		{
			get
			{
				return this.queryProcessor is RbacQuery.CmdletQueryProcessor;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0004D4B1 File Offset: 0x0004B6B1
		public string SnapInName
		{
			get
			{
				if (!this.IsCmdletQuery)
				{
					return null;
				}
				return ((RbacQuery.CmdletQueryProcessor)this.queryProcessor).SnapInName;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0004D4CD File Offset: 0x0004B6CD
		public string CmdletName
		{
			get
			{
				if (!this.IsCmdletQuery)
				{
					return null;
				}
				return ((RbacQuery.CmdletQueryProcessor)this.queryProcessor).CmdletName;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004D4E9 File Offset: 0x0004B6E9
		public string QualifiedCmdletName
		{
			get
			{
				if (!this.IsCmdletQuery)
				{
					return null;
				}
				return ((RbacQuery.CmdletQueryProcessor)this.queryProcessor).QualifiedCmdletName;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0004D505 File Offset: 0x0004B705
		public string[] ParameterNames
		{
			get
			{
				if (!this.IsCmdletQuery)
				{
					return null;
				}
				return ((RbacQuery.CmdletQueryProcessor)this.queryProcessor).ParameterNames;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004D521 File Offset: 0x0004B721
		public ScopeSet ScopeSet
		{
			get
			{
				if (!this.IsCmdletQuery)
				{
					return null;
				}
				return ((RbacQuery.CmdletQueryProcessor)this.queryProcessor).ScopeSet;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0004D53D File Offset: 0x0004B73D
		public RoleType RoleType
		{
			get
			{
				if (!this.IsRoleTypeQuery)
				{
					return (RoleType)0;
				}
				return ((RbacQuery.RoleTypeQueryProcessor)this.queryProcessor).RoleType;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0004D559 File Offset: 0x0004B759
		public bool IsRoleTypeQuery
		{
			get
			{
				return this.queryProcessor is RbacQuery.RoleTypeQueryProcessor;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004D569 File Offset: 0x0004B769
		public bool IsExchangeSkuQuery
		{
			get
			{
				return this.queryProcessor is RbacQuery.ExchangeSkuQueryProcessor;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004D579 File Offset: 0x0004B779
		public Datacenter.ExchangeSku ExchangeSku
		{
			get
			{
				return ((RbacQuery.ExchangeSkuQueryProcessor)this.queryProcessor).ExchangeSku;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0004D58B File Offset: 0x0004B78B
		public bool IsBrowserCheckQuery
		{
			get
			{
				return this.queryProcessor is RbacQuery.BrowserQueryProcessor;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004D59B File Offset: 0x0004B79B
		public static bool LegacyIsScoped
		{
			get
			{
				return RbacQuery.LegacyTargetObject is ADRawEntry;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0004D5AA File Offset: 0x0004B7AA
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x0004D5C5 File Offset: 0x0004B7C5
		public static IConfigurable LegacyTargetObject
		{
			get
			{
				return (IConfigurable)HttpContext.Current.Items[RbacQuery.rbacQueryRawEntryKey];
			}
			set
			{
				HttpContext.Current.Items[RbacQuery.rbacQueryRawEntryKey] = value;
			}
		}

		// Token: 0x040005F3 RID: 1523
		internal static Dictionary<string, RbacQuery.RbacQueryProcessor> WellKnownQueryProcessors = new Dictionary<string, RbacQuery.RbacQueryProcessor>(127)
		{
			{
				"*",
				RbacQuery.AccountFeaturesQueryProcessor.Any
			},
			{
				"LiveID",
				RbacQuery.ExchangeSkuQueryProcessor.LiveId
			},
			{
				"Enterprise",
				RbacQuery.ExchangeSkuQueryProcessor.Enterprise
			},
			{
				"Hosted",
				RbacQuery.ExchangeSkuQueryProcessor.PartnerHosted
			},
			{
				"MultiTenant",
				new RbacQuery.AnyQueryProcessor(new List<RbacQuery.RbacQueryProcessor>
				{
					RbacQuery.ExchangeSkuQueryProcessor.LiveId,
					RbacQuery.ExchangeSkuQueryProcessor.ForeFrontForOffice,
					RbacQuery.ExchangeSkuQueryProcessor.PartnerHosted
				})
			},
			{
				"Dedicated",
				RbacQuery.ExchangeSkuQueryProcessor.Dedicated
			},
			{
				"IE",
				RbacQuery.BrowserQueryProcessor.IE
			},
			{
				"SkipToUAndParentalControlCheck",
				RbacQuery.OrganizationPropertyQueryProcessor.SkipToUAndParentalControlCheck
			},
			{
				"OWA",
				RbacQuery.AccountFeaturesQueryProcessor.OWA
			},
			{
				"Admin",
				RbacQuery.AccountFeaturesQueryProcessor.Admin
			},
			{
				"MailboxFullAccess",
				RbacQuery.AccountFeaturesQueryProcessor.MailboxFullAccess
			},
			{
				"UM",
				RbacQuery.AccountFeaturesQueryProcessor.UM
			},
			{
				"ExternalReplies",
				RbacQuery.AccountFeaturesQueryProcessor.ExternalReplies
			},
			{
				"Resource",
				RbacQuery.AccountFeaturesQueryProcessor.Resource
			},
			{
				"UMConfigured",
				RbacQuery.AccountFeaturesQueryProcessor.UMConfigured
			},
			{
				"ActiveSync",
				RbacQuery.AccountFeaturesQueryProcessor.ActiveSync
			},
			{
				"Outlook",
				RbacQuery.AccountFeaturesQueryProcessor.Outlook
			},
			{
				"Impersonated",
				RbacQuery.AccountFeaturesQueryProcessor.Impersonated
			},
			{
				"Partner",
				RbacQuery.AccountFeaturesQueryProcessor.Partner
			},
			{
				"RetentionPolicy",
				RbacQuery.AccountFeaturesQueryProcessor.RetentionPolicy
			},
			{
				"DelegatedAdmin",
				RbacQuery.AccountFeaturesQueryProcessor.DelegatedAdmin
			},
			{
				"ByoidAdmin",
				RbacQuery.ByoidAdminQueryProcessor.ByoidAdmin
			},
			{
				"MachineToPersonTextingOnly",
				RbacQuery.AccountFeaturesQueryProcessor.MachineToPersonTextMessagingOnlyEnabled
			},
			{
				"Mailbox",
				RbacQuery.RecipientTypeQueryProcessor.Mailbox
			},
			{
				"LogonUserMailbox",
				RbacQuery.RecipientTypeQueryProcessor.LogonUserMailbox
			},
			{
				"LogonMailUser",
				RbacQuery.RecipientTypeQueryProcessor.LogonMailUser
			},
			{
				"Rules",
				RbacQuery.OwaSegmentationQueryProcessor.Rules
			},
			{
				"Signatures",
				RbacQuery.OwaSegmentationQueryProcessor.Signatures
			},
			{
				"SpellChecker",
				RbacQuery.OwaSegmentationQueryProcessor.SpellChecker
			},
			{
				"Calendar",
				RbacQuery.OwaSegmentationQueryProcessor.Calendar
			},
			{
				"RemindersAndNotifications",
				RbacQuery.OwaSegmentationQueryProcessor.RemindersAndNotifications
			},
			{
				"GlobalAddressList",
				RbacQuery.OwaSegmentationQueryProcessor.GlobalAddressList
			},
			{
				"Contacts",
				RbacQuery.OwaSegmentationQueryProcessor.Contacts
			},
			{
				"OWALight",
				RbacQuery.OwaSegmentationQueryProcessor.OWALight
			},
			{
				"ChangePassword",
				RbacQuery.OwaSegmentationQueryProcessor.ChangePassword
			},
			{
				"SMime",
				RbacQuery.OwaSegmentationQueryProcessor.SMime
			},
			{
				"UMIntegration",
				RbacQuery.OwaSegmentationQueryProcessor.UMIntegration
			},
			{
				"TextMessaging",
				RbacQuery.OwaSegmentationQueryProcessor.TextMessaging
			},
			{
				"JunkEmail",
				RbacQuery.OwaSegmentationQueryProcessor.JunkEmail
			},
			{
				"SetPhotoEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.SetPhotoEnabled
			},
			{
				"MobileDevices",
				RbacQuery.OwaSegmentationQueryProcessor.MobileDevices
			},
			{
				"OrgMgmControlPanel",
				RbacQuery.ExchangeFeaturesEnabledProcessor.OrgMgmECPEnabled
			},
			{
				"IsLicensingEnforced",
				RbacQuery.OrganizationPropertyQueryProcessor.IsLicensingEnforced
			},
			{
				"BposUser",
				RbacQuery.AccountFeaturesQueryProcessor.BposUser
			},
			{
				"FFO",
				RbacQuery.FFOQueryProcessor.FFO
			},
			{
				"EOPStandard",
				RbacQuery.EOPStandardQueryProcessor.EOPStandard
			},
			{
				"EOPPremium",
				RbacQuery.OrganizationConfigurationQueryProcessor.IsEOPPremiumCapability
			},
			{
				"FacebookEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.FacebookEnabled
			},
			{
				"LinkedInEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.LinkedInEnabled
			},
			{
				"WacExternalServicesEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.WacExternalServicesEnabled
			},
			{
				"WacOMEXEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.WacOMEXEnabled
			},
			{
				"FFOMigrationInProgress",
				RbacQuery.OrganizationConfigurationQueryProcessor.FFOMigrationInProgressQueryProcessor
			},
			{
				"IsPilotMode",
				RbacQuery.OrganizationConfigurationQueryProcessor.IsPilotModeQueryProcessor
			},
			{
				"IsGallatin",
				RbacQuery.GallatinQueryProcessor.IsGallatin
			},
			{
				"ReportJunkEmailEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.ReportJunkEmailEnabled
			},
			{
				"GroupCreationEnabled",
				RbacQuery.OwaSegmentationQueryProcessor.GroupCreationEnabled
			},
			{
				"EndUserExperience",
				new RbacQuery.EndUserExperienceQueryProcessor()
			}
		};

		// Token: 0x040005F4 RID: 1524
		private RbacQuery.RbacQueryProcessor queryProcessor;

		// Token: 0x040005F5 RID: 1525
		private static object rbacQueryRawEntryKey = new object();

		// Token: 0x0200024A RID: 586
		internal abstract class RbacQueryProcessor
		{
			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004D5DC File Offset: 0x0004B7DC
			public virtual bool CanCache
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060014B9 RID: 5305 RVA: 0x0004D5E0 File Offset: 0x0004B7E0
			public bool IsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return this.TryIsInRole(rbacConfiguration) ?? false;
			}

			// Token: 0x060014BA RID: 5306
			public abstract bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration);
		}

		// Token: 0x0200024B RID: 587
		private sealed class NotQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014BC RID: 5308 RVA: 0x0004D60F File Offset: 0x0004B80F
			public NotQueryProcessor(RbacQuery.RbacQueryProcessor predicate)
			{
				this.Predicate = predicate;
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x060014BD RID: 5309 RVA: 0x0004D61E File Offset: 0x0004B81E
			// (set) Token: 0x060014BE RID: 5310 RVA: 0x0004D626 File Offset: 0x0004B826
			public RbacQuery.RbacQueryProcessor Predicate { get; private set; }

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x060014BF RID: 5311 RVA: 0x0004D62F File Offset: 0x0004B82F
			public override bool CanCache
			{
				get
				{
					return this.Predicate.CanCache;
				}
			}

			// Token: 0x060014C0 RID: 5312 RVA: 0x0004D63C File Offset: 0x0004B83C
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				bool? flag = this.Predicate.TryIsInRole(rbacConfiguration);
				if (flag == null)
				{
					return flag;
				}
				bool? flag2 = flag;
				if (flag2 == null)
				{
					return null;
				}
				return new bool?(!flag2.GetValueOrDefault());
			}
		}

		// Token: 0x0200024C RID: 588
		private sealed class AnyQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014C1 RID: 5313 RVA: 0x0004D685 File Offset: 0x0004B885
			public AnyQueryProcessor(List<RbacQuery.RbacQueryProcessor> predicates)
			{
				this.Predicates = predicates;
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0004D694 File Offset: 0x0004B894
			// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0004D69C File Offset: 0x0004B89C
			public List<RbacQuery.RbacQueryProcessor> Predicates { get; private set; }

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
			public override bool CanCache
			{
				get
				{
					foreach (RbacQuery.RbacQueryProcessor rbacQueryProcessor in this.Predicates)
					{
						if (rbacQueryProcessor.CanCache)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x0004D704 File Offset: 0x0004B904
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				bool? result = null;
				foreach (RbacQuery.RbacQueryProcessor rbacQueryProcessor in this.Predicates)
				{
					bool? flag = rbacQueryProcessor.TryIsInRole(rbacConfiguration);
					if (flag == true)
					{
						return new bool?(true);
					}
					if (flag == false)
					{
						result = new bool?(false);
					}
				}
				return result;
			}
		}

		// Token: 0x0200024D RID: 589
		private sealed class ExchangeSkuQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0004D7A4 File Offset: 0x0004B9A4
			// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0004D7AC File Offset: 0x0004B9AC
			public Datacenter.ExchangeSku ExchangeSku { get; private set; }

			// Token: 0x060014C8 RID: 5320 RVA: 0x0004D7B5 File Offset: 0x0004B9B5
			private ExchangeSkuQueryProcessor(Datacenter.ExchangeSku exchangeSku)
			{
				this.ExchangeSku = exchangeSku;
			}

			// Token: 0x060014C9 RID: 5321 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(this.ExchangeSku == Datacenter.GetExchangeSku());
			}

			// Token: 0x040005F8 RID: 1528
			public static readonly RbacQuery.RbacQueryProcessor LiveId = new RbacQuery.ExchangeSkuQueryProcessor(Datacenter.ExchangeSku.ExchangeDatacenter);

			// Token: 0x040005F9 RID: 1529
			public static readonly RbacQuery.RbacQueryProcessor Enterprise = new RbacQuery.ExchangeSkuQueryProcessor(Datacenter.ExchangeSku.Enterprise);

			// Token: 0x040005FA RID: 1530
			public static readonly RbacQuery.RbacQueryProcessor PartnerHosted = new RbacQuery.ExchangeSkuQueryProcessor(Datacenter.ExchangeSku.PartnerHosted);

			// Token: 0x040005FB RID: 1531
			public static readonly RbacQuery.RbacQueryProcessor ForeFrontForOffice = new RbacQuery.ExchangeSkuQueryProcessor(Datacenter.ExchangeSku.ForefrontForOfficeDatacenter);

			// Token: 0x040005FC RID: 1532
			public static readonly RbacQuery.RbacQueryProcessor Dedicated = new RbacQuery.ExchangeSkuQueryProcessor(Datacenter.ExchangeSku.DatacenterDedicated);
		}

		// Token: 0x0200024E RID: 590
		private sealed class RoleTypeQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060014CB RID: 5323 RVA: 0x0004D811 File Offset: 0x0004BA11
			// (set) Token: 0x060014CC RID: 5324 RVA: 0x0004D819 File Offset: 0x0004BA19
			public RoleType RoleType { get; private set; }

			// Token: 0x060014CD RID: 5325 RVA: 0x0004D822 File Offset: 0x0004BA22
			public RoleTypeQueryProcessor(RoleType roleType)
			{
				this.RoleType = roleType;
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x0004D831 File Offset: 0x0004BA31
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(rbacConfiguration.HasRoleOfType(this.RoleType));
			}
		}

		// Token: 0x0200024F RID: 591
		private sealed class RecipientTypeQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060014CF RID: 5327 RVA: 0x0004D844 File Offset: 0x0004BA44
			// (set) Token: 0x060014D0 RID: 5328 RVA: 0x0004D84C File Offset: 0x0004BA4C
			public RecipientType RecipientType { get; private set; }

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0004D855 File Offset: 0x0004BA55
			// (set) Token: 0x060014D2 RID: 5330 RVA: 0x0004D85D File Offset: 0x0004BA5D
			public bool ForLogonUser { get; private set; }

			// Token: 0x060014D3 RID: 5331 RVA: 0x0004D866 File Offset: 0x0004BA66
			public RecipientTypeQueryProcessor(RecipientType recipientType, bool forLogonUser)
			{
				this.RecipientType = recipientType;
				this.ForLogonUser = forLogonUser;
			}

			// Token: 0x060014D4 RID: 5332 RVA: 0x0004D87C File Offset: 0x0004BA7C
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(this.RecipientType == (this.ForLogonUser ? rbacConfiguration.LogonUserRecipientType : rbacConfiguration.ExecutingUserRecipientType));
			}

			// Token: 0x040005FF RID: 1535
			public static readonly RbacQuery.RecipientTypeQueryProcessor Mailbox = new RbacQuery.RecipientTypeQueryProcessor(RecipientType.UserMailbox, false);

			// Token: 0x04000600 RID: 1536
			public static readonly RbacQuery.RecipientTypeQueryProcessor LogonUserMailbox = new RbacQuery.RecipientTypeQueryProcessor(RecipientType.UserMailbox, true);

			// Token: 0x04000601 RID: 1537
			public static readonly RbacQuery.RecipientTypeQueryProcessor LogonMailUser = new RbacQuery.RecipientTypeQueryProcessor(RecipientType.MailUser, true);
		}

		// Token: 0x02000250 RID: 592
		private sealed class OrganizationPropertyQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014D6 RID: 5334 RVA: 0x0004D8C7 File Offset: 0x0004BAC7
			public OrganizationPropertyQueryProcessor(Func<OrganizationProperties, bool> predicate)
			{
				this.predicate = predicate;
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0004D8D6 File Offset: 0x0004BAD6
			public override bool CanCache
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060014D8 RID: 5336 RVA: 0x0004D8DC File Offset: 0x0004BADC
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				OrganizationProperties arg;
				if (!OrganizationPropertyCache.TryGetOrganizationProperties(rbacConfiguration.OrganizationId, out arg))
				{
					throw new ArgumentException("The organization does not exist in AD. OrgId:" + rbacConfiguration.OrganizationId);
				}
				return new bool?(this.predicate(arg));
			}

			// Token: 0x04000604 RID: 1540
			public static readonly RbacQuery.RbacQueryProcessor SkipToUAndParentalControlCheck = new RbacQuery.OrganizationPropertyQueryProcessor((OrganizationProperties x) => x.SkipToUAndParentalControlCheck);

			// Token: 0x04000605 RID: 1541
			public static readonly RbacQuery.RbacQueryProcessor IsLicensingEnforced = new RbacQuery.OrganizationPropertyQueryProcessor((OrganizationProperties x) => x.IsLicensingEnforced);

			// Token: 0x04000606 RID: 1542
			private Func<OrganizationProperties, bool> predicate;
		}

		// Token: 0x02000251 RID: 593
		private sealed class OrganizationConfigurationQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014DC RID: 5340 RVA: 0x0004D98B File Offset: 0x0004BB8B
			public OrganizationConfigurationQueryProcessor(Func<ExchangeConfigurationUnit, bool> predicate, bool canCache)
			{
				this.predicate = predicate;
				this.canCache = canCache;
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060014DD RID: 5341 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
			public override bool CanCache
			{
				get
				{
					return this.canCache;
				}
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x0004D9B0 File Offset: 0x0004BBB0
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				bool? result;
				try
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(rbacConfiguration.OrganizationId, false);
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 265, "TryIsInRole", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\RbacQuery\\RbacQuery.cs");
					ExchangeConfigurationUnit arg = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(rbacConfiguration.OrganizationId.ConfigurationUnit);
					result = new bool?(this.predicate(arg));
				}
				catch
				{
					result = new bool?(false);
				}
				return result;
			}

			// Token: 0x04000609 RID: 1545
			private readonly bool canCache = true;

			// Token: 0x0400060A RID: 1546
			private Func<ExchangeConfigurationUnit, bool> predicate;

			// Token: 0x0400060B RID: 1547
			public static RbacQuery.OrganizationConfigurationQueryProcessor FFOMigrationInProgressQueryProcessor = new RbacQuery.OrganizationConfigurationQueryProcessor((ExchangeConfigurationUnit v) => v.IsFfoMigrationInProgress, false);

			// Token: 0x0400060C RID: 1548
			public static RbacQuery.OrganizationConfigurationQueryProcessor IsPilotModeQueryProcessor = new RbacQuery.OrganizationConfigurationQueryProcessor((ExchangeConfigurationUnit v) => v.IsPilotingOrganization, false);

			// Token: 0x0400060D RID: 1549
			public static RbacQuery.OrganizationConfigurationQueryProcessor IsEOPPremiumCapability = new RbacQuery.OrganizationConfigurationQueryProcessor((ExchangeConfigurationUnit v) => v.PersistedCapabilities.Contains(Capability.BPOS_S_EopPremiumAddOn) && !v.PersistedCapabilities.Contains(Capability.BPOS_S_Standard) && !v.PersistedCapabilities.Contains(Capability.BPOS_S_Enterprise), false);

			// Token: 0x0400060E RID: 1550
			public static RbacQuery.OrganizationConfigurationQueryProcessor IsEOPStandardCapability = new RbacQuery.OrganizationConfigurationQueryProcessor((ExchangeConfigurationUnit v) => v.PersistedCapabilities.Contains(Capability.BPOS_S_EopStandardAddOn), false);
		}

		// Token: 0x02000252 RID: 594
		private sealed class ExchangeFeaturesEnabledProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014E4 RID: 5348 RVA: 0x0004DB29 File Offset: 0x0004BD29
			private ExchangeFeaturesEnabledProcessor(bool featureEnabled)
			{
				this.featureEnabled = featureEnabled;
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x0004DB38 File Offset: 0x0004BD38
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(this.featureEnabled);
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0004DB48 File Offset: 0x0004BD48
			private static bool OMECPEnabled
			{
				get
				{
					return Datacenter.GetExchangeSku() != Datacenter.ExchangeSku.PartnerHosted || RbacQuery.ExchangeFeaturesEnabledProcessor.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "OMECPDisabled") == null;
				}
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x0004DB78 File Offset: 0x0004BD78
			private static object ReadRegistryKey(string keyPath, string valueName)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyPath))
				{
					if (registryKey != null)
					{
						return registryKey.GetValue(valueName, null);
					}
				}
				return null;
			}

			// Token: 0x04000613 RID: 1555
			private readonly bool featureEnabled;

			// Token: 0x04000614 RID: 1556
			public static readonly RbacQuery.RbacQueryProcessor OrgMgmECPEnabled = new RbacQuery.ExchangeFeaturesEnabledProcessor(RbacQuery.ExchangeFeaturesEnabledProcessor.OMECPEnabled);
		}

		// Token: 0x02000253 RID: 595
		private sealed class AccountFeaturesQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x060014E9 RID: 5353 RVA: 0x0004DBD1 File Offset: 0x0004BDD1
			private AccountFeaturesQueryProcessor(Func<ExchangeRunspaceConfiguration, bool> predicate, bool canCache)
			{
				this.predicate = predicate;
				this.canCache = canCache;
			}

			// Token: 0x060014EA RID: 5354 RVA: 0x0004DBEE File Offset: 0x0004BDEE
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(this.predicate(rbacConfiguration));
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004DC01 File Offset: 0x0004BE01
			public override bool CanCache
			{
				get
				{
					return this.canCache;
				}
			}

			// Token: 0x04000615 RID: 1557
			private readonly bool canCache = true;

			// Token: 0x04000616 RID: 1558
			public static readonly RbacQuery.RbacQueryProcessor OWA = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsAllowedOWA, true);

			// Token: 0x04000617 RID: 1559
			public static readonly RbacQuery.RbacQueryProcessor MailboxFullAccess = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => !x.Impersonated || !x.OpenMailboxAsAdmin, true);

			// Token: 0x04000618 RID: 1560
			public static readonly RbacQuery.RbacQueryProcessor UM = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsUmEnabled, true);

			// Token: 0x04000619 RID: 1561
			public static readonly RbacQuery.RbacQueryProcessor ExternalReplies = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserHasExternalOofOptions, true);

			// Token: 0x0400061A RID: 1562
			public static readonly RbacQuery.RbacQueryProcessor Resource = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsResource, true);

			// Token: 0x0400061B RID: 1563
			public static readonly RbacQuery.RbacQueryProcessor ActiveSync = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsActiveSyncEnabled, true);

			// Token: 0x0400061C RID: 1564
			public static readonly RbacQuery.RbacQueryProcessor Outlook = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsMAPIEnabled, true);

			// Token: 0x0400061D RID: 1565
			public static readonly RbacQuery.RbacQueryProcessor UMConfigured = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsUmConfigured, false);

			// Token: 0x0400061E RID: 1566
			public static readonly RbacQuery.RbacQueryProcessor Impersonated = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.Impersonated, true);

			// Token: 0x0400061F RID: 1567
			public static readonly RbacQuery.RbacQueryProcessor Partner = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => ExchangeRunspaceConfiguration.IsAllowedOrganizationForPartnerAccounts(x.OrganizationId), true);

			// Token: 0x04000620 RID: 1568
			public static readonly RbacQuery.RbacQueryProcessor RetentionPolicy = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserHasRetentionPolicy, true);

			// Token: 0x04000621 RID: 1569
			public static readonly RbacQuery.RbacQueryProcessor DelegatedAdmin = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.DelegatedPrincipal != null, true);

			// Token: 0x04000622 RID: 1570
			public static readonly RbacQuery.RbacQueryProcessor Any = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => true, true);

			// Token: 0x04000623 RID: 1571
			public static readonly RbacQuery.RbacQueryProcessor MachineToPersonTextMessagingOnlyEnabled = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => !x.ExecutingUserIsPersonToPersonTextMessagingEnabled && x.ExecutingUserIsMachineToPersonTextMessagingEnabled, false);

			// Token: 0x04000624 RID: 1572
			public static readonly RbacQuery.RbacQueryProcessor Admin = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.HasAdminRoles, true);

			// Token: 0x04000625 RID: 1573
			public static readonly RbacQuery.RbacQueryProcessor BposUser = new RbacQuery.AccountFeaturesQueryProcessor((ExchangeRunspaceConfiguration x) => x.ExecutingUserIsBposUser, true);

			// Token: 0x04000626 RID: 1574
			private Func<ExchangeRunspaceConfiguration, bool> predicate;
		}

		// Token: 0x02000254 RID: 596
		private sealed class OwaSegmentationQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x060014FD RID: 5373 RVA: 0x0004DF35 File Offset: 0x0004C135
			// (set) Token: 0x060014FE RID: 5374 RVA: 0x0004DF3D File Offset: 0x0004C13D
			public ADPropertyDefinition PropertyDefinition { get; private set; }

			// Token: 0x060014FF RID: 5375 RVA: 0x0004DF46 File Offset: 0x0004C146
			private OwaSegmentationQueryProcessor(ADPropertyDefinition propertyDefinition)
			{
				this.PropertyDefinition = propertyDefinition;
			}

			// Token: 0x06001500 RID: 5376 RVA: 0x0004DF55 File Offset: 0x0004C155
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(rbacConfiguration.OwaSegmentationSettings[this.PropertyDefinition]);
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x06001501 RID: 5377 RVA: 0x0004DF6D File Offset: 0x0004C16D
			public override bool CanCache
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000637 RID: 1591
			public static readonly RbacQuery.RbacQueryProcessor Rules = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.RulesEnabled);

			// Token: 0x04000638 RID: 1592
			public static readonly RbacQuery.RbacQueryProcessor Signatures = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.SignaturesEnabled);

			// Token: 0x04000639 RID: 1593
			public static readonly RbacQuery.RbacQueryProcessor SpellChecker = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.SpellCheckerEnabled);

			// Token: 0x0400063A RID: 1594
			public static readonly RbacQuery.RbacQueryProcessor Calendar = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.CalendarEnabled);

			// Token: 0x0400063B RID: 1595
			public static readonly RbacQuery.RbacQueryProcessor RemindersAndNotifications = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.RemindersAndNotificationsEnabled);

			// Token: 0x0400063C RID: 1596
			public static readonly RbacQuery.RbacQueryProcessor GlobalAddressList = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.GlobalAddressListEnabled);

			// Token: 0x0400063D RID: 1597
			public static readonly RbacQuery.RbacQueryProcessor Contacts = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.ContactsEnabled);

			// Token: 0x0400063E RID: 1598
			public static readonly RbacQuery.RbacQueryProcessor OWALight = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.OWALightEnabled);

			// Token: 0x0400063F RID: 1599
			public static readonly RbacQuery.RbacQueryProcessor ChangePassword = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.ChangePasswordEnabled);

			// Token: 0x04000640 RID: 1600
			public static readonly RbacQuery.RbacQueryProcessor SMime = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.SMimeEnabled);

			// Token: 0x04000641 RID: 1601
			public static readonly RbacQuery.RbacQueryProcessor UMIntegration = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.UMIntegrationEnabled);

			// Token: 0x04000642 RID: 1602
			public static readonly RbacQuery.RbacQueryProcessor TextMessaging = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.TextMessagingEnabled);

			// Token: 0x04000643 RID: 1603
			public static readonly RbacQuery.RbacQueryProcessor JunkEmail = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.JunkEmailEnabled);

			// Token: 0x04000644 RID: 1604
			public static readonly RbacQuery.RbacQueryProcessor SetPhotoEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.SetPhotoEnabled);

			// Token: 0x04000645 RID: 1605
			public static readonly RbacQuery.RbacQueryProcessor MobileDevices = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.ActiveSyncIntegrationEnabled);

			// Token: 0x04000646 RID: 1606
			public static readonly RbacQuery.RbacQueryProcessor FacebookEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.FacebookEnabled);

			// Token: 0x04000647 RID: 1607
			public static readonly RbacQuery.RbacQueryProcessor LinkedInEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.LinkedInEnabled);

			// Token: 0x04000648 RID: 1608
			public static readonly RbacQuery.RbacQueryProcessor WacExternalServicesEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.WacExternalServicesEnabled);

			// Token: 0x04000649 RID: 1609
			public static readonly RbacQuery.RbacQueryProcessor WacOMEXEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.WacOMEXEnabled);

			// Token: 0x0400064A RID: 1610
			public static readonly RbacQuery.RbacQueryProcessor ReportJunkEmailEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.ReportJunkEmailEnabled);

			// Token: 0x0400064B RID: 1611
			public static readonly RbacQuery.RbacQueryProcessor GroupCreationEnabled = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.GroupCreationEnabled);

			// Token: 0x0400064C RID: 1612
			public static readonly RbacQuery.RbacQueryProcessor SkipCreateUnifiedGroupCustomSharepointClassification = new RbacQuery.OwaSegmentationQueryProcessor(OwaMailboxPolicySchema.SkipCreateUnifiedGroupCustomSharepointClassification);
		}

		// Token: 0x02000255 RID: 597
		private sealed class BrowserQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x06001503 RID: 5379 RVA: 0x0004E0C7 File Offset: 0x0004C2C7
			// (set) Token: 0x06001504 RID: 5380 RVA: 0x0004E0CF File Offset: 0x0004C2CF
			public string BrowserSignature { get; private set; }

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
			// (set) Token: 0x06001506 RID: 5382 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
			public int MajorVersion { get; private set; }

			// Token: 0x06001507 RID: 5383 RVA: 0x0004E0E9 File Offset: 0x0004C2E9
			private BrowserQueryProcessor(string browserSignature, int majorVersion)
			{
				this.BrowserSignature = browserSignature;
				this.MajorVersion = majorVersion;
			}

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06001508 RID: 5384 RVA: 0x0004E0FF File Offset: 0x0004C2FF
			public override bool CanCache
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001509 RID: 5385 RVA: 0x0004E104 File Offset: 0x0004C304
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				return new bool?(browser.MajorVersion >= this.MajorVersion && browser.Browser.IndexOf(this.BrowserSignature, StringComparison.OrdinalIgnoreCase) >= 0);
			}

			// Token: 0x0400064E RID: 1614
			public static readonly RbacQuery.RbacQueryProcessor IE = new RbacQuery.BrowserQueryProcessor("IE", 6);
		}

		// Token: 0x02000256 RID: 598
		private sealed class ByoidAdminQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x0600150B RID: 5387 RVA: 0x0004E161 File Offset: 0x0004C361
			private ByoidAdminQueryProcessor()
			{
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004E169 File Offset: 0x0004C369
			public override bool CanCache
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600150D RID: 5389 RVA: 0x0004E16C File Offset: 0x0004C36C
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["msExchOrganizationContext"]));
			}

			// Token: 0x04000651 RID: 1617
			public static readonly RbacQuery.ByoidAdminQueryProcessor ByoidAdmin = new RbacQuery.ByoidAdminQueryProcessor();
		}

		// Token: 0x02000257 RID: 599
		public sealed class ConditionalQueryProcessors
		{
			// Token: 0x0600150F RID: 5391 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
			public static bool TryParse(string rbacQuery, out RbacQuery.RbacQueryProcessor queryProcessor)
			{
				queryProcessor = null;
				bool flag = false;
				if (rbacQuery == null || rbacQuery.Length == 0)
				{
					return false;
				}
				if (rbacQuery[0] == '!')
				{
					flag = true;
					rbacQuery = rbacQuery.Substring(1);
				}
				if (RbacQuery.ConditionalQueryProcessors.creators != null)
				{
					foreach (RbacQuery.ConditionalQueryProcessors.QueryProcesorCreator queryProcesorCreator in RbacQuery.ConditionalQueryProcessors.creators)
					{
						if (queryProcesorCreator(rbacQuery, out queryProcessor))
						{
							if (flag)
							{
								queryProcessor = new RbacQuery.NotQueryProcessor(queryProcessor);
							}
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06001510 RID: 5392 RVA: 0x0004E238 File Offset: 0x0004C438
			public static void Regist(RbacQuery.ConditionalQueryProcessors.QueryProcesorCreator creator)
			{
				if (RbacQuery.ConditionalQueryProcessors.creators == null)
				{
					RbacQuery.ConditionalQueryProcessors.creators = new List<RbacQuery.ConditionalQueryProcessors.QueryProcesorCreator>();
				}
				RbacQuery.ConditionalQueryProcessors.creators.Add(creator);
			}

			// Token: 0x04000652 RID: 1618
			private static List<RbacQuery.ConditionalQueryProcessors.QueryProcesorCreator> creators;

			// Token: 0x02000258 RID: 600
			// (Invoke) Token: 0x06001513 RID: 5395
			public delegate bool QueryProcesorCreator(string rbacQuery, out RbacQuery.RbacQueryProcessor queryProcessor);
		}

		// Token: 0x02000259 RID: 601
		private sealed class CmdletQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001516 RID: 5398 RVA: 0x0004E25E File Offset: 0x0004C45E
			// (set) Token: 0x06001517 RID: 5399 RVA: 0x0004E266 File Offset: 0x0004C466
			public string SnapInName { get; private set; }

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x06001518 RID: 5400 RVA: 0x0004E26F File Offset: 0x0004C46F
			// (set) Token: 0x06001519 RID: 5401 RVA: 0x0004E277 File Offset: 0x0004C477
			public string CmdletName { get; private set; }

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x0600151A RID: 5402 RVA: 0x0004E280 File Offset: 0x0004C480
			// (set) Token: 0x0600151B RID: 5403 RVA: 0x0004E288 File Offset: 0x0004C488
			public string QualifiedCmdletName { get; private set; }

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x0600151C RID: 5404 RVA: 0x0004E291 File Offset: 0x0004C491
			// (set) Token: 0x0600151D RID: 5405 RVA: 0x0004E299 File Offset: 0x0004C499
			public string[] ParameterNames { get; private set; }

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x0600151E RID: 5406 RVA: 0x0004E2A2 File Offset: 0x0004C4A2
			// (set) Token: 0x0600151F RID: 5407 RVA: 0x0004E2AA File Offset: 0x0004C4AA
			public ScopeSet ScopeSet { get; private set; }

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x06001520 RID: 5408 RVA: 0x0004E2B3 File Offset: 0x0004C4B3
			// (set) Token: 0x06001521 RID: 5409 RVA: 0x0004E2BB File Offset: 0x0004C4BB
			public ADRawEntry TargetObject { get; set; }

			// Token: 0x06001522 RID: 5410 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
			static CmdletQueryProcessor()
			{
				using (Stream manifestResourceStream = typeof(RbacQuery).Assembly.GetManifestResourceStream(typeof(RbacQuery), "RbacQuery.txt"))
				{
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						RbacQuery.CmdletQueryProcessor.regex = new Regex(streamReader.ReadToEnd(), RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
					}
				}
			}

			// Token: 0x06001523 RID: 5411 RVA: 0x0004E344 File Offset: 0x0004C544
			public static bool TryParse(string rbacQuery, out RbacQuery.RbacQueryProcessor queryProcessor)
			{
				Match match = RbacQuery.CmdletQueryProcessor.regex.Match(rbacQuery);
				if (match.Success)
				{
					queryProcessor = new RbacQuery.CmdletQueryProcessor(match);
					return true;
				}
				queryProcessor = null;
				return false;
			}

			// Token: 0x06001524 RID: 5412 RVA: 0x0004E374 File Offset: 0x0004C574
			private CmdletQueryProcessor(Match m)
			{
				this.SnapInName = m.Groups["snapinName"].Value;
				if (string.IsNullOrEmpty(this.SnapInName))
				{
					this.SnapInName = "Microsoft.Exchange.Management.PowerShell.E2010";
				}
				else if (this.SnapInName.IndexOf('.') == -1)
				{
					this.SnapInName = SnapInAliasMap.GetSnapInName(this.SnapInName);
				}
				this.CmdletName = m.Groups["cmdletName"].Value;
				this.QualifiedCmdletName = this.SnapInName + "\\" + this.CmdletName;
				CaptureCollection captures = m.Groups["parameterName"].Captures;
				if (captures.Count > 0)
				{
					this.ParameterNames = new string[captures.Count];
					for (int i = 0; i < this.ParameterNames.Length; i++)
					{
						this.ParameterNames[i] = captures[i].Value;
					}
				}
				ADScope recipientReadScope = RbacQuery.CmdletQueryProcessor.CreateRbacScope(m.Groups["domainReadScope"].Value, false);
				ADScope configReadScope = RbacQuery.CmdletQueryProcessor.CreateRbacScope(m.Groups["configScope"].Value, false);
				ADScopeCollection[] recipientWriteScopes = null;
				CaptureCollection captures2 = m.Groups["domainWriteScope"].Captures;
				if (captures2.Count > 0)
				{
					List<ADScope> list = new List<ADScope>(captures2.Count);
					for (int j = 0; j < captures2.Count; j++)
					{
						ADScope adscope = RbacQuery.CmdletQueryProcessor.CreateRbacScope(captures2[j].Value, true);
						if (adscope != null)
						{
							list.Add(adscope);
						}
					}
					recipientWriteScopes = new ADScopeCollection[]
					{
						new ADScopeCollection(list)
					};
				}
				this.ScopeSet = new ScopeSet(recipientReadScope, recipientWriteScopes, configReadScope, null);
			}

			// Token: 0x06001525 RID: 5413 RVA: 0x0004E538 File Offset: 0x0004C738
			private static ScopeType GetScopeType(string scopeTypeName)
			{
				switch (scopeTypeName)
				{
				case "Organization":
					return ScopeType.Organization;
				case "MyGAL":
					return ScopeType.MyGAL;
				case "Self":
					return ScopeType.Self;
				case "MyDirectReports":
					return ScopeType.MyDirectReports;
				case "OU":
					return ScopeType.OU;
				case "MyDistributionGroups":
					return ScopeType.MyDistributionGroups;
				case "MyExecutive":
					return ScopeType.MyExecutive;
				case "OrganizationConfig":
					return ScopeType.OrganizationConfig;
				case "ServerScope":
					return ScopeType.CustomConfigScope;
				}
				return ScopeType.None;
			}

			// Token: 0x06001526 RID: 5414 RVA: 0x0004E624 File Offset: 0x0004C824
			private static ADScope CreateRbacScope(string scopeTypeName, bool ignoreOrganizationScope)
			{
				ScopeType scopeType = RbacQuery.CmdletQueryProcessor.GetScopeType(scopeTypeName);
				if (ignoreOrganizationScope && scopeType == ScopeType.Organization)
				{
					scopeType = ScopeType.None;
				}
				return new RbacScope(scopeType);
			}

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004E647 File Offset: 0x0004C847
			public override bool CanCache
			{
				get
				{
					return !RbacQuery.LegacyIsScoped && !this.IsScoped;
				}
			}

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004E65B File Offset: 0x0004C85B
			public bool IsScoped
			{
				get
				{
					return this.TargetObject != null;
				}
			}

			// Token: 0x06001529 RID: 5417 RVA: 0x0004E66C File Offset: 0x0004C86C
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				ADRawEntry adrawEntry = this.TargetObject ?? (RbacQuery.LegacyTargetObject as ADRawEntry);
				if (adrawEntry != null && !(adrawEntry is ADConfigurationObject) && !(adrawEntry is OrganizationConfig))
				{
					ScopeLocation scopeLocation = this.CmdletName.StartsWith("get-", StringComparison.OrdinalIgnoreCase) ? ScopeLocation.RecipientRead : ScopeLocation.RecipientWrite;
					return new bool?(rbacConfiguration.IsCmdletAllowedInScope(this.CmdletName, this.ParameterNames ?? new string[0], adrawEntry, scopeLocation));
				}
				return new bool?(rbacConfiguration.IsCmdletAllowedInScope(this.QualifiedCmdletName, this.ParameterNames, this.ScopeSet));
			}

			// Token: 0x04000653 RID: 1619
			private static Regex regex;
		}

		// Token: 0x0200025A RID: 602
		private sealed class FFOQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x0600152A RID: 5418 RVA: 0x0004E6FC File Offset: 0x0004C8FC
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(DatacenterRegistry.IsForefrontForOffice() || RbacQuery.OrganizationConfigurationQueryProcessor.IsEOPPremiumCapability.TryIsInRole(rbacConfiguration).Value);
			}

			// Token: 0x0400065A RID: 1626
			public static readonly RbacQuery.FFOQueryProcessor FFO = new RbacQuery.FFOQueryProcessor();
		}

		// Token: 0x0200025B RID: 603
		private sealed class EOPStandardQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x0600152D RID: 5421 RVA: 0x0004E740 File Offset: 0x0004C940
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(DatacenterRegistry.IsForefrontForOffice() || RbacQuery.OrganizationConfigurationQueryProcessor.IsEOPStandardCapability.TryIsInRole(rbacConfiguration).Value);
			}

			// Token: 0x0400065B RID: 1627
			public static readonly RbacQuery.EOPStandardQueryProcessor EOPStandard = new RbacQuery.EOPStandardQueryProcessor();
		}

		// Token: 0x0200025C RID: 604
		private sealed class GallatinQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x06001530 RID: 5424 RVA: 0x0004E783 File Offset: 0x0004C983
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(DatacenterRegistry.IsForefrontForOffice() ? DatacenterRegistry.IsFFOGallatinDatacenter() : DatacenterRegistry.IsGallatinDatacenter());
			}

			// Token: 0x0400065C RID: 1628
			public static readonly RbacQuery.GallatinQueryProcessor IsGallatin = new RbacQuery.GallatinQueryProcessor();
		}

		// Token: 0x0200025D RID: 605
		private sealed class EndUserExperienceQueryProcessor : RbacQuery.RbacQueryProcessor
		{
			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x06001533 RID: 5427 RVA: 0x0004E7B1 File Offset: 0x0004C9B1
			public override bool CanCache
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001534 RID: 5428 RVA: 0x0004E7B4 File Offset: 0x0004C9B4
			public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
			{
				return new bool?(RbacQuery.EndUserExperienceQueryProcessor.IsEndUserExperience.Value);
			}

			// Token: 0x06001535 RID: 5429 RVA: 0x0004E7C8 File Offset: 0x0004C9C8
			private static bool GetEndUserExperienceSetting()
			{
				bool result;
				if (!bool.TryParse(ConfigurationManager.AppSettings["EndUserExperience"], out result))
				{
					result = false;
				}
				return result;
			}

			// Token: 0x0400065D RID: 1629
			private static readonly Lazy<bool> IsEndUserExperience = new Lazy<bool>(new Func<bool>(RbacQuery.EndUserExperienceQueryProcessor.GetEndUserExperienceSetting));
		}
	}
}
