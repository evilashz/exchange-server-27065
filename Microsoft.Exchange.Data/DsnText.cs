using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FA RID: 250
	[Serializable]
	public struct DsnText : IComparable, ISerializable
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x0001C230 File Offset: 0x0001A430
		public DsnText(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.DsnText, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001C263 File Offset: 0x0001A463
		public static DsnText Parse(string s)
		{
			return new DsnText(s);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001C26C File Offset: 0x0001A46C
		private DsnText(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.DsnText, this.value.ToString());
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001C2C2 File Offset: 0x0001A4C2
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001C2D5 File Offset: 0x0001A4D5
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 256 && DsnText.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		public static DsnText Empty
		{
			get
			{
				return default(DsnText);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001C30E File Offset: 0x0001A50E
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

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001C33A File Offset: 0x0001A53A
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001C355 File Offset: 0x0001A555
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001C375 File Offset: 0x0001A575
		public override bool Equals(object obj)
		{
			return obj is DsnText && this.Equals((DsnText)obj);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001C38D File Offset: 0x0001A58D
		public bool Equals(DsnText obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001C3A1 File Offset: 0x0001A5A1
		public static bool operator ==(DsnText a, DsnText b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001C3B6 File Offset: 0x0001A5B6
		public static bool operator !=(DsnText a, DsnText b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		public int CompareTo(object obj)
		{
			if (!(obj is DsnText))
			{
				throw new ArgumentException("Parameter is not of type DsnText.");
			}
			return string.Compare(this.value, ((DsnText)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005B4 RID: 1460
		public const int MaxLength = 256;

		// Token: 0x040005B5 RID: 1461
		public const string AllowedCharacters = ".";

		// Token: 0x040005B6 RID: 1462
		public static readonly Regex ValidatingExpression = new Regex("^.+$", RegexOptions.Compiled);

		// Token: 0x040005B7 RID: 1463
		private string value;
	}
}
