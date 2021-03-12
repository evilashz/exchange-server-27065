using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000078 RID: 120
	internal class DataLatencyDetailCollection
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000F891 File Offset: 0x0000DA91
		public DataLatencyDetailCollection(int fssCopyId, IList<DataLatencyDetail> details)
		{
			if (details == null)
			{
				throw new ArgumentNullException("detail");
			}
			this.FssCopyId = fssCopyId;
			this.details = details;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000F8B5 File Offset: 0x0000DAB5
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000F8BD File Offset: 0x0000DABD
		public int FssCopyId { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
		public IList<DataLatencyDetail> Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x170001B5 RID: 437
		public DataLatencyDetail this[int index]
		{
			get
			{
				return (from d in this.details
				orderby d.TemporalPartition
				select d).Reverse<DataLatencyDetail>().ElementAt(index);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DataLatencyDetailCollection dataLatencyDetailCollection = obj as DataLatencyDetailCollection;
			return dataLatencyDetailCollection != null && this.Equals(dataLatencyDetailCollection);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public bool Equals(DataLatencyDetailCollection collection)
		{
			return this.FssCopyId == collection.FssCopyId && this.Details.Count == collection.Details.Count && this.details.All((DataLatencyDetail d1) => collection.details.Contains(d1));
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000F9AF File Offset: 0x0000DBAF
		public override int GetHashCode()
		{
			return this.details.GetHashCode();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		public static DataLatencyDetailCollection GetEmpty(int fssCopyId)
		{
			return new DataLatencyDetailCollection(fssCopyId, new List<DataLatencyDetail>());
		}

		// Token: 0x040002EC RID: 748
		private IList<DataLatencyDetail> details;
	}
}
