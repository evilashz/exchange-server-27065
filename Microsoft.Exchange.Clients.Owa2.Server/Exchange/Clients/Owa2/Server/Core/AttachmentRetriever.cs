using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000035 RID: 53
	internal class AttachmentRetriever : DisposeTrackableBase, AttachmentHandler.IAttachmentRetriever, IDisposable
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00004D68 File Offset: 0x00002F68
		private AttachmentRetriever(string id, IdConverterDependencies converterDependencies)
		{
			bool flag = false;
			try
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string>((long)this.GetHashCode(), "Initialize IdAndSession to get attachment, id = {0}", id);
				List<AttachmentId> attachmentIds = new List<AttachmentId>();
				IdHeaderInformation headerInformation = ServiceIdConverter.ConvertFromConcatenatedId(id, BasicTypes.ItemOrAttachment, attachmentIds);
				Item item = null;
				IdAndSession idAndSession;
				try
				{
					idAndSession = IdConverter.ConvertId(converterDependencies, headerInformation, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.NoBind, BasicTypes.ItemOrAttachment, attachmentIds, null, this.GetHashCode(), false, ref item);
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
						item = null;
					}
				}
				this.attachmentHierarchy = new AttachmentHierarchy(idAndSession, false, true);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004E04 File Offset: 0x00003004
		public BlockStatus BlockStatus
		{
			get
			{
				if (this.blockStatus == null)
				{
					this.blockStatus = new BlockStatus?(BlockStatus.DontKnow);
					object obj = this.attachmentHierarchy.RootItem.TryGetProperty(ItemSchema.BlockStatus);
					if (obj is BlockStatus)
					{
						this.blockStatus = new BlockStatus?((BlockStatus)obj);
					}
				}
				return this.blockStatus.Value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004E64 File Offset: 0x00003064
		public Attachment Attachment
		{
			get
			{
				if (this.attachmentHierarchy.Last != null)
				{
					return this.attachmentHierarchy.Last.Attachment;
				}
				return null;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004E85 File Offset: 0x00003085
		public Item RootItem
		{
			get
			{
				return this.attachmentHierarchy.RootItem;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004E94 File Offset: 0x00003094
		internal static AttachmentHandler.IAttachmentRetriever CreateInstance(string id, CallContext callContext)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id cannot be null or empty");
			}
			if (callContext == null)
			{
				throw new ArgumentException("callContext cannot be null");
			}
			IdConverterDependencies converterDependencies = new IdConverterDependencies.FromCallContext(callContext);
			return new AttachmentRetriever(id, converterDependencies);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004ED0 File Offset: 0x000030D0
		internal static AttachmentHandler.IAttachmentRetriever CreateInstance(string id, IdConverterDependencies converterDependencies)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id cannot be null or empty");
			}
			if (converterDependencies == null)
			{
				throw new ArgumentException("converterDependencies cannot be null");
			}
			return new AttachmentRetriever(id, converterDependencies);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004EFA File Offset: 0x000030FA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.attachmentHierarchy != null)
			{
				this.attachmentHierarchy.Dispose();
				this.attachmentHierarchy = null;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004F19 File Offset: 0x00003119
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentRetriever>(this);
		}

		// Token: 0x04000071 RID: 113
		private BlockStatus? blockStatus;

		// Token: 0x04000072 RID: 114
		private AttachmentHierarchy attachmentHierarchy;
	}
}
