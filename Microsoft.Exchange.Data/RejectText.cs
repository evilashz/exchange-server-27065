using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public struct RejectText : IComparable, ISerializable
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0001C048 File Offset: 0x0001A248
		public RejectText(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.RejectText, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001C07B File Offset: 0x0001A27B
		public static RejectText Parse(string s)
		{
			return new RejectText(s);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001C084 File Offset: 0x0001A284
		private RejectText(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.RejectText, this.value.ToString());
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001C0DA File Offset: 0x0001A2DA
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001C0ED File Offset: 0x0001A2ED
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 128 && RejectText.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001C110 File Offset: 0x0001A310
		public static RejectText Empty
		{
			get
			{
				return default(RejectText);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001C126 File Offset: 0x0001A326
		public string Value
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

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001C152 File Offset: 0x0001A352
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001C16D File Offset: 0x0001A36D
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001C18D File Offset: 0x0001A38D
		public override bool Equals(object obj)
		{
			return obj is RejectText && this.Equals((RejectText)obj);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public bool Equals(RejectText obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001C1B9 File Offset: 0x0001A3B9
		public static bool operator ==(RejectText a, RejectText b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001C1CE File Offset: 0x0001A3CE
		public static bool operator !=(RejectText a, RejectText b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		public int CompareTo(object obj)
		{
			if (!(obj is RejectText))
			{
				throw new ArgumentException("Parameter is not of type RejectText.");
			}
			return string.Compare(this.value, ((RejectText)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005B0 RID: 1456
		public const int MaxLength = 128;

		// Token: 0x040005B1 RID: 1457
		public const string AllowedCharacters = "[\\x20-\\x7e]";

		// Token: 0x040005B2 RID: 1458
		public static readonly Regex ValidatingExpression = new Regex("^[\\x20-\\x7e]+$", RegexOptions.Compiled);

		// Token: 0x040005B3 RID: 1459
		private string value;
	}
}
