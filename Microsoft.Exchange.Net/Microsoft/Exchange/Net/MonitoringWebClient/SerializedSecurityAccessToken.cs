using System;
using System.Text;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079E RID: 1950
	[Serializable]
	internal sealed class SerializedSecurityAccessToken : ISecurityAccessToken
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000535B4 File Offset: 0x000517B4
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x000535BC File Offset: 0x000517BC
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

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000535C5 File Offset: 0x000517C5
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x000535CD File Offset: 0x000517CD
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

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000535D6 File Offset: 0x000517D6
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000535DE File Offset: 0x000517DE
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

		// Token: 0x06002745 RID: 10053 RVA: 0x000535E8 File Offset: 0x000517E8
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

		// Token: 0x06002746 RID: 10054 RVA: 0x0005364C File Offset: 0x0005184C
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

		// Token: 0x06002747 RID: 10055 RVA: 0x00053694 File Offset: 0x00051894
		private static void SerializeStringToByteArray(string stringToSerialize, byte[] byteArray, ref int byteIndex)
		{
			int index = byteIndex;
			byteIndex += 4;
			int bytes = Encoding.UTF8.GetBytes(stringToSerialize, 0, stringToSerialize.Length, byteArray, byteIndex);
			byteIndex += bytes;
			BitConverter.GetBytes(bytes).CopyTo(byteArray, index);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000536D4 File Offset: 0x000518D4
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

		// Token: 0x06002749 RID: 10057 RVA: 0x00053758 File Offset: 0x00051958
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

		// Token: 0x04002370 RID: 9072
		private const int SerializedSecurityAccessTokenVersion = 1;

		// Token: 0x04002371 RID: 9073
		private static byte[] serializedSecurityAccessTokenCookie = new byte[]
		{
			83,
			83,
			65,
			84
		};

		// Token: 0x04002372 RID: 9074
		private string userSid;

		// Token: 0x04002373 RID: 9075
		private SidStringAndAttributes[] groupSids;

		// Token: 0x04002374 RID: 9076
		private SidStringAndAttributes[] restrictedGroupSids;
	}
}
