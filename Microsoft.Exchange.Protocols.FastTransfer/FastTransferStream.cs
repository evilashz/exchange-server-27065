using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000028 RID: 40
	internal class FastTransferStream : MapiPropBagBase
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000DA07 File Offset: 0x0000BC07
		private FastTransferStream(FastTransferStreamMode mode) : base(MapiObjectType.FastTransferStream)
		{
			this.mode = mode;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public static FastTransferStream CreateNew(MapiContext context, MapiLogon logon, FastTransferStreamMode mode)
		{
			FastTransferStream fastTransferStream = null;
			bool flag = false;
			FastTransferStream result;
			try
			{
				fastTransferStream = new FastTransferStream(mode);
				fastTransferStream.ConfigureNew(context, logon);
				flag = true;
				result = fastTransferStream;
			}
			finally
			{
				if (!flag && fastTransferStream != null)
				{
					fastTransferStream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public FastTransferState State
		{
			get
			{
				FastTransferDownloadContext fastTransferDownloadContext = this.streamContext as FastTransferDownloadContext;
				if (fastTransferDownloadContext != null)
				{
					return fastTransferDownloadContext.State;
				}
				FastTransferUploadContext fastTransferUploadContext = this.streamContext as FastTransferUploadContext;
				if (fastTransferUploadContext != null)
				{
					return fastTransferUploadContext.State;
				}
				return FastTransferState.Error;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public int GetNextBuffer(MapiContext operationContext, ArraySegment<byte> buffer)
		{
			base.ThrowIfNotValid(null);
			if (this.mode != FastTransferStreamMode.Download)
			{
				throw new ExExceptionNoSupport((LID)38224U, "Stream was not opened for download");
			}
			if (this.streamContext == null)
			{
				this.streamContext = this.CreateContextForDownload(operationContext);
			}
			return ((FastTransferDownloadContext)this.streamContext).GetNextBuffer(operationContext, buffer);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public void PutNextBuffer(MapiContext operationContext, ArraySegment<byte> buffer)
		{
			base.ThrowIfNotValid(null);
			if (this.mode != FastTransferStreamMode.Upload)
			{
				throw new ExExceptionNoSupport((LID)54608U, "Stream was not opened for upload");
			}
			if (this.streamContext == null)
			{
				this.streamContext = this.CreateContextForUpload(operationContext);
			}
			((FastTransferUploadContext)this.streamContext).PutNextBuffer(operationContext, buffer);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public void Flush(MapiContext operationContext)
		{
			base.ThrowIfNotValid(null);
			if (this.mode != FastTransferStreamMode.Upload)
			{
				throw new ExExceptionNoSupport((LID)42320U, "Stream was not opened for upload");
			}
			((FastTransferUploadContext)this.streamContext).Flush(operationContext);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000DB83 File Offset: 0x0000BD83
		private void ConfigureNew(MapiContext context, MapiLogon logon)
		{
			this.configPropertyBag = new FastTransferStream.ConfigurationPropertyBag(logon.StoreMailbox);
			base.Logon = logon;
			base.ParentObject = logon;
			base.IsValid = true;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000DBAB File Offset: 0x0000BDAB
		protected override PropertyBag StorePropertyBag
		{
			get
			{
				if (this.configPropertyBag == null)
				{
					throw new StoreException((LID)58704U, ErrorCodeValue.Rejected, "Configuration information cannot be accessed after upload or download started.");
				}
				return this.configPropertyBag;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		internal override void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000DBD7 File Offset: 0x0000BDD7
		internal override void CheckPropertyRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000DBD9 File Offset: 0x0000BDD9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferStream>(this);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000DBE1 File Offset: 0x0000BDE1
		protected override void InternalDispose(bool isCalledFromDispose)
		{
			if (isCalledFromDispose && this.streamContext != null)
			{
				this.streamContext.Dispose();
				this.streamContext = null;
			}
			base.InternalDispose(isCalledFromDispose);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000DC34 File Offset: 0x0000BE34
		private FastTransferDownloadContext CreateContextForDownload(MapiContext context)
		{
			Guid? guid = base.GetOnePropValue(context, PropTag.FastTransferStream.InstanceGuid) as Guid?;
			if (guid == null)
			{
				throw new ExExceptionNoSupport((LID)34128U, "Invalid stream instance ID");
			}
			if (guid.Value == FastTransferStream.WellKnownIds.InferenceLog)
			{
				FastTransferDownloadContext fastTransferDownloadContext = new FastTransferDownloadContext(Array<PropertyTag>.Empty);
				bool flag = false;
				try
				{
					FastTransferDownloadContext capturedContext = fastTransferDownloadContext;
					fastTransferDownloadContext.Configure(base.Logon, FastTransferSendOption.Unicode, delegate(MapiContext operationContext)
					{
						InferenceLogIterator messageIterator = new InferenceLogIterator(capturedContext);
						return new FastTransferMessageIterator(messageIterator, FastTransferCopyMessagesFlag.None, true);
					});
					flag = true;
					return fastTransferDownloadContext;
				}
				finally
				{
					if (!flag && fastTransferDownloadContext != null)
					{
						fastTransferDownloadContext.Dispose();
					}
				}
			}
			throw new ExExceptionNoSupport((LID)36688U, "Unknown stream instance ID: " + guid.Value.ToString());
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		private FastTransferUploadContext CreateContextForUpload(MapiContext context)
		{
			Guid? guid = base.GetOnePropValue(context, PropTag.FastTransferStream.InstanceGuid) as Guid?;
			if (guid == null)
			{
				throw new ExExceptionNoSupport((LID)50512U, "Invalid stream instance ID");
			}
			if (guid.Value == FastTransferStream.WellKnownIds.InferenceLog)
			{
				FastTransferUploadContext fastTransferUploadContext = new FastTransferUploadContext();
				bool flag = false;
				try
				{
					FastTransferUploadContext capturedContext = fastTransferUploadContext;
					fastTransferUploadContext.Configure(base.Logon, delegate(MapiContext operationContext)
					{
						InferenceLogIterator messageIteratorClient = new InferenceLogIterator(capturedContext);
						return new FastTransferMessageIterator(messageIteratorClient, true);
					}, null, null);
					flag = true;
					return fastTransferUploadContext;
				}
				finally
				{
					if (!flag && fastTransferUploadContext != null)
					{
						fastTransferUploadContext.Dispose();
					}
				}
			}
			throw new ExExceptionNoSupport((LID)53072U, "Unknown stream instance ID: " + guid.Value.ToString());
		}

		// Token: 0x040000C7 RID: 199
		private FastTransferContext streamContext;

		// Token: 0x040000C8 RID: 200
		private FastTransferStream.ConfigurationPropertyBag configPropertyBag;

		// Token: 0x040000C9 RID: 201
		private FastTransferStreamMode mode;

		// Token: 0x02000029 RID: 41
		public static class WellKnownIds
		{
			// Token: 0x040000CA RID: 202
			public static readonly Guid InferenceLog = new Guid("8ebdc484-475b-4d27-aaad-647e1cac4144");
		}

		// Token: 0x0200002A RID: 42
		private class ConfigurationPropertyBag : PrivatePropertyBag
		{
			// Token: 0x060001B5 RID: 437 RVA: 0x0000DE29 File Offset: 0x0000C029
			public ConfigurationPropertyBag(Mailbox mailbox) : base(false)
			{
				mailbox.IsValid();
				this.mailbox = mailbox;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000DE3F File Offset: 0x0000C03F
			public override ObjectPropertySchema Schema
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000DE42 File Offset: 0x0000C042
			public override Context CurrentOperationContext
			{
				get
				{
					return this.mailbox.CurrentOperationContext;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000DE4F File Offset: 0x0000C04F
			public override ReplidGuidMap ReplidGuidMap
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000DE52 File Offset: 0x0000C052
			protected override StorePropTag MapPropTag(Context context, uint propertyTag)
			{
				return this.mailbox.GetStorePropTag(context, propertyTag, ObjectType.FastTransferStream);
			}

			// Token: 0x040000CB RID: 203
			private readonly Mailbox mailbox;
		}
	}
}
