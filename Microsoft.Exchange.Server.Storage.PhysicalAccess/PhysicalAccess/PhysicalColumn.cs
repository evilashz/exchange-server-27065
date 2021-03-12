using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200000B RID: 11
	public abstract class PhysicalColumn : Column, IEquatable<PhysicalColumn>
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000BB0C File Offset: 0x00009D0C
		protected PhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, bool schemaExtension, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength) : base(name, type, nullable, schemaExtension, visibility, maxLength, size, table)
		{
			this.physicalName = physicalName;
			this.index = index;
			this.identity = identity;
			this.streamSupport = streamSupport;
			this.notFetchedByDefault = notFetchedByDefault;
			this.maxInlineLength = maxInlineLength;
			base.CacheHashCode();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000BB6C File Offset: 0x00009D6C
		protected PhysicalColumn(string name, string physicalName, Type type, bool nullable, bool identity, bool streamSupport, bool notFetchedByDefault, Visibility visibility, int maxLength, int size, Table table, int index, int maxInlineLength) : this(name, physicalName, type, nullable, identity, streamSupport, notFetchedByDefault, false, visibility, maxLength, size, table, index, maxInlineLength)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000BB97 File Offset: 0x00009D97
		public bool IsIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000BB9F File Offset: 0x00009D9F
		public bool StreamSupport
		{
			get
			{
				return this.streamSupport;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		public bool NotFetchedByDefault
		{
			get
			{
				return this.notFetchedByDefault;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000BBAF File Offset: 0x00009DAF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000BBB7 File Offset: 0x00009DB7
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public int MaxInlineLength
		{
			get
			{
				return this.maxInlineLength;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public string PhysicalName
		{
			get
			{
				return this.physicalName ?? this.Name;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public override void AppendToString(StringBuilder sb, StringFormatOptions formatOptions)
		{
			if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails && base.Table != null)
			{
				sb.Append(base.Table.Name);
				sb.Append(".");
			}
			sb.Append(this.Name);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000BC28 File Offset: 0x00009E28
		public bool Equals(PhysicalColumn other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000BC36 File Offset: 0x00009E36
		protected internal override bool ActualColumnEquals(Column other)
		{
			return this.Equals(other as PhysicalColumn);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000BC44 File Offset: 0x00009E44
		protected override int GetSize(ITWIR context)
		{
			return context.GetPhysicalColumnSize(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000BC4D File Offset: 0x00009E4D
		protected override object GetValue(ITWIR context)
		{
			return context.GetPhysicalColumnValue(this);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000BC56 File Offset: 0x00009E56
		public override void GetNameOrIdForSerialization(out string columnName, out uint columnId)
		{
			columnName = this.Name;
			columnId = 0U;
		}

		// Token: 0x04000060 RID: 96
		private readonly bool identity;

		// Token: 0x04000061 RID: 97
		private readonly bool streamSupport;

		// Token: 0x04000062 RID: 98
		private readonly bool notFetchedByDefault;

		// Token: 0x04000063 RID: 99
		private readonly int maxInlineLength;

		// Token: 0x04000064 RID: 100
		private readonly string physicalName;

		// Token: 0x04000065 RID: 101
		private int index = -1;
	}
}
