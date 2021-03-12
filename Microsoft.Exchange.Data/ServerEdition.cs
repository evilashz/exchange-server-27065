using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	internal sealed class ServerEdition
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0002B948 File Offset: 0x00029B48
		public ServerEdition()
		{
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002B95E File Offset: 0x00029B5E
		public ServerEdition(ServerEditionValue serverType, ServerEditionValue setupType)
		{
			this.serverType = serverType;
			this.setupType = setupType;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002B982 File Offset: 0x00029B82
		public ServerEdition(string serverTypeInAD)
		{
			if (!string.IsNullOrEmpty(serverTypeInAD) && serverTypeInAD.Length <= 32)
			{
				this.DecryptServerType(serverTypeInAD);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0002B9B1 File Offset: 0x00029BB1
		public bool IsStandard
		{
			get
			{
				return this.serverType == ServerEditionValue.Standard;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0002B9BC File Offset: 0x00029BBC
		public bool IsEnterprise
		{
			get
			{
				return this.serverType == ServerEditionValue.Enterprise;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0002B9C7 File Offset: 0x00029BC7
		public bool IsCoexistence
		{
			get
			{
				return this.serverType == ServerEditionValue.Coexistence;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0002B9D3 File Offset: 0x00029BD3
		public bool IsEvaluation
		{
			get
			{
				return this.setupType == ServerEditionValue.Evaluation;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		public override string ToString()
		{
			string result;
			if (this.IsEvaluation)
			{
				if (this.IsStandard)
				{
					result = DataStrings.StandardTrialEdition;
				}
				else if (this.IsEnterprise)
				{
					result = DataStrings.EnterpriseTrialEdition;
				}
				else if (this.IsCoexistence)
				{
					result = DataStrings.CoexistenceTrialEdition;
				}
				else
				{
					result = DataStrings.UnknownEdition;
				}
			}
			else if (this.IsStandard)
			{
				result = DataStrings.StandardEdition;
			}
			else if (this.IsEnterprise)
			{
				result = DataStrings.EnterpriseEdition;
			}
			else if (this.IsCoexistence)
			{
				result = DataStrings.CoexistenceEdition;
			}
			else
			{
				result = DataStrings.UnknownEdition;
			}
			return result;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002BA94 File Offset: 0x00029C94
		private string EncryptServerType()
		{
			long value = DateTime.UtcNow.ToFileTimeUtc() & (long)((ulong)-1);
			string s = string.Format("0x{0:x8};0x{1:x8};0x{2:x8}", (int)this.serverType, Convert.ToUInt32(value), (int)this.setupType);
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			byte[] array = bytes;
			int num = 0;
			array[num] ^= 75;
			for (int i = 1; i < 64; i++)
			{
				byte[] array2 = bytes;
				int num2 = i;
				array2[num2] ^= (bytes[i - 1] ^ 73);
			}
			return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002BB3C File Offset: 0x00029D3C
		private void DecryptServerType(string rawString)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(rawString);
			byte[] array = new byte[64];
			for (int i = 0; i < bytes.Length; i++)
			{
				array[i] = bytes[i];
			}
			for (int j = 64; j > 1; j--)
			{
				byte[] array2 = array;
				int num = j - 1;
				array2[num] ^= (array[j - 2] ^ 73);
			}
			byte[] array3 = array;
			int num2 = 0;
			array3[num2] ^= 75;
			string[] array4 = Encoding.Unicode.GetString(array, 0, array.Length).Split(new char[]
			{
				';'
			});
			int num3 = Convert.ToInt32(array4[0], 16);
			int num4 = Convert.ToInt32(array4[2], 16);
			if (Enum.IsDefined(typeof(ServerEditionValue), num3))
			{
				this.serverType = (ServerEditionValue)num3;
			}
			if (Enum.IsDefined(typeof(ServerEditionValue), num4))
			{
				this.setupType = (ServerEditionValue)num4;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002BC34 File Offset: 0x00029E34
		public static ServerEditionType DecryptServerEdition(string serverTypeInAD)
		{
			if (serverTypeInAD == null)
			{
				throw new ArgumentNullException("serverTypeInAD");
			}
			if (serverTypeInAD.Length != 32)
			{
				return ServerEditionType.Unknown;
			}
			ServerEdition serverEdition = new ServerEdition(serverTypeInAD);
			if (serverEdition.IsStandard && !serverEdition.IsEvaluation)
			{
				return ServerEditionType.Standard;
			}
			if (serverEdition.IsStandard && serverEdition.IsEvaluation)
			{
				return ServerEditionType.StandardEvaluation;
			}
			if (serverEdition.IsEnterprise && !serverEdition.IsEvaluation)
			{
				return ServerEditionType.Enterprise;
			}
			if (serverEdition.IsEnterprise && serverEdition.IsEvaluation)
			{
				return ServerEditionType.EnterpriseEvaluation;
			}
			if (serverEdition.IsCoexistence && !serverEdition.IsEvaluation)
			{
				return ServerEditionType.Coexistence;
			}
			if (serverEdition.IsCoexistence && serverEdition.IsEvaluation)
			{
				return ServerEditionType.CoexistenceEvaluation;
			}
			return ServerEditionType.Unknown;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002BCD0 File Offset: 0x00029ED0
		public static string EncryptServerEdition(ServerEditionType edition)
		{
			ServerEditionValue serverEditionValue;
			ServerEditionValue serverEditionValue2;
			switch (edition)
			{
			case ServerEditionType.Standard:
				serverEditionValue = ServerEditionValue.Standard;
				serverEditionValue2 = ServerEditionValue.Standard;
				break;
			case ServerEditionType.StandardEvaluation:
				serverEditionValue = ServerEditionValue.Standard;
				serverEditionValue2 = ServerEditionValue.Evaluation;
				break;
			case ServerEditionType.Enterprise:
				serverEditionValue = ServerEditionValue.Enterprise;
				serverEditionValue2 = ServerEditionValue.Enterprise;
				break;
			case ServerEditionType.EnterpriseEvaluation:
				serverEditionValue = ServerEditionValue.Enterprise;
				serverEditionValue2 = ServerEditionValue.Evaluation;
				break;
			case ServerEditionType.Coexistence:
				serverEditionValue = ServerEditionValue.Coexistence;
				serverEditionValue2 = ServerEditionValue.Coexistence;
				break;
			case ServerEditionType.CoexistenceEvaluation:
				serverEditionValue = ServerEditionValue.Coexistence;
				serverEditionValue2 = ServerEditionValue.Evaluation;
				break;
			default:
				throw new ArgumentException(DataStrings.UnsupportServerEdition(edition.ToString()), "edition");
			}
			ServerEdition serverEdition = new ServerEdition(serverEditionValue, serverEditionValue2);
			return serverEdition.EncryptServerType();
		}

		// Token: 0x0400085E RID: 2142
		private const int MaxRawStringLength = 32;

		// Token: 0x0400085F RID: 2143
		private const byte seed = 73;

		// Token: 0x04000860 RID: 2144
		private ServerEditionValue serverType = ServerEditionValue.None;

		// Token: 0x04000861 RID: 2145
		private ServerEditionValue setupType = ServerEditionValue.None;
	}
}
