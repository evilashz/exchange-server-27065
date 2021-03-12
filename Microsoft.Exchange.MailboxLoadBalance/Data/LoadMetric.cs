using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	internal class LoadMetric
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000041DB File Offset: 0x000023DB
		public LoadMetric(string name, bool sizeMetric)
		{
			this.name = name;
			this.isSizeMetric = sizeMetric;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000041F1 File Offset: 0x000023F1
		public LocalizedString FriendlyName
		{
			get
			{
				return new LocalizedString(this.Name);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000041FE File Offset: 0x000023FE
		public virtual bool IsSize
		{
			get
			{
				return this.isSizeMetric;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004206 File Offset: 0x00002406
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000420E File Offset: 0x0000240E
		public static bool operator ==(LoadMetric left, LoadMetric right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004217 File Offset: 0x00002417
		public static bool operator !=(LoadMetric left, LoadMetric right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004224 File Offset: 0x00002424
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			LoadMetric left = obj as LoadMetric;
			return !(left == null) && this.Equals((LoadMetric)obj);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004265 File Offset: 0x00002465
		public override int GetHashCode()
		{
			if (this.name == null)
			{
				return 0;
			}
			return this.name.GetHashCode();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000427C File Offset: 0x0000247C
		public virtual EntitySelector GetSelector(LoadContainer container, string constraintSetIdentity, long units)
		{
			return new NullEntitySelector();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004283 File Offset: 0x00002483
		public virtual long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return 0L;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004287 File Offset: 0x00002487
		public virtual long GetUnitsForSize(ByteQuantifiedSize size)
		{
			if (!this.IsSize)
			{
				throw new InvalidOperationException("Cannot get units for size on a non-size metric.");
			}
			return (long)size.ToBytes();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000042A4 File Offset: 0x000024A4
		public virtual string GetValueString(long value)
		{
			string arg = this.IsSize ? this.ToByteQuantifiedSize(value).ToString() : value.ToString(CultureInfo.InvariantCulture);
			return string.Format("{0}={1}", this.Name, arg);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000042EE File Offset: 0x000024EE
		public virtual ByteQuantifiedSize ToByteQuantifiedSize(long value)
		{
			if (!this.isSizeMetric)
			{
				throw new InvalidOperationException("Cannot convert a non-size based metric to ByteQuantifiedSize.");
			}
			return this.CreateByteQuantifiedSizeFromValue((ulong)value);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000430A File Offset: 0x0000250A
		public override string ToString()
		{
			return string.Format("LoadMetric::{0}", this.name);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000431C File Offset: 0x0000251C
		protected virtual ByteQuantifiedSize CreateByteQuantifiedSizeFromValue(ulong value)
		{
			return ByteQuantifiedSize.FromBytes(value);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004324 File Offset: 0x00002524
		protected bool Equals(LoadMetric other)
		{
			return string.Equals(this.name, other.name);
		}

		// Token: 0x04000031 RID: 49
		[DataMember]
		private readonly bool isSizeMetric;

		// Token: 0x04000032 RID: 50
		[DataMember]
		private readonly string name;
	}
}
