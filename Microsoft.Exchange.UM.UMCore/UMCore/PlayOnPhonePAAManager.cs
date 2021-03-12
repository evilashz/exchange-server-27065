using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002A6 RID: 678
	internal class PlayOnPhonePAAManager : PAAManagerBase
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x0005926F File Offset: 0x0005746F
		internal PlayOnPhonePAAManager(ActivityManager manager, PlayOnPhonePAAManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00059279 File Offset: 0x00057479
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x0005928B File Offset: 0x0005748B
		internal ITempWavFile Recording
		{
			get
			{
				return (ITempWavFile)this.ReadVariable("recording");
			}
			set
			{
				base.WriteVariable("recording", value);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00059299 File Offset: 0x00057499
		internal bool ValidPAA
		{
			get
			{
				return this.validPAA;
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x000592A1 File Offset: 0x000574A1
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager::Start()", new object[0]);
			base.Subscriber = vo.CurrentCallContext.CallerInfo;
			base.Start(vo, refInfo);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000592D4 File Offset: 0x000574D4
		internal string GetAutoAttendant(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager::GetAutoAttendant()", new object[0]);
			PlayOnPhonePAAGreetingRequest playOnPhonePAAGreetingRequest = (PlayOnPhonePAAGreetingRequest)vo.CurrentCallContext.WebServiceRequest;
			Guid identity = playOnPhonePAAGreetingRequest.Identity;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager::GetAutoAttendant() identity = {0}", new object[]
			{
				identity.ToString()
			});
			using (IPAAStore ipaastore = PAAStore.Create(base.Subscriber))
			{
				PersonalAutoAttendant autoAttendant = ipaastore.GetAutoAttendant(identity, PAAValidationMode.None);
				if (autoAttendant == null)
				{
					CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager::GetAutoAttendant() did not find a PAA with identity = {0}", new object[]
					{
						identity.ToString()
					});
					this.validPAA = false;
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager::GetAutoAttendant() Found a PAA with identity = {0}", new object[]
					{
						identity.ToString()
					});
					this.validPAA = true;
				}
				base.PersonalAutoAttendant = autoAttendant;
			}
			return null;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000593E0 File Offset: 0x000575E0
		internal string ClearRecording(BaseUMCallSession vo)
		{
			base.RecordContext.Reset();
			this.Recording = null;
			return null;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000593F8 File Offset: 0x000575F8
		internal string SaveGreeting(BaseUMCallSession vo)
		{
			ITempWavFile recording = this.Recording;
			using (IPAAStore ipaastore = PAAStore.Create(base.Subscriber))
			{
				using (GreetingBase greetingBase = ipaastore.OpenGreeting(base.PersonalAutoAttendant))
				{
					greetingBase.Put(recording.FilePath);
				}
			}
			return null;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00059468 File Offset: 0x00057668
		internal string DeleteGreeting(BaseUMCallSession vo)
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.Subscriber))
			{
				using (GreetingBase greetingBase = ipaastore.OpenGreeting(base.PersonalAutoAttendant))
				{
					greetingBase.Delete();
				}
				base.PersonalGreeting = null;
				base.HaveGreeting = false;
			}
			return null;
		}

		// Token: 0x04000CAE RID: 3246
		private bool validPAA;

		// Token: 0x020002A7 RID: 679
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06001498 RID: 5272 RVA: 0x000594D8 File Offset: 0x000576D8
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06001499 RID: 5273 RVA: 0x000594E1 File Offset: 0x000576E1
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PlayOnPhonePAAManager.ConfigClass::CreateActivityManager()", new object[0]);
				return new PlayOnPhonePAAManager(manager, this);
			}
		}
	}
}
