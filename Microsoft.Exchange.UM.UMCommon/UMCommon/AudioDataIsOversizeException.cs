using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D0 RID: 464
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AudioDataIsOversizeException : LocalizedException
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0003617C File Offset: 0x0003437C
		public AudioDataIsOversizeException(int maxAudioDataMegabytes, long maxGreetingSizeMinutes) : base(Strings.AudioDataIsOversizeException(maxAudioDataMegabytes, maxGreetingSizeMinutes))
		{
			this.maxAudioDataMegabytes = maxAudioDataMegabytes;
			this.maxGreetingSizeMinutes = maxGreetingSizeMinutes;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00036199 File Offset: 0x00034399
		public AudioDataIsOversizeException(int maxAudioDataMegabytes, long maxGreetingSizeMinutes, Exception innerException) : base(Strings.AudioDataIsOversizeException(maxAudioDataMegabytes, maxGreetingSizeMinutes), innerException)
		{
			this.maxAudioDataMegabytes = maxAudioDataMegabytes;
			this.maxGreetingSizeMinutes = maxGreetingSizeMinutes;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000361B8 File Offset: 0x000343B8
		protected AudioDataIsOversizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maxAudioDataMegabytes = (int)info.GetValue("maxAudioDataMegabytes", typeof(int));
			this.maxGreetingSizeMinutes = (long)info.GetValue("maxGreetingSizeMinutes", typeof(long));
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003620D File Offset: 0x0003440D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maxAudioDataMegabytes", this.maxAudioDataMegabytes);
			info.AddValue("maxGreetingSizeMinutes", this.maxGreetingSizeMinutes);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00036239 File Offset: 0x00034439
		public int MaxAudioDataMegabytes
		{
			get
			{
				return this.maxAudioDataMegabytes;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00036241 File Offset: 0x00034441
		public long MaxGreetingSizeMinutes
		{
			get
			{
				return this.maxGreetingSizeMinutes;
			}
		}

		// Token: 0x040007A2 RID: 1954
		private readonly int maxAudioDataMegabytes;

		// Token: 0x040007A3 RID: 1955
		private readonly long maxGreetingSizeMinutes;
	}
}
