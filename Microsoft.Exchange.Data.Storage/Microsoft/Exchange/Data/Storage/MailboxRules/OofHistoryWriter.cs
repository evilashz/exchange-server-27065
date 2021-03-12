using System;
using System.IO;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BEA RID: 3050
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OofHistoryWriter
	{
		// Token: 0x06006C44 RID: 27716 RVA: 0x001D0C0F File Offset: 0x001CEE0F
		public static void Reset(Stream oofHistoryStream)
		{
			oofHistoryStream.Seek(0L, SeekOrigin.Begin);
			oofHistoryStream.Write(OofHistoryWriter.InitialBytes, 0, 6);
			if (oofHistoryStream.Length > 6L)
			{
				oofHistoryStream.SetLength(6L);
			}
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x001D0C3A File Offset: 0x001CEE3A
		public void Initialize(Stream oofHistoryStream)
		{
			this.oofHistoryStream = oofHistoryStream;
			this.dataPosition = 0;
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x001D0C4C File Offset: 0x001CEE4C
		public void AppendEntry(int entryCount, byte[] senderAddress, byte[] globalRuleId)
		{
			if (this.data != null)
			{
				throw new InvalidOperationException("OOF history writer only supports one append operation.");
			}
			this.oofHistoryStream.Position = this.oofHistoryStream.Length;
			int num = 7 + senderAddress.Length + globalRuleId.Length;
			this.data = new byte[num];
			this.data[0] = 2;
			this.dataPosition = 1;
			this.AppendProperty(OofHistory.PropId.SenderAddress, senderAddress);
			this.AppendProperty(OofHistory.PropId.GlobalRuleId, globalRuleId);
			this.oofHistoryStream.Write(this.data, 0, this.dataPosition);
			this.oofHistoryStream.Seek(2L, SeekOrigin.Begin);
			ExBitConverter.Write(entryCount, this.data, 0);
			this.oofHistoryStream.Write(this.data, 0, 4);
			this.oofHistoryStream.Flush();
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x001D0D0C File Offset: 0x001CEF0C
		private void AppendProperty(OofHistory.PropId propId, byte[] propertyValue)
		{
			this.data[this.dataPosition++] = (byte)propId;
			ushort num = (ushort)propertyValue.Length;
			ExBitConverter.Write(num, this.data, this.dataPosition);
			this.dataPosition += 2;
			Buffer.BlockCopy(propertyValue, 0, this.data, this.dataPosition, (int)num);
			this.dataPosition += (int)num;
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x001D0D7C File Offset: 0x001CEF7C
		// Note: this type is marked as 'beforefieldinit'.
		static OofHistoryWriter()
		{
			byte[] array = new byte[6];
			array[0] = 1;
			array[1] = 1;
			OofHistoryWriter.InitialBytes = array;
		}

		// Token: 0x04003DF4 RID: 15860
		internal static readonly byte[] InitialBytes;

		// Token: 0x04003DF5 RID: 15861
		private byte[] data;

		// Token: 0x04003DF6 RID: 15862
		private int dataPosition;

		// Token: 0x04003DF7 RID: 15863
		private Stream oofHistoryStream;
	}
}
