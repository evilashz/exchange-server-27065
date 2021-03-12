using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000715 RID: 1813
	[ComVisible(true)]
	public struct SerializationEntry
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0011C00A File Offset: 0x0011A20A
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0011C012 File Offset: 0x0011A212
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x0011C01A File Offset: 0x0011A21A
		public Type ObjectType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x0011C022 File Offset: 0x0011A222
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this.m_value = entryValue;
			this.m_name = entryName;
			this.m_type = entryType;
		}

		// Token: 0x0400239D RID: 9117
		private Type m_type;

		// Token: 0x0400239E RID: 9118
		private object m_value;

		// Token: 0x0400239F RID: 9119
		private string m_name;
	}
}
