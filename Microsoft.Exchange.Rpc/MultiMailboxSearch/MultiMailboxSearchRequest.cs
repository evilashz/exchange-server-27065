using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	internal sealed class MultiMailboxSearchRequest : MultiMailboxRequestBase
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00009EA8 File Offset: 0x000092A8
		internal MultiMailboxSearchRequest(int version) : base(version)
		{
			this.refinersEnabled = false;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00009E90 File Offset: 0x00009290
		internal MultiMailboxSearchRequest() : base(MultiMailboxSearchBase.CurrentVersion)
		{
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00009EC4 File Offset: 0x000092C4
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00009ED8 File Offset: 0x000092D8
		internal bool RefinersEnabled
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.refinersEnabled;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.refinersEnabled = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00009EEC File Offset: 0x000092EC
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x00009F00 File Offset: 0x00009300
		internal int RefinerResultsTrimCount
		{
			get
			{
				return this.refinerResultsTrimCount;
			}
			set
			{
				this.refinerResultsTrimCount = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00009F14 File Offset: 0x00009314
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00009F28 File Offset: 0x00009328
		internal string Query
		{
			get
			{
				return this.query;
			}
			set
			{
				this.query = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00009F3C File Offset: 0x0000933C
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00009F50 File Offset: 0x00009350
		internal byte[] SortCriteria
		{
			get
			{
				return this.sortCriteria;
			}
			set
			{
				this.sortCriteria = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00009F64 File Offset: 0x00009364
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00009F78 File Offset: 0x00009378
		internal Sorting SortingOrder
		{
			get
			{
				return this.sortingOrder;
			}
			set
			{
				this.sortingOrder = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00009F8C File Offset: 0x0000938C
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00009FA0 File Offset: 0x000093A0
		internal PagingInfo Paging
		{
			get
			{
				return this.pagingInfo;
			}
			set
			{
				this.pagingInfo = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00009FB4 File Offset: 0x000093B4
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00009FC8 File Offset: 0x000093C8
		internal byte[] Restriction
		{
			get
			{
				return this.restriction;
			}
			set
			{
				this.restriction = value;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00009FDC File Offset: 0x000093DC
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static MultiMailboxSearchRequest DeSerialize(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				MemoryStream memoryStream = null;
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				try
				{
					MultiMailboxSearchRequest result;
					try
					{
						memoryStream = new MemoryStream(bytes);
						return binaryFormatter.Deserialize(memoryStream) as MultiMailboxSearchRequest;
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

		// Token: 0x06000918 RID: 2328 RVA: 0x0000A05C File Offset: 0x0000945C
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static byte[] Serialize(MultiMailboxSearchRequest request)
		{
			if (request == null)
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
					ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null).Serialize(memoryStream, request);
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

		// Token: 0x04000B0D RID: 2829
		private string query;

		// Token: 0x04000B0E RID: 2830
		private PagingInfo pagingInfo;

		// Token: 0x04000B0F RID: 2831
		private Sorting sortingOrder;

		// Token: 0x04000B10 RID: 2832
		private byte[] restriction;

		// Token: 0x04000B11 RID: 2833
		private bool refinersEnabled;

		// Token: 0x04000B12 RID: 2834
		private int refinerResultsTrimCount;

		// Token: 0x04000B13 RID: 2835
		private byte[] sortCriteria;
	}
}
