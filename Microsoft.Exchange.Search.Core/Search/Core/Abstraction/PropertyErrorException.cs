using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class PropertyErrorException : OperationFailedException
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002548 File Offset: 0x00000748
		public PropertyErrorException(string property) : this(property, Strings.PropertyError(property), null)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002558 File Offset: 0x00000758
		public PropertyErrorException(string property, Exception innerException) : this(property, Strings.PropertyError(property), innerException)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002568 File Offset: 0x00000768
		protected PropertyErrorException(string property, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.property = property;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002579 File Offset: 0x00000779
		protected PropertyErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000025A3 File Offset: 0x000007A3
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000025AB File Offset: 0x000007AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
		}

		// Token: 0x04000010 RID: 16
		private readonly string property;
	}
}
