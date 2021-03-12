using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200038A RID: 906
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x06002E27 RID: 11815 RVA: 0x000B0EE8 File Offset: 0x000AF0E8
		[SecurityCritical]
		internal unsafe CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000B0F38 File Offset: 0x000AF138
		[SecurityCritical]
		internal unsafe static string CreateString(sbyte* pStrings, uint index)
		{
			if (*pStrings == 124)
			{
				int num = 1;
				int num2 = 1;
				for (;;)
				{
					sbyte b = pStrings[num2];
					if (b == 124 || b == 0)
					{
						if (index == 0U)
						{
							break;
						}
						index -= 1U;
						num = num2 + 1;
						if (b == 0)
						{
							goto IL_37;
						}
					}
					num2++;
				}
				return new string(pStrings, num, num2 - num);
				IL_37:
				throw new ArgumentException("pStrings");
			}
			return new string(pStrings);
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000B0F8D File Offset: 0x000AF18D
		public unsafe string WebName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000B0FC2 File Offset: 0x000AF1C2
		public virtual int UIFamilyCodePage
		{
			get
			{
				return this.m_uiFamilyCodePage;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000B0FCA File Offset: 0x000AF1CA
		public unsafe string HeaderName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_headerName == null)
				{
					this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
				}
				return this.m_headerName;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000B0FFF File Offset: 0x000AF1FF
		public unsafe string BodyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_bodyName == null)
				{
					this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
				}
				return this.m_bodyName;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000B1034 File Offset: 0x000AF234
		public uint Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001354 RID: 4948
		internal int m_dataIndex;

		// Token: 0x04001355 RID: 4949
		internal int m_uiFamilyCodePage;

		// Token: 0x04001356 RID: 4950
		internal string m_webName;

		// Token: 0x04001357 RID: 4951
		internal string m_headerName;

		// Token: 0x04001358 RID: 4952
		internal string m_bodyName;

		// Token: 0x04001359 RID: 4953
		internal uint m_flags;
	}
}
