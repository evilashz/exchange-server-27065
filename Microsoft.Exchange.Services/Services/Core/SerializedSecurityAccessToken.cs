using System;
using System.Text;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	internal sealed class SerializedSecurityAccessToken : ISecurityAccessToken
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000169E8 File Offset: 0x00014BE8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x000169F0 File Offset: 0x00014BF0
		public SidStringAndAttributes[] GroupSids { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000169F9 File Offset: 0x00014BF9
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00016A01 File Offset: 0x00014C01
		public SidStringAndAttributes[] RestrictedGroupSids { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00016A0A File Offset: 0x00014C0A
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00016A12 File Offset: 0x00014C12
		public string UserSid { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00016A1B File Offset: 0x00014C1B
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00016A23 File Offset: 0x00014C23
		public string SmtpAddress { get; set; }

		// Token: 0x0600045F RID: 1119 RVA: 0x00016A2C File Offset: 0x00014C2C
		public static SerializedSecurityAccessToken FromBytes(byte[] serializedTokenBytes)
		{
			SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
			int num = 0;
			if (serializedTokenBytes.Length < SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.Length + 1)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			for (int i = 0; i < SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.Length; i++)
			{
				if (serializedTokenBytes[num++] != SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie[i])
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
				}
			}
			if (serializedTokenBytes[num++] != 1)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			serializedSecurityAccessToken.UserSid = SerializedSecurityAccessToken.DeserializeStringFromByteArray(serializedTokenBytes, ref num);
			serializedSecurityAccessToken.GroupSids = SerializedSecurityAccessToken.DeserializeSidArrayFromByteArray(serializedTokenBytes, ref num);
			serializedSecurityAccessToken.RestrictedGroupSids = SerializedSecurityAccessToken.DeserializeSidArrayFromByteArray(serializedTokenBytes, ref num);
			if (num < serializedTokenBytes.Length)
			{
				serializedSecurityAccessToken.SmtpAddress = SerializedSecurityAccessToken.DeserializeStringFromByteArray(serializedTokenBytes, ref num);
			}
			return serializedSecurityAccessToken;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016AE0 File Offset: 0x00014CE0
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
			if (!string.IsNullOrEmpty(this.SmtpAddress))
			{
				SerializedSecurityAccessToken.SerializeStringToByteArray(this.SmtpAddress, array, ref num);
			}
			return array;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00016B60 File Offset: 0x00014D60
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

		// Token: 0x06000462 RID: 1122 RVA: 0x00016BA8 File Offset: 0x00014DA8
		internal static void SerializeStringToByteArray(string stringToSerialize, byte[] byteArray, ref int byteIndex)
		{
			int index = byteIndex;
			byteIndex += 4;
			int bytes = Encoding.UTF8.GetBytes(stringToSerialize, 0, stringToSerialize.Length, byteArray, byteIndex);
			byteIndex += bytes;
			BitConverter.GetBytes(bytes).CopyTo(byteArray, index);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016BE8 File Offset: 0x00014DE8
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

		// Token: 0x06000464 RID: 1124 RVA: 0x00016C6C File Offset: 0x00014E6C
		internal static int ReadInt32(byte[] byteArray, ref int byteIndex)
		{
			if (byteArray.Length < byteIndex + 4)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			int result = BitConverter.ToInt32(byteArray, byteIndex);
			byteIndex += 4;
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016CA0 File Offset: 0x00014EA0
		private static uint ReadUInt32(byte[] byteArray, ref int byteIndex)
		{
			if (byteArray.Length < byteIndex + 4)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			uint result = BitConverter.ToUInt32(byteArray, byteIndex);
			byteIndex += 4;
			return result;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016CD4 File Offset: 0x00014ED4
		internal static string DeserializeStringFromByteArray(byte[] byteArray, ref int byteIndex)
		{
			int num = SerializedSecurityAccessToken.ReadInt32(byteArray, ref byteIndex);
			if (byteArray.Length < byteIndex + num)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			string @string = Encoding.UTF8.GetString(byteArray, byteIndex, num);
			byteIndex += num;
			return @string;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016D14 File Offset: 0x00014F14
		private static SidStringAndAttributes[] DeserializeSidArrayFromByteArray(byte[] byteArray, ref int byteIndex)
		{
			int num = SerializedSecurityAccessToken.ReadInt32(byteArray, ref byteIndex);
			if (num == 0)
			{
				return null;
			}
			SidStringAndAttributes[] array = new SidStringAndAttributes[num];
			for (int i = 0; i < num; i++)
			{
				string identifier = SerializedSecurityAccessToken.DeserializeStringFromByteArray(byteArray, ref byteIndex);
				uint attribute = SerializedSecurityAccessToken.ReadUInt32(byteArray, ref byteIndex);
				array[i] = new SidStringAndAttributes(identifier, attribute);
			}
			return array;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016D60 File Offset: 0x00014F60
		private int GetByteCountToSerializeToken()
		{
			int num = 0;
			num += SerializedSecurityAccessToken.serializedSecurityAccessTokenCookie.Length;
			num++;
			num += 4;
			num += Encoding.UTF8.GetByteCount(this.UserSid);
			num += SerializedSecurityAccessToken.GetByteCountToSerializeSidArray(this.GroupSids);
			num += SerializedSecurityAccessToken.GetByteCountToSerializeSidArray(this.RestrictedGroupSids);
			if (!string.IsNullOrEmpty(this.SmtpAddress))
			{
				num += 4;
				num += Encoding.UTF8.GetByteCount(this.SmtpAddress);
			}
			return num;
		}

		// Token: 0x04000649 RID: 1609
		private const int SerializedSecurityAccessTokenVersion = 1;

		// Token: 0x0400064A RID: 1610
		private static byte[] serializedSecurityAccessTokenCookie = new byte[]
		{
			83,
			83,
			65,
			84
		};
	}
}
