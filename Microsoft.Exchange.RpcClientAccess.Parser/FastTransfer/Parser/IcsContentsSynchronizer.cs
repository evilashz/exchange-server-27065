using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000184 RID: 388
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IcsContentsSynchronizer : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0001A649 File Offset: 0x00018849
		public IcsContentsSynchronizer(IContentsSynchronizer contentsSynchronizer, IcsContentsSynchronizer.Options options) : this(contentsSynchronizer, null, options, null)
		{
			Util.ThrowOnNullArgument(contentsSynchronizer, "contentsSynchronizer");
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001A660 File Offset: 0x00018860
		public IcsContentsSynchronizer(IContentsSynchronizer contentsSynchronizer, IcsContentsSynchronizer.Options options, IEnumerable<PropertyTag> additionalHeaderProperties) : this(contentsSynchronizer, null, options, additionalHeaderProperties)
		{
			Util.ThrowOnNullArgument(contentsSynchronizer, "contentsSynchronizer");
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001A677 File Offset: 0x00018877
		internal IcsContentsSynchronizer(IContentsSynchronizerClient contentsSynchronizerClient, IcsContentsSynchronizer.Options options) : this(null, contentsSynchronizerClient, options, null)
		{
			Util.ThrowOnNullArgument(contentsSynchronizerClient, "contentsSynchronizerClient");
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001A690 File Offset: 0x00018890
		private IcsContentsSynchronizer(IContentsSynchronizer contentsSynchronizer, IContentsSynchronizerClient contentsSynchronizerClient, IcsContentsSynchronizer.Options options, IEnumerable<PropertyTag> additionalHeaderProperties) : base(true)
		{
			this.contentsSynchronizer = contentsSynchronizer;
			this.contentsSynchronizerClient = contentsSynchronizerClient;
			this.options = options;
			this.additionalHeaderProperties = additionalHeaderProperties;
			if ((this.contentsSynchronizer != null && (this.options & IcsContentsSynchronizer.Options.PartialItem) == IcsContentsSynchronizer.Options.PartialItem && (this.options & IcsContentsSynchronizer.Options.ManifestMode) == IcsContentsSynchronizer.Options.None) || this.contentsSynchronizerClient != null)
			{
				this.partialItemState = new IcsPartialItemState();
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeProgressInfo(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.SerializeMessageChanges(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(IcsContentsSynchronizer.SerializeFixedPropertyBag(context, this.contentsSynchronizer.GetDeletions(), PropertyTag.IncrSyncDel, IcsContentsSynchronizer.deletionsPropertyTags)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(IcsContentsSynchronizer.SerializeFixedPropertyBag(context, this.contentsSynchronizer.GetReadUnreadStateChanges(), PropertyTag.IncrSyncRead, IcsContentsSynchronizer.readUnreadStateChangesPropertyTags)));
			FastTransferIcsState state = this.CreateDownloadFinalState();
			yield return new FastTransferStateMachine?(context.CreateStateMachine(state));
			context.DataInterface.PutMarker(PropertyTag.IncrSyncEnd);
			yield break;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001AA64 File Offset: 0x00018C64
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.ParseProgressInformation(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(this.ParseMessageChanges(context)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(IcsContentsSynchronizer.ParsePropertyBag(context, this.contentsSynchronizerClient.LoadDeletionPropertyBag(), PropertyTag.IncrSyncDel)));
			yield return new FastTransferStateMachine?(new FastTransferStateMachine(IcsContentsSynchronizer.ParsePropertyBag(context, this.contentsSynchronizerClient.LoadReadUnreadPropertyBag(), PropertyTag.IncrSyncRead)));
			FastTransferIcsState state = this.CreateUploadFinalState();
			yield return new FastTransferStateMachine?(context.CreateStateMachine(state));
			context.DataInterface.ReadMarker(PropertyTag.IncrSyncEnd);
			yield break;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001AA87 File Offset: 0x00018C87
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.contentsSynchronizer);
			Util.DisposeIfPresent(this.contentsSynchronizerClient);
			base.InternalDispose();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001AAA5 File Offset: 0x00018CA5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsContentsSynchronizer>(this);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001ABAC File Offset: 0x00018DAC
		private static IEnumerator<FastTransferStateMachine?> SerializeFixedPropertyBag(FastTransferDownloadContext context, IPropertyBag propertyBag, PropertyTag beginMarker, IList<PropertyTag> propertyTagsSuperset)
		{
			if (!propertyBag.GetAnnotatedProperties().IsEmpty<AnnotatedPropertyValue>())
			{
				context.DataInterface.PutMarker(beginMarker);
				yield return null;
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(propertyBag, propertyTagsSuperset)
				{
					SkipPropertyError = true
				}));
			}
			yield break;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001ACDC File Offset: 0x00018EDC
		private static IEnumerator<FastTransferStateMachine?> ParsePropertyBag(FastTransferUploadContext context, IPropertyBag propertyBag, PropertyTag beginMarker)
		{
			PropertyTag marker = default(PropertyTag);
			context.DataInterface.TryPeekMarker(out marker);
			if (marker == beginMarker)
			{
				context.DataInterface.ReadMarker(beginMarker);
				yield return null;
				yield return new FastTransferStateMachine?(context.CreateStateMachine(new FastTransferPropList(propertyBag)));
			}
			yield break;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001AD08 File Offset: 0x00018F08
		private FastTransferIcsState CreateDownloadFinalState()
		{
			FastTransferIcsState result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IIcsState finalState = this.contentsSynchronizer.GetFinalState();
				disposeGuard.Add<IIcsState>(finalState);
				FastTransferIcsState fastTransferIcsState = new FastTransferIcsState(finalState);
				disposeGuard.Add<FastTransferIcsState>(fastTransferIcsState);
				disposeGuard.Success();
				result = fastTransferIcsState;
			}
			return result;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001AEB4 File Offset: 0x000190B4
		private IEnumerator<FastTransferStateMachine?> SerializeProgressInfo(FastTransferDownloadContext context)
		{
			if ((this.options & IcsContentsSynchronizer.Options.ProgressMode) == IcsContentsSynchronizer.Options.ProgressMode)
			{
				context.DataInterface.PutMarker(PropertyTag.IncrSyncProgressMode);
				yield return null;
				byte[] buffer = new byte[32];
				PropertyValue progressInformation;
				using (Writer writer = new BufferWriter(buffer))
				{
					this.contentsSynchronizer.ProgressInformation.Serialize(writer);
					progressInformation = new PropertyValue(IcsContentsSynchronizer.progressInformationPropertyTag, buffer);
				}
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, progressInformation));
			}
			yield break;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001B02C File Offset: 0x0001922C
		private IEnumerator<FastTransferStateMachine?> SerializeMessageChanges(FastTransferDownloadContext context)
		{
			using (IEnumerator<IMessageChange> messageChanges = this.contentsSynchronizer.GetChanges())
			{
				while (messageChanges.MoveNext())
				{
					IMessageChange messageChange = messageChanges.Current;
					FastTransferMessageChange fastTransferMessageChange = this.CreateDownloadFastTransferMessageChange(messageChange);
					yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessageChange));
				}
			}
			yield break;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001B050 File Offset: 0x00019250
		private FastTransferMessageChange CreateDownloadFastTransferMessageChange(IMessageChange messageChange)
		{
			FastTransferMessageChange result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IMessageChange>(messageChange);
				FastTransferMessageChange fastTransferMessageChange = new FastTransferMessageChange(messageChange, this.options, this.additionalHeaderProperties, this.partialItemState, base.IsTopLevel);
				disposeGuard.Add<FastTransferMessageChange>(fastTransferMessageChange);
				disposeGuard.Success();
				result = fastTransferMessageChange;
			}
			return result;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001B0C0 File Offset: 0x000192C0
		private FastTransferIcsState CreateUploadFinalState()
		{
			FastTransferIcsState result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IIcsState icsState = this.contentsSynchronizerClient.LoadFinalState();
				disposeGuard.Add<IIcsState>(icsState);
				FastTransferIcsState fastTransferIcsState = new FastTransferIcsState(icsState);
				disposeGuard.Add<FastTransferIcsState>(fastTransferIcsState);
				disposeGuard.Success();
				result = fastTransferIcsState;
			}
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001B288 File Offset: 0x00019488
		private IEnumerator<FastTransferStateMachine?> ParseProgressInformation(FastTransferUploadContext context)
		{
			PropertyTag marker = default(PropertyTag);
			context.DataInterface.TryPeekMarker(out marker);
			if (marker == PropertyTag.IncrSyncProgressMode)
			{
				context.DataInterface.ReadMarker(PropertyTag.IncrSyncProgressMode);
				yield return null;
				SingleMemberPropertyBag singleMemberPropertyBag = new SingleMemberPropertyBag(IcsContentsSynchronizer.progressInformationPropertyTag);
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, singleMemberPropertyBag));
				using (Reader reader = Reader.CreateBufferReader(singleMemberPropertyBag.PropertyValue.GetValue<byte[]>()))
				{
					this.contentsSynchronizerClient.SetProgressInformation(ProgressInformation.Parse(reader));
					yield break;
				}
			}
			yield break;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001B378 File Offset: 0x00019578
		private IEnumerator<FastTransferStateMachine?> ParseMessageChanges(FastTransferUploadContext context)
		{
			PropertyTag marker = default(PropertyTag);
			while (context.DataInterface.TryPeekMarker(out marker) && FastTransferMessageChange.IsMessageBeginMarker(marker))
			{
				FastTransferMessageChange fastTransferMessageChange = this.CreateUploadFastTransferMessageChange();
				yield return new FastTransferStateMachine?(context.CreateStateMachine(fastTransferMessageChange));
			}
			yield break;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001B39C File Offset: 0x0001959C
		private FastTransferMessageChange CreateUploadFastTransferMessageChange()
		{
			FastTransferMessageChange result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMessageChangeClient messageChangeClient = this.contentsSynchronizerClient.UploadMessageChange();
				disposeGuard.Add<IMessageChangeClient>(messageChangeClient);
				FastTransferMessageChange fastTransferMessageChange = new FastTransferMessageChange(messageChangeClient, this.options, this.partialItemState, base.IsTopLevel);
				disposeGuard.Add<FastTransferMessageChange>(fastTransferMessageChange);
				disposeGuard.Success();
				result = fastTransferMessageChange;
			}
			return result;
		}

		// Token: 0x040003BF RID: 959
		private static readonly PropertyTag progressInformationPropertyTag = new PropertyTag(PropertyId.Null, PropertyType.Binary);

		// Token: 0x040003C0 RID: 960
		private static readonly PropertyTag[] deletionsPropertyTags = new PropertyTag[]
		{
			PropertyTag.IdsetDeleted,
			PropertyTag.IdsetSoftDeleted,
			PropertyTag.IdsetExpired
		};

		// Token: 0x040003C1 RID: 961
		private static readonly PropertyTag[] readUnreadStateChangesPropertyTags = new PropertyTag[]
		{
			PropertyTag.IdsetRead,
			PropertyTag.IdsetUnread
		};

		// Token: 0x040003C2 RID: 962
		private readonly IContentsSynchronizer contentsSynchronizer;

		// Token: 0x040003C3 RID: 963
		private readonly IContentsSynchronizerClient contentsSynchronizerClient;

		// Token: 0x040003C4 RID: 964
		private readonly IcsContentsSynchronizer.Options options;

		// Token: 0x040003C5 RID: 965
		private readonly IEnumerable<PropertyTag> additionalHeaderProperties;

		// Token: 0x040003C6 RID: 966
		private readonly IcsPartialItemState partialItemState;

		// Token: 0x02000185 RID: 389
		[Flags]
		internal enum Options
		{
			// Token: 0x040003C8 RID: 968
			None = 0,
			// Token: 0x040003C9 RID: 969
			ProgressMode = 1,
			// Token: 0x040003CA RID: 970
			IncludeMid = 2,
			// Token: 0x040003CB RID: 971
			IncludeMessageSize = 4,
			// Token: 0x040003CC RID: 972
			IncludeChangeNumber = 8,
			// Token: 0x040003CD RID: 973
			ManifestMode = 16,
			// Token: 0x040003CE RID: 974
			PartialItem = 32,
			// Token: 0x040003CF RID: 975
			Conversations = 64,
			// Token: 0x040003D0 RID: 976
			IncludeReadChangeNumber = 128
		}
	}
}
