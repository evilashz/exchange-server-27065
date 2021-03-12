using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public class DagNetworkObjectId : ObjectId
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000374DC File Offset: 0x000356DC
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x000374E4 File Offset: 0x000356E4
		public string DagName
		{
			get
			{
				return this.m_dagName;
			}
			set
			{
				if (this.m_fullName != null)
				{
					this.m_fullName = null;
				}
				this.m_dagName = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000374FC File Offset: 0x000356FC
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x00037504 File Offset: 0x00035704
		public string NetName
		{
			get
			{
				return this.m_netName;
			}
			set
			{
				if (this.m_fullName != null)
				{
					this.m_fullName = null;
				}
				this.m_netName = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0003751C File Offset: 0x0003571C
		public string FullName
		{
			get
			{
				if (this.m_fullName == null)
				{
					this.m_fullName = DagNetworkObjectId.BuildCompositeName(this.DagName, this.NetName);
				}
				return this.m_fullName;
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00037543 File Offset: 0x00035743
		public override byte[] GetBytes()
		{
			if (this.DagName == null)
			{
				return null;
			}
			return Encoding.UTF8.GetBytes(this.FullName);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003755F File Offset: 0x0003575F
		public DagNetworkObjectId(string dagName, string netName)
		{
			this.m_dagName = dagName;
			this.m_netName = netName;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00037578 File Offset: 0x00035778
		public DagNetworkObjectId(string compositeName)
		{
			string[] array = compositeName.Split(new char[]
			{
				'\\'
			});
			if (array.Length >= 1)
			{
				this.m_dagName = array[0];
				if (array.Length > 1)
				{
					this.m_netName = array[1];
				}
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000375C0 File Offset: 0x000357C0
		public override bool Equals(object obj)
		{
			DagNetworkObjectId dagNetworkObjectId = obj as DagNetworkObjectId;
			return dagNetworkObjectId != null && this.NetName.Equals(dagNetworkObjectId.NetName) && this.DagName.Equals(dagNetworkObjectId.DagName);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000375FF File Offset: 0x000357FF
		public override int GetHashCode()
		{
			return this.NetName.GetHashCode() ^ this.DagName.GetHashCode();
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00037618 File Offset: 0x00035818
		private static string BuildCompositeName(string dagName, string netName)
		{
			ExAssert.RetailAssert(!string.IsNullOrEmpty(dagName), "dagName must be provided");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(dagName);
			if (!string.IsNullOrEmpty(netName))
			{
				stringBuilder.Append('\\');
				stringBuilder.Append(netName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00037665 File Offset: 0x00035865
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000AF8 RID: 2808
		internal const char ElementSeparatorChar = '\\';

		// Token: 0x04000AF9 RID: 2809
		private string m_dagName;

		// Token: 0x04000AFA RID: 2810
		private string m_netName;

		// Token: 0x04000AFB RID: 2811
		private string m_fullName;
	}
}
