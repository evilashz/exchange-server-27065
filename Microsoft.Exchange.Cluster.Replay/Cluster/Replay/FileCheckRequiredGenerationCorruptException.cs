using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003D3 RID: 979
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckRequiredGenerationCorruptException : FileCheckException
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x000B80EF File Offset: 0x000B62EF
		public FileCheckRequiredGenerationCorruptException(string file, long min, long max) : base(ReplayStrings.FileCheckRequiredGenerationCorrupt(file, min, max))
		{
			this.file = file;
			this.min = min;
			this.max = max;
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000B8119 File Offset: 0x000B6319
		public FileCheckRequiredGenerationCorruptException(string file, long min, long max, Exception innerException) : base(ReplayStrings.FileCheckRequiredGenerationCorrupt(file, min, max), innerException)
		{
			this.file = file;
			this.min = min;
			this.max = max;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000B8148 File Offset: 0x000B6348
		protected FileCheckRequiredGenerationCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.min = (long)info.GetValue("min", typeof(long));
			this.max = (long)info.GetValue("max", typeof(long));
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000B81BD File Offset: 0x000B63BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("min", this.min);
			info.AddValue("max", this.max);
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000B81FA File Offset: 0x000B63FA
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x000B8202 File Offset: 0x000B6402
		public long Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000B820A File Offset: 0x000B640A
		public long Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x040013E2 RID: 5090
		private readonly string file;

		// Token: 0x040013E3 RID: 5091
		private readonly long min;

		// Token: 0x040013E4 RID: 5092
		private readonly long max;
	}
}
