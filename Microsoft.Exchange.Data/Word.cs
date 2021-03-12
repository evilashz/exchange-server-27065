using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public struct Word : IComparable, ISerializable
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x0001BA92 File Offset: 0x00019C92
		public Word(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.Word, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001BAC5 File Offset: 0x00019CC5
		public static Word Parse(string s)
		{
			return new Word(s);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		private Word(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.Word, this.value.ToString());
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001BB26 File Offset: 0x00019D26
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001BB39 File Offset: 0x00019D39
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 128 && Word.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001BB5C File Offset: 0x00019D5C
		public static Word Empty
		{
			get
			{
				return default(Word);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001BB72 File Offset: 0x00019D72
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

		// Token: 0x06000875 RID: 2165 RVA: 0x0001BB9E File Offset: 0x00019D9E
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001BBB9 File Offset: 0x00019DB9
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001BBD9 File Offset: 0x00019DD9
		public override bool Equals(object obj)
		{
			return obj is Word && this.Equals((Word)obj);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001BBF1 File Offset: 0x00019DF1
		public bool Equals(Word obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001BC05 File Offset: 0x00019E05
		public static bool operator ==(Word a, Word b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001BC1A File Offset: 0x00019E1A
		public static bool operator !=(Word a, Word b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001BC30 File Offset: 0x00019E30
		public int CompareTo(object obj)
		{
			if (!(obj is Word))
			{
				throw new ArgumentException("Parameter is not of type Word.");
			}
			return string.Compare(this.value, ((Word)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005A4 RID: 1444
		public const int MaxLength = 128;

		// Token: 0x040005A5 RID: 1445
		public const string AllowedCharacters = ".";

		// Token: 0x040005A6 RID: 1446
		public static readonly Regex ValidatingExpression = new Regex("^.+$", RegexOptions.Compiled);

		// Token: 0x040005A7 RID: 1447
		private string value;
	}
}
