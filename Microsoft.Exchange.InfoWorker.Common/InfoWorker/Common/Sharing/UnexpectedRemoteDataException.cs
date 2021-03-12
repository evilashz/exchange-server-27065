using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	public sealed class UnexpectedRemoteDataException : SharingSynchronizationException
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x00050C7A File Offset: 0x0004EE7A
		public UnexpectedRemoteDataException() : base(Strings.UnexpectedRemoteDataException)
		{
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00050C87 File Offset: 0x0004EE87
		public UnexpectedRemoteDataException(Exception innerException) : base(Strings.UnexpectedRemoteDataException, innerException)
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00050C98 File Offset: 0x0004EE98
		public UnexpectedRemoteDataException(params ResponseMessageType[] items) : this()
		{
			this.items = items;
			int num = 0;
			foreach (ResponseMessageType item in items)
			{
				string key = string.Format("Item{0}", num++);
				this.Data[key] = UnexpectedRemoteDataException.ResponseMessageTypeToString(item);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00050CF4 File Offset: 0x0004EEF4
		internal ResponseMessageType[] Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00050CFC File Offset: 0x0004EEFC
		private static string ResponseMessageTypeToString(ResponseMessageType item)
		{
			if (item == null)
			{
				return "<null>";
			}
			XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				xmlSerializer.Serialize(memoryStream, item);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x04000B99 RID: 2969
		private readonly ResponseMessageType[] items;
	}
}
