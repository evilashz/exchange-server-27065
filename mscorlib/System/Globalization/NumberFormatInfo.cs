using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020003AC RID: 940
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		// Token: 0x060030E8 RID: 12520 RVA: 0x000BC1AC File Offset: 0x000BA3AC
		[__DynamicallyInvokable]
		public NumberFormatInfo() : this(null)
		{
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BC1B8 File Offset: 0x000BA3B8
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BC243 File Offset: 0x000BA443
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BC245 File Offset: 0x000BA445
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BC247 File Offset: 0x000BA447
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyDecString"));
			}
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BC275 File Offset: 0x000BA475
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
			}
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000BC28C File Offset: 0x000BA48C
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitCount"), propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_ArrayValue"));
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
				}
			}
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000BC36A File Offset: 0x000BA56A
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDigitSubstitution"), propertyName);
			}
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000BC384 File Offset: 0x000BA584
		[SecuritySafeCritical]
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000BC509 File Offset: 0x000BA709
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000BC524 File Offset: 0x000BA724
		[__DynamicallyInvokable]
		public static NumberFormatInfo InvariantInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000BC55C File Offset: 0x000BA75C
		[__DynamicallyInvokable]
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = (formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo);
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000BC5C0 File Offset: 0x000BA7C0
		[__DynamicallyInvokable]
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000BC5E1 File Offset: 0x000BA7E1
		// (set) Token: 0x060030F6 RID: 12534 RVA: 0x000BC5EC File Offset: 0x000BA7EC
		[__DynamicallyInvokable]
		public int CurrencyDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.currencyDecimalDigits = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000BC63B File Offset: 0x000BA83B
		// (set) Token: 0x060030F8 RID: 12536 RVA: 0x000BC643 File Offset: 0x000BA843
		[__DynamicallyInvokable]
		public string CurrencyDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
				this.currencyDecimalSeparator = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000BC65D File Offset: 0x000BA85D
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000BC668 File Offset: 0x000BA868
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
					}
					i++;
				}
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x000BC6C0 File Offset: 0x000BA8C0
		// (set) Token: 0x060030FC RID: 12540 RVA: 0x000BC6D4 File Offset: 0x000BA8D4
		[__DynamicallyInvokable]
		public int[] CurrencyGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", groupSize);
				this.currencyGroupSizes = groupSize;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000BC71D File Offset: 0x000BA91D
		// (set) Token: 0x060030FE RID: 12542 RVA: 0x000BC730 File Offset: 0x000BA930
		[__DynamicallyInvokable]
		public int[] NumberGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("NumberGroupSizes", groupSize);
				this.numberGroupSizes = groupSize;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000BC779 File Offset: 0x000BA979
		// (set) Token: 0x06003100 RID: 12544 RVA: 0x000BC78C File Offset: 0x000BA98C
		[__DynamicallyInvokable]
		public int[] PercentGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("PercentGroupSizes", groupSize);
				this.percentGroupSizes = groupSize;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000BC7D5 File Offset: 0x000BA9D5
		// (set) Token: 0x06003102 RID: 12546 RVA: 0x000BC7DD File Offset: 0x000BA9DD
		[__DynamicallyInvokable]
		public string CurrencyGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
				this.currencyGroupSeparator = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000BC7F7 File Offset: 0x000BA9F7
		// (set) Token: 0x06003104 RID: 12548 RVA: 0x000BC7FF File Offset: 0x000BA9FF
		[__DynamicallyInvokable]
		public string CurrencySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.currencySymbol = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000BC828 File Offset: 0x000BAA28
		[__DynamicallyInvokable]
		public static NumberFormatInfo CurrentInfo
		{
			[__DynamicallyInvokable]
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000BC869 File Offset: 0x000BAA69
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000BC871 File Offset: 0x000BAA71
		[__DynamicallyInvokable]
		public string NaNSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nanSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000BC898 File Offset: 0x000BAA98
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x000BC8A0 File Offset: 0x000BAAA0
		[__DynamicallyInvokable]
		public int CurrencyNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 15));
				}
				this.VerifyWritable();
				this.currencyNegativePattern = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000BC8EF File Offset: 0x000BAAEF
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x000BC8F8 File Offset: 0x000BAAF8
		[__DynamicallyInvokable]
		public int NumberNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 4));
				}
				this.VerifyWritable();
				this.numberNegativePattern = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000BC945 File Offset: 0x000BAB45
		// (set) Token: 0x0600310D RID: 12557 RVA: 0x000BC950 File Offset: 0x000BAB50
		[__DynamicallyInvokable]
		public int PercentPositivePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentPositivePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 3));
				}
				this.VerifyWritable();
				this.percentPositivePattern = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000BC99D File Offset: 0x000BAB9D
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x000BC9A8 File Offset: 0x000BABA8
		[__DynamicallyInvokable]
		public int PercentNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 11));
				}
				this.VerifyWritable();
				this.percentNegativePattern = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000BC9F7 File Offset: 0x000BABF7
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x000BC9FF File Offset: 0x000BABFF
		[__DynamicallyInvokable]
		public string NegativeInfinitySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.negativeInfinitySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.negativeInfinitySymbol = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000BCA26 File Offset: 0x000BAC26
		// (set) Token: 0x06003113 RID: 12563 RVA: 0x000BCA2E File Offset: 0x000BAC2E
		[__DynamicallyInvokable]
		public string NegativeSign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.negativeSign;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.negativeSign = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000BCA55 File Offset: 0x000BAC55
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x000BCA60 File Offset: 0x000BAC60
		[__DynamicallyInvokable]
		public int NumberDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.numberDecimalDigits = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000BCAAF File Offset: 0x000BACAF
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x000BCAB7 File Offset: 0x000BACB7
		[__DynamicallyInvokable]
		public string NumberDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
				this.numberDecimalSeparator = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000BCAD1 File Offset: 0x000BACD1
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x000BCAD9 File Offset: 0x000BACD9
		[__DynamicallyInvokable]
		public string NumberGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
				this.numberGroupSeparator = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000BCAF3 File Offset: 0x000BACF3
		// (set) Token: 0x0600311B RID: 12571 RVA: 0x000BCAFC File Offset: 0x000BACFC
		[__DynamicallyInvokable]
		public int CurrencyPositivePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyPositivePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 3));
				}
				this.VerifyWritable();
				this.currencyPositivePattern = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000BCB49 File Offset: 0x000BAD49
		// (set) Token: 0x0600311D RID: 12573 RVA: 0x000BCB51 File Offset: 0x000BAD51
		[__DynamicallyInvokable]
		public string PositiveInfinitySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.positiveInfinitySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.positiveInfinitySymbol = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000BCB78 File Offset: 0x000BAD78
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x000BCB80 File Offset: 0x000BAD80
		[__DynamicallyInvokable]
		public string PositiveSign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.positiveSign;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.positiveSign = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000BCBA7 File Offset: 0x000BADA7
		// (set) Token: 0x06003121 RID: 12577 RVA: 0x000BCBB0 File Offset: 0x000BADB0
		[__DynamicallyInvokable]
		public int PercentDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.percentDecimalDigits = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000BCBFF File Offset: 0x000BADFF
		// (set) Token: 0x06003123 RID: 12579 RVA: 0x000BCC07 File Offset: 0x000BAE07
		[__DynamicallyInvokable]
		public string PercentDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
				this.percentDecimalSeparator = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000BCC21 File Offset: 0x000BAE21
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x000BCC29 File Offset: 0x000BAE29
		[__DynamicallyInvokable]
		public string PercentGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
				this.percentGroupSeparator = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000BCC43 File Offset: 0x000BAE43
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000BCC4B File Offset: 0x000BAE4B
		[__DynamicallyInvokable]
		public string PercentSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.percentSymbol = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000BCC72 File Offset: 0x000BAE72
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x000BCC7A File Offset: 0x000BAE7A
		[__DynamicallyInvokable]
		public string PerMilleSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.perMilleSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.perMilleSymbol = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000BCCA1 File Offset: 0x000BAEA1
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x000BCCB3 File Offset: 0x000BAEB3
		[ComVisible(false)]
		public string[] NativeDigits
		{
			get
			{
				return (string[])this.nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
				this.nativeDigits = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000BCCCD File Offset: 0x000BAECD
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x000BCCD5 File Offset: 0x000BAED5
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
				this.digitSubstitution = (int)value;
			}
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000BCCEF File Offset: 0x000BAEEF
		[__DynamicallyInvokable]
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000BCD08 File Offset: 0x000BAF08
		[__DynamicallyInvokable]
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000BCD44 File Offset: 0x000BAF44
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHexStyle"));
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000BCD91 File Offset: 0x000BAF91
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_HexStyleNotSupported"));
			}
		}

		// Token: 0x04001492 RID: 5266
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04001493 RID: 5267
		internal int[] numberGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04001494 RID: 5268
		internal int[] currencyGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04001495 RID: 5269
		internal int[] percentGroupSizes = new int[]
		{
			3
		};

		// Token: 0x04001496 RID: 5270
		internal string positiveSign = "+";

		// Token: 0x04001497 RID: 5271
		internal string negativeSign = "-";

		// Token: 0x04001498 RID: 5272
		internal string numberDecimalSeparator = ".";

		// Token: 0x04001499 RID: 5273
		internal string numberGroupSeparator = ",";

		// Token: 0x0400149A RID: 5274
		internal string currencyGroupSeparator = ",";

		// Token: 0x0400149B RID: 5275
		internal string currencyDecimalSeparator = ".";

		// Token: 0x0400149C RID: 5276
		internal string currencySymbol = "¤";

		// Token: 0x0400149D RID: 5277
		internal string ansiCurrencySymbol;

		// Token: 0x0400149E RID: 5278
		internal string nanSymbol = "NaN";

		// Token: 0x0400149F RID: 5279
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x040014A0 RID: 5280
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x040014A1 RID: 5281
		internal string percentDecimalSeparator = ".";

		// Token: 0x040014A2 RID: 5282
		internal string percentGroupSeparator = ",";

		// Token: 0x040014A3 RID: 5283
		internal string percentSymbol = "%";

		// Token: 0x040014A4 RID: 5284
		internal string perMilleSymbol = "‰";

		// Token: 0x040014A5 RID: 5285
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};

		// Token: 0x040014A6 RID: 5286
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x040014A7 RID: 5287
		internal int numberDecimalDigits = 2;

		// Token: 0x040014A8 RID: 5288
		internal int currencyDecimalDigits = 2;

		// Token: 0x040014A9 RID: 5289
		internal int currencyPositivePattern;

		// Token: 0x040014AA RID: 5290
		internal int currencyNegativePattern;

		// Token: 0x040014AB RID: 5291
		internal int numberNegativePattern = 1;

		// Token: 0x040014AC RID: 5292
		internal int percentPositivePattern;

		// Token: 0x040014AD RID: 5293
		internal int percentNegativePattern;

		// Token: 0x040014AE RID: 5294
		internal int percentDecimalDigits = 2;

		// Token: 0x040014AF RID: 5295
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x040014B0 RID: 5296
		internal bool isReadOnly;

		// Token: 0x040014B1 RID: 5297
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x040014B2 RID: 5298
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x040014B3 RID: 5299
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x040014B4 RID: 5300
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x040014B5 RID: 5301
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
