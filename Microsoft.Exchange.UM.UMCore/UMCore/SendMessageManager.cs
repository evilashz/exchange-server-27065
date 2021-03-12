using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000049 RID: 73
	internal class SendMessageManager : ActivityManager
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		internal SendMessageManager(ActivityManager manager, ActivityManagerConfig config) : base(manager, config)
		{
			this.Recipients = new Stack<Participant>();
			base.WriteVariable("numRecipients", this.Recipients.Count);
			base.IsSentImportant = false;
			base.MessageMarkedPrivate = false;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000D420 File Offset: 0x0000B620
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000D428 File Offset: 0x0000B628
		protected Stack<Participant> Recipients
		{
			get
			{
				return this.recipients;
			}
			set
			{
				this.recipients = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000D431 File Offset: 0x0000B631
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000D439 File Offset: 0x0000B639
		protected IRecordedMessage SendMsg
		{
			get
			{
				return this.sendMsg;
			}
			set
			{
				this.sendMsg = value;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D444 File Offset: 0x0000B644
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SendMessageManager asked to do action: {0}.", new object[]
			{
				action
			});
			string input = null;
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			if (string.Equals(action, "addRecipientBySearch", StringComparison.OrdinalIgnoreCase))
			{
				input = this.AddRecipientBySearch(vo);
			}
			else if (string.Equals(action, "removeRecipient", StringComparison.OrdinalIgnoreCase))
			{
				input = this.RemoveRecipient();
			}
			else if (string.Equals(action, "cancelMessage", StringComparison.OrdinalIgnoreCase))
			{
				this.CleanupMessage();
			}
			else if (string.Equals(action, "sendMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.SendMessage(vo);
			}
			else
			{
				if (!string.Equals(action, "sendMessageUrgent", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				base.IsSentImportant = true;
				input = this.SendMessage(vo);
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D50C File Offset: 0x0000B70C
		internal string TogglePrivacy(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "TogglePrivacy", new object[0]);
			base.MessageMarkedPrivate = !base.MessageMarkedPrivate;
			return null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D534 File Offset: 0x0000B734
		internal string ToggleImportance(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "ToggleImportance", new object[0]);
			base.IsSentImportant = !base.IsSentImportant;
			return null;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D55C File Offset: 0x0000B75C
		internal string ClearSelection(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "ClearSelection", new object[0]);
			base.IsSentImportant = false;
			base.MessageMarkedPrivate = false;
			return null;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D583 File Offset: 0x0000B783
		internal string SendMessagePrivate(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "SendMessagePrivate", new object[0]);
			base.MessageMarkedPrivate = true;
			return this.SendMessage(vo);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D5A9 File Offset: 0x0000B7A9
		internal string SendMessagePrivateAndUrgent(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "SendMessagePrivateAndUrgent", new object[0]);
			base.MessageMarkedPrivate = true;
			base.IsSentImportant = true;
			return this.SendMessage(vo);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		protected virtual string SendMessage(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SendMessageMananger::SendMessage. markPrivate={0}, imp={1}", new object[]
			{
				base.MessageMarkedPrivate,
				base.IsSentImportant
			});
			this.SendMsg.DoSubmit(base.IsSentImportant ? Importance.High : Importance.Normal, base.MessageMarkedPrivate, this.Recipients);
			base.MessageHasBeenSentWithHighImportance = base.IsSentImportant;
			this.CleanupMessage();
			return null;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D64F File Offset: 0x0000B84F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SendMessageManager>(this);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D658 File Offset: 0x0000B858
		protected void CleanupMessage()
		{
			base.RecordContext.Reset();
			this.Recipients.Clear();
			base.WriteVariable("numRecipients", this.Recipients.Count);
			this.SendMsg = null;
			this.ClearSelection(null);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		private string AddRecipientBySearch(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SendMessageManager::AddRecipientBySearch.", new object[0]);
			string result = null;
			ContactSearchItem contactSearchItem = (ContactSearchItem)this.ReadVariable("directorySearchResult");
			Participant emailParticipant = contactSearchItem.EmailParticipant;
			if (null == emailParticipant)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SendMessageManager::AddRecipientBySearch did not find a valid recipient.", new object[0]);
				result = "unknownRecipient";
			}
			else
			{
				this.Recipients.Push(emailParticipant);
				if (contactSearchItem.Recipient != null)
				{
					base.SetRecordedName("userName", contactSearchItem.Recipient);
				}
				else
				{
					base.SetTextPartVariable("userName", contactSearchItem.FullName ?? contactSearchItem.PrimarySmtpAddress);
				}
			}
			base.WriteVariable("numRecipients", this.Recipients.Count);
			return result;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D76C File Offset: 0x0000B96C
		private string RemoveRecipient()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "SendMessageMananger::RemoveRecipient.", new object[0]);
			string result = null;
			if (this.Recipients.Count != 0)
			{
				Participant participant = this.Recipients.Pop();
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, participant.EmailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "Removed recipient p=_EmailAddress.", new object[0]);
			}
			if (this.Recipients.Count == 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Removed the final recipient.  Returning noRecipients event.", new object[0]);
				result = "noRecipients";
			}
			base.WriteVariable("numRecipients", this.Recipients.Count);
			return result;
		}

		// Token: 0x040000F3 RID: 243
		private Stack<Participant> recipients;

		// Token: 0x040000F4 RID: 244
		private IRecordedMessage sendMsg;
	}
}
