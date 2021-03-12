using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x02000064 RID: 100
	internal class DialPlanPublishingSession : PublishingSessionBase
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000DC94 File Offset: 0x0000BE94
		internal DialPlanPublishingSession(string userName, UMDialPlan dialPlan) : base(userName, dialPlan)
		{
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		protected override UMDialPlan DialPlan
		{
			get
			{
				return (UMDialPlan)base.ConfigurationObject;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public override void Upload(string source, string destinationName)
		{
			try
			{
				base.Upload(source, destinationName);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DialPlanCustomPromptUploadSucceeded, null, new object[]
				{
					base.UserName,
					this.DialPlan.Id,
					destinationName
				});
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PromptProvisioningTracer, this, "DialPlanPublishingSession.Upload: {0}.", new object[]
				{
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DialPlanCustomPromptUploadFailed, null, new object[]
				{
					base.UserName,
					this.DialPlan.Id,
					ex.Message
				});
				throw;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000DD60 File Offset: 0x0000BF60
		protected override void AddConfiguredFiles(IDictionary fileList)
		{
			PublishingSessionBase.AddIfNotEmpty(fileList, this.DialPlan.WelcomeGreetingFilename);
			PublishingSessionBase.AddIfNotEmpty(fileList, this.DialPlan.InfoAnnouncementFilename);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000DD84 File Offset: 0x0000BF84
		protected override void UpdatePromptChangeKey(Guid guid)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.DialPlan.OrganizationId, false);
			UMDialPlan dialPlanFromId = iadsystemConfigurationLookup.GetDialPlanFromId(this.DialPlan.Id);
			dialPlanFromId.PromptChangeKey = guid.ToString("N");
			dialPlanFromId.Session.Save(dialPlanFromId);
		}
	}
}
