using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	internal sealed class MultiMailboxSearchResponse : MultiMailboxResponseBase
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0000A404 File Offset: 0x00009804
		internal MultiMailboxSearchResponse(int version, Dictionary<string, List<MultiMailboxSearchRefinersResult>> refinersOutput, long totalResultCount, long totalResultSize) : base(version)
		{
			this.refinersOutput = refinersOutput;
			this.totalResultCount = totalResultCount;
			this.totalResultSize = totalResultSize;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000A3DC File Offset: 0x000097DC
		internal MultiMailboxSearchResponse(Dictionary<string, List<MultiMailboxSearchRefinersResult>> refinersOutput, long totalResultCount, long totalResultSize)
		{
			this.refinersOutput = refinersOutput;
			this.totalResultCount = totalResultCount;
			this.totalResultSize = totalResultSize;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000A3BC File Offset: 0x000097BC
		internal MultiMailboxSearchResponse(int version) : base(version)
		{
			this.refinersOutput = new Dictionary<string, List<MultiMailboxSearchRefinersResult>>(0);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000A39C File Offset: 0x0000979C
		internal MultiMailboxSearchResponse()
		{
			this.refinersOutput = new Dictionary<string, List<MultiMailboxSearchRefinersResult>>(0);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0000A430 File Offset: 0x00009830
		internal Dictionary<string, List<MultiMailboxSearchRefinersResult>> RefinerOutput
		{
			get
			{
				return this.refinersOutput;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0000A444 File Offset: 0x00009844
		internal long TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0000A458 File Offset: 0x00009858
		internal long TotalResultSize
		{
			get
			{
				return this.totalResultSize;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0000A46C File Offset: 0x0000986C
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0000A480 File Offset: 0x00009880
		internal PageReference PagingReference
		{
			get
			{
				return this.pageReference;
			}
			set
			{
				this.pageReference = value;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0000A494 File Offset: 0x00009894
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static MultiMailboxSearchResponse DeSerialize(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				MemoryStream memoryStream = null;
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				try
				{
					MultiMailboxSearchResponse result;
					try
					{
						memoryStream = new MemoryStream(bytes);
						return binaryFormatter.Deserialize(memoryStream) as MultiMailboxSearchResponse;
					}
					catch (SerializationException)
					{
						result = null;
					}
					return result;
				}
				finally
				{
					if (null != memoryStream)
					{
						memoryStream.Close();
					}
				}
			}
			return null;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0000A514 File Offset: 0x00009914
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static byte[] Serialize(MultiMailboxSearchResponse response)
		{
			if (response == null)
			{
				return new byte[0];
			}
			MemoryStream memoryStream = null;
			byte[] result;
			try
			{
				byte[] array;
				try
				{
					memoryStream = new MemoryStream();
					ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null).Serialize(memoryStream, response);
					return memoryStream.ToArray();
				}
				catch (SerializationException)
				{
					array = new byte[0];
				}
				result = array;
			}
			finally
			{
				if (null != memoryStream)
				{
					memoryStream.Close();
				}
			}
			return result;
		}

		// Token: 0x04000B20 RID: 2848
		private PageReference pageReference;

		// Token: 0x04000B21 RID: 2849
		private readonly Dictionary<string, List<MultiMailboxSearchRefinersResult>> refinersOutput;

		// Token: 0x04000B22 RID: 2850
		private readonly long totalResultCount;

		// Token: 0x04000B23 RID: 2851
		private readonly long totalResultSize;
	}
}
