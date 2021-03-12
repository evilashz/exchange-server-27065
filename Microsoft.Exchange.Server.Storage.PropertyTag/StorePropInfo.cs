using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000006 RID: 6
	public class StorePropInfo
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000307D File Offset: 0x0000127D
		public StorePropInfo(string descriptiveName, ushort propId, PropertyType propType, StorePropInfo.Flags flags, ulong groupMask, PropertyCategories categories)
		{
			this.descriptiveName = descriptiveName;
			this.propId = propId;
			this.propType = propType;
			this.groupMask = groupMask;
			this.categories = categories;
			this.visibility = StorePropInfo.GetVisibility(flags);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000030B7 File Offset: 0x000012B7
		public string DescriptiveName
		{
			get
			{
				return this.descriptiveName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000030BF File Offset: 0x000012BF
		public virtual ushort PropId
		{
			get
			{
				return this.propId;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000030C7 File Offset: 0x000012C7
		public PropertyType PropType
		{
			get
			{
				return this.propType;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000030CF File Offset: 0x000012CF
		public ulong GroupMask
		{
			get
			{
				return this.groupMask;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000030D7 File Offset: 0x000012D7
		public bool IsNamedProperty
		{
			get
			{
				return this.propId >= 32768;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000030E9 File Offset: 0x000012E9
		public virtual StorePropName PropName
		{
			get
			{
				if (this.IsNamedProperty)
				{
					return StorePropName.Invalid;
				}
				return new StorePropName(StorePropName.UnnamedPropertyNamespaceGuid, (uint)this.propId);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00003109 File Offset: 0x00001309
		public Visibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003114 File Offset: 0x00001314
		public bool IsCategory(int category)
		{
			return this.categories.CheckCategory(category);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003130 File Offset: 0x00001330
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003152 File Offset: 0x00001352
		public virtual void AppendToString(StringBuilder sb)
		{
			if (this.descriptiveName != null)
			{
				sb.Append(this.descriptiveName);
				return;
			}
			sb.Append("generic");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003176 File Offset: 0x00001376
		private static Visibility GetVisibility(StorePropInfo.Flags flags)
		{
			if ((byte)(flags & StorePropInfo.Flags.Private) == 1)
			{
				return Visibility.Private;
			}
			if ((byte)(flags & StorePropInfo.Flags.Redacted) == 2)
			{
				return Visibility.Redacted;
			}
			return Visibility.Public;
		}

		// Token: 0x0400004F RID: 79
		public const ulong OtherGroupMask = 9223372036854775808UL;

		// Token: 0x04000050 RID: 80
		private readonly string descriptiveName;

		// Token: 0x04000051 RID: 81
		private readonly ushort propId;

		// Token: 0x04000052 RID: 82
		private readonly PropertyType propType;

		// Token: 0x04000053 RID: 83
		private readonly ulong groupMask;

		// Token: 0x04000054 RID: 84
		private readonly PropertyCategories categories;

		// Token: 0x04000055 RID: 85
		private readonly Visibility visibility;

		// Token: 0x02000007 RID: 7
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04000057 RID: 87
			None = 0,
			// Token: 0x04000058 RID: 88
			Private = 1,
			// Token: 0x04000059 RID: 89
			Redacted = 2
		}
	}
}
