using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.AssistantsClientResources;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000109 RID: 265
	internal abstract class SmsNotification
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00047167 File Offset: 0x00045367
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0004716F File Offset: 0x0004536F
		protected string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.remainingChar = this.maxUsableCharacters - value.Length;
				this.body = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0004718B File Offset: 0x0004538B
		protected int RemainingChar
		{
			get
			{
				return this.remainingChar;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00047193 File Offset: 0x00045393
		protected string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0004719B File Offset: 0x0004539B
		protected string CallerId
		{
			get
			{
				return this.callerId;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000471A3 File Offset: 0x000453A3
		protected StoreObject Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000471AC File Offset: 0x000453AC
		internal SmsNotification(MailboxSession session, CultureInfo preferredCulture, StoreObject item, UMDialPlan dialPlan)
		{
			this.preferredCulture = (preferredCulture ?? ((session == null) ? CultureInfo.InvariantCulture : session.PreferedCulture));
			this.item = item;
			this.maxUsableCharacters = 150;
			this.maxUsableCharacters -= Math.Max(session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString().Length, (session.MailboxOwner.MailboxInfo.DisplayName ?? string.Empty).Length);
			if (this.maxUsableCharacters < 0)
			{
				this.maxUsableCharacters = 0;
			}
			this.remainingChar = this.maxUsableCharacters;
			this.name = this.ReadStoreProperty<string>(MessageItemSchema.VoiceMessageSenderName, string.Empty);
			this.callerId = this.ReadStoreProperty<string>(MessageItemSchema.PstnCallbackTelephoneNumber, string.Empty);
			if (string.IsNullOrEmpty(this.callerId))
			{
				this.callerId = this.ReadStoreProperty<string>(MessageItemSchema.SenderTelephoneNumber, string.Empty);
			}
			if (string.IsNullOrEmpty(this.callerId))
			{
				this.callerId = Strings.SMSEmptyCallerId.ToString(this.GetMailboxCulture());
				return;
			}
			PhoneNumber phoneNumber;
			if (PhoneNumber.TryParse(dialPlan, this.callerId, out phoneNumber))
			{
				this.callerId = phoneNumber.ToDisplay;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000472EE File Offset: 0x000454EE
		internal void PrepareSmsMessage(MessageItem newMessage)
		{
			this.BuildSmsMessage();
			this.WriteSmsToMessageItem(newMessage);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00047300 File Offset: 0x00045500
		protected virtual void BuildSmsMessage()
		{
			string text = string.IsNullOrEmpty(this.name) ? this.BaseMessageWithCallerId : this.FullBaseMessage;
			this.subject = text;
			if (this.maxUsableCharacters < this.MinimumMaxUsableCharacters)
			{
				this.Body = text;
				return;
			}
			if (!this.CanAddString(text) && !string.IsNullOrEmpty(this.name))
			{
				text = this.BaseMessageWithName;
			}
			if (!this.CanAddString(text))
			{
				text = text.Substring(0, this.RemainingChar);
			}
			this.Body = text;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AEA RID: 2794
		protected abstract string FullBaseMessage { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AEB RID: 2795
		protected abstract string BaseMessageWithName { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AEC RID: 2796
		protected abstract string BaseMessageWithCallerId { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AED RID: 2797
		protected abstract int MinimumMaxUsableCharacters { get; }

		// Token: 0x06000AEE RID: 2798 RVA: 0x00047384 File Offset: 0x00045584
		protected T ReadStoreProperty<T>(StorePropertyDefinition property, T defaultValue)
		{
			object obj = this.item.TryGetProperty(property);
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000473B1 File Offset: 0x000455B1
		protected CultureInfo GetMailboxCulture()
		{
			return this.preferredCulture;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000473B9 File Offset: 0x000455B9
		private bool CanAddString(string addString)
		{
			return this.RemainingChar - addString.Length >= 0;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000473D0 File Offset: 0x000455D0
		private void WriteSmsToMessageItem(MessageItem newMessage)
		{
			newMessage.Subject = this.subject;
			using (TextWriter textWriter = newMessage.Body.OpenTextWriter(BodyFormat.TextPlain))
			{
				textWriter.Write(this.Body);
			}
		}

		// Token: 0x040006F1 RID: 1777
		private readonly int maxUsableCharacters;

		// Token: 0x040006F2 RID: 1778
		private string body;

		// Token: 0x040006F3 RID: 1779
		private string subject;

		// Token: 0x040006F4 RID: 1780
		private int remainingChar;

		// Token: 0x040006F5 RID: 1781
		private string name;

		// Token: 0x040006F6 RID: 1782
		private string callerId;

		// Token: 0x040006F7 RID: 1783
		private StoreObject item;

		// Token: 0x040006F8 RID: 1784
		private CultureInfo preferredCulture;
	}
}
