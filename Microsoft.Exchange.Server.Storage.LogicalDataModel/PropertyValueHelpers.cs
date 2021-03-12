using System;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001D RID: 29
	public static class PropertyValueHelpers
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0002CB98 File Offset: 0x0002AD98
		public static byte[] FormatSdForTransfer(SecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor.BinaryForm == null)
			{
				return null;
			}
			byte[] array = new byte[PropertyValueHelpers.MapiSdHeader.Length + securityDescriptor.BinaryForm.Length];
			Array.Copy(PropertyValueHelpers.MapiSdHeader, 0, array, 0, PropertyValueHelpers.MapiSdHeader.Length);
			Array.Copy(securityDescriptor.BinaryForm, 0, array, PropertyValueHelpers.MapiSdHeader.Length, securityDescriptor.BinaryForm.Length);
			return array;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		public static byte[] FormatSdForTransfer(ArraySegment<byte> securityDescriptorSegment)
		{
			byte[] array = new byte[PropertyValueHelpers.MapiSdHeader.Length + securityDescriptorSegment.Count];
			Array.Copy(PropertyValueHelpers.MapiSdHeader, 0, array, 0, PropertyValueHelpers.MapiSdHeader.Length);
			Array.Copy(securityDescriptorSegment.Array, securityDescriptorSegment.Offset, array, PropertyValueHelpers.MapiSdHeader.Length, securityDescriptorSegment.Count);
			return array;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0002CC54 File Offset: 0x0002AE54
		public static SecurityDescriptor UnformatSdFromTransfer(byte[] transferredSDBytes)
		{
			ArraySegment<byte> arraySegment = PropertyValueHelpers.UnformatSdSegmentFromTransfer(transferredSDBytes);
			byte[] array = new byte[arraySegment.Count];
			Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
			return new SecurityDescriptor(array);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0002CC98 File Offset: 0x0002AE98
		public static ArraySegment<byte> UnformatSdSegmentFromTransfer(byte[] transferredSDBytes)
		{
			int num = 0;
			ParseSerialize.GetWord(transferredSDBytes, ref num, transferredSDBytes.Length);
			num += 2;
			ushort word = ParseSerialize.GetWord(transferredSDBytes, ref num, transferredSDBytes.Length);
			num += 2;
			switch (word)
			{
			case 3:
				num += 4;
				break;
			}
			return new ArraySegment<byte>(transferredSDBytes, num, transferredSDBytes.Length - num);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0002CCF0 File Offset: 0x0002AEF0
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyValueHelpers()
		{
			byte[] array = new byte[8];
			array[0] = 8;
			array[2] = 3;
			PropertyValueHelpers.MapiSdHeader = array;
		}

		// Token: 0x04000226 RID: 550
		private static readonly byte[] MapiSdHeader;
	}
}
