using System;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionContextIdentifier
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0001124C File Offset: 0x0000F44C
		public SessionContextIdentifier()
		{
			this.contextId = Interlocked.Increment(ref SessionContextIdentifier.nextContextId);
			this.contextCookie = SessionContextIdentifier.BuildContextCookie(SessionContextIdentifier.CookieInstancePrefix, this.contextId);
			this.sequenceCounter = 0;
			this.nextSequenceCookie = SessionContextIdentifier.GenerateSequenceCookie(this.sequenceCounter);
			this.previousSequenceCookie = string.Empty;
			this.currentSequenceCookie = null;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000112BA File Offset: 0x0000F4BA
		public long Id
		{
			get
			{
				return this.contextId;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000112C2 File Offset: 0x0000F4C2
		public string Cookie
		{
			get
			{
				return this.contextCookie;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000112CA File Offset: 0x0000F4CA
		public string NextSequenceCookie
		{
			get
			{
				return this.nextSequenceCookie;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000112D2 File Offset: 0x0000F4D2
		private static Random Random
		{
			get
			{
				if (SessionContextIdentifier.random == null)
				{
					SessionContextIdentifier.random = new Random();
				}
				return SessionContextIdentifier.random;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000112EC File Offset: 0x0000F4EC
		private static byte[] CookieInstancePrefix
		{
			get
			{
				if (SessionContextIdentifier.cookieInstancePrefix == null)
				{
					lock (SessionContextIdentifier.cookieInstancePrefixLock)
					{
						if (SessionContextIdentifier.cookieInstancePrefix == null)
						{
							SessionContextIdentifier.cookieInstancePrefix = SessionContextIdentifier.BuildCookieInstancePrefix(Environment.MachineName, DateTime.UtcNow, SessionContextIdentifier.Random.Next());
						}
					}
				}
				return SessionContextIdentifier.cookieInstancePrefix;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011358 File Offset: 0x0000F558
		public static bool TryGetIdFromCookie(string cookie, out long id, out Exception failureException)
		{
			id = 0L;
			failureException = null;
			byte[] array;
			try
			{
				array = Convert.FromBase64String(cookie);
			}
			catch (FormatException)
			{
				failureException = ProtocolException.FromResponseCode((LID)54688, "Context cookie is not proper Base64 encoding.", ResponseCode.InvalidContextCookie, null);
				return false;
			}
			byte[] array2 = SessionContextIdentifier.CookieInstancePrefix;
			if (array.Length < array2.Length + 8)
			{
				if (!SessionContextIdentifier.TryGetRoutingFailure(array, out failureException))
				{
					failureException = ProtocolException.FromResponseCode((LID)42400, "Context cookie not found.", ResponseCode.ContextNotFound, null);
				}
				return false;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (array[i] != array2[i])
				{
					if (!SessionContextIdentifier.TryGetRoutingFailure(array, out failureException))
					{
						failureException = ProtocolException.FromResponseCode((LID)58784, "Context cookie not found.", ResponseCode.ContextNotFound, null);
					}
					return false;
				}
			}
			id = BitConverter.ToInt64(array, array2.Length);
			return true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011410 File Offset: 0x0000F610
		public string BeginSequenceOperation(string sequenceCookie)
		{
			string result;
			lock (this.sequenceLock)
			{
				if (this.currentSequenceCookie != null)
				{
					throw ProtocolException.FromResponseCode((LID)44316, string.Format("Currently processing a sequenced operation; found={0}, current={1}.", sequenceCookie, this.currentSequenceCookie), ResponseCode.InvalidSequence, null);
				}
				if (string.Compare(sequenceCookie, this.nextSequenceCookie, true) != 0)
				{
					if (string.Compare(sequenceCookie, this.previousSequenceCookie, true) == 0)
					{
						throw ProtocolException.FromResponseCode((LID)39456, string.Format("Request contains previous sequence cookie; found={0}, expected={1}.", sequenceCookie, this.nextSequenceCookie), ResponseCode.InvalidSequence, null);
					}
					throw ProtocolException.FromResponseCode((LID)39456, string.Format("Request contains wrong sequence cookie; found={0}, expected={1}.", sequenceCookie, this.nextSequenceCookie), ResponseCode.InvalidSequence, null);
				}
				else
				{
					this.sequenceCounter++;
					this.previousSequenceCookie = this.nextSequenceCookie;
					this.currentSequenceCookie = this.nextSequenceCookie;
					this.nextSequenceCookie = SessionContextIdentifier.GenerateSequenceCookie(this.sequenceCounter);
					result = this.nextSequenceCookie;
				}
			}
			return result;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001150C File Offset: 0x0000F70C
		public void EndSequenceOperation()
		{
			this.currentSequenceCookie = null;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011518 File Offset: 0x0000F718
		internal static byte[] BuildCookieInstancePrefix(string machineName, DateTime creationTime, int randomNumber)
		{
			Util.ThrowOnNullArgument(machineName, "machineName");
			byte[] array = SessionContextIdentifier.Scramble(new ArraySegment<byte>(Encoding.UTF8.GetBytes(string.Format("{0}#{1}#{2}", machineName, creationTime.ToString("u"), randomNumber))));
			byte[] bytes = BitConverter.GetBytes(0);
			byte[] array2 = new byte[SessionContextIdentifier.cookieInstanceSignature.Length + bytes.Length + array.Length];
			Array.Copy(SessionContextIdentifier.cookieInstanceSignature, 0, array2, 0, SessionContextIdentifier.cookieInstanceSignature.Length);
			Array.Copy(bytes, 0, array2, SessionContextIdentifier.cookieInstanceSignature.Length, bytes.Length);
			Array.Copy(array, 0, array2, SessionContextIdentifier.cookieInstanceSignature.Length + bytes.Length, array.Length);
			return array2;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000115BC File Offset: 0x0000F7BC
		internal static string BuildContextCookie(byte[] cookieInstancePrefix, long contextId)
		{
			Util.ThrowOnNullArgument(cookieInstancePrefix, "cookieInstancePrefix");
			byte[] bytes = BitConverter.GetBytes(contextId);
			byte[] array = new byte[cookieInstancePrefix.Length + bytes.Length];
			Array.Copy(cookieInstancePrefix, 0, array, 0, cookieInstancePrefix.Length);
			Array.Copy(bytes, 0, array, cookieInstancePrefix.Length, bytes.Length);
			return Convert.ToBase64String(array);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011608 File Offset: 0x0000F808
		private static string GenerateSequenceCookie(int sequenceCounter)
		{
			return string.Format("{0}-{1}", sequenceCounter, Convert.ToBase64String(BitConverter.GetBytes(SessionContextIdentifier.Random.Next())));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00011630 File Offset: 0x0000F830
		private static bool TryGetRoutingInfo(byte[] cookieBytes, out string backendServer)
		{
			backendServer = null;
			if (cookieBytes.Length <= SessionContextIdentifier.cookieInstanceSignature.Length + 4 + 8)
			{
				return false;
			}
			for (int i = 0; i < SessionContextIdentifier.cookieInstanceSignature.Length; i++)
			{
				if (cookieBytes[i] != SessionContextIdentifier.cookieInstanceSignature[i])
				{
					return false;
				}
			}
			try
			{
				int num = BitConverter.ToInt32(cookieBytes, SessionContextIdentifier.cookieInstanceSignature.Length);
				if (num != 0)
				{
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
			bool result;
			try
			{
				string @string = Encoding.UTF8.GetString(SessionContextIdentifier.Unscramble(new ArraySegment<byte>(cookieBytes, SessionContextIdentifier.cookieInstanceSignature.Length + 4, cookieBytes.Length - (SessionContextIdentifier.cookieInstanceSignature.Length + 4 + 8))));
				string[] array = @string.Split(SessionContextIdentifier.cookieSeparator, StringSplitOptions.RemoveEmptyEntries);
				if (array == null || array.Length < 1)
				{
					result = false;
				}
				else
				{
					backendServer = array[0];
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011708 File Offset: 0x0000F908
		private static bool TryGetRoutingFailure(byte[] cookieBytes, out Exception failureException)
		{
			failureException = null;
			string text;
			if (!SessionContextIdentifier.TryGetRoutingInfo(cookieBytes, out text))
			{
				return false;
			}
			if (Environment.MachineName != text)
			{
				failureException = ProtocolException.FromResponseCode((LID)55068, string.Format("Context cookie for a different BE machine [{0}]; possible failover detected.", text), ResponseCode.ContextNotFound, null);
				return true;
			}
			failureException = ProtocolException.FromResponseCode((LID)63228, "Context cookie for previous instance on this BE machine; possible application pool recycle detected.", ResponseCode.ContextNotFound, null);
			return true;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011764 File Offset: 0x0000F964
		private static byte[] Scramble(ArraySegment<byte> data)
		{
			if (data.Count == 0)
			{
				return Array<byte>.Empty;
			}
			byte[] array = new byte[data.Count];
			Array.Copy(data.Array, data.Offset, array, 0, array.Length);
			array[0] = (array[0] ^ 165);
			int i = 0;
			int num = 1;
			while (i < array.Length - 1)
			{
				array[num] ^= array[num - 1];
				num++;
				i++;
			}
			return array;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000117D8 File Offset: 0x0000F9D8
		private static byte[] Unscramble(ArraySegment<byte> data)
		{
			if (data.Count == 0)
			{
				return Array<byte>.Empty;
			}
			byte[] array = new byte[data.Count];
			Array.Copy(data.Array, data.Offset, array, 0, array.Length);
			int i = 0;
			int num = array.Length - 1;
			while (i < array.Length - 1)
			{
				array[num] ^= array[num - 1];
				num--;
				i++;
			}
			array[0] = (array[0] ^ 165);
			return array;
		}

		// Token: 0x0400013C RID: 316
		private const int CurrentCookieVersion = 0;

		// Token: 0x0400013D RID: 317
		private static readonly byte[] cookieInstanceSignature = new byte[]
		{
			48,
			3,
			200
		};

		// Token: 0x0400013E RID: 318
		private static readonly char[] cookieSeparator = new char[]
		{
			'#'
		};

		// Token: 0x0400013F RID: 319
		private static readonly object cookieInstancePrefixLock = new object();

		// Token: 0x04000140 RID: 320
		private static byte[] cookieInstancePrefix = null;

		// Token: 0x04000141 RID: 321
		private static long nextContextId = 0L;

		// Token: 0x04000142 RID: 322
		[ThreadStatic]
		private static Random random = null;

		// Token: 0x04000143 RID: 323
		private readonly string contextCookie;

		// Token: 0x04000144 RID: 324
		private readonly long contextId;

		// Token: 0x04000145 RID: 325
		private readonly object sequenceLock = new object();

		// Token: 0x04000146 RID: 326
		private int sequenceCounter;

		// Token: 0x04000147 RID: 327
		private string currentSequenceCookie;

		// Token: 0x04000148 RID: 328
		private string previousSequenceCookie;

		// Token: 0x04000149 RID: 329
		private string nextSequenceCookie;
	}
}
