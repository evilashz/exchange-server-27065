using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.CommonMath;

namespace Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection
{
	// Token: 0x0200005A RID: 90
	[DataContract]
	[Serializable]
	internal class WeightedCluster<T>
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000085E8 File Offset: 0x000067E8
		public WeightedCluster(Func<T, Box2D> getOutline, Func<T, float> getDensity)
		{
			this.getOutline = getOutline;
			this.getDensity = getDensity;
			this.Reset();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008604 File Offset: 0x00006804
		public WeightedCluster(Func<T, Box2D> getOutline, Func<T, float> getDensity, IEnumerable<T> clusteredRegions) : this(getOutline, getDensity)
		{
			this.Compute(clusteredRegions);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008615 File Offset: 0x00006815
		public WeightedCluster(Func<T, Box2D> getOutline, Func<T, float> getDensity, params T[] clusteredRegions) : this(getOutline, getDensity)
		{
			this.Compute(clusteredRegions);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008628 File Offset: 0x00006828
		public float OutlineDensity
		{
			get
			{
				if (this.ElementCount <= 0)
				{
					return 0f;
				}
				return this.Mass / this.Outline.Area;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00008659 File Offset: 0x00006859
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00008661 File Offset: 0x00006861
		[DataMember]
		public float MaximumDensity { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000866A File Offset: 0x0000686A
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00008672 File Offset: 0x00006872
		[DataMember]
		public Box2D Outline { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000867B File Offset: 0x0000687B
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00008683 File Offset: 0x00006883
		[DataMember]
		public Vector2 Center { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000868C File Offset: 0x0000688C
		public Vector2 GeometricalCenter
		{
			get
			{
				return this.Outline.Center;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000086A7 File Offset: 0x000068A7
		// (set) Token: 0x060002CE RID: 718 RVA: 0x000086AF File Offset: 0x000068AF
		[DataMember]
		public float Mass { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000086B8 File Offset: 0x000068B8
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000086C0 File Offset: 0x000068C0
		[DataMember]
		public int ElementCount { get; private set; }

		// Token: 0x060002D1 RID: 721 RVA: 0x000086C9 File Offset: 0x000068C9
		public void Reset()
		{
			this.MaximumDensity = 0f;
			this.Mass = 0f;
			this.Outline = Box2D.Null;
			this.Center = Vector2.Zero;
			this.ElementCount = 0;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008700 File Offset: 0x00006900
		public void Add(T region)
		{
			if (this.getDensity == null || this.getOutline == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "This cluster is locked and can not accept new elements. You can only access properties on it or combine it with another cluster.", new object[0]));
			}
			float num = this.getDensity(region);
			if (num > 0f)
			{
				Box2D box = this.getOutline(region);
				float num2 = num * box.Area;
				this.Center = (this.Center * this.Mass + box.Center * num2) / (this.Mass + num2);
				this.Mass += num2;
				this.Outline += box;
				this.MaximumDensity = Math.Max(this.MaximumDensity, num);
				this.ElementCount++;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000087E0 File Offset: 0x000069E0
		public void Compute(IEnumerable<T> clusteredRegions)
		{
			if (clusteredRegions == null)
			{
				throw new ArgumentNullException("clusteredRegions");
			}
			foreach (T region in clusteredRegions)
			{
				this.Add(region);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00008838 File Offset: 0x00006A38
		public WeightedCluster<T> Lock()
		{
			this.getDensity = null;
			this.getOutline = null;
			return this;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000884C File Offset: 0x00006A4C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} center at {1} of mass {2} with {3} elements.", new object[]
			{
				this.Outline,
				this.Center,
				this.Mass,
				this.ElementCount
			});
		}

		// Token: 0x040001E7 RID: 487
		[NonSerialized]
		private Func<T, Box2D> getOutline;

		// Token: 0x040001E8 RID: 488
		[NonSerialized]
		private Func<T, float> getDensity;
	}
}
