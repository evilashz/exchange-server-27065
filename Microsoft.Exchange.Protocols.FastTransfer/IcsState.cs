using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000006 RID: 6
	internal class IcsState : DisposableBase, IIcsState, IDisposable, ISession
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002504 File Offset: 0x00000704
		public IcsState()
		{
			this.statePropBag = new MemoryPropertyBag(this, ConfigurationSchema.MaxIcsStatePropertySize.Value);
			this.idsetGiven = new IcsState.IdSetWrapper(new PropertyTag(PropTag.IcsState.IdsetGiven.PropTag));
			this.cnsetSeen = new IcsState.IdSetWrapper(new PropertyTag(PropTag.IcsState.CnsetSeen.PropTag));
			this.cnsetSeenAssociated = new IcsState.IdSetWrapper(new PropertyTag(PropTag.IcsState.CnsetSeenFAI.PropTag));
			this.cnsetRead = new IcsState.IdSetWrapper(new PropertyTag(PropTag.IcsState.CnsetRead.PropTag));
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025A1 File Offset: 0x000007A1
		public IdSet IdsetGiven
		{
			get
			{
				if (this.idsetGivenNeedsReload)
				{
					this.idsetGiven.Load(this.statePropBag);
					this.idsetGivenNeedsReload = false;
				}
				return this.idsetGiven.IdSet;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025CE File Offset: 0x000007CE
		public IdSet CnsetSeen
		{
			get
			{
				if (this.cnsetSeenNeedsReload)
				{
					this.cnsetSeen.Load(this.statePropBag);
					this.cnsetSeenNeedsReload = false;
				}
				return this.cnsetSeen.IdSet;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025FB File Offset: 0x000007FB
		public IdSet CnsetSeenAssociated
		{
			get
			{
				if (this.cnsetSeenAssociatedNeedsReload)
				{
					this.cnsetSeenAssociated.Load(this.statePropBag);
					this.cnsetSeenAssociatedNeedsReload = false;
				}
				return this.cnsetSeenAssociated.IdSet;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002628 File Offset: 0x00000828
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002655 File Offset: 0x00000855
		public IdSet CnsetRead
		{
			get
			{
				if (this.cnsetReadNeedsReload)
				{
					this.cnsetRead.Load(this.statePropBag);
					this.cnsetReadNeedsReload = false;
				}
				return this.cnsetRead.IdSet;
			}
			set
			{
				this.cnsetRead.IdSet = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002663 File Offset: 0x00000863
		public bool StateUploadStarted
		{
			get
			{
				return this.statePropertyUploadStream != null;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002671 File Offset: 0x00000871
		IPropertyBag IIcsState.PropertyBag
		{
			get
			{
				this.Checkpoint();
				return this.statePropBag;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000267F File Offset: 0x0000087F
		void IIcsState.Flush()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002681 File Offset: 0x00000881
		bool ISession.TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty)
		{
			namedProperty = null;
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002687 File Offset: 0x00000887
		bool ISession.TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag)
		{
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000268C File Offset: 0x0000088C
		public ErrorCode BeginUploadStateProperty(StorePropTag propTag, uint size)
		{
			if (this.StateUploadStarted)
			{
				return ErrorCode.CreateCallFailed((LID)41112U);
			}
			if (propTag != PropTag.IcsState.IdsetGiven && propTag != PropTag.IcsState.IdsetGivenInt32 && propTag != PropTag.IcsState.CnsetSeen && propTag != PropTag.IcsState.CnsetSeenFAI && propTag != PropTag.IcsState.CnsetRead)
			{
				return ErrorCode.CreateNotSupported((LID)57496U);
			}
			this.statePropertyUploadStream = this.OpenPropertyWriteStream(propTag, size);
			this.declaredStatePropertySize = size;
			return ErrorCode.NoError;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000271C File Offset: 0x0000091C
		public ErrorCode ContinueUploadStateProperty(ArraySegment<byte> data)
		{
			if (!this.StateUploadStarted)
			{
				return ErrorCode.CreateCallFailed((LID)32920U);
			}
			if (this.statePropertyUploadStream.Length + (long)data.Count > (long)((ulong)this.declaredStatePropertySize))
			{
				return ErrorCode.CreateNotSupported((LID)49304U);
			}
			this.statePropertyUploadStream.Write(data.Array, data.Offset, data.Count);
			return ErrorCode.NoError;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002794 File Offset: 0x00000994
		public ErrorCode EndUploadStateProperty()
		{
			if (!this.StateUploadStarted)
			{
				return ErrorCode.CreateCallFailed((LID)48792U);
			}
			ErrorCode result = ErrorCode.NoError;
			if (this.statePropertyUploadStream.Length != (long)((ulong)this.declaredStatePropertySize))
			{
				result = ErrorCode.CreateCallFailed((LID)65176U);
			}
			this.statePropertyUploadStream.Dispose();
			this.statePropertyUploadStream = null;
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002800 File Offset: 0x00000A00
		public ErrorCode OpenIcsStateDownloadContext(MapiLogon logon, out FastTransferDownloadContext outputContext)
		{
			if (this.statePropertyUploadStream != null)
			{
				throw new ExExceptionNoSupport((LID)60216U, "Unfinished state property upload.");
			}
			FastTransferDownloadContext fastTransferDownloadContext = new FastTransferDownloadContext(Array<PropertyTag>.Empty);
			ErrorCode errorCode = fastTransferDownloadContext.Configure(logon, FastTransferSendOption.UseMAPI, (MapiContext operationContext) => new FastTransferIcsState(this));
			if (errorCode != ErrorCode.NoError)
			{
				fastTransferDownloadContext.Dispose();
				fastTransferDownloadContext = null;
			}
			outputContext = fastTransferDownloadContext;
			return errorCode;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002864 File Offset: 0x00000A64
		public Stream OpenPropertyWriteStream(StorePropTag propTag, uint size)
		{
			ushort propId = propTag.PropId;
			if (propId <= 26518)
			{
				if (propId != 16407)
				{
					if (propId == 26518)
					{
						this.cnsetSeenNeedsReload = true;
					}
				}
				else
				{
					this.idsetGivenNeedsReload = true;
					if (propTag == PropTag.IcsState.IdsetGivenInt32)
					{
						propTag = PropTag.IcsState.IdsetGiven;
					}
				}
			}
			else if (propId != 26578)
			{
				if (propId == 26586)
				{
					this.cnsetSeenAssociatedNeedsReload = true;
				}
			}
			else
			{
				this.cnsetReadNeedsReload = true;
			}
			return this.statePropBag.SetPropertyStream(new PropertyTag(propTag.PropTag), (long)((ulong)size));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028F4 File Offset: 0x00000AF4
		public void Checkpoint()
		{
			if (this.idsetGiven.IsDirty)
			{
				this.idsetGiven.Save(this.statePropBag);
			}
			if (this.cnsetSeen.IsDirty)
			{
				this.cnsetSeen.Save(this.statePropBag);
			}
			if (this.cnsetSeenAssociated.IsDirty)
			{
				this.cnsetSeenAssociated.Save(this.statePropBag);
			}
			if (this.cnsetRead.IsDirty)
			{
				this.cnsetRead.Save(this.statePropBag);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000297C File Offset: 0x00000B7C
		public void ReloadIfNecessary()
		{
			if (this.idsetGivenNeedsReload)
			{
				this.idsetGiven.Load(this.statePropBag);
				this.idsetGivenNeedsReload = false;
			}
			if (this.cnsetSeenNeedsReload)
			{
				this.cnsetSeen.Load(this.statePropBag);
				this.cnsetSeenNeedsReload = false;
			}
			if (this.cnsetSeenAssociatedNeedsReload)
			{
				this.cnsetSeenAssociated.Load(this.statePropBag);
				this.cnsetSeenAssociatedNeedsReload = false;
			}
			if (this.cnsetReadNeedsReload)
			{
				this.cnsetRead.Load(this.statePropBag);
				this.cnsetReadNeedsReload = false;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A0C File Offset: 0x00000C0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("IdsetGiven:");
			stringBuilder.Append(this.idsetGiven.IdSet.ToString());
			stringBuilder.Append("  CnsetSeen:");
			stringBuilder.Append(this.cnsetSeen.IdSet.ToString());
			stringBuilder.Append("  CnsetSeenAssoc:");
			stringBuilder.Append(this.cnsetSeenAssociated.IdSet.ToString());
			stringBuilder.Append("  CnsetRead:");
			stringBuilder.Append(this.cnsetRead.IdSet.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AB3 File Offset: 0x00000CB3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IcsState>(this);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002ABB File Offset: 0x00000CBB
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.statePropertyUploadStream != null)
			{
				this.statePropertyUploadStream.Dispose();
				this.statePropertyUploadStream = null;
			}
		}

		// Token: 0x0400000C RID: 12
		public static readonly ExchangeId PerUserIdsetIndicator = ExchangeId.Create(ReplidGuidMap.ReservedPerUserIndicatorGuid, 1UL, 1);

		// Token: 0x0400000D RID: 13
		public static readonly ExchangeId NonPerUserIdsetIndicator = ExchangeId.Create(ReplidGuidMap.ReservedNonPerUserIndicatorGuid, 1UL, 1);

		// Token: 0x0400000E RID: 14
		private MemoryPropertyBag statePropBag;

		// Token: 0x0400000F RID: 15
		private IcsState.IdSetWrapper idsetGiven;

		// Token: 0x04000010 RID: 16
		private bool idsetGivenNeedsReload;

		// Token: 0x04000011 RID: 17
		private IcsState.IdSetWrapper cnsetSeen;

		// Token: 0x04000012 RID: 18
		private bool cnsetSeenNeedsReload;

		// Token: 0x04000013 RID: 19
		private IcsState.IdSetWrapper cnsetSeenAssociated;

		// Token: 0x04000014 RID: 20
		private bool cnsetSeenAssociatedNeedsReload;

		// Token: 0x04000015 RID: 21
		private IcsState.IdSetWrapper cnsetRead;

		// Token: 0x04000016 RID: 22
		private bool cnsetReadNeedsReload;

		// Token: 0x04000017 RID: 23
		private Stream statePropertyUploadStream;

		// Token: 0x04000018 RID: 24
		private uint declaredStatePropertySize;

		// Token: 0x02000007 RID: 7
		internal class IdSetWrapper
		{
			// Token: 0x0600003B RID: 59 RVA: 0x00002B00 File Offset: 0x00000D00
			public IdSetWrapper(PropertyTag propertyTag)
			{
				this.propertyTag = propertyTag;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B0F File Offset: 0x00000D0F
			public bool IsDirty
			{
				get
				{
					return this.idset != null && this.idset.IsDirty;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B26 File Offset: 0x00000D26
			// (set) Token: 0x0600003E RID: 62 RVA: 0x00002B41 File Offset: 0x00000D41
			public IdSet IdSet
			{
				get
				{
					if (this.idset == null)
					{
						this.idset = new IdSet();
					}
					return this.idset;
				}
				set
				{
					this.idset = value;
					this.idset.IsDirty = true;
				}
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00002B58 File Offset: 0x00000D58
			public void Load(IPropertyBag propertyBag)
			{
				IdSet idSet = null;
				if (!propertyBag.GetAnnotatedProperty(this.propertyTag).PropertyValue.IsNotFound)
				{
					using (Stream propertyStream = propertyBag.GetPropertyStream(this.propertyTag))
					{
						using (Reader reader = Reader.CreateStreamReader(propertyStream))
						{
							try
							{
								idSet = IdSet.ThrowableParse(reader);
							}
							catch (StoreException innerException)
							{
								throw new RopExecutionException("Invalid IdSet format.", ErrorCode.IdSetFormatError, innerException);
							}
							long num = propertyStream.Length - propertyStream.Position;
							if (num != 0L)
							{
								throw new RopExecutionException(string.Format("Property stream contained {0} more bytes after parsing an IdSet", num), ErrorCode.IdSetFormatError);
							}
						}
					}
				}
				this.idset = idSet;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002C2C File Offset: 0x00000E2C
			public void Save(IPropertyBag propertyBag)
			{
				if (this.idset != null)
				{
					using (Stream stream = propertyBag.SetPropertyStream(this.propertyTag, 512L))
					{
						using (Writer writer = new StreamWriter(stream))
						{
							this.idset.Serialize(writer);
						}
						goto IL_5D;
					}
				}
				propertyBag.SetProperty(new PropertyValue(this.propertyTag, new byte[0]));
				IL_5D:
				this.idset.IsDirty = false;
			}

			// Token: 0x04000019 RID: 25
			public const int DefaultIcsStateStreamCapacity = 512;

			// Token: 0x0400001A RID: 26
			private PropertyTag propertyTag;

			// Token: 0x0400001B RID: 27
			private IdSet idset;
		}
	}
}
