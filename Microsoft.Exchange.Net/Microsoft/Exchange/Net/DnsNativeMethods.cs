using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000CA3 RID: 3235
	[SuppressUnmanagedCodeSecurity]
	[ComVisible(false)]
	internal static class DnsNativeMethods
	{
		// Token: 0x0600473B RID: 18235
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DnsNameCompare_W")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DnsNameCompare([In] string dnsName1, [In] string dnsName2);

		// Token: 0x0600473C RID: 18236
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode)]
		public static extern DnsNativeMethods.WinDnsStatus DnsQueryConfig(DnsNativeMethods.DnsConfigType config, uint dnsFlag, [In] string adapterName, IntPtr reserved, [Out] byte[] configBuffer, [In] [Out] ref uint bufferLength);

		// Token: 0x0600473D RID: 18237 RVA: 0x000BFEDC File Offset: 0x000BE0DC
		public static byte[] DnsQuestionToBuffer(bool udpRequest, string questionName, DnsRecordType recordType, ushort queryIdentifier, int recursionDesired)
		{
			byte[] array = new byte[1472];
			uint num = (uint)array.Length;
			if (!DnsNativeMethods.DnsWriteQuestionToBuffer(array, ref num, questionName, recordType, queryIdentifier, recursionDesired))
			{
				if ((ulong)num < (ulong)((long)array.Length))
				{
					return null;
				}
				array = new byte[num];
				if (!DnsNativeMethods.DnsWriteQuestionToBuffer(array, ref num, questionName, recordType, queryIdentifier, recursionDesired))
				{
					return null;
				}
			}
			int num2 = (int)num;
			byte[] array2;
			if (udpRequest)
			{
				array2 = new byte[num2];
				Buffer.BlockCopy(array, 0, array2, 0, num2);
			}
			else
			{
				array2 = new byte[num2 + 2];
				Buffer.BlockCopy(array, 0, array2, 2, num2);
				short num3 = IPAddress.HostToNetworkOrder((short)num);
				array2[0] = (byte)num3;
				array2[1] = (byte)(num3 >> 8);
			}
			return array2;
		}

		// Token: 0x0600473E RID: 18238
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode)]
		public static extern void DnsRecordListFree([In] IntPtr ptrRecords, [In] FreeType freeType);

		// Token: 0x0600473F RID: 18239
		[DllImport("dnsapi.dll", EntryPoint = "DnsExtractRecordsFromMessage_W")]
		public unsafe static extern DnsNativeMethods.WinDnsStatus DnsExtractRecordsFromMessage(byte* buffer, ushort messageLength, out IntPtr dnsRecords);

		// Token: 0x06004740 RID: 18240
		[DllImport("dnsapi.dll", EntryPoint = "DnsWriteQuestionToBuffer_UTF8")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DnsWriteQuestionToBuffer([Out] byte[] dnsBuffer, [In] [Out] ref uint bufferSize, string hostName, DnsRecordType questionType, ushort xid, int recursionDesired);

		// Token: 0x06004741 RID: 18241
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DnsValidateName_W")]
		internal static extern int ValidateName([In] string name, int format);

		// Token: 0x04003C4D RID: 15437
		private const string DNSAPI = "dnsapi.dll";

		// Token: 0x02000CA4 RID: 3236
		public enum DnsConfigType
		{
			// Token: 0x04003C4F RID: 15439
			PrimaryDomainName,
			// Token: 0x04003C50 RID: 15440
			DnsServerList = 6,
			// Token: 0x04003C51 RID: 15441
			PrimaryHostNameRegistrationEnabled = 9,
			// Token: 0x04003C52 RID: 15442
			AdapterHostNameRegistrationEnabled,
			// Token: 0x04003C53 RID: 15443
			AddressRegistrationMaxCount,
			// Token: 0x04003C54 RID: 15444
			HostName,
			// Token: 0x04003C55 RID: 15445
			FullHostName = 15
		}

		// Token: 0x02000CA5 RID: 3237
		public enum WinDnsStatus : uint
		{
			// Token: 0x04003C57 RID: 15447
			Success,
			// Token: 0x04003C58 RID: 15448
			ErrorInvalidName = 123U,
			// Token: 0x04003C59 RID: 15449
			InfoNoRecords = 9501U,
			// Token: 0x04003C5A RID: 15450
			ErrorBadPacket,
			// Token: 0x04003C5B RID: 15451
			ErrorNoPacket,
			// Token: 0x04003C5C RID: 15452
			ErrorRCode,
			// Token: 0x04003C5D RID: 15453
			ErrorServerFailure = 9002U,
			// Token: 0x04003C5E RID: 15454
			ErrorUnsecurePacket = 9505U,
			// Token: 0x04003C5F RID: 15455
			ErrorRCodeNameError = 9003U
		}
	}
}
