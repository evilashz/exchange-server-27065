using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	internal sealed class MultiMailboxKeywordStatsResponse : MultiMailboxResponseBase
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x0000A5B0 File Offset: 0x000099B0
		internal MultiMailboxKeywordStatsResponse(int version) : base(version)
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0000A59C File Offset: 0x0000999C
		internal MultiMailboxKeywordStatsResponse()
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0000A5C4 File Offset: 0x000099C4
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static MultiMailboxKeywordStatsResponse DeSerialize(byte[] bytes)
		{
			if (bytes != null && bytes.Length > 0)
			{
				MemoryStream memoryStream = null;
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				try
				{
					MultiMailboxKeywordStatsResponse result;
					try
					{
						memoryStream = new MemoryStream(bytes);
						return binaryFormatter.Deserialize(memoryStream) as MultiMailboxKeywordStatsResponse;
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

		// Token: 0x06000943 RID: 2371 RVA: 0x0000A644 File Offset: 0x00009A44
		[SuppressMessage("Exchange.Security", "EX0043:DoNotUseBinarySoapFormatter", Justification = "Suppress warning in current code base.The usage has already been verified.")]
		internal static byte[] Serialize(MultiMailboxKeywordStatsResponse response)
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
	}
}
