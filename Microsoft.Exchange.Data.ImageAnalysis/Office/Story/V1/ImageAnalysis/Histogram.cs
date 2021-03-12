using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal class Histogram<T>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000247C File Offset: 0x0000067C
		public Histogram(Func<T, double> valueExtractor, double start, double end, double interval, IEnumerable<T> items) : this(valueExtractor, start, end, interval)
		{
			this.Add(items);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002494 File Offset: 0x00000694
		public Histogram(Func<T, double> valueExtractor, double start, double end, double interval)
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException("Interval must be positive.", "interval");
			}
			if (start >= end)
			{
				throw new ArgumentException("End of the interval must be above Start of the interval.", "end");
			}
			this.valueExtractor = valueExtractor;
			this.Start = start;
			this.End = end;
			this.Interval = interval;
			this.ItemsCount = 0;
			this.OutOfRangeItemsCount = 0;
			this.Bins = new List<Bin<T>>();
			for (double num = this.Start; num < this.End; num += this.Interval)
			{
				this.Bins.Add(new Bin<T>
				{
					Items = new List<T>(),
					RangeStart = num,
					RangeEnd = num + interval
				});
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002557 File Offset: 0x00000757
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000255F File Offset: 0x0000075F
		public double Start { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002570 File Offset: 0x00000770
		public double End { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002579 File Offset: 0x00000779
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002581 File Offset: 0x00000781
		public double Interval { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000258A File Offset: 0x0000078A
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002592 File Offset: 0x00000792
		public int ItemsCount { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000259B File Offset: 0x0000079B
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000025A3 File Offset: 0x000007A3
		public int OutOfRangeItemsCount { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000025AC File Offset: 0x000007AC
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000025B4 File Offset: 0x000007B4
		public List<Bin<T>> Bins { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x000025C0 File Offset: 0x000007C0
		public void Add(T item)
		{
			double num = this.valueExtractor(item);
			if (num >= this.Start && num < this.End)
			{
				int index = (int)((num - this.Start) / this.Interval);
				this.Bins[index].Items.Add(item);
			}
			else
			{
				this.OutOfRangeItemsCount++;
			}
			this.ItemsCount++;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002634 File Offset: 0x00000834
		public void Add(IEnumerable<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (T item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000268C File Offset: 0x0000088C
		public double ScanForRange(double startWindow, double endWindow, Func<Bin<T>, double> extractor, Func<double, double, double> aggregator, Func<double, double, bool> exitCriteria)
		{
			if (extractor == null)
			{
				throw new ArgumentNullException("extractor");
			}
			if (aggregator == null)
			{
				throw new ArgumentNullException("aggregator");
			}
			if (exitCriteria == null)
			{
				throw new ArgumentNullException("exitCriteria");
			}
			double arg = 0.0;
			double num;
			if (startWindow < endWindow)
			{
				num = startWindow;
				for (int i = 0; i < this.Bins.Count; i++)
				{
					Bin<T> bin = this.Bins[i];
					if (bin.RangeStart >= startWindow)
					{
						double arg2 = extractor(bin);
						arg = aggregator(arg, arg2);
						if (exitCriteria(arg, arg2))
						{
							break;
						}
						num = Math.Max(num, bin.RangeEnd);
					}
				}
			}
			else
			{
				num = startWindow;
				for (int j = this.Bins.Count - 1; j >= 0; j--)
				{
					Bin<T> bin2 = this.Bins[j];
					if (bin2.RangeEnd >= startWindow)
					{
						double arg3 = extractor(bin2);
						arg = aggregator(arg, arg3);
						if (exitCriteria(arg, arg3))
						{
							break;
						}
						num = Math.Min(num, bin2.RangeStart);
					}
				}
			}
			return num;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027E1 File Offset: 0x000009E1
		public override string ToString()
		{
			return string.Join("\n", from bin in this.Bins
			select string.Format(CultureInfo.InvariantCulture, "{0}\t{1}\t", new object[]
			{
				bin.RangeStart,
				bin.Items.Count<T>()
			}));
		}

		// Token: 0x04000015 RID: 21
		[NonSerialized]
		private readonly Func<T, double> valueExtractor;
	}
}
