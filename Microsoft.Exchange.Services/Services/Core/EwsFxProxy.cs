using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E6 RID: 742
	internal class EwsFxProxy : DisposeTrackableBase, IMapiFxProxy, IDisposeTrackable, IDisposable
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00067B48 File Offset: 0x00065D48
		public EwsFxProxy(XmlWriter writer)
		{
			Guid objectType = InterfaceIds.IMessageGuid;
			byte[] serverVersion = new byte[]
			{
				8,
				0,
				130,
				140,
				0,
				0
			};
			this.cachedObjectData = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(objectType);
				serializer.Write(serverVersion);
			});
			this.writer = writer;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00067BA1 File Offset: 0x00065DA1
		byte[] IMapiFxProxy.GetObjectData()
		{
			return this.cachedObjectData;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00067BAC File Offset: 0x00065DAC
		void IMapiFxProxy.ProcessRequest(FxOpcodes opcode, byte[] request)
		{
			this.intBuffer = BitConverter.GetBytes((int)opcode);
			this.writer.WriteBase64(this.intBuffer, 0, this.intBuffer.GetLength(0));
			int length = request.GetLength(0);
			this.intBuffer = BitConverter.GetBytes(length);
			this.writer.WriteBase64(this.intBuffer, 0, this.intBuffer.GetLength(0));
			if (length != 0)
			{
				this.writer.WriteBase64(request, 0, request.GetLength(0));
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00067C2C File Offset: 0x00065E2C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EwsFxProxy>(this);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00067C34 File Offset: 0x00065E34
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04000DFF RID: 3583
		private byte[] cachedObjectData;

		// Token: 0x04000E00 RID: 3584
		private XmlWriter writer;

		// Token: 0x04000E01 RID: 3585
		private byte[] intBuffer;
	}
}
