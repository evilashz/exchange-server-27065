using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	internal sealed class MultiMailboxKeywordStatsRequest : MultiMailboxRequestBase
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0000A6E0 File Offset: 0x00009AE0
		internal MultiMailboxKeywordStatsRequest(int version) : base(version)
		{
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000A6CC File Offset: 0x00009ACC
		internal MultiMailboxKeywordStatsRequest()
		{
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0000A6F4 File Offset: 0x00009AF4
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0000A708 File Offset: 0x00009B08
		internal List<KeyValuePair<string, byte[]>> Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0000A71C File Offset: 0x00009B1C
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static MultiMailboxKeywordStatsRequest DeSerialize(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				MemoryStream memoryStream = null;
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				try
				{
					MultiMailboxKeywordStatsRequest result;
					try
					{
						memoryStream = new MemoryStream(bytes);
						return binaryFormatter.Deserialize(memoryStream) as MultiMailboxKeywordStatsRequest;
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

		// Token: 0x06000949 RID: 2377 RVA: 0x0000A79C File Offset: 0x00009B9C
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static byte[] Serialize(MultiMailboxKeywordStatsRequest request)
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

		// Token: 0x04000B24 RID: 2852
		private List<KeyValuePair<string, byte[]>> keywords;
	}
}
