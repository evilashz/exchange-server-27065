using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001AF RID: 431
	internal class PlayOnPhoneManager : ActivityManager
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x00036B7A File Offset: 0x00034D7A
		internal PlayOnPhoneManager(ActivityManager manager, PlayOnPhoneManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00036B84 File Offset: 0x00034D84
		internal bool ProtectedMessage
		{
			get
			{
				return this.protectedMessage;
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00036B8C File Offset: 0x00034D8C
		internal override void CheckAuthorization(UMSubscriber u)
		{
			if (!this.isAutoAttendantCall)
			{
				base.CheckAuthorization(u);
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00036B9D File Offset: 0x00034D9D
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			vo.CurrentCallContext.WebServiceRequest.EndResult = UMOperationResult.InProgress;
			this.isAutoAttendantCall = (vo.CurrentCallContext.WebServiceRequest is PlayOnPhoneAAGreetingRequest);
			base.Start(vo, refInfo);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00036BD1 File Offset: 0x00034DD1
		internal override void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			if (reason != DropCallReason.GracefulHangup)
			{
				this.SetOperationResultFailed(vo);
			}
			base.DropCall(vo, reason);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00036BE7 File Offset: 0x00034DE7
		internal string SetOperationResultFailed(BaseUMCallSession vo)
		{
			vo.CurrentCallContext.WebServiceRequest.EndResult = UMOperationResult.Failure;
			return null;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00036BFC File Offset: 0x00034DFC
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager asked to do action {0}.", new object[]
			{
				action
			});
			string input = null;
			CallContext currentCallContext = vo.CurrentCallContext;
			if (string.Equals(action, "getPlayOnPhoneType", StringComparison.OrdinalIgnoreCase))
			{
				if (currentCallContext.WebServiceRequest is PlayOnPhoneAAGreetingRequest)
				{
					input = "playOnPhoneAAGreeting";
				}
				else
				{
					if (currentCallContext.WebServiceRequest is PlayOnPhoneMessageRequest)
					{
						PlayOnPhoneMessageRequest request = (PlayOnPhoneMessageRequest)currentCallContext.WebServiceRequest;
						try
						{
							this.PrepareMessagePlayer(request);
							this.CheckIfProtectedMessage(vo);
							if (currentCallContext.OCFeature.FeatureType == OCFeatureType.SingleVoicemail)
							{
								this.MarkMessageAsRead(vo);
							}
							input = "playOnPhoneVoicemail";
							goto IL_353;
						}
						catch (ArgumentException ex)
						{
							CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager::ExecuteAction(getPlayOnPhoneType)", new object[]
							{
								ex.ToString()
							});
							input = "xsoError";
							goto IL_353;
						}
						catch (CorruptDataException ex2)
						{
							CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager::ExecuteAction(getPlayOnPhoneType)", new object[]
							{
								ex2.ToString()
							});
							input = "xsoError";
							goto IL_353;
						}
					}
					if (currentCallContext.WebServiceRequest is PlayOnPhoneGreetingRequest)
					{
						PlayOnPhoneGreetingRequest playOnPhoneGreetingRequest = (PlayOnPhoneGreetingRequest)currentCallContext.WebServiceRequest;
						base.WriteVariable("greetingType", playOnPhoneGreetingRequest.GreetingType.ToString());
						base.WriteVariable("OofCustom", UMGreetingType.OofCustom.ToString());
						base.WriteVariable("NormalCustom", UMGreetingType.NormalCustom.ToString());
						using (GreetingBase greetingBase = (playOnPhoneGreetingRequest.GreetingType == UMGreetingType.NormalCustom) ? currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail) : currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away))
						{
							this.FetchGreeting(greetingBase);
						}
						input = "playOnPhoneGreeting";
					}
				}
			}
			else
			{
				if (string.Equals(action, "getExternal", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase2 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail))
					{
						this.FetchGreeting(greetingBase2);
						goto IL_353;
					}
				}
				if (string.Equals(action, "saveExternal", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase3 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail))
					{
						this.SaveGreeting(greetingBase3);
						goto IL_353;
					}
				}
				if (string.Equals(action, "deleteExternal", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase4 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail))
					{
						greetingBase4.Delete();
						goto IL_353;
					}
				}
				if (string.Equals(action, "getOof", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase5 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away))
					{
						this.FetchGreeting(greetingBase5);
						goto IL_353;
					}
				}
				if (string.Equals(action, "saveOof", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase6 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away))
					{
						this.SaveGreeting(greetingBase6);
						goto IL_353;
					}
				}
				if (string.Equals(action, "deleteOof", StringComparison.OrdinalIgnoreCase))
				{
					using (GreetingBase greetingBase7 = currentCallContext.CallerInfo.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away))
					{
						greetingBase7.Delete();
						goto IL_353;
					}
				}
				if (!string.Equals(action, "resetCallType", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				vo.CurrentCallContext.CallType = 1;
				vo.CurrentCallContext.OCFeature.FeatureType = OCFeatureType.None;
				this.GlobalManager.WriteVariable("ocFeature", null);
			}
			IL_353:
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00036FD8 File Offset: 0x000351D8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PlayOnPhoneManager>(this);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00036FE0 File Offset: 0x000351E0
		private void PrepareMessagePlayer(PlayOnPhoneMessageRequest request)
		{
			byte[] entryId = Convert.FromBase64String(request.ObjectId);
			StoreObjectId id = StoreObjectId.FromProviderSpecificId(entryId);
			base.MessagePlayerContext.Reset(id);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003700C File Offset: 0x0003520C
		private void CheckIfProtectedMessage(BaseUMCallSession vo)
		{
			try
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager attempting to CheckIfProtectedMessage {0}", new object[]
				{
					base.MessagePlayerContext.Id
				});
				UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = callerInfo.CreateSessionLock())
				{
					using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, base.MessagePlayerContext.Id))
					{
						this.protectedMessage = messageItem.IsRestricted;
					}
				}
			}
			catch (ObjectNotFoundException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager.CheckIfProtectedMessage: {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000370DC File Offset: 0x000352DC
		private void MarkMessageAsRead(BaseUMCallSession vo)
		{
			try
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager attempting to mark msg {0} as read.", new object[]
				{
					base.MessagePlayerContext.Id
				});
				UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = callerInfo.CreateSessionLock())
				{
					using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, base.MessagePlayerContext.Id))
					{
						messageItem.OpenAsReadWrite();
						messageItem.Load(new PropertyDefinition[]
						{
							MessageItemSchema.IsRead
						});
						if (!messageItem.IsRead)
						{
							messageItem.IsRead = true;
							messageItem.Save(SaveMode.ResolveConflicts);
						}
					}
				}
			}
			catch (ObjectNotFoundException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.StateMachineTracer, this, "PlayOnPhoneManager.MarkMessageAsRead: {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000371D8 File Offset: 0x000353D8
		private bool FetchGreeting(GreetingBase g)
		{
			base.WriteVariable("greeting", null);
			base.RecordContext.Reset();
			ITempWavFile tempWavFile = g.Get();
			if (tempWavFile == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "No greeting found for {0}.  Returning false.", new object[]
				{
					g.GetType()
				});
				return false;
			}
			base.WriteVariable("greeting", tempWavFile);
			return true;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00037238 File Offset: 0x00035438
		private void SaveGreeting(GreetingBase g)
		{
			ITempWavFile recording = base.RecordContext.Recording;
			base.RecordContext.Reset();
			if (recording != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Saving greeting at file={0} to store.", new object[]
				{
					recording.FilePath
				});
				g.Put(recording.FilePath);
			}
		}

		// Token: 0x04000A41 RID: 2625
		private bool protectedMessage;

		// Token: 0x04000A42 RID: 2626
		private bool isAutoAttendantCall;

		// Token: 0x020001B0 RID: 432
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000CB1 RID: 3249 RVA: 0x0003728C File Offset: 0x0003548C
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00037295 File Offset: 0x00035495
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing PlayOnPhone activity manager.", new object[0]);
				return new PlayOnPhoneManager(manager, this);
			}
		}
	}
}
