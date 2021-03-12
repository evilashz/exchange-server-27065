using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LazilyInitialized<T> : IEquatable<T>
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003334 File Offset: 0x00001534
		public LazilyInitialized(Func<T> initializer)
		{
			if (initializer == null)
			{
				throw new ArgumentNullException("initializer");
			}
			this.initializer = initializer;
			this.value = default(T);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000335D File Offset: 0x0000155D
		internal LazilyInitialized() : this(new Func<T>(LazilyInitialized<T>.InitializerNotSupported))
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003374 File Offset: 0x00001574
		public T Value
		{
			get
			{
				Func<T> func = this.initializer;
				if (func != null)
				{
					this.value = func();
					this.initializer = null;
				}
				return this.value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000033A4 File Offset: 0x000015A4
		internal bool IsInitialized
		{
			get
			{
				return this.initializer == null;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000033AF File Offset: 0x000015AF
		public static implicit operator T(LazilyInitialized<T> delayInitialized)
		{
			return delayInitialized.Value;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000033B7 File Offset: 0x000015B7
		public static bool operator ==(LazilyInitialized<T> op1, T op2)
		{
			return op1 != null && op1.Equals(op2);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033C5 File Offset: 0x000015C5
		public static bool operator !=(LazilyInitialized<T> op1, T op2)
		{
			return !(op1 == op2);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000033D4 File Offset: 0x000015D4
		public override bool Equals(object obj)
		{
			LazilyInitialized<T> lazilyInitialized = obj as LazilyInitialized<T>;
			if (lazilyInitialized == null || !this.Equals(lazilyInitialized.Value))
			{
				T t = this.Value;
				return t.Equals(obj);
			}
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003410 File Offset: 0x00001610
		public bool Equals(T v)
		{
			return EqualityComparer<T>.Default.Equals(this.Value, v);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003424 File Offset: 0x00001624
		public override int GetHashCode()
		{
			T t = this.Value;
			return t.GetHashCode();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003445 File Offset: 0x00001645
		public override string ToString()
		{
			if (this.initializer == null)
			{
				return this.value.ToString();
			}
			return "Uninitialized";
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003466 File Offset: 0x00001666
		internal void Set(T value)
		{
			if (this.IsInitialized)
			{
				throw new InvalidOperationException();
			}
			this.initializer = null;
			this.value = value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003484 File Offset: 0x00001684
		private static T InitializerNotSupported()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400002C RID: 44
		private Func<T> initializer;

		// Token: 0x0400002D RID: 45
		private T value;
	}
}
