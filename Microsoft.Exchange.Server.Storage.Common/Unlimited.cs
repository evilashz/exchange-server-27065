using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C0 RID: 192
	public class Unlimited<T> : IComparable, IComparable<Unlimited<T>>, IEquatable<Unlimited<T>> where T : struct, IComparable<T>, IEquatable<T>
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x0001A4C6 File Offset: 0x000186C6
		public Unlimited()
		{
			this.value = null;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001A4DA File Offset: 0x000186DA
		public Unlimited(T value)
		{
			this.value = new T?(value);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001A4F0 File Offset: 0x000186F0
		public bool IsUnlimited
		{
			get
			{
				return this.value == null;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001A510 File Offset: 0x00018710
		public T Value
		{
			get
			{
				return this.value.Value;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001A52B File Offset: 0x0001872B
		public static bool operator !=(Unlimited<T> left, Unlimited<T> right)
		{
			return !(left == right);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001A537 File Offset: 0x00018737
		public static bool operator ==(Unlimited<T> left, Unlimited<T> right)
		{
			if (object.ReferenceEquals(left, null))
			{
				return object.ReferenceEquals(right, null);
			}
			return left.Equals(right);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001A551 File Offset: 0x00018751
		public static bool operator <(Unlimited<T> left, Unlimited<T> right)
		{
			return left.CompareTo(right) == -1;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001A55D File Offset: 0x0001875D
		public static bool operator >(Unlimited<T> left, Unlimited<T> right)
		{
			return left.CompareTo(right) == 1;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001A569 File Offset: 0x00018769
		public static bool operator <=(Unlimited<T> left, Unlimited<T> right)
		{
			return left.CompareTo(right) != 1;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001A578 File Offset: 0x00018778
		public static bool operator >=(Unlimited<T> left, Unlimited<T> right)
		{
			return left.CompareTo(right) != -1;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001A587 File Offset: 0x00018787
		public override bool Equals(object other)
		{
			return other is Unlimited<T> && this.Equals((Unlimited<T>)other);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001A59F File Offset: 0x0001879F
		public bool Equals(Unlimited<T> other)
		{
			return !object.ReferenceEquals(other, null) && this.CompareTo(other) == 0;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001A5B6 File Offset: 0x000187B6
		public int CompareTo(object other)
		{
			if (object.ReferenceEquals(other, null))
			{
				throw new ArgumentException();
			}
			if (!(other is Unlimited<T>))
			{
				throw new ArgumentException();
			}
			return this.CompareTo((Unlimited<T>)other);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001A5E4 File Offset: 0x000187E4
		public int CompareTo(Unlimited<T> other)
		{
			if (object.ReferenceEquals(other, null))
			{
				throw new ArgumentException();
			}
			if (this.IsUnlimited)
			{
				if (other.IsUnlimited)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (other.IsUnlimited)
				{
					return -1;
				}
				T t = this.value.Value;
				return t.CompareTo(other.value.Value);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001A648 File Offset: 0x00018848
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001A66C File Offset: 0x0001886C
		public override string ToString()
		{
			if (this.IsUnlimited)
			{
				return "<Unlimited>";
			}
			T t = this.Value;
			return t.ToString();
		}

		// Token: 0x0400073B RID: 1851
		private readonly T? value;

		// Token: 0x0400073C RID: 1852
		public static Unlimited<T> UnlimitedValue = new Unlimited<T>();
	}
}
