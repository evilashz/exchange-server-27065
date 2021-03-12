using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000493 RID: 1171
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileReadException : LocalizedException
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x000BF8B3 File Offset: 0x000BDAB3
		public FileReadException(string fileName, int expectedBytes, int actualBytes) : base(ReplayStrings.FileReadException(fileName, expectedBytes, actualBytes))
		{
			this.fileName = fileName;
			this.expectedBytes = expectedBytes;
			this.actualBytes = actualBytes;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000BF8D8 File Offset: 0x000BDAD8
		public FileReadException(string fileName, int expectedBytes, int actualBytes, Exception innerException) : base(ReplayStrings.FileReadException(fileName, expectedBytes, actualBytes), innerException)
		{
			this.fileName = fileName;
			this.expectedBytes = expectedBytes;
			this.actualBytes = actualBytes;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000BF900 File Offset: 0x000BDB00
		protected FileReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
			this.expectedBytes = (int)info.GetValue("expectedBytes", typeof(int));
			this.actualBytes = (int)info.GetValue("actualBytes", typeof(int));
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000BF975 File Offset: 0x000BDB75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
			info.AddValue("expectedBytes", this.expectedBytes);
			info.AddValue("actualBytes", this.actualBytes);
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000BF9B2 File Offset: 0x000BDBB2
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000BF9BA File Offset: 0x000BDBBA
		public int ExpectedBytes
		{
			get
			{
				return this.expectedBytes;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000BF9C2 File Offset: 0x000BDBC2
		public int ActualBytes
		{
			get
			{
				return this.actualBytes;
			}
		}

		// Token: 0x040014E7 RID: 5351
		private readonly string fileName;

		// Token: 0x040014E8 RID: 5352
		private readonly int expectedBytes;

		// Token: 0x040014E9 RID: 5353
		private readonly int actualBytes;
	}
}
