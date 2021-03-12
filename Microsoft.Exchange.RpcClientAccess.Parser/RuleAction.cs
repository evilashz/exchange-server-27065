using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000378 RID: 888
	internal abstract class RuleAction
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x00037D08 File Offset: 0x00035F08
		protected RuleAction(RuleActionType type, uint flavor, uint userFlags)
		{
			this.ActionType = type;
			this.Flavor = flavor;
			this.UserFlags = userFlags;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00037D28 File Offset: 0x00035F28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{").Append(this.ActionType);
			stringBuilder.Append(", flavor=").Append(this.Flavor);
			stringBuilder.Append(", flags=0x").AppendFormat("{0:X}", this.UserFlags);
			string value = this.InternalToString();
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append(", ").Append(value);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00037DC4 File Offset: 0x00035FC4
		protected static string ByteArrayToString(byte[] buffer)
		{
			if (buffer != null)
			{
				StringBuilder stringBuilder = new StringBuilder(buffer.Length * 2);
				foreach (byte b in buffer)
				{
					stringBuilder.Append(RuleAction.HexDigits[(int)(b / 16)]);
					stringBuilder.Append(RuleAction.HexDigits[(int)(b % 16)]);
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00037E21 File Offset: 0x00036021
		protected virtual string InternalToString()
		{
			return null;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00037E24 File Offset: 0x00036024
		internal static RuleAction[] Parse(Reader reader)
		{
			RuleAction.currentDepth += 1U;
			RuleAction[] result;
			try
			{
				if (256U < RuleAction.currentDepth)
				{
					throw new BufferParseException("Action depth is greater than the maximum depth allowed.");
				}
				uint num = (uint)reader.ReadUInt16();
				reader.CheckBoundary(num, 11U);
				RuleAction[] array = new RuleAction[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					array[(int)((UIntPtr)num2)] = RuleAction.ParseOneAction(reader);
				}
				result = array;
			}
			finally
			{
				RuleAction.currentDepth -= 1U;
			}
			return result;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00037EA4 File Offset: 0x000360A4
		internal virtual void Serialize(Writer writer, Encoding string8Encoding)
		{
			writer.WriteByte((byte)this.ActionType);
			writer.WriteUInt32(this.Flavor);
			writer.WriteUInt32(this.UserFlags);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00037ECA File Offset: 0x000360CA
		internal virtual void ResolveString8Values(Encoding string8Encoding)
		{
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00037ECC File Offset: 0x000360CC
		private static RuleAction ParseOneAction(Reader reader)
		{
			RuleAction.currentDepth += 1U;
			RuleAction result;
			try
			{
				if (256U < RuleAction.currentDepth)
				{
					throw new BufferParseException("Action depth is greater than the maximum depth allowed.");
				}
				reader.CheckBoundary(1U, 11U);
				uint num = (uint)reader.ReadUInt16();
				reader.CheckBoundary(num, 1U);
				RuleActionType ruleActionType = (RuleActionType)reader.ReadByte();
				uint flavor = reader.ReadUInt32();
				uint flags = reader.ReadUInt32();
				switch (ruleActionType)
				{
				case RuleActionType.Move:
					result = RuleAction.MoveAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Copy:
					result = RuleAction.CopyAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Reply:
					result = RuleAction.ReplyAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.OutOfOfficeReply:
					result = RuleAction.OutOfOfficeReplyAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.DeferAction:
					result = RuleAction.DeferAction.Parse(reader, flavor, flags, (ulong)(num - 9U));
					break;
				case RuleActionType.Bounce:
					result = RuleAction.BounceAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Forward:
					result = RuleAction.ForwardAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Delegate:
					result = RuleAction.DelegateAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Tag:
					result = RuleAction.TagAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.Delete:
					result = RuleAction.DeleteAction.Parse(reader, flavor, flags);
					break;
				case RuleActionType.MarkAsRead:
					result = RuleAction.MarkAsReadAction.Parse(reader, flavor, flags);
					break;
				default:
					throw new BufferParseException(string.Format("Invalid action type {0}.", (byte)ruleActionType));
				}
			}
			finally
			{
				RuleAction.currentDepth -= 1U;
			}
			return result;
		}

		// Token: 0x04000B48 RID: 2888
		private const int MaximumDepth = 256;

		// Token: 0x04000B49 RID: 2889
		private const uint MinimumActionSize = 11U;

		// Token: 0x04000B4A RID: 2890
		private static readonly char[] HexDigits = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04000B4B RID: 2891
		[ThreadStatic]
		private static uint currentDepth = 0U;

		// Token: 0x04000B4C RID: 2892
		public readonly RuleActionType ActionType;

		// Token: 0x04000B4D RID: 2893
		public readonly uint Flavor;

		// Token: 0x04000B4E RID: 2894
		public readonly uint UserFlags;

		// Token: 0x02000379 RID: 889
		internal abstract class MoveCopyActionBase : RuleAction
		{
			// Token: 0x060015A8 RID: 5544 RVA: 0x00038080 File Offset: 0x00036280
			protected MoveCopyActionBase(RuleActionType type, uint flavor, uint flags, bool folderIsInThisStore, byte[] storeEntryId, byte[] folderId) : base(type, flavor, flags)
			{
				if (!folderIsInThisStore)
				{
					Util.ThrowOnNullArgument(storeEntryId, "storeEntryId");
				}
				Util.ThrowOnNullArgument(folderId, "folderId");
				this.FolderIsInThisStore = folderIsInThisStore;
				this.DestinationStoreEntryId = (folderIsInThisStore ? null : storeEntryId);
				this.DestinationFolderId = folderId;
			}

			// Token: 0x060015A9 RID: 5545 RVA: 0x000380D1 File Offset: 0x000362D1
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				writer.WriteBool(this.FolderIsInThisStore);
				writer.WriteSizedBytes(this.FolderIsInThisStore ? RuleAction.MoveCopyActionBase.fakeStoreEntryId : this.DestinationStoreEntryId);
				writer.WriteSizedBytes(this.DestinationFolderId);
			}

			// Token: 0x060015AA RID: 5546 RVA: 0x0003810E File Offset: 0x0003630E
			protected static void ParseMoveCopyData(Reader reader, out bool folderIsInThisStore, out byte[] storeEntryId, out byte[] folderId)
			{
				folderIsInThisStore = reader.ReadBool();
				storeEntryId = reader.ReadSizeAndByteArray();
				folderId = reader.ReadSizeAndByteArray();
			}

			// Token: 0x060015AB RID: 5547 RVA: 0x00038128 File Offset: 0x00036328
			protected override string InternalToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("folderInThisStore=").Append(this.FolderIsInThisStore);
				stringBuilder.Append(", storeEntryId={").Append(RuleAction.ByteArrayToString(this.DestinationStoreEntryId)).Append("}");
				stringBuilder.Append(", folderId={").Append(RuleAction.ByteArrayToString(this.DestinationFolderId)).Append("}");
				return stringBuilder.ToString();
			}

			// Token: 0x04000B4F RID: 2895
			public readonly bool FolderIsInThisStore;

			// Token: 0x04000B50 RID: 2896
			public readonly byte[] DestinationStoreEntryId;

			// Token: 0x04000B51 RID: 2897
			public readonly byte[] DestinationFolderId;

			// Token: 0x04000B52 RID: 2898
			private static byte[] fakeStoreEntryId = new byte[1];
		}

		// Token: 0x0200037A RID: 890
		internal sealed class MoveAction : RuleAction.MoveCopyActionBase
		{
			// Token: 0x060015AD RID: 5549 RVA: 0x000381B1 File Offset: 0x000363B1
			internal MoveAction(uint flavor, uint flags, bool folderIsInThisStore, byte[] storeEntryId, byte[] folderId) : base(RuleActionType.Move, flavor, flags, folderIsInThisStore, storeEntryId, folderId)
			{
			}

			// Token: 0x060015AE RID: 5550 RVA: 0x000381C4 File Offset: 0x000363C4
			internal static RuleAction.MoveAction Parse(Reader reader, uint flavor, uint flags)
			{
				bool folderIsInThisStore;
				byte[] storeEntryId;
				byte[] folderId;
				RuleAction.MoveCopyActionBase.ParseMoveCopyData(reader, out folderIsInThisStore, out storeEntryId, out folderId);
				return new RuleAction.MoveAction(flavor, flags, folderIsInThisStore, storeEntryId, folderId);
			}
		}

		// Token: 0x0200037B RID: 891
		internal sealed class CopyAction : RuleAction.MoveCopyActionBase
		{
			// Token: 0x060015AF RID: 5551 RVA: 0x000381E7 File Offset: 0x000363E7
			internal CopyAction(uint flavor, uint flags, bool folderIsInThisStore, byte[] storeEntryId, byte[] folderId) : base(RuleActionType.Copy, flavor, flags, folderIsInThisStore, storeEntryId, folderId)
			{
			}

			// Token: 0x060015B0 RID: 5552 RVA: 0x000381F8 File Offset: 0x000363F8
			internal static RuleAction.CopyAction Parse(Reader reader, uint flavor, uint flags)
			{
				bool folderIsInThisStore;
				byte[] storeEntryId;
				byte[] folderId;
				RuleAction.MoveCopyActionBase.ParseMoveCopyData(reader, out folderIsInThisStore, out storeEntryId, out folderId);
				return new RuleAction.CopyAction(flavor, flags, folderIsInThisStore, storeEntryId, folderId);
			}
		}

		// Token: 0x0200037C RID: 892
		internal abstract class ReplyActionBase : RuleAction
		{
			// Token: 0x060015B1 RID: 5553 RVA: 0x0003821B File Offset: 0x0003641B
			internal ReplyActionBase(RuleActionType type, uint flavor, uint flags, StoreId replyTemplateFolderId, StoreId replyTemplateMessageId, Guid replyTemplateGuid) : base(type, flavor, flags)
			{
				this.ReplyTemplateFolderId = replyTemplateFolderId;
				this.ReplyTemplateMessageId = replyTemplateMessageId;
				this.ReplyTemplateGuid = replyTemplateGuid;
			}

			// Token: 0x060015B2 RID: 5554 RVA: 0x0003823E File Offset: 0x0003643E
			protected static void ParseReplyData(Reader reader, out StoreId replyTemplateFolderId, out StoreId replyTemplateMessageId, out Guid replyTemplateGuid)
			{
				replyTemplateFolderId = StoreId.Parse(reader);
				replyTemplateMessageId = StoreId.Parse(reader);
				replyTemplateGuid = reader.ReadGuid();
			}

			// Token: 0x060015B3 RID: 5555 RVA: 0x00038264 File Offset: 0x00036464
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				this.ReplyTemplateFolderId.Serialize(writer);
				this.ReplyTemplateMessageId.Serialize(writer);
				writer.WriteGuid(this.ReplyTemplateGuid);
			}

			// Token: 0x060015B4 RID: 5556 RVA: 0x000382A4 File Offset: 0x000364A4
			protected override string InternalToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("TemplateFID=").Append(this.ReplyTemplateFolderId);
				stringBuilder.Append(", TemplateMID=").Append(this.ReplyTemplateMessageId);
				stringBuilder.Append(", TemplateGuid=").Append(this.ReplyTemplateGuid);
				return stringBuilder.ToString();
			}

			// Token: 0x04000B53 RID: 2899
			public readonly StoreId ReplyTemplateFolderId;

			// Token: 0x04000B54 RID: 2900
			public readonly StoreId ReplyTemplateMessageId;

			// Token: 0x04000B55 RID: 2901
			public readonly Guid ReplyTemplateGuid;
		}

		// Token: 0x0200037D RID: 893
		internal sealed class ReplyAction : RuleAction.ReplyActionBase
		{
			// Token: 0x060015B5 RID: 5557 RVA: 0x00038311 File Offset: 0x00036511
			internal ReplyAction(uint flavor, uint flags, StoreId replyTemplateFolderId, StoreId replyTemplateMessageId, Guid replyTemplateGuid) : base(RuleActionType.Reply, flavor, flags, replyTemplateFolderId, replyTemplateMessageId, replyTemplateGuid)
			{
			}

			// Token: 0x060015B6 RID: 5558 RVA: 0x00038324 File Offset: 0x00036524
			internal static RuleAction.ReplyAction Parse(Reader reader, uint flavor, uint flags)
			{
				StoreId replyTemplateFolderId;
				StoreId replyTemplateMessageId;
				Guid replyTemplateGuid;
				RuleAction.ReplyActionBase.ParseReplyData(reader, out replyTemplateFolderId, out replyTemplateMessageId, out replyTemplateGuid);
				return new RuleAction.ReplyAction(flavor, flags, replyTemplateFolderId, replyTemplateMessageId, replyTemplateGuid);
			}

			// Token: 0x0200037E RID: 894
			[Flags]
			public enum ReplyFlags
			{
				// Token: 0x04000B57 RID: 2903
				None = 0,
				// Token: 0x04000B58 RID: 2904
				DoNotSendToOriginator = 1,
				// Token: 0x04000B59 RID: 2905
				UseStockReplyTemplate = 2
			}
		}

		// Token: 0x0200037F RID: 895
		internal sealed class OutOfOfficeReplyAction : RuleAction.ReplyActionBase
		{
			// Token: 0x060015B7 RID: 5559 RVA: 0x00038347 File Offset: 0x00036547
			internal OutOfOfficeReplyAction(uint flavor, uint flags, StoreId replyTemplateFolderId, StoreId replyTemplateMessageId, Guid replyTemplateGuid) : base(RuleActionType.OutOfOfficeReply, flavor, flags, replyTemplateFolderId, replyTemplateMessageId, replyTemplateGuid)
			{
			}

			// Token: 0x060015B8 RID: 5560 RVA: 0x00038358 File Offset: 0x00036558
			internal static RuleAction.OutOfOfficeReplyAction Parse(Reader reader, uint flavor, uint flags)
			{
				StoreId replyTemplateFolderId;
				StoreId replyTemplateMessageId;
				Guid replyTemplateGuid;
				RuleAction.ReplyActionBase.ParseReplyData(reader, out replyTemplateFolderId, out replyTemplateMessageId, out replyTemplateGuid);
				return new RuleAction.OutOfOfficeReplyAction(flavor, flags, replyTemplateFolderId, replyTemplateMessageId, replyTemplateGuid);
			}
		}

		// Token: 0x02000380 RID: 896
		internal sealed class DeferAction : RuleAction
		{
			// Token: 0x060015B9 RID: 5561 RVA: 0x0003837B File Offset: 0x0003657B
			internal DeferAction(uint flavor, uint flags, byte[] data) : base(RuleActionType.DeferAction, flavor, flags)
			{
				this.Data = data;
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x0003838D File Offset: 0x0003658D
			internal static RuleAction.DeferAction Parse(Reader reader, uint flavor, uint flags, ulong length)
			{
				return new RuleAction.DeferAction(flavor, flags, reader.ReadBytes((uint)length));
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x0003839E File Offset: 0x0003659E
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				if (this.Data.Length > 0)
				{
					writer.WriteBytes(this.Data);
				}
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x000383C0 File Offset: 0x000365C0
			protected override string InternalToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.Data != null && 0 < this.Data.Length)
				{
					stringBuilder.Append("data={").Append(RuleAction.ByteArrayToString(this.Data)).Append("}");
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04000B5A RID: 2906
			public readonly byte[] Data;
		}

		// Token: 0x02000381 RID: 897
		internal sealed class BounceAction : RuleAction
		{
			// Token: 0x060015BD RID: 5565 RVA: 0x00038412 File Offset: 0x00036612
			internal BounceAction(uint flavor, uint flags, uint bounceCode) : base(RuleActionType.Bounce, flavor, flags)
			{
				this.BounceCode = bounceCode;
			}

			// Token: 0x060015BE RID: 5566 RVA: 0x00038424 File Offset: 0x00036624
			internal static RuleAction.BounceAction Parse(Reader reader, uint flavor, uint flags)
			{
				return new RuleAction.BounceAction(flavor, flags, reader.ReadUInt32());
			}

			// Token: 0x060015BF RID: 5567 RVA: 0x00038433 File Offset: 0x00036633
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				writer.WriteUInt32(this.BounceCode);
			}

			// Token: 0x060015C0 RID: 5568 RVA: 0x00038449 File Offset: 0x00036649
			protected override string InternalToString()
			{
				return string.Format("bounceCode=0x{0:X}", this.BounceCode);
			}

			// Token: 0x04000B5B RID: 2907
			public readonly uint BounceCode;
		}

		// Token: 0x02000382 RID: 898
		internal abstract class ForwardActionBase : RuleAction
		{
			// Token: 0x060015C1 RID: 5569 RVA: 0x00038460 File Offset: 0x00036660
			protected ForwardActionBase(RuleActionType type, uint flavor, uint flags, RuleAction.ForwardActionBase.ActionRecipient[] recipients) : base(type, flavor, flags)
			{
				if (recipients == null)
				{
					throw new ArgumentNullException("recipients");
				}
				this.Recipients = recipients;
			}

			// Token: 0x060015C2 RID: 5570 RVA: 0x00038484 File Offset: 0x00036684
			protected static void ParseForwardDelegateData(Reader reader, out RuleAction.ForwardActionBase.ActionRecipient[] recipients)
			{
				ushort num = reader.ReadUInt16();
				reader.CheckBoundary((uint)num, 3U);
				recipients = new RuleAction.ForwardActionBase.ActionRecipient[(int)num];
				for (ushort num2 = 0; num2 < num; num2 += 1)
				{
					recipients[(int)num2] = new RuleAction.ForwardActionBase.ActionRecipient(reader.ReadByte(), reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop));
				}
			}

			// Token: 0x060015C3 RID: 5571 RVA: 0x000384D4 File Offset: 0x000366D4
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				writer.WriteUInt16((ushort)this.Recipients.Length);
				for (int i = 0; i < this.Recipients.Length; i++)
				{
					writer.WriteByte(this.Recipients[i].Reserved);
					writer.WriteCountAndPropertyValueList(this.Recipients[i].Properties, string8Encoding, WireFormatStyle.Rop);
				}
			}

			// Token: 0x060015C4 RID: 5572 RVA: 0x00038548 File Offset: 0x00036748
			internal override void ResolveString8Values(Encoding string8Encoding)
			{
				for (int i = 0; i < this.Recipients.Length; i++)
				{
					for (int j = 0; j < this.Recipients[i].Properties.Length; j++)
					{
						this.Recipients[i].Properties[j].ResolveString8Values(string8Encoding);
					}
				}
			}

			// Token: 0x060015C5 RID: 5573 RVA: 0x000385B0 File Offset: 0x000367B0
			protected override string InternalToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (0 < this.Recipients.Length)
				{
					stringBuilder.Append("recipients={");
					foreach (RuleAction.ForwardActionBase.ActionRecipient actionRecipient in this.Recipients)
					{
						stringBuilder.Append("{").Append(actionRecipient).Append("}");
					}
					stringBuilder.Append("}");
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04000B5C RID: 2908
			public readonly RuleAction.ForwardActionBase.ActionRecipient[] Recipients;

			// Token: 0x02000383 RID: 899
			internal struct ActionRecipient
			{
				// Token: 0x060015C6 RID: 5574 RVA: 0x0003862F File Offset: 0x0003682F
				public ActionRecipient(byte reserved, PropertyValue[] properties)
				{
					if (properties == null)
					{
						throw new ArgumentNullException("properties");
					}
					this.Reserved = reserved;
					this.Properties = properties;
				}

				// Token: 0x04000B5D RID: 2909
				public readonly byte Reserved;

				// Token: 0x04000B5E RID: 2910
				public readonly PropertyValue[] Properties;
			}
		}

		// Token: 0x02000384 RID: 900
		internal sealed class ForwardAction : RuleAction.ForwardActionBase
		{
			// Token: 0x060015C7 RID: 5575 RVA: 0x0003864D File Offset: 0x0003684D
			internal ForwardAction(uint flavor, uint flags, RuleAction.ForwardActionBase.ActionRecipient[] recipients) : base(RuleActionType.Forward, flavor, flags, recipients)
			{
			}

			// Token: 0x060015C8 RID: 5576 RVA: 0x0003865C File Offset: 0x0003685C
			internal static RuleAction.ForwardAction Parse(Reader reader, uint flavor, uint flags)
			{
				RuleAction.ForwardActionBase.ActionRecipient[] recipients;
				RuleAction.ForwardActionBase.ParseForwardDelegateData(reader, out recipients);
				return new RuleAction.ForwardAction(flavor, flags, recipients);
			}

			// Token: 0x02000385 RID: 901
			[Flags]
			internal enum ForwardFlags
			{
				// Token: 0x04000B60 RID: 2912
				None = 0,
				// Token: 0x04000B61 RID: 2913
				PreserveSender = 1,
				// Token: 0x04000B62 RID: 2914
				DoNotChangeMessage = 2,
				// Token: 0x04000B63 RID: 2915
				ForwardAsAttachment = 4,
				// Token: 0x04000B64 RID: 2916
				SendSmsAlert = 8
			}
		}

		// Token: 0x02000386 RID: 902
		internal sealed class DelegateAction : RuleAction.ForwardActionBase
		{
			// Token: 0x060015C9 RID: 5577 RVA: 0x00038679 File Offset: 0x00036879
			internal DelegateAction(uint flavor, uint flags, RuleAction.ForwardActionBase.ActionRecipient[] recipients) : base(RuleActionType.Delegate, flavor, flags, recipients)
			{
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x00038688 File Offset: 0x00036888
			internal static RuleAction.DelegateAction Parse(Reader reader, uint flavor, uint flags)
			{
				RuleAction.ForwardActionBase.ActionRecipient[] recipients;
				RuleAction.ForwardActionBase.ParseForwardDelegateData(reader, out recipients);
				return new RuleAction.DelegateAction(flavor, flags, recipients);
			}
		}

		// Token: 0x02000387 RID: 903
		internal sealed class TagAction : RuleAction
		{
			// Token: 0x060015CB RID: 5579 RVA: 0x000386A5 File Offset: 0x000368A5
			internal TagAction(uint flavor, uint flags, PropertyValue propertyValue) : base(RuleActionType.Tag, flavor, flags)
			{
				this.PropertyValue = propertyValue;
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x000386B8 File Offset: 0x000368B8
			internal static RuleAction.TagAction Parse(Reader reader, uint flavor, uint flags)
			{
				PropertyValue propertyValue = reader.ReadPropertyValue(WireFormatStyle.Rop);
				return new RuleAction.TagAction(flavor, flags, propertyValue);
			}

			// Token: 0x060015CD RID: 5581 RVA: 0x000386D5 File Offset: 0x000368D5
			internal override void Serialize(Writer writer, Encoding string8Encoding)
			{
				base.Serialize(writer, string8Encoding);
				writer.WritePropertyValue(this.PropertyValue, string8Encoding, WireFormatStyle.Rop);
			}

			// Token: 0x060015CE RID: 5582 RVA: 0x000386F0 File Offset: 0x000368F0
			internal override void ResolveString8Values(Encoding string8Encoding)
			{
				this.PropertyValue.ResolveString8Values(string8Encoding);
			}

			// Token: 0x060015CF RID: 5583 RVA: 0x0003870C File Offset: 0x0003690C
			protected override string InternalToString()
			{
				return string.Format("property={0}", this.PropertyValue);
			}

			// Token: 0x04000B65 RID: 2917
			public readonly PropertyValue PropertyValue;
		}

		// Token: 0x02000388 RID: 904
		internal sealed class DeleteAction : RuleAction
		{
			// Token: 0x060015D0 RID: 5584 RVA: 0x00038723 File Offset: 0x00036923
			internal DeleteAction(uint flavor, uint flags) : base(RuleActionType.Delete, flavor, flags)
			{
			}

			// Token: 0x060015D1 RID: 5585 RVA: 0x0003872F File Offset: 0x0003692F
			internal static RuleAction.DeleteAction Parse(Reader reader, uint flavor, uint flags)
			{
				return new RuleAction.DeleteAction(flavor, flags);
			}
		}

		// Token: 0x02000389 RID: 905
		internal sealed class MarkAsReadAction : RuleAction
		{
			// Token: 0x060015D2 RID: 5586 RVA: 0x00038738 File Offset: 0x00036938
			internal MarkAsReadAction(uint flavor, uint flags) : base(RuleActionType.MarkAsRead, flavor, flags)
			{
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x00038744 File Offset: 0x00036944
			internal static RuleAction.MarkAsReadAction Parse(Reader reader, uint flavor, uint flags)
			{
				return new RuleAction.MarkAsReadAction(flavor, flags);
			}
		}
	}
}
