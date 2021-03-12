using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000168 RID: 360
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferMessageChange : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00015482 File Offset: 0x00013682
		public FastTransferMessageChange(IMessageChange messageChange, IcsContentsSynchronizer.Options options, IEnumerable<PropertyTag> additionalHeaderProperties, IcsPartialItemState partialItemState, bool isTopLevel) : this(messageChange, null, options, additionalHeaderProperties, partialItemState, isTopLevel)
		{
			Util.ThrowOnNullArgument(messageChange, "messageChange");
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001549D File Offset: 0x0001369D
		public FastTransferMessageChange(IMessageChangeClient messageChangeClient, IcsContentsSynchronizer.Options options, IcsPartialItemState partialItemState, bool isTopLevel) : this(null, messageChangeClient, options, null, partialItemState, isTopLevel)
		{
			Util.ThrowOnNullArgument(messageChangeClient, "messageChangeClient");
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000154B8 File Offset: 0x000136B8
		private FastTransferMessageChange(IMessageChange messageChange, IMessageChangeClient messageChangeClient, IcsContentsSynchronizer.Options options, IEnumerable<PropertyTag> additionalHeaderProperties, IcsPartialItemState partialItemState, bool isTopLevel) : base(isTopLevel)
		{
			this.messageChange = messageChange;
			this.messageChangeClient = messageChangeClient;
			this.options = options;
			this.partialItemState = partialItemState;
			this.messageHeaderProperties = FastTransferMessageChange.GetMessageHeaderPropertyList(options);
			if ((this.options & IcsContentsSynchronizer.Options.ManifestMode) == IcsContentsSynchronizer.Options.ManifestMode && additionalHeaderProperties != null)
			{
				this.additionalMessageHeaderProperties = additionalHeaderProperties;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00015510 File Offset: 0x00013710
		public static bool IsMessageBeginMarker(PropertyTag marker)
		{
			return marker == PropertyTag.IncrSyncProgressPerMsg || marker == PropertyTag.IncrSyncChg || marker == PropertyTag.IncrSyncGroupInfo || marker == PropertyTag.IncrSyncGroupId || marker == PropertyTag.IncrSyncChgPartial;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00015560 File Offset: 0x00013760
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			IMessageChangePartial messageChangePartial = null;
			if ((this.options & IcsContentsSynchronizer.Options.ManifestMode) == IcsContentsSynchronizer.Options.None && this.partialItemState != null)
			{
				messageChangePartial = this.messageChange.PartialChange;
			}
			if (messageChangePartial != null)
			{
				return this.SerializePartialMessage(context, messageChangePartial);
			}
			return this.SerializeFullMessage(context);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00015A78 File Offset: 0x00013C78
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.ParseProgress(context)));
			PropertyTag marker;
			context.DataInterface.TryPeekMarker(out marker);
			if (marker == PropertyTag.IncrSyncChg)
			{
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncChg);
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChangeClient.MessageHeaderPropertyBag)));
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncMsg);
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChangeClient.Message.PropertyBag)));
			}
			else
			{
				PropertyGroupMapping propGroupMapping;
				if (marker == PropertyTag.IncrSyncGroupInfo)
				{
					context.DataInterface.ReadMarker(PropertyTag.IncrSyncGroupInfo);
					SingleMemberPropertyBag singleMemberPropertyBag = new SingleMemberPropertyBag(PropertyTag.PropertyGroupMappingInfo);
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
					propGroupMapping = PropertyGroupMapping.Deserialize(singleMemberPropertyBag.PropertyValue.GetValue<byte[]>());
					this.partialItemState.AddMapping(propGroupMapping);
					context.DataInterface.TryPeekMarker(out marker);
				}
				if (marker == PropertyTag.IncrSyncGroupId)
				{
					context.DataInterface.ReadMarker(PropertyTag.IncrSyncGroupId);
					yield return null;
					PropertyValue mappingIdValue = context.DataInterface.ReadAndParseFixedSizeValue(PropertyTag.IncrSyncGroupId);
					this.partialItemState.CurrentMappingId = mappingIdValue.GetValue<int>();
					yield return null;
				}
				propGroupMapping = this.partialItemState.GetCurrentMapping();
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncChgPartial);
				this.messageChangeClient.SetPartial();
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChangeClient.MessageHeaderPropertyBag)));
				while (context.DataInterface.TryPeekMarker(out marker) && marker == PropertyTag.IncrSyncMsgPartial)
				{
					context.DataInterface.ReadMarker(PropertyTag.IncrSyncMsgPartial);
					yield return null;
					int propGroupIndex = context.DataInterface.ReadAndParseFixedSizeValue(PropertyTag.IncrSyncMsgPartial).GetValue<int>();
					if (!propGroupMapping.IsValidPropGroupIndex(propGroupIndex))
					{
						throw new RopExecutionException(string.Format("Invalid property group index {0}", propGroupIndex), (ErrorCode)2147942487U);
					}
					yield return null;
					this.messageChangeClient.ScrubGroupProperties(propGroupMapping, propGroupIndex);
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChangeClient.Message.PropertyBag)));
				}
			}
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(FastTransferMessageChange.ParseRecipientsAndAttachments(context, this.messageChangeClient.Message)));
			yield break;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00015BCC File Offset: 0x00013DCC
		internal static IEnumerator<FastTransferStateMachine?> SerializeRecipientsAndAttachments(FastTransferDownloadContext context, IMessage message, bool downloadRecipients, bool downloadAttachments)
		{
			if (downloadRecipients)
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, FastTransferMessageChange.FXDelPropMessageRecipientsPropertyValue));
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferRecipientList(message)));
			}
			if (downloadAttachments)
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, FastTransferMessageChange.FXDelPropMessageAttachmentsPropertyValue));
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferAttachmentList(message)));
			}
			yield break;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00015DA8 File Offset: 0x00013FA8
		internal static IEnumerator<FastTransferStateMachine?> ParseRecipientsAndAttachments(FastTransferUploadContext context, IMessage messageClient)
		{
			SingleMemberPropertyBag singleMemberPropertyBag = new SingleMemberPropertyBag(PropertyTag.FXDelProp);
			PropertyTag metaProperty;
			while (!context.NoMoreData && !context.DataInterface.TryPeekMarker(out metaProperty))
			{
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
				PropertyTag subObject = new PropertyTag((uint)singleMemberPropertyBag.PropertyValue.GetValue<int>());
				if (subObject == PropertyTag.MessageRecipients)
				{
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferRecipientList(messageClient)));
				}
				else
				{
					if (!(subObject == PropertyTag.MessageAttachments))
					{
						throw new RopExecutionException(string.Format("Sub-object property {0} was not expected when processing {1} property", subObject, metaProperty), ErrorCode.FxUnexpectedMarker);
					}
					yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferAttachmentList(messageClient)));
				}
			}
			yield break;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00015DCB File Offset: 0x00013FCB
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.messageChange);
			Util.DisposeIfPresent(this.messageChangeClient);
			base.InternalDispose();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00015DE9 File Offset: 0x00013FE9
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferMessageChange>(this);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00015DF4 File Offset: 0x00013FF4
		private static int GetIndexFromOptions(IcsContentsSynchronizer.Options options)
		{
			int num = 0;
			int num2 = 1;
			foreach (IcsContentsSynchronizer.Options options2 in FastTransferMessageChange.OptionsIncludeList)
			{
				if ((options & options2) != IcsContentsSynchronizer.Options.None)
				{
					num |= num2;
				}
				num2 <<= 1;
			}
			return num;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00015E30 File Offset: 0x00014030
		private static IcsContentsSynchronizer.Options GetOptionsFromIndex(int index)
		{
			IcsContentsSynchronizer.Options options = IcsContentsSynchronizer.Options.None;
			int num = 1;
			for (int i = 0; i < FastTransferMessageChange.OptionsIncludeList.Length; i++)
			{
				if ((index & num) != 0)
				{
					options |= FastTransferMessageChange.OptionsIncludeList[i];
				}
				num <<= 1;
			}
			return options;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015E68 File Offset: 0x00014068
		private static int GetCountOfOptionCombinations()
		{
			int num = 0;
			int num2 = 142;
			for (int num3 = 1; num3 != 0; num3 <<= 1)
			{
				if ((num2 & num3) != 0)
				{
					num <<= 1;
					num++;
				}
			}
			return num + 1;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015E9C File Offset: 0x0001409C
		private static ReadOnlyCollection<PropertyTag> GetMessageHeaderPropertyList(IcsContentsSynchronizer.Options options)
		{
			if ((options & IcsContentsSynchronizer.Options.Conversations) != IcsContentsSynchronizer.Options.None)
			{
				return FastTransferMessageChange.ConversationHeaderPropertyList;
			}
			int indexFromOptions = FastTransferMessageChange.GetIndexFromOptions(options);
			return FastTransferMessageChange.MessageHeaderPropertyLists[indexFromOptions];
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00015EC4 File Offset: 0x000140C4
		private static ReadOnlyCollection<PropertyTag>[] BuildMessageHeaderPropertyLists()
		{
			int countOfOptionCombinations = FastTransferMessageChange.GetCountOfOptionCombinations();
			ReadOnlyCollection<PropertyTag>[] array = new ReadOnlyCollection<PropertyTag>[countOfOptionCombinations];
			for (int i = 0; i < countOfOptionCombinations; i++)
			{
				IcsContentsSynchronizer.Options optionsFromIndex = FastTransferMessageChange.GetOptionsFromIndex(i);
				array[i] = FastTransferMessageChange.ConstructMessageHeaderPropertyList(optionsFromIndex);
			}
			return array;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00015EFC File Offset: 0x000140FC
		private static ReadOnlyCollection<PropertyTag> ConstructMessageHeaderPropertyList(IcsContentsSynchronizer.Options options)
		{
			List<PropertyTag> list = new List<PropertyTag>(FastTransferMessageChange.AllMessageHeaderProperties);
			if ((options & IcsContentsSynchronizer.Options.IncludeChangeNumber) == IcsContentsSynchronizer.Options.None)
			{
				list.Remove(PropertyTag.ChangeNumber);
			}
			if ((options & IcsContentsSynchronizer.Options.IncludeMessageSize) == IcsContentsSynchronizer.Options.None)
			{
				list.Remove(PropertyTag.MessageSize);
			}
			if ((options & IcsContentsSynchronizer.Options.IncludeMid) == IcsContentsSynchronizer.Options.None)
			{
				list.Remove(PropertyTag.Mid);
			}
			if ((options & IcsContentsSynchronizer.Options.IncludeReadChangeNumber) == IcsContentsSynchronizer.Options.None)
			{
				list.Remove(PropertyTag.ReadChangeNumber);
			}
			return new ReadOnlyCollection<PropertyTag>(list);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000160C4 File Offset: 0x000142C4
		private IEnumerator<FastTransferStateMachine?> ParseProgress(FastTransferUploadContext context)
		{
			PropertyTag marker;
			context.DataInterface.TryPeekMarker(out marker);
			if (marker == PropertyTag.IncrSyncProgressPerMsg)
			{
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncProgressPerMsg);
				SingleMemberPropertyBag singleMemberPropertyBag = new SingleMemberPropertyBag(FastTransferMessageChange.ProgressMessageSizePropertyTag);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
				this.messageChangeClient.ReportMessageSize(singleMemberPropertyBag.PropertyValue.GetValue<int>());
				singleMemberPropertyBag = new SingleMemberPropertyBag(FastTransferMessageChange.ProgressIsAssociatedMessagePropertyTag);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
				this.messageChangeClient.ReportIsAssociatedMessage(singleMemberPropertyBag.PropertyValue.GetValue<bool>());
			}
			yield break;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00016260 File Offset: 0x00014460
		private IEnumerator<FastTransferStateMachine?> SerializeFullMessage(FastTransferDownloadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeProgress(context)));
			context.DataInterface.PutMarker(PropertyTag.IncrSyncChg);
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeMessageHeaderProperties(context)));
			if ((this.options & IcsContentsSynchronizer.Options.ManifestMode) == IcsContentsSynchronizer.Options.None)
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncMsg);
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeMessageProperties(context)));
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(FastTransferMessageChange.SerializeRecipientsAndAttachments(context, this.messageChange.Message, true, true)));
			}
			yield break;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00016780 File Offset: 0x00014980
		private IEnumerator<FastTransferStateMachine?> SerializePartialMessage(FastTransferDownloadContext context, IMessageChangePartial partialChange)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeProgress(context)));
			PropertyGroupMapping propGroupMapping = partialChange.PropertyGroupMapping;
			if (this.partialItemState.AddMapping(propGroupMapping))
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncGroupInfo);
				PropertyValue propValuePropertyGroupMappingInfo = new PropertyValue(PropertyTag.PropertyGroupMappingInfo, propGroupMapping.Serialize());
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, propValuePropertyGroupMappingInfo));
			}
			else
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncGroupInfo);
				PropertyValue propValuePropertyGroupMappingInfo2 = new PropertyValue(PropertyTag.PropertyGroupMappingInfo, PropertyGroupMapping.SerializedFakeMapping);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, propValuePropertyGroupMappingInfo2));
			}
			if (propGroupMapping.MappingId != this.partialItemState.CurrentMappingId)
			{
				this.partialItemState.CurrentMappingId = propGroupMapping.MappingId;
				PropertyValue propValueIncrSyncGroupId = new PropertyValue(PropertyTag.IncrSyncGroupId, propGroupMapping.MappingId);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, propValueIncrSyncGroupId));
			}
			context.DataInterface.PutMarker(PropertyTag.IncrSyncChgPartial);
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeMessageHeaderProperties(context)));
			bool recipientsGroupChanged = false;
			bool attachmentsGroupChanged = false;
			foreach (int groupIndex in partialChange.ChangedPropGroups)
			{
				PropertyValue propValueIncrSyncMsgPartial = new PropertyValue(PropertyTag.IncrSyncMsgPartial, groupIndex);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, propValueIncrSyncMsgPartial));
				IEnumerable<PropertyTag> propTagsInGroup;
				if (groupIndex >= 0)
				{
					propTagsInGroup = from annotatedPropertyTag in propGroupMapping[groupIndex]
					select annotatedPropertyTag.PropertyTag;
				}
				else
				{
					propTagsInGroup = partialChange.OtherGroupPropTags;
				}
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChange.Message.PropertyBag, context.PropertyFilterFactory.GetMessageFilter(base.IsTopLevel), propTagsInGroup)));
				if (groupIndex == propGroupMapping.RecipientsGroupIndex)
				{
					recipientsGroupChanged = true;
				}
				if (groupIndex == propGroupMapping.AttachmentsGroupIndex)
				{
					attachmentsGroupChanged = true;
				}
			}
			if (recipientsGroupChanged || attachmentsGroupChanged)
			{
				yield return new FastTransferStateMachine?(new FastTransferStateMachine(FastTransferMessageChange.SerializeRecipientsAndAttachments(context, this.messageChange.Message, recipientsGroupChanged, attachmentsGroupChanged)));
			}
			yield break;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000168C0 File Offset: 0x00014AC0
		private IEnumerator<FastTransferStateMachine?> SerializeProgress(FastTransferDownloadContext context)
		{
			if ((this.options & IcsContentsSynchronizer.Options.ProgressMode) == IcsContentsSynchronizer.Options.ProgressMode)
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncProgressPerMsg);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, new PropertyValue(FastTransferMessageChange.ProgressMessageSizePropertyTag, this.messageChange.MessageSize)));
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, new PropertyValue(FastTransferMessageChange.ProgressIsAssociatedMessagePropertyTag, this.messageChange.IsAssociatedMessage)));
			}
			yield break;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00016A18 File Offset: 0x00014C18
		private IEnumerator<FastTransferStateMachine?> SerializeMessageHeaderProperties(FastTransferDownloadContext context)
		{
			FastTransferPropList fastTransferProplist = new FastTransferPropList(this.messageChange.MessageHeaderPropertyBag, this.messageHeaderProperties);
			if ((this.options & IcsContentsSynchronizer.Options.Conversations) == IcsContentsSynchronizer.Options.None)
			{
				fastTransferProplist.ThrowOnPropertyError = true;
			}
			yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferProplist));
			if ((this.options & IcsContentsSynchronizer.Options.ManifestMode) == IcsContentsSynchronizer.Options.ManifestMode && this.additionalMessageHeaderProperties != null)
			{
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChange.MessageHeaderPropertyBag, this.additionalMessageHeaderProperties)));
			}
			yield break;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00016AF0 File Offset: 0x00014CF0
		private IEnumerator<FastTransferStateMachine?> SerializeMessageProperties(FastTransferDownloadContext context)
		{
			yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(this.messageChange.Message.PropertyBag, context.PropertyFilterFactory.GetMessageFilter(base.IsTopLevel))));
			yield break;
		}

		// Token: 0x04000371 RID: 881
		private const IcsContentsSynchronizer.Options OptionsIncludeMask = IcsContentsSynchronizer.Options.IncludeMid | IcsContentsSynchronizer.Options.IncludeMessageSize | IcsContentsSynchronizer.Options.IncludeChangeNumber | IcsContentsSynchronizer.Options.IncludeReadChangeNumber;

		// Token: 0x04000372 RID: 882
		public static readonly ReadOnlyCollection<PropertyTag> AllMessageHeaderProperties = new ReadOnlyCollection<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.ExternalMid,
			PropertyTag.LastModificationTime,
			PropertyTag.ExternalChangeNumber,
			PropertyTag.ExternalPredecessorChangeList,
			PropertyTag.Associated,
			PropertyTag.Mid,
			PropertyTag.MessageSize,
			PropertyTag.ChangeNumber,
			PropertyTag.ReadChangeNumber
		});

		// Token: 0x04000373 RID: 883
		private static readonly PropertyTag ProgressMessageSizePropertyTag = new PropertyTag(PropertyId.Null, PropertyType.Int32);

		// Token: 0x04000374 RID: 884
		private static readonly PropertyTag ProgressIsAssociatedMessagePropertyTag = new PropertyTag(PropertyId.Null, PropertyType.Bool);

		// Token: 0x04000375 RID: 885
		private static readonly IcsContentsSynchronizer.Options[] OptionsIncludeList = new IcsContentsSynchronizer.Options[]
		{
			IcsContentsSynchronizer.Options.IncludeChangeNumber,
			IcsContentsSynchronizer.Options.IncludeMessageSize,
			IcsContentsSynchronizer.Options.IncludeMid,
			IcsContentsSynchronizer.Options.IncludeReadChangeNumber
		};

		// Token: 0x04000376 RID: 886
		private static readonly ReadOnlyCollection<PropertyTag>[] MessageHeaderPropertyLists = FastTransferMessageChange.BuildMessageHeaderPropertyLists();

		// Token: 0x04000377 RID: 887
		private static readonly ReadOnlyCollection<PropertyTag> ConversationHeaderPropertyList = new ReadOnlyCollection<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.Mid,
			PropertyTag.ChangeNumber,
			PropertyTag.LastModificationTime,
			PropertyTag.ChangeType
		});

		// Token: 0x04000378 RID: 888
		private static readonly PropertyValue FXDelPropMessageRecipientsPropertyValue = new PropertyValue(PropertyTag.FXDelProp, (int)PropertyTag.MessageRecipients);

		// Token: 0x04000379 RID: 889
		private static readonly PropertyValue FXDelPropMessageAttachmentsPropertyValue = new PropertyValue(PropertyTag.FXDelProp, (int)PropertyTag.MessageAttachments);

		// Token: 0x0400037A RID: 890
		private readonly IMessageChange messageChange;

		// Token: 0x0400037B RID: 891
		private readonly IMessageChangeClient messageChangeClient;

		// Token: 0x0400037C RID: 892
		private readonly IcsContentsSynchronizer.Options options;

		// Token: 0x0400037D RID: 893
		private readonly ReadOnlyCollection<PropertyTag> messageHeaderProperties;

		// Token: 0x0400037E RID: 894
		private readonly IEnumerable<PropertyTag> additionalMessageHeaderProperties;

		// Token: 0x0400037F RID: 895
		private readonly IcsPartialItemState partialItemState;
	}
}
