using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000010 RID: 16
	internal class EmptyCount<TEntityType, TCountType> : ICount<TEntityType, TCountType>, IEquatable<EmptyCount<TEntityType, TCountType>> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004F81 File Offset: 0x00003181
		public EmptyCount(ICountedEntity<TEntityType> entity, TCountType measure)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			this.Entity = entity;
			this.Measure = measure;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004FA2 File Offset: 0x000031A2
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004FAA File Offset: 0x000031AA
		public ICountedEntity<TEntityType> Entity { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004FB3 File Offset: 0x000031B3
		public ICountedConfig Config
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004FB6 File Offset: 0x000031B6
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004FBE File Offset: 0x000031BE
		public TCountType Measure { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004FC7 File Offset: 0x000031C7
		public bool IsPromoted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004FCA File Offset: 0x000031CA
		public long Total
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004FCE File Offset: 0x000031CE
		public long Average
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004FD2 File Offset: 0x000031D2
		public ITrendline Trend
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004FD5 File Offset: 0x000031D5
		public bool TryGetObject(string key, out object value)
		{
			value = null;
			return false;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004FDB File Offset: 0x000031DB
		public void SetObject(string key, object value)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004FE0 File Offset: 0x000031E0
		public bool Equals(EmptyCount<TEntityType, TCountType> other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (this.Entity.Equals(other.Entity))
			{
				TCountType measure = this.Measure;
				return measure.Equals(other.Measure);
			}
			return false;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005031 File Offset: 0x00003231
		public override bool Equals(object obj)
		{
			return obj != null && (object.ReferenceEquals(this, obj) || (obj is EmptyCount<TEntityType, TCountType> && this.Equals((EmptyCount<TEntityType, TCountType>)obj)));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000505C File Offset: 0x0000325C
		public override int GetHashCode()
		{
			int num = this.Entity.GetHashCode() * 397;
			TCountType measure = this.Measure;
			return num ^ measure.GetHashCode();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000508F File Offset: 0x0000328F
		public override string ToString()
		{
			return string.Format("Entity:{0}, Measure:{1}", this.Entity, this.Measure);
		}
	}
}
