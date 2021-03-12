using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A6 RID: 1958
	internal class InvalidationMessage
	{
		// Token: 0x06006149 RID: 24905 RVA: 0x0014B2FC File Offset: 0x001494FC
		public InvalidationMessage(Guid orgId, Guid[] keys) : this(orgId, keys, InvalidationMessage.IsGlobalCacheEntry(keys))
		{
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0014B30C File Offset: 0x0014950C
		private InvalidationMessage(Guid orgId, Guid[] keys, bool isGlobal)
		{
			if (keys == null || keys.Length == 0)
			{
				throw new ArgumentException("The keys are either null or empty.");
			}
			this.OrganizationId = orgId;
			this.CacheKeys = keys;
			this.IsGlobal = isGlobal;
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x0014B33C File Offset: 0x0014953C
		// (set) Token: 0x0600614C RID: 24908 RVA: 0x0014B344 File Offset: 0x00149544
		public Guid OrganizationId { get; private set; }

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x0600614D RID: 24909 RVA: 0x0014B34D File Offset: 0x0014954D
		// (set) Token: 0x0600614E RID: 24910 RVA: 0x0014B355 File Offset: 0x00149555
		public Guid[] CacheKeys { get; private set; }

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x0600614F RID: 24911 RVA: 0x0014B35E File Offset: 0x0014955E
		public bool IsCacheClearMessage
		{
			get
			{
				return this.CacheKeys[0].Equals(Guid.Empty);
			}
		}

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06006150 RID: 24912 RVA: 0x0014B376 File Offset: 0x00149576
		// (set) Token: 0x06006151 RID: 24913 RVA: 0x0014B37E File Offset: 0x0014957E
		public bool IsGlobal { get; private set; }

		// Token: 0x06006152 RID: 24914 RVA: 0x0014B388 File Offset: 0x00149588
		public static InvalidationMessage TryFromReceivedData(byte[] buffer, int bufLen, out Exception ex)
		{
			ex = null;
			InvalidationMessage result = null;
			try
			{
				result = InvalidationMessage.FromReceivedData(buffer, bufLen);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			return result;
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x0014B3BC File Offset: 0x001495BC
		public static InvalidationMessage FromReceivedData(byte[] buffer, int bufLen)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (bufLen <= 0)
			{
				throw new ArgumentException("bufLen is less than zero.");
			}
			if (buffer.Length < bufLen)
			{
				throw new ArgumentException("The buffer is too small.");
			}
			string @string = Encoding.UTF8.GetString(buffer, 0, bufLen);
			string[] array = @string.Split(new char[]
			{
				'\n'
			});
			if (array == null || array.Length != 2)
			{
				throw new ArgumentException("Message is invalid.");
			}
			Guid empty = Guid.Empty;
			bool isGlobal = false;
			if (!array[0].Equals("GlobalCacheKeys", StringComparison.OrdinalIgnoreCase))
			{
				empty = new Guid(array[0]);
			}
			else
			{
				isGlobal = true;
			}
			string[] array2 = array[1].Split(new char[]
			{
				';'
			});
			Guid[] array3 = new Guid[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array3[i] = new Guid(array2[i]);
			}
			return new InvalidationMessage(empty, array3, isGlobal);
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0014B4B4 File Offset: 0x001496B4
		public static bool IsGlobalCacheEntry(Guid[] keys)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			foreach (Guid a in keys)
			{
				foreach (Guid b in CannedProvisioningCacheKeys.GlobalCacheKeys)
				{
					if (a == b)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0014B52C File Offset: 0x0014972C
		public byte[] ToSendMessage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsGlobal)
			{
				stringBuilder.Append("GlobalCacheKeys");
			}
			else
			{
				stringBuilder.Append(this.OrganizationId.ToString());
			}
			stringBuilder.Append('\n');
			foreach (Guid guid in this.CacheKeys)
			{
				stringBuilder.Append(guid.ToString());
				stringBuilder.Append(';');
			}
			string s = stringBuilder.ToString().TrimEnd(new char[]
			{
				';'
			});
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x04004154 RID: 16724
		private const char OrgIdDelimiter = '\n';

		// Token: 0x04004155 RID: 16725
		private const char Separator = ';';

		// Token: 0x04004156 RID: 16726
		private const string GlobalCacheKeys = "GlobalCacheKeys";

		// Token: 0x04004157 RID: 16727
		public static readonly int Version = 1;
	}
}
