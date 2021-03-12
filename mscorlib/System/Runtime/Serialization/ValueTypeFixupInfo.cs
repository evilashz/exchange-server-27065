using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072F RID: 1839
	internal class ValueTypeFixupInfo
	{
		// Token: 0x060051D5 RID: 20949 RVA: 0x0011F094 File Offset: 0x0011D294
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyParent"));
			}
			if (containerID == 0L && member == null)
			{
				this.m_containerID = containerID;
				this.m_parentField = member;
				this.m_parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MemberAndArray"));
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyContainer"));
				}
			}
			this.m_containerID = containerID;
			this.m_parentField = member;
			this.m_parentIndex = parentIndex;
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x0011F135 File Offset: 0x0011D335
		public long ContainerID
		{
			get
			{
				return this.m_containerID;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0011F13D File Offset: 0x0011D33D
		public FieldInfo ParentField
		{
			get
			{
				return this.m_parentField;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x0011F145 File Offset: 0x0011D345
		public int[] ParentIndex
		{
			get
			{
				return this.m_parentIndex;
			}
		}

		// Token: 0x0400240E RID: 9230
		private long m_containerID;

		// Token: 0x0400240F RID: 9231
		private FieldInfo m_parentField;

		// Token: 0x04002410 RID: 9232
		private int[] m_parentIndex;
	}
}
