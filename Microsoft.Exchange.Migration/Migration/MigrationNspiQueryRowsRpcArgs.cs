using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E8 RID: 232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiQueryRowsRpcArgs : MigrationNspiRpcArgs
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00034141 File Offset: 0x00032341
		public MigrationNspiQueryRowsRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint, int? batchSize, int? startIndex, long[] longPropTags) : base(endpoint, MigrationProxyRpcType.QueryRows)
		{
			this.BatchSize = batchSize;
			this.StartIndex = startIndex;
			this.LongPropTags = longPropTags;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00034161 File Offset: 0x00032361
		public MigrationNspiQueryRowsRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.QueryRows)
		{
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0003416C File Offset: 0x0003236C
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x000341AA File Offset: 0x000323AA
		public int? BatchSize
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2416312323U, out obj) && obj is int)
				{
					return new int?((int)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2416312323U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2416312323U);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000341E4 File Offset: 0x000323E4
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00034222 File Offset: 0x00032422
		public int? StartIndex
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2416377859U, out obj) && obj is int)
				{
					return new int?((int)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2416377859U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2416377859U);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003425B File Offset: 0x0003245B
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00034268 File Offset: 0x00032468
		public long[] LongPropTags
		{
			private get
			{
				return base.GetProperty<long[]>(2416447508U);
			}
			set
			{
				base.SetProperty(2416447508U, value);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00034278 File Offset: 0x00032478
		public PropTag[] PropTags
		{
			get
			{
				long[] longPropTags = this.LongPropTags;
				if (longPropTags == null)
				{
					return null;
				}
				PropTag[] array = new PropTag[longPropTags.Length];
				for (int i = 0; i < longPropTags.Length; i++)
				{
					array[i] = (PropTag)longPropTags[i];
				}
				return array;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000342B0 File Offset: 0x000324B0
		public override bool Validate(out string errorMsg)
		{
			if (!base.Validate(out errorMsg))
			{
				return false;
			}
			if (this.StartIndex == null || this.StartIndex.Value < 0)
			{
				errorMsg = "Invalid Start Index.";
				return false;
			}
			if (this.BatchSize == null || this.BatchSize.Value < 1)
			{
				errorMsg = "Invalid Batch Size.";
				return false;
			}
			if (this.LongPropTags == null || this.LongPropTags.Length == 0)
			{
				errorMsg = "PropTags cannot be null.";
				return false;
			}
			errorMsg = null;
			return true;
		}
	}
}
