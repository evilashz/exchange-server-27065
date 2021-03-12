using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F4 RID: 1524
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class UMAutoAttendant : ADConfigurationObject
	{
		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x0010B160 File Offset: 0x00109360
		internal override ADObjectSchema Schema
		{
			get
			{
				return UMAutoAttendant.schema;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x0010B167 File Offset: 0x00109367
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UMAutoAttendant.mostDerivedClass;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x0010B16E File Offset: 0x0010936E
		internal override ADObjectId ParentPath
		{
			get
			{
				return UMAutoAttendant.parentPath;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x0010B175 File Offset: 0x00109375
		private AutoAttendantSettings BusinessHourSettings
		{
			get
			{
				if (string.IsNullOrEmpty(this.BusinessHourFeatures))
				{
					this.businessHourSettings = new AutoAttendantSettings();
				}
				else
				{
					this.businessHourSettings = AutoAttendantSettings.FromXml(this.BusinessHourFeatures);
				}
				this.businessHourSettings.Parent = this;
				return this.businessHourSettings;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x0010B1B4 File Offset: 0x001093B4
		private AutoAttendantSettings AfterHourSettings
		{
			get
			{
				if (string.IsNullOrEmpty(this.AfterHourFeatures))
				{
					this.afterHourSettings = new AutoAttendantSettings();
				}
				else
				{
					this.afterHourSettings = AutoAttendantSettings.FromXml(this.AfterHourFeatures);
				}
				this.afterHourSettings.Parent = this;
				return this.afterHourSettings;
			}
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x0010B1F3 File Offset: 0x001093F3
		private void FlushBusinessHourSettings()
		{
			if (this.businessHourSettings != null)
			{
				this.BusinessHourFeatures = AutoAttendantSettings.ToXml(this.businessHourSettings);
			}
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0010B20E File Offset: 0x0010940E
		private void FlushAfterHourSettings()
		{
			if (this.afterHourSettings != null)
			{
				this.AfterHourFeatures = AutoAttendantSettings.ToXml(this.afterHourSettings);
			}
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0010B229 File Offset: 0x00109429
		internal void SetStatus(StatusEnum status)
		{
			this.Enabled = (status == StatusEnum.Enabled);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0010B235 File Offset: 0x00109435
		internal void SetDialPlan(ADObjectId dialPlanId)
		{
			this[UMAutoAttendantSchema.UMDialPlan] = dialPlanId;
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x0010B243 File Offset: 0x00109443
		// (set) Token: 0x0600483A RID: 18490 RVA: 0x0010B255 File Offset: 0x00109455
		internal string DefaultMailboxLegacyDN
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.DefaultMailboxLegacyDN];
			}
			set
			{
				this[UMAutoAttendantSchema.DefaultMailboxLegacyDN] = value;
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x0010B263 File Offset: 0x00109463
		// (set) Token: 0x0600483C RID: 18492 RVA: 0x0010B275 File Offset: 0x00109475
		private string AfterHourFeatures
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.AfterHourFeatures];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHourFeatures] = value;
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x0010B283 File Offset: 0x00109483
		// (set) Token: 0x0600483E RID: 18494 RVA: 0x0010B295 File Offset: 0x00109495
		private string BusinessHourFeatures
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.BusinessHourFeatures];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHourFeatures] = value;
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x0010B2A3 File Offset: 0x001094A3
		// (set) Token: 0x06004840 RID: 18496 RVA: 0x0010B2B5 File Offset: 0x001094B5
		[Parameter(Mandatory = false)]
		public bool SpeechEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AutomaticSpeechRecognitionEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.AutomaticSpeechRecognitionEnabled] = value;
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x0010B2C8 File Offset: 0x001094C8
		// (set) Token: 0x06004842 RID: 18498 RVA: 0x0010B2DA File Offset: 0x001094DA
		[Parameter(Mandatory = false)]
		public bool AllowDialPlanSubscribers
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AllowDialPlanSubscribers];
			}
			set
			{
				this[UMAutoAttendantSchema.AllowDialPlanSubscribers] = value;
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x0010B2ED File Offset: 0x001094ED
		// (set) Token: 0x06004844 RID: 18500 RVA: 0x0010B2FF File Offset: 0x001094FF
		[Parameter(Mandatory = false)]
		public bool AllowExtensions
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AllowExtensions];
			}
			set
			{
				this[UMAutoAttendantSchema.AllowExtensions] = value;
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x0010B312 File Offset: 0x00109512
		// (set) Token: 0x06004846 RID: 18502 RVA: 0x0010B324 File Offset: 0x00109524
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInCountryOrRegionGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMAutoAttendantSchema.AllowedInCountryOrRegionGroups];
			}
			set
			{
				this[UMAutoAttendantSchema.AllowedInCountryOrRegionGroups] = value;
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x0010B332 File Offset: 0x00109532
		// (set) Token: 0x06004848 RID: 18504 RVA: 0x0010B344 File Offset: 0x00109544
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInternationalGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMAutoAttendantSchema.AllowedInternationalGroups];
			}
			set
			{
				this[UMAutoAttendantSchema.AllowedInternationalGroups] = value;
			}
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x0010B352 File Offset: 0x00109552
		// (set) Token: 0x0600484A RID: 18506 RVA: 0x0010B364 File Offset: 0x00109564
		[Parameter(Mandatory = false)]
		public bool CallSomeoneEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.CallSomeoneEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.CallSomeoneEnabled] = value;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x0600484B RID: 18507 RVA: 0x0010B377 File Offset: 0x00109577
		// (set) Token: 0x0600484C RID: 18508 RVA: 0x0010B389 File Offset: 0x00109589
		[Parameter(Mandatory = false)]
		public DialScopeEnum ContactScope
		{
			get
			{
				return (DialScopeEnum)this[UMAutoAttendantSchema.ContactScope];
			}
			set
			{
				this[UMAutoAttendantSchema.ContactScope] = value;
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x0010B39C File Offset: 0x0010959C
		// (set) Token: 0x0600484E RID: 18510 RVA: 0x0010B3AE File Offset: 0x001095AE
		public ADObjectId ContactAddressList
		{
			get
			{
				return (ADObjectId)this[UMAutoAttendantSchema.ContactAddressList];
			}
			set
			{
				this[UMAutoAttendantSchema.ContactAddressList] = value;
			}
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x0600484F RID: 18511 RVA: 0x0010B3BC File Offset: 0x001095BC
		// (set) Token: 0x06004850 RID: 18512 RVA: 0x0010B3CE File Offset: 0x001095CE
		[Parameter(Mandatory = false)]
		public bool SendVoiceMsgEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.SendVoiceMsgEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.SendVoiceMsgEnabled] = value;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x0010B3E1 File Offset: 0x001095E1
		// (set) Token: 0x06004852 RID: 18514 RVA: 0x0010B3F3 File Offset: 0x001095F3
		[Parameter(Mandatory = false)]
		public ScheduleInterval[] BusinessHoursSchedule
		{
			get
			{
				return (ScheduleInterval[])this[UMAutoAttendantSchema.BusinessHoursSchedule];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursSchedule] = value;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x0010B401 File Offset: 0x00109601
		// (set) Token: 0x06004854 RID: 18516 RVA: 0x0010B413 File Offset: 0x00109613
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> PilotIdentifierList
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMAutoAttendantSchema.PilotIdentifierList];
			}
			set
			{
				this[UMAutoAttendantSchema.PilotIdentifierList] = value;
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x0010B421 File Offset: 0x00109621
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[UMAutoAttendantSchema.UMDialPlan];
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x0010B433 File Offset: 0x00109633
		// (set) Token: 0x06004857 RID: 18519 RVA: 0x0010B445 File Offset: 0x00109645
		public ADObjectId DTMFFallbackAutoAttendant
		{
			get
			{
				return (ADObjectId)this[UMAutoAttendantSchema.DTMFFallbackAutoAttendant];
			}
			set
			{
				this[UMAutoAttendantSchema.DTMFFallbackAutoAttendant] = value;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x0010B453 File Offset: 0x00109653
		// (set) Token: 0x06004859 RID: 18521 RVA: 0x0010B465 File Offset: 0x00109665
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<HolidaySchedule> HolidaySchedule
		{
			get
			{
				return (MultiValuedProperty<HolidaySchedule>)this[UMAutoAttendantSchema.HolidaySchedule];
			}
			set
			{
				this[UMAutoAttendantSchema.HolidaySchedule] = value;
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x0010B473 File Offset: 0x00109673
		// (set) Token: 0x0600485B RID: 18523 RVA: 0x0010B485 File Offset: 0x00109685
		public string TimeZone
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.TimeZone];
			}
			set
			{
				this[UMAutoAttendantSchema.TimeZone] = value;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x0010B493 File Offset: 0x00109693
		// (set) Token: 0x0600485D RID: 18525 RVA: 0x0010B4A5 File Offset: 0x001096A5
		public UMTimeZone TimeZoneName
		{
			get
			{
				return (UMTimeZone)this[UMAutoAttendantSchema.TimeZoneName];
			}
			set
			{
				this[UMAutoAttendantSchema.TimeZoneName] = value;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x0010B4B3 File Offset: 0x001096B3
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x0010B4C5 File Offset: 0x001096C5
		[Parameter(Mandatory = false)]
		public AutoAttendantDisambiguationFieldEnum MatchedNameSelectionMethod
		{
			get
			{
				return (AutoAttendantDisambiguationFieldEnum)this[UMAutoAttendantSchema.MatchedNameSelectionMethod];
			}
			set
			{
				this[UMAutoAttendantSchema.MatchedNameSelectionMethod] = value;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x0010B4D8 File Offset: 0x001096D8
		// (set) Token: 0x06004861 RID: 18529 RVA: 0x0010B4EA File Offset: 0x001096EA
		internal Guid PromptChangeKey
		{
			get
			{
				return (Guid)this[UMAutoAttendantSchema.PromptChangeKey];
			}
			set
			{
				this[UMAutoAttendantSchema.PromptChangeKey] = value;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x0010B4FD File Offset: 0x001096FD
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x0010B50F File Offset: 0x0010970F
		[Parameter(Mandatory = false)]
		public string BusinessLocation
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.BusinessLocation];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessLocation] = value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x0010B51D File Offset: 0x0010971D
		// (set) Token: 0x06004865 RID: 18533 RVA: 0x0010B52F File Offset: 0x0010972F
		[Parameter(Mandatory = false)]
		public DayOfWeek WeekStartDay
		{
			get
			{
				return (DayOfWeek)this[UMAutoAttendantSchema.WeekStartDay];
			}
			set
			{
				this[UMAutoAttendantSchema.WeekStartDay] = value;
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x0010B542 File Offset: 0x00109742
		// (set) Token: 0x06004867 RID: 18535 RVA: 0x0010B554 File Offset: 0x00109754
		private bool Enabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.Enabled];
			}
			set
			{
				this[UMAutoAttendantSchema.Enabled] = value;
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x0010B567 File Offset: 0x00109767
		public StatusEnum Status
		{
			get
			{
				if (!this.Enabled)
				{
					return StatusEnum.Disabled;
				}
				return StatusEnum.Enabled;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x0010B574 File Offset: 0x00109774
		// (set) Token: 0x0600486A RID: 18538 RVA: 0x0010B586 File Offset: 0x00109786
		[Parameter(Mandatory = false)]
		public UMLanguage Language
		{
			get
			{
				return (UMLanguage)this[UMAutoAttendantSchema.Language];
			}
			set
			{
				this[UMAutoAttendantSchema.Language] = value;
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x0010B594 File Offset: 0x00109794
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x0010B5A6 File Offset: 0x001097A6
		[Parameter(Mandatory = false)]
		public string OperatorExtension
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.OperatorExtension];
			}
			set
			{
				this[UMAutoAttendantSchema.OperatorExtension] = value;
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x0010B5B4 File Offset: 0x001097B4
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x0010B5C6 File Offset: 0x001097C6
		[Parameter(Mandatory = false)]
		public string InfoAnnouncementFilename
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.InfoAnnouncementFilename];
			}
			set
			{
				this[UMAutoAttendantSchema.InfoAnnouncementFilename] = value;
			}
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x0010B5D4 File Offset: 0x001097D4
		// (set) Token: 0x06004870 RID: 18544 RVA: 0x0010B5E6 File Offset: 0x001097E6
		[Parameter(Mandatory = false)]
		public InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
		{
			get
			{
				return (InfoAnnouncementEnabledEnum)this[UMAutoAttendantSchema.InfoAnnouncementEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.InfoAnnouncementEnabled] = value;
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x0010B5F9 File Offset: 0x001097F9
		// (set) Token: 0x06004872 RID: 18546 RVA: 0x0010B60B File Offset: 0x0010980B
		[Parameter(Mandatory = false)]
		public bool NameLookupEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.NameLookupEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.NameLookupEnabled] = value;
			}
		}

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x0010B61E File Offset: 0x0010981E
		// (set) Token: 0x06004874 RID: 18548 RVA: 0x0010B630 File Offset: 0x00109830
		[Parameter(Mandatory = false)]
		public bool StarOutToDialPlanEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.StarOutToDialPlanEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.StarOutToDialPlanEnabled] = value;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x0010B643 File Offset: 0x00109843
		// (set) Token: 0x06004876 RID: 18550 RVA: 0x0010B655 File Offset: 0x00109855
		[Parameter(Mandatory = false)]
		public bool ForwardCallsToDefaultMailbox
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.ForwardCallsToDefaultMailbox];
			}
			set
			{
				this[UMAutoAttendantSchema.ForwardCallsToDefaultMailbox] = value;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x0010B668 File Offset: 0x00109868
		// (set) Token: 0x06004878 RID: 18552 RVA: 0x0010B670 File Offset: 0x00109870
		public ADUser DefaultMailbox
		{
			get
			{
				return this.defaultMailbox;
			}
			internal set
			{
				this.defaultMailbox = value;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x0010B679 File Offset: 0x00109879
		// (set) Token: 0x0600487A RID: 18554 RVA: 0x0010B68B File Offset: 0x0010988B
		[Parameter(Mandatory = false)]
		public string BusinessName
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.BusinessName];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessName] = value;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x0010B699 File Offset: 0x00109899
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x0010B6AB File Offset: 0x001098AB
		[Parameter(Mandatory = false)]
		public string BusinessHoursWelcomeGreetingFilename
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.BusinessHoursWelcomeGreetingFilename];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursWelcomeGreetingFilename] = value;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x0010B6B9 File Offset: 0x001098B9
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x0010B6CB File Offset: 0x001098CB
		[Parameter(Mandatory = false)]
		public bool BusinessHoursWelcomeGreetingEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.BusinessHoursWelcomeGreetingEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursWelcomeGreetingEnabled] = value;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x0010B6DE File Offset: 0x001098DE
		// (set) Token: 0x06004880 RID: 18560 RVA: 0x0010B6F0 File Offset: 0x001098F0
		[Parameter(Mandatory = false)]
		public string BusinessHoursMainMenuCustomPromptFilename
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.BusinessHoursMainMenuCustomPromptFilename];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursMainMenuCustomPromptFilename] = value;
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x0010B6FE File Offset: 0x001098FE
		// (set) Token: 0x06004882 RID: 18562 RVA: 0x0010B710 File Offset: 0x00109910
		[Parameter(Mandatory = false)]
		public bool BusinessHoursMainMenuCustomPromptEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.BusinessHoursMainMenuCustomPromptEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursMainMenuCustomPromptEnabled] = value;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x0010B723 File Offset: 0x00109923
		// (set) Token: 0x06004884 RID: 18564 RVA: 0x0010B735 File Offset: 0x00109935
		[Parameter(Mandatory = false)]
		public bool BusinessHoursTransferToOperatorEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.BusinessHoursTransferToOperatorEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursTransferToOperatorEnabled] = value;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x0010B748 File Offset: 0x00109948
		// (set) Token: 0x06004886 RID: 18566 RVA: 0x0010B75A File Offset: 0x0010995A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CustomMenuKeyMapping> BusinessHoursKeyMapping
		{
			get
			{
				return (MultiValuedProperty<CustomMenuKeyMapping>)this[UMAutoAttendantSchema.BusinessHoursKeyMapping];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursKeyMapping] = value;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x0010B768 File Offset: 0x00109968
		// (set) Token: 0x06004888 RID: 18568 RVA: 0x0010B77A File Offset: 0x0010997A
		[Parameter(Mandatory = false)]
		public bool BusinessHoursKeyMappingEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.BusinessHoursKeyMappingEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.BusinessHoursKeyMappingEnabled] = value;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x0010B78D File Offset: 0x0010998D
		// (set) Token: 0x0600488A RID: 18570 RVA: 0x0010B79F File Offset: 0x0010999F
		[Parameter(Mandatory = false)]
		public string AfterHoursWelcomeGreetingFilename
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.AfterHoursWelcomeGreetingFilename];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursWelcomeGreetingFilename] = value;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x0010B7AD File Offset: 0x001099AD
		// (set) Token: 0x0600488C RID: 18572 RVA: 0x0010B7BF File Offset: 0x001099BF
		[Parameter(Mandatory = false)]
		public bool AfterHoursWelcomeGreetingEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AfterHoursWelcomeGreetingEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursWelcomeGreetingEnabled] = value;
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x0010B7D2 File Offset: 0x001099D2
		// (set) Token: 0x0600488E RID: 18574 RVA: 0x0010B7E4 File Offset: 0x001099E4
		[Parameter(Mandatory = false)]
		public string AfterHoursMainMenuCustomPromptFilename
		{
			get
			{
				return (string)this[UMAutoAttendantSchema.AfterHoursMainMenuCustomPromptFilename];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursMainMenuCustomPromptFilename] = value;
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x0010B7F2 File Offset: 0x001099F2
		// (set) Token: 0x06004890 RID: 18576 RVA: 0x0010B804 File Offset: 0x00109A04
		[Parameter(Mandatory = false)]
		public bool AfterHoursMainMenuCustomPromptEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AfterHoursMainMenuCustomPromptEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursMainMenuCustomPromptEnabled] = value;
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x06004891 RID: 18577 RVA: 0x0010B817 File Offset: 0x00109A17
		// (set) Token: 0x06004892 RID: 18578 RVA: 0x0010B829 File Offset: 0x00109A29
		[Parameter(Mandatory = false)]
		public bool AfterHoursTransferToOperatorEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AfterHoursTransferToOperatorEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursTransferToOperatorEnabled] = value;
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x0010B83C File Offset: 0x00109A3C
		// (set) Token: 0x06004894 RID: 18580 RVA: 0x0010B84E File Offset: 0x00109A4E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CustomMenuKeyMapping> AfterHoursKeyMapping
		{
			get
			{
				return (MultiValuedProperty<CustomMenuKeyMapping>)this[UMAutoAttendantSchema.AfterHoursKeyMapping];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursKeyMapping] = value;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x0010B85C File Offset: 0x00109A5C
		// (set) Token: 0x06004896 RID: 18582 RVA: 0x0010B86E File Offset: 0x00109A6E
		[Parameter(Mandatory = false)]
		public bool AfterHoursKeyMappingEnabled
		{
			get
			{
				return (bool)this[UMAutoAttendantSchema.AfterHoursKeyMappingEnabled];
			}
			set
			{
				this[UMAutoAttendantSchema.AfterHoursKeyMappingEnabled] = value;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x0010B881 File Offset: 0x00109A81
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x0010B888 File Offset: 0x00109A88
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0010B88C File Offset: 0x00109A8C
		internal UMDialPlan GetDialPlan()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(this.UMDialPlan);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 2111, "GetDialPlan", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\umautoattendantconfig.cs");
			return tenantOrTopologyConfigurationSession.Read<UMDialPlan>(this.UMDialPlan);
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0010B8D4 File Offset: 0x00109AD4
		internal static UMAutoAttendant FindAutoAttendantByPilotIdentifierAndDialPlan(string pilotIdentifier, ADObjectId dialPlanId)
		{
			if (pilotIdentifier == null)
			{
				throw new ArgumentNullException("pilotIdentifier");
			}
			if (dialPlanId == null)
			{
				throw new ArgumentNullException("dialPlanId");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(dialPlanId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 2144, "FindAutoAttendantByPilotIdentifierAndDialPlan", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\umautoattendantconfig.cs");
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, UMAutoAttendantSchema.PilotIdentifierList, pilotIdentifier),
				new ComparisonFilter(ComparisonOperator.Equal, UMAutoAttendantSchema.UMDialPlan, dialPlanId)
			});
			UMAutoAttendant[] array = tenantOrTopologyConfigurationSession.Find<UMAutoAttendant>(null, QueryScope.SubTree, filter, null, 0);
			switch (array.Length)
			{
			case 0:
				return null;
			case 1:
				return array[0];
			default:
				throw new NonUniquePilotIdentifierException(pilotIdentifier, dialPlanId.ToString());
			}
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0010B988 File Offset: 0x00109B88
		internal AutoAttendantSettings GetCurrentSettings(out HolidaySchedule holidaySettings, ref bool isBusinessHour)
		{
			ExTimeZone exTimeZone = null;
			string timeZone = this.TimeZone;
			if (string.IsNullOrEmpty(timeZone))
			{
				ExTraceGlobals.UMAutoAttendantTracer.TraceDebug<string>((long)this.GetHashCode(), "AA [Name = \"{0}\"] TZ Id = empty string, defaulting to using Current machine timezone", base.Name);
				exTimeZone = ExTimeZone.CurrentTimeZone;
			}
			else if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.TimeZone, out exTimeZone))
			{
				throw new InvalidOperationException(DirectoryStrings.InvalidTimeZoneId(this.TimeZone));
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			ExDateTime exDateTime = exTimeZone.ConvertDateTime(utcNow);
			ExTraceGlobals.UMAutoAttendantTracer.TraceDebug((long)this.GetHashCode(), "AA [Name = \"{0}\"] UTC = {1} LOCAL = {2} TZ Id = {3}", new object[]
			{
				base.Name,
				utcNow.ToString("R"),
				exDateTime.ToString("R"),
				this.TimeZone
			});
			AutoAttendantSettings autoAttendantSettings = null;
			HolidaySchedule holidaySchedule = null;
			MultiValuedProperty<HolidaySchedule> holidaySchedule2 = this.HolidaySchedule;
			if (holidaySchedule2 != null && holidaySchedule2.Count > 0)
			{
				using (MultiValuedProperty<HolidaySchedule>.Enumerator enumerator = holidaySchedule2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HolidaySchedule holidaySchedule3 = enumerator.Current;
						if ((ExDateTime)holidaySchedule3.StartDate.Date <= exDateTime.Date && (ExDateTime)holidaySchedule3.EndDate.Date >= exDateTime.Date)
						{
							ExTraceGlobals.UMAutoAttendantTracer.TraceDebug((long)this.GetHashCode(), "AA: {0} Call Time: {1} Matched with holiday: {2} {3}-{4}", new object[]
							{
								base.Name,
								exDateTime.ToString("R"),
								holidaySchedule3.Name,
								holidaySchedule3.StartDate.ToString("R"),
								holidaySchedule3.EndDate.ToString("R")
							});
							autoAttendantSettings = this.AfterHourSettings;
							isBusinessHour = false;
							if (holidaySchedule == null)
							{
								holidaySchedule = holidaySchedule3;
							}
							else if (holidaySchedule3.StartDate.Date > holidaySchedule.StartDate.Date)
							{
								holidaySchedule = holidaySchedule3;
							}
							else if (holidaySchedule3.StartDate.Date == holidaySchedule.StartDate.Date)
							{
								int num = string.Compare(holidaySchedule.Name, holidaySchedule3.Name, StringComparison.OrdinalIgnoreCase);
								holidaySchedule = ((num > 0) ? holidaySchedule3 : holidaySchedule);
							}
						}
					}
					goto IL_290;
				}
			}
			ExTraceGlobals.UMAutoAttendantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "AA: {0} Call Time: {1} No holiday schedule found", base.Name, exDateTime.ToString("R"));
			IL_290:
			holidaySettings = holidaySchedule;
			if (autoAttendantSettings != null)
			{
				return autoAttendantSettings;
			}
			autoAttendantSettings = this.AfterHourSettings;
			isBusinessHour = false;
			foreach (ScheduleInterval scheduleInterval in this.BusinessHoursSchedule)
			{
				ExTraceGlobals.UMAutoAttendantTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "AA: {0} Call Time: {1} BusinessHour: {2}", base.Name, exDateTime.ToString("R"), scheduleInterval.ToString());
				if (scheduleInterval.Contains(new WeekDayAndTime((DateTime)exDateTime)))
				{
					ExTraceGlobals.UMAutoAttendantTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "AA: {0} Call Time: {1} Matched with BusinessHour: {2}", base.Name, exDateTime.ToString("R"), scheduleInterval.ToString());
					isBusinessHour = true;
					autoAttendantSettings = this.BusinessHourSettings;
					break;
				}
			}
			if (!isBusinessHour)
			{
				ExTraceGlobals.UMAutoAttendantTracer.TraceDebug<string, string>((long)this.GetHashCode(), "AA: {0} Call Time: {1} Returning AfterHour settings", base.Name, exDateTime.ToString("R"));
			}
			return autoAttendantSettings;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0010BD44 File Offset: 0x00109F44
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.IsModified(ADObjectSchema.Name) && base.ObjectState != ObjectState.New)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.AACantChangeName, base.Id, string.Empty));
			}
			LocalizedString localizedString = this.ValidateSettings(this.BusinessHourSettings, AASettingsEnum.BusinessHourSettings, this.OperatorExtension);
			if (localizedString != LocalizedString.Empty)
			{
				errors.Add(new PropertyValidationError(localizedString, UMAutoAttendantSchema.BusinessHoursKeyMapping, this));
			}
			localizedString = this.ValidateSettings(this.AfterHourSettings, AASettingsEnum.AfterHourSettings, this.OperatorExtension);
			if (localizedString != LocalizedString.Empty)
			{
				errors.Add(new PropertyValidationError(localizedString, UMAutoAttendantSchema.AfterHoursKeyMapping, this));
			}
			if (this.InfoAnnouncementEnabled != InfoAnnouncementEnabledEnum.False && string.IsNullOrEmpty(this.InfoAnnouncementFilename))
			{
				localizedString = DirectoryStrings.InvalidAutoAttendantSetting("InfoAnnouncementEnabled", "InfoAnnouncementFilename");
				errors.Add(new ObjectValidationError(localizedString, base.Id, string.Empty));
			}
			if (!string.IsNullOrEmpty(this.InfoAnnouncementFilename) && !this.VerifyValidCustomGreetingFile(this.InfoAnnouncementFilename))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCustomGreetingFilename("InfoAnnouncementFilename"), base.Id, string.Empty));
			}
			if (this.HolidaySchedule.Count > 0)
			{
				HolidaySchedule[] array = this.HolidaySchedule.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					HolidaySchedule holidaySchedule = array[i];
					if (!string.IsNullOrEmpty(holidaySchedule.Greeting) && !this.VerifyValidCustomGreetingFile(holidaySchedule.Greeting))
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCustomGreetingFilename("HolidaySchedule"), base.Id, string.Empty));
					}
					for (int j = i + 1; j < array.Length; j++)
					{
						if (i != j)
						{
							HolidaySchedule holidaySchedule2 = array[j];
							if (string.Compare(holidaySchedule.Name, holidaySchedule2.Name, StringComparison.OrdinalIgnoreCase) == 0)
							{
								errors.Add(new ObjectValidationError(DirectoryStrings.DuplicateHolidaysError(holidaySchedule.Name), base.Id, string.Empty));
								break;
							}
						}
					}
				}
			}
			if ((this.CallSomeoneEnabled || this.SendVoiceMsgEnabled) && this.ContactScope == DialScopeEnum.AddressList && this.ContactAddressList == null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCallSomeoneScopeSettings("AddressList", "ContactAddressList"), base.Id, string.Empty));
			}
			if (base.IsModified(UMAutoAttendantSchema.DefaultMailboxLegacyDN))
			{
				ADUser aduser = this.DefaultMailbox;
				if (aduser != null && (!aduser.UMEnabled || aduser.UMRecipientDialPlanId == null || !aduser.UMRecipientDialPlanId.Equals(this.UMDialPlan) || string.IsNullOrEmpty(this.DefaultMailboxLegacyDN)))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.InvalidDefaultMailbox, base.Id, string.Empty));
				}
			}
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0010BFDC File Offset: 0x0010A1DC
		private bool VerifyValidCustomGreetingFile(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				return true;
			}
			string extension = Path.GetExtension(file);
			return string.Compare(extension, ".wav", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".wma", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0010C01C File Offset: 0x0010A21C
		private LocalizedString ValidateSettings(AutoAttendantSettings aa, AASettingsEnum settings, string operatorExtension)
		{
			string text = (settings == AASettingsEnum.BusinessHourSettings) ? "BusinessHours" : "AfterHours";
			if (aa.WelcomeGreetingEnabled && string.IsNullOrEmpty(aa.WelcomeGreetingFilename))
			{
				return DirectoryStrings.InvalidAutoAttendantSetting(text + "WelcomeGreetingEnabled", text + "WelcomeGreetingFilename");
			}
			if (!string.IsNullOrEmpty(aa.WelcomeGreetingFilename) && !this.VerifyValidCustomGreetingFile(aa.WelcomeGreetingFilename))
			{
				return DirectoryStrings.InvalidCustomGreetingFilename(text + "WelcomeGreetingFilename");
			}
			if (aa.MainMenuCustomPromptEnabled && string.IsNullOrEmpty(aa.MainMenuCustomPromptFilename))
			{
				return DirectoryStrings.InvalidAutoAttendantSetting(text + "MainMenuCustomPromptEnabled", text + "MainMenuCustomPromptFilename");
			}
			if (!string.IsNullOrEmpty(aa.MainMenuCustomPromptFilename) && !this.VerifyValidCustomGreetingFile(aa.MainMenuCustomPromptFilename))
			{
				return DirectoryStrings.InvalidCustomGreetingFilename(text + "MainMenuCustomPromptFilename");
			}
			if (aa.KeyMappingEnabled && (aa.KeyMapping == null || aa.KeyMapping.Length == 0))
			{
				return DirectoryStrings.InvalidAutoAttendantSetting(text + "KeyMappingEnabled", text + "KeyMapping");
			}
			if (aa.KeyMapping != null && aa.KeyMapping.Length > 10)
			{
				return DirectoryStrings.TooManyKeyMappings(text);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x04003208 RID: 12808
		private static UMAutoAttendantSchema schema = ObjectSchema.GetInstance<UMAutoAttendantSchema>();

		// Token: 0x04003209 RID: 12809
		private static string mostDerivedClass = "msExchUMAutoAttendant";

		// Token: 0x0400320A RID: 12810
		private static ADObjectId parentPath = new ADObjectId("CN=UM AutoAttendant Container");

		// Token: 0x0400320B RID: 12811
		private AutoAttendantSettings businessHourSettings;

		// Token: 0x0400320C RID: 12812
		private AutoAttendantSettings afterHourSettings;

		// Token: 0x0400320D RID: 12813
		private ADUser defaultMailbox;
	}
}
