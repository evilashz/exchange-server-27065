using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public struct ByteQuantifiedSize : IComparable, IComparable<ByteQuantifiedSize>, IFormattable
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public ByteQuantifiedSize(ulong byteValue)
		{
			this.bytes = byteValue;
			this.unit = ByteQuantifiedSize.Quantifier.None;
			this.canonicalForm = null;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C000 File Offset: 0x0000A200
		private ByteQuantifiedSize(ulong byteValue, ByteQuantifiedSize.Quantifier desiredUnitToDisplay)
		{
			this.bytes = byteValue;
			this.unit = desiredUnitToDisplay;
			this.canonicalForm = null;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C017 File Offset: 0x0000A217
		public static ByteQuantifiedSize FromBytes(ulong bytesValue)
		{
			return new ByteQuantifiedSize(bytesValue, ByteQuantifiedSize.Quantifier.None);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C024 File Offset: 0x0000A224
		public static ByteQuantifiedSize FromKB(ulong kbValue)
		{
			if (18014398509481983UL < kbValue)
			{
				throw new OverflowException(DataStrings.ExceptionValueOverflow(ByteQuantifiedSize.MinValue.ToString("K"), ByteQuantifiedSize.MaxValue.ToString("K"), kbValue.ToString()));
			}
			ulong byteValue = kbValue << 10;
			return new ByteQuantifiedSize(byteValue, ByteQuantifiedSize.Quantifier.KB);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C08C File Offset: 0x0000A28C
		public static ByteQuantifiedSize FromMB(ulong mbValue)
		{
			if (17592186044415UL < mbValue)
			{
				throw new OverflowException(DataStrings.ExceptionValueOverflow(ByteQuantifiedSize.MinValue.ToString("M"), ByteQuantifiedSize.MaxValue.ToString("M"), mbValue.ToString()));
			}
			ulong byteValue = mbValue << 20;
			return new ByteQuantifiedSize(byteValue, ByteQuantifiedSize.Quantifier.MB);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public static ByteQuantifiedSize FromGB(ulong gbValue)
		{
			if (17179869183UL < gbValue)
			{
				throw new OverflowException(DataStrings.ExceptionValueOverflow(ByteQuantifiedSize.MinValue.ToString("G"), ByteQuantifiedSize.MaxValue.ToString("G"), gbValue.ToString()));
			}
			ulong byteValue = gbValue << 30;
			return new ByteQuantifiedSize(byteValue, ByteQuantifiedSize.Quantifier.GB);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C15C File Offset: 0x0000A35C
		public static ByteQuantifiedSize FromTB(ulong tbValue)
		{
			if (16777215UL < tbValue)
			{
				throw new OverflowException(DataStrings.ExceptionValueOverflow(ByteQuantifiedSize.MinValue.ToString("T"), ByteQuantifiedSize.MaxValue.ToString("T"), tbValue.ToString()));
			}
			ulong byteValue = tbValue << 40;
			return new ByteQuantifiedSize(byteValue, ByteQuantifiedSize.Quantifier.TB);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		public ulong ToBytes()
		{
			return this.bytes;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		public ulong ToKB()
		{
			return this.bytes >> 10;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public ulong ToMB()
		{
			return this.bytes >> 20;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C1DF File Offset: 0x0000A3DF
		public ulong ToGB()
		{
			return this.bytes >> 30;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C1EA File Offset: 0x0000A3EA
		public ulong ToTB()
		{
			return this.bytes >> 40;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		private string ToLargestAppropriateUnitFormatString(bool includeFullBytes = true)
		{
			if (includeFullBytes)
			{
				if (this.ToTB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1} ({2:N0} bytes)", new object[]
					{
						this.ToBytes() / 1099511627776.0,
						"TB",
						this.ToBytes()
					});
				}
				if (this.ToGB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1} ({2:N0} bytes)", new object[]
					{
						this.ToBytes() / 1073741824.0,
						"GB",
						this.ToBytes()
					});
				}
				if (this.ToMB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1} ({2:N0} bytes)", new object[]
					{
						this.ToBytes() / 1048576.0,
						"MB",
						this.ToBytes()
					});
				}
				if (this.ToKB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1} ({2:N0} bytes)", new object[]
					{
						this.ToBytes() / 1024.0,
						"KB",
						this.ToBytes()
					});
				}
				return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1} ({2:N0} bytes)", new object[]
				{
					this.ToBytes(),
					"B",
					this.ToBytes()
				});
			}
			else
			{
				if (this.ToTB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1}", new object[]
					{
						this.ToBytes() / 1099511627776.0,
						"TB"
					});
				}
				if (this.ToGB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1}", new object[]
					{
						this.ToBytes() / 1073741824.0,
						"GB"
					});
				}
				if (this.ToMB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1}", new object[]
					{
						this.ToBytes() / 1048576.0,
						"MB"
					});
				}
				if (this.ToKB() > 0UL)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1}", new object[]
					{
						this.ToBytes() / 1024.0,
						"KB"
					});
				}
				return string.Format(CultureInfo.InvariantCulture, "{0:G4} {1}", new object[]
				{
					this.ToBytes(),
					"B"
				});
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public ulong RoundUpToUnit(ByteQuantifiedSize.Quantifier quantifier)
		{
			if (quantifier == (ByteQuantifiedSize.Quantifier)0UL || (quantifier & quantifier - 1UL) != (ByteQuantifiedSize.Quantifier)0UL)
			{
				throw new ArgumentException("Invalid quantifier value", "quantifier");
			}
			ulong num = this.bytes / (ulong)quantifier;
			if ((this.bytes & quantifier - ByteQuantifiedSize.Quantifier.None) != 0UL)
			{
				num += 1UL;
			}
			return num;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C534 File Offset: 0x0000A734
		public override string ToString()
		{
			if (this.canonicalForm == null)
			{
				this.canonicalForm = this.ToString("A");
			}
			return this.canonicalForm;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C558 File Offset: 0x0000A758
		public string ToString(string format)
		{
			if (!string.IsNullOrEmpty(format) && format.Length == 1)
			{
				char c = format[0];
				if (c <= 'G')
				{
					switch (c)
					{
					case 'A':
						return this.ToLargestAppropriateUnitFormatString(true);
					case 'B':
						return this.ToString(ByteQuantifiedSize.Quantifier.None);
					default:
						if (c == 'G')
						{
							return this.ToString(ByteQuantifiedSize.Quantifier.GB);
						}
						break;
					}
				}
				else
				{
					switch (c)
					{
					case 'K':
						return this.ToString(ByteQuantifiedSize.Quantifier.KB);
					case 'L':
						break;
					case 'M':
						return this.ToString(ByteQuantifiedSize.Quantifier.MB);
					default:
						if (c == 'T')
						{
							return this.ToString(ByteQuantifiedSize.Quantifier.TB);
						}
						if (c == 'a')
						{
							return this.ToLargestAppropriateUnitFormatString(false);
						}
						break;
					}
				}
				throw new FormatException(DataStrings.ExceptionFormatNotSupported);
			}
			throw new FormatException(DataStrings.ExceptionFormatNotSupported);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C641 File Offset: 0x0000A841
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				return this.ToString();
			}
			return this.ToString(format);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C65F File Offset: 0x0000A85F
		public override bool Equals(object obj)
		{
			return obj != null && obj is ByteQuantifiedSize && this.Equals((ByteQuantifiedSize)obj);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C67A File Offset: 0x0000A87A
		public bool Equals(ByteQuantifiedSize other)
		{
			return other.bytes == this.bytes;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C68B File Offset: 0x0000A88B
		public override int GetHashCode()
		{
			return this.bytes.GetHashCode();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C698 File Offset: 0x0000A898
		public static bool operator <(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes < value2.bytes;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		public static bool operator <=(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes <= value2.bytes;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C6BF File Offset: 0x0000A8BF
		public static bool operator >(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes > value2.bytes;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		public static bool operator >=(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes >= value2.bytes;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
		public static bool operator ==(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes == value2.bytes;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public static bool operator !=(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return value1.bytes != value2.bytes;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C70D File Offset: 0x0000A90D
		public static ByteQuantifiedSize operator *(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes * value2.bytes));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C723 File Offset: 0x0000A923
		public static ByteQuantifiedSize operator *(ByteQuantifiedSize value1, ulong value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes * value2));
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C733 File Offset: 0x0000A933
		public static ByteQuantifiedSize operator *(ByteQuantifiedSize value1, int value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes * (ulong)value2));
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C744 File Offset: 0x0000A944
		public static ByteQuantifiedSize operator /(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return new ByteQuantifiedSize(value1.bytes / value2.bytes);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C75A File Offset: 0x0000A95A
		public static ByteQuantifiedSize operator /(ByteQuantifiedSize value1, ulong value2)
		{
			return new ByteQuantifiedSize(value1.bytes / value2);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C76A File Offset: 0x0000A96A
		public static ByteQuantifiedSize operator /(ByteQuantifiedSize value1, int value2)
		{
			return new ByteQuantifiedSize(value1.bytes / (ulong)((long)value2));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C77B File Offset: 0x0000A97B
		public static ByteQuantifiedSize operator +(ByteQuantifiedSize value1, ulong value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes + value2));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C78B File Offset: 0x0000A98B
		public static ByteQuantifiedSize operator +(ByteQuantifiedSize value1, int value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes + (ulong)value2));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C79C File Offset: 0x0000A99C
		public static ByteQuantifiedSize operator +(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes + value2.bytes));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C7B2 File Offset: 0x0000A9B2
		public static ByteQuantifiedSize operator -(ByteQuantifiedSize value1, ByteQuantifiedSize value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes - value2.bytes));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public static ByteQuantifiedSize operator -(ByteQuantifiedSize value1, ulong value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes - value2));
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public static ByteQuantifiedSize operator -(ByteQuantifiedSize value1, int value2)
		{
			return new ByteQuantifiedSize(checked(value1.bytes - (ulong)value2));
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C7E9 File Offset: 0x0000A9E9
		public static explicit operator ulong(ByteQuantifiedSize size)
		{
			return size.bytes;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C7F2 File Offset: 0x0000A9F2
		public static explicit operator double(ByteQuantifiedSize size)
		{
			return size.bytes;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C800 File Offset: 0x0000AA00
		public static ByteQuantifiedSize Parse(string expression, ByteQuantifiedSize.Quantifier defaultUnit)
		{
			ulong number;
			ByteQuantifiedSize result;
			if (ulong.TryParse(expression, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out number))
			{
				result = ByteQuantifiedSize.FromSpecifiedUnit(number, defaultUnit);
			}
			else
			{
				result = ByteQuantifiedSize.Parse(expression);
			}
			return result;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C830 File Offset: 0x0000AA30
		public static bool TryParse(string expression, ByteQuantifiedSize.Quantifier defaultUnit, out ByteQuantifiedSize byteQuantifiedSize)
		{
			bool result = true;
			ulong number;
			if (ulong.TryParse(expression, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out number))
			{
				try
				{
					byteQuantifiedSize = ByteQuantifiedSize.FromSpecifiedUnit(number, defaultUnit);
					return result;
				}
				catch (ArgumentException)
				{
					byteQuantifiedSize = default(ByteQuantifiedSize);
					return false;
				}
			}
			result = ByteQuantifiedSize.TryParse(expression, out byteQuantifiedSize);
			return result;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C884 File Offset: 0x0000AA84
		public static ByteQuantifiedSize Parse(string expression)
		{
			ByteQuantifiedSize result = default(ByteQuantifiedSize);
			ulong num = 0UL;
			ByteQuantifiedSize.Quantifier quantifier;
			Exception ex;
			if (ByteQuantifiedSize.InternalTryParse(expression, out num, out quantifier, out ex))
			{
				result.bytes = num;
				result.unit = quantifier;
				result.canonicalForm = result.ToString("A");
				return result;
			}
			throw ex;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public static bool TryParse(string expression, out ByteQuantifiedSize byteQuantifiedSize)
		{
			byteQuantifiedSize = default(ByteQuantifiedSize);
			byteQuantifiedSize.bytes = 0UL;
			byteQuantifiedSize.unit = ByteQuantifiedSize.Quantifier.None;
			ulong num = 0UL;
			ByteQuantifiedSize.Quantifier quantifier;
			Exception ex;
			if (ByteQuantifiedSize.InternalTryParse(expression, out num, out quantifier, out ex))
			{
				byteQuantifiedSize.bytes = num;
				byteQuantifiedSize.unit = quantifier;
				byteQuantifiedSize.canonicalForm = byteQuantifiedSize.ToString("A");
				return true;
			}
			return false;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C92C File Offset: 0x0000AB2C
		private static bool InternalTryParse(string expression, out ulong bytes, out ByteQuantifiedSize.Quantifier unit, out Exception error)
		{
			bytes = 0UL;
			unit = ByteQuantifiedSize.Quantifier.None;
			error = null;
			expression = expression.Trim();
			Match match = ByteQuantifiedSize.LargestAppropriateUnitFormatPattern.Match(expression);
			if (!match.Success)
			{
				error = new FormatException(DataStrings.ExceptionFormatNotCorrect(expression));
				return false;
			}
			string a;
			ByteQuantifiedSize.MaximumValues maximumValues;
			if ((a = match.Groups["Unit"].Value.ToUpper()) != null)
			{
				if (a == "KB")
				{
					unit = ByteQuantifiedSize.Quantifier.KB;
					maximumValues = ByteQuantifiedSize.MaximumValues.KB;
					goto IL_EC;
				}
				if (a == "MB")
				{
					unit = ByteQuantifiedSize.Quantifier.MB;
					maximumValues = ByteQuantifiedSize.MaximumValues.MB;
					goto IL_EC;
				}
				if (a == "GB")
				{
					unit = ByteQuantifiedSize.Quantifier.GB;
					maximumValues = ByteQuantifiedSize.MaximumValues.GB;
					goto IL_EC;
				}
				if (a == "TB")
				{
					unit = ByteQuantifiedSize.Quantifier.TB;
					maximumValues = ByteQuantifiedSize.MaximumValues.TB;
					goto IL_EC;
				}
			}
			unit = ByteQuantifiedSize.Quantifier.None;
			maximumValues = ByteQuantifiedSize.MaximumValues.None;
			IL_EC:
			if (match.Groups["LongBytes"].Success)
			{
				if (!ulong.TryParse(match.Groups["LongBytes"].Value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out bytes))
				{
					error = new FormatException(DataStrings.ExceptionFormatNotCorrect(expression));
					return false;
				}
			}
			else
			{
				double num;
				if (!double.TryParse(match.Groups["ShortBytes"].Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null, out num))
				{
					error = new FormatException(DataStrings.ExceptionFormatNotCorrect(expression));
					return false;
				}
				if (maximumValues < num || 0.0 > num)
				{
					error = new OverflowException(DataStrings.ExceptionValueOverflow(ByteQuantifiedSize.MinValue.ToString(unit), ByteQuantifiedSize.MaxValue.ToString(unit), expression));
					return false;
				}
				bytes = (ulong)(unit * num);
			}
			return true;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public static ByteQuantifiedSize FromSpecifiedUnit(ulong number, ByteQuantifiedSize.Quantifier specifiedUnit)
		{
			ByteQuantifiedSize byteQuantifiedSize = default(ByteQuantifiedSize);
			if (specifiedUnit <= ByteQuantifiedSize.Quantifier.KB)
			{
				if (specifiedUnit == ByteQuantifiedSize.Quantifier.None)
				{
					return ByteQuantifiedSize.FromBytes(number);
				}
				if (specifiedUnit == ByteQuantifiedSize.Quantifier.KB)
				{
					return ByteQuantifiedSize.FromKB(number);
				}
			}
			else
			{
				if (specifiedUnit == ByteQuantifiedSize.Quantifier.MB)
				{
					return ByteQuantifiedSize.FromMB(number);
				}
				if (specifiedUnit == ByteQuantifiedSize.Quantifier.GB)
				{
					return ByteQuantifiedSize.FromGB(number);
				}
				if (specifiedUnit == ByteQuantifiedSize.Quantifier.TB)
				{
					return ByteQuantifiedSize.FromTB(number);
				}
			}
			throw new ArgumentException(DataStrings.ExceptionUnknownUnit);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CB8E File Offset: 0x0000AD8E
		public int CompareTo(ByteQuantifiedSize other)
		{
			return Comparer<ulong>.Default.Compare(this.bytes, other.bytes);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CBA7 File Offset: 0x0000ADA7
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (other is ByteQuantifiedSize)
			{
				return this.CompareTo((ByteQuantifiedSize)other);
			}
			throw new ArgumentException(DataStrings.ExceptionObjectInvalid);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		private string ToString(ByteQuantifiedSize.Quantifier quantifier)
		{
			if (quantifier <= ByteQuantifiedSize.Quantifier.KB)
			{
				if (quantifier == ByteQuantifiedSize.Quantifier.None)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:N0} B", new object[]
					{
						this.ToBytes()
					});
				}
				if (quantifier == ByteQuantifiedSize.Quantifier.KB)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:N0} KB", new object[]
					{
						this.ToKB()
					});
				}
			}
			else
			{
				if (quantifier == ByteQuantifiedSize.Quantifier.MB)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:N0} MB", new object[]
					{
						this.ToMB()
					});
				}
				if (quantifier == ByteQuantifiedSize.Quantifier.GB)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:N0} GB", new object[]
					{
						this.ToGB()
					});
				}
				if (quantifier == ByteQuantifiedSize.Quantifier.TB)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0:N0} TB", new object[]
					{
						this.ToTB()
					});
				}
			}
			return this.ToLargestAppropriateUnitFormatString(true);
		}

		// Token: 0x04000101 RID: 257
		private static readonly Regex LargestAppropriateUnitFormatPattern = new Regex("^(?<ShortBytes>\\S{0,14}\\d)( ?(?<Unit>[KMGT]?B)( \\((?<LongBytes>\\S+) bytes\\))?)?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000102 RID: 258
		public static readonly ByteQuantifiedSize MinValue = ByteQuantifiedSize.FromBytes(0UL);

		// Token: 0x04000103 RID: 259
		public static readonly ByteQuantifiedSize MaxValue = ByteQuantifiedSize.FromBytes(ulong.MaxValue);

		// Token: 0x04000104 RID: 260
		public static readonly ByteQuantifiedSize Zero = ByteQuantifiedSize.FromBytes(0UL);

		// Token: 0x04000105 RID: 261
		public static readonly IFormatProvider KilobyteQuantifierProvider = new ByteQuantifiedSize.QuantifierProvider(ByteQuantifiedSize.Quantifier.KB);

		// Token: 0x04000106 RID: 262
		public static readonly IFormatProvider MegabyteQuantifierProvider = new ByteQuantifiedSize.QuantifierProvider(ByteQuantifiedSize.Quantifier.MB);

		// Token: 0x04000107 RID: 263
		private ulong bytes;

		// Token: 0x04000108 RID: 264
		private ByteQuantifiedSize.Quantifier unit;

		// Token: 0x04000109 RID: 265
		private string canonicalForm;

		// Token: 0x02000057 RID: 87
		public enum Quantifier : ulong
		{
			// Token: 0x0400010B RID: 267
			None = 1UL,
			// Token: 0x0400010C RID: 268
			KB = 1024UL,
			// Token: 0x0400010D RID: 269
			MB = 1048576UL,
			// Token: 0x0400010E RID: 270
			GB = 1073741824UL,
			// Token: 0x0400010F RID: 271
			TB = 1099511627776UL
		}

		// Token: 0x02000058 RID: 88
		[Serializable]
		private class QuantifierProvider : IFormatProvider
		{
			// Token: 0x060002B4 RID: 692 RVA: 0x0000CD6D File Offset: 0x0000AF6D
			public QuantifierProvider(ByteQuantifiedSize.Quantifier quantifier)
			{
				this.quantifier = quantifier;
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x0000CD7C File Offset: 0x0000AF7C
			public object GetFormat(Type formatType)
			{
				if (formatType != typeof(ByteQuantifiedSize.Quantifier))
				{
					throw new ArgumentException("This kind of format type is not implemented", "formatType");
				}
				return this.quantifier;
			}

			// Token: 0x04000110 RID: 272
			private readonly ByteQuantifiedSize.Quantifier quantifier;
		}

		// Token: 0x02000059 RID: 89
		private enum MaximumValues : ulong
		{
			// Token: 0x04000112 RID: 274
			None = 18446744073709551615UL,
			// Token: 0x04000113 RID: 275
			KB = 18014398509481983UL,
			// Token: 0x04000114 RID: 276
			MB = 17592186044415UL,
			// Token: 0x04000115 RID: 277
			GB = 17179869183UL,
			// Token: 0x04000116 RID: 278
			TB = 16777215UL
		}

		// Token: 0x0200005A RID: 90
		private enum QuantifierZeroBits
		{
			// Token: 0x04000118 RID: 280
			None,
			// Token: 0x04000119 RID: 281
			KB = 10,
			// Token: 0x0400011A RID: 282
			MB = 20,
			// Token: 0x0400011B RID: 283
			GB = 30,
			// Token: 0x0400011C RID: 284
			TB = 40
		}
	}
}
