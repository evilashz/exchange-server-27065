using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public struct SclValue : IComparable, ISerializable
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		public SclValue(int input)
		{
			this.value = int.MinValue;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.SclValue, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
		private SclValue(SerializationInfo info, StreamingContext context)
		{
			this.value = (int)info.GetValue("value", typeof(int));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.SclValue, this.value.ToString());
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001CC3E File Offset: 0x0001AE3E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001CC51 File Offset: 0x0001AE51
		private bool IsValid(int input)
		{
			return -1 <= input && input <= 9;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001CC61 File Offset: 0x0001AE61
		public static SclValue Parse(string s)
		{
			return new SclValue(int.Parse(s));
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		public int Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					return this.value;
				}
				throw new ArgumentOutOfRangeException("Value", this.value.ToString());
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001CC9A File Offset: 0x0001AE9A
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001CCA7 File Offset: 0x0001AEA7
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		public override bool Equals(object obj)
		{
			return obj is SclValue && this.Equals((SclValue)obj);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001CCCC File Offset: 0x0001AECC
		public bool Equals(SclValue obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001CCDD File Offset: 0x0001AEDD
		public static bool operator ==(SclValue a, SclValue b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001CCEF File Offset: 0x0001AEEF
		public static bool operator !=(SclValue a, SclValue b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001CD04 File Offset: 0x0001AF04
		public int CompareTo(object obj)
		{
			if (!(obj is SclValue))
			{
				throw new ArgumentException("Parameter is not of type SclValue.");
			}
			return this.value.CompareTo(((SclValue)obj).Value);
		}

		// Token: 0x040005C8 RID: 1480
		public const int MinValue = -1;

		// Token: 0x040005C9 RID: 1481
		public const int MaxValue = 9;

		// Token: 0x040005CA RID: 1482
		private int value;
	}
}
