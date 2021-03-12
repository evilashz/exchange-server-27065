using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000126 RID: 294
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Random
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x00032F9F File Offset: 0x0003119F
		[__DynamicallyInvokable]
		public Random() : this(Environment.TickCount)
		{
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00032FAC File Offset: 0x000311AC
		[__DynamicallyInvokable]
		public Random(int Seed)
		{
			int num = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
			int num2 = 161803398 - num;
			this.SeedArray[55] = num2;
			int num3 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num4 = 21 * i % 55;
				this.SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = this.SeedArray[num4];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
					if (this.SeedArray[k] < 0)
					{
						this.SeedArray[k] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 21;
			Seed = 1;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000330A9 File Offset: 0x000312A9
		[__DynamicallyInvokable]
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000330BC File Offset: 0x000312BC
		private int InternalSample()
		{
			int num = this.inext;
			int num2 = this.inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this.SeedArray[num] - this.SeedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this.SeedArray[num] = num3;
			this.inext = num;
			this.inextp = num2;
			return num3;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003312F File Offset: 0x0003132F
		[__DynamicallyInvokable]
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00033138 File Offset: 0x00031338
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			bool flag = this.InternalSample() % 2 == 0;
			if (flag)
			{
				num = -num;
			}
			double num2 = (double)num;
			num2 += 2147483646.0;
			return num2 / 4294967293.0;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00033180 File Offset: 0x00031380
		[__DynamicallyInvokable]
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", Environment.GetResourceString("Argument_MinMaxValue", new object[]
				{
					"minValue",
					"maxValue"
				}));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000331E6 File Offset: 0x000313E6
		[__DynamicallyInvokable]
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[]
				{
					"maxValue"
				}));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00033219 File Offset: 0x00031419
		[__DynamicallyInvokable]
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00033224 File Offset: 0x00031424
		[__DynamicallyInvokable]
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.InternalSample() % 256);
			}
		}

		// Token: 0x040005F0 RID: 1520
		private const int MBIG = 2147483647;

		// Token: 0x040005F1 RID: 1521
		private const int MSEED = 161803398;

		// Token: 0x040005F2 RID: 1522
		private const int MZ = 0;

		// Token: 0x040005F3 RID: 1523
		private int inext;

		// Token: 0x040005F4 RID: 1524
		private int inextp;

		// Token: 0x040005F5 RID: 1525
		private int[] SeedArray = new int[56];
	}
}
