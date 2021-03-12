using System;

namespace System.Globalization
{
	// Token: 0x0200039D RID: 925
	internal class CalendricalCalculationsHelper
	{
		// Token: 0x06002F92 RID: 12178 RVA: 0x000B60CE File Offset: 0x000B42CE
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.141592653589793 / 180.0;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000B60E5 File Offset: 0x000B42E5
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000B60F2 File Offset: 0x000B42F2
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000B60FF File Offset: 0x000B42FF
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000B610C File Offset: 0x000B430C
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000B6129 File Offset: 0x000B4329
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000B6136 File Offset: 0x000B4336
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000B614C File Offset: 0x000B434C
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000B618C File Offset: 0x000B438C
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000B61A7 File Offset: 0x000B43A7
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B61D4 File Offset: 0x000B43D4
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B61E4 File Offset: 0x000B43E4
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000B621C File Offset: 0x000B441C
		private static double CenturiesFrom1900(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1));
			return (double)(numberOfDays - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000B624C File Offset: 0x000B444C
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1));
			double num = (double)(numberOfDays - CalendricalCalculationsHelper.StartOf1810);
			double x = 0.5 + num;
			return (Math.Pow(x, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000B62A9 File Offset: 0x000B44A9
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000B62C0 File Offset: 0x000B44C0
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, indeterminate);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000B62E0 File Offset: 0x000B44E0
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, indeterminate);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000B6300 File Offset: 0x000B4500
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, indeterminate) / 86400.0;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000B632C File Offset: 0x000B452C
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double indeterminate = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, indeterminate) / 86400.0;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B6358 File Offset: 0x000B4558
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] ephemerisCorrectionTable = CalendricalCalculationsHelper.EphemerisCorrectionTable;
			int i = 0;
			while (i < ephemerisCorrectionTable.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = ephemerisCorrectionTable[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_7F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_7F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000B63EC File Offset: 0x000B45EC
		public static double JulianCenturies(double moment)
		{
			double num = moment + CalendricalCalculationsHelper.EphemerisCorrection(moment);
			return (num - 730120.5) / 36525.0;
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000B6417 File Offset: 0x000B4617
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B6422 File Offset: 0x000B4622
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000B6438 File Offset: 0x000B4638
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.Obliquity(num);
			double num6 = CalendricalCalculationsHelper.TanOfDegree(num5 / 2.0);
			double num7 = num6 * num6;
			double num8 = num7 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num7 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num7, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num9 = 6.283185307179586;
			double num10 = num8 / num9;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num10), 0.5), num10);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000B6568 File Offset: 0x000B4768
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double time = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(time);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000B6586 File Offset: 0x000B4786
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000B65A0 File Offset: 0x000B47A0
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000B65BC File Offset: 0x000B47BC
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000B65D2 File Offset: 0x000B47D2
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000B65E4 File Offset: 0x000B47E4
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			double num = 0.0;
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536);
			return num + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B6BBC File Offset: 0x000B4DBC
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000B6BEC File Offset: 0x000B4DEC
		private static double Nutation(double julianCenturies)
		{
			double degree = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies);
			double degree2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(degree) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(degree2);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B6C34 File Offset: 0x000B4E34
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = 282.7771834 + 36000.76953744 * num + 5.729577951308232E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num);
			double longitude = num2 + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num);
			return CalendricalCalculationsHelper.InitLongitude(longitude);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B6C85 File Offset: 0x000B4E85
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B6CA0 File Offset: 0x000B4EA0
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B6CF0 File Offset: 0x000B4EF0
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double date = (double)numberOfDays;
			double d = CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(date));
			long num = (long)Math.Floor(d) - 1L;
			long num2 = num + 3L;
			long num3;
			for (num3 = num; num3 != num2; num3 += 1L)
			{
				double time = CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num3);
				double num4 = CalendricalCalculationsHelper.Compute(time);
				if (0.0 <= num4 && num4 <= 2.0)
				{
					break;
				}
			}
			return num3 - 1L;
		}

		// Token: 0x04001415 RID: 5141
		private const double FullCircleOfArc = 360.0;

		// Token: 0x04001416 RID: 5142
		private const int HalfCircleOfArc = 180;

		// Token: 0x04001417 RID: 5143
		private const double TwelveHours = 0.5;

		// Token: 0x04001418 RID: 5144
		private const double Noon2000Jan01 = 730120.5;

		// Token: 0x04001419 RID: 5145
		internal const double MeanTropicalYearInDays = 365.242189;

		// Token: 0x0400141A RID: 5146
		private const double MeanSpeedOfSun = 1.0145616361111112;

		// Token: 0x0400141B RID: 5147
		private const double LongitudeSpring = 0.0;

		// Token: 0x0400141C RID: 5148
		private const double TwoDegreesAfterSpring = 2.0;

		// Token: 0x0400141D RID: 5149
		private const int SecondsPerDay = 86400;

		// Token: 0x0400141E RID: 5150
		private const int DaysInUniformLengthCentury = 36525;

		// Token: 0x0400141F RID: 5151
		private const int SecondsPerMinute = 60;

		// Token: 0x04001420 RID: 5152
		private const int MinutesPerDegree = 60;

		// Token: 0x04001421 RID: 5153
		private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x04001422 RID: 5154
		private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x04001423 RID: 5155
		private static double[] Coefficients1900to1987 = new double[]
		{
			-2E-05,
			0.000297,
			0.025184,
			-0.181133,
			0.55304,
			-0.861938,
			0.677066,
			-0.212591
		};

		// Token: 0x04001424 RID: 5156
		private static double[] Coefficients1800to1899 = new double[]
		{
			-9E-06,
			0.003844,
			0.083563,
			0.865736,
			4.867575,
			15.845535,
			31.332267,
			38.291999,
			28.316289,
			11.636204,
			2.043794
		};

		// Token: 0x04001425 RID: 5157
		private static double[] Coefficients1700to1799 = new double[]
		{
			8.118780842,
			-0.005092142,
			0.003336121,
			-2.66484E-05
		};

		// Token: 0x04001426 RID: 5158
		private static double[] Coefficients1620to1699 = new double[]
		{
			196.58333,
			-4.0675,
			0.0219167
		};

		// Token: 0x04001427 RID: 5159
		private static double[] LambdaCoefficients = new double[]
		{
			280.46645,
			36000.76983,
			0.0003032
		};

		// Token: 0x04001428 RID: 5160
		private static double[] AnomalyCoefficients = new double[]
		{
			357.5291,
			35999.0503,
			-0.0001559,
			-4.8E-07
		};

		// Token: 0x04001429 RID: 5161
		private static double[] EccentricityCoefficients = new double[]
		{
			0.016708617,
			-4.2037E-05,
			-1.236E-07
		};

		// Token: 0x0400142A RID: 5162
		private static double[] Coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x0400142B RID: 5163
		private static double[] CoefficientsA = new double[]
		{
			124.9,
			-1934.134,
			0.002063
		};

		// Token: 0x0400142C RID: 5164
		private static double[] CoefficientsB = new double[]
		{
			201.11,
			72001.5377,
			0.00057
		};

		// Token: 0x0400142D RID: 5165
		private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x02000B38 RID: 2872
		private enum CorrectionAlgorithm
		{
			// Token: 0x0400337F RID: 13183
			Default,
			// Token: 0x04003380 RID: 13184
			Year1988to2019,
			// Token: 0x04003381 RID: 13185
			Year1900to1987,
			// Token: 0x04003382 RID: 13186
			Year1800to1899,
			// Token: 0x04003383 RID: 13187
			Year1700to1799,
			// Token: 0x04003384 RID: 13188
			Year1620to1699
		}

		// Token: 0x02000B39 RID: 2873
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x06006AFE RID: 27390 RVA: 0x00171731 File Offset: 0x0016F931
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x04003385 RID: 13189
			internal int _lowestYear;

			// Token: 0x04003386 RID: 13190
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
