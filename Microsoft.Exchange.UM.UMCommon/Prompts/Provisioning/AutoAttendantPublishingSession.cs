using System;
using System.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x02000019 RID: 25
	internal class AutoAttendantPublishingSession : PublishingSessionBase
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000077E0 File Offset: 0x000059E0
		internal AutoAttendantPublishingSession(string userName, UMAutoAttendant autoAttendant) : base(userName, autoAttendant)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(autoAttendant.OrganizationId);
			this.dialPlan = iadsystemConfigurationLookup.GetDialPlanFromId(autoAttendant.UMDialPlan);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007813 File Offset: 0x00005A13
		protected override UMDialPlan DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000781B File Offset: 0x00005A1B
		private UMAutoAttendant AutoAttendant
		{
			get
			{
				return (UMAutoAttendant)base.ConfigurationObject;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007828 File Offset: 0x00005A28
		public override void Upload(string source, string destinationName)
		{
			try
			{
				base.Upload(source, destinationName);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantCustomPromptUploadSucceeded, null, new object[]
				{
					base.UserName,
					this.AutoAttendant.Id,
					destinationName
				});
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PromptProvisioningTracer, this, "AutoAttendantPublishingSession.Upload: {0}.", new object[]
				{
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantCustomPromptUploadFailed, null, new object[]
				{
					base.UserName,
					this.AutoAttendant.Id,
					ex.Message
				});
				throw;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000078DC File Offset: 0x00005ADC
		protected override void AddConfiguredFiles(IDictionary fileList)
		{
			PublishingSessionBase.AddIfNotEmpty(fileList, this.AutoAttendant.InfoAnnouncementFilename);
			PublishingSessionBase.AddIfNotEmpty(fileList, this.AutoAttendant.AfterHoursWelcomeGreetingFilename);
			PublishingSessionBase.AddIfNotEmpty(fileList, this.AutoAttendant.AfterHoursMainMenuCustomPromptFilename);
			if (this.AutoAttendant.AfterHoursKeyMapping != null)
			{
				foreach (CustomMenuKeyMapping customMenuKeyMapping in this.AutoAttendant.AfterHoursKeyMapping)
				{
					PublishingSessionBase.AddIfNotEmpty(fileList, customMenuKeyMapping.PromptFileName);
				}
			}
			PublishingSessionBase.AddIfNotEmpty(fileList, this.AutoAttendant.BusinessHoursWelcomeGreetingFilename);
			PublishingSessionBase.AddIfNotEmpty(fileList, this.AutoAttendant.BusinessHoursMainMenuCustomPromptFilename);
			if (this.AutoAttendant.BusinessHoursKeyMapping != null)
			{
				foreach (CustomMenuKeyMapping customMenuKeyMapping2 in this.AutoAttendant.BusinessHoursKeyMapping)
				{
					PublishingSessionBase.AddIfNotEmpty(fileList, customMenuKeyMapping2.PromptFileName);
				}
			}
			if (this.AutoAttendant.HolidaySchedule != null)
			{
				foreach (HolidaySchedule holidaySchedule in this.AutoAttendant.HolidaySchedule)
				{
					PublishingSessionBase.AddIfNotEmpty(fileList, holidaySchedule.Greeting);
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007A50 File Offset: 0x00005C50
		protected override void UpdatePromptChangeKey(Guid guid)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.AutoAttendant.OrganizationId, false);
			UMAutoAttendant autoAttendantFromId = iadsystemConfigurationLookup.GetAutoAttendantFromId(this.AutoAttendant.Id);
			autoAttendantFromId.PromptChangeKey = guid;
			autoAttendantFromId.Session.Save(autoAttendantFromId);
		}

		// Token: 0x0400008C RID: 140
		private UMDialPlan dialPlan;
	}
}
