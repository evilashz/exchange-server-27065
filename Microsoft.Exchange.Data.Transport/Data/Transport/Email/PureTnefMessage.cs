using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F7 RID: 247
	internal class PureTnefMessage : MessageImplementation, IBody, IMapiPropertyAccess, IRelayable
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x00017D0C File Offset: 0x00015F0C
		internal PureTnefMessage(MimeTnefMessage topMessage, MimePart tnefPart, DataStorage tnefStorage, long tnefStart, long tnefEnd)
		{
			tnefStorage.AddRef();
			this.tnefStorage = tnefStorage;
			this.tnefStart = tnefStart;
			this.tnefEnd = tnefEnd;
			this.topMessage = topMessage;
			this.properties = new TnefPropertyBag(this);
			this.accessToken = new PureTnefMessage.PureTnefMessageThreadAccessToken(this);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00017D74 File Offset: 0x00015F74
		internal PureTnefMessage(TnefAttachmentData parentAttachmentData, DataStorage tnefStorage, long tnefStart, long tnefEnd)
		{
			tnefStorage.AddRef();
			this.tnefStorage = tnefStorage;
			this.tnefStart = tnefStart;
			this.tnefEnd = tnefEnd;
			this.parentAttachmentData = parentAttachmentData;
			this.properties = new TnefPropertyBag(this);
			this.accessToken = new PureTnefMessage.PureTnefMessageThreadAccessToken(this);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00017DD8 File Offset: 0x00015FD8
		internal static IEnumerable<string> SystemMessageClassNames
		{
			get
			{
				return PureTnefMessage.systemMessageClassNames;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00017DDF File Offset: 0x00015FDF
		internal static IEnumerable<string> SystemMessageClassPrefixes
		{
			get
			{
				return PureTnefMessage.systemMessageClassPrefixes;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00017DE6 File Offset: 0x00015FE6
		internal static IEnumerable<string> InterpersonalMessageClassNames
		{
			get
			{
				return PureTnefMessage.interpersonalMessageClassNames;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00017DED File Offset: 0x00015FED
		internal static IEnumerable<string> InterpersonalMessageClassPrefixes
		{
			get
			{
				return PureTnefMessage.interpersonalMessageClassPrefixes;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00017DF4 File Offset: 0x00015FF4
		internal override ObjectThreadAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00017DFC File Offset: 0x00015FFC
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00017EA8 File Offset: 0x000160A8
		public override EmailRecipient From
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.FromRecipient == null && this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) != null)
				{
					this.FromRecipient = new EmailRecipient(new TnefRecipient(this, int.MinValue, (this.GetProperty(TnefPropertyTag.SentRepresentingNameA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingAddrtypeA) as string) ?? string.Empty));
				}
				return this.FromRecipient;
			}
			set
			{
				this.ThrowIfDisposed();
				this.FromRecipient = value;
				if (value == null)
				{
					this.SetProperty(TnefPropertyTag.SentRepresentingNameA, null);
					this.SetProperty(TnefPropertyTag.SentRepresentingEmailAddressA, null);
					this.SetProperty(TnefPropertyTag.SentRepresentingAddrtypeA, null);
				}
				else
				{
					this.SetProperty(TnefPropertyTag.SentRepresentingNameA, value.DisplayName);
					this.SetProperty(TnefPropertyTag.SentRepresentingEmailAddressA, value.SmtpAddress);
					this.SetProperty(TnefPropertyTag.SentRepresentingAddrtypeA, value.NativeAddressType);
				}
				this.SetProperty(TnefPropertyTag.SentRepresentingEntryId, null);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00017F2A File Offset: 0x0001612A
		public override EmailRecipientCollection To
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.ToRecipients == null)
				{
					this.ToRecipients = new EmailRecipientCollection(this, RecipientType.To);
				}
				return this.ToRecipients;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00017F4D File Offset: 0x0001614D
		public override EmailRecipientCollection Cc
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.CcRecipients == null)
				{
					this.CcRecipients = new EmailRecipientCollection(this, RecipientType.Cc);
				}
				return this.CcRecipients;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00017F70 File Offset: 0x00016170
		public override EmailRecipientCollection Bcc
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.BccRecipients == null)
				{
					this.BccRecipients = new EmailRecipientCollection(this, RecipientType.Bcc, true);
				}
				return this.BccRecipients;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00017F94 File Offset: 0x00016194
		public override EmailRecipientCollection ReplyTo
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.ReplyToRecipients == null)
				{
					this.ReplyToRecipients = new EmailRecipientCollection(this, RecipientType.ReplyTo, true);
				}
				return this.ReplyToRecipients;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00017FB8 File Offset: 0x000161B8
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001811C File Offset: 0x0001631C
		public override EmailRecipient DispositionNotificationTo
		{
			get
			{
				this.ThrowIfDisposed();
				if (!(bool)(this.GetProperty(TnefPropertyTag.ReadReceiptRequested) ?? false))
				{
					return null;
				}
				if (this.DntRecipient == null)
				{
					if (this.properties[TnefPropertyTag.ReadReceiptEmailAddressA] != null)
					{
						this.DntRecipient = new EmailRecipient(new TnefRecipient(this, int.MinValue, (this.GetProperty(TnefPropertyTag.ReadReceiptDisplayNameA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.ReadReceiptEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.ReadReceiptEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.ReadReceiptAddrtypeA) as string) ?? string.Empty));
					}
					else if (this.properties[TnefPropertyTag.SentRepresentingEmailAddressA] != null)
					{
						this.DntRecipient = new EmailRecipient(new TnefRecipient(this, int.MinValue, (this.GetProperty(TnefPropertyTag.SentRepresentingNameA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingAddrtypeA) as string) ?? string.Empty));
					}
				}
				return this.DntRecipient;
			}
			set
			{
				this.ThrowIfDisposed();
				this.DntRecipient = value;
				this.SetProperty(TnefPropertyTag.ReadReceiptRequested, value != null);
				if (value == null)
				{
					this.SetProperty(TnefPropertyTag.ReadReceiptDisplayNameA, null);
					this.SetProperty(TnefPropertyTag.ReadReceiptEmailAddressA, null);
					this.SetProperty(TnefPropertyTag.ReadReceiptAddrtypeA, null);
				}
				else
				{
					this.SetProperty(TnefPropertyTag.ReadReceiptDisplayNameA, value.DisplayName);
					this.SetProperty(TnefPropertyTag.ReadReceiptEmailAddressA, value.NativeAddress);
					this.SetProperty(TnefPropertyTag.ReadReceiptAddrtypeA, value.NativeAddressType);
				}
				this.SetProperty(TnefPropertyTag.ReadReceiptEntryId, null);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x000181B8 File Offset: 0x000163B8
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x000182FC File Offset: 0x000164FC
		public override EmailRecipient Sender
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.SenderRecipient == null)
				{
					if (this.properties[TnefPropertyTag.SenderEmailAddressA] != null)
					{
						this.SenderRecipient = new EmailRecipient(new TnefRecipient(this, int.MinValue, (this.GetProperty(TnefPropertyTag.SenderNameA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SenderEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SenderEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SenderAddrtypeA) as string) ?? string.Empty));
					}
					else if (this.properties[TnefPropertyTag.SentRepresentingEmailAddressA] != null)
					{
						this.SenderRecipient = new EmailRecipient(new TnefRecipient(this, int.MinValue, (this.GetProperty(TnefPropertyTag.SentRepresentingNameA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingEmailAddressA) as string) ?? string.Empty, (this.GetProperty(TnefPropertyTag.SentRepresentingAddrtypeA) as string) ?? string.Empty));
					}
				}
				return this.SenderRecipient;
			}
			set
			{
				this.ThrowIfDisposed();
				this.SenderRecipient = value;
				if (value == null)
				{
					this.SetProperty(TnefPropertyTag.SenderNameA, null);
					this.SetProperty(TnefPropertyTag.SenderEmailAddressA, null);
					this.SetProperty(TnefPropertyTag.SenderAddrtypeA, null);
				}
				else
				{
					this.SetProperty(TnefPropertyTag.SenderNameA, value.DisplayName);
					this.SetProperty(TnefPropertyTag.SenderEmailAddressA, value.SmtpAddress);
					this.SetProperty(TnefPropertyTag.SenderAddrtypeA, value.NativeAddressType);
				}
				this.SetProperty(TnefPropertyTag.SenderEntryId, null);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00018380 File Offset: 0x00016580
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x000183AF File Offset: 0x000165AF
		public override DateTime Date
		{
			get
			{
				DateTime? property = this.GetProperty<DateTime>(TnefPropertyTag.ClientSubmitTime);
				if (property == null)
				{
					return DateTime.MinValue;
				}
				return property.Value;
			}
			set
			{
				this.SetProperty(TnefPropertyTag.ClientSubmitTime, (value != DateTime.MinValue) ? value : null);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x000183D4 File Offset: 0x000165D4
		// (set) Token: 0x0600070C RID: 1804 RVA: 0x00018403 File Offset: 0x00016603
		public override DateTime Expires
		{
			get
			{
				DateTime? property = this.GetProperty<DateTime>(TnefPropertyTag.ExpiryTime);
				if (property == null)
				{
					return DateTime.MinValue;
				}
				return property.Value;
			}
			set
			{
				this.SetProperty(TnefPropertyTag.ExpiryTime, (value != DateTime.MinValue) ? value : null);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00018428 File Offset: 0x00016628
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00018457 File Offset: 0x00016657
		public override DateTime ReplyBy
		{
			get
			{
				DateTime? property = this.GetProperty<DateTime>(TnefPropertyTag.ReplyTime);
				if (property == null)
				{
					return DateTime.MinValue;
				}
				return property.Value;
			}
			set
			{
				this.SetProperty(TnefPropertyTag.ReplyTime, (value != DateTime.MinValue) ? value : null);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001847C File Offset: 0x0001667C
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00018575 File Offset: 0x00016775
		public override string Subject
		{
			get
			{
				string text = this.GetProperty(TnefPropertyTag.SubjectA) as string;
				string text2 = this.GetProperty(TnefPropertyTag.SubjectPrefixA) as string;
				string text3 = this.GetProperty(TnefPropertyTag.NormalizedSubjectA) as string;
				if (string.IsNullOrEmpty(text))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
					}
					if (!string.IsNullOrEmpty(text3))
					{
						text = (string.IsNullOrEmpty(text) ? text3 : (text + text3));
					}
					if (string.IsNullOrEmpty(text))
					{
						text = string.Empty;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(text2))
					{
						text2 = string.Empty;
					}
					if (string.IsNullOrEmpty(text3))
					{
						text3 = string.Empty;
					}
					int num = text2.Length + text3.Length;
					if (num > text.Length || (text2.Length > 0 && text3.Length > 0 && num < text.Length) || !text.StartsWith(text2) || !text.EndsWith(text3))
					{
						this.SetProperty(TnefPropertyTag.SubjectPrefixA, null);
						this.SetProperty(TnefPropertyTag.NormalizedSubjectA, null);
					}
				}
				return text;
			}
			set
			{
				this.SetProperty(TnefPropertyTag.SubjectA, value);
				this.SetProperty(TnefPropertyTag.SubjectPrefixA, null);
				this.SetProperty(TnefPropertyTag.NormalizedSubjectA, null);
				this.SetProperty(TnefPropertyTag.ConversationTopicA, null);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000185A8 File Offset: 0x000167A8
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x000185D5 File Offset: 0x000167D5
		public override string MessageId
		{
			get
			{
				string text = this.GetProperty(TnefPropertyTag.InternetMessageIdA) as string;
				if (string.IsNullOrEmpty(text))
				{
					text = string.Empty;
				}
				return text;
			}
			set
			{
				this.SetProperty(TnefPropertyTag.InternetMessageIdA, value);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000185E4 File Offset: 0x000167E4
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x000185FC File Offset: 0x000167FC
		public override Importance Importance
		{
			get
			{
				Importance result;
				this.TryGetImportance(out result);
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				int num = 1;
				if (Importance.Low == value)
				{
					num = 0;
				}
				else if (Importance.High == value)
				{
					num = 2;
				}
				else if (value != Importance.Normal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetProperty(TnefPropertyTag.Importance, (num != 1) ? num : null);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00018648 File Offset: 0x00016848
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00018660 File Offset: 0x00016860
		public override Priority Priority
		{
			get
			{
				Priority result;
				this.TryGetPriority(out result);
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				int num = 0;
				if (Priority.NonUrgent == value)
				{
					num = -1;
				}
				else if (Priority.Urgent == value)
				{
					num = 1;
				}
				else if (value != Priority.Normal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetProperty(TnefPropertyTag.Priority, (num != 0) ? num : null);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x000186AC File Offset: 0x000168AC
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x000186C4 File Offset: 0x000168C4
		public override Sensitivity Sensitivity
		{
			get
			{
				Sensitivity result;
				this.TryGetSensitivity(out result);
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				if (Sensitivity.CompanyConfidential < value || value < Sensitivity.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetProperty(TnefPropertyTag.Sensitivity, (value != Sensitivity.None) ? ((int)value) : null);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00018703 File Offset: 0x00016903
		public override string MapiMessageClass
		{
			get
			{
				this.ThrowIfDisposed();
				return (this.GetProperty(TnefPropertyTag.MessageClassA) as string) ?? "IPM.Note";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00018724 File Offset: 0x00016924
		public override MimeDocument MimeDocument
		{
			get
			{
				this.ThrowIfDisposed();
				return null;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001872D File Offset: 0x0001692D
		public override MimePart RootPart
		{
			get
			{
				this.ThrowIfDisposed();
				return null;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00018736 File Offset: 0x00016936
		public override MimePart TnefPart
		{
			get
			{
				this.ThrowIfDisposed();
				return null;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001873F File Offset: 0x0001693F
		public override bool IsInterpersonalMessage
		{
			get
			{
				return this.MatchMessageClass(PureTnefMessage.InterpersonalMessageClassPrefixes, PureTnefMessage.InterpersonalMessageClassNames) && !this.IsRightsProtectedMessage;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001875E File Offset: 0x0001695E
		public override bool IsSystemMessage
		{
			get
			{
				return this.MatchMessageClass(PureTnefMessage.SystemMessageClassPrefixes, PureTnefMessage.SystemMessageClassNames) || this.IsPublicFolderReplicationMessage || this.IsLinkMonitorMessage;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001878C File Offset: 0x0001698C
		public override bool IsPublicFolderReplicationMessage
		{
			get
			{
				string mapiMessageClass = this.MapiMessageClass;
				if (mapiMessageClass.Equals("IPM.Replication", StringComparison.OrdinalIgnoreCase))
				{
					string text = this.GetProperty(TnefPropertyTag.ContentIdentifierW) as string;
					if (!string.IsNullOrEmpty(text) && text.Equals("ExSysMessage", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				else if (mapiMessageClass.Equals("IPM.Conflict.Folder", StringComparison.OrdinalIgnoreCase) || mapiMessageClass.Equals("IPM.Conflict.Message", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				return false;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000187F8 File Offset: 0x000169F8
		private bool IsRightsProtectedMessage
		{
			get
			{
				if (1 != this.tnefAttachments.Count)
				{
					return false;
				}
				string text = this.properties[TnefPropertyBag.TnefNameIdContentClass] as string;
				if (string.IsNullOrEmpty(text))
				{
					return false;
				}
				if (!text.Equals("rpmsg.message", StringComparison.OrdinalIgnoreCase) && !text.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) && !text.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				TnefAttachmentData dataAtPublicIndex = this.tnefAttachments.GetDataAtPublicIndex(0);
				string text2;
				return Utility.TrySanitizeAttachmentFileName(Utility.GetRawFileName(dataAtPublicIndex.Properties, this.stnef), out text2) && text2.Equals("message.rpmsg", StringComparison.Ordinal);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001889C File Offset: 0x00016A9C
		public override bool IsOpaqueMessage
		{
			get
			{
				MessageSecurityType messageSecurityType = this.MessageSecurityType;
				return messageSecurityType == MessageSecurityType.Encrypted || messageSecurityType == MessageSecurityType.OpaqueSigned;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x000188BC File Offset: 0x00016ABC
		public override MessageSecurityType MessageSecurityType
		{
			get
			{
				string mapiMessageClass = this.MapiMessageClass;
				if (mapiMessageClass.Equals("IPM.Note.SMIME", StringComparison.OrdinalIgnoreCase))
				{
					string text = this.properties[TnefPropertyBag.TnefNameIdContentType] as string;
					if (string.IsNullOrEmpty(text))
					{
						return MessageSecurityType.Encrypted;
					}
					if (text.StartsWith("application/octet-stream", StringComparison.OrdinalIgnoreCase))
					{
						return MessageSecurityType.OpaqueSigned;
					}
					int num = text.IndexOf("smime-type", StringComparison.OrdinalIgnoreCase);
					if (-1 == num)
					{
						return MessageSecurityType.Encrypted;
					}
					if (text.Length < num + 1 || text[num] != '=')
					{
						return MessageSecurityType.Encrypted;
					}
					if (-1 != text.IndexOf("signed-data", num, StringComparison.OrdinalIgnoreCase))
					{
						return MessageSecurityType.OpaqueSigned;
					}
					return MessageSecurityType.Encrypted;
				}
				else
				{
					if (mapiMessageClass.Equals("IPM.Note.SMIME.MultipartSigned", StringComparison.OrdinalIgnoreCase))
					{
						return MessageSecurityType.ClearSigned;
					}
					if (mapiMessageClass.Equals("IPM.Note.Secure.Sign", StringComparison.OrdinalIgnoreCase))
					{
						return MessageSecurityType.ClearSigned;
					}
					if (mapiMessageClass.Equals("IPM.Note.Secure", StringComparison.OrdinalIgnoreCase))
					{
						return MessageSecurityType.Encrypted;
					}
					if (this.IsRightsProtectedMessage)
					{
						return MessageSecurityType.Encrypted;
					}
					return MessageSecurityType.None;
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00018988 File Offset: 0x00016B88
		internal override void Dispose(bool disposing)
		{
			if (disposing && this.tnefStorage != null)
			{
				foreach (TnefAttachmentData tnefAttachmentData in this.tnefAttachments.InternalList)
				{
					if (tnefAttachmentData != null)
					{
						this.DisposeAttachment(tnefAttachmentData);
					}
				}
				this.bodyData.Dispose();
				this.tnefStorage.Release();
				this.tnefStorage = null;
				this.properties.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018A20 File Offset: 0x00016C20
		public override void Normalize(bool allowUTF8 = false)
		{
			this.Normalize(NormalizeOptions.NormalizeTnef, allowUTF8);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00018A30 File Offset: 0x00016C30
		internal override void Normalize(NormalizeOptions normalizeOptions, bool allowUTF8)
		{
			if (this.topMessage == null)
			{
				return;
			}
			if ((normalizeOptions & NormalizeOptions.DropTnefRecipientTable) != (NormalizeOptions)0 && !this.dropRecipientTable)
			{
				this.topMessage.InvalidateTnefContent();
				this.dropRecipientTable = true;
			}
			if ((normalizeOptions & NormalizeOptions.DropTnefSenderProperties) != (NormalizeOptions)0)
			{
				for (int i = 0; i < PureTnefMessage.SenderProperties.Length; i++)
				{
					this.SetProperty(PureTnefMessage.SenderProperties[i], null);
				}
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00018A9B File Offset: 0x00016C9B
		internal override void Synchronize()
		{
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00018A9D File Offset: 0x00016C9D
		internal override int Version
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00018AA0 File Offset: 0x00016CA0
		internal override EmailRecipientCollection BccFromOrgHeader
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.BccFromOrgHeaderRecipients == null)
				{
					this.BccFromOrgHeaderRecipients = new EmailRecipientCollection(this, RecipientType.Bcc, true);
				}
				return this.BccFromOrgHeaderRecipients;
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00018AC4 File Offset: 0x00016CC4
		internal override void SetReadOnly(bool makeReadOnly)
		{
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00018AC6 File Offset: 0x00016CC6
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00018ACE File Offset: 0x00016CCE
		internal bool Stnef
		{
			get
			{
				return this.stnef;
			}
			set
			{
				this.stnef = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00018AD8 File Offset: 0x00016CD8
		internal string Correlator
		{
			get
			{
				byte[] array = this.GetProperty(TnefPropertyTag.TnefCorrelationKey) as byte[];
				if (array == null)
				{
					return null;
				}
				int num = array.Length;
				if (num == 0)
				{
					return string.Empty;
				}
				return ByteString.BytesToString(array, 0, num - 1, false);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00018B13 File Offset: 0x00016D13
		internal Charset TextCharset
		{
			get
			{
				return this.textCharset;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00018B1B File Offset: 0x00016D1B
		internal Charset BinaryCharset
		{
			get
			{
				return this.binaryCharset;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00018B23 File Offset: 0x00016D23
		internal EmailRecipientCollection GetRecipientCollection(RecipientType recipientType)
		{
			if (recipientType == RecipientType.To)
			{
				return this.ToRecipients;
			}
			if (RecipientType.Cc == recipientType)
			{
				return this.CcRecipients;
			}
			return null;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00018B3B File Offset: 0x00016D3B
		internal override void AddRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient newRecipient)
		{
			this.ThrowIfDisposed();
			newRecipient.TnefRecipient.TnefMessage = this;
			this.SetDirty();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00018B55 File Offset: 0x00016D55
		internal override void RemoveRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient oldRecipient)
		{
			this.ThrowIfDisposed();
			oldRecipient.TnefRecipient.TnefMessage = null;
			oldRecipient.TnefRecipient.OriginalIndex = int.MinValue;
			this.SetDirty();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00018B80 File Offset: 0x00016D80
		internal override void ClearRecipients(RecipientType recipientType, ref object cachedHeader)
		{
			this.ThrowIfDisposed();
			foreach (EmailRecipient emailRecipient in this.GetRecipientCollection(recipientType))
			{
				emailRecipient.TnefRecipient.TnefMessage = null;
				emailRecipient.TnefRecipient.OriginalIndex = int.MinValue;
			}
			this.SetDirty();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00018BF8 File Offset: 0x00016DF8
		internal override IBody GetBody()
		{
			return this;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00018BFB File Offset: 0x00016DFB
		BodyFormat IBody.GetBodyFormat()
		{
			return this.bodyData.BodyFormat;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00018C08 File Offset: 0x00016E08
		string IBody.GetCharsetName()
		{
			return this.bodyData.CharsetName;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00018C15 File Offset: 0x00016E15
		MimePart IBody.GetMimePart()
		{
			return null;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00018C18 File Offset: 0x00016E18
		Stream IBody.GetContentReadStream()
		{
			Stream readStream = this.bodyData.GetReadStream();
			return this.bodyData.ConvertReadStreamFormat(readStream);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00018C3F File Offset: 0x00016E3F
		bool IBody.TryGetContentReadStream(out Stream stream)
		{
			stream = this.bodyData.GetReadStream();
			stream = this.bodyData.ConvertReadStreamFormat(stream);
			return true;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00018C60 File Offset: 0x00016E60
		Stream IBody.GetContentWriteStream(Charset charset)
		{
			this.bodyData.ReleaseStorage();
			if (this.topMessage != null && !this.stnef)
			{
				MimePart legacyPlainTextBody = this.topMessage.GetLegacyPlainTextBody();
				if (legacyPlainTextBody != null)
				{
					legacyPlainTextBody.SetStorage(null, 0L, 0L);
					if (charset != null && charset != this.bodyData.Charset)
					{
						Charset charset2 = Utility.TranslateWriteStreamCharset(charset);
						if (charset2 != this.binaryCharset)
						{
							this.binaryCharset = charset2;
							this.forceUnicode = true;
							this.SetProperty(TnefPropertyTag.InternetCPID, this.binaryCharset.CodePage);
						}
					}
					this.topMessage.PureMimeMessage.SetPartCharset(legacyPlainTextBody, this.binaryCharset.Name);
					ContentTransferEncoding contentTransferEncoding = legacyPlainTextBody.ContentTransferEncoding;
					if (contentTransferEncoding == ContentTransferEncoding.Unknown || ContentTransferEncoding.SevenBit == contentTransferEncoding || ContentTransferEncoding.EightBit == contentTransferEncoding)
					{
						legacyPlainTextBody.UpdateTransferEncoding(ContentTransferEncoding.QuotedPrintable);
					}
				}
			}
			if (this.bodyPropertyTag.Id == TnefPropertyId.RtfCompressed)
			{
				this.properties[TnefPropertyId.Body] = null;
				if (this.bodyData.BodyFormat == BodyFormat.Html)
				{
					this.properties[TnefPropertyId.RtfCompressed] = null;
					this.bodyPropertyTag = TnefPropertyTag.BodyHtmlB;
					this.properties[this.bodyPropertyTag] = new StoragePropertyValue(this.bodyPropertyTag, null, 0L, 0L);
					this.bodyData.SetFormat(BodyFormat.Html, InternalBodyFormat.Html, this.binaryCharset);
				}
				if (this.bodyData.BodyFormat == BodyFormat.Text && charset != null)
				{
					this.binaryCharset = charset;
					this.SetProperty(TnefPropertyTag.InternetCPID, this.binaryCharset.CodePage);
				}
			}
			else if (this.bodyPropertyTag.Id == TnefPropertyId.BodyHtml)
			{
				this.properties[TnefPropertyId.RtfCompressed] = null;
				this.properties[TnefPropertyId.Body] = null;
				StoragePropertyValue storagePropertyValue = this.properties[TnefPropertyId.BodyHtml] as StoragePropertyValue;
				if (storagePropertyValue == null)
				{
					storagePropertyValue = (this.properties[TnefPropertyTag.BodyHtmlB] as StoragePropertyValue);
				}
				storagePropertyValue.SetStorage(null, 0L, 0L);
				storagePropertyValue.SetBinaryPropertyTag();
				this.bodyPropertyTag = storagePropertyValue.PropertyTag;
				this.bodyData.SetFormat(BodyFormat.Html, InternalBodyFormat.Html, this.binaryCharset);
			}
			else
			{
				StoragePropertyValue storagePropertyValue = this.properties[TnefPropertyId.Body] as StoragePropertyValue;
				storagePropertyValue.SetStorage(null, 0L, 0L);
				if (charset != null && charset != this.bodyData.Charset && this.bodyData.Charset != Charset.Unicode)
				{
					storagePropertyValue.SetUnicodePropertyTag();
					this.bodyPropertyTag = storagePropertyValue.PropertyTag;
					this.bodyData.SetFormat(BodyFormat.Text, InternalBodyFormat.Text, Charset.Unicode);
					this.binaryCharset = Charset.UTF8;
					this.SetProperty(TnefPropertyTag.InternetCPID, this.binaryCharset.CodePage);
				}
			}
			Stream stream = new BodyContentWriteStream(this);
			return this.bodyData.ConvertWriteStreamFormat(stream, charset);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00018F2F File Offset: 0x0001712F
		void IBody.SetNewContent(DataStorage storage, long start, long end)
		{
			this.bodyData.SetStorage(storage, start, end);
			this.BodyModified();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00018F48 File Offset: 0x00017148
		bool IBody.ConversionNeeded(int[] validCodepages)
		{
			bool result = false;
			object property = this.GetProperty(TnefPropertyTag.InternetCPID);
			if (property != null && property is int)
			{
				result = true;
				foreach (int num in validCodepages)
				{
					if (num == (int)property)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00018F98 File Offset: 0x00017198
		private void PickBestBody()
		{
			StoragePropertyValue storagePropertyValue = this.properties[TnefPropertyId.BodyHtml] as StoragePropertyValue;
			if (storagePropertyValue != null)
			{
				Charset charset = this.PickCharsetBasedOnPropertyTag(storagePropertyValue.PropertyTag);
				this.SetBody(storagePropertyValue.PropertyTag, charset, storagePropertyValue.Storage, storagePropertyValue.Start, storagePropertyValue.End);
				return;
			}
			StoragePropertyValue storagePropertyValue2 = this.properties[TnefPropertyId.RtfCompressed] as StoragePropertyValue;
			if (storagePropertyValue2 != null)
			{
				this.SetBody(storagePropertyValue2.PropertyTag, this.binaryCharset, storagePropertyValue2.Storage, storagePropertyValue2.Start, storagePropertyValue2.End);
				return;
			}
			StoragePropertyValue storagePropertyValue3 = this.properties[TnefPropertyId.Body] as StoragePropertyValue;
			if (storagePropertyValue3 != null)
			{
				Charset charset2 = this.PickCharsetBasedOnPropertyTag(storagePropertyValue3.PropertyTag);
				this.SetBody(storagePropertyValue3.PropertyTag, charset2, storagePropertyValue3.Storage, storagePropertyValue3.Start, storagePropertyValue3.End);
				return;
			}
			this.bodyData.SetFormat(BodyFormat.None, InternalBodyFormat.None, null);
			this.bodyData.SetStorage(null, 0L, 0L);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019090 File Offset: 0x00017290
		private void SetBody(TnefPropertyTag tag, Charset charset, DataStorage storage, long start, long end)
		{
			this.bodyData.SetStorage(storage, start, end);
			this.bodyPropertyTag = tag;
			BodyFormat format;
			InternalBodyFormat internalFormat;
			this.GetFormat(out format, out internalFormat, ref charset);
			this.bodyData.SetFormat(format, internalFormat, charset);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000190CE File Offset: 0x000172CE
		private Charset PickCharsetBasedOnPropertyTag(TnefPropertyTag tag)
		{
			if (tag.TnefType == TnefPropertyType.Binary)
			{
				return this.binaryCharset;
			}
			if (tag.TnefType != TnefPropertyType.String8)
			{
				return Charset.Unicode;
			}
			return this.textCharset;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000190FC File Offset: 0x000172FC
		private void GetFormat(out BodyFormat bodyFormat, out InternalBodyFormat internalBodyFormat, ref Charset charset)
		{
			TnefPropertyId id = this.bodyPropertyTag.Id;
			if (id == TnefPropertyId.Body)
			{
				bodyFormat = BodyFormat.Text;
				internalBodyFormat = InternalBodyFormat.Text;
				return;
			}
			if (id != TnefPropertyId.RtfCompressed)
			{
				if (id != TnefPropertyId.BodyHtml)
				{
					bodyFormat = BodyFormat.None;
					internalBodyFormat = InternalBodyFormat.None;
					return;
				}
				bodyFormat = BodyFormat.Html;
				internalBodyFormat = InternalBodyFormat.Html;
				return;
			}
			else
			{
				bodyFormat = BodyFormat.Rtf;
				internalBodyFormat = InternalBodyFormat.RtfCompressed;
				Stream readStream = this.bodyData.GetReadStream();
				Stream inputRtfStream = new ConverterStream(readStream, new RtfCompressedToRtf(), ConverterStreamAccess.Read);
				this.bodyRtfPreviewStream = new RtfPreviewStream(inputRtfStream, 4096);
				switch (this.bodyRtfPreviewStream.Encapsulation)
				{
				case RtfEncapsulation.None:
					this.bodyRtfPreviewStream.Dispose();
					this.bodyRtfPreviewStream = null;
					return;
				case RtfEncapsulation.Text:
					bodyFormat = BodyFormat.Text;
					charset = Charset.Unicode;
					return;
				case RtfEncapsulation.Html:
					bodyFormat = BodyFormat.Html;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000191B4 File Offset: 0x000173B4
		internal void BodyModified()
		{
			StoragePropertyValue storagePropertyValue;
			DataStorage dataStorage;
			long num;
			long num2;
			if (this.bodyPropertyTag.Id == TnefPropertyId.RtfCompressed)
			{
				storagePropertyValue = (this.properties[TnefPropertyId.RtfCompressed] as StoragePropertyValue);
				this.bodyData.GetStorage(InternalBodyFormat.RtfCompressed, this.binaryCharset, out dataStorage, out num, out num2);
			}
			else if (this.bodyPropertyTag.Id == TnefPropertyId.BodyHtml)
			{
				storagePropertyValue = (this.properties[TnefPropertyId.BodyHtml] as StoragePropertyValue);
				if (storagePropertyValue == null)
				{
					storagePropertyValue = (this.properties[TnefPropertyTag.BodyHtmlB] as StoragePropertyValue);
				}
				this.bodyData.GetStorage(InternalBodyFormat.Html, this.binaryCharset, out dataStorage, out num, out num2);
			}
			else
			{
				storagePropertyValue = (this.properties[TnefPropertyId.Body] as StoragePropertyValue);
				Charset charset = this.PickCharsetBasedOnPropertyTag(storagePropertyValue.PropertyTag);
				this.bodyData.GetStorage(InternalBodyFormat.Text, charset, out dataStorage, out num, out num2);
			}
			storagePropertyValue.SetStorage(dataStorage, num, num2);
			dataStorage.Release();
			this.properties.Touch(this.bodyPropertyTag.Id);
			this.SetDirty();
			if (this.topMessage != null && !this.stnef)
			{
				MimePart legacyPlainTextBody = this.topMessage.GetLegacyPlainTextBody();
				if (legacyPlainTextBody != null)
				{
					this.bodyData.GetStorage(InternalBodyFormat.Text, this.binaryCharset, out dataStorage, out num, out num2);
					legacyPlainTextBody.SetStorage(dataStorage, num, num2);
					dataStorage.Release();
				}
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019310 File Offset: 0x00017510
		internal override AttachmentCookie AttachmentCollection_AddAttachment(Attachment attachment)
		{
			this.ThrowIfDisposed();
			if (this.IsOpaqueMessage)
			{
				throw new InvalidOperationException(EmailMessageStrings.CannotAddAttachment);
			}
			this.SetDirty();
			TnefAttachmentData tnefAttachmentData = new TnefAttachmentData(int.MinValue, this);
			tnefAttachmentData.Attachment = attachment;
			int index = this.tnefAttachments.Add(tnefAttachmentData);
			AttachmentCookie result = new AttachmentCookie(index, this);
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019368 File Offset: 0x00017568
		internal override bool AttachmentCollection_RemoveAttachment(AttachmentCookie cookie)
		{
			this.ThrowIfDisposed();
			TnefAttachmentData dataAtPrivateIndex = this.tnefAttachments.GetDataAtPrivateIndex(cookie.Index);
			bool flag = this.tnefAttachments.RemoveAtPrivateIndex(cookie.Index);
			if (flag)
			{
				dataAtPrivateIndex.Invalidate();
				this.SetDirty();
			}
			return flag;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000193B4 File Offset: 0x000175B4
		internal override void AttachmentCollection_ClearAttachments()
		{
			this.ThrowIfDisposed();
			foreach (TnefAttachmentData tnefAttachmentData in this.tnefAttachments.InternalList)
			{
				if (tnefAttachmentData != null)
				{
					tnefAttachmentData.Invalidate();
				}
			}
			this.tnefAttachments.Clear();
			this.SetDirty();
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019428 File Offset: 0x00017628
		internal override int AttachmentCollection_Count()
		{
			return this.tnefAttachments.Count;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00019438 File Offset: 0x00017638
		internal override object AttachmentCollection_Indexer(int publicIndex)
		{
			TnefAttachmentData dataAtPublicIndex = this.tnefAttachments.GetDataAtPublicIndex(publicIndex);
			return dataAtPublicIndex.Attachment;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00019458 File Offset: 0x00017658
		internal override AttachmentCookie AttachmentCollection_CacheAttachment(int publicIndex, object attachment)
		{
			TnefAttachmentData dataAtPublicIndex = this.tnefAttachments.GetDataAtPublicIndex(publicIndex);
			dataAtPublicIndex.Attachment = attachment;
			int privateIndex = this.tnefAttachments.GetPrivateIndex(publicIndex);
			AttachmentCookie result = new AttachmentCookie(privateIndex, this);
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00019490 File Offset: 0x00017690
		private TnefAttachmentData DataFromCookie(AttachmentCookie cookie)
		{
			TnefAttachmentData dataAtPrivateIndex = this.tnefAttachments.GetDataAtPrivateIndex(cookie.Index);
			if (dataAtPrivateIndex == null)
			{
				throw new InvalidOperationException(EmailMessageStrings.AttachmentRemovedFromMessage);
			}
			return dataAtPrivateIndex;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000194BF File Offset: 0x000176BF
		internal override MimePart Attachment_GetMimePart(AttachmentCookie cookie)
		{
			return null;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000194C4 File Offset: 0x000176C4
		internal override string Attachment_GetContentType(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return this.GetAttachmentContentType(tnefAttachmentData.Properties);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000194E8 File Offset: 0x000176E8
		internal override void Attachment_SetContentType(AttachmentCookie cookie, string contentType)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			this.SetAttachmentContentType(tnefAttachmentData.Properties, contentType);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001950C File Offset: 0x0001770C
		internal override AttachmentMethod Attachment_GetAttachmentMethod(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			AttachmentMethod attachmentMethod = Utility.GetAttachmentMethod(tnefAttachmentData.Properties);
			if (AttachmentMethod.EmbeddedMessage == attachmentMethod && tnefAttachmentData.EmbeddedMessage == null)
			{
				attachmentMethod = AttachmentMethod.AttachByValue;
			}
			return attachmentMethod;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001953C File Offset: 0x0001773C
		internal override InternalAttachmentType Attachment_GetAttachmentType(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.InternalAttachmentType;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00019558 File Offset: 0x00017758
		internal override void Attachment_SetAttachmentType(AttachmentCookie cookie, InternalAttachmentType attachmentType)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			tnefAttachmentData.InternalAttachmentType = attachmentType;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00019574 File Offset: 0x00017774
		internal override EmailMessage Attachment_GetEmbeddedMessage(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.EmbeddedMessage;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001958F File Offset: 0x0001778F
		internal override void Attachment_SetEmbeddedMessage(AttachmentCookie cookie, EmailMessage value)
		{
			throw new InvalidOperationException(EmailMessageStrings.CannotSetEmbeddedMessageForTnefAttachment);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001959C File Offset: 0x0001779C
		internal override string Attachment_GetFileName(AttachmentCookie cookie, ref int attachmentNumber)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			if (tnefAttachmentData.FileName != null)
			{
				return tnefAttachmentData.FileName;
			}
			string rawFileName = Utility.GetRawFileName(tnefAttachmentData.Properties, this.stnef);
			tnefAttachmentData.FileName = Utility.SanitizeOrRegenerateFileName(rawFileName, ref attachmentNumber);
			return tnefAttachmentData.FileName;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000195E8 File Offset: 0x000177E8
		internal override void Attachment_SetFileName(AttachmentCookie cookie, string name)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			if (Utility.GetAttachmentMethod(tnefAttachmentData.Properties) == AttachmentMethod.EmbeddedMessage)
			{
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.DisplayNameA, this.stnef, name);
			}
			else
			{
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.AttachLongFilenameA, this.stnef, name);
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.AttachTransportNameA, this.stnef, name);
				string value = null;
				try
				{
					value = Path.GetExtension(name);
				}
				catch (ArgumentException)
				{
					value = null;
				}
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.AttachExtensionA, this.stnef, value);
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.AttachPathnameA, this.stnef, null);
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.AttachFilenameA, this.stnef, null);
				tnefAttachmentData.Properties.SetProperty(TnefPropertyTag.DisplayNameA, this.stnef, null);
			}
			tnefAttachmentData.FileName = name;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000196D8 File Offset: 0x000178D8
		internal override string Attachment_GetContentDisposition(AttachmentCookie cookie)
		{
			return "attachment";
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000196DF File Offset: 0x000178DF
		internal override void Attachment_SetContentDisposition(AttachmentCookie cookie, string unused)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000196E6 File Offset: 0x000178E6
		internal override bool Attachment_IsAppleDouble(AttachmentCookie cookie)
		{
			return false;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000196EC File Offset: 0x000178EC
		internal override Stream Attachment_GetContentReadStream(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return this.GetContentReadStream(tnefAttachmentData.Properties);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019710 File Offset: 0x00017910
		private Stream GetContentReadStream(TnefPropertyBag properties)
		{
			StoragePropertyValue storagePropertyValue = properties[TnefPropertyId.AttachData] as StoragePropertyValue;
			if (storagePropertyValue == null)
			{
				return DataStorage.NewEmptyReadStream();
			}
			return storagePropertyValue.Storage.OpenReadStream(storagePropertyValue.Start, storagePropertyValue.End);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001974E File Offset: 0x0001794E
		internal override bool Attachment_TryGetContentReadStream(AttachmentCookie cookie, out Stream result)
		{
			result = this.Attachment_GetContentReadStream(cookie);
			return true;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001975C File Offset: 0x0001795C
		internal override Stream Attachment_GetContentWriteStream(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			tnefAttachmentData.EmbeddedMessage = null;
			this.SetDirty(tnefAttachmentData);
			TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
			tnefAttachmentData.Properties.Touch(TnefPropertyId.AttachData);
			StoragePropertyValue storagePropertyValue = tnefAttachmentData.Properties[TnefPropertyId.AttachData] as StoragePropertyValue;
			if (storagePropertyValue == null)
			{
				tnefAttachmentData.Properties[TnefPropertyId.AttachData] = new StoragePropertyValue(TnefPropertyTag.AttachDataBin, temporaryDataStorage, 0L, long.MaxValue);
			}
			else
			{
				storagePropertyValue.SetStorage(temporaryDataStorage, 0L, long.MaxValue);
			}
			return temporaryDataStorage.OpenWriteStream(true);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000197F0 File Offset: 0x000179F0
		internal override int Attachment_GetRenderingPosition(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			int result = -1;
			object obj = tnefAttachmentData.Properties[TnefPropertyId.RenderingPosition];
			if (obj is int)
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00019828 File Offset: 0x00017A28
		internal override string Attachment_GetAttachContentID(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.Properties[TnefPropertyId.AttachContentId] as string;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00019854 File Offset: 0x00017A54
		internal override string Attachment_GetAttachContentLocation(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.Properties[TnefPropertyId.AttachContentLocation] as string;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00019880 File Offset: 0x00017A80
		internal override byte[] Attachment_GetAttachRendering(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.Properties[TnefPropertyId.AttachRendering] as byte[];
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000198AC File Offset: 0x00017AAC
		internal override int Attachment_GetAttachmentFlags(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			int result = 0;
			object obj = tnefAttachmentData.Properties[TnefPropertyId.AttachmentFlags];
			if (obj is int)
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000198E4 File Offset: 0x00017AE4
		internal override bool Attachment_GetAttachHidden(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			bool result = false;
			object obj = tnefAttachmentData.Properties[TnefPropertyId.AttachHidden];
			if (obj is bool)
			{
				result = (bool)obj;
			}
			return result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001991C File Offset: 0x00017B1C
		internal override int Attachment_GetHashCode(AttachmentCookie cookie)
		{
			TnefAttachmentData tnefAttachmentData = this.DataFromCookie(cookie);
			return tnefAttachmentData.Properties.GetHashCode();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001993C File Offset: 0x00017B3C
		internal override void Attachment_Dispose(AttachmentCookie cookie)
		{
			TnefAttachmentData data = this.DataFromCookie(cookie);
			this.DisposeAttachment(data);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00019958 File Offset: 0x00017B58
		private string GetAttachmentContentType(TnefPropertyBag properties)
		{
			string text = properties.GetProperty(TnefPropertyTag.AttachMimeTagA, this.stnef) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = "application/octet-stream";
			}
			return text;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001998B File Offset: 0x00017B8B
		private void SetAttachmentContentType(TnefPropertyBag properties, string contentType)
		{
			properties.SetProperty(TnefPropertyTag.AttachMimeTagA, this.stnef, contentType);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001999F File Offset: 0x00017B9F
		private void DisposeAttachment(TnefAttachmentData data)
		{
			if (data.EmbeddedMessage != null)
			{
				data.EmbeddedMessage.Dispose();
				data.EmbeddedMessage = null;
			}
			data.Properties.Dispose();
			data.Invalidate();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000199CC File Offset: 0x00017BCC
		private void SetDirty(TnefAttachmentData data)
		{
			this.SetDirty();
			if (data.EmbeddedMessage != null)
			{
				data.Properties.Touch(TnefPropertyId.AttachData);
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000199EC File Offset: 0x00017BEC
		internal override void CopyTo(MessageImplementation destination)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000199F0 File Offset: 0x00017BF0
		internal bool TryGetImportance(out Importance importance)
		{
			this.ThrowIfDisposed();
			importance = Importance.Normal;
			object property = this.GetProperty(TnefPropertyTag.Importance);
			if (property == null || !(property is int))
			{
				return false;
			}
			int num = (int)property;
			if (num == 0)
			{
				importance = Importance.Low;
			}
			else if (2 == num)
			{
				importance = Importance.High;
			}
			return true;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019A38 File Offset: 0x00017C38
		internal bool TryGetPriority(out Priority priority)
		{
			this.ThrowIfDisposed();
			priority = Priority.Normal;
			object property = this.GetProperty(TnefPropertyTag.Priority);
			if (property == null || !(property is int))
			{
				return false;
			}
			int num = (int)property;
			if (-1 == num)
			{
				priority = Priority.NonUrgent;
			}
			else if (1 == num)
			{
				priority = Priority.Urgent;
			}
			return true;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00019A80 File Offset: 0x00017C80
		internal bool TryGetSensitivity(out Sensitivity sensitivity)
		{
			this.ThrowIfDisposed();
			sensitivity = Sensitivity.None;
			object property = this.GetProperty(TnefPropertyTag.Sensitivity);
			if (property == null || !(property is int))
			{
				return false;
			}
			int num = (int)property;
			if (0 < num && num < 4)
			{
				sensitivity = (Sensitivity)num;
			}
			return true;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00019AC2 File Offset: 0x00017CC2
		internal override IMapiPropertyAccess MapiProperties
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019AC5 File Offset: 0x00017CC5
		public object GetProperty(TnefPropertyTag tag)
		{
			return this.properties.GetProperty(tag, this.stnef);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019AD9 File Offset: 0x00017CD9
		public void SetProperty(TnefPropertyTag tag, object value)
		{
			this.properties.SetProperty(tag, this.stnef, value);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00019AEE File Offset: 0x00017CEE
		public object GetProperty(TnefNameTag nameTag)
		{
			return this.properties[nameTag];
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00019AFC File Offset: 0x00017CFC
		public void SetProperty(TnefNameTag nameTag, object value)
		{
			this.properties[nameTag] = value;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00019B0B File Offset: 0x00017D0B
		private void ThrowIfDisposed()
		{
			if (this.tnefStorage == null)
			{
				throw new ObjectDisposedException("EmailMessage");
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00019B20 File Offset: 0x00017D20
		private TnefAttachmentData FindAttachment(int originalIndex)
		{
			foreach (TnefAttachmentData tnefAttachmentData in this.tnefAttachments.InternalList)
			{
				if (tnefAttachmentData != null && originalIndex == tnefAttachmentData.OriginalIndex)
				{
					return tnefAttachmentData;
				}
			}
			return null;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019B84 File Offset: 0x00017D84
		private EmailRecipient FindRecipient(int originalIndex)
		{
			foreach (EmailRecipient emailRecipient in this.To)
			{
				if (originalIndex == emailRecipient.TnefRecipient.OriginalIndex)
				{
					return emailRecipient;
				}
			}
			foreach (EmailRecipient emailRecipient2 in this.Cc)
			{
				if (originalIndex == emailRecipient2.TnefRecipient.OriginalIndex)
				{
					return emailRecipient2;
				}
			}
			return null;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00019C34 File Offset: 0x00017E34
		internal void SetDirty()
		{
			if (this.parentAttachmentData != null)
			{
				if (this.parentAttachmentData.MessageImplementation != null)
				{
					PureTnefMessage pureTnefMessage = (PureTnefMessage)this.parentAttachmentData.MessageImplementation;
					pureTnefMessage.SetDirty();
				}
				if (this.parentAttachmentData.EmbeddedMessage != null)
				{
					this.parentAttachmentData.Properties.Touch(TnefPropertyId.AttachData);
					return;
				}
			}
			else if (this.topMessage != null)
			{
				this.topMessage.InvalidateTnefContent();
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00019CA4 File Offset: 0x00017EA4
		internal void SetDirty(TnefRecipient tnefRecipient)
		{
			if (this.FromRecipient != null && tnefRecipient == this.FromRecipient.TnefRecipient)
			{
				this.From = this.FromRecipient;
				return;
			}
			if (this.SenderRecipient != null && tnefRecipient == this.SenderRecipient.TnefRecipient)
			{
				this.Sender = this.SenderRecipient;
				return;
			}
			if (this.DntRecipient != null && tnefRecipient == this.DntRecipient.TnefRecipient)
			{
				this.DispositionNotificationTo = this.DntRecipient;
				return;
			}
			this.SetDirty();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019D20 File Offset: 0x00017F20
		internal void Attachment_Invalidate(object cookie, bool isEmbeddedMessage)
		{
			TnefPropertyBag tnefPropertyBag = (TnefPropertyBag)cookie;
			this.SetDirty();
			if (isEmbeddedMessage)
			{
				tnefPropertyBag.Touch(TnefPropertyId.AttachData);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00019D48 File Offset: 0x00017F48
		internal T? GetProperty<T>(TnefPropertyTag tag) where T : struct
		{
			this.ThrowIfDisposed();
			object property = this.GetProperty(tag);
			if (property == null || !(property is T))
			{
				return null;
			}
			return new T?((T)((object)property));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00019D83 File Offset: 0x00017F83
		internal void SetProperty<T>(TnefPropertyTag tag, T? propertyValue) where T : struct
		{
			this.ThrowIfDisposed();
			this.SetProperty(tag, (propertyValue != null) ? propertyValue.Value : null);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00019DAC File Offset: 0x00017FAC
		private bool MatchMessageClass(IEnumerable<string> prefixes, IEnumerable<string> names)
		{
			string mapiMessageClass = this.MapiMessageClass;
			foreach (string value in prefixes)
			{
				if (mapiMessageClass.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			foreach (string value2 in names)
			{
				if (mapiMessageClass.Equals(value2, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00019E50 File Offset: 0x00018050
		private bool IsLinkMonitorMessage
		{
			get
			{
				string text = this.GetProperty(TnefPropertyTag.ContentIdentifierW) as string;
				return !string.IsNullOrEmpty(text) && text.Equals("ExLinkMonitor", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00019E88 File Offset: 0x00018088
		private bool MimeCameFromLegacyExchange()
		{
			Header header = this.topMessage.RootPart.Headers.FindFirst("X-MimeOLE");
			if (header == null)
			{
				return false;
			}
			string headerValue = Utility.GetHeaderValue(header);
			return headerValue.StartsWith("Produced By Microsoft Exchange", StringComparison.Ordinal);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00019EC8 File Offset: 0x000180C8
		internal bool Load(Stream tnefReadStream)
		{
			this.GetTnefCharsetsFromMime();
			bool result;
			try
			{
				int defaultMessageCodepage = (this.textCharset != null) ? this.textCharset.CodePage : 0;
				using (TnefReader tnefReader = new TnefReader(tnefReadStream, defaultMessageCodepage, TnefComplianceMode.Loose))
				{
					bool flag = this.Load(tnefReader, 0, this.binaryCharset);
					result = flag;
				}
			}
			catch (ByteEncoderException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00019F3C File Offset: 0x0001813C
		internal bool Load(TnefReader reader, int embeddingDepth, Charset binaryCharset)
		{
			if (this.topMessage == null)
			{
				this.textCharset = Charset.GetCharset(reader.MessageCodepage);
				this.binaryCharset = binaryCharset;
			}
			int num = 0;
			bool flag = this.properties.Load(reader, this.tnefStorage, this.tnefStart, this.tnefEnd, TnefAttributeLevel.Message, embeddingDepth, binaryCharset);
			while (flag && TnefAttributeTag.AttachRenderData == reader.AttributeTag)
			{
				TnefAttachmentData tnefAttachmentData = new TnefAttachmentData(num++, this);
				TnefPropertyBag tnefPropertyBag = tnefAttachmentData.Properties;
				flag = tnefPropertyBag.Load(reader, this.tnefStorage, this.tnefStart, this.tnefEnd, TnefAttributeLevel.Attachment, embeddingDepth, binaryCharset);
				this.tnefAttachments.Add(tnefAttachmentData);
				if (flag && TnefAttributeLevel.Message == reader.AttributeLevel)
				{
					flag = this.properties.Load(reader, this.tnefStorage, this.tnefStart, this.tnefEnd, TnefAttributeLevel.Message, embeddingDepth, binaryCharset);
				}
			}
			if (this.textCharset == null)
			{
				this.textCharset = Charset.GetCharset((reader.MessageCodepage == 0) ? 1252 : reader.MessageCodepage);
			}
			object property = this.GetProperty(TnefPropertyTag.InternetCPID);
			Charset charset;
			if (property != null && property is int && Charset.TryGetCharset((int)property, out charset))
			{
				this.binaryCharset = charset;
			}
			this.PickBestBody();
			return EmailMessage.TestabilityEnableBetterFuzzing || TnefComplianceStatus.Compliant == (reader.ComplianceStatus & ~(TnefComplianceStatus.InvalidAttributeChecksum | TnefComplianceStatus.InvalidMessageCodepage | TnefComplianceStatus.InvalidDate));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001A084 File Offset: 0x00018284
		internal void LoadRecipients(TnefPropertyReader propertyReader)
		{
			int num = 0;
			while (propertyReader.ReadNextRow())
			{
				EmailRecipientCollection emailRecipientCollection = null;
				string displayName = null;
				string smtpAddress = null;
				string nativeAddress = null;
				string nativeAddressType = null;
				while (propertyReader.ReadNextProperty())
				{
					TnefPropertyTag propertyTag = propertyReader.PropertyTag;
					if (!propertyTag.IsMultiValued && !propertyReader.IsLargeValue)
					{
						TnefPropertyId id = propertyTag.Id;
						if (TnefPropertyTag.RecipientType == propertyTag)
						{
							if (TnefPropertyType.Long == propertyTag.TnefType || TnefPropertyType.I2 == propertyTag.TnefType)
							{
								int num2 = propertyReader.ReadValueAsInt32();
								emailRecipientCollection = ((1 == num2) ? this.To : ((2 == num2) ? this.Cc : null));
							}
						}
						else if (TnefPropertyId.SmtpAddress == id && (propertyTag.TnefType == TnefPropertyType.String8 || propertyTag.TnefType == TnefPropertyType.Unicode))
						{
							smtpAddress = this.ReadStringValue(propertyReader, propertyTag);
						}
						else if (TnefPropertyId.EmailAddress == id && (propertyTag.TnefType == TnefPropertyType.String8 || propertyTag.TnefType == TnefPropertyType.Unicode))
						{
							nativeAddress = this.ReadStringValue(propertyReader, propertyTag);
						}
						else if (TnefPropertyId.Addrtype == id && (propertyTag.TnefType == TnefPropertyType.String8 || propertyTag.TnefType == TnefPropertyType.Unicode))
						{
							nativeAddressType = this.ReadStringValue(propertyReader, propertyTag);
						}
						else if (TnefPropertyId.DisplayName == id && (propertyTag.TnefType == TnefPropertyType.String8 || propertyTag.TnefType == TnefPropertyType.Unicode))
						{
							displayName = this.ReadStringValue(propertyReader, propertyTag);
						}
					}
				}
				if (emailRecipientCollection != null)
				{
					emailRecipientCollection.InternalAdd(new EmailRecipient(new TnefRecipient(this, num, displayName, smtpAddress, nativeAddress, nativeAddressType)));
				}
				num++;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A20D File Offset: 0x0001840D
		private string ReadStringValue(TnefPropertyReader reader, TnefPropertyTag tag)
		{
			if (TnefPropertyType.String8 == tag.TnefType || TnefPropertyType.Unicode == tag.TnefType)
			{
				return reader.ReadValueAsString();
			}
			return string.Empty;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001A234 File Offset: 0x00018434
		private void GetTnefCharsetsFromMime()
		{
			this.binaryCharset = this.topMessage.GetMessageCharsetFromMime();
			if (this.MimeCameFromLegacyExchange())
			{
				this.textCharset = this.binaryCharset.Culture.WindowsCharset;
				if (this.textCharset.CodePage == 1200)
				{
					this.textCharset = null;
					return;
				}
			}
			else
			{
				this.textCharset = null;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001A294 File Offset: 0x00018494
		public void WriteTo(Stream destination)
		{
			if (this.scratchBuffer == null)
			{
				this.scratchBuffer = new byte[8192];
			}
			int codePage = this.textCharset.CodePage;
			using (TnefReader tnefReader = new TnefReader(this.tnefStorage.OpenReadStream(this.tnefStart, this.tnefEnd), codePage, TnefComplianceMode.Loose))
			{
				using (TnefWriter tnefWriter = new TnefWriter(new SuppressCloseStream(destination), tnefReader.AttachmentKey, tnefReader.MessageCodepage, TnefWriterFlags.NoStandardAttributes))
				{
					this.WriteMessage(tnefReader, tnefWriter, this.scratchBuffer);
				}
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001A340 File Offset: 0x00018540
		internal bool WriteMessage(TnefReader reader, TnefWriter writer, byte[] scratchBuffer)
		{
			bool flag = this.WriteMessageProperties(reader, writer, scratchBuffer);
			int num = 0;
			while (flag && TnefAttributeTag.AttachRenderData == reader.AttributeTag && TnefAttributeLevel.Attachment == reader.AttributeLevel)
			{
				TnefAttachmentData attachmentData = this.FindAttachment(num++);
				flag = this.WriteAttachmentProperties(reader, writer, attachmentData, scratchBuffer);
				if (flag && TnefAttributeLevel.Message == reader.AttributeLevel)
				{
					while (flag && TnefAttributeTag.AttachRenderData != reader.AttributeTag)
					{
						flag = this.properties.Write(reader, writer, TnefAttributeLevel.Message, this.dropRecipientTable, this.forceUnicode, scratchBuffer);
					}
				}
			}
			this.WriteNewAttachments(writer, scratchBuffer);
			return flag;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001A3D0 File Offset: 0x000185D0
		private bool WriteMessageProperties(TnefReader reader, TnefWriter writer, byte[] scratchBuffer)
		{
			bool flag = reader.ReadNextAttribute();
			if (flag && TnefAttributeTag.AttachRenderData != reader.AttributeTag && TnefAttributeLevel.Message == reader.AttributeLevel)
			{
				flag = this.properties.Write(reader, writer, TnefAttributeLevel.Message, this.dropRecipientTable, this.forceUnicode, scratchBuffer);
			}
			return flag;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001A41C File Offset: 0x0001861C
		internal void WriteRecipients(TnefPropertyReader propertyReader, TnefWriter writer, ref char[] buffer)
		{
			if (this.To.Count > 0 || this.Cc.Count > 0)
			{
				writer.StartAttribute(TnefAttributeTag.RecipientTable, TnefAttributeLevel.Message);
			}
			int num = 0;
			while (propertyReader.ReadNextRow())
			{
				EmailRecipient emailRecipient = this.FindRecipient(num++);
				if (emailRecipient != null)
				{
					writer.StartRow();
					bool flag = true;
					bool flag2 = true;
					bool flag3 = true;
					bool flag4 = true;
					while (propertyReader.ReadNextProperty())
					{
						TnefPropertyTag tag = propertyReader.PropertyTag;
						TnefPropertyId id = tag.Id;
						if (this.forceUnicode)
						{
							tag = tag.ToUnicode();
						}
						if (TnefPropertyId.SmtpAddress == id && (tag.TnefType == TnefPropertyType.String8 || tag.TnefType == TnefPropertyType.Unicode))
						{
							if (emailRecipient.SmtpAddress != null)
							{
								writer.WriteProperty(tag, emailRecipient.SmtpAddress);
								flag = false;
							}
						}
						else if (TnefPropertyId.EmailAddress == id && (tag.TnefType == TnefPropertyType.String8 || tag.TnefType == TnefPropertyType.Unicode))
						{
							if (emailRecipient.NativeAddress != null)
							{
								writer.WriteProperty(tag, emailRecipient.NativeAddress);
								flag2 = false;
							}
						}
						else if (TnefPropertyId.Addrtype == id && (tag.TnefType == TnefPropertyType.String8 || tag.TnefType == TnefPropertyType.Unicode))
						{
							if (emailRecipient.NativeAddressType != null)
							{
								writer.WriteProperty(tag, emailRecipient.NativeAddressType);
								flag3 = false;
							}
						}
						else if (TnefPropertyId.DisplayName == id && (tag.TnefType == TnefPropertyType.String8 || tag.TnefType == TnefPropertyType.Unicode))
						{
							if (emailRecipient.DisplayName != null)
							{
								writer.WriteProperty(tag, emailRecipient.DisplayName);
								flag4 = false;
							}
						}
						else if (tag.IsTnefTypeValid && tag.ValueTnefType != TnefPropertyType.Object)
						{
							if (this.forceUnicode && propertyReader.PropertyTag.ValueTnefType == TnefPropertyType.String8)
							{
								TnefPropertyBag.WriteUnicodeProperty(writer, propertyReader, tag, ref buffer);
							}
							else
							{
								writer.WriteProperty(propertyReader);
							}
						}
					}
					if (flag && emailRecipient.SmtpAddress != null)
					{
						writer.WriteProperty(TnefPropertyTag.SmtpAddressW, emailRecipient.SmtpAddress);
					}
					if (flag2 && emailRecipient.NativeAddress != null)
					{
						writer.WriteProperty(TnefPropertyTag.EmailAddressW, emailRecipient.NativeAddress);
					}
					if (flag3 && emailRecipient.NativeAddressType != null)
					{
						writer.WriteProperty(TnefPropertyTag.AddrtypeW, emailRecipient.NativeAddressType);
					}
					if (flag4 && emailRecipient.DisplayName != null)
					{
						writer.WriteProperty(TnefPropertyTag.DisplayNameW, emailRecipient.DisplayName);
					}
				}
			}
			this.WriteNewRecipients(writer);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001A66C File Offset: 0x0001886C
		private void WriteNewRecipients(TnefWriter writer)
		{
			for (int i = 0; i < 2; i++)
			{
				EmailRecipientCollection emailRecipientCollection = (i == 0) ? this.ToRecipients : this.CcRecipients;
				if (emailRecipientCollection != null)
				{
					int value = (i == 0) ? 1 : 2;
					foreach (EmailRecipient emailRecipient in emailRecipientCollection)
					{
						if (-2147483648 == emailRecipient.TnefRecipient.OriginalIndex)
						{
							writer.StartRow();
							writer.WriteProperty(TnefPropertyTag.DisplayNameW, emailRecipient.DisplayName ?? string.Empty);
							writer.WriteProperty(TnefPropertyTag.AddrtypeW, emailRecipient.NativeAddressType ?? string.Empty);
							writer.WriteProperty(TnefPropertyTag.EmailAddressW, emailRecipient.NativeAddress ?? string.Empty);
							writer.WriteProperty(TnefPropertyTag.RecipientType, value);
							writer.WriteProperty(TnefPropertyTag.SmtpAddressW, emailRecipient.SmtpAddress ?? string.Empty);
						}
					}
				}
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001A778 File Offset: 0x00018978
		private bool WriteAttachmentProperties(TnefReader reader, TnefWriter writer, TnefAttachmentData attachmentData, byte[] scratchBuffer)
		{
			bool result;
			if (attachmentData != null)
			{
				result = attachmentData.Properties.Write(reader, writer, TnefAttributeLevel.Attachment, this.dropRecipientTable, this.forceUnicode, scratchBuffer);
			}
			else
			{
				while ((result = reader.ReadNextAttribute()) && TnefAttributeTag.AttachRenderData != reader.AttributeTag && TnefAttributeLevel.Message != reader.AttributeLevel)
				{
				}
			}
			return result;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A7C8 File Offset: 0x000189C8
		private void WriteNewAttachments(TnefWriter writer, byte[] scratchBuffer)
		{
			DateTime utcNow = DateTime.UtcNow;
			foreach (TnefAttachmentData tnefAttachmentData in this.tnefAttachments.InternalList)
			{
				if (tnefAttachmentData != null && -2147483648 == tnefAttachmentData.OriginalIndex)
				{
					writer.StartAttribute(TnefAttributeTag.AttachRenderData, TnefAttributeLevel.Attachment);
					writer.WriteProperty(TnefPropertyTag.AttachMethod, tnefAttachmentData.AttachmentMethod);
					int value = -1;
					writer.WriteProperty(TnefPropertyTag.RenderingPosition, value);
					string value2 = tnefAttachmentData.FileName ?? string.Empty;
					if (!this.forceUnicode)
					{
						writer.StartAttribute(TnefAttributeTag.AttachTitle, TnefAttributeLevel.Attachment);
						writer.WriteProperty(TnefPropertyTag.AttachFilenameA, value2);
					}
					writer.StartAttribute(TnefAttributeTag.AttachCreateDate, TnefAttributeLevel.Attachment);
					writer.WriteProperty(TnefPropertyTag.CreationTime, utcNow);
					writer.StartAttribute(TnefAttributeTag.AttachModifyDate, TnefAttributeLevel.Attachment);
					writer.WriteProperty(TnefPropertyTag.LastModificationTime, utcNow);
					using (Stream contentReadStream = this.GetContentReadStream(tnefAttachmentData.Properties))
					{
						int num = contentReadStream.Read(scratchBuffer, 0, scratchBuffer.Length);
						if (num > 0)
						{
							writer.StartAttribute(TnefAttributeTag.AttachData, TnefAttributeLevel.Attachment);
							do
							{
								writer.WriteAttributeRawValue(scratchBuffer, 0, num);
								num = contentReadStream.Read(scratchBuffer, 0, scratchBuffer.Length);
							}
							while (num > 0);
						}
					}
					string attachmentContentType = this.GetAttachmentContentType(tnefAttachmentData.Properties);
					writer.StartAttribute(TnefAttributeTag.Attachment, TnefAttributeLevel.Attachment);
					writer.WriteProperty(TnefPropertyTag.CreationTime, utcNow);
					writer.WriteProperty(TnefPropertyTag.LastModificationTime, utcNow);
					writer.WriteProperty(TnefPropertyTag.AttachLongFilenameW, value2);
					writer.WriteProperty(TnefPropertyTag.RenderingPosition, value);
					writer.WriteProperty(TnefPropertyTag.AttachMimeTagW, attachmentContentType);
				}
			}
		}

		// Token: 0x040003FF RID: 1023
		internal const TnefComplianceStatus BannedTnefComplianceViolations = ~(TnefComplianceStatus.InvalidAttributeChecksum | TnefComplianceStatus.InvalidMessageCodepage | TnefComplianceStatus.InvalidDate);

		// Token: 0x04000400 RID: 1024
		internal EmailRecipientCollection ToRecipients;

		// Token: 0x04000401 RID: 1025
		internal EmailRecipientCollection CcRecipients;

		// Token: 0x04000402 RID: 1026
		internal EmailRecipientCollection BccRecipients;

		// Token: 0x04000403 RID: 1027
		internal EmailRecipientCollection BccFromOrgHeaderRecipients;

		// Token: 0x04000404 RID: 1028
		internal EmailRecipientCollection ReplyToRecipients;

		// Token: 0x04000405 RID: 1029
		internal EmailRecipient FromRecipient;

		// Token: 0x04000406 RID: 1030
		internal EmailRecipient SenderRecipient;

		// Token: 0x04000407 RID: 1031
		internal EmailRecipient DntRecipient;

		// Token: 0x04000408 RID: 1032
		private static readonly TnefPropertyTag[] SenderProperties = new TnefPropertyTag[]
		{
			TnefPropertyTag.SenderNameA,
			TnefPropertyTag.SenderEmailAddressA,
			TnefPropertyTag.SenderAddrtypeA,
			TnefPropertyTag.SenderEntryId,
			TnefPropertyTag.ReadReceiptRequested,
			TnefPropertyTag.SentRepresentingNameA,
			TnefPropertyTag.SentRepresentingEmailAddressA,
			TnefPropertyTag.SentRepresentingAddrtypeA,
			TnefPropertyTag.SentRepresentingEntryId
		};

		// Token: 0x04000409 RID: 1033
		private static readonly string[] systemMessageClassPrefixes = new string[]
		{
			"Report.IPM.Note.",
			"IPM.Document.",
			"IPM.Note.StorageQuotaWarning.",
			"IPM.Mailbeat.Bounce."
		};

		// Token: 0x0400040A RID: 1034
		private static readonly string[] systemMessageClassNames = new string[]
		{
			"SrvInfo.Expiry",
			"IPM.Note.StorageQuotaWarning",
			"IPM.Microsoft.Approval.Initiation"
		};

		// Token: 0x0400040B RID: 1035
		private static readonly string[] interpersonalMessageClassPrefixes = new string[]
		{
			"IPM.Schedule.Meeting.",
			"IPM.Note.Rules.ExternalOofTemplate.",
			"IPM.Note.Rules.OofTemplate.",
			"IPM.Recall.Report.",
			"IPM.Form.",
			"IPM.Note.Rules.ReplyTemplate.",
			"IPM.Note.Microsoft.Approval."
		};

		// Token: 0x0400040C RID: 1036
		private static readonly string[] interpersonalMessageClassNames = new string[]
		{
			"IPM.Outlook.Recall",
			"IPM.Note",
			"IPM.Form",
			"IPM.TaskRequest",
			"IPM.Note.Microsoft.Conversation.Voice",
			"IPM.Note.Microsoft.Missed.Voice",
			"IPM.Note.Microsoft.Voicemail.UM",
			"IPM.Note.Microsoft.Voicemail.UM.CA"
		};

		// Token: 0x0400040D RID: 1037
		private PureTnefMessage.PureTnefMessageThreadAccessToken accessToken;

		// Token: 0x0400040E RID: 1038
		private AttachmentDataCollection<TnefAttachmentData> tnefAttachments = new AttachmentDataCollection<TnefAttachmentData>();

		// Token: 0x0400040F RID: 1039
		private TnefPropertyBag properties;

		// Token: 0x04000410 RID: 1040
		private DataStorage tnefStorage;

		// Token: 0x04000411 RID: 1041
		private long tnefStart;

		// Token: 0x04000412 RID: 1042
		private long tnefEnd;

		// Token: 0x04000413 RID: 1043
		private Charset textCharset;

		// Token: 0x04000414 RID: 1044
		private Charset binaryCharset;

		// Token: 0x04000415 RID: 1045
		private MimeTnefMessage topMessage;

		// Token: 0x04000416 RID: 1046
		private TnefAttachmentData parentAttachmentData;

		// Token: 0x04000417 RID: 1047
		private byte[] scratchBuffer;

		// Token: 0x04000418 RID: 1048
		private bool dropRecipientTable;

		// Token: 0x04000419 RID: 1049
		private bool forceUnicode;

		// Token: 0x0400041A RID: 1050
		private bool stnef;

		// Token: 0x0400041B RID: 1051
		private TnefPropertyTag bodyPropertyTag;

		// Token: 0x0400041C RID: 1052
		private RtfPreviewStream bodyRtfPreviewStream;

		// Token: 0x0400041D RID: 1053
		private BodyData bodyData = new BodyData();

		// Token: 0x020000F8 RID: 248
		private class PureTnefMessageThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x06000786 RID: 1926 RVA: 0x0001AB42 File Offset: 0x00018D42
			internal PureTnefMessageThreadAccessToken(PureTnefMessage parent)
			{
			}
		}
	}
}
