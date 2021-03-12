using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000048 RID: 72
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadMetricStorage
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00008C95 File Offset: 0x00006E95
		public LoadMetricStorage()
		{
			this.values = new ConcurrentDictionary<LoadMetric, long>();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public LoadMetricStorage(LoadMetricStorage metricStorage) : this()
		{
			foreach (LoadMetric metric in metricStorage.Metrics)
			{
				this[metric] = metricStorage[metric];
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00008D04 File Offset: 0x00006F04
		public IEnumerable<LoadMetric> Metrics
		{
			get
			{
				return this.values.Keys;
			}
		}

		// Token: 0x170000D9 RID: 217
		public long this[LoadMetric metric]
		{
			get
			{
				long result;
				if (!this.values.TryGetValue(metric, out result))
				{
					result = 0L;
				}
				return result;
			}
			set
			{
				this.values[metric] = value;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008D44 File Offset: 0x00006F44
		public static LoadMetricStorage operator +(LoadMetricStorage left, LoadMetricStorage right)
		{
			AnchorUtil.ThrowOnNullArgument(left, "left");
			AnchorUtil.ThrowOnNullArgument(right, "right");
			LoadMetricStorage loadMetricStorage = new LoadMetricStorage();
			foreach (LoadMetric metric in left.values.Keys)
			{
				loadMetricStorage[metric] = left[metric];
			}
			foreach (LoadMetric metric2 in right.values.Keys)
			{
				loadMetricStorage[metric2] += right[metric2];
			}
			return loadMetricStorage;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008E2C File Offset: 0x0000702C
		public static LoadMetricStorage operator -(LoadMetricStorage left, LoadMetricStorage right)
		{
			LoadMetricStorage result = new LoadMetricStorage();
			foreach (LoadMetric metric in left.values.Keys)
			{
				result[metric] = left[metric];
			}
			foreach (LoadMetric metric2 in from key in right.values.Keys
			where result.values.ContainsKey(key)
			select key)
			{
				result[metric2] -= right[metric2];
			}
			return result;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008F14 File Offset: 0x00007114
		public void ConvertFromSerializationFormat()
		{
			foreach (LoadMetric loadMetric in this.Metrics)
			{
				long value;
				this.values.TryRemove(loadMetric, out value);
				foreach (LoadMetric loadMetric2 in LoadMetricRepository.DefaultMetrics)
				{
					if (loadMetric.Name.Equals(loadMetric2.Name))
					{
						this[loadMetric2] = value;
					}
				}
				BandData bandData = loadMetric as BandData;
				if (bandData != null)
				{
					this[bandData.Band] = value;
				}
				else
				{
					Band band = loadMetric as Band;
					if (band != null)
					{
						this[band] = value;
					}
				}
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009004 File Offset: 0x00007204
		public void ConvertToSerializationMetrics(bool convertBandToBandData)
		{
			foreach (LoadMetric loadMetric in this.Metrics)
			{
				long num;
				this.values.TryRemove(loadMetric, out num);
				Band band = loadMetric as Band;
				LoadMetric metric;
				if (band != null)
				{
					if (convertBandToBandData)
					{
						metric = new BandData(band)
						{
							TotalWeight = (int)num
						};
					}
					else
					{
						metric = band;
					}
				}
				else
				{
					metric = new LoadMetric(loadMetric.Name, loadMetric.IsSize);
				}
				this[metric] = num;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000090C0 File Offset: 0x000072C0
		public BandData GetBandData(Band band)
		{
			Band left = this.Metrics.OfType<Band>().FirstOrDefault((Band bd) => object.Equals(band, bd));
			BandData bandData = new BandData(band)
			{
				TotalWeight = 0
			};
			if (left == null)
			{
				return bandData;
			}
			bandData.TotalWeight = (int)this[band];
			return bandData;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000912C File Offset: 0x0000732C
		public ByteQuantifiedSize GetSizeMetric(LoadMetric sizeMetric)
		{
			long num;
			if (!this.values.TryGetValue(sizeMetric, out num) || num <= 0L)
			{
				return ByteQuantifiedSize.Zero;
			}
			return sizeMetric.ToByteQuantifiedSize(num);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000915C File Offset: 0x0000735C
		public bool SupportsAdditional(LoadMetricStorage incomingLoad, out LoadMetric exceededMetric, out long requestedUnits, out long availableUnits)
		{
			AnchorUtil.ThrowOnNullArgument(incomingLoad, "incomingLoad");
			exceededMetric = null;
			requestedUnits = 0L;
			availableUnits = 0L;
			foreach (LoadMetric loadMetric in this.values.Keys)
			{
				if (this[loadMetric] < incomingLoad[loadMetric] || this[loadMetric] == 0L)
				{
					exceededMetric = loadMetric;
					availableUnits = this[loadMetric];
					requestedUnits = incomingLoad[loadMetric];
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000921C File Offset: 0x0000741C
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.values.Select(delegate(KeyValuePair<LoadMetric, long> kvp)
			{
				LoadMetric key = kvp.Key;
				return key.GetValueString(kvp.Value);
			});
			string arg = string.Join(",", enumerable);
			return string.Format("{{{0}}}", arg);
		}

		// Token: 0x040000C6 RID: 198
		[DataMember]
		private readonly ConcurrentDictionary<LoadMetric, long> values;
	}
}
