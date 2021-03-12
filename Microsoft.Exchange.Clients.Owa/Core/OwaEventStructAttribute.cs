using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000191 RID: 401
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class OwaEventStructAttribute : Attribute
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0005D5C2 File Offset: 0x0005B7C2
		public OwaEventStructAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.fieldInfoTable = new Hashtable();
			this.fieldInfoIndexTable = new OwaEventFieldAttribute[32];
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0005D5F7 File Offset: 0x0005B7F7
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0005D5FF File Offset: 0x0005B7FF
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0005D607 File Offset: 0x0005B807
		internal uint RequiredMask
		{
			get
			{
				return this.requiredMask;
			}
			set
			{
				this.requiredMask = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0005D610 File Offset: 0x0005B810
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0005D618 File Offset: 0x0005B818
		internal uint AllFieldsMask
		{
			get
			{
				return this.allFieldsMask;
			}
			set
			{
				this.allFieldsMask = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0005D621 File Offset: 0x0005B821
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0005D629 File Offset: 0x0005B829
		internal int FieldCount
		{
			get
			{
				return this.fieldCount;
			}
			set
			{
				this.fieldCount = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0005D632 File Offset: 0x0005B832
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x0005D63A File Offset: 0x0005B83A
		internal Type StructType
		{
			get
			{
				return this.structType;
			}
			set
			{
				this.structType = value;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005D643 File Offset: 0x0005B843
		internal void AddFieldInfo(OwaEventFieldAttribute fieldInfo, int index)
		{
			this.fieldInfoTable.Add(fieldInfo.Name, fieldInfo);
			this.fieldInfoIndexTable[index] = fieldInfo;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0005D660 File Offset: 0x0005B860
		internal OwaEventFieldAttribute FindFieldInfo(string name)
		{
			return (OwaEventFieldAttribute)this.fieldInfoTable[name];
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0005D673 File Offset: 0x0005B873
		internal Hashtable FieldInfoTable
		{
			get
			{
				return this.fieldInfoTable;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0005D67B File Offset: 0x0005B87B
		internal OwaEventFieldAttribute[] FieldInfoIndexTable
		{
			get
			{
				return this.fieldInfoIndexTable;
			}
		}

		// Token: 0x040009FE RID: 2558
		private string name;

		// Token: 0x040009FF RID: 2559
		private Hashtable fieldInfoTable;

		// Token: 0x04000A00 RID: 2560
		private OwaEventFieldAttribute[] fieldInfoIndexTable;

		// Token: 0x04000A01 RID: 2561
		private Type structType;

		// Token: 0x04000A02 RID: 2562
		private uint requiredMask;

		// Token: 0x04000A03 RID: 2563
		private uint allFieldsMask;

		// Token: 0x04000A04 RID: 2564
		private int fieldCount;
	}
}
