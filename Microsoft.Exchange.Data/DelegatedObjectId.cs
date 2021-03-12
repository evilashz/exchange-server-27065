using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class DelegatedObjectId : ObjectId, IEquatable<DelegatedObjectId>
	{
		// Token: 0x060012DC RID: 4828 RVA: 0x00039B3C File Offset: 0x00037D3C
		public DelegatedObjectId() : this(string.Empty, string.Empty)
		{
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00039B4E File Offset: 0x00037D4E
		public DelegatedObjectId(string userName) : this(userName, string.Empty)
		{
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00039B5C File Offset: 0x00037D5C
		public DelegatedObjectId(string userName, string orgName)
		{
			this.orgName = orgName;
			this.userName = userName;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00039B72 File Offset: 0x00037D72
		public string Organization
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00039B7A File Offset: 0x00037D7A
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00039B82 File Offset: 0x00037D82
		public override byte[] GetBytes()
		{
			return this.GetBytes(Encoding.Unicode);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00039B90 File Offset: 0x00037D90
		public byte[] GetBytes(Encoding encoding)
		{
			int byteCount = encoding.GetByteCount(this.toStringValue);
			byte[] array = new byte[byteCount];
			encoding.GetBytes(this.toStringValue, 0, this.toStringValue.Length, array, 0);
			return array;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00039BD4 File Offset: 0x00037DD4
		public override string ToString()
		{
			if (this.toStringValue == null)
			{
				if (!string.IsNullOrEmpty(this.orgName) && !string.IsNullOrEmpty(this.userName))
				{
					this.toStringValue = this.orgName + DelegatedObjectId.Separator + this.userName;
				}
				else if (!string.IsNullOrEmpty(this.userName))
				{
					this.toStringValue = this.userName;
				}
				else
				{
					this.toStringValue = string.Empty;
				}
			}
			return this.toStringValue;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00039C57 File Offset: 0x00037E57
		public override bool Equals(object obj)
		{
			return obj != null && this.Equals(obj as DelegatedObjectId);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00039C6A File Offset: 0x00037E6A
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00039C77 File Offset: 0x00037E77
		public bool Equals(DelegatedObjectId other)
		{
			return other != null && this.ToString().Equals(other.ToString());
		}

		// Token: 0x04000B27 RID: 2855
		private string orgName;

		// Token: 0x04000B28 RID: 2856
		private string userName;

		// Token: 0x04000B29 RID: 2857
		private volatile string toStringValue;

		// Token: 0x04000B2A RID: 2858
		private static string Separator = "\\";
	}
}
