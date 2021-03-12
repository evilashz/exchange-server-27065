using System;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200005F RID: 95
	internal class CallerNameOrNumberPrompt : VariablePrompt<NameOrNumberOfCaller>
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00014630 File Offset: 0x00012830
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"callerNameOrNumber",
				base.Config.PromptName,
				string.Empty,
				base.SbLog.ToString()
			});
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014680 File Offset: 0x00012880
		internal override string ToSsml()
		{
			return base.SbSsml.ToString();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014690 File Offset: 0x00012890
		protected override void InternalInitialize()
		{
			switch (base.InitVal.TypeOfCall)
			{
			case NameOrNumberOfCaller.TypeOfVoiceCall.MissedCall:
				this.AddMissedCallPrompts();
				return;
			case NameOrNumberOfCaller.TypeOfVoiceCall.VoicemailCall:
				this.AddVoicemailCallPrompts();
				return;
			default:
				throw new InvalidArgumentException("TypeOfCall");
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000146D4 File Offset: 0x000128D4
		private void AddMissedCallPrompts()
		{
			if (!this.IsNullEmailSender() && base.InitVal.CallerId != null)
			{
				Prompt[] paramPrompt = new Prompt[]
				{
					new SpokenNamePrompt("emailSender", base.Culture, base.InitVal.EmailSender),
					new TelephoneNumberPrompt("senderCallerID", base.Culture, base.InitVal.CallerId)
				};
				base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiEmailStatusTypeMissedCallNameAndNumber", base.Culture, paramPrompt));
				return;
			}
			if (this.IsNullEmailSender() && base.InitVal.CallerId != null)
			{
				TelephoneNumberPrompt telephoneNumberPrompt = new TelephoneNumberPrompt("senderCallerID", base.Culture, base.InitVal.CallerId);
				base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiEmailStatusTypeMissedCallNumber", base.Culture, new Prompt[]
				{
					telephoneNumberPrompt
				}));
				return;
			}
			if (this.IsNullEmailSender() && base.InitVal.CallerId == null)
			{
				base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiEmailStatusTypeMissedCallUnknown", base.Culture, null));
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000147D4 File Offset: 0x000129D4
		private void AddVoicemailCallPrompts()
		{
			if (base.InitVal.CallerId != null)
			{
				Prompt[] paramPrompt = new Prompt[]
				{
					new TimePrompt("messageReceivedTime", base.Culture, base.InitVal.MessageReceivedTime),
					new TelephoneNumberPrompt("senderCallerID", base.Culture, base.InitVal.CallerId)
				};
				base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiVoiceMessageEnvelope", base.Culture, paramPrompt));
				return;
			}
			if (!string.IsNullOrEmpty(base.InitVal.CallerName))
			{
				Prompt[] paramPrompt2 = new Prompt[]
				{
					new TimePrompt("messageReceivedTime", base.Culture, base.InitVal.MessageReceivedTime),
					new TextPrompt("senderInfo", base.Culture, base.InitVal.CallerName)
				};
				base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiVoiceMessageEnvelope", base.Culture, paramPrompt2));
				return;
			}
			TimePrompt timePrompt = new TimePrompt("messageReceivedTime", base.Culture, base.InitVal.MessageReceivedTime);
			base.AddPrompt(PromptUtils.CreateSingleStatementPrompt("tuiMessageReceived", base.Culture, new Prompt[]
			{
				timePrompt
			}));
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000148F4 File Offset: 0x00012AF4
		private bool IsNullEmailSender()
		{
			if (base.InitVal.EmailSender is string)
			{
				return string.IsNullOrEmpty((string)base.InitVal.EmailSender);
			}
			return base.InitVal.EmailSender == null;
		}

		// Token: 0x04000183 RID: 387
		private const string EmailStatusMissedCallNameAndNumberPrompt = "tuiEmailStatusTypeMissedCallNameAndNumber";

		// Token: 0x04000184 RID: 388
		private const string EmailStatusTypeMissedCallNumberPrompt = "tuiEmailStatusTypeMissedCallNumber";

		// Token: 0x04000185 RID: 389
		private const string EmailStatusTypeMissedCallUnknownPrompt = "tuiEmailStatusTypeMissedCallUnknown";

		// Token: 0x04000186 RID: 390
		private const string VoiceMessageEnvelopePrompt = "tuiVoiceMessageEnvelope";

		// Token: 0x04000187 RID: 391
		private const string VoiceMessageReceivedPrompt = "tuiMessageReceived";
	}
}
