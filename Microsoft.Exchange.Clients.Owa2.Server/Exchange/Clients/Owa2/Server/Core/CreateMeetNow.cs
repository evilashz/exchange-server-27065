using System;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.OnlineMeetings;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002FD RID: 765
	internal class CreateMeetNow : CreateMeetingBase
	{
		// Token: 0x060019B4 RID: 6580 RVA: 0x0005B310 File Offset: 0x00059510
		public CreateMeetNow(CallContext callContext, string sipUri, string subject, bool isPrivate) : base(callContext, sipUri, isPrivate)
		{
			WcfServiceCommandBase.ThrowIfNull("callContext", "sipUri", "CreateMeetNow");
			if (string.IsNullOrEmpty(sipUri))
			{
				throw new OwaInvalidRequestException("No sipUri specified");
			}
			this.subject = subject;
			OwsLogRegistry.Register(CreateMeetNow.CreateMeetNowActionName, typeof(CreateOnlineMeetingMetadata), new Type[0]);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0005B370 File Offset: 0x00059570
		protected override OnlineMeetingType ProcessOnlineMeetingResult(UserContext userContext, OnlineMeetingResult result)
		{
			return new OnlineMeetingType
			{
				WebUrl = result.OnlineMeeting.WebUrl
			};
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0005B398 File Offset: 0x00059598
		protected override void SetDefaultValuesForOptics()
		{
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.IsTaskCompleted, bool.FalseString);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.IsUcwaSupported, bool.TrueString);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ItemId, this.subject);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0005B3FA File Offset: 0x000595FA
		protected override bool ShoudlMeetingBeCreated()
		{
			return true;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0005B400 File Offset: 0x00059600
		protected override OnlineMeetingSettings ConstructOnlineMeetingSettings()
		{
			OnlineMeetingSettings onlineMeetingSettings = new OnlineMeetingSettings();
			if (this.subject != null)
			{
				onlineMeetingSettings.Subject = this.subject;
			}
			return onlineMeetingSettings;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0005B428 File Offset: 0x00059628
		protected override void UpdateOpticsLog(OnlineMeetingResult createMeeting)
		{
			OnlineMeeting onlineMeeting = createMeeting.OnlineMeeting;
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.UserGuid, ExtensibleLogger.FormatPIIValue(this.sipUri));
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ConferenceId, onlineMeeting.PstnMeetingId);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.LeaderCount, onlineMeeting.Leaders.Count<string>());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AttendeeCount, onlineMeeting.Attendees.Count<string>());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.ExpiryTime, onlineMeeting.ExpiryTime);
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AutomaticLeaderAssignment, onlineMeeting.AutomaticLeaderAssignment.ToString());
			base.CallContext.ProtocolLog.Set(CreateOnlineMeetingMetadata.AccessLevel, onlineMeeting.Accesslevel.ToString());
		}

		// Token: 0x04000E32 RID: 3634
		private static readonly string CreateMeetNowActionName = typeof(CreateMeetNow).Name;

		// Token: 0x04000E33 RID: 3635
		private readonly string subject;
	}
}
