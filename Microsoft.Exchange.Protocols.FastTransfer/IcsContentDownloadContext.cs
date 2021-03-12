using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200000E RID: 14
	internal class IcsContentDownloadContext : IcsDownloadContext
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004D25 File Offset: 0x00002F25
		public ErrorCode Configure(MapiLogon logon, IContentSynchronizationScope scope, FastTransferSendOption sendOptions, SyncFlag syncFlags, SyncExtraFlag extraFlags, StorePropTag[] propertyTags, ExchangeId[] messageIds)
		{
			this.scope = scope;
			this.syncFlags = syncFlags;
			this.extraFlags = extraFlags;
			this.propertyTags = propertyTags;
			this.messageIds = messageIds;
			return base.Configure(logon, sendOptions);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004D56 File Offset: 0x00002F56
		public SyncFlag SyncFlags
		{
			get
			{
				return this.syncFlags;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004D5E File Offset: 0x00002F5E
		public SyncExtraFlag ExtraFlags
		{
			get
			{
				return this.extraFlags;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004D66 File Offset: 0x00002F66
		public StorePropTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004D6E File Offset: 0x00002F6E
		public IContentSynchronizationScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004D76 File Offset: 0x00002F76
		public ExchangeId[] MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004D80 File Offset: 0x00002F80
		public override IChunked PrepareIndexes(MapiContext context)
		{
			if (!ConfigurationSchema.ChunkedIndexPopulationEnabled.Value)
			{
				return null;
			}
			if (!this.indexesPrepared)
			{
				this.indexesPrepared = true;
				ContentSynchronizationScopeBase contentSynchronizationScopeBase = this.scope as ContentSynchronizationScopeBase;
				if (contentSynchronizationScopeBase != null)
				{
					return contentSynchronizationScopeBase.PrepareIndexes(context, base.IcsState);
				}
			}
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004DC8 File Offset: 0x00002FC8
		protected override IFastTransferProcessor<FastTransferDownloadContext> GetFastTransferProcessor(MapiContext operationContext)
		{
			IcsContentDownloadContext.ContentsSynchronizer contentsSynchronizer = new IcsContentDownloadContext.ContentsSynchronizer(operationContext, this, this.scope, base.IcsState, this.SyncFlags, this.ExtraFlags);
			IcsContentsSynchronizer.Options options = IcsContentsSynchronizer.Options.None;
			if ((this.ExtraFlags & SyncExtraFlag.Eid) != SyncExtraFlag.None)
			{
				options |= IcsContentsSynchronizer.Options.IncludeMid;
			}
			if ((this.ExtraFlags & SyncExtraFlag.MessageSize) != SyncExtraFlag.None)
			{
				options |= IcsContentsSynchronizer.Options.IncludeMessageSize;
			}
			if ((this.ExtraFlags & SyncExtraFlag.Cn) != SyncExtraFlag.None)
			{
				options |= IcsContentsSynchronizer.Options.IncludeChangeNumber;
			}
			if ((this.ExtraFlags & SyncExtraFlag.ReadCn) != SyncExtraFlag.None)
			{
				options |= IcsContentsSynchronizer.Options.IncludeReadChangeNumber;
			}
			if ((ushort)(this.SyncFlags & SyncFlag.ProgressMode) != 0)
			{
				options |= IcsContentsSynchronizer.Options.ProgressMode;
			}
			if ((byte)(base.SendOptions & FastTransferSendOption.PartialItem) != 0)
			{
				options |= IcsContentsSynchronizer.Options.PartialItem;
			}
			if ((ushort)(this.SyncFlags & SyncFlag.Conversations) != 0)
			{
				options |= IcsContentsSynchronizer.Options.Conversations;
			}
			List<PropertyTag> list = null;
			if ((this.ExtraFlags & SyncExtraFlag.ManifestMode) != SyncExtraFlag.None)
			{
				options |= IcsContentsSynchronizer.Options.ManifestMode;
				if (this.PropertyTags != null && this.PropertyTags.Length != 0)
				{
					list = new List<PropertyTag>(this.PropertyTags.Length);
					foreach (StorePropTag storePropTag in this.PropertyTags)
					{
						list.Add(new PropertyTag(storePropTag.PropTag));
					}
				}
			}
			return new IcsContentsSynchronizer(contentsSynchronizer, options, list);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004EE5 File Offset: 0x000030E5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IcsContentDownloadContext>(this);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004EED File Offset: 0x000030ED
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.scope != null)
			{
				this.scope.Dispose();
				this.scope = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0400003F RID: 63
		private IContentSynchronizationScope scope;

		// Token: 0x04000040 RID: 64
		private SyncFlag syncFlags;

		// Token: 0x04000041 RID: 65
		private SyncExtraFlag extraFlags;

		// Token: 0x04000042 RID: 66
		private StorePropTag[] propertyTags;

		// Token: 0x04000043 RID: 67
		private ExchangeId[] messageIds;

		// Token: 0x04000044 RID: 68
		private bool indexesPrepared;

		// Token: 0x0200000F RID: 15
		internal class ContentsSynchronizer : DisposableBase, IContentsSynchronizer, IDisposable
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x00004F1C File Offset: 0x0000311C
			public ContentsSynchronizer(MapiContext operationContext, IcsContentDownloadContext context, IContentSynchronizationScope scope, IcsState state, SyncFlag syncFlags, SyncExtraFlag extraFlags)
			{
				state.ReloadIfNecessary();
				if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("InitialState=[");
					stringBuilder.Append(state.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				this.context = context;
				this.scope = scope;
				this.state = state;
				this.syncFlags = syncFlags;
				this.extraFlags = extraFlags;
				this.cnsetSeenServer = scope.GetServerCnsetSeen(operationContext, (ushort)(this.syncFlags & SyncFlag.Conversations) != 0);
				if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder2 = new StringBuilder(100);
					stringBuilder2.Append("Server CnsetSeen=[");
					stringBuilder2.Append(this.cnsetSeenServer.ToString());
					stringBuilder2.Append("]");
					ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder2.ToString());
				}
				if ((ushort)(this.syncFlags & SyncFlag.CatchUp) == 0 || ((this.extraFlags & SyncExtraFlag.CatchUpFull) != SyncExtraFlag.None && (ushort)(this.syncFlags & SyncFlag.Conversations) == 0))
				{
					this.changedMessages = scope.GetChangedMessages(operationContext, state);
					if ((ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
					{
						if ((ushort)(this.syncFlags & SyncFlag.NoDeletions) == 0)
						{
							this.idsetDeletes = scope.GetDeletes(operationContext, state);
							if ((ushort)(this.syncFlags & SyncFlag.NoSoftDeletions) == 0)
							{
								this.idsetSoftDeletes = scope.GetSoftDeletes(operationContext, state);
							}
						}
						if ((ushort)(this.syncFlags & SyncFlag.ReadState) != 0)
						{
							scope.GetNewReadsUnreads(operationContext, state, out this.idsetNewReads, out this.idsetNewUnreads, out this.finalCnsetRead);
						}
					}
				}
				if ((ushort)(this.syncFlags & SyncFlag.ProgressMode) != 0)
				{
					int num = 0;
					long num2 = 0L;
					int num3 = 0;
					long num4 = 0L;
					if (this.changedMessages != null)
					{
						foreach (Properties properties in this.changedMessages)
						{
							bool flag = (bool)properties[4].Value;
							uint num5 = (uint)((int)properties[5].Value);
							if (flag)
							{
								num++;
								num2 += (long)((ulong)num5);
							}
							else
							{
								num3++;
								num4 += (long)((ulong)num5);
							}
						}
					}
					this.progressInfo = new ProgressInformation(0, num3, num, (ulong)num4, (ulong)num2);
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005184 File Offset: 0x00003384
			public ProgressInformation ProgressInformation
			{
				get
				{
					return this.progressInfo;
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000056AC File Offset: 0x000038AC
			public IEnumerator<IMessageChange> GetChanges()
			{
				if (this.changedMessages != null)
				{
					if (((this.extraFlags & SyncExtraFlag.NoChanges) != SyncExtraFlag.None || (this.extraFlags & SyncExtraFlag.CatchUpFull) != SyncExtraFlag.None) && (ushort)(this.syncFlags & SyncFlag.MessageSelective) == 0)
					{
						if ((ushort)(this.syncFlags & SyncFlag.Normal) != 0 || (ushort)(this.syncFlags & SyncFlag.Conversations) != 0)
						{
							this.state.CnsetSeen.Insert(this.cnsetSeenServer);
						}
						if ((ushort)(this.syncFlags & SyncFlag.Associated) != 0)
						{
							this.state.CnsetSeenAssociated.Insert(this.cnsetSeenServer);
						}
					}
					foreach (Properties messageHeader in this.changedMessages)
					{
						bool skipItem = false;
						if ((this.extraFlags & SyncExtraFlag.NoChanges) == SyncExtraFlag.None && (this.extraFlags & SyncExtraFlag.CatchUpFull) == SyncExtraFlag.None)
						{
							IcsContentDownloadContext.MessageChange messageChange = null;
							bool success = false;
							try
							{
								messageChange = new IcsContentDownloadContext.MessageChange(this.context, this.scope, this.syncFlags, messageHeader);
								if ((ushort)(this.syncFlags & SyncFlag.Conversations) != 0 || (this.extraFlags & SyncExtraFlag.ManifestMode) != SyncExtraFlag.None || messageChange.FastTransferMessage != null)
								{
									success = true;
									yield return messageChange;
								}
								else
								{
									DiagnosticContext.TraceLocation((LID)35936U);
									skipItem = true;
								}
							}
							finally
							{
								if (!success && messageChange != null)
								{
									messageChange.Dispose();
								}
							}
						}
						if (!skipItem)
						{
							Context currentOperationContext = this.context.CurrentOperationContext;
							IReplidGuidMap replidGuidMap = this.context.Logon.StoreMailbox.ReplidGuidMap;
							Properties properties = messageHeader;
							ExchangeId id = ExchangeId.CreateFrom9ByteArray(currentOperationContext, replidGuidMap, (byte[])properties[1].Value);
							if (this.cnsetSeenServer.Contains(id))
							{
								if ((ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
								{
									Properties properties2 = messageHeader;
									if ((bool)properties2[4].Value)
									{
										this.state.CnsetSeenAssociated.Insert(id);
										goto IL_2CB;
									}
								}
								this.state.CnsetSeen.Insert(id);
							}
							IL_2CB:
							if ((ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
							{
								Context currentOperationContext2 = this.context.CurrentOperationContext;
								IReplidGuidMap replidGuidMap2 = this.context.Logon.StoreMailbox.ReplidGuidMap;
								Properties properties3 = messageHeader;
								ExchangeId id2 = ExchangeId.CreateFromInt64(currentOperationContext2, replidGuidMap2, (long)properties3[0].Value);
								this.state.IdsetGiven.Insert(id2);
							}
						}
					}
				}
				if ((ushort)(this.syncFlags & SyncFlag.MessageSelective) == 0)
				{
					if ((ushort)(this.syncFlags & SyncFlag.Normal) != 0 || (ushort)(this.syncFlags & SyncFlag.Conversations) != 0)
					{
						this.state.CnsetSeen.Insert(this.cnsetSeenServer);
						this.state.CnsetSeen.IdealPack();
					}
					if ((ushort)(this.syncFlags & SyncFlag.Associated) != 0)
					{
						this.state.CnsetSeenAssociated.Insert(this.cnsetSeenServer);
						this.state.CnsetSeenAssociated.IdealPack();
					}
				}
				yield break;
			}

			// Token: 0x060000AA RID: 170 RVA: 0x000056C8 File Offset: 0x000038C8
			public IPropertyBag GetDeletions()
			{
				MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag(this.context);
				if ((this.extraFlags & SyncExtraFlag.CatchUpFull) == SyncExtraFlag.None)
				{
					if (this.idsetDeletes != null && !this.idsetDeletes.IsEmpty)
					{
						byte[] value = this.idsetDeletes.Serialize(new Func<Guid, ReplId>(this.scope.GuidToReplid));
						memoryPropertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetDeleted, value));
					}
					if (this.idsetSoftDeletes != null && !this.idsetSoftDeletes.IsEmpty)
					{
						byte[] value2 = this.idsetSoftDeletes.Serialize(new Func<Guid, ReplId>(this.scope.GuidToReplid));
						memoryPropertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetSoftDeleted, value2));
					}
				}
				return memoryPropertyBag;
			}

			// Token: 0x060000AB RID: 171 RVA: 0x00005778 File Offset: 0x00003978
			public IPropertyBag GetReadUnreadStateChanges()
			{
				if (this.idsetDeletes != null)
				{
					this.state.IdsetGiven.Remove(this.idsetDeletes);
				}
				if (this.idsetSoftDeletes != null)
				{
					this.state.IdsetGiven.Remove(this.idsetSoftDeletes);
				}
				MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag(this.context);
				if ((this.extraFlags & SyncExtraFlag.CatchUpFull) == SyncExtraFlag.None)
				{
					if (this.idsetNewReads != null)
					{
						byte[] value = this.idsetNewReads.Serialize(new Func<Guid, ReplId>(this.scope.GuidToReplid));
						memoryPropertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetRead, value));
					}
					if (this.idsetNewUnreads != null)
					{
						byte[] value2 = this.idsetNewUnreads.Serialize(new Func<Guid, ReplId>(this.scope.GuidToReplid));
						memoryPropertyBag.SetProperty(new PropertyValue(PropertyTag.IdsetUnread, value2));
					}
				}
				return memoryPropertyBag;
			}

			// Token: 0x060000AC RID: 172 RVA: 0x0000584C File Offset: 0x00003A4C
			public IIcsState GetFinalState()
			{
				if ((ushort)(this.syncFlags & SyncFlag.ReadState) != 0 && (ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
				{
					if (this.finalCnsetRead != null)
					{
						this.state.CnsetRead.Insert(this.finalCnsetRead);
					}
					if (this.state.CnsetRead.Contains(IcsState.NonPerUserIdsetIndicator))
					{
						this.state.CnsetRead.IdealPack();
					}
				}
				if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("FinalState=[");
					stringBuilder.Append(this.state.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				return this.state;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00005910 File Offset: 0x00003B10
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<IcsContentDownloadContext.ContentsSynchronizer>(this);
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00005918 File Offset: 0x00003B18
			protected override void InternalDispose(bool isCalledFromDispose)
			{
			}

			// Token: 0x04000045 RID: 69
			private const int MaxNumberOfChangesToKeepInMemory = 100;

			// Token: 0x04000046 RID: 70
			private IcsContentDownloadContext context;

			// Token: 0x04000047 RID: 71
			private IContentSynchronizationScope scope;

			// Token: 0x04000048 RID: 72
			private IcsState state;

			// Token: 0x04000049 RID: 73
			private SyncFlag syncFlags;

			// Token: 0x0400004A RID: 74
			private SyncExtraFlag extraFlags;

			// Token: 0x0400004B RID: 75
			private IdSet cnsetSeenServer;

			// Token: 0x0400004C RID: 76
			private IdSet idsetDeletes;

			// Token: 0x0400004D RID: 77
			private IdSet idsetSoftDeletes;

			// Token: 0x0400004E RID: 78
			private IdSet idsetNewReads;

			// Token: 0x0400004F RID: 79
			private IdSet idsetNewUnreads;

			// Token: 0x04000050 RID: 80
			private IdSet finalCnsetRead;

			// Token: 0x04000051 RID: 81
			private IEnumerable<Properties> changedMessages;

			// Token: 0x04000052 RID: 82
			private ProgressInformation progressInfo;
		}

		// Token: 0x02000010 RID: 16
		internal class MessageChange : DisposableBase, IMessageChange, IMessageChangePartial, IDisposable, IPropertyBag
		{
			// Token: 0x060000AF RID: 175 RVA: 0x0000591A File Offset: 0x00003B1A
			public MessageChange(IcsDownloadContext context, IContentSynchronizationScope scope, SyncFlag syncFlags, Properties headerValues)
			{
				this.context = context;
				this.scope = scope;
				this.syncFlags = syncFlags;
				this.headerValues = headerValues;
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000593F File Offset: 0x00003B3F
			public IMessage Message
			{
				get
				{
					return this.FastTransferMessage;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005948 File Offset: 0x00003B48
			public int MessageSize
			{
				get
				{
					if ((ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
					{
						return (int)this.headerValues[5].Value;
					}
					return 0;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005980 File Offset: 0x00003B80
			public bool IsAssociatedMessage
			{
				get
				{
					return (ushort)(this.syncFlags & SyncFlag.Conversations) == 0 && (bool)this.headerValues[4].Value;
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x000059B7 File Offset: 0x00003BB7
			public IPropertyBag MessageHeaderPropertyBag
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x000059BC File Offset: 0x00003BBC
			public IMessageChangePartial PartialChange
			{
				get
				{
					bool flag = this.CanDoPartialDownload();
					if (ExTraceGlobals.IcsDownloadTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExchangeId mid = this.FastTransferMessage.MapiMessage.Mid;
						ExchangeId fid = this.FastTransferMessage.MapiMessage.GetFid();
						StringBuilder stringBuilder = new StringBuilder(100);
						stringBuilder.Append(flag ? "ICS partial download" : "ICS full download");
						stringBuilder.Append(" - Fid:[");
						stringBuilder.Append(fid.ToString());
						stringBuilder.Append("] Mid=[");
						stringBuilder.Append(mid.ToString());
						if (flag)
						{
							stringBuilder.Append("] ChangedGroups:[");
							stringBuilder.AppendAsString(this.changedGroups);
						}
						stringBuilder.Append("]");
						ExTraceGlobals.IcsDownloadTracer.TraceDebug(0L, stringBuilder.ToString());
					}
					if (!flag)
					{
						return null;
					}
					return this;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005A9F File Offset: 0x00003C9F
			public PropertyGroupMapping PropertyGroupMapping
			{
				get
				{
					return this.scope.GetPropertyGroupMapping();
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005AAC File Offset: 0x00003CAC
			public IEnumerable<int> ChangedPropGroups
			{
				get
				{
					return this.changedGroups;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005ADC File Offset: 0x00003CDC
			public IEnumerable<PropertyTag> OtherGroupPropTags
			{
				get
				{
					PropertyGroupMapping propertyGroupMapping = this.scope.GetPropertyGroupMapping();
					return from propertyTag in this.FastTransferMessage.GetPropertyList()
					where !propertyGroupMapping.IsPropertyInAnyGroup(propertyTag) && ContentSynchronizationScope.ValidClientSideGroupProperty((ushort)propertyTag.PropertyId)
					select propertyTag;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005B1C File Offset: 0x00003D1C
			public FastTransferMessage FastTransferMessage
			{
				get
				{
					if (this.message == null)
					{
						ExchangeId exchangeId = this.scope.GetExchangeId((long)this.headerValues[0].Value);
						this.message = this.scope.OpenMessage(exchangeId);
					}
					return this.message;
				}
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00005B70 File Offset: 0x00003D70
			private PropertyValue GetProperty(PropertyTag property)
			{
				if (property == PropertyTag.SourceKey && (ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
				{
					object value;
					if (this.headerValues[6].IsError || this.headerValues[6].Value == null || (ushort)(this.syncFlags & SyncFlag.NoForeignKeys) != 0)
					{
						value = ExchangeId.CreateFromInt64(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox.ReplidGuidMap, (long)this.headerValues[0].Value).To22ByteArray();
						return RcaTypeHelpers.MassageOutgoingProperty(new Property(PropTag.Message.SourceKey, value), false);
					}
					value = this.headerValues[6].Value;
					return RcaTypeHelpers.MassageOutgoingProperty(new Property(PropTag.Message.SourceKey, value), false);
				}
				else if (property == PropertyTag.ChangeKey && (ushort)(this.syncFlags & SyncFlag.Conversations) == 0)
				{
					object value;
					if (this.headerValues[7].IsError || this.headerValues[7].Value == null)
					{
						value = ExchangeId.CreateFrom9ByteArray(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox.ReplidGuidMap, (byte[])this.headerValues[1].Value).To22ByteArray();
						return RcaTypeHelpers.MassageOutgoingProperty(new Property(PropTag.Message.ChangeKey, value), false);
					}
					value = this.headerValues[7].Value;
					return RcaTypeHelpers.MassageOutgoingProperty(new Property(PropTag.Message.ChangeKey, value), false);
				}
				else
				{
					if (property == PropertyTag.ChangeNumber)
					{
						object value = ExchangeIdHelpers.Convert9ByteToLong((byte[])this.headerValues[1].Value);
						return RcaTypeHelpers.MassageOutgoingProperty(new Property(PropTag.Message.ChangeNumber, value), false);
					}
					int num = this.FindProperty(property);
					if (num < 0)
					{
						return PropertyValue.Error(property.PropertyId, (ErrorCode)2147746063U);
					}
					return RcaTypeHelpers.MassageOutgoingProperty(this.headerValues[num], true);
				}
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00005DA4 File Offset: 0x00003FA4
			void IPropertyBag.SetProperty(PropertyValue propertyValue)
			{
				throw new System.NotSupportedException();
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00005DAB File Offset: 0x00003FAB
			private IEnumerable<PropertyTag> GetPropertyList()
			{
				throw new System.NotSupportedException();
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00005DB4 File Offset: 0x00003FB4
			AnnotatedPropertyValue IPropertyBag.GetAnnotatedProperty(PropertyTag propertyTag)
			{
				PropertyValue property = this.GetProperty(propertyTag);
				NamedProperty namedProperty = null;
				if (propertyTag.IsNamedProperty)
				{
					((IPropertyBag)this).Session.TryResolveToNamedProperty(propertyTag, out namedProperty);
				}
				return new AnnotatedPropertyValue(propertyTag, property, namedProperty);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00005F84 File Offset: 0x00004184
			IEnumerable<AnnotatedPropertyValue> IPropertyBag.GetAnnotatedProperties()
			{
				foreach (PropertyTag propertyTag in this.GetPropertyList())
				{
					yield return ((IPropertyBag)this).GetAnnotatedProperty(propertyTag);
				}
				yield break;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00005FA1 File Offset: 0x000041A1
			void IPropertyBag.Delete(PropertyTag property)
			{
				throw new System.NotSupportedException();
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00005FA8 File Offset: 0x000041A8
			Stream IPropertyBag.GetPropertyStream(PropertyTag property)
			{
				throw new System.NotSupportedException();
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00005FAF File Offset: 0x000041AF
			Stream IPropertyBag.SetPropertyStream(PropertyTag property, long dataSizeEstimate)
			{
				throw new System.NotSupportedException();
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005FB6 File Offset: 0x000041B6
			ISession IPropertyBag.Session
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00005FC0 File Offset: 0x000041C0
			private int FindProperty(PropertyTag property)
			{
				for (int i = 0; i < this.headerValues.Count; i++)
				{
					if (property.PropertyId == (PropertyId)this.headerValues[i].Tag.PropId)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x0000600C File Offset: 0x0000420C
			private bool CanDoPartialDownload()
			{
				ExchangeId mid = this.FastTransferMessage.MapiMessage.Mid;
				if (this.IsAssociatedMessage || !this.context.IcsState.IdsetGiven.Contains(mid))
				{
					return false;
				}
				PropGroupChangeInfo propGroupChangeInfo = this.FastTransferMessage.GetPropGroupChangeInfo();
				if (!propGroupChangeInfo.IsValid)
				{
					return false;
				}
				this.changedGroups = new List<int>(4);
				IdSet idSet = this.IsAssociatedMessage ? this.context.IcsState.CnsetSeenAssociated : this.context.IcsState.CnsetSeen;
				for (int i = 0; i < propGroupChangeInfo.Count; i++)
				{
					ExchangeId id = propGroupChangeInfo[i];
					if (!idSet.Contains(id))
					{
						this.changedGroups.Add(i);
					}
				}
				ExchangeId other = propGroupChangeInfo.Other;
				if (!idSet.Contains(other))
				{
					this.changedGroups.Add(-1);
				}
				return true;
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x000060EC File Offset: 0x000042EC
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<IcsContentDownloadContext.MessageChange>(this);
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x000060F4 File Offset: 0x000042F4
			protected override void InternalDispose(bool isCalledFromDispose)
			{
				if (isCalledFromDispose && this.message != null)
				{
					this.message.Dispose();
					this.message = null;
				}
			}

			// Token: 0x04000053 RID: 83
			public const int IndexOfMidColumn = 0;

			// Token: 0x04000054 RID: 84
			public const int IndexOfInternalChangeNumberColumn = 1;

			// Token: 0x04000055 RID: 85
			public const int IndexOfLastModificationTimeColumn = 2;

			// Token: 0x04000056 RID: 86
			public const int IndexOfMessageDeliveryTimeColumn = 3;

			// Token: 0x04000057 RID: 87
			public const int IndexOfAssociatedColumn = 4;

			// Token: 0x04000058 RID: 88
			public const int IndexOfMessageSizeColumn = 5;

			// Token: 0x04000059 RID: 89
			public const int IndexOfInternalSourceKeyColumn = 6;

			// Token: 0x0400005A RID: 90
			public const int IndexOfInternalChangeKeyColumn = 7;

			// Token: 0x0400005B RID: 91
			public const int IndexOfPredecessorChangeListColumn = 8;

			// Token: 0x0400005C RID: 92
			public const int IndexOfChangeTypeColumn = 3;

			// Token: 0x0400005D RID: 93
			public const int IndexOfConversationIdColumn = 4;

			// Token: 0x0400005E RID: 94
			public static StorePropTag[] StandardHeaderColumns = new StorePropTag[]
			{
				PropTag.Message.Mid,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.LastModificationTime,
				PropTag.Message.MessageDeliveryTime,
				PropTag.Message.Associated,
				PropTag.Message.MessageSize32,
				PropTag.Message.InternalSourceKey,
				PropTag.Message.InternalChangeKey,
				PropTag.Message.PredecessorChangeList
			};

			// Token: 0x0400005F RID: 95
			public static StorePropTag[] StandardConversationHeaderColumns = new StorePropTag[]
			{
				PropTag.Message.Mid,
				PropTag.Message.Internal9ByteChangeNumber,
				PropTag.Message.LastModificationTime,
				PropTag.Message.ChangeType,
				PropTag.Message.ConversationId
			};

			// Token: 0x04000060 RID: 96
			private IcsDownloadContext context;

			// Token: 0x04000061 RID: 97
			private IContentSynchronizationScope scope;

			// Token: 0x04000062 RID: 98
			private SyncFlag syncFlags;

			// Token: 0x04000063 RID: 99
			private Properties headerValues;

			// Token: 0x04000064 RID: 100
			private FastTransferMessage message;

			// Token: 0x04000065 RID: 101
			private List<int> changedGroups;
		}
	}
}
