using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200092A RID: 2346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VotingInfo
	{
		// Token: 0x060057A7 RID: 22439 RVA: 0x001684B8 File Offset: 0x001666B8
		internal VotingInfo(MessageItem messageItem)
		{
			this.messageItem = messageItem;
			byte[] largeBinaryProperty = this.messageItem.PropertyBag.GetLargeBinaryProperty(InternalSchema.OutlookUserPropsVerbStream);
			if (largeBinaryProperty != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(largeBinaryProperty))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						this.Read(binaryReader);
					}
				}
			}
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x0016854C File Offset: 0x0016674C
		private void InitializeDefaults()
		{
			if (this.defaultOptions.Count == 0)
			{
				this.defaultOptions.Add(VotingInfo.VotingOption.ReplyOption);
				this.defaultOptions.Add(VotingInfo.VotingOption.ReplyAllOption);
				this.defaultOptions.Add(VotingInfo.VotingOption.ForwardOption);
				this.defaultOptions.Add(VotingInfo.VotingOption.ReplyToFolderOption);
			}
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x001685A8 File Offset: 0x001667A8
		public IList<string> GetOptionsList()
		{
			string[] array = new string[this.userOptions.Count];
			for (int i = 0; i < this.userOptions.Count; i++)
			{
				array[i] = this.userOptions[i].DisplayName;
			}
			return array;
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x001685F4 File Offset: 0x001667F4
		public IList<VotingInfo.OptionData> GetOptionsDataList()
		{
			VotingInfo.OptionData[] array = new VotingInfo.OptionData[this.userOptions.Count];
			for (int i = 0; i < this.userOptions.Count; i++)
			{
				array[i] = this.userOptions[i].OptionData;
			}
			return array;
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x00168648 File Offset: 0x00166848
		public void AddOption(string optionDisplayName)
		{
			VotingInfo.OptionData data;
			data.DisplayName = optionDisplayName;
			data.SendPrompt = VotingInfo.SendPrompt.VotingOption;
			this.AddOption(data);
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x0016866C File Offset: 0x0016686C
		public void AddOption(VotingInfo.OptionData data)
		{
			this.InitializeDefaults();
			if (this.userOptions.Count == 31)
			{
				throw new ArgumentException("Can't add more options", data.DisplayName);
			}
			this.userOptions.Add(new VotingInfo.VotingOption(data, this.userOptions.Count + 1));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.Write(binaryWriter);
				}
				this.messageItem[InternalSchema.OutlookUserPropsVerbStream] = memoryStream.ToArray();
				this.messageItem[InternalSchema.IsReplyRequested] = true;
				this.messageItem[InternalSchema.IsResponseRequested] = true;
			}
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x00168748 File Offset: 0x00166948
		public VersionedId GetCorrelatedItem(StoreId folderId)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			VersionedId correlatedItem;
			using (Folder folder = Folder.Bind(this.messageItem.Session, folderId))
			{
				correlatedItem = this.GetCorrelatedItem(folder);
			}
			return correlatedItem;
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x00168798 File Offset: 0x00166998
		public VersionedId GetCorrelatedItem(Folder folder)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			try
			{
				byte[] messageCorrelationBlob = this.MessageCorrelationBlob;
				if (messageCorrelationBlob != null)
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
					{
						new SortBy(InternalSchema.ReportTag, SortOrder.Ascending)
					}, new PropertyDefinition[]
					{
						InternalSchema.ItemId,
						InternalSchema.ReportTag
					}))
					{
						queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ReportTag, messageCorrelationBlob));
						object[][] rows = queryResult.GetRows(1);
						if (rows.Length == 1)
						{
							byte[] array = rows[0][1] as byte[];
							if (array != null && ArrayComparer<byte>.Comparer.Equals(messageCorrelationBlob, array))
							{
								return rows[0][0] as VersionedId;
							}
						}
					}
				}
			}
			catch (CorruptDataException)
			{
			}
			return null;
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x060057AF RID: 22447 RVA: 0x00168878 File Offset: 0x00166A78
		// (set) Token: 0x060057B0 RID: 22448 RVA: 0x001688D5 File Offset: 0x00166AD5
		public byte[] MessageCorrelationBlob
		{
			get
			{
				object obj = this.messageItem.TryGetProperty(InternalSchema.ReportTag);
				PropertyError propertyError = obj as PropertyError;
				if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed)
				{
					throw new CorruptDataException(ServerStrings.ExCorruptMessageCorrelationBlob);
				}
				byte[] array = obj as byte[];
				if (array != null && array.Length > 250)
				{
					throw new CorruptDataException(ServerStrings.ExCorruptMessageCorrelationBlob);
				}
				return array;
			}
			set
			{
				Util.ThrowOnNullArgument(value, "MessageCorrelationBlob");
				if (value.Length == 0 || value.Length > 250)
				{
					throw new ArgumentException("MessageCorrelationBlob.Length > 0 && < 250");
				}
				this.messageItem.SafeSetProperty(InternalSchema.ReportTag, value);
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x060057B1 RID: 22449 RVA: 0x0016890D File Offset: 0x00166B0D
		public string Response
		{
			get
			{
				return this.messageItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.VotingResponse);
			}
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x00168924 File Offset: 0x00166B24
		private void Read(BinaryReader reader)
		{
			try
			{
				List<VotingInfo.VotingOption> list = new List<VotingInfo.VotingOption>();
				VotingInfo.VotingBlobVersion votingBlobVersion = (VotingInfo.VotingBlobVersion)reader.ReadUInt16();
				if (votingBlobVersion != VotingInfo.VotingBlobVersion.AsciiBlob)
				{
					throw new CorruptDataException(ServerStrings.VotingDataCorrupt);
				}
				for (int i = reader.ReadInt32(); i > 0; i--)
				{
					VotingInfo.VotingOption item = VotingInfo.VotingOption.ReadVersion102(reader);
					list.Add(item);
				}
				if (reader.BaseStream.Position < reader.BaseStream.Length)
				{
					votingBlobVersion = (VotingInfo.VotingBlobVersion)reader.ReadUInt16();
					if (votingBlobVersion >= VotingInfo.VotingBlobVersion.UnicodeBlob)
					{
						for (int j = 0; j < list.Count; j++)
						{
							list[j].ReadVersion104(reader);
							if (list[j].IsDefaultOption)
							{
								this.defaultOptions.Add(list[j]);
							}
							else
							{
								this.userOptions.Add(list[j]);
							}
						}
					}
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExVotingBlobCorrupt, innerException);
			}
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x00168A14 File Offset: 0x00166C14
		private void Write(BinaryWriter writer)
		{
			writer.Write(258);
			writer.Write(this.defaultOptions.Count + this.userOptions.Count);
			for (int i = 0; i < this.defaultOptions.Count; i++)
			{
				this.defaultOptions[i].WriteVersion102(writer);
			}
			for (int j = 0; j < this.userOptions.Count; j++)
			{
				this.userOptions[j].WriteVersion102(writer);
			}
			writer.Write(260);
			for (int k = 0; k < this.defaultOptions.Count; k++)
			{
				this.defaultOptions[k].WriteVersion104(writer);
			}
			for (int l = 0; l < this.userOptions.Count; l++)
			{
				this.userOptions[l].WriteVersion104(writer);
			}
		}

		// Token: 0x04002EC0 RID: 11968
		private List<VotingInfo.VotingOption> defaultOptions = new List<VotingInfo.VotingOption>();

		// Token: 0x04002EC1 RID: 11969
		private List<VotingInfo.VotingOption> userOptions = new List<VotingInfo.VotingOption>();

		// Token: 0x04002EC2 RID: 11970
		private MessageItem messageItem;

		// Token: 0x0200092B RID: 2347
		[Flags]
		private enum ReplyStyle
		{
			// Token: 0x04002EC4 RID: 11972
			Omit = 0,
			// Token: 0x04002EC5 RID: 11973
			Embed = 1,
			// Token: 0x04002EC6 RID: 11974
			Include = 2,
			// Token: 0x04002EC7 RID: 11975
			Indent = 4
		}

		// Token: 0x0200092C RID: 2348
		private enum VotingBlobVersion : ushort
		{
			// Token: 0x04002EC9 RID: 11977
			AsciiBlob = 258,
			// Token: 0x04002ECA RID: 11978
			UnicodeBlob = 260
		}

		// Token: 0x0200092D RID: 2349
		private enum UIVerb
		{
			// Token: 0x04002ECC RID: 11980
			SendPrompt = 2
		}

		// Token: 0x0200092E RID: 2350
		private enum ShowVerb
		{
			// Token: 0x04002ECE RID: 11982
			MenuAndToolbar = 2
		}

		// Token: 0x0200092F RID: 2351
		private enum PropertyCopyLike
		{
			// Token: 0x04002ED0 RID: 11984
			Reply,
			// Token: 0x04002ED1 RID: 11985
			ReplyAll,
			// Token: 0x04002ED2 RID: 11986
			Forward,
			// Token: 0x04002ED3 RID: 11987
			ReplyToFolder,
			// Token: 0x04002ED4 RID: 11988
			VotingButton
		}

		// Token: 0x02000930 RID: 2352
		public enum SendPrompt
		{
			// Token: 0x04002ED6 RID: 11990
			None,
			// Token: 0x04002ED7 RID: 11991
			Send,
			// Token: 0x04002ED8 RID: 11992
			VotingOption
		}

		// Token: 0x02000931 RID: 2353
		public struct OptionData
		{
			// Token: 0x04002ED9 RID: 11993
			public string DisplayName;

			// Token: 0x04002EDA RID: 11994
			public VotingInfo.SendPrompt SendPrompt;
		}

		// Token: 0x02000932 RID: 2354
		private class VotingOption
		{
			// Token: 0x060057B4 RID: 22452 RVA: 0x00168AF4 File Offset: 0x00166CF4
			internal VotingOption(VotingInfo.OptionData data, int verbId) : this(data.DisplayName, verbId)
			{
				this.sendPrompt = data.SendPrompt;
			}

			// Token: 0x060057B5 RID: 22453 RVA: 0x00168B14 File Offset: 0x00166D14
			internal VotingOption(string votingOption, int verbId)
			{
				Util.ThrowOnNullArgument(votingOption, "votingOption");
				if (votingOption.Length == 0)
				{
					throw new ArgumentException("invalid argument", "votingOption");
				}
				this.displayName = votingOption;
				this.actionPrefix = this.displayName;
				this.actionMessageClass = "IPM.Note";
				this.actionFormName = string.Empty;
				this.actionTemplateName = string.Empty;
				this.propertyCopyLike = VotingInfo.PropertyCopyLike.VotingButton;
				this.replyStyle = VotingInfo.ReplyStyle.Omit;
				this.showVerb = VotingInfo.ShowVerb.MenuAndToolbar;
				this.enabled = true;
				this.sendPrompt = VotingInfo.SendPrompt.VotingOption;
				this.verbId = (LastAction)verbId;
				this.verbPosition = -1;
			}

			// Token: 0x060057B6 RID: 22454 RVA: 0x00168BAF File Offset: 0x00166DAF
			private VotingOption()
			{
			}

			// Token: 0x060057B7 RID: 22455 RVA: 0x00168BB8 File Offset: 0x00166DB8
			private static string ReadString(BinaryReader reader, bool unicode)
			{
				ushort num = (ushort)reader.ReadByte();
				if (num == 255)
				{
					num = reader.ReadUInt16();
				}
				if (unicode)
				{
					byte[] array = reader.ReadBytes((int)(num * 2));
					return VotingInfo.VotingOption.unicodeEncoder.GetString(array, 0, array.Length);
				}
				byte[] array2 = reader.ReadBytes((int)num);
				return VotingInfo.VotingOption.asciiEncoder.GetString(array2, 0, array2.Length);
			}

			// Token: 0x060057B8 RID: 22456 RVA: 0x00168C10 File Offset: 0x00166E10
			private static void WriteString(BinaryWriter writer, string str, bool unicode)
			{
				if (str.Length >= 255)
				{
					writer.Write(byte.MaxValue);
					writer.Write((ushort)str.Length);
				}
				else
				{
					writer.Write((byte)str.Length);
				}
				if (unicode)
				{
					writer.Write(VotingInfo.VotingOption.unicodeEncoder.GetBytes(str));
					return;
				}
				writer.Write(VotingInfo.VotingOption.asciiEncoder.GetBytes(str));
			}

			// Token: 0x060057B9 RID: 22457 RVA: 0x00168C78 File Offset: 0x00166E78
			internal static VotingInfo.VotingOption ReadVersion102(BinaryReader reader)
			{
				return new VotingInfo.VotingOption
				{
					propertyCopyLike = (VotingInfo.PropertyCopyLike)reader.ReadUInt32(),
					displayName = VotingInfo.VotingOption.ReadString(reader, false),
					actionMessageClass = VotingInfo.VotingOption.ReadString(reader, false),
					actionFormName = VotingInfo.VotingOption.ReadString(reader, false),
					actionPrefix = VotingInfo.VotingOption.ReadString(reader, false),
					replyStyle = (VotingInfo.ReplyStyle)reader.ReadUInt32(),
					actionTemplateName = VotingInfo.VotingOption.ReadString(reader, false),
					usePrefixHeader = (reader.ReadUInt32() != 0U),
					enabled = (reader.ReadUInt32() != 0U),
					sendPrompt = (VotingInfo.SendPrompt)reader.ReadUInt32(),
					showVerb = (VotingInfo.ShowVerb)reader.ReadInt32(),
					verbId = (LastAction)reader.ReadUInt32(),
					verbPosition = reader.ReadInt32()
				};
			}

			// Token: 0x060057BA RID: 22458 RVA: 0x00168D39 File Offset: 0x00166F39
			internal void ReadVersion104(BinaryReader reader)
			{
				this.displayName = VotingInfo.VotingOption.ReadString(reader, true);
				this.actionPrefix = VotingInfo.VotingOption.ReadString(reader, true);
			}

			// Token: 0x060057BB RID: 22459 RVA: 0x00168D58 File Offset: 0x00166F58
			internal void WriteVersion102(BinaryWriter writer)
			{
				writer.Write((int)this.propertyCopyLike);
				VotingInfo.VotingOption.WriteString(writer, this.displayName, false);
				VotingInfo.VotingOption.WriteString(writer, this.actionMessageClass, false);
				VotingInfo.VotingOption.WriteString(writer, this.actionFormName, false);
				VotingInfo.VotingOption.WriteString(writer, this.actionPrefix, false);
				writer.Write((int)this.replyStyle);
				VotingInfo.VotingOption.WriteString(writer, this.actionTemplateName, false);
				writer.Write(this.usePrefixHeader ? 1U : 0U);
				writer.Write(this.enabled ? 1U : 0U);
				writer.Write((int)this.sendPrompt);
				writer.Write((int)this.showVerb);
				writer.Write((int)this.verbId);
				writer.Write(this.verbPosition);
			}

			// Token: 0x060057BC RID: 22460 RVA: 0x00168E12 File Offset: 0x00167012
			internal void WriteVersion104(BinaryWriter writer)
			{
				VotingInfo.VotingOption.WriteString(writer, this.displayName, true);
				VotingInfo.VotingOption.WriteString(writer, this.actionPrefix, true);
			}

			// Token: 0x17001854 RID: 6228
			// (get) Token: 0x060057BD RID: 22461 RVA: 0x00168E2E File Offset: 0x0016702E
			public string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x17001855 RID: 6229
			// (get) Token: 0x060057BE RID: 22462 RVA: 0x00168E38 File Offset: 0x00167038
			public VotingInfo.OptionData OptionData
			{
				get
				{
					VotingInfo.OptionData result;
					result.SendPrompt = this.sendPrompt;
					result.DisplayName = this.displayName;
					return result;
				}
			}

			// Token: 0x17001856 RID: 6230
			// (get) Token: 0x060057BF RID: 22463 RVA: 0x00168E60 File Offset: 0x00167060
			internal bool IsDefaultOption
			{
				get
				{
					return this.verbId == LastAction.ReplyToSender || this.verbId == LastAction.ReplyToAll || this.verbId == LastAction.Forward || this.verbId == LastAction.ReplyToFolder;
				}
			}

			// Token: 0x060057C0 RID: 22464 RVA: 0x00168E8C File Offset: 0x0016708C
			private static VotingInfo.VotingOption GetReplyOption()
			{
				return new VotingInfo.VotingOption("Reply", 0)
				{
					propertyCopyLike = VotingInfo.PropertyCopyLike.Reply,
					actionPrefix = "RE",
					actionMessageClass = "IPM.Note",
					actionFormName = "Message",
					replyStyle = (VotingInfo.ReplyStyle.Embed | VotingInfo.ReplyStyle.Indent),
					usePrefixHeader = false,
					enabled = true,
					sendPrompt = VotingInfo.SendPrompt.None,
					showVerb = VotingInfo.ShowVerb.MenuAndToolbar,
					verbId = LastAction.ReplyToSender,
					verbPosition = 2
				};
			}

			// Token: 0x060057C1 RID: 22465 RVA: 0x00168F00 File Offset: 0x00167100
			private static VotingInfo.VotingOption GetReplyAllOption()
			{
				return new VotingInfo.VotingOption("Reply to All", 0)
				{
					propertyCopyLike = VotingInfo.PropertyCopyLike.ReplyAll,
					actionPrefix = "RE",
					actionMessageClass = "IPM.Note",
					actionFormName = "Message",
					replyStyle = (VotingInfo.ReplyStyle.Embed | VotingInfo.ReplyStyle.Indent),
					usePrefixHeader = false,
					enabled = true,
					sendPrompt = VotingInfo.SendPrompt.None,
					showVerb = VotingInfo.ShowVerb.MenuAndToolbar,
					verbId = LastAction.ReplyToAll,
					verbPosition = 3
				};
			}

			// Token: 0x060057C2 RID: 22466 RVA: 0x00168F74 File Offset: 0x00167174
			private static VotingInfo.VotingOption GetForwardOption()
			{
				return new VotingInfo.VotingOption("Forward", 0)
				{
					propertyCopyLike = VotingInfo.PropertyCopyLike.Forward,
					actionPrefix = "FW",
					actionMessageClass = "IPM.Note",
					actionFormName = "Message",
					replyStyle = (VotingInfo.ReplyStyle.Embed | VotingInfo.ReplyStyle.Indent),
					usePrefixHeader = false,
					enabled = true,
					sendPrompt = VotingInfo.SendPrompt.None,
					showVerb = VotingInfo.ShowVerb.MenuAndToolbar,
					verbId = LastAction.Forward,
					verbPosition = 4
				};
			}

			// Token: 0x060057C3 RID: 22467 RVA: 0x00168FE8 File Offset: 0x001671E8
			private static VotingInfo.VotingOption GetReplyToFolderOption()
			{
				return new VotingInfo.VotingOption("Reply to Folder", 0)
				{
					propertyCopyLike = VotingInfo.PropertyCopyLike.ReplyToFolder,
					actionPrefix = string.Empty,
					actionMessageClass = "IPM.Post",
					actionFormName = "Post",
					replyStyle = (VotingInfo.ReplyStyle.Embed | VotingInfo.ReplyStyle.Indent),
					usePrefixHeader = false,
					enabled = true,
					sendPrompt = VotingInfo.SendPrompt.None,
					showVerb = VotingInfo.ShowVerb.MenuAndToolbar,
					verbId = LastAction.ReplyToFolder,
					verbPosition = 8
				};
			}

			// Token: 0x04002EDB RID: 11995
			private static readonly UnicodeEncoding unicodeEncoder = new UnicodeEncoding();

			// Token: 0x04002EDC RID: 11996
			private static readonly Encoding asciiEncoder = CTSGlobals.AsciiEncoding;

			// Token: 0x04002EDD RID: 11997
			private string displayName;

			// Token: 0x04002EDE RID: 11998
			private string actionPrefix;

			// Token: 0x04002EDF RID: 11999
			private string actionMessageClass;

			// Token: 0x04002EE0 RID: 12000
			private string actionFormName;

			// Token: 0x04002EE1 RID: 12001
			private string actionTemplateName;

			// Token: 0x04002EE2 RID: 12002
			private VotingInfo.ReplyStyle replyStyle;

			// Token: 0x04002EE3 RID: 12003
			private VotingInfo.ShowVerb showVerb;

			// Token: 0x04002EE4 RID: 12004
			private VotingInfo.PropertyCopyLike propertyCopyLike;

			// Token: 0x04002EE5 RID: 12005
			private bool usePrefixHeader;

			// Token: 0x04002EE6 RID: 12006
			private bool enabled;

			// Token: 0x04002EE7 RID: 12007
			private VotingInfo.SendPrompt sendPrompt;

			// Token: 0x04002EE8 RID: 12008
			private LastAction verbId;

			// Token: 0x04002EE9 RID: 12009
			private int verbPosition;

			// Token: 0x04002EEA RID: 12010
			internal static readonly VotingInfo.VotingOption ReplyOption = VotingInfo.VotingOption.GetReplyOption();

			// Token: 0x04002EEB RID: 12011
			internal static readonly VotingInfo.VotingOption ReplyAllOption = VotingInfo.VotingOption.GetReplyAllOption();

			// Token: 0x04002EEC RID: 12012
			internal static readonly VotingInfo.VotingOption ForwardOption = VotingInfo.VotingOption.GetForwardOption();

			// Token: 0x04002EED RID: 12013
			internal static readonly VotingInfo.VotingOption ReplyToFolderOption = VotingInfo.VotingOption.GetReplyToFolderOption();
		}
	}
}
