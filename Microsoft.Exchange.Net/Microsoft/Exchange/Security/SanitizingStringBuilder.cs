using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A46 RID: 2630
	public sealed class SanitizingStringBuilder<SanitizingPolicy> where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x06003971 RID: 14705 RVA: 0x00091BBE File Offset: 0x0008FDBE
		public SanitizingStringBuilder()
		{
			this.builder = new StringBuilder();
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00091BD1 File Offset: 0x0008FDD1
		public SanitizingStringBuilder(int capacity)
		{
			this.builder = new StringBuilder(capacity);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00091BE5 File Offset: 0x0008FDE5
		public SanitizingStringBuilder(string content) : this((content == null) ? 0 : content.Length)
		{
			if (content == null)
			{
				content = string.Empty;
			}
			this.Append(content);
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x00091C0A File Offset: 0x0008FE0A
		// (set) Token: 0x06003975 RID: 14709 RVA: 0x00091C17 File Offset: 0x0008FE17
		public int Capacity
		{
			get
			{
				return this.builder.Capacity;
			}
			set
			{
				this.builder.Capacity = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x00091C25 File Offset: 0x0008FE25
		public int MaxCapacity
		{
			get
			{
				return this.builder.MaxCapacity;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x00091C32 File Offset: 0x0008FE32
		public int Length
		{
			get
			{
				return this.builder.Length;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x00091C3F File Offset: 0x0008FE3F
		public StringBuilder UnsafeInnerStringBuilder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x17000E6D RID: 3693
		public char this[int index]
		{
			get
			{
				return this.builder[index];
			}
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x00091C55 File Offset: 0x0008FE55
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			this.builder.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x00091C67 File Offset: 0x0008FE67
		public int EnsureCapacity(int capacity)
		{
			return this.builder.EnsureCapacity(capacity);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00091C75 File Offset: 0x0008FE75
		public void Clear()
		{
			this.builder.Length = 0;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00091C83 File Offset: 0x0008FE83
		public void Append(string str)
		{
			if (str == null)
			{
				str = string.Empty;
			}
			if (StringSanitizer<SanitizingPolicy>.IsTrustedString(str))
			{
				this.builder.Append(str);
				return;
			}
			this.builder.Append(StringSanitizer<SanitizingPolicy>.Sanitize(str));
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00091CB8 File Offset: 0x0008FEB8
		public void Append<T>(T obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj is ISanitizedString<SanitizingPolicy>)
			{
				this.builder.Append(obj.ToString());
				return;
			}
			this.Append(obj.ToString());
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x00091D12 File Offset: 0x0008FF12
		public void AppendLine(string str)
		{
			if (str == null)
			{
				str = string.Empty;
			}
			this.Append(str);
			this.builder.AppendLine();
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00091D34 File Offset: 0x0008FF34
		public void AppendLine<T>(T obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj is ISanitizedString<SanitizingPolicy>)
			{
				this.builder.AppendLine(obj.ToString());
				return;
			}
			this.AppendLine(obj.ToString());
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00091D8E File Offset: 0x0008FF8E
		public void AppendLine()
		{
			this.builder.AppendLine();
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00091D9C File Offset: 0x0008FF9C
		public void AppendFormat(string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			this.builder.Append(StringSanitizer<SanitizingPolicy>.SanitizeFormat(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00091DC4 File Offset: 0x0008FFC4
		public void AppendFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			this.builder.Append(StringSanitizer<SanitizingPolicy>.SanitizeFormat(formatProvider, format, args));
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x00091DE8 File Offset: 0x0008FFE8
		public override string ToString()
		{
			return this.builder.ToString();
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00091DF8 File Offset: 0x0008FFF8
		public T ToSanitizedString<T>() where T : ISanitizedString<SanitizingPolicy>, new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			result.UntrustedValue = this.builder.ToString();
			result.DecreeToBeTrusted();
			return result;
		}

		// Token: 0x040030E6 RID: 12518
		private StringBuilder builder;
	}
}
