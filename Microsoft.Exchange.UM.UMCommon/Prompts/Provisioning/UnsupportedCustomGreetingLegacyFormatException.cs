using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001CD RID: 461
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedCustomGreetingLegacyFormatException : PublishingException
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x000360A6 File Offset: 0x000342A6
		public UnsupportedCustomGreetingLegacyFormatException(string fileName) : base(Strings.UnsupportedCustomGreetingLegacyFormat(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000360BB File Offset: 0x000342BB
		public UnsupportedCustomGreetingLegacyFormatException(string fileName, Exception innerException) : base(Strings.UnsupportedCustomGreetingLegacyFormat(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000360D1 File Offset: 0x000342D1
		protected UnsupportedCustomGreetingLegacyFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000360FB File Offset: 0x000342FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00036116 File Offset: 0x00034316
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x040007A1 RID: 1953
		private readonly string fileName;
	}
}
