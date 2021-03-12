using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001F4 RID: 500
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbStatus
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000226F0 File Offset: 0x000208F0
		internal MdbStatus(MDBSTATUSRAW pMdbStatus, IntPtr pStartAddress)
		{
			this.mdbGuid = pMdbStatus.guidMdb;
			this.status = (MdbStatusFlags)pMdbStatus.ulStatus;
			if (pMdbStatus.ibMdbName != 0U)
			{
				IntPtr ptr = (IntPtr)((long)pStartAddress + (long)((ulong)pMdbStatus.ibMdbName));
				if (Marshal.ReadByte(ptr, (int)(pMdbStatus.cbMdbName - 1U)) == 0)
				{
					this.mdbName = Marshal.PtrToStringAnsi(ptr, (int)(pMdbStatus.cbMdbName - 1U));
				}
			}
			if (pMdbStatus.ibVServerName != 0U)
			{
				IntPtr ptr2 = (IntPtr)((long)pStartAddress + (long)((ulong)pMdbStatus.ibVServerName));
				if (Marshal.ReadByte(ptr2, (int)(pMdbStatus.cbVServerName - 1U)) == 0)
				{
					this.vServerName = Marshal.PtrToStringAnsi(ptr2, (int)(pMdbStatus.cbVServerName - 1U));
				}
			}
			if (pMdbStatus.ibMdbLegacyDN != 0U)
			{
				IntPtr ptr3 = (IntPtr)((long)pStartAddress + (long)((ulong)pMdbStatus.ibMdbLegacyDN));
				if (Marshal.ReadByte(ptr3, (int)(pMdbStatus.cbMdbLegacyDN - 1U)) == 0)
				{
					this.mdbLegacyDN = Marshal.PtrToStringAnsi(ptr3, (int)(pMdbStatus.cbMdbLegacyDN - 1U));
				}
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002280A File Offset: 0x00020A0A
		internal MdbStatus(Guid _mdbGuid, uint _status)
		{
			this.mdbGuid = _mdbGuid;
			this.status = (MdbStatusFlags)_status;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00022841 File Offset: 0x00020A41
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00022849 File Offset: 0x00020A49
		public MdbStatusFlags Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00022851 File Offset: 0x00020A51
		public string MdbName
		{
			get
			{
				return this.mdbName;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00022859 File Offset: 0x00020A59
		public string VServerName
		{
			get
			{
				return this.vServerName;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00022861 File Offset: 0x00020A61
		public string MdbLegacyDN
		{
			get
			{
				return this.mdbLegacyDN;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002286C File Offset: 0x00020A6C
		public override string ToString()
		{
			string str = string.Empty;
			if (this.vServerName != string.Empty)
			{
				str = string.Format("Virtual Server {0} ", this.vServerName);
			}
			if (this.mdbName != string.Empty)
			{
				str += string.Format("MDB {0} ", this.mdbName);
			}
			return str + string.Format("({0}): {1}\n", this.mdbGuid, this.status);
		}

		// Token: 0x040006D2 RID: 1746
		private Guid mdbGuid;

		// Token: 0x040006D3 RID: 1747
		private MdbStatusFlags status;

		// Token: 0x040006D4 RID: 1748
		private string mdbName = string.Empty;

		// Token: 0x040006D5 RID: 1749
		private string vServerName = string.Empty;

		// Token: 0x040006D6 RID: 1750
		private string mdbLegacyDN = string.Empty;
	}
}
