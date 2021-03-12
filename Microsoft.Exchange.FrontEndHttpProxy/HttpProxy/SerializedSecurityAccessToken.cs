using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	internal sealed class SerializedSecurityAccessToken : ISecurityAccessToken
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00016817 File Offset: 0x00014A17
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001681F File Offset: 0x00014A1F
		public SidStringAndAttributes[] GroupSids
		{
			get
			{
				return this.groupSids;
			}
			set
			{
				this.groupSids = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00016828 File Offset: 0x00014A28
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00016830 File Offset: 0x00014A30
		public SidStringAndAttributes[] RestrictedGroupSids
		{
			get
			{
				return this.restrictedGroupSids;
			}
			set
			{
				this.restrictedGroupSids = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00016839 File Offset: 0x00014A39
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00016841 File Offset: 0x00014A41
		public string UserSid
		{
			get
			{
				return this.userSid;
			}
			set
			{
				this.userSid = value;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001684C File Offset: 0x00014A4C
		public byte[] GetBytes()
		{
			byte[] array = new byte[this.GetByteCountToSerializeToken()];
			int num = 0;
			SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.CopyTo(array, num);
			num += SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.Length;
			array[num++] = 1;
			SerializedSecurityAccessToken.SerializeStringToByteArray(this.UserSid, array, ref num);
			SerializedSecurityAccessToken.SerializeSidArrayToByteArray(this.GroupSids, array, ref num);
			SerializedSecurityAccessToken.SerializeSidArrayToByteArray(this.RestrictedGroupSids, array, ref num);
			return array;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000168B0 File Offset: 0x00014AB0
		public byte[] GetSecurityContextBytes()
		{
			int num = (this.GroupSids == null) ? 0 : this.GroupSids.Length;
			int num2 = (this.RestrictedGroupSids == null) ? 0 : this.RestrictedGroupSids.Length;
			if (num + num2 > 3000)
			{
				ExTraceGlobals.VerboseTracer.TraceError<int>(0L, "[SerializedSecurityAccessToken::GetSecurityContextBytes] Token contained more than {0} group sids.", 3000);
				throw new InvalidOperationException();
			}
			byte[] bytes = this.GetBytes();
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
				memoryStream.Flush();
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00016974 File Offset: 0x00014B74
		private static int GetByteCountToSerializeSidArray(SidStringAndAttributes[] sidArray)
		{
			int num = 0;
			num += 4;
			if (sidArray == null)
			{
				return num;
			}
			foreach (SidStringAndAttributes sidStringAndAttributes in sidArray)
			{
				num += 4;
				num += Encoding.UTF8.GetByteCount(sidStringAndAttributes.SecurityIdentifier);
				num += 4;
			}
			return num;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000169BC File Offset: 0x00014BBC
		private static void SerializeStringToByteArray(string stringToSerialize, byte[] byteArray, ref int byteIndex)
		{
			int index = byteIndex;
			byteIndex += 4;
			int bytes = Encoding.UTF8.GetBytes(stringToSerialize, 0, stringToSerialize.Length, byteArray, byteIndex);
			byteIndex += bytes;
			BitConverter.GetBytes(bytes).CopyTo(byteArray, index);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000169FC File Offset: 0x00014BFC
		private static void SerializeSidArrayToByteArray(SidStringAndAttributes[] sidArray, byte[] byteArray, ref int byteIndex)
		{
			if (sidArray == null || sidArray.Length == 0)
			{
				for (int i = 0; i < 4; i++)
				{
					byteArray[byteIndex++] = 0;
				}
				return;
			}
			BitConverter.GetBytes(sidArray.Length).CopyTo(byteArray, byteIndex);
			byteIndex += 4;
			foreach (SidStringAndAttributes sidStringAndAttributes in sidArray)
			{
				SerializedSecurityAccessToken.SerializeStringToByteArray(sidStringAndAttributes.SecurityIdentifier, byteArray, ref byteIndex);
				BitConverter.GetBytes(sidStringAndAttributes.Attributes).CopyTo(byteArray, byteIndex);
				byteIndex += 4;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00016A80 File Offset: 0x00014C80
		private int GetByteCountToSerializeToken()
		{
			int num = 0;
			num += SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.Length;
			num++;
			num += 4;
			num += Encoding.UTF8.GetByteCount(this.UserSid);
			num += SerializedSecurityAccessToken.GetByteCountToSerializeSidArray(this.GroupSids);
			return num + SerializedSecurityAccessToken.GetByteCountToSerializeSidArray(this.RestrictedGroupSids);
		}

		// Token: 0x040002C2 RID: 706
		private const int SerializedSecurityAccessTokenVersion = 1;

		// Token: 0x040002C3 RID: 707
		private static byte[] serializedSecurityAccessTokenCookie = new byte[]
		{
			83,
			83,
			65,
			84
		};

		// Token: 0x040002C4 RID: 708
		private string userSid;

		// Token: 0x040002C5 RID: 709
		private SidStringAndAttributes[] groupSids;

		// Token: 0x040002C6 RID: 710
		private SidStringAndAttributes[] restrictedGroupSids;
	}
}
