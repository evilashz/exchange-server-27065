using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A03 RID: 2563
	internal struct ResourceLoad
	{
		// Token: 0x06007692 RID: 30354 RVA: 0x00186917 File Offset: 0x00184B17
		public ResourceLoad(double loadRatio, int? metric, object info = null)
		{
			if (loadRatio >= 1.7976931348623157E+308)
			{
				this.loadRatio = double.MaxValue;
			}
			else
			{
				this.loadRatio = loadRatio + 1.0;
			}
			this.metric = metric;
			this.info = info;
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x00186955 File Offset: 0x00184B55
		public static bool operator ==(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio == load2.LoadRatio;
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x00186967 File Offset: 0x00184B67
		public static bool operator !=(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio != load2.LoadRatio;
		}

		// Token: 0x06007695 RID: 30357 RVA: 0x0018697C File Offset: 0x00184B7C
		public static bool operator <(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio < load2.LoadRatio;
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x0018698E File Offset: 0x00184B8E
		public static bool operator >(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio > load2.LoadRatio;
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x001869A0 File Offset: 0x00184BA0
		public static bool operator <=(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio <= load2.LoadRatio;
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x001869B5 File Offset: 0x00184BB5
		public static bool operator >=(ResourceLoad load1, ResourceLoad load2)
		{
			return load1.LoadRatio >= load2.LoadRatio;
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x001869CC File Offset: 0x00184BCC
		public static double operator -(ResourceLoad load1, ResourceLoad load2)
		{
			ResourceLoadState state = load1.State;
			if (state == ResourceLoadState.Unknown || state == ResourceLoadState.Critical)
			{
				return double.NaN;
			}
			ResourceLoadState state2 = load2.State;
			if (state2 == ResourceLoadState.Unknown || state2 == ResourceLoadState.Critical)
			{
				return double.NaN;
			}
			return load1.LoadRatio - load2.LoadRatio;
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x00186A1C File Offset: 0x00184C1C
		public static ResourceLoad operator -(ResourceLoad load, double delta)
		{
			ResourceLoadState state = load.State;
			if (state == ResourceLoadState.Unknown || state == ResourceLoadState.Critical)
			{
				return load;
			}
			return new ResourceLoad(load.LoadRatio - delta, load.Metric, load.Info);
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x00186A58 File Offset: 0x00184C58
		public static ResourceLoad operator +(ResourceLoad load, double delta)
		{
			ResourceLoadState state = load.State;
			if (state == ResourceLoadState.Unknown || state == ResourceLoadState.Critical)
			{
				return load;
			}
			return new ResourceLoad(load.LoadRatio + delta, load.Metric, load.Info);
		}

		// Token: 0x17002A6F RID: 10863
		// (get) Token: 0x0600769C RID: 30364 RVA: 0x00186A94 File Offset: 0x00184C94
		public ResourceLoadState State
		{
			get
			{
				if (this.LoadRatio < 0.0)
				{
					return ResourceLoadState.Unknown;
				}
				if (this.LoadRatio == 1.7976931348623157E+308)
				{
					return ResourceLoadState.Critical;
				}
				if (this.LoadRatio > 1.0)
				{
					return ResourceLoadState.Overloaded;
				}
				if (this.LoadRatio == 1.0)
				{
					return ResourceLoadState.Full;
				}
				if (this.LoadRatio < 1.0)
				{
					return ResourceLoadState.Underloaded;
				}
				throw new InvalidOperationException("Unexpected load ratio value.");
			}
		}

		// Token: 0x17002A70 RID: 10864
		// (get) Token: 0x0600769D RID: 30365 RVA: 0x00186B0A File Offset: 0x00184D0A
		public double LoadRatio
		{
			get
			{
				if (this.loadRatio != 1.7976931348623157E+308)
				{
					return this.loadRatio - 1.0;
				}
				return double.MaxValue;
			}
		}

		// Token: 0x17002A71 RID: 10865
		// (get) Token: 0x0600769E RID: 30366 RVA: 0x00186B37 File Offset: 0x00184D37
		public int? Metric
		{
			get
			{
				return this.metric;
			}
		}

		// Token: 0x17002A72 RID: 10866
		// (get) Token: 0x0600769F RID: 30367 RVA: 0x00186B3F File Offset: 0x00184D3F
		public object Info
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x00186B48 File Offset: 0x00184D48
		public override string ToString()
		{
			ResourceLoadState state = this.State;
			if (state != ResourceLoadState.Critical && state != ResourceLoadState.Unknown)
			{
				return string.Format("{0}/{1:#0.00}/{2}", state, this.LoadRatio, this.Metric);
			}
			return string.Format("{0}//{1}", state, this.Metric);
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x00186BA8 File Offset: 0x00184DA8
		public override int GetHashCode()
		{
			return this.LoadRatio.GetHashCode();
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x00186BC4 File Offset: 0x00184DC4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.State == ResourceLoadState.Unknown;
			}
			if (obj is ResourceLoad)
			{
				ResourceLoad resourceLoad = (ResourceLoad)obj;
				return this.loadRatio == resourceLoad.loadRatio;
			}
			return false;
		}

		// Token: 0x04004BEE RID: 19438
		public static readonly ResourceLoad Unknown = default(ResourceLoad);

		// Token: 0x04004BEF RID: 19439
		public static readonly ResourceLoad Zero = new ResourceLoad(0.0, null, null);

		// Token: 0x04004BF0 RID: 19440
		public static readonly ResourceLoad Full = new ResourceLoad(1.0, null, null);

		// Token: 0x04004BF1 RID: 19441
		public static readonly ResourceLoad Critical = new ResourceLoad(double.MaxValue, null, null);

		// Token: 0x04004BF2 RID: 19442
		private readonly double loadRatio;

		// Token: 0x04004BF3 RID: 19443
		private readonly int? metric;

		// Token: 0x04004BF4 RID: 19444
		private readonly object info;
	}
}
