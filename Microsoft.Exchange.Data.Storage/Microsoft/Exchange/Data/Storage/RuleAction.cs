using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BC1 RID: 3009
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RuleAction
	{
		// Token: 0x06006B74 RID: 27508 RVA: 0x001CBF62 File Offset: 0x001CA162
		protected RuleAction(RuleActionType type, uint userFlags)
		{
			EnumValidator.ThrowIfInvalid<RuleActionType>(type);
			this.ActionType = type;
			this.UserFlags = userFlags;
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x001CBF7E File Offset: 0x001CA17E
		public override string ToString()
		{
			return string.Format("[{0}, user flags=0x{1:X}.{2}]", this.ActionType, this.UserFlags, this.InternalToString());
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x001CBFA6 File Offset: 0x001CA1A6
		protected virtual string InternalToString()
		{
			return string.Empty;
		}

		// Token: 0x04003D74 RID: 15732
		public readonly RuleActionType ActionType;

		// Token: 0x04003D75 RID: 15733
		public readonly uint UserFlags;

		// Token: 0x02000BC2 RID: 3010
		public abstract class MoveCopyActionBase : RuleAction
		{
			// Token: 0x06006B77 RID: 27511 RVA: 0x001CBFB0 File Offset: 0x001CA1B0
			protected MoveCopyActionBase(RuleActionType type, uint userFlags, byte[] destinationStoreEntryId, StoreObjectId destinationFolderId, byte[] externalDestinationFolderId) : base(type, userFlags)
			{
				if (externalDestinationFolderId != null)
				{
					Util.ThrowOnNullArgument(destinationStoreEntryId, "destinationStoreEntryId");
				}
				if (destinationFolderId != null == (externalDestinationFolderId != null))
				{
					throw new ArgumentException("There must be either a destination folder id (as a StoreObjectId) or an external folder id (as a byte[]), but not both.");
				}
				this.DestinationStoreEntryId = destinationStoreEntryId;
				this.DestinationFolderId = destinationFolderId;
				this.ExternalDestinationFolderId = externalDestinationFolderId;
				this.FolderIsInThisStore = (destinationFolderId != null);
			}

			// Token: 0x06006B78 RID: 27512 RVA: 0x001CC016 File Offset: 0x001CA216
			protected override string InternalToString()
			{
				return string.Format("DestinationStoreEntryId={0}.DestinationFolderId={1}.", this.DestinationStoreEntryId, this.DestinationFolderId);
			}

			// Token: 0x04003D76 RID: 15734
			public readonly byte[] DestinationStoreEntryId;

			// Token: 0x04003D77 RID: 15735
			public readonly StoreObjectId DestinationFolderId;

			// Token: 0x04003D78 RID: 15736
			public readonly byte[] ExternalDestinationFolderId;

			// Token: 0x04003D79 RID: 15737
			public readonly bool FolderIsInThisStore;
		}

		// Token: 0x02000BC3 RID: 3011
		public sealed class MoveAction : RuleAction.MoveCopyActionBase
		{
			// Token: 0x06006B79 RID: 27513 RVA: 0x001CC02E File Offset: 0x001CA22E
			public MoveAction(uint userFlags, StoreObjectId destinationFolderId) : base(RuleActionType.Move, userFlags, null, destinationFolderId, null)
			{
			}

			// Token: 0x06006B7A RID: 27514 RVA: 0x001CC03B File Offset: 0x001CA23B
			public MoveAction(uint userFlags, byte[] destinationStoreEntryId, byte[] externalDestinationFolderId) : base(RuleActionType.Move, userFlags, destinationStoreEntryId, null, externalDestinationFolderId)
			{
			}
		}

		// Token: 0x02000BC4 RID: 3012
		public sealed class CopyAction : RuleAction.MoveCopyActionBase
		{
			// Token: 0x06006B7B RID: 27515 RVA: 0x001CC048 File Offset: 0x001CA248
			public CopyAction(uint userFlags, StoreObjectId destinationFolderId) : base(RuleActionType.Copy, userFlags, null, destinationFolderId, null)
			{
			}

			// Token: 0x06006B7C RID: 27516 RVA: 0x001CC055 File Offset: 0x001CA255
			public CopyAction(uint userFlags, byte[] destinationStoreEntryId, byte[] externalDestinationFolderId) : base(RuleActionType.Copy, userFlags, destinationStoreEntryId, null, externalDestinationFolderId)
			{
			}
		}

		// Token: 0x02000BC5 RID: 3013
		public abstract class ReplyActionBase : RuleAction
		{
			// Token: 0x06006B7D RID: 27517 RVA: 0x001CC062 File Offset: 0x001CA262
			internal ReplyActionBase(RuleActionType type, uint userFlags, StoreObjectId replyTemplateMessageId, Guid replyTemplateGuid) : base(type, userFlags)
			{
				this.ReplyTemplateMessageId = replyTemplateMessageId;
				this.ReplyTemplateGuid = replyTemplateGuid;
			}

			// Token: 0x06006B7E RID: 27518 RVA: 0x001CC07B File Offset: 0x001CA27B
			protected override string InternalToString()
			{
				return string.Format("TemplateMessageId={0}.TemplateGuid={1}.", this.ReplyTemplateMessageId, this.ReplyTemplateGuid);
			}

			// Token: 0x04003D7A RID: 15738
			public readonly StoreObjectId ReplyTemplateMessageId;

			// Token: 0x04003D7B RID: 15739
			public readonly Guid ReplyTemplateGuid;
		}

		// Token: 0x02000BC6 RID: 3014
		public sealed class ReplyAction : RuleAction.ReplyActionBase
		{
			// Token: 0x06006B7F RID: 27519 RVA: 0x001CC098 File Offset: 0x001CA298
			public ReplyAction(uint userFlags, RuleAction.ReplyAction.ReplyFlags replyFlags, StoreObjectId replyTemplateMessageId, Guid replyTemplateGuid) : base(RuleActionType.Reply, userFlags, replyTemplateMessageId, replyTemplateGuid)
			{
				EnumValidator.ThrowIfInvalid<RuleAction.ReplyAction.ReplyFlags>(replyFlags);
				this.Flags = replyFlags;
			}

			// Token: 0x06006B80 RID: 27520 RVA: 0x001CC0B2 File Offset: 0x001CA2B2
			protected override string InternalToString()
			{
				return string.Format("ReplyFlags={0}.{1}", this.Flags, base.InternalToString());
			}

			// Token: 0x04003D7C RID: 15740
			public readonly RuleAction.ReplyAction.ReplyFlags Flags;

			// Token: 0x02000BC7 RID: 3015
			[Flags]
			public enum ReplyFlags
			{
				// Token: 0x04003D7E RID: 15742
				None = 0,
				// Token: 0x04003D7F RID: 15743
				DoNotSendToOriginator = 1,
				// Token: 0x04003D80 RID: 15744
				UseStockReplyTemplate = 2
			}
		}

		// Token: 0x02000BC8 RID: 3016
		public sealed class OutOfOfficeReplyAction : RuleAction.ReplyActionBase
		{
			// Token: 0x06006B81 RID: 27521 RVA: 0x001CC0CF File Offset: 0x001CA2CF
			public OutOfOfficeReplyAction(uint userFlags, StoreObjectId replyTemplateMessageId, Guid replyTemplateGuid) : base(RuleActionType.OutOfOfficeReply, userFlags, replyTemplateMessageId, replyTemplateGuid)
			{
			}
		}

		// Token: 0x02000BC9 RID: 3017
		public sealed class DeferAction : RuleAction
		{
			// Token: 0x06006B82 RID: 27522 RVA: 0x001CC0DB File Offset: 0x001CA2DB
			public DeferAction(uint userFlags, byte[] data) : base(RuleActionType.DeferAction, userFlags)
			{
				Util.ThrowOnNullArgument(data, "data");
				this.Data = data;
			}

			// Token: 0x06006B83 RID: 27523 RVA: 0x001CC0F7 File Offset: 0x001CA2F7
			protected override string InternalToString()
			{
				return string.Format("Data.Length={0}", this.Data.Length);
			}

			// Token: 0x04003D81 RID: 15745
			public readonly byte[] Data;
		}

		// Token: 0x02000BCA RID: 3018
		public sealed class BounceAction : RuleAction
		{
			// Token: 0x06006B84 RID: 27524 RVA: 0x001CC110 File Offset: 0x001CA310
			public BounceAction(uint userFlags, uint bounceCode) : base(RuleActionType.Bounce, userFlags)
			{
				this.BounceCode = bounceCode;
			}

			// Token: 0x06006B85 RID: 27525 RVA: 0x001CC121 File Offset: 0x001CA321
			protected override string InternalToString()
			{
				return string.Format("bounceCode=0x{0:X}", this.BounceCode);
			}

			// Token: 0x04003D82 RID: 15746
			public readonly uint BounceCode;
		}

		// Token: 0x02000BCB RID: 3019
		public abstract class ForwardActionBase : RuleAction
		{
			// Token: 0x06006B86 RID: 27526 RVA: 0x001CC138 File Offset: 0x001CA338
			protected ForwardActionBase(RuleActionType type, uint userFlags, RuleAction.ForwardActionBase.ActionRecipient[] recipients) : base(type, userFlags)
			{
				Util.ThrowOnNullArgument(recipients, "recipients");
				this.Recipients = recipients;
			}

			// Token: 0x06006B87 RID: 27527 RVA: 0x001CC154 File Offset: 0x001CA354
			protected override string InternalToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (RuleAction.ForwardActionBase.ActionRecipient actionRecipient in this.Recipients)
				{
					stringBuilder.AppendFormat("[{0}]", actionRecipient);
				}
				return string.Format("Recipients=[{0}].", stringBuilder.ToString());
			}

			// Token: 0x04003D83 RID: 15747
			public readonly RuleAction.ForwardActionBase.ActionRecipient[] Recipients;

			// Token: 0x02000BCC RID: 3020
			public struct ActionRecipient
			{
				// Token: 0x06006B88 RID: 27528 RVA: 0x001CC1AB File Offset: 0x001CA3AB
				public ActionRecipient(IList<NativeStorePropertyDefinition> propertyDefinitions, IList<object> propertyValues)
				{
					Util.ThrowOnNullArgument(propertyDefinitions, "propertyDefinitions");
					Util.ThrowOnNullArgument(propertyValues, "propertyValues");
					if (propertyDefinitions.Count != propertyValues.Count)
					{
						throw new ArgumentException("propertyDefinitions and propertyValues should be of the same size.");
					}
					this.PropertyDefinitions = propertyDefinitions;
					this.PropertyValues = propertyValues;
				}

				// Token: 0x04003D84 RID: 15748
				public readonly IList<NativeStorePropertyDefinition> PropertyDefinitions;

				// Token: 0x04003D85 RID: 15749
				public readonly IList<object> PropertyValues;
			}
		}

		// Token: 0x02000BCD RID: 3021
		public sealed class ForwardAction : RuleAction.ForwardActionBase
		{
			// Token: 0x06006B89 RID: 27529 RVA: 0x001CC1EA File Offset: 0x001CA3EA
			public ForwardAction(uint userFlags, RuleAction.ForwardActionBase.ActionRecipient[] recipients, RuleAction.ForwardAction.ForwardFlags forwardFlags) : base(RuleActionType.Forward, userFlags, recipients)
			{
				EnumValidator.ThrowIfInvalid<RuleAction.ForwardAction.ForwardFlags>(forwardFlags);
				this.Flags = forwardFlags;
			}

			// Token: 0x06006B8A RID: 27530 RVA: 0x001CC202 File Offset: 0x001CA402
			protected override string InternalToString()
			{
				return string.Format("{0}ForwardFlags={1}.", base.InternalToString(), this.Flags);
			}

			// Token: 0x04003D86 RID: 15750
			public readonly RuleAction.ForwardAction.ForwardFlags Flags;

			// Token: 0x02000BCE RID: 3022
			[Flags]
			public enum ForwardFlags
			{
				// Token: 0x04003D88 RID: 15752
				None = 0,
				// Token: 0x04003D89 RID: 15753
				PreserveSender = 1,
				// Token: 0x04003D8A RID: 15754
				DoNotChangeMessage = 2,
				// Token: 0x04003D8B RID: 15755
				ForwardAsAttachment = 4,
				// Token: 0x04003D8C RID: 15756
				SendSmsAlert = 8
			}
		}

		// Token: 0x02000BCF RID: 3023
		public sealed class DelegateAction : RuleAction.ForwardActionBase
		{
			// Token: 0x06006B8B RID: 27531 RVA: 0x001CC21F File Offset: 0x001CA41F
			public DelegateAction(uint userFlags, RuleAction.ForwardActionBase.ActionRecipient[] recipients) : base(RuleActionType.Delegate, userFlags, recipients)
			{
			}
		}

		// Token: 0x02000BD0 RID: 3024
		public sealed class TagAction : RuleAction
		{
			// Token: 0x06006B8C RID: 27532 RVA: 0x001CC22A File Offset: 0x001CA42A
			public TagAction(uint userFlags, NativeStorePropertyDefinition propertyDefinition, object propertyValue) : base(RuleActionType.Tag, userFlags)
			{
				Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
				Util.ThrowOnNullArgument(propertyValue, "propertyValue");
				this.PropertyDefinition = propertyDefinition;
				this.PropertyValue = propertyValue;
			}

			// Token: 0x06006B8D RID: 27533 RVA: 0x001CC259 File Offset: 0x001CA459
			protected override string InternalToString()
			{
				return string.Format("Property={0}.Value={1}.", this.PropertyDefinition, this.PropertyValue);
			}

			// Token: 0x04003D8D RID: 15757
			public readonly NativeStorePropertyDefinition PropertyDefinition;

			// Token: 0x04003D8E RID: 15758
			public readonly object PropertyValue;
		}

		// Token: 0x02000BD1 RID: 3025
		public sealed class DeleteAction : RuleAction
		{
			// Token: 0x06006B8E RID: 27534 RVA: 0x001CC271 File Offset: 0x001CA471
			public DeleteAction(uint userFlags) : base(RuleActionType.Delete, userFlags)
			{
			}
		}

		// Token: 0x02000BD2 RID: 3026
		public sealed class MarkAsReadAction : RuleAction
		{
			// Token: 0x06006B8F RID: 27535 RVA: 0x001CC27C File Offset: 0x001CA47C
			public MarkAsReadAction(uint userFlags) : base(RuleActionType.MarkAsRead, userFlags)
			{
			}
		}
	}
}
