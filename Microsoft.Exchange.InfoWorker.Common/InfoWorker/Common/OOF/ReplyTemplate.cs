using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x0200002E RID: 46
	internal sealed class ReplyTemplate : IDisposable
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00005F68 File Offset: 0x00004168
		public static ReplyTemplate Find(MailboxSession session, RuleAction.OOFReply ruleAction)
		{
			ReplyTemplate result;
			try
			{
				if (ruleAction.ReplyTemplateMessageEntryID == null)
				{
					result = ReplyTemplate.FindByTemplateGuid(session, ruleAction);
				}
				else
				{
					StoreObjectId messageId = StoreObjectId.FromProviderSpecificId(ruleAction.ReplyTemplateMessageEntryID);
					MessageItem messageItem = MessageItem.Bind(session, messageId, new PropertyDefinition[]
					{
						ItemSchema.ReplyTemplateId
					});
					messageItem.OpenAsReadWrite();
					ReplyTemplate.Tracer.TraceDebug<IExchangePrincipal, ByteArray>(0L, "Mailbox:{0}: Found reply template by entry Id. Entry id={1}", session.MailboxOwner, new ByteArray(ruleAction.ReplyTemplateMessageEntryID));
					ReplyTemplate.TracerPfd.TracePfd<int, IExchangePrincipal, ByteArray>(0L, "PFD IWO {0} Mailbox:{1}: Found reply template by entry Id. Entry id={2}", 31639, session.MailboxOwner, new ByteArray(ruleAction.ReplyTemplateMessageEntryID));
					result = new ReplyTemplate(messageItem);
				}
			}
			catch (ObjectNotFoundException)
			{
				ReplyTemplate.Tracer.TraceDebug<IExchangePrincipal, ByteArray>(0L, "Mailbox:{0}: Found no reply template by entry Id. Entry id={1}", session.MailboxOwner, new ByteArray(ruleAction.ReplyTemplateMessageEntryID));
				result = ReplyTemplate.FindByTemplateGuid(session, ruleAction);
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006048 File Offset: 0x00004248
		public static ReplyTemplate Create(MailboxSession session, Guid templateGuid, string messageClass, OofReplyType oofReplyType)
		{
			ReplyTemplate.Tracer.TraceDebug<IExchangePrincipal, Guid, string>(0L, "Mailbox:{0}: Creating new reply template. GUID={1}, MessageClass={2}", session.MailboxOwner, templateGuid, messageClass);
			ReplyTemplate.TracerPfd.TracePfd(0L, "PFD IWO {0} Mailbox:{1}: Created new reply template. GUID={2}, MessageClass={3}", new object[]
			{
				23447,
				session.MailboxOwner,
				templateGuid,
				messageClass
			});
			return new ReplyTemplate(session, templateGuid, messageClass, oofReplyType);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000060F8 File Offset: 0x000042F8
		public string PlainTextBody
		{
			get
			{
				string result;
				using (TextReader textReader = this.messageItem.Body.OpenTextReader(BodyFormat.TextPlain))
				{
					result = textReader.ReadToEnd();
				}
				return result;
			}
			set
			{
				using (TextWriter textWriter = this.messageItem.Body.OpenTextWriter(ReplyTemplate.ReplyTemplateBodyConfigurationPlain))
				{
					textWriter.Write(value);
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006140 File Offset: 0x00004340
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000614D File Offset: 0x0000434D
		public string ClassName
		{
			get
			{
				return this.messageItem.ClassName;
			}
			set
			{
				this.messageItem.ClassName = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000615B File Offset: 0x0000435B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00006172 File Offset: 0x00004372
		public OofReplyType OofReplyType
		{
			get
			{
				return (OofReplyType)this.messageItem[MessageItemSchema.OofReplyType];
			}
			set
			{
				this.messageItem[MessageItemSchema.OofReplyType] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000618A File Offset: 0x0000438A
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00006192 File Offset: 0x00004392
		public string CharSet
		{
			get
			{
				return this.charSet;
			}
			set
			{
				this.charSet = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000619C File Offset: 0x0000439C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000061E0 File Offset: 0x000043E0
		public string HtmlBody
		{
			get
			{
				string result;
				using (TextReader textReader = this.messageItem.Body.OpenTextReader(BodyFormat.TextHtml))
				{
					result = textReader.ReadToEnd();
				}
				return result;
			}
			set
			{
				using (TextWriter textWriter = this.messageItem.Body.OpenTextWriter(ReplyTemplate.ReplyTemplateBodyConfigurationHtml))
				{
					if (value != null)
					{
						textWriter.Write(value);
					}
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000622C File Offset: 0x0000442C
		public byte[] EntryId
		{
			get
			{
				return this.messageItem.Id.ObjectId.ProviderLevelItemId;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006243 File Offset: 0x00004443
		public void Dispose()
		{
			if (this.messageItem != null)
			{
				this.messageItem.Dispose();
				this.messageItem = null;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006260 File Offset: 0x00004460
		public void SaveChanges()
		{
			ConflictResolutionResult conflictResolutionResult = this.messageItem.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.messageItem.Id), conflictResolutionResult);
			}
			this.messageItem.Load();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000062A5 File Offset: 0x000044A5
		public void Load()
		{
			this.messageItem.Load();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000062B4 File Offset: 0x000044B4
		private ReplyTemplate(MailboxSession session, Guid templateGuid, string messageClass, OofReplyType oofReplyType)
		{
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Inbox))
			{
				this.messageItem = MessageItem.CreateAssociated(session, folder.Id);
				using (this.messageItem.Body.OpenWriteStream(ReplyTemplate.ReplyTemplateBodyConfigurationPlain))
				{
				}
				object[] propertyValues = new object[]
				{
					templateGuid.ToByteArray(),
					oofReplyType
				};
				this.messageItem.SetProperties(ReplyTemplate.NewPropsArray, propertyValues);
				this.messageItem.ClassName = messageClass;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006368 File Offset: 0x00004568
		private static BodyWriteConfiguration CreateBodyWriteConfiguration(BodyFormat bf)
		{
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(bf, "utf-8");
			bodyWriteConfiguration.SetTargetFormat(bf, "utf-8", BodyCharsetFlags.DisableCharsetDetection);
			return bodyWriteConfiguration;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000638F File Offset: 0x0000458F
		private ReplyTemplate(MessageItem messageItem)
		{
			this.messageItem = messageItem;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000063A0 File Offset: 0x000045A0
		private static ReplyTemplate FindByTemplateGuid(MailboxSession session, RuleAction.OOFReply ruleAction)
		{
			MessageItem messageItem = null;
			try
			{
				using (Folder folder = Folder.Bind(session, DefaultFolderType.Inbox))
				{
					byte[] replyTemplateEntryIdFromTemplateGuid = ReplyTemplate.GetReplyTemplateEntryIdFromTemplateGuid(folder, ruleAction.ReplyTemplateGuid);
					if (replyTemplateEntryIdFromTemplateGuid != null)
					{
						StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(replyTemplateEntryIdFromTemplateGuid);
						messageItem = MessageItem.Bind(session, storeObjectId, new PropertyDefinition[]
						{
							ItemSchema.ReplyTemplateId
						});
						messageItem.OpenAsReadWrite();
						ReplyTemplate.Tracer.TraceDebug<IExchangePrincipal, Guid, byte[]>(0L, "Mailbox:{0}: Found reply template by GUID. GUID={1}, Entry Id={2}", session.MailboxOwner, ruleAction.ReplyTemplateGuid, storeObjectId.GetBytes());
					}
					else
					{
						ReplyTemplate.Tracer.TraceDebug<IExchangePrincipal, Guid>(0L, "Mailbox:{0}: Found no reply template by GUID. GUID={1}", session.MailboxOwner, ruleAction.ReplyTemplateGuid);
						messageItem = null;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				return null;
			}
			if (messageItem != null)
			{
				return new ReplyTemplate(messageItem);
			}
			return null;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006474 File Offset: 0x00004674
		private static byte[] GetReplyTemplateEntryIdFromTemplateGuid(Folder folder, Guid replyTemplateGuid)
		{
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, null, ReplyTemplate.ReplyTemplateProperties))
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				for (;;)
				{
					object[][] rows = queryResult.GetRows(10);
					if (rows.GetLength(0) == 0)
					{
						goto IL_6E;
					}
					foreach (object[] array2 in rows)
					{
						byte[] array3 = array2[1] as byte[];
						if (array3 != null && replyTemplateGuid == new Guid(array3))
						{
							goto Block_5;
						}
					}
				}
				Block_5:
				object[] array2;
				return array2[0] as byte[];
				IL_6E:;
			}
			return null;
		}

		// Token: 0x0400008F RID: 143
		private MessageItem messageItem;

		// Token: 0x04000090 RID: 144
		private string charSet;

		// Token: 0x04000091 RID: 145
		private static readonly PropertyDefinition[] ReplyTemplateProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ReplyTemplateId
		};

		// Token: 0x04000092 RID: 146
		private static readonly PropertyDefinition[] NewPropsArray = new PropertyDefinition[]
		{
			ItemSchema.ReplyTemplateId,
			MessageItemSchema.OofReplyType
		};

		// Token: 0x04000093 RID: 147
		private static readonly Trace Tracer = ExTraceGlobals.OOFTracer;

		// Token: 0x04000094 RID: 148
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000095 RID: 149
		private static readonly BodyWriteConfiguration ReplyTemplateBodyConfigurationPlain = ReplyTemplate.CreateBodyWriteConfiguration(BodyFormat.TextPlain);

		// Token: 0x04000096 RID: 150
		private static readonly BodyWriteConfiguration ReplyTemplateBodyConfigurationHtml = ReplyTemplate.CreateBodyWriteConfiguration(BodyFormat.TextHtml);

		// Token: 0x0200002F RID: 47
		private enum ReplyTemplatePropertyIndex
		{
			// Token: 0x04000098 RID: 152
			EntryId,
			// Token: 0x04000099 RID: 153
			ReplyTemplateId
		}
	}
}
