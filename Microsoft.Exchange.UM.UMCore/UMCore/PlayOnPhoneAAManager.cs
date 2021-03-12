using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001AC RID: 428
	internal class PlayOnPhoneAAManager : ActivityManager
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x000368F4 File Offset: 0x00034AF4
		internal PlayOnPhoneAAManager(ActivityManager manager, PlayOnPhoneAAManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00036905 File Offset: 0x00034B05
		internal bool FileExists
		{
			get
			{
				return this.specifiedFileExists;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003690D File Offset: 0x00034B0D
		internal string ExistingFilePath
		{
			get
			{
				return this.existingFileFullPath;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00036915 File Offset: 0x00034B15
		internal bool PlayingExistingGreetingFirstTime
		{
			get
			{
				return this.playingExistingGreetingFirstTime;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003691D File Offset: 0x00034B1D
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00036920 File Offset: 0x00034B20
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneAAManager : Start() ", new object[0]);
			this.aa = vo.CurrentCallContext.AutoAttendantInfo;
			PlayOnPhoneAAGreetingRequest playOnPhoneAAGreetingRequest = vo.CurrentCallContext.WebServiceRequest as PlayOnPhoneAAGreetingRequest;
			this.filename = playOnPhoneAAGreetingRequest.FileName;
			this.configCache = vo.CurrentCallContext.UMConfigCache;
			this.userRecordingTheGreeting = playOnPhoneAAGreetingRequest.UserRecordingTheGreeting;
			this.CheckIfFileExists();
			vo.CurrentCallContext.WebServiceRequest.EndResult = UMOperationResult.InProgress;
			base.Start(vo, refInfo);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000369AD File Offset: 0x00034BAD
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs callSessionEventArgs)
		{
			if (!this.greetingSaved)
			{
				this.SetOperationResultFailed(vo);
			}
			base.OnUserHangup(vo, callSessionEventArgs);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000369C7 File Offset: 0x00034BC7
		internal string ExistingGreetingAlreadyPlayed(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneAAManager : ExistingGreetingAlreadyPlayed() ", new object[0]);
			this.playingExistingGreetingFirstTime = false;
			return null;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000369E8 File Offset: 0x00034BE8
		internal string SaveGreeting(BaseUMCallSession vo)
		{
			string filePath = base.RecordContext.Recording.FilePath;
			IPublishingSession publishingSession = null;
			try
			{
				publishingSession = PublishingPoint.GetPublishingSession(this.userRecordingTheGreeting, this.aa);
				publishingSession.Upload(filePath, this.filename);
			}
			catch (PublishingException)
			{
				return "failedToSaveGreeting";
			}
			finally
			{
				if (publishingSession != null)
				{
					publishingSession.Dispose();
				}
			}
			this.configCache.SetPrompt<UMAutoAttendant>(this.aa, filePath, this.filename);
			this.greetingSaved = true;
			return null;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00036A7C File Offset: 0x00034C7C
		internal string SetOperationResultFailed(BaseUMCallSession vo)
		{
			vo.CurrentCallContext.WebServiceRequest.EndResult = UMOperationResult.Failure;
			return null;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00036A90 File Offset: 0x00034C90
		internal override void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			if (reason != DropCallReason.GracefulHangup)
			{
				this.SetOperationResultFailed(vo);
			}
			base.DropCall(vo, reason);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00036AA6 File Offset: 0x00034CA6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PlayOnPhoneAAManager>(this);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00036AB0 File Offset: 0x00034CB0
		private void CheckIfFileExists()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneAAManager : CheckIfFileExists() ", new object[0]);
			try
			{
				this.existingFileFullPath = this.configCache.CheckIfFileExists<UMAutoAttendant>(this.aa, this.filename);
			}
			catch (FileNotFoundException)
			{
			}
			catch (IOException)
			{
			}
			this.specifiedFileExists = (this.existingFileFullPath != null);
		}

		// Token: 0x04000A39 RID: 2617
		private UMAutoAttendant aa;

		// Token: 0x04000A3A RID: 2618
		private UMConfigCache configCache;

		// Token: 0x04000A3B RID: 2619
		private string filename;

		// Token: 0x04000A3C RID: 2620
		private string userRecordingTheGreeting;

		// Token: 0x04000A3D RID: 2621
		private string existingFileFullPath;

		// Token: 0x04000A3E RID: 2622
		private bool specifiedFileExists;

		// Token: 0x04000A3F RID: 2623
		private bool playingExistingGreetingFirstTime = true;

		// Token: 0x04000A40 RID: 2624
		private bool greetingSaved;

		// Token: 0x020001AD RID: 429
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000C9F RID: 3231 RVA: 0x00036B28 File Offset: 0x00034D28
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x00036B31 File Offset: 0x00034D31
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing PlayOnPhoneAAManager activity manager.", new object[0]);
				return new PlayOnPhoneAAManager(manager, this);
			}
		}
	}
}
