using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C05 RID: 3077
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RuleWriter : DisposableObject
	{
		// Token: 0x06006DCF RID: 28111 RVA: 0x001D6CA4 File Offset: 0x001D4EA4
		public RuleWriter(MailboxSession session, Folder initialFolder, Stream output)
		{
			this.session = session;
			this.initialFolder = initialFolder;
			this.folders = new List<Folder>(4);
			this.folders.Add(initialFolder);
			this.writer = XmlWriter.Create(output, new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "\t",
				OmitXmlDeclaration = true,
				NewLineOnAttributes = true
			});
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x001D6D1C File Offset: 0x001D4F1C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (Folder folder in this.folders)
				{
					if (folder != null && folder != this.initialFolder)
					{
						folder.Dispose();
					}
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x001D6D84 File Offset: 0x001D4F84
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RuleWriter>(this);
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x001D6D8C File Offset: 0x001D4F8C
		public void WriteRules()
		{
			this.CheckDisposed(null);
			this.writer.WriteStartElement("Mailbox");
			this.writer.WriteAttributeString("Owner", this.session.MailboxOwner.ToString());
			this.DumpReceiveFolders();
			for (int i = 0; i < this.folders.Count; i++)
			{
				Folder folder = this.folders[i];
				this.WriteFolder(folder);
			}
			this.writer.WriteEndElement();
			this.writer.Flush();
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x001D6E18 File Offset: 0x001D5018
		public void WriteAction(RuleAction action)
		{
			this.CheckDisposed(null);
			this.writer.WriteStartElement("Action");
			if (action == null)
			{
				this.writer.WriteAttributeString("Type", ServerStrings.Null);
			}
			else
			{
				this.writer.WriteAttributeString("Type", action.ActionType.ToString());
				this.writer.WriteAttributeString("UserFlags", action.UserFlags.ToString("X08"));
				switch (action.ActionType)
				{
				case RuleAction.Type.OP_MOVE:
				case RuleAction.Type.OP_COPY:
					this.WriteMoveCopyAction((RuleAction.MoveCopy)action);
					break;
				case RuleAction.Type.OP_REPLY:
				{
					RuleAction.Reply reply = (RuleAction.Reply)action;
					this.writer.WriteAttributeString("ActionFlags", reply.Flags.ToString());
					byte[] replyTemplateMessageEntryID = reply.ReplyTemplateMessageEntryID;
					Guid replyTemplateGuid = reply.ReplyTemplateGuid;
					this.WriteReplyAction(replyTemplateMessageEntryID, replyTemplateGuid, true);
					break;
				}
				case RuleAction.Type.OP_OOF_REPLY:
				{
					RuleAction.OOFReply oofreply = (RuleAction.OOFReply)action;
					byte[] replyTemplateMessageEntryID = oofreply.ReplyTemplateMessageEntryID;
					Guid replyTemplateGuid = oofreply.ReplyTemplateGuid;
					this.WriteReplyAction(replyTemplateMessageEntryID, replyTemplateGuid, false);
					break;
				}
				case RuleAction.Type.OP_DEFER_ACTION:
				{
					RuleAction.Defer defer = (RuleAction.Defer)action;
					this.WriteBinaryData("Defer", defer.Data);
					break;
				}
				case RuleAction.Type.OP_BOUNCE:
				{
					RuleAction.Bounce bounce = (RuleAction.Bounce)action;
					this.writer.WriteAttributeString("BounceCode", bounce.Code.ToString());
					break;
				}
				case RuleAction.Type.OP_FORWARD:
				{
					RuleAction.Forward forward = (RuleAction.Forward)action;
					this.writer.WriteAttributeString("ForwardFlags", forward.Flags.ToString());
					this.WriteForwardRecipients(forward.Recipients);
					break;
				}
				case RuleAction.Type.OP_DELEGATE:
				{
					RuleAction.Delegate @delegate = (RuleAction.Delegate)action;
					this.WriteForwardRecipients(@delegate.Recipients);
					break;
				}
				case RuleAction.Type.OP_TAG:
				{
					RuleAction.Tag tag = (RuleAction.Tag)action;
					this.WritePropValue(tag.Value);
					break;
				}
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x001D7018 File Offset: 0x001D5218
		private static string GetAsciiString(IList<byte> bytes, int start, int length)
		{
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = start; i < start + length; i++)
			{
				char c = (char)bytes[i];
				if (c < ' ' || c > '~')
				{
					c = '.';
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x001D7060 File Offset: 0x001D5260
		private static string BytesToString(IList<byte> bytes, int start, int length)
		{
			if (bytes == null || bytes.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Count * 3);
			for (int i = start; i < start + length; i++)
			{
				byte b = bytes[i];
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(b.ToString("X02"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x001D70CD File Offset: 0x001D52CD
		private static string EnumToString(Enum value)
		{
			return string.Format("{0} / {1}", value.ToString(), value.ToString("X"));
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x001D70EC File Offset: 0x001D52EC
		private void DumpReceiveFolders()
		{
			this.writer.WriteStartElement("ReceiveFolders");
			this.DumpReceiveFolder("IPM.Note");
			this.DumpReceiveFolder("IPM.Appointment");
			this.DumpReceiveFolder("IPM.Schedule.Meeting");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Notification");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Request");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Canceled");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Resp.Pos");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Resp.Neg");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Resp.Tent");
			this.DumpReceiveFolder("IPM.Schedule.Meeting.Notification.Forward");
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DD8 RID: 28120 RVA: 0x001D7184 File Offset: 0x001D5384
		private void DumpReceiveFolder(string messageClass)
		{
			string value;
			StoreObjectId receiveFolderId = this.session.GetReceiveFolderId(messageClass, out value);
			try
			{
				string displayName;
				using (Folder folder = Folder.Bind(this.session, receiveFolderId))
				{
					displayName = folder.DisplayName;
				}
				this.writer.WriteStartElement("ReceiveFolder");
				this.writer.WriteAttributeString("MessageClass", messageClass);
				this.writer.WriteAttributeString("FolderName", displayName);
				if (string.Compare("ExplicitClass", messageClass, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.writer.WriteAttributeString("ExplicitClass", value);
				}
				this.writer.WriteEndElement();
			}
			catch (ObjectNotFoundException)
			{
				this.writer.WriteStartElement("ReceiveFolder");
				this.writer.WriteAttributeString("MessageClass", messageClass);
				this.writer.WriteAttributeString("FolderName", ServerStrings.RuleWriterObjectNotFound);
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x001D72C4 File Offset: 0x001D54C4
		private void WriteFolder(Folder folder)
		{
			this.writer.WriteStartElement("Folder");
			if (folder != null)
			{
				Rule[] rules = null;
				Exception exception;
				if (RuleUtil.TryRunStoreCode(delegate
				{
					MapiFolder mapiFolder = folder.MapiFolder;
					rules = mapiFolder.GetRules(new PropTag[0]);
				}, out exception))
				{
					this.writer.WriteAttributeString("FolderName", folder.DisplayName);
					this.writer.WriteAttributeString("Rules", rules.Length.ToString());
					for (int i = 0; i < rules.Length; i++)
					{
						Rule rule = rules[i];
						this.WriteRule(rule, i);
					}
				}
				else
				{
					this.WriteExceptionElement("Getting rules from folder: " + folder.DisplayName, exception);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x001D73B1 File Offset: 0x001D55B1
		private void WriteRule(Rule rule, int index)
		{
			this.writer.WriteStartElement("Rule");
			if (rule != null)
			{
				this.WriteRuleAttributes(rule, index);
				this.WriteRestriction(rule.Condition);
				this.WriteActions(rule.Actions);
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x001D73F4 File Offset: 0x001D55F4
		private void WriteRuleAttributes(Rule rule, int index)
		{
			this.writer.WriteAttributeString("Index", index.ToString());
			this.writer.WriteAttributeString("Name", rule.Name);
			this.writer.WriteAttributeString("Id", ((ulong)rule.ID).ToString());
			this.writer.WriteAttributeString("Provider", rule.Provider);
			this.writer.WriteAttributeString("ExecutionSequence", ((ulong)((long)rule.ExecutionSequence)).ToString());
			this.writer.WriteAttributeString("Level", rule.Level.ToString());
			this.writer.WriteAttributeString("IsExtended", rule.IsExtended.ToString());
			this.writer.WriteAttributeString("StateFlags", rule.StateFlags.ToString());
			this.writer.WriteAttributeString("UserFlags", rule.UserFlags.ToString());
			this.WriteBinaryData("ProviderData", rule.ProviderData);
			if (rule.ExtraProperties != null && rule.ExtraProperties.Length > 0)
			{
				this.writer.WriteStartElement("ExtraProperties");
				foreach (PropValue propValue in rule.ExtraProperties)
				{
					this.WritePropValue(propValue);
				}
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x001D7570 File Offset: 0x001D5770
		private void WriteRestrictions(string elementName, IList<Restriction> restrictions)
		{
			this.writer.WriteStartElement(elementName);
			if (restrictions != null)
			{
				foreach (Restriction restriction in restrictions)
				{
					this.WriteRestriction(restriction);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DDD RID: 28125 RVA: 0x001D75D4 File Offset: 0x001D57D4
		private void WriteRestriction(Restriction restriction)
		{
			this.writer.WriteStartElement("Restriction");
			if (restriction != null)
			{
				Restriction.ResType type = restriction.Type;
				this.writer.WriteAttributeString("Type", type.ToString());
				Restriction.ResType resType = type;
				switch (resType)
				{
				case Restriction.ResType.And:
					this.WriteRestrictions("And", ((Restriction.AndRestriction)restriction).Restrictions);
					break;
				case Restriction.ResType.Or:
					this.WriteRestrictions("Or", ((Restriction.OrRestriction)restriction).Restrictions);
					break;
				case Restriction.ResType.Not:
					this.writer.WriteStartElement("Not");
					this.WriteRestriction(((Restriction.NotRestriction)restriction).Restriction);
					this.writer.WriteEndElement();
					break;
				case Restriction.ResType.Content:
				{
					Restriction.ContentRestriction contentRestriction = (Restriction.ContentRestriction)restriction;
					this.writer.WriteAttributeString("ContentFlags", RuleWriter.EnumToString(contentRestriction.Flags));
					this.writer.WriteAttributeString("PropTag", RuleWriter.EnumToString(contentRestriction.PropTag));
					this.writer.WriteAttributeString("MultiValued", contentRestriction.MultiValued.ToString());
					this.WritePropValue(contentRestriction.PropValue);
					break;
				}
				case Restriction.ResType.Property:
				{
					Restriction.PropertyRestriction propertyRestriction = (Restriction.PropertyRestriction)restriction;
					this.writer.WriteAttributeString("Operation", propertyRestriction.Op.ToString());
					this.writer.WriteAttributeString("PropTag", RuleWriter.EnumToString(propertyRestriction.PropTag));
					this.writer.WriteAttributeString("MultiValued", propertyRestriction.MultiValued.ToString());
					this.WritePropValue(propertyRestriction.PropValue);
					break;
				}
				case Restriction.ResType.CompareProps:
				{
					Restriction.ComparePropertyRestriction comparePropertyRestriction = (Restriction.ComparePropertyRestriction)restriction;
					this.writer.WriteAttributeString("Operation", comparePropertyRestriction.Op.ToString());
					this.writer.WriteAttributeString("PropTagLeft", RuleWriter.EnumToString(comparePropertyRestriction.TagLeft));
					this.writer.WriteAttributeString("PropTagRight", RuleWriter.EnumToString(comparePropertyRestriction.TagRight));
					break;
				}
				case Restriction.ResType.BitMask:
				{
					Restriction.BitMaskRestriction bitMaskRestriction = (Restriction.BitMaskRestriction)restriction;
					this.writer.WriteAttributeString("PropTag", RuleWriter.EnumToString(bitMaskRestriction.Tag));
					this.writer.WriteAttributeString("Operation", bitMaskRestriction.Bmr.ToString());
					this.writer.WriteAttributeString("Mask", bitMaskRestriction.Mask.ToString("X08"));
					break;
				}
				case Restriction.ResType.Size:
				{
					Restriction.SizeRestriction sizeRestriction = (Restriction.SizeRestriction)restriction;
					this.writer.WriteAttributeString("Operation", sizeRestriction.Op.ToString());
					this.writer.WriteAttributeString("PropTag", RuleWriter.EnumToString(sizeRestriction.Tag));
					this.writer.WriteAttributeString("Size", sizeRestriction.Size.ToString("X08"));
					break;
				}
				case Restriction.ResType.Exist:
				{
					Restriction.ExistRestriction existRestriction = (Restriction.ExistRestriction)restriction;
					this.writer.WriteAttributeString("PropTag", RuleWriter.EnumToString(existRestriction.Tag));
					break;
				}
				case Restriction.ResType.SubRestriction:
				{
					Restriction.SubRestriction subRestriction = (Restriction.SubRestriction)restriction;
					this.writer.WriteAttributeString("SubType", subRestriction.GetType().Name);
					this.WriteRestriction(subRestriction.Restriction);
					break;
				}
				case Restriction.ResType.Comment:
				{
					Restriction.CommentRestriction commentRestriction = (Restriction.CommentRestriction)restriction;
					this.WritePropValues("Comment", commentRestriction.Values);
					this.WriteRestriction(commentRestriction.Restriction);
					break;
				}
				case Restriction.ResType.Count:
				{
					Restriction.CountRestriction countRestriction = (Restriction.CountRestriction)restriction;
					this.writer.WriteAttributeString("Count", countRestriction.Count.ToString());
					this.WriteRestriction(countRestriction.Restriction);
					break;
				}
				default:
					switch (resType)
					{
					}
					this.writer.WriteAttributeString("Error", "Unsupported restriction type: " + type.ToString());
					break;
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x001D7A0C File Offset: 0x001D5C0C
		private void WriteActions(IList<RuleAction> actions)
		{
			this.writer.WriteStartElement("Actions");
			if (actions != null)
			{
				foreach (RuleAction action in actions)
				{
					this.WriteAction(action);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x001D7B68 File Offset: 0x001D5D68
		private void WriteMoveCopyAction(RuleAction.MoveCopy moveCopy)
		{
			Exception exception;
			if (!RuleUtil.TryRunStoreCode(delegate
			{
				StoreObjectId folderId = StoreObjectId.FromProviderSpecificId(moveCopy.FolderEntryID, StoreObjectType.Folder);
				try
				{
					Folder folder = this.folders.FirstOrDefault((Folder candidate) => candidate.StoreObjectId.Equals(folderId));
					if (folder == null)
					{
						folder = Folder.Bind(this.session, folderId);
						this.folders.Add(folder);
					}
					this.writer.WriteAttributeString("FolderName", folder.DisplayName);
				}
				catch (ObjectNotFoundException)
				{
					this.writer.WriteAttributeString("FolderName", ServerStrings.RuleWriterObjectNotFound);
				}
			}, out exception))
			{
				this.WriteExceptionElement("Opening target folder for rule action.", exception);
			}
			this.WriteBinaryData("FolderEntry", moveCopy.FolderEntryID);
			this.WriteBinaryData("StoreEntry", moveCopy.StoreEntryID);
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x001D7D0C File Offset: 0x001D5F0C
		private void WriteReplyAction(byte[] replyTemplateMessageId, Guid replyGuid, bool includeSubject)
		{
			this.writer.WriteAttributeString("ReplyTemplateGuid", replyGuid.ToString());
			Exception exception;
			if (!RuleUtil.TryRunStoreCode(delegate
			{
				try
				{
					StoreId itemId = StoreObjectId.FromProviderSpecificId(replyTemplateMessageId);
					using (MessageItem messageItem = Item.BindAsMessage(this.session, itemId, StoreObjectSchema.ContentConversionProperties))
					{
						if (includeSubject)
						{
							this.writer.WriteAttributeString("ReplySubject", messageItem.Subject);
						}
						using (TextReader textReader = messageItem.Body.OpenTextReader(BodyFormat.TextPlain))
						{
							string text = textReader.ReadToEnd();
							this.writer.WriteStartElement("ReplyBody");
							this.writer.WriteString(Environment.NewLine);
							this.writer.WriteString(text);
							this.writer.WriteString(Environment.NewLine);
							this.writer.WriteEndElement();
						}
					}
				}
				catch (ObjectNotFoundException)
				{
					this.writer.WriteAttributeString("ReplySubject", ServerStrings.RuleWriterObjectNotFound);
				}
			}, out exception))
			{
				this.WriteExceptionElement("Opening reply template message", exception);
			}
			this.WriteBinaryData("ReplyTemplateMessageId", replyTemplateMessageId);
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x001D7D84 File Offset: 0x001D5F84
		private void WriteForwardRecipients(IList<AdrEntry> recipients)
		{
			this.writer.WriteStartElement("Recipients");
			if (recipients != null)
			{
				foreach (AdrEntry adrEntry in recipients)
				{
					this.WritePropValues("Recipient", adrEntry.Values);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x001D7DF4 File Offset: 0x001D5FF4
		private void WritePropValues(string xmlName, IList<PropValue> propValues)
		{
			this.writer.WriteStartElement(xmlName);
			if (propValues != null)
			{
				foreach (PropValue propValue in propValues)
				{
					this.WritePropValue(propValue);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x001D7E58 File Offset: 0x001D6058
		private void WritePropValue(PropValue propValue)
		{
			this.writer.WriteStartElement("Property");
			this.writer.WriteAttributeString("Id", RuleWriter.EnumToString(propValue.PropTag));
			this.writer.WriteAttributeString("DataType", RuleWriter.EnumToString(propValue.PropType));
			object obj = propValue.Value ?? "(null)";
			string text = obj as string;
			byte[] array = obj as byte[];
			if (text != null)
			{
				this.writer.WriteElementString("Value", text);
			}
			else if (array != null)
			{
				this.WriteBinaryData("Value", array);
			}
			else
			{
				if (obj is IEnumerable)
				{
					using (IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							string value = (obj2 == null) ? "(null)" : obj2.ToString();
							this.writer.WriteElementString("Value", value);
						}
						goto IL_114;
					}
				}
				this.writer.WriteElementString("Value", obj.ToString());
			}
			IL_114:
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x001D7F94 File Offset: 0x001D6194
		private void WriteBinaryData(string elementName, byte[] bytes)
		{
			this.writer.WriteStartElement(elementName);
			if (bytes != null && bytes.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Environment.NewLine);
				for (int i = 0; i < bytes.Length; i += 16)
				{
					int num = bytes.Length - i;
					int length = (num > 16) ? 16 : num;
					string value = this.WriteBinaryDataRow(bytes, i, length);
					stringBuilder.Append(value);
				}
				this.writer.WriteCData(stringBuilder.ToString());
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x001D8018 File Offset: 0x001D6218
		private string WriteBinaryDataRow(byte[] bytes, int start, int length)
		{
			string text = start.ToString("X08");
			string text2 = RuleWriter.BytesToString(bytes, start, length);
			string asciiString = RuleWriter.GetAsciiString(bytes, start, length);
			return string.Format("{0} {1,-50} |{2,-16}|{3}", new object[]
			{
				text,
				text2,
				asciiString,
				Environment.NewLine
			});
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x001D8074 File Offset: 0x001D6274
		private void WriteExceptionElement(string action, Exception exception)
		{
			this.writer.WriteStartElement("Exception");
			this.writer.WriteAttributeString("AttemptedAction", action);
			this.writer.WriteAttributeString("ExceptionType", exception.GetType().FullName);
			this.writer.WriteAttributeString("ExceptionMessage", exception.Message);
			this.writer.WriteCData(exception.StackTrace);
			this.writer.WriteEndElement();
		}

		// Token: 0x04003EA9 RID: 16041
		private readonly MailboxSession session;

		// Token: 0x04003EAA RID: 16042
		private readonly Folder initialFolder;

		// Token: 0x04003EAB RID: 16043
		private readonly XmlWriter writer;

		// Token: 0x04003EAC RID: 16044
		private readonly List<Folder> folders = new List<Folder>();
	}
}
