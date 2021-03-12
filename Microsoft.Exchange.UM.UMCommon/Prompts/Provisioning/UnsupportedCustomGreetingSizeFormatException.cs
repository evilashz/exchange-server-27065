using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001CC RID: 460
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedCustomGreetingSizeFormatException : PublishingException
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x0003602E File Offset: 0x0003422E
		public UnsupportedCustomGreetingSizeFormatException(string minutes) : base(Strings.UnsupportedCustomGreetingSizeFormat(minutes))
		{
			this.minutes = minutes;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00036043 File Offset: 0x00034243
		public UnsupportedCustomGreetingSizeFormatException(string minutes, Exception innerException) : base(Strings.UnsupportedCustomGreetingSizeFormat(minutes), innerException)
		{
			this.minutes = minutes;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00036059 File Offset: 0x00034259
		protected UnsupportedCustomGreetingSizeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.minutes = (string)info.GetValue("minutes", typeof(string));
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00036083 File Offset: 0x00034283
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("minutes", this.minutes);
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0003609E File Offset: 0x0003429E
		public string Minutes
		{
			get
			{
				return this.minutes;
			}
		}

		// Token: 0x040007A0 RID: 1952
		private readonly string minutes;
	}
}
