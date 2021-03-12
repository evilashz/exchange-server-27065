using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.LAD;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200017F RID: 383
	internal class MessagePartManager : ActivityManager
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00030C50 File Offset: 0x0002EE50
		internal MessagePartManager(ActivityManager manager, MessagePartManager.ConfigClass config) : base(manager, config)
		{
			this.context = (base.MessagePlayerContext = manager.MessagePlayerContext);
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00030C7A File Offset: 0x0002EE7A
		internal OrganizationId OrgId
		{
			get
			{
				return this.user.ADUser.OrganizationId;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00030C8C File Offset: 0x0002EE8C
		private PlaybackContent ContentType
		{
			get
			{
				if (PlaybackContent.Unknown == this.context.ContentType)
				{
					this.SetPlaybackContentType();
				}
				return this.context.ContentType;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00030CB0 File Offset: 0x0002EEB0
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00030CF8 File Offset: 0x0002EEF8
		private LinkedListNode<MessagePartManager.MessagePart> CurrentPart
		{
			get
			{
				LinkedListNode<MessagePartManager.MessagePart> result = null;
				switch (this.context.Mode)
				{
				case PlaybackMode.Audio:
					result = this.context.CurrentWavePart;
					break;
				case PlaybackMode.Text:
					result = this.context.CurrentTextPart;
					break;
				}
				return result;
			}
			set
			{
				switch (this.context.Mode)
				{
				case PlaybackMode.Audio:
					this.context.CurrentWavePart = value;
					return;
				case PlaybackMode.Text:
					this.context.CurrentTextPart = value;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00030D39 File Offset: 0x0002EF39
		internal override void PreActionExecute(BaseUMCallSession vo)
		{
			if (this.user != null)
			{
				this.guard = this.user.CreateConnectionGuard();
			}
			base.PreActionExecute(vo);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00030D5B File Offset: 0x0002EF5B
		internal override void PostActionExecute(BaseUMCallSession vo)
		{
			if (this.item != null)
			{
				this.item.Dispose();
				this.item = null;
			}
			if (this.guard != null)
			{
				this.guard.Dispose();
				this.guard = null;
			}
			base.PostActionExecute(vo);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00030D98 File Offset: 0x0002EF98
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.user = vo.CurrentCallContext.CallerInfo;
			this.preferredCulture = vo.CurrentCallContext.Culture;
			base.Start(vo, refInfo);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		internal override void CheckAuthorization(UMSubscriber u)
		{
			if (!u.IsAuthenticated)
			{
				base.CheckAuthorization(u);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00030DD8 File Offset: 0x0002EFD8
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MessagePartManager::ExecuteAction action={0}.", new object[]
			{
				action
			});
			string input;
			if (string.Equals(action, "nextMessagePart", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextMessagePart();
			}
			else if (string.Equals(action, "firstMessagePart", StringComparison.OrdinalIgnoreCase))
			{
				input = this.FirstMessagePart();
			}
			else if (string.Equals(action, "nextMessageSection", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextMessageSection();
			}
			else
			{
				if (string.Equals(action, "selectLanguagePause", StringComparison.OrdinalIgnoreCase))
				{
					base.ExecuteAction("pause", vo);
					return base.ExecuteAction("selectLanguage", vo);
				}
				if (string.Equals(action, "nextLanguagePause", StringComparison.OrdinalIgnoreCase))
				{
					base.ExecuteAction("pause", vo);
					return base.ExecuteAction("nextLanguage", vo);
				}
				return base.ExecuteAction(action, vo);
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00030EB0 File Offset: 0x0002F0B0
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MessagePartManager::Dispose.", new object[0]);
					if (this.item != null)
					{
						this.item.Dispose();
						this.item = null;
					}
					if (this.guard != null)
					{
						this.guard.Dispose();
						this.guard = null;
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00030F24 File Offset: 0x0002F124
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MessagePartManager>(this);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00030F2C File Offset: 0x0002F12C
		private Item GetMessage(MailboxSession session)
		{
			if (this.item == null)
			{
				StoreObjectId id = this.context.Id;
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Getting item={0} from the store.", new object[]
				{
					id.ToBase64String()
				});
				this.item = Item.Bind(session, id);
				if (XsoUtil.IsProtectedVoicemail(this.item.ClassName))
				{
					Item item = null;
					try
					{
						this.item.Load(StoreObjectSchema.ContentConversionProperties);
						item = DRMUtils.OpenRestrictedContent((MessageItem)this.item, this.OrgId);
						UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.ProtectedVoiceMessageEncryptDecrypt.ToString());
					}
					catch (OpenRestrictedContentException obj)
					{
						UmGlobals.ExEvent.LogEvent(this.user.OrganizationId, UMEventLogConstants.Tuple_RMSReadFailure, this.user.OrganizationId.ToString(), this.user.ToString(), CommonUtil.ToEventLogString(obj));
						Util.IncrementCounter(SubscriberAccessCounters.VoiceMessageDecryptionFailures);
						UMEventNotificationHelper.PublishUMFailureEventNotificationItem(ExchangeComponent.UMProtocol, UMNotificationEvent.ProtectedVoiceMessageEncryptDecrypt.ToString());
						throw;
					}
					finally
					{
						this.item.Dispose();
						this.item = item;
					}
				}
			}
			return this.item;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00031068 File Offset: 0x0002F268
		private void SetPlaybackContentType()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				string value = (this.GetMessage(mailboxSessionLock.Session).Body.PreviewText ?? string.Empty).Trim();
				if (XsoUtil.IsPureVoice(this.GetMessage(mailboxSessionLock.Session).ClassName) || XsoUtil.IsProtectedVoicemail(this.GetMessage(mailboxSessionLock.Session).ClassName))
				{
					this.context.ContentType = PlaybackContent.Audio;
				}
				else if (XsoUtil.IsMixedVoice(this.GetMessage(mailboxSessionLock.Session).ClassName))
				{
					this.context.ContentType = (string.IsNullOrEmpty(value) ? PlaybackContent.Audio : PlaybackContent.AudioText);
				}
				else
				{
					this.context.ContentType = PlaybackContent.Text;
					ICollection<string> collection = MessagePartManager.WavePartBuilder.BuildAttachmentPlayOrder(this.GetMessage(mailboxSessionLock.Session));
					if (0 < collection.Count)
					{
						this.context.ContentType = (string.IsNullOrEmpty(value) ? PlaybackContent.Audio : PlaybackContent.TextAudio);
					}
				}
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00031170 File Offset: 0x0002F370
		private string FirstMessagePart()
		{
			if (this.builder == null)
			{
				this.InitializePlayback();
			}
			string result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				LinkedListNode<MessagePartManager.MessagePart> next = (this.CurrentPart != null) ? this.CurrentPart.List.First : this.builder.BuildParts(this.GetMessage(mailboxSessionLock.Session));
				result = this.MoveToNextNonEmptyPart(next);
			}
			return result;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000311F0 File Offset: 0x0002F3F0
		private string NextMessagePart()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MessagePartManager::NextMessagePart.", new object[0]);
			string result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				LinkedListNode<MessagePartManager.MessagePart> next;
				if (this.CurrentPart.Equals(this.CurrentPart.List.Last))
				{
					next = this.builder.BuildParts(this.GetMessage(mailboxSessionLock.Session));
				}
				else
				{
					next = this.CurrentPart.Next;
				}
				result = this.MoveToNextNonEmptyPart(next);
			}
			return result;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003128C File Offset: 0x0002F48C
		private string MoveToNextNonEmptyPart(LinkedListNode<MessagePartManager.MessagePart> next)
		{
			if (next == null || next.Value == null)
			{
				return "endOfSection";
			}
			this.MoveToPart(next);
			if (!next.Value.IsEmpty)
			{
				return null;
			}
			return this.NextMessagePart();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000312BC File Offset: 0x0002F4BC
		private void MoveToPart(LinkedListNode<MessagePartManager.MessagePart> part)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MessagePartManager::MoveToPart.", new object[0]);
			if (this.CurrentPart != null)
			{
				this.CurrentPart.Value.Deactivate(this);
			}
			if (part != null)
			{
				this.CurrentPart = part;
				try
				{
					this.CurrentPart.Value.Activate(this);
				}
				catch (OpenRestrictedContentException obj)
				{
					this.user.ToString();
					UmGlobals.ExEvent.LogEvent(this.user.OrganizationId, UMEventLogConstants.Tuple_RMSReadFailure, this.user.OrganizationId.ToString(), this.user.ToString(), CommonUtil.ToEventLogString(obj));
					Util.IncrementCounter(SubscriberAccessCounters.VoiceMessageDecryptionFailures);
					throw;
				}
				if (this.context.Language == null && UmCultures.GetSupportedPromptCultures().Count > 1)
				{
					this.CurrentPart.Value.DetectLanguage(this.preferredCulture);
					this.context.Language = this.CurrentPart.Value.Language;
					base.Manager.WriteVariable("messageLanguage", this.context.Language);
					if (!object.Equals(this.context.Language, this.preferredCulture))
					{
						base.Manager.WriteVariable("languageDetected", this.context.Language);
					}
				}
				this.WriteIntroConditions();
			}
			string varName = (this.context.Mode == PlaybackMode.Audio) ? "isEmptyWave" : "isEmptyText";
			object obj2 = this.ReadVariable(varName);
			bool flag = this.CurrentPart == null || this.CurrentPart.Value.IsEmpty;
			if ((obj2 == null || (bool)obj2) && (PlaybackContent.TextAudio == this.ContentType || PlaybackContent.AudioText == this.ContentType))
			{
				base.WriteVariable(varName, flag);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00031488 File Offset: 0x0002F688
		private string NextMessageSection()
		{
			string result = "endOfSection";
			base.WriteVariable("isEmptyWave", null);
			base.WriteVariable("isEmptyText", null);
			if (this.ContentType == PlaybackContent.AudioText && this.context.Mode == PlaybackMode.Audio)
			{
				this.builder = new MessagePartManager.TextPartBuilder(this.context);
				this.context.Mode = PlaybackMode.Text;
				result = this.FirstMessagePart();
			}
			else if (this.ContentType == PlaybackContent.TextAudio && PlaybackMode.Text == this.context.Mode)
			{
				this.builder = new MessagePartManager.WavePartBuilder(this.context);
				this.context.Mode = PlaybackMode.Audio;
				result = this.FirstMessagePart();
			}
			return result;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003152C File Offset: 0x0002F72C
		private void InitializePlayback()
		{
			switch (this.ContentType)
			{
			case PlaybackContent.Audio:
			case PlaybackContent.AudioText:
				this.builder = new MessagePartManager.WavePartBuilder(this.context);
				this.context.Mode = PlaybackMode.Audio;
				return;
			case PlaybackContent.TextAudio:
			case PlaybackContent.Text:
				this.builder = new MessagePartManager.TextPartBuilder(this.context);
				this.context.Mode = PlaybackMode.Text;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00031594 File Offset: 0x0002F794
		private void WriteIntroConditions()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = this.CurrentPart != null && this.CurrentPart == this.CurrentPart.List.First;
			if (this.ContentType == PlaybackContent.AudioText)
			{
				flag2 = (flag4 && PlaybackMode.Audio == this.context.Mode);
				flag3 = (flag4 && PlaybackMode.Text == this.context.Mode);
				flag = flag2;
			}
			else if (this.ContentType == PlaybackContent.TextAudio)
			{
				flag2 = (flag4 && PlaybackMode.Audio == this.context.Mode);
				flag3 = (flag4 && PlaybackMode.Text == this.context.Mode);
				flag = flag3;
			}
			base.WriteVariable("playMixedContentIntro", flag);
			base.WriteVariable("playAudioContentIntro", flag2);
			base.WriteVariable("playTextContentIntro", flag3);
		}

		// Token: 0x040009C1 RID: 2497
		private Item item;

		// Token: 0x040009C2 RID: 2498
		private MessagePartManager.MessagePartBuilder builder;

		// Token: 0x040009C3 RID: 2499
		private UMSubscriber user;

		// Token: 0x040009C4 RID: 2500
		private UMMailboxRecipient.MailboxConnectionGuard guard;

		// Token: 0x040009C5 RID: 2501
		private CultureInfo preferredCulture;

		// Token: 0x040009C6 RID: 2502
		private MessagePlayerContext context;

		// Token: 0x02000180 RID: 384
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000B64 RID: 2916 RVA: 0x0003166A File Offset: 0x0002F86A
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x00031673 File Offset: 0x0002F873
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing MessagePartConfig activity manager.", new object[0]);
				return new MessagePartManager(manager, this);
			}
		}

		// Token: 0x02000181 RID: 385
		internal abstract class MessagePartBuilder
		{
			// Token: 0x06000B66 RID: 2918 RVA: 0x00031692 File Offset: 0x0002F892
			protected MessagePartBuilder(MessagePlayerContext context)
			{
				this.context = context;
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000316A1 File Offset: 0x0002F8A1
			protected MessagePlayerContext Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x06000B68 RID: 2920
			internal abstract LinkedListNode<MessagePartManager.MessagePart> BuildParts(Item item);

			// Token: 0x040009C7 RID: 2503
			private MessagePlayerContext context;
		}

		// Token: 0x02000182 RID: 386
		internal class TextPartBuilder : MessagePartManager.MessagePartBuilder
		{
			// Token: 0x06000B69 RID: 2921 RVA: 0x000316A9 File Offset: 0x0002F8A9
			internal TextPartBuilder(MessagePlayerContext context) : base(context)
			{
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x000316B4 File Offset: 0x0002F8B4
			internal override LinkedListNode<MessagePartManager.MessagePart> BuildParts(Item item)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextPartBuilder::BuildParts.", new object[0]);
				StreamReader streamReader = this.GetReader(item);
				char[] array = new char[16384];
				base.Context.Remnant.CopyTo(array, 0);
				int num = base.Context.Remnant.Length;
				base.Context.Remnant = new char[0];
				num += streamReader.ReadBlock(array, num, array.Length - num);
				base.Context.SeekPosition = streamReader.BaseStream.Position;
				if (num < 1)
				{
					return null;
				}
				LinkedList<MessagePartManager.MessagePart> linkedList = (base.Context.CurrentTextPart == null) ? new LinkedList<MessagePartManager.MessagePart>() : base.Context.CurrentTextPart.List;
				LinkedListNode<MessagePartManager.MessagePart> last = linkedList.Last;
				int num2 = 0;
				MessagePartManager.TextMessagePart textMessagePart = this.BuildTextPart(array, ref num2, num);
				if (textMessagePart != null)
				{
					linkedList.AddLast(textMessagePart);
				}
				int num3 = Math.Max(0, num - num2);
				base.Context.Remnant = new char[num3];
				if (num3 > 0)
				{
					Array.Copy(array, num2, base.Context.Remnant, 0, num3);
				}
				if (last != null)
				{
					return last.Next;
				}
				return linkedList.First;
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x000317DC File Offset: 0x0002F9DC
			private static bool IsCalendarHeaderPattern(char[] buffer, int bufIdx, int maxBufSize)
			{
				if (Constants.CDOCalendarSeparator.Length > maxBufSize - bufIdx)
				{
					return false;
				}
				int i = 0;
				while (i < Constants.CDOCalendarSeparator.Length)
				{
					if (Constants.CDOCalendarSeparator[i] != buffer[bufIdx])
					{
						return false;
					}
					i++;
					bufIdx++;
				}
				return true;
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x00031820 File Offset: 0x0002FA20
			private static bool IsEmailHeaderPattern(char[] buffer, int bufIdx, int maxBufSize)
			{
				bool result = true;
				int num = 0;
				while (bufIdx < maxBufSize && num < 5)
				{
					if ('-' != buffer[bufIdx] && '_' != buffer[bufIdx])
					{
						result = false;
						break;
					}
					num++;
					bufIdx++;
				}
				return result;
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x00031858 File Offset: 0x0002FA58
			private Stream GetItemStream(Item item)
			{
				if (this.stream == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextPartBuilder::ItemStream.", new object[0]);
					long size = item.Body.Size;
					if (size > 262144L)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Using file stream to back item store content with size={0}.", new object[]
						{
							size
						});
						this.tempFile = TempFileFactory.CreateTempFile();
						this.stream = new FileStream(this.tempFile.FilePath, FileMode.Create);
					}
					else
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Using memory stream to back item store content with size={0}.", new object[]
						{
							size
						});
						this.stream = new MemoryStream();
					}
					BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.TextPlain, "unicode");
					using (Stream stream = item.Body.OpenReadStream(configuration))
					{
						byte[] array = new byte[8192];
						for (;;)
						{
							int num = stream.Read(array, 0, array.Length);
							if (num == 0)
							{
								break;
							}
							this.stream.Write(array, 0, num);
						}
					}
					this.stream.Seek(base.Context.SeekPosition, SeekOrigin.Begin);
				}
				return this.stream;
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x00031994 File Offset: 0x0002FB94
			private StreamReader GetReader(Item item)
			{
				if (this.reader == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextPartBuilder::Reader.", new object[0]);
					this.reader = new StreamReader(this.GetItemStream(item), Encoding.Unicode);
				}
				return this.reader;
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x000319D4 File Offset: 0x0002FBD4
			private MessagePartManager.TextMessagePart BuildTextPart(char[] buffer, ref int idx, int bufSize)
			{
				int num = idx;
				int num2 = idx;
				int num3 = idx;
				int num4 = 0;
				bufSize = Math.Min(checked(idx + 16384), bufSize);
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "idx={0}, bufSize={1}.", new object[]
				{
					idx,
					bufSize
				});
				int num5 = idx;
				while (idx < bufSize)
				{
					char c = buffer[idx];
					if (c <= '.')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							if (idx > 0 && buffer[idx - 1] != '\r')
							{
								num4++;
							}
							num = idx;
							goto IL_193;
						case '\v':
						case '\f':
							goto IL_193;
						case '\r':
							num4++;
							num = idx;
							goto IL_193;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
								goto IL_FF;
							default:
								switch (c)
								{
								case '*':
								{
									if (!MessagePartManager.TextPartBuilder.IsCalendarHeaderPattern(buffer, idx, bufSize))
									{
										goto IL_193;
									}
									if (4 > num4 && num5 == 0)
									{
										num5 = idx + Constants.CDOCalendarSeparator.Length;
										goto IL_193;
									}
									int i = 0;
									while (i < Constants.CDOCalendarSeparator.Length)
									{
										buffer[idx] = ' ';
										i++;
										idx++;
									}
									goto IL_193;
								}
								case '+':
								case ',':
									goto IL_193;
								case '-':
									goto IL_165;
								case '.':
									goto IL_FF;
								default:
									goto IL_193;
								}
								break;
							}
							break;
						}
						num3 = idx;
					}
					else
					{
						switch (c)
						{
						case ':':
						case ';':
							goto IL_FF;
						default:
							if (c == '?')
							{
								goto IL_FF;
							}
							if (c == '_')
							{
								goto IL_165;
							}
							break;
						}
					}
					IL_193:
					idx++;
					continue;
					IL_FF:
					if (idx + 1 < bufSize && char.IsWhiteSpace(buffer[idx + 1]))
					{
						num2 = idx;
						goto IL_193;
					}
					goto IL_193;
					IL_165:
					if (MessagePartManager.TextPartBuilder.IsEmailHeaderPattern(buffer, idx, bufSize))
					{
						while (idx < bufSize && ('-' == buffer[idx] || '_' == buffer[idx]))
						{
							buffer[idx] = ' ';
							idx++;
						}
						goto IL_193;
					}
					goto IL_193;
				}
				if (bufSize == buffer.Length)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "maxParagraphEnd ={0}, maxPhraseEnd={1}, maxWhitespace={2}.", new object[]
					{
						num,
						num2,
						num3
					});
					int num6 = (bufSize - num5) / 2 + 1;
					if (num > num6)
					{
						idx = num;
					}
					else if (num2 > num6)
					{
						idx = num2;
					}
					else if (num3 > num5)
					{
						idx = num3;
					}
				}
				if (idx == num5)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Could not build a text part: idx={0}, startIdx={1}. Returning null.", new object[]
					{
						idx,
						num5
					});
					return null;
				}
				return new MessagePartManager.TextMessagePart(new string(buffer, num5, idx - num5));
			}

			// Token: 0x040009C8 RID: 2504
			internal const long MaximumMemoryBuffer = 262144L;

			// Token: 0x040009C9 RID: 2505
			internal const int MaximumTtsBlock = 16384;

			// Token: 0x040009CA RID: 2506
			private Stream stream;

			// Token: 0x040009CB RID: 2507
			private ITempFile tempFile;

			// Token: 0x040009CC RID: 2508
			private StreamReader reader;
		}

		// Token: 0x02000183 RID: 387
		internal class WavePartBuilder : MessagePartManager.MessagePartBuilder
		{
			// Token: 0x06000B70 RID: 2928 RVA: 0x00031C30 File Offset: 0x0002FE30
			internal WavePartBuilder(MessagePlayerContext context) : base(context)
			{
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x00031C3C File Offset: 0x0002FE3C
			internal static ICollection<string> BuildAttachmentPlayOrder(Item item)
			{
				List<string> list = new List<string>();
				string attachmentOrderString = XsoUtil.GetAttachmentOrderString(item);
				string[] array = attachmentOrderString.Split(new char[]
				{
					';'
				});
				for (int i = array.Length - 1; i >= 0; i--)
				{
					string text = array[i].Trim();
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
				return list;
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x00031C9C File Offset: 0x0002FE9C
			internal override LinkedListNode<MessagePartManager.MessagePart> BuildParts(Item item)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "WavePartBuilder::BuildParts.", new object[0]);
				if (base.Context.CurrentWavePart != null && base.Context.CurrentWavePart.Value is MessagePartManager.WaveMessagePart)
				{
					return null;
				}
				LinkedList<MessagePartManager.MessagePart> linkedList = (base.Context.CurrentWavePart == null) ? new LinkedList<MessagePartManager.MessagePart>() : base.Context.CurrentWavePart.List;
				LinkedListNode<MessagePartManager.MessagePart> last = linkedList.Last;
				ICollection<string> collection = MessagePartManager.WavePartBuilder.BuildAttachmentPlayOrder(item);
				foreach (string attachName in collection)
				{
					MessagePartManager.WaveMessagePart value = new MessagePartManager.WaveMessagePart(attachName);
					linkedList.AddLast(new LinkedListNode<MessagePartManager.MessagePart>(value));
				}
				if (last != null)
				{
					return last.Next;
				}
				return linkedList.First;
			}
		}

		// Token: 0x02000184 RID: 388
		internal abstract class MessagePart
		{
			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000B73 RID: 2931
			internal abstract bool IsEmpty { get; }

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00031D78 File Offset: 0x0002FF78
			internal virtual CultureInfo Language
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000B75 RID: 2933
			internal abstract void Activate(MessagePartManager context);

			// Token: 0x06000B76 RID: 2934
			internal abstract void Deactivate(MessagePartManager context);

			// Token: 0x06000B77 RID: 2935 RVA: 0x00031D7B File Offset: 0x0002FF7B
			internal virtual void DetectLanguage(CultureInfo preferred)
			{
			}
		}

		// Token: 0x02000185 RID: 389
		internal class TextMessagePart : MessagePartManager.MessagePart
		{
			// Token: 0x06000B79 RID: 2937 RVA: 0x00031D88 File Offset: 0x0002FF88
			internal TextMessagePart(string text)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, null, "TextMessagePart::TextMessagePart", new object[0]);
				this.text = new EmailNormalizedText(text);
				this.originalText = (text ?? string.Empty);
				this.originalText = this.originalText.Trim();
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00031DDE File Offset: 0x0002FFDE
			internal override bool IsEmpty
			{
				get
				{
					return this.text == null || this.text.ToString().Length == 0 || !Regex.IsMatch(this.text.ToString(), "[^\\s<>]");
				}
			}

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00031E16 File Offset: 0x00030016
			internal override CultureInfo Language
			{
				get
				{
					return this.detectedLanguage;
				}
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x00031E20 File Offset: 0x00030020
			internal override void Activate(MessagePartManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::Activate.", new object[0]);
				manager.WriteVariable("textMessagePart", this.text);
				manager.WriteVariable("textPart", true);
				manager.WriteVariable("wavePart", false);
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x00031E76 File Offset: 0x00030076
			internal override void Deactivate(MessagePartManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::Deactivate.", new object[0]);
				manager.WriteVariable("textMessagePart", null);
				manager.WriteVariable("textPart", null);
				manager.WriteVariable("wavePart", null);
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x00031EB4 File Offset: 0x000300B4
			internal override void DetectLanguage(CultureInfo preferred)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::DetectLanguage.", new object[0]);
				if (this.IsEmpty)
				{
					return;
				}
				uint length = (uint)this.originalText.Length;
				if (GlobCfg.LanguageAutoDetectionMinLength < 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "DetectLanguage: Language autodetection is disabled by configuration file.", new object[0]);
					return;
				}
				if ((ulong)length < (ulong)((long)GlobCfg.LanguageAutoDetectionMinLength))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "DetectLanguage: Text length({0})<LanguageAutoDetectionMinLength({1}), skipping.", new object[]
					{
						length,
						GlobCfg.LanguageAutoDetectionMinLength
					});
					return;
				}
				string text;
				if ((ulong)length > (ulong)((long)GlobCfg.LanguageAutoDetectionMaxLength))
				{
					text = this.originalText.Substring(0, GlobCfg.LanguageAutoDetectionMaxLength);
					length = (uint)text.Length;
				}
				else
				{
					text = this.originalText;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::DetectLanguage Starting language detection. Lenght = {0}", new object[]
				{
					length
				});
				CultureInfo cultureInfo = LanguageDetector.Instance.AnalyzeText(text, length);
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::DetectLanguage Language detection complete. Detected = {0}, Preferred = {1}", new object[]
				{
					cultureInfo,
					preferred
				});
				if (cultureInfo != null)
				{
					cultureInfo = UmCultures.GetBestSupportedPromptCulture(cultureInfo);
				}
				if (cultureInfo != null && preferred != null && string.Equals(cultureInfo.TwoLetterISOLanguageName, preferred.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase))
				{
					this.detectedLanguage = preferred;
				}
				else
				{
					this.detectedLanguage = cultureInfo;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "TextMessagePart::DetectLanguage: DetectedLanguage = {0}", new object[]
				{
					this.detectedLanguage
				});
			}

			// Token: 0x040009CD RID: 2509
			private string originalText;

			// Token: 0x040009CE RID: 2510
			private EmailNormalizedText text;

			// Token: 0x040009CF RID: 2511
			private CultureInfo detectedLanguage;
		}

		// Token: 0x02000186 RID: 390
		internal class WaveMessagePart : MessagePartManager.MessagePart
		{
			// Token: 0x06000B7F RID: 2943 RVA: 0x0003201D File Offset: 0x0003021D
			internal WaveMessagePart(string attachName)
			{
				this.attachName = attachName;
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0003202C File Offset: 0x0003022C
			internal override bool IsEmpty
			{
				get
				{
					return this.tmpWavFile == null || !File.Exists(this.tmpWavFile.FilePath) || 0L == new FileInfo(this.tmpWavFile.FilePath).Length;
				}
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x00032068 File Offset: 0x00030268
			internal override void Activate(MessagePartManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "WaveMessagePart::Activate.", new object[0]);
				Stream stream = null;
				Stream stream2 = null;
				try
				{
					if (this.tmpWavFile == null)
					{
						using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = manager.user.CreateSessionLock())
						{
							using (ITempFile tempFile = TempFileFactory.CreateTempSoundFileFromAttachmentName(this.attachName))
							{
								using (Attachment attachmentStream = this.GetAttachmentStream(manager, mailboxSessionLock, out stream))
								{
									if (attachmentStream != null)
									{
										stream2 = new FileStream(tempFile.FilePath, FileMode.Create);
										CommonUtil.CopyStream(stream, stream2);
										stream.Close();
										stream = null;
										stream2.Close();
										stream2 = null;
									}
									this.tmpWavFile = MediaMethods.ToPcm(tempFile);
								}
							}
						}
					}
				}
				catch (AudioConversionException)
				{
					this.tmpWavFile = null;
				}
				finally
				{
					manager.WriteVariable("waveMessagePart", this.tmpWavFile);
					manager.WriteVariable("textPart", false);
					manager.WriteVariable("wavePart", !this.IsEmpty);
					if (stream != null)
					{
						stream.Close();
					}
					if (stream2 != null)
					{
						stream2.Close();
					}
				}
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x000321B4 File Offset: 0x000303B4
			internal override void Deactivate(MessagePartManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "WaveMessagePart::Deactivate.", new object[0]);
				manager.WriteVariable("waveMessagePart", null);
				manager.WriteVariable("textPart", null);
				manager.WriteVariable("wavePart", null);
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x000321F0 File Offset: 0x000303F0
			private Attachment GetAttachmentStream(MessagePartManager manager, UMMailboxRecipient.MailboxSessionLock mbx, out Stream result)
			{
				result = null;
				Item message = manager.GetMessage(mbx.Session);
				foreach (AttachmentHandle handle in message.AttachmentCollection)
				{
					Attachment attachment = null;
					try
					{
						attachment = message.AttachmentCollection.Open(handle);
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "found attachment name={0}.", new object[]
						{
							attachment.FileName
						});
						if (string.Equals(attachment.FileName, this.attachName, StringComparison.OrdinalIgnoreCase))
						{
							if (XsoUtil.IsProtectedVoicemail(message.ClassName))
							{
								result = DRMUtils.OpenProtectedAttachment(attachment, manager.OrgId);
							}
							else
							{
								result = (attachment as StreamAttachment).GetContentStream();
							}
							return attachment;
						}
					}
					finally
					{
						if (result == null && attachment != null)
						{
							attachment.Dispose();
						}
					}
				}
				return null;
			}

			// Token: 0x040009D0 RID: 2512
			private string attachName;

			// Token: 0x040009D1 RID: 2513
			private ITempWavFile tmpWavFile;
		}
	}
}
