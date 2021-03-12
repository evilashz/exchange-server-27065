using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	internal class ReplicationCursor
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x0004F405 File Offset: 0x0004D605
		public ReplicationCursor()
		{
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004F40D File Offset: 0x0004D60D
		public ReplicationCursor(Guid sourceInvocationId, long upToDatenessUsn, DateTime lastSuccessfulSyncTime, ADObjectId sourceDsa)
		{
			this.sourceInvocationId = sourceInvocationId;
			this.upToDatenessUsn = upToDatenessUsn;
			this.lastSuccessfulSyncTime = lastSuccessfulSyncTime;
			this.sourceDsa = sourceDsa;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0004F432 File Offset: 0x0004D632
		public Guid SourceInvocationId
		{
			get
			{
				return this.sourceInvocationId;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0004F43A File Offset: 0x0004D63A
		public long UpToDatenessUsn
		{
			get
			{
				return this.upToDatenessUsn;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004F442 File Offset: 0x0004D642
		public DateTime LastSuccessfulSyncTime
		{
			get
			{
				return this.lastSuccessfulSyncTime;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0004F44A File Offset: 0x0004D64A
		public ADObjectId SourceDsa
		{
			get
			{
				return this.sourceDsa;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004F454 File Offset: 0x0004D654
		public static ReplicationCursor Parse(byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length < 36)
			{
				throw new ArgumentException("value");
			}
			byte[] array = new byte[16];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			byte[] array4 = new byte[4];
			Array.Copy(value, 0, array, 0, 16);
			Guid guid = new Guid(array);
			Array.Copy(value, 16, array2, 0, 8);
			long num = BitConverter.ToInt64(array2, 0);
			Array.Copy(value, 24, array3, 0, 8);
			uint num2 = BitConverter.ToUInt32(array3, 0);
			uint num3 = BitConverter.ToUInt32(array3, 4);
			ulong fileTime = ((ulong)num3 << 32) + (ulong)num2;
			DateTime dateTime = DateTime.FromFileTimeUtc((long)fileTime);
			Array.Copy(value, 32, array4, 0, 4);
			int num4 = BitConverter.ToInt32(array4, 0);
			ADObjectId adobjectId = null;
			if (num4 > 36)
			{
				byte[] array5 = new byte[value.Length - num4];
				Array.Copy(value, num4, array5, 0, value.Length - num4);
				string text = Encoding.Unicode.GetString(array5);
				string text2 = text;
				char[] trimChars = new char[1];
				text = text2.Trim(trimChars);
				adobjectId = new ADObjectId(text);
			}
			return new ReplicationCursor(guid, num, dateTime, adobjectId);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004F569 File Offset: 0x0004D769
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.SourceInvocationId, this.UpToDatenessUsn);
		}

		// Token: 0x04000958 RID: 2392
		private Guid sourceInvocationId;

		// Token: 0x04000959 RID: 2393
		private long upToDatenessUsn;

		// Token: 0x0400095A RID: 2394
		private DateTime lastSuccessfulSyncTime;

		// Token: 0x0400095B RID: 2395
		private ADObjectId sourceDsa;
	}
}
