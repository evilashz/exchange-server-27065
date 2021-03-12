using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Providers
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	internal class AlternateMailboxObjectId : ObjectId
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002225 File Offset: 0x00000425
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000222D File Offset: 0x0000042D
		internal Guid? UserId
		{
			get
			{
				return this.m_userId;
			}
			set
			{
				this.m_userId = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002236 File Offset: 0x00000436
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000223E File Offset: 0x0000043E
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
			set
			{
				if (this.m_fullName != null)
				{
					this.m_fullName = null;
				}
				this.m_userName = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000225E File Offset: 0x0000045E
		internal Guid? AmId
		{
			get
			{
				return this.m_amId;
			}
			set
			{
				this.m_amId = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000226F File Offset: 0x0000046F
		public string AmName
		{
			get
			{
				return this.m_amName;
			}
			set
			{
				if (this.m_fullName != null)
				{
					this.m_fullName = null;
				}
				this.m_amName = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002287 File Offset: 0x00000487
		public string FullName
		{
			get
			{
				if (this.m_fullName == null)
				{
					this.m_fullName = AlternateMailboxObjectId.BuildCompositeName(this.UserName, this.AmName);
				}
				return this.m_fullName;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022AE File Offset: 0x000004AE
		public override byte[] GetBytes()
		{
			if (this.UserName == null)
			{
				return null;
			}
			return Encoding.UTF8.GetBytes(this.FullName);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022CA File Offset: 0x000004CA
		internal AlternateMailboxObjectId(string userName, string amName, Guid userId, Guid? amId)
		{
			this.m_userName = userName;
			this.m_amName = amName;
			this.m_userId = new Guid?(userId);
			this.m_amId = amId;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022F4 File Offset: 0x000004F4
		public AlternateMailboxObjectId(string userName, string amName)
		{
			this.m_userName = userName;
			this.m_amName = amName;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000230C File Offset: 0x0000050C
		public AlternateMailboxObjectId(string compositeName)
		{
			int num = compositeName.LastIndexOf('\\');
			if (num == -1)
			{
				this.m_userName = compositeName;
				return;
			}
			if (num != compositeName.Length - 1)
			{
				this.m_amName = compositeName.Substring(num + 1);
			}
			if (num != 0)
			{
				this.m_userName = compositeName.Substring(0, num);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002360 File Offset: 0x00000560
		private static string BuildCompositeName(string userName, string amName)
		{
			ExAssert.RetailAssert(!string.IsNullOrEmpty(userName), "userName must be provided");
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(userName);
			if (!string.IsNullOrEmpty(amName))
			{
				stringBuilder.Append('\\');
				stringBuilder.Append(amName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023AF File Offset: 0x000005AF
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000002 RID: 2
		internal const char ElementSeparatorChar = '\\';

		// Token: 0x04000003 RID: 3
		private Guid? m_userId;

		// Token: 0x04000004 RID: 4
		private string m_userName;

		// Token: 0x04000005 RID: 5
		private Guid? m_amId;

		// Token: 0x04000006 RID: 6
		private string m_amName;

		// Token: 0x04000007 RID: 7
		private string m_fullName;
	}
}
