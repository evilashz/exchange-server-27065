using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000016 RID: 22
	public class CryptoMessage
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00006B14 File Offset: 0x00004D14
		private static byte[] Init()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 112, "Init", "f:\\15.00.1497\\sources\\dev\\clients\\src\\common\\CryptoMessage.cs");
			byte[] array = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().ObjectGuid.ToByteArray();
			byte[] array2 = topologyConfigurationSession.GetDatabasesContainerId().ObjectGuid.ToByteArray();
			byte[] array3 = new byte[array.Length + array2.Length];
			array.CopyTo(array3, 0);
			array2.CopyTo(array3, array.Length);
			if (ExTraceGlobals.CryptoTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				using (SHA256Cng sha256Cng = new SHA256Cng())
				{
					byte[] bytes = sha256Cng.ComputeHash(array3);
					ExTraceGlobals.CryptoTracer.TraceDebug<string, string, string>(0L, "{0}.{1}: adObjectIdsBinaryHash={2}", "Clients.Common.CryptoMessage", "CryptoMessage()", CryptoMessage.GetHexString(bytes));
					sha256Cng.Clear();
				}
			}
			return array3;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public CryptoMessage(byte[] message, byte[] hiddenMessage)
		{
			this.CreateSignedMessage(message, hiddenMessage);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006C00 File Offset: 0x00004E00
		public CryptoMessage(DateTime timeStamp, string message, Guid userContextId, string logonUniqueKey)
		{
			byte[] bytes = BitConverter.GetBytes(timeStamp.Ticks);
			byte[] array = CryptoMessage.EncodeToByteArray(message, CryptoMessage.EncodingKind.UTF8);
			this.CreateSignedMessage(CryptoMessage.MergeArrays(new byte[][]
			{
				bytes,
				array
			}), CryptoMessage.GetHiddenMessage(userContextId, logonUniqueKey));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006C4C File Offset: 0x00004E4C
		public static byte[] EncodeToByteArray(string message, CryptoMessage.EncodingKind encodingKind)
		{
			if (message == null)
			{
				message = string.Empty;
			}
			byte[] array = new byte[]
			{
				(byte)encodingKind
			};
			byte[] bytes;
			switch (encodingKind)
			{
			case CryptoMessage.EncodingKind.UTF8:
				bytes = new UTF8Encoding().GetBytes(message);
				goto IL_43;
			}
			bytes = new UnicodeEncoding().GetBytes(message);
			IL_43:
			return CryptoMessage.MergeArrays(new byte[][]
			{
				array,
				bytes
			});
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public static string DecodeToString(byte[] message, bool legacy)
		{
			if (message != null)
			{
				if (legacy)
				{
					if (message.Length > 0)
					{
						return new UnicodeEncoding().GetString(message);
					}
				}
				else if (message.Length > 1)
				{
					switch (message[0])
					{
					case 1:
						return new UTF8Encoding().GetString(message, 1, message.Length - 1);
					}
					return new UnicodeEncoding().GetString(message, 1, message.Length - 1);
				}
			}
			return null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006D20 File Offset: 0x00004F20
		private static string GetHexString(byte[] bytes)
		{
			if (!ExTraceGlobals.CryptoTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return null;
			}
			if (bytes == null)
			{
				return "NULL_BYTE_ARRAY";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006D78 File Offset: 0x00004F78
		private static byte[] ComputeHash(byte[] messageBinary, byte[] privateKeyBinary)
		{
			byte[] result;
			using (HMACSHA256Cng hmacsha256Cng = new HMACSHA256Cng(privateKeyBinary))
			{
				byte[] array = hmacsha256Cng.ComputeHash(messageBinary);
				hmacsha256Cng.Clear();
				result = array;
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006DBC File Offset: 0x00004FBC
		private static bool AreEqualTimeSafe(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < a.Length; i++)
			{
				num |= (int)(a[i] ^ b[i]);
			}
			return num == 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006DF8 File Offset: 0x00004FF8
		private static byte[] Clone(byte[] byteArray)
		{
			byte[] array = new byte[(byteArray == null) ? 0 : byteArray.Length];
			if (array.Length > 0)
			{
				Array.Copy(byteArray, array, array.Length);
			}
			return array;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006E28 File Offset: 0x00005028
		public static string Encode(byte[] byteArray)
		{
			int num = (byteArray.Length + 2 - (byteArray.Length + 2) % 3) / 3 * 4;
			char[] array = new char[num];
			int num2 = Convert.ToBase64CharArray(byteArray, 0, byteArray.Length, array, 0);
			for (int i = 0; i < num2; i++)
			{
				char c = array[i];
				if (c != '+')
				{
					if (c != '/')
					{
						if (c == '=')
						{
							array[i] = '.';
						}
					}
					else
					{
						array[i] = '_';
					}
				}
				else
				{
					array[i] = '-';
				}
			}
			return new string(array);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006E9C File Offset: 0x0000509C
		public static byte[] Decode(string stringToDecode)
		{
			char[] array = stringToDecode.ToCharArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				switch (c)
				{
				case '-':
					array[i] = '+';
					break;
				case '.':
					array[i] = '=';
					break;
				default:
					if (c == '_')
					{
						array[i] = '/';
					}
					break;
				}
			}
			return Convert.FromBase64CharArray(array, 0, num);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006EF8 File Offset: 0x000050F8
		public static bool ParseMessage(byte[] hashAndMessageBinary, byte[] hiddenMessageBinary, out byte[] messageBinary)
		{
			hashAndMessageBinary = (hashAndMessageBinary ?? CryptoMessage.zeroArray);
			hiddenMessageBinary = (hiddenMessageBinary ?? CryptoMessage.zeroArray);
			int num = hashAndMessageBinary.Length - 32;
			if (num < 0)
			{
				messageBinary = CryptoMessage.zeroArray;
				return false;
			}
			messageBinary = new byte[num];
			Array.Copy(hashAndMessageBinary, 32, messageBinary, 0, num);
			CryptoMessage cryptoMessage = new CryptoMessage(messageBinary, hiddenMessageBinary);
			return CryptoMessage.AreEqualTimeSafe(cryptoMessage.hashAndMessageBinary, hashAndMessageBinary);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006F5C File Offset: 0x0000515C
		public static byte[] GetHiddenMessage(Guid userContextId, string logonUniqueKey)
		{
			byte[] array = userContextId.ToByteArray();
			byte[] bytes = new UnicodeEncoding().GetBytes(logonUniqueKey ?? string.Empty);
			return CryptoMessage.MergeArrays(new byte[][]
			{
				array,
				bytes
			});
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F9C File Offset: 0x0000519C
		private void CreateSignedMessage(byte[] message, byte[] hiddenMessage)
		{
			this.messageBinary = CryptoMessage.Clone(message);
			this.hiddenMessageBinary = CryptoMessage.Clone(hiddenMessage);
			this.privateKeyBinary = CryptoMessage.MergeArrays(new byte[][]
			{
				CryptoMessage.adObjectIdsBinary,
				this.hiddenMessageBinary
			});
			this.hashBinary = CryptoMessage.ComputeHash(this.messageBinary, this.privateKeyBinary);
			this.hashAndMessageBinary = CryptoMessage.MergeArrays(new byte[][]
			{
				this.hashBinary,
				this.messageBinary
			});
			this.hashAndMessageString = CryptoMessage.Encode(this.hashAndMessageBinary);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007034 File Offset: 0x00005234
		public static byte[] MergeArrays(params byte[][] inpArrays)
		{
			if (inpArrays == null || inpArrays.Length == 0)
			{
				return null;
			}
			int num = 0;
			foreach (byte[] array in inpArrays)
			{
				num += array.Length;
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			foreach (byte[] array3 in inpArrays)
			{
				Array.Copy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return array2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000070AB File Offset: 0x000052AB
		public static bool ParseMessage(string hashAndMessage, byte[] hiddenMessageBinary, out DateTime timeStamp, out byte[] message)
		{
			return CryptoMessage.ParseMessage(hashAndMessage, hiddenMessageBinary, false, out timeStamp, out message);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000070B8 File Offset: 0x000052B8
		public static bool ParseMessage(string hashAndMessage, byte[] hiddenMessageBinary, bool ignoreHmac, out DateTime timeStamp, out byte[] message)
		{
			timeStamp = DateTime.MinValue;
			bool flag = false;
			message = null;
			try
			{
				byte[] array = CryptoMessage.Decode(hashAndMessage ?? string.Empty);
				byte[] array2;
				flag = CryptoMessage.ParseMessage(array, hiddenMessageBinary, out array2);
				if (flag || ignoreHmac)
				{
					long ticks = BitConverter.ToInt64(array2, 0);
					timeStamp = new DateTime(ticks, DateTimeKind.Utc);
					int num = array2.Length - 8;
					message = new byte[num];
					Array.Copy(array2, 8, message, 0, num);
				}
				else
				{
					ExTraceGlobals.CryptoTracer.TraceDebug(2L, "{0}.{1}: failed: messageString={2}, hiddenMessage={3}", new object[]
					{
						"Clients.Common.CryptoMessage",
						"ParseMessage",
						hashAndMessage,
						CryptoMessage.GetHexString(hiddenMessageBinary)
					});
				}
			}
			catch (Exception ex)
			{
				flag = false;
				ExTraceGlobals.CryptoTracer.TraceDebug(3L, "{0}.{1}: Exception: messageString={2}, hiddenMessage={3}, exception={3}", new object[]
				{
					"Clients.Common.CryptoMessage",
					"ParseMessage",
					hashAndMessage,
					CryptoMessage.GetHexString(hiddenMessageBinary),
					ex
				});
			}
			return flag;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000071C4 File Offset: 0x000053C4
		public static string ExtractUrl(string hashAndMessage, bool legacyFormat)
		{
			DateTime dateTime;
			byte[] message;
			CryptoMessage.ParseMessage(hashAndMessage, null, true, out dateTime, out message);
			return CryptoMessage.DecodeToString(message, legacyFormat);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000071E5 File Offset: 0x000053E5
		public override string ToString()
		{
			return this.hashAndMessageString;
		}

		// Token: 0x04000218 RID: 536
		private const string ThisClassName = "Clients.Common.CryptoMessage";

		// Token: 0x04000219 RID: 537
		private const int HashLength = 32;

		// Token: 0x0400021A RID: 538
		private const int TimeStampLength = 8;

		// Token: 0x0400021B RID: 539
		private static byte[] zeroArray = new byte[0];

		// Token: 0x0400021C RID: 540
		private static byte[] adObjectIdsBinary = CryptoMessage.Init();

		// Token: 0x0400021D RID: 541
		private string hashAndMessageString;

		// Token: 0x0400021E RID: 542
		private byte[] hashAndMessageBinary;

		// Token: 0x0400021F RID: 543
		private byte[] hashBinary;

		// Token: 0x04000220 RID: 544
		private byte[] messageBinary;

		// Token: 0x04000221 RID: 545
		private byte[] hiddenMessageBinary;

		// Token: 0x04000222 RID: 546
		private byte[] privateKeyBinary;

		// Token: 0x02000017 RID: 23
		public enum EncodingKind : byte
		{
			// Token: 0x04000224 RID: 548
			Unicode,
			// Token: 0x04000225 RID: 549
			UTF8
		}
	}
}
