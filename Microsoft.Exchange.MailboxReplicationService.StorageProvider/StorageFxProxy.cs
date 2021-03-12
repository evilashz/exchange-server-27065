using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	internal abstract class StorageFxProxy<T> : DisposeTrackableBase, IFxProxy, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000855E File Offset: 0x0000675E
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00008566 File Offset: 0x00006766
		protected bool IsMoveUser { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000856F File Offset: 0x0000676F
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00008577 File Offset: 0x00006777
		protected T TargetObject { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x000085A4 File Offset: 0x000067A4
		public static byte[] CreateObjectData(Guid objectType)
		{
			byte[] exchangeVersionBlob = new byte[]
			{
				(byte)VersionInformation.MRS.ProductMinor,
				(byte)VersionInformation.MRS.ProductMajor,
				(byte)(VersionInformation.MRS.BuildMajor % 256),
				(byte)(VersionInformation.MRS.BuildMajor / 256 + 128),
				(byte)(VersionInformation.MRS.BuildMinor % 256),
				(byte)(VersionInformation.MRS.BuildMinor / 256)
			};
			return BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(objectType);
				serializer.Write(exchangeVersionBlob);
			});
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008651 File Offset: 0x00006851
		byte[] IMapiFxProxy.GetObjectData()
		{
			return this.GetObjectDataImplementation();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000865C File Offset: 0x0000685C
		void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] data)
		{
			switch (opCode)
			{
			case FxOpcodes.Config:
			{
				if (data == null || data.Length != 8)
				{
					throw new FastTransferBufferException("data", (data == null) ? -1 : data.Length);
				}
				uint transferMethod = BitConverter.ToUInt32(data, 4);
				IFastTransferProcessor<FastTransferUploadContext> fxProcessor = this.GetFxProcessor(transferMethod);
				this.uploadContext = new FastTransferUploadContext(Encoding.ASCII, NullResourceTracker.Instance, PropertyFilterFactory.IncludeAllFactory, this.IsMoveUser);
				this.uploadContext.PushInitial(fxProcessor);
				return;
			}
			case FxOpcodes.TransferBuffer:
				ExAssert.RetailAssert(this.uploadContext != null, "StorageFxProxy:ProcessRequest: null upload context");
				try
				{
					this.uploadContext.PutNextBuffer(new ArraySegment<byte>(data));
					return;
				}
				catch (ArgumentException innerException)
				{
					throw new FastTransferArgumentException(innerException);
				}
				break;
			case FxOpcodes.IsInterfaceOk:
			case FxOpcodes.TellPartnerVersion:
				return;
			}
			throw new FastTransferBufferException("opCode", (int)opCode);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008730 File Offset: 0x00006930
		void IFxProxy.Flush()
		{
		}

		// Token: 0x060000A6 RID: 166
		protected abstract byte[] GetObjectDataImplementation();

		// Token: 0x060000A7 RID: 167
		protected abstract IFastTransferProcessor<FastTransferUploadContext> GetFxProcessor(uint transferMethod);

		// Token: 0x060000A8 RID: 168 RVA: 0x00008732 File Offset: 0x00006932
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.uploadContext != null)
			{
				this.uploadContext.Dispose();
				this.uploadContext = null;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008751 File Offset: 0x00006951
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StorageFxProxy<T>>(this);
		}

		// Token: 0x04000018 RID: 24
		protected const uint TransferMethodCopyTo = 1U;

		// Token: 0x04000019 RID: 25
		protected const uint TransferMethodCopyMessages = 3U;

		// Token: 0x0400001A RID: 26
		private FastTransferUploadContext uploadContext;
	}
}
