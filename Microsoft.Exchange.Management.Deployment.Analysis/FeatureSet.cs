using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200001B RID: 27
	public sealed class FeatureSet : IEnumerable<Feature>, IEnumerable
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000043EC File Offset: 0x000025EC
		private FeatureSet(IEnumerable<Feature> features, Func<FeatureSet> parent = null)
		{
			this.parent = parent;
			if (features == null)
			{
				throw new ArgumentNullException("features");
			}
			if (features.Any((Feature x) => x == null))
			{
				throw new ArgumentException(Strings.CannotAddNullFeature, "features");
			}
			if ((from x in features
			group x by x.GetType()).Any((IGrouping<Type, Feature> x) => x.Count<Feature>() > 1))
			{
				throw new ArgumentException(Strings.CanOnlyHaveOneFeatureOfEachType, "features");
			}
			this.features = new List<Feature>(features);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000044CD File Offset: 0x000026CD
		public bool HasFeature<T>() where T : Feature
		{
			return this.features.Any((Feature x) => x.GetType() == typeof(T)) || (this.parent != null && this.parent().HasFeature<T>());
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000451C File Offset: 0x0000271C
		public T GetFeature<T>() where T : Feature
		{
			T t = (T)((object)this.features.FirstOrDefault((Feature x) => x.GetType() == typeof(T)));
			if (t != null)
			{
				return t;
			}
			if (this.parent == null)
			{
				throw new Exception(Strings.FeatureMissing(typeof(T).Name));
			}
			return this.parent().GetFeature<T>();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000480C File Offset: 0x00002A0C
		public IEnumerator<Feature> GetEnumerator()
		{
			foreach (Feature feature2 in this.features)
			{
				yield return feature2;
			}
			if (this.parent != null)
			{
				using (IEnumerator<Feature> enumerator2 = this.parent().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Feature feature = enumerator2.Current;
						if (!this.features.Any((Feature x) => x.GetType() == feature.GetType()))
						{
							yield return feature;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004828 File Offset: 0x00002A28
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400004D RID: 77
		private readonly Func<FeatureSet> parent;

		// Token: 0x0400004E RID: 78
		private readonly List<Feature> features;

		// Token: 0x0200001C RID: 28
		public abstract class Builder
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x00004830 File Offset: 0x00002A30
			protected FeatureSet BuildFeatureSet(IEnumerable<Feature> features, params Feature[] explicitFeatures)
			{
				return new FeatureSet(features.Union(explicitFeatures), null);
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x0000483F File Offset: 0x00002A3F
			protected FeatureSet BuildFeatureSet(Func<FeatureSet> parent, IEnumerable<Feature> features, params Feature[] explicitFeatures)
			{
				return new FeatureSet(features.Union(explicitFeatures), null);
			}
		}
	}
}
